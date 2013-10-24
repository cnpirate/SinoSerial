namespace SinoSerial
{
    partial class SinoSerial
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);

            _continue = false;
            readThread.Join();
            _serialPort.Close(); 
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbPortName = new System.Windows.Forms.ComboBox();
            this.labelPortName = new System.Windows.Forms.Label();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.labelBaudRate = new System.Windows.Forms.Label();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.labelParity = new System.Windows.Forms.Label();
            this.cbDataBits = new System.Windows.Forms.ComboBox();
            this.cbStopBits = new System.Windows.Forms.ComboBox();
            this.labelDataBits = new System.Windows.Forms.Label();
            this.labelStopBits = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tbHeader = new System.Windows.Forms.TextBox();
            this.tbCmd = new System.Windows.Forms.TextBox();
            this.tbData = new System.Windows.Forms.TextBox();
            this.labelHeader = new System.Windows.Forms.Label();
            this.labelCmd = new System.Windows.Forms.Label();
            this.labelData = new System.Windows.Forms.Label();
            this.btnSendCmd = new System.Windows.Forms.Button();
            this.tbFilePath = new System.Windows.Forms.TextBox();
            this.btnFileBrowser = new System.Windows.Forms.Button();
            this.btnFileSend = new System.Windows.Forms.Button();
            this.tbScreen = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbPortName
            // 
            this.cbPortName.FormattingEnabled = true;
            this.cbPortName.Location = new System.Drawing.Point(70, 12);
            this.cbPortName.Name = "cbPortName";
            this.cbPortName.Size = new System.Drawing.Size(69, 21);
            this.cbPortName.TabIndex = 0;
            this.cbPortName.Click += new System.EventHandler(cbPortName_click);
            // 
            // labelPortName
            // 
            this.labelPortName.AutoSize = true;
            this.labelPortName.Location = new System.Drawing.Point(8, 15);
            this.labelPortName.Name = "labelPortName";
            this.labelPortName.Size = new System.Drawing.Size(39, 13);
            this.labelPortName.TabIndex = 1;
            this.labelPortName.Text = "Serial: ";
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Items.AddRange(new object[] {
            9600,
            115200});
            this.cbBaudRate.Location = new System.Drawing.Point(68, 60);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(71, 21);
            this.cbBaudRate.TabIndex = 2;
            this.cbBaudRate.Text = "115200";
            // 
            // labelBaudRate
            // 
            this.labelBaudRate.AutoSize = true;
            this.labelBaudRate.Location = new System.Drawing.Point(2, 63);
            this.labelBaudRate.Name = "labelBaudRate";
            this.labelBaudRate.Size = new System.Drawing.Size(58, 13);
            this.labelBaudRate.TabIndex = 3;
            this.labelBaudRate.Text = "BaudRate:";
            // 
            // cbParity
            // 
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.cbParity.Location = new System.Drawing.Point(203, 60);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(82, 21);
            this.cbParity.TabIndex = 4;
            this.cbParity.Text = "Even";
            // 
            // labelParity
            // 
            this.labelParity.AutoSize = true;
            this.labelParity.Location = new System.Drawing.Point(162, 63);
            this.labelParity.Name = "labelParity";
            this.labelParity.Size = new System.Drawing.Size(36, 13);
            this.labelParity.TabIndex = 5;
            this.labelParity.Text = "Parity:";
            // 
            // cbDataBits
            // 
            this.cbDataBits.FormattingEnabled = true;
            this.cbDataBits.Items.AddRange(new object[] {
            8,
            9});
            this.cbDataBits.Location = new System.Drawing.Point(68, 112);
            this.cbDataBits.Name = "cbDataBits";
            this.cbDataBits.Size = new System.Drawing.Size(71, 21);
            this.cbDataBits.TabIndex = 6;
            this.cbDataBits.Text = "8";
            // 
            // cbStopBits
            // 
            this.cbStopBits.FormattingEnabled = true;
            this.cbStopBits.Items.AddRange(new object[] {
            "None",
            "One",
            "OnePointFive",
            "Two"});
            this.cbStopBits.Location = new System.Drawing.Point(203, 112);
            this.cbStopBits.Name = "cbStopBits";
            this.cbStopBits.Size = new System.Drawing.Size(82, 21);
            this.cbStopBits.TabIndex = 7;
            this.cbStopBits.Text = "One";
            // 
            // labelDataBits
            // 
            this.labelDataBits.AutoSize = true;
            this.labelDataBits.Location = new System.Drawing.Point(7, 115);
            this.labelDataBits.Name = "labelDataBits";
            this.labelDataBits.Size = new System.Drawing.Size(50, 13);
            this.labelDataBits.TabIndex = 8;
            this.labelDataBits.Text = "DataBits:";
            // 
            // labelStopBits
            // 
            this.labelStopBits.AutoSize = true;
            this.labelStopBits.Location = new System.Drawing.Point(150, 115);
            this.labelStopBits.Name = "labelStopBits";
            this.labelStopBits.Size = new System.Drawing.Size(49, 13);
            this.labelStopBits.TabIndex = 9;
            this.labelStopBits.Text = "StopBits:";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(169, 152);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(116, 40);
            this.btnOpen.TabIndex = 10;
            this.btnOpen.Text = "OPEN";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnClose
            // 
            this.btnClose.Enabled = false;
            this.btnClose.Location = new System.Drawing.Point(42, 152);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(103, 39);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "CLOSE";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tbHeader
            // 
            this.tbHeader.Location = new System.Drawing.Point(45, 252);
            this.tbHeader.Name = "tbHeader";
            this.tbHeader.Size = new System.Drawing.Size(60, 20);
            this.tbHeader.TabIndex = 12;
            // 
            // tbCmd
            // 
            this.tbCmd.Location = new System.Drawing.Point(111, 252);
            this.tbCmd.Name = "tbCmd";
            this.tbCmd.Size = new System.Drawing.Size(60, 20);
            this.tbCmd.TabIndex = 13;
            // 
            // tbData
            // 
            this.tbData.Location = new System.Drawing.Point(177, 252);
            this.tbData.Name = "tbData";
            this.tbData.Size = new System.Drawing.Size(108, 20);
            this.tbData.TabIndex = 14;
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Location = new System.Drawing.Point(55, 236);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(42, 13);
            this.labelHeader.TabIndex = 15;
            this.labelHeader.Text = "Header";
            // 
            // labelCmd
            // 
            this.labelCmd.AutoSize = true;
            this.labelCmd.Location = new System.Drawing.Point(114, 236);
            this.labelCmd.Name = "labelCmd";
            this.labelCmd.Size = new System.Drawing.Size(54, 13);
            this.labelCmd.TabIndex = 16;
            this.labelCmd.Text = "Command";
            // 
            // labelData
            // 
            this.labelData.AutoSize = true;
            this.labelData.Location = new System.Drawing.Point(217, 236);
            this.labelData.Name = "labelData";
            this.labelData.Size = new System.Drawing.Size(30, 13);
            this.labelData.TabIndex = 17;
            this.labelData.Text = "Data";
            // 
            // btnSendCmd
            // 
            this.btnSendCmd.Enabled = false;
            this.btnSendCmd.Location = new System.Drawing.Point(169, 293);
            this.btnSendCmd.Name = "btnSendCmd";
            this.btnSendCmd.Size = new System.Drawing.Size(116, 39);
            this.btnSendCmd.TabIndex = 18;
            this.btnSendCmd.Text = "SEND";
            this.btnSendCmd.UseVisualStyleBackColor = true;
            this.btnSendCmd.Click += new System.EventHandler(this.btnSendCmd_Click);
            // 
            // tbFilePath
            // 
            this.tbFilePath.Location = new System.Drawing.Point(8, 367);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.Size = new System.Drawing.Size(189, 20);
            this.tbFilePath.TabIndex = 19;
            // 
            // btnFileBrowser
            // 
            this.btnFileBrowser.Enabled = false;
            this.btnFileBrowser.Location = new System.Drawing.Point(203, 365);
            this.btnFileBrowser.Name = "btnFileBrowser";
            this.btnFileBrowser.Size = new System.Drawing.Size(75, 23);
            this.btnFileBrowser.TabIndex = 20;
            this.btnFileBrowser.Text = "Browser";
            this.btnFileBrowser.UseVisualStyleBackColor = true;
            this.btnFileBrowser.Click += new System.EventHandler(this.btnFileBrowser_Click);
            // 
            // btnFileSend
            // 
            this.btnFileSend.Enabled = false;
            this.btnFileSend.Location = new System.Drawing.Point(169, 423);
            this.btnFileSend.Name = "btnFileSend";
            this.btnFileSend.Size = new System.Drawing.Size(116, 35);
            this.btnFileSend.TabIndex = 21;
            this.btnFileSend.Text = "SEND";
            this.btnFileSend.UseVisualStyleBackColor = true;
            this.btnFileSend.Click += new System.EventHandler(this.btnFileSend_Click);
            // 
            // tbScreen
            // 
            this.tbScreen.Location = new System.Drawing.Point(315, 12);
            this.tbScreen.Multiline = true;
            this.tbScreen.Name = "tbScreen";
            this.tbScreen.ReadOnly = true;
            this.tbScreen.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbScreen.Size = new System.Drawing.Size(324, 401);
            this.tbScreen.TabIndex = 22;
            this.tbScreen.TextChanged += new System.EventHandler(this.tbScreen_TextChanged);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(451, 429);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(65, 23);
            this.btnClear.TabIndex = 23;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // SinoSerial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 475);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.tbScreen);
            this.Controls.Add(this.btnFileSend);
            this.Controls.Add(this.btnFileBrowser);
            this.Controls.Add(this.tbFilePath);
            this.Controls.Add(this.btnSendCmd);
            this.Controls.Add(this.labelData);
            this.Controls.Add(this.labelCmd);
            this.Controls.Add(this.labelHeader);
            this.Controls.Add(this.tbData);
            this.Controls.Add(this.tbCmd);
            this.Controls.Add(this.tbHeader);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.labelStopBits);
            this.Controls.Add(this.labelDataBits);
            this.Controls.Add(this.cbStopBits);
            this.Controls.Add(this.cbDataBits);
            this.Controls.Add(this.labelParity);
            this.Controls.Add(this.cbParity);
            this.Controls.Add(this.labelBaudRate);
            this.Controls.Add(this.cbBaudRate);
            this.Controls.Add(this.labelPortName);
            this.Controls.Add(this.cbPortName);
            this.Name = "SinoSerial";
            this.Text = "SinoSerial";
            this.Load += new System.EventHandler(this.SinoSerial_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPortName;
        private System.Windows.Forms.Label labelPortName;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.Label labelBaudRate;
        private System.Windows.Forms.ComboBox cbParity;
        private System.Windows.Forms.Label labelParity;
        private System.Windows.Forms.ComboBox cbDataBits;
        private System.Windows.Forms.ComboBox cbStopBits;
        private System.Windows.Forms.Label labelDataBits;
        private System.Windows.Forms.Label labelStopBits;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox tbHeader;
        private System.Windows.Forms.TextBox tbCmd;
        private System.Windows.Forms.TextBox tbData;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Label labelCmd;
        private System.Windows.Forms.Label labelData;
        private System.Windows.Forms.Button btnSendCmd;
        private System.Windows.Forms.TextBox tbFilePath;
        private System.Windows.Forms.Button btnFileBrowser;
        private System.Windows.Forms.Button btnFileSend;
        private System.Windows.Forms.TextBox tbScreen;
        private System.Windows.Forms.Button btnClear;
    }
}

