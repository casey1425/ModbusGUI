using System;
using System.Drawing;
using System.IO.Ports; // 시리얼 포트 확인용
using System.Windows.Forms;
using ModbusGUI.Models;
using ModbusGUI.Protocol;
using ModbusGUI.Transports;

namespace ModbusGUI
{
    public partial class Form1 : Form
    {
        // 전역 변수: 연결 객체는 메인 폼이 관리하고, 자식 창들에게 빌려줍니다.
        private IModbusTransport _transport;
        private ModbusClient _modbusClient;

        public Form1()
        {
            InitializeComponent();

            // 폼이 로드될 때 콤보박스 아이템을 코드로 채워줍니다. (디자이너에서 안 해도 됨)
            InitializeFunctionCodeComboBox();
        }

        // 콤보박스 초기화 함수
        private void InitializeFunctionCodeComboBox()
        {
            // 디자이너에 'cbFunctionCode'라는 ComboBox를 추가했다고 가정하거나,
            // 없다면 btnRead 대신 사용할 버튼 근처에 만들어주세요.
            if (cbFunctionCode.Items.Count == 0)
            {
                cbFunctionCode.Items.Add("01 Read Coils (Output)");
                cbFunctionCode.Items.Add("02 Read Discrete Inputs (Input)");
                cbFunctionCode.Items.Add("03 Read Holding Registers");
                cbFunctionCode.Items.Add("04 Read Input Registers");
                cbFunctionCode.SelectedIndex = 2; // 기본값: 03 Holding Register
            }
        }

        // 1. [연결] 버튼 클릭
        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                btnConnect.Enabled = false; // 중복 클릭 방지

                // A. TCP 모드
                if (rbTcp.Checked)
                {
                    Log("모드: TCP");
                    string ip = txtIp.Text;
                    if (!int.TryParse(txtPort.Text, out int port))
                    {
                        MessageBox.Show("포트 번호를 숫자로 입력해주세요.");
                        btnConnect.Enabled = true;
                        return;
                    }

                    Log($"TCP 연결 시도: {ip}:{port}...");
                    _transport = new TcpTransport(ip, port);
                }
                // B. Serial 모드 (수정된 로직 적용)
                else
                {
                    Log("모드: Serial");
                    string comPort = txtComPort.Text;
                    if (!int.TryParse(txtBaud.Text, out int baud))
                    {
                        MessageBox.Show("보드레이트(속도)를 숫자로 입력해주세요.");
                        btnConnect.Enabled = true;
                        return;
                    }

                    Log($"Serial 연결 시도: {comPort} ({baud})...");

                    // ★ 중요: SerialTransport 생성 (Parity.None은 SerialTransport.cs에서 수정했어야 함)
                    _transport = new SerialTransport(comPort, baud);
                }

                // C. 공통 연결 로직
                await _transport.ConnectAsync();
                _modbusClient = new ModbusClient(_transport);

                Log(">> 연결 성공!");

                // UI 상태 변경
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;

                // 설정값 변경 막기
                rbTcp.Enabled = false;
                rbSerial.Enabled = false;

                // (참고) 이제 데이터를 여기서 읽지 않으므로 btnRead는 활성화할 필요 없음
                // 대신 새 창 띄우기 버튼을 활성화하면 됨
            }
            catch (Exception ex)
            {
                Log($"[오류] 연결 실패: {ex.Message}", true);
                btnConnect.Enabled = true; // 다시 활성화

                if (_transport != null)
                {
                    _transport.Disconnect();
                    _transport = null;
                }
            }
        }

        // 2. [해제] 버튼 클릭
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (_transport != null)
            {
                _transport.Disconnect();
                _transport = null;
                _modbusClient = null;
            }

            Log("연결 해제됨.");

            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;

            // 설정값 변경 허용
            rbTcp.Enabled = true;
            rbSerial.Enabled = true;
        }

        // 3. [모니터링 창 띄우기] 버튼 클릭
        // ★ 디자이너에서 기존 [읽기] 버튼의 Text를 "창 띄우기"로 바꾸고
        // 이 함수를 연결하거나, 새로 버튼을 만드세요.
        private void btnOpenWindow_Click(object sender, EventArgs e)
        {
            // 1. 연결 확인
            if (_modbusClient == null)
            {
                MessageBox.Show("먼저 통신 연결을 해주세요.");
                return;
            }

            // 2. 입력값 파싱
            if (!byte.TryParse(txtUnitId.Text, out byte unitId) ||
                !ushort.TryParse(txtStartAddr.Text, out ushort startAddr) ||
                !ushort.TryParse(txtCount.Text, out ushort count))
            {
                MessageBox.Show("ID, Address, Count를 올바른 숫자로 입력해주세요.");
                return;
            }

            // 3. Function Code 확인
            if (cbFunctionCode.SelectedIndex < 0)
            {
                MessageBox.Show("Function Code를 콤보박스에서 선택해주세요.");
                return;
            }

            try
            {
                // 콤보박스 텍스트("03 Read...")에서 앞 2글자("03")만 잘라서 숫자로 변환
                string selectedText = cbFunctionCode.SelectedItem.ToString();
                int fc = int.Parse(selectedText.Substring(0, 2));

                // 4. 모니터링 폼 생성 및 표시 (비동기가 아님, 창만 띄움)
                // ★ MonitorForm 클래스가 없으면 여기서 빨간줄이 뜹니다.
                MonitorForm monitor = new MonitorForm(_modbusClient, selectedText, fc, unitId, startAddr, count);
                monitor.Show(); // Show()는 새 창을 띄우고, ShowDialog()는 새 창 닫을 때까지 멈춤

                Log($"새 창 열림: {selectedText} (Addr: {startAddr})");
            }
            catch (Exception ex)
            {
                MessageBox.Show("창 열기 실패: " + ex.Message);
            }
        }

        // 로그 출력 함수
        private void Log(string msg, bool isError = false)
        {
            if (rtbLog.InvokeRequired)
            {
                rtbLog.Invoke(new Action(() => Log(msg, isError)));
                return;
            }

            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.SelectionLength = 0;
            rtbLog.SelectionColor = isError ? Color.Red : Color.Black;
            rtbLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\r\n");
            rtbLog.ScrollToCaret();
        }

        // 폼 닫을 때 연결 정리
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _transport?.Disconnect();
            base.OnFormClosing(e);
        }

        // 패널 전환 로직
        private void rbTcp_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTcp.Checked)
            {
                panelTcp.Visible = true;
                panelSerial.Visible = false;
                panelTcp.BringToFront();
            }
        }

        private void rbSerial_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSerial.Checked)
            {
                panelTcp.Visible = false;
                panelSerial.Visible = true;
                panelSerial.BringToFront();
            }
        }

        // (필요 시) 폼 로드 이벤트
        private void Form1_Load(object sender, EventArgs e)
        {
            // 초기 설정
            rbTcp.Checked = true;
        }
    }
}