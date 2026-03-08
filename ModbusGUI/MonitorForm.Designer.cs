namespace ModbusGUI
{
    partial class MonitorForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.btnRefresh = new System.Windows.Forms.Button();
            this.rtbDisplay = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnRefresh (상단 새로고침 버튼)
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(400, 40);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "새로고침 (Refresh)";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // rtbDisplay (데이터 보여주는 화면)
            // 
            this.rtbDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDisplay.Font = new System.Drawing.Font("Consolas", 10F); // 고정폭 글꼴 사용
            this.rtbDisplay.Location = new System.Drawing.Point(0, 40);
            this.rtbDisplay.Name = "rtbDisplay";
            this.rtbDisplay.Size = new System.Drawing.Size(400, 360);
            this.rtbDisplay.TabIndex = 1;
            this.rtbDisplay.Text = "";
            // 
            // MonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Controls.Add(this.rtbDisplay);
            this.Controls.Add(this.btnRefresh);
            this.Name = "MonitorForm";
            this.Text = "Monitor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.RichTextBox rtbDisplay;
    }
}