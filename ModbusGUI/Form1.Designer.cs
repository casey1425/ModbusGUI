namespace ModbusGUI
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.rbTcp = new System.Windows.Forms.RadioButton();
            this.rbSerial = new System.Windows.Forms.RadioButton();
            this.panelTcp = new System.Windows.Forms.Panel();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblIp = new System.Windows.Forms.Label();
            this.panelSerial = new System.Windows.Forms.Panel();
            this.txtBaud = new System.Windows.Forms.TextBox();
            this.txtComPort = new System.Windows.Forms.TextBox();
            this.lblBaud = new System.Windows.Forms.Label();
            this.lblCom = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.grpMonitor = new System.Windows.Forms.GroupBox();
            this.cbFunctionCode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenWindow = new System.Windows.Forms.Button();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.txtStartAddr = new System.Windows.Forms.TextBox();
            this.txtUnitId = new System.Windows.Forms.TextBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblAddr = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.panelTcp.SuspendLayout();
            this.panelSerial.SuspendLayout();
            this.grpMonitor.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbTcp (TCP 선택 라디오 버튼)
            // 
            this.rbTcp.AutoSize = true;
            this.rbTcp.Checked = true;
            this.rbTcp.Location = new System.Drawing.Point(24, 21);
            this.rbTcp.Name = "rbTcp";
            this.rbTcp.Size = new System.Drawing.Size(107, 19);
            this.rbTcp.TabIndex = 0;
            this.rbTcp.TabStop = true;
            this.rbTcp.Text = "TCP/IP Mode";
            this.rbTcp.UseVisualStyleBackColor = true;
            this.rbTcp.CheckedChanged += new System.EventHandler(this.rbTcp_CheckedChanged);
            // 
            // rbSerial (Serial 선택 라디오 버튼)
            // 
            this.rbSerial.AutoSize = true;
            this.rbSerial.Location = new System.Drawing.Point(146, 21);
            this.rbSerial.Name = "rbSerial";
            this.rbSerial.Size = new System.Drawing.Size(127, 19);
            this.rbSerial.TabIndex = 1;
            this.rbSerial.Text = "Serial (RTU) Mode";
            this.rbSerial.UseVisualStyleBackColor = true;
            this.rbSerial.CheckedChanged += new System.EventHandler(this.rbSerial_CheckedChanged);
            // 
            // panelTcp (TCP 설정 패널)
            // 
            this.panelTcp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTcp.Controls.Add(this.txtPort);
            this.panelTcp.Controls.Add(this.txtIp);
            this.panelTcp.Controls.Add(this.lblPort);
            this.panelTcp.Controls.Add(this.lblIp);
            this.panelTcp.Location = new System.Drawing.Point(24, 55);
            this.panelTcp.Name = "panelTcp";
            this.panelTcp.Size = new System.Drawing.Size(260, 100);
            this.panelTcp.TabIndex = 2;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(85, 54);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(155, 23);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "502";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(85, 17);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(155, 23);
            this.txtIp.TabIndex = 2;
            this.txtIp.Text = "127.0.0.1";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(15, 57);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(29, 15);
            this.lblPort.TabIndex = 1;
            this.lblPort.Text = "Port";
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Location = new System.Drawing.Point(15, 20);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(20, 15);
            this.lblIp.TabIndex = 0;
            this.lblIp.Text = "IP";
            // 
            // panelSerial (시리얼 설정 패널 - 위치를 TCP와 겹쳐놓음)
            // 
            this.panelSerial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSerial.Controls.Add(this.txtBaud);
            this.panelSerial.Controls.Add(this.txtComPort);
            this.panelSerial.Controls.Add(this.lblBaud);
            this.panelSerial.Controls.Add(this.lblCom);
            this.panelSerial.Location = new System.Drawing.Point(24, 55);
            this.panelSerial.Name = "panelSerial";
            this.panelSerial.Size = new System.Drawing.Size(260, 100);
            this.panelSerial.TabIndex = 3;
            this.panelSerial.Visible = false; // 기본은 숨김
            // 
            // txtBaud
            // 
            this.txtBaud.Location = new System.Drawing.Point(85, 54);
            this.txtBaud.Name = "txtBaud";
            this.txtBaud.Size = new System.Drawing.Size(155, 23);
            this.txtBaud.TabIndex = 3;
            this.txtBaud.Text = "9600";
            // 
            // txtComPort
            // 
            this.txtComPort.Location = new System.Drawing.Point(85, 17);
            this.txtComPort.Name = "txtComPort";
            this.txtComPort.Size = new System.Drawing.Size(155, 23);
            this.txtComPort.TabIndex = 2;
            this.txtComPort.Text = "COM3";
            // 
            // lblBaud
            // 
            this.lblBaud.AutoSize = true;
            this.lblBaud.Location = new System.Drawing.Point(15, 57);
            this.lblBaud.Name = "lblBaud";
            this.lblBaud.Size = new System.Drawing.Size(35, 15);
            this.lblBaud.TabIndex = 1;
            this.lblBaud.Text = "Baud";
            // 
            // lblCom
            // 
            this.lblCom.AutoSize = true;
            this.lblCom.Location = new System.Drawing.Point(15, 20);
            this.lblCom.Name = "lblCom";
            this.lblCom.Size = new System.Drawing.Size(35, 15);
            this.lblCom.TabIndex = 0;
            this.lblCom.Text = "Port";
            // 
            // btnConnect (연결 버튼)
            // 
            this.btnConnect.Location = new System.Drawing.Point(299, 55);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(95, 45);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "연 결";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect (해제 버튼)
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(299, 110);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(95, 45);
            this.btnDisconnect.TabIndex = 5;
            this.btnDisconnect.Text = "해 제";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // grpMonitor (모니터링 설정 그룹박스)
            // 
            this.grpMonitor.Controls.Add(this.cbFunctionCode);
            this.grpMonitor.Controls.Add(this.label1);
            this.grpMonitor.Controls.Add(this.btnOpenWindow);
            this.grpMonitor.Controls.Add(this.txtCount);
            this.grpMonitor.Controls.Add(this.txtStartAddr);
            this.grpMonitor.Controls.Add(this.txtUnitId);
            this.grpMonitor.Controls.Add(this.lblCount);
            this.grpMonitor.Controls.Add(this.lblAddr);
            this.grpMonitor.Controls.Add(this.lblId);
            this.grpMonitor.Location = new System.Drawing.Point(24, 172);
            this.grpMonitor.Name = "grpMonitor";
            this.grpMonitor.Size = new System.Drawing.Size(370, 185);
            this.grpMonitor.TabIndex = 6;
            this.grpMonitor.TabStop = false;
            this.grpMonitor.Text = "모니터링 설정";
            // 
            // cbFunctionCode (기능 코드 콤보박스)
            // 
            this.cbFunctionCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFunctionCode.FormattingEnabled = true;
            this.cbFunctionCode.Location = new System.Drawing.Point(86, 29);
            this.cbFunctionCode.Name = "cbFunctionCode";
            this.cbFunctionCode.Size = new System.Drawing.Size(264, 23);
            this.cbFunctionCode.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Function";
            // 
            // btnOpenWindow (창 띄우기 버튼)
            // 
            this.btnOpenWindow.Location = new System.Drawing.Point(230, 68);
            this.btnOpenWindow.Name = "btnOpenWindow";
            this.btnOpenWindow.Size = new System.Drawing.Size(120, 100);
            this.btnOpenWindow.TabIndex = 6;
            this.btnOpenWindow.Text = "모니터링\r\n창 띄우기";
            this.btnOpenWindow.UseVisualStyleBackColor = true;
            this.btnOpenWindow.Click += new System.EventHandler(this.btnOpenWindow_Click);
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(86, 143);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(120, 23);
            this.txtCount.TabIndex = 5;
            this.txtCount.Text = "10";
            // 
            // txtStartAddr
            // 
            this.txtStartAddr.Location = new System.Drawing.Point(86, 105);
            this.txtStartAddr.Name = "txtStartAddr";
            this.txtStartAddr.Size = new System.Drawing.Size(120, 23);
            this.txtStartAddr.TabIndex = 4;
            this.txtStartAddr.Text = "0";
            // 
            // txtUnitId
            // 
            this.txtUnitId.Location = new System.Drawing.Point(86, 68);
            this.txtUnitId.Name = "txtUnitId";
            this.txtUnitId.Size = new System.Drawing.Size(120, 23);
            this.txtUnitId.TabIndex = 3;
            this.txtUnitId.Text = "1";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(16, 146);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(40, 15);
            this.lblCount.TabIndex = 2;
            this.lblCount.Text = "Count";
            // 
            // lblAddr
            // 
            this.lblAddr.AutoSize = true;
            this.lblAddr.Location = new System.Drawing.Point(16, 108);
            this.lblAddr.Name = "lblAddr";
            this.lblAddr.Size = new System.Drawing.Size(51, 15);
            this.lblAddr.TabIndex = 1;
            this.lblAddr.Text = "Address";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(16, 71);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(43, 15);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "Unit ID";
            // 
            // rtbLog (로그 박스)
            // 
            this.rtbLog.Location = new System.Drawing.Point(24, 375);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(370, 150);
            this.rtbLog.TabIndex = 7;
            this.rtbLog.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 540);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.grpMonitor);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.panelSerial);
            this.Controls.Add(this.panelTcp);
            this.Controls.Add(this.rbSerial);
            this.Controls.Add(this.rbTcp);
            this.Name = "Form1";
            this.Text = "Modbus GUI Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelTcp.ResumeLayout(false);
            this.panelTcp.PerformLayout();
            this.panelSerial.ResumeLayout(false);
            this.panelSerial.PerformLayout();
            this.grpMonitor.ResumeLayout(false);
            this.grpMonitor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbTcp;
        private System.Windows.Forms.RadioButton rbSerial;
        private System.Windows.Forms.Panel panelTcp;
        private System.Windows.Forms.Panel panelSerial;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtComPort;
        private System.Windows.Forms.TextBox txtBaud;
        private System.Windows.Forms.Label lblCom;
        private System.Windows.Forms.Label lblBaud;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.GroupBox grpMonitor;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblAddr;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.TextBox txtUnitId;
        private System.Windows.Forms.TextBox txtStartAddr;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Button btnOpenWindow;
        private System.Windows.Forms.ComboBox cbFunctionCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbLog;
    }
}