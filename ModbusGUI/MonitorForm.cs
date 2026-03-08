using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ModbusGUI.Models;
using ModbusGUI.Protocol;

namespace ModbusGUI
{
    public partial class MonitorForm : Form
    {
        private readonly ModbusClient _client;
        private readonly int _functionCode;
        private readonly byte _unitId;
        private readonly ushort _startAddr;
        private readonly ushort _count;

        private System.Windows.Forms.Timer _timer;
        private bool _isMonitoring = false;
        private bool _isBusy = false;

        public MonitorForm(ModbusClient client, string modeName, int fc, byte unitId, ushort startAddr, ushort count)
        {
            InitializeComponent();

            _client = client;
            _functionCode = fc;
            _unitId = unitId;
            _startAddr = startAddr;
            _count = count;

            this.Text = $"{modeName} (ID:{unitId}, Addr:{startAddr}, Len:{count})";

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 500; // 500ms 
            _timer.Tick += Timer_Tick; // 시간이 되면 실행할 함수 연결

            // 버튼 텍스트 변경
            btnRefresh.Text = "▶ 모니터링 시작 (Start)";
            Log("대기 중... [시작] 버튼을 누르면 0.5초마다 갱신됩니다.");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (_isMonitoring)
            {
                // 멈추기
                StopMonitoring();
            }
            else
            {
                // 시작하기
                StartMonitoring();
            }
        }

        private void StartMonitoring()
        {
            _isMonitoring = true;
            btnRefresh.Text = "■ 모니터링 중지 (Stop)";
            btnRefresh.BackColor = Color.LightPink; 
            _timer.Start();
            Log(">> 자동 갱신 시작됨 (Interval: 500ms)");
        }

        private void StopMonitoring()
        {
            _isMonitoring = false;
            _timer.Stop(); // 타이머 정지
            btnRefresh.Text = "▶ 모니터링 시작 (Start)";
            btnRefresh.BackColor = SystemColors.Control;
            Log(">> 자동 갱신 중지됨");
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            if (_isBusy) return;

            _isBusy = true;

            try
            {
                ModbusResult result = null;

                // 통신 요청 (기존 로직 동일)
                switch (_functionCode)
                {
                    case 1: result = await _client.ReadCoilsAsync(_unitId, _startAddr, _count); break;
                    case 2: result = await _client.ReadDiscreteInputsAsync(_unitId, _startAddr, _count); break;
                    case 3: result = await _client.ReadHoldingRegistersAsync(_unitId, _startAddr, _count); break;
                    case 4: result = await _client.ReadInputRegistersAsync(_unitId, _startAddr, _count); break;
                }

                // 결과 처리
                if (result != null && result.IsSuccess)
                {
                    PrintData(result.Data);
                }
                else
                {
                    // 에러가 나면 로그만 찍고 멈추지는 않음 (계속 재시도)
                    Log($"[통신 실패] {result?.ErrorMessage}", true);
                }
            }
            catch (Exception ex)
            {
                Log($"[오류] {ex.Message}", true);
            }
            finally
            {
                _isBusy = false;
            }
        }

        // 데이터 출력 
        private void PrintData(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"[{DateTime.Now:HH:mm:ss.fff}] 수신 완료 ({data.Length} bytes)"); // 밀리초까지 표시

            if (_functionCode == 1 || _functionCode == 2)
            {
                for (int i = 0; i < _count; i++)
                {
                    int byteIndex = i / 8;
                    int bitIndex = i % 8;
                    if (byteIndex < data.Length)
                    {
                        bool isOne = ((data[byteIndex] >> bitIndex) & 1) == 1;
                        int currentAddr = _startAddr + i;
                        string status = isOne ? "ON" : "OFF";
                        sb.AppendLine($"   [{currentAddr:0000}] : {status}");
                    }
                }
            }
            else
            {
                for (int i = 0; i < data.Length; i += 2)
                {
                    if (i + 1 < data.Length)
                    {
                        ushort val = (ushort)((data[i] << 8) | data[i + 1]);
                        int currentAddr = _startAddr + (i / 2);
                        sb.AppendLine($"   [{currentAddr:0000}] : {val,5}  (0x{val:X4})");
                    }
                }
            }
            rtbDisplay.Text = sb.ToString();
        }

        private void Log(string msg, bool isError = false)
        {
            // 로그창이 너무 길어지면 위쪽 잘라내기
            if (rtbDisplay.TextLength > 5000) rtbDisplay.Clear();

            rtbDisplay.SelectionColor = isError ? Color.Red : Color.Black;
            rtbDisplay.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\r\n");
            rtbDisplay.ScrollToCaret();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _timer.Stop();
            _timer.Dispose();
            base.OnFormClosing(e);
        }
    }
}