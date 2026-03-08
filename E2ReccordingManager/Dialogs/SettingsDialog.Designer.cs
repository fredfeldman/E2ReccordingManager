namespace E2ReccordingManager.Dialogs
{
    partial class SettingsDialog
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConversion = new System.Windows.Forms.TabPage();
            this.groupBoxQuality = new System.Windows.Forms.GroupBox();
            this.lblQualityInfo = new System.Windows.Forms.Label();
            this.lblMaxBitrate = new System.Windows.Forms.Label();
            this.numericMaxBitrate = new System.Windows.Forms.NumericUpDown();
            this.radioLow = new System.Windows.Forms.RadioButton();
            this.radioBalanced = new System.Windows.Forms.RadioButton();
            this.radioHigh = new System.Windows.Forms.RadioButton();
            this.groupBoxFFmpeg = new System.Windows.Forms.GroupBox();
            this.btnTestFFmpeg = new System.Windows.Forms.Button();
            this.btnBrowseFFmpeg = new System.Windows.Forms.Button();
            this.txtFFmpegPath = new System.Windows.Forms.TextBox();
            this.lblFFmpegPath = new System.Windows.Forms.Label();
            this.checkBoxUseHardwareAcceleration = new System.Windows.Forms.CheckBox();
            this.checkBoxDeleteAfterConversion = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoConvert = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPageConversion.SuspendLayout();
            this.groupBoxQuality.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxBitrate)).BeginInit();
            this.groupBoxFFmpeg.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConversion);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(706, 520);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConversion
            // 
            this.tabPageConversion.Controls.Add(this.groupBoxQuality);
            this.tabPageConversion.Controls.Add(this.groupBoxFFmpeg);
            this.tabPageConversion.Controls.Add(this.checkBoxUseHardwareAcceleration);
            this.tabPageConversion.Controls.Add(this.checkBoxDeleteAfterConversion);
            this.tabPageConversion.Controls.Add(this.checkBoxAutoConvert);
            this.tabPageConversion.Location = new System.Drawing.Point(4, 34);
            this.tabPageConversion.Name = "tabPageConversion";
            this.tabPageConversion.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConversion.Size = new System.Drawing.Size(698, 482);
            this.tabPageConversion.TabIndex = 0;
            this.tabPageConversion.Text = "Conversion";
            this.tabPageConversion.UseVisualStyleBackColor = true;
            // 
            // groupBoxQuality
            // 
            this.groupBoxQuality.Controls.Add(this.lblQualityInfo);
            this.groupBoxQuality.Controls.Add(this.lblMaxBitrate);
            this.groupBoxQuality.Controls.Add(this.numericMaxBitrate);
            this.groupBoxQuality.Controls.Add(this.radioLow);
            this.groupBoxQuality.Controls.Add(this.radioBalanced);
            this.groupBoxQuality.Controls.Add(this.radioHigh);
            this.groupBoxQuality.Location = new System.Drawing.Point(15, 270);
            this.groupBoxQuality.Name = "groupBoxQuality";
            this.groupBoxQuality.Size = new System.Drawing.Size(665, 195);
            this.groupBoxQuality.TabIndex = 4;
            this.groupBoxQuality.TabStop = false;
            this.groupBoxQuality.Text = "Quality Settings";
            // 
            // lblQualityInfo
            // 
            this.lblQualityInfo.AutoSize = true;
            this.lblQualityInfo.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblQualityInfo.Location = new System.Drawing.Point(15, 145);
            this.lblQualityInfo.MaximumSize = new System.Drawing.Size(635, 0);
            this.lblQualityInfo.Name = "lblQualityInfo";
            this.lblQualityInfo.Size = new System.Drawing.Size(631, 50);
            this.lblQualityInfo.TabIndex = 5;
            this.lblQualityInfo.Text = "High: Best quality, largest file size | Balanced: Good quality, moderate size | Low: Lower quality, smallest size\r\nNote: Hardware acceleration uses NVENC (NVIDIA GPU required)";
            // 
            // lblMaxBitrate
            // 
            this.lblMaxBitrate.AutoSize = true;
            this.lblMaxBitrate.Location = new System.Drawing.Point(15, 105);
            this.lblMaxBitrate.Name = "lblMaxBitrate";
            this.lblMaxBitrate.Size = new System.Drawing.Size(163, 25);
            this.lblMaxBitrate.TabIndex = 3;
            this.lblMaxBitrate.Text = "Max Bitrate (Mbps):";
            // 
            // numericMaxBitrate
            // 
            this.numericMaxBitrate.Location = new System.Drawing.Point(184, 103);
            this.numericMaxBitrate.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericMaxBitrate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMaxBitrate.Name = "numericMaxBitrate";
            this.numericMaxBitrate.Size = new System.Drawing.Size(100, 31);
            this.numericMaxBitrate.TabIndex = 4;
            this.numericMaxBitrate.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // radioLow
            // 
            this.radioLow.AutoSize = true;
            this.radioLow.Location = new System.Drawing.Point(362, 35);
            this.radioLow.Name = "radioLow";
            this.radioLow.Size = new System.Drawing.Size(70, 29);
            this.radioLow.TabIndex = 2;
            this.radioLow.Text = "Low";
            this.radioLow.UseVisualStyleBackColor = true;
            // 
            // radioBalanced
            // 
            this.radioBalanced.AutoSize = true;
            this.radioBalanced.Checked = true;
            this.radioBalanced.Location = new System.Drawing.Point(184, 35);
            this.radioBalanced.Name = "radioBalanced";
            this.radioBalanced.Size = new System.Drawing.Size(113, 29);
            this.radioBalanced.TabIndex = 1;
            this.radioBalanced.TabStop = true;
            this.radioBalanced.Text = "Balanced";
            this.radioBalanced.UseVisualStyleBackColor = true;
            // 
            // radioHigh
            // 
            this.radioHigh.AutoSize = true;
            this.radioHigh.Location = new System.Drawing.Point(15, 35);
            this.radioHigh.Name = "radioHigh";
            this.radioHigh.Size = new System.Drawing.Size(78, 29);
            this.radioHigh.TabIndex = 0;
            this.radioHigh.Text = "High";
            this.radioHigh.UseVisualStyleBackColor = true;
            // 
            // groupBoxFFmpeg
            // 
            this.groupBoxFFmpeg.Controls.Add(this.btnTestFFmpeg);
            this.groupBoxFFmpeg.Controls.Add(this.btnBrowseFFmpeg);
            this.groupBoxFFmpeg.Controls.Add(this.txtFFmpegPath);
            this.groupBoxFFmpeg.Controls.Add(this.lblFFmpegPath);
            this.groupBoxFFmpeg.Location = new System.Drawing.Point(15, 140);
            this.groupBoxFFmpeg.Name = "groupBoxFFmpeg";
            this.groupBoxFFmpeg.Size = new System.Drawing.Size(665, 115);
            this.groupBoxFFmpeg.TabIndex = 3;
            this.groupBoxFFmpeg.TabStop = false;
            this.groupBoxFFmpeg.Text = "FFmpeg Settings";
            // 
            // btnTestFFmpeg
            // 
            this.btnTestFFmpeg.Location = new System.Drawing.Point(540, 60);
            this.btnTestFFmpeg.Name = "btnTestFFmpeg";
            this.btnTestFFmpeg.Size = new System.Drawing.Size(100, 35);
            this.btnTestFFmpeg.TabIndex = 3;
            this.btnTestFFmpeg.Text = "Test";
            this.btnTestFFmpeg.UseVisualStyleBackColor = true;
            this.btnTestFFmpeg.Click += new System.EventHandler(this.BtnTestFFmpeg_Click);
            // 
            // btnBrowseFFmpeg
            // 
            this.btnBrowseFFmpeg.Location = new System.Drawing.Point(615, 30);
            this.btnBrowseFFmpeg.Name = "btnBrowseFFmpeg";
            this.btnBrowseFFmpeg.Size = new System.Drawing.Size(25, 25);
            this.btnBrowseFFmpeg.TabIndex = 2;
            this.btnBrowseFFmpeg.Text = "...";
            this.btnBrowseFFmpeg.UseVisualStyleBackColor = true;
            this.btnBrowseFFmpeg.Click += new System.EventHandler(this.BtnBrowseFFmpeg_Click);
            // 
            // txtFFmpegPath
            // 
            this.txtFFmpegPath.Location = new System.Drawing.Point(128, 30);
            this.txtFFmpegPath.Name = "txtFFmpegPath";
            this.txtFFmpegPath.Size = new System.Drawing.Size(481, 31);
            this.txtFFmpegPath.TabIndex = 1;
            // 
            // lblFFmpegPath
            // 
            this.lblFFmpegPath.AutoSize = true;
            this.lblFFmpegPath.Location = new System.Drawing.Point(15, 33);
            this.lblFFmpegPath.Name = "lblFFmpegPath";
            this.lblFFmpegPath.Size = new System.Drawing.Size(107, 25);
            this.lblFFmpegPath.TabIndex = 0;
            this.lblFFmpegPath.Text = "FFmpeg Path:";
            // 
            // checkBoxUseHardwareAcceleration
            // 
            this.checkBoxUseHardwareAcceleration.AutoSize = true;
            this.checkBoxUseHardwareAcceleration.Checked = true;
            this.checkBoxUseHardwareAcceleration.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseHardwareAcceleration.Location = new System.Drawing.Point(15, 90);
            this.checkBoxUseHardwareAcceleration.Name = "checkBoxUseHardwareAcceleration";
            this.checkBoxUseHardwareAcceleration.Size = new System.Drawing.Size(399, 29);
            this.checkBoxUseHardwareAcceleration.TabIndex = 2;
            this.checkBoxUseHardwareAcceleration.Text = "Use Hardware Acceleration (NVIDIA NVENC)";
            this.checkBoxUseHardwareAcceleration.UseVisualStyleBackColor = true;
            // 
            // checkBoxDeleteAfterConversion
            // 
            this.checkBoxDeleteAfterConversion.AutoSize = true;
            this.checkBoxDeleteAfterConversion.Location = new System.Drawing.Point(15, 55);
            this.checkBoxDeleteAfterConversion.Name = "checkBoxDeleteAfterConversion";
            this.checkBoxDeleteAfterConversion.Size = new System.Drawing.Size(316, 29);
            this.checkBoxDeleteAfterConversion.TabIndex = 1;
            this.checkBoxDeleteAfterConversion.Text = "Delete .TS files after conversion";
            this.checkBoxDeleteAfterConversion.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoConvert
            // 
            this.checkBoxAutoConvert.AutoSize = true;
            this.checkBoxAutoConvert.Location = new System.Drawing.Point(15, 20);
            this.checkBoxAutoConvert.Name = "checkBoxAutoConvert";
            this.checkBoxAutoConvert.Size = new System.Drawing.Size(374, 29);
            this.checkBoxAutoConvert.TabIndex = 0;
            this.checkBoxAutoConvert.Text = "Automatically convert .TS files to .MP4";
            this.checkBoxAutoConvert.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(518, 548);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(94, 35);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(624, 548);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 35);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 595);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.tabControl.ResumeLayout(false);
            this.tabPageConversion.ResumeLayout(false);
            this.tabPageConversion.PerformLayout();
            this.groupBoxQuality.ResumeLayout(false);
            this.groupBoxQuality.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxBitrate)).EndInit();
            this.groupBoxFFmpeg.ResumeLayout(false);
            this.groupBoxFFmpeg.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConversion;
        private System.Windows.Forms.CheckBox checkBoxAutoConvert;
        private System.Windows.Forms.CheckBox checkBoxDeleteAfterConversion;
        private System.Windows.Forms.CheckBox checkBoxUseHardwareAcceleration;
        private System.Windows.Forms.GroupBox groupBoxFFmpeg;
        private System.Windows.Forms.Label lblFFmpegPath;
        private System.Windows.Forms.TextBox txtFFmpegPath;
        private System.Windows.Forms.Button btnBrowseFFmpeg;
        private System.Windows.Forms.Button btnTestFFmpeg;
        private System.Windows.Forms.GroupBox groupBoxQuality;
        private System.Windows.Forms.RadioButton radioHigh;
        private System.Windows.Forms.RadioButton radioBalanced;
        private System.Windows.Forms.RadioButton radioLow;
        private System.Windows.Forms.Label lblMaxBitrate;
        private System.Windows.Forms.NumericUpDown numericMaxBitrate;
        private System.Windows.Forms.Label lblQualityInfo;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}
