namespace E2ReccordingManager.Dialogs
{
    partial class DownloadProgressDialog
    {
        private System.ComponentModel.IContainer components = null;
        private TabControl tabControl;
        private TabPage tabPageDownload;
        private TabPage tabPageConversion;
        private Label lblStatus;
        private Label lblCurrentFile;
        private Label lblProgress;
        private Label lblFileProgress;
        private ProgressBar progressBarTotal;
        private ProgressBar progressBarCurrent;
        private Label lblConversionStatus;
        private Label lblConversionFile;
        private Label lblConversionProgress;
        private Label lblConversionFileProgress;
        private ProgressBar progressBarConversionTotal;
        private ProgressBar progressBarConversionCurrent;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tabControl = new TabControl();
            tabPageDownload = new TabPage();
            lblStatus = new Label();
            lblCurrentFile = new Label();
            lblProgress = new Label();
            lblFileProgress = new Label();
            progressBarTotal = new ProgressBar();
            progressBarCurrent = new ProgressBar();
            tabPageConversion = new TabPage();
            lblConversionStatus = new Label();
            lblConversionFile = new Label();
            lblConversionProgress = new Label();
            lblConversionFileProgress = new Label();
            progressBarConversionTotal = new ProgressBar();
            progressBarConversionCurrent = new ProgressBar();
            btnCancel = new Button();
            tabControl.SuspendLayout();
            tabPageDownload.SuspendLayout();
            tabPageConversion.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPageDownload);
            tabControl.Controls.Add(tabPageConversion);
            tabControl.Location = new Point(12, 12);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(706, 345);
            tabControl.TabIndex = 0;
            // 
            // tabPageDownload
            // 
            tabPageDownload.Controls.Add(lblStatus);
            tabPageDownload.Controls.Add(lblProgress);
            tabPageDownload.Controls.Add(progressBarTotal);
            tabPageDownload.Controls.Add(lblCurrentFile);
            tabPageDownload.Controls.Add(progressBarCurrent);
            tabPageDownload.Controls.Add(lblFileProgress);
            tabPageDownload.Location = new Point(4, 34);
            tabPageDownload.Name = "tabPageDownload";
            tabPageDownload.Padding = new Padding(3);
            tabPageDownload.Size = new Size(698, 307);
            tabPageDownload.TabIndex = 0;
            tabPageDownload.Text = "Download";
            tabPageDownload.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatus.Location = new Point(6, 10);
            lblStatus.Margin = new Padding(4, 0, 4, 0);
            lblStatus.MaximumSize = new Size(680, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(199, 25);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Preparing download...";
            // 
            // lblCurrentFile
            // 
            lblCurrentFile.AutoSize = true;
            lblCurrentFile.Location = new Point(6, 155);
            lblCurrentFile.Margin = new Padding(4, 0, 4, 0);
            lblCurrentFile.MaximumSize = new Size(680, 0);
            lblCurrentFile.Name = "lblCurrentFile";
            lblCurrentFile.Size = new Size(86, 25);
            lblCurrentFile.TabIndex = 3;
            lblCurrentFile.Text = "Current: -";
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Location = new Point(6, 37);
            lblProgress.Margin = new Padding(4, 0, 4, 0);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(90, 25);
            lblProgress.TabIndex = 1;
            lblProgress.Text = "File 0 of 0";
            // 
            // lblFileProgress
            // 
            lblFileProgress.AutoSize = true;
            lblFileProgress.Location = new Point(6, 225);
            lblFileProgress.Margin = new Padding(4, 0, 4, 0);
            lblFileProgress.Name = "lblFileProgress";
            lblFileProgress.Size = new Size(151, 25);
            lblFileProgress.TabIndex = 4;
            lblFileProgress.Text = "0 MB / 0 MB (0%)";
            // 
            // progressBarTotal
            // 
            progressBarTotal.Location = new Point(6, 112);
            progressBarTotal.Margin = new Padding(4);
            progressBarTotal.Name = "progressBarTotal";
            progressBarTotal.Size = new Size(680, 29);
            progressBarTotal.TabIndex = 2;
            // 
            // progressBarCurrent
            // 
            progressBarCurrent.Location = new Point(6, 256);
            progressBarCurrent.Margin = new Padding(4);
            progressBarCurrent.Name = "progressBarCurrent";
            progressBarCurrent.Size = new Size(680, 29);
            progressBarCurrent.TabIndex = 5;
            // 
            // tabPageConversion
            // 
            tabPageConversion.Controls.Add(lblConversionStatus);
            tabPageConversion.Controls.Add(lblConversionProgress);
            tabPageConversion.Controls.Add(progressBarConversionTotal);
            tabPageConversion.Controls.Add(lblConversionFile);
            tabPageConversion.Controls.Add(progressBarConversionCurrent);
            tabPageConversion.Controls.Add(lblConversionFileProgress);
            tabPageConversion.Location = new Point(4, 34);
            tabPageConversion.Name = "tabPageConversion";
            tabPageConversion.Padding = new Padding(3);
            tabPageConversion.Size = new Size(698, 307);
            tabPageConversion.TabIndex = 1;
            tabPageConversion.Text = "Conversion";
            tabPageConversion.UseVisualStyleBackColor = true;
            // 
            // lblConversionStatus
            // 
            lblConversionStatus.AutoSize = true;
            lblConversionStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblConversionStatus.Location = new Point(6, 10);
            lblConversionStatus.Margin = new Padding(4, 0, 4, 0);
            lblConversionStatus.MaximumSize = new Size(680, 0);
            lblConversionStatus.Name = "lblConversionStatus";
            lblConversionStatus.Size = new Size(151, 25);
            lblConversionStatus.TabIndex = 0;
            lblConversionStatus.Text = "Waiting to start...";
            // 
            // lblConversionFile
            // 
            lblConversionFile.AutoSize = true;
            lblConversionFile.Location = new Point(6, 155);
            lblConversionFile.Margin = new Padding(4, 0, 4, 0);
            lblConversionFile.MaximumSize = new Size(680, 0);
            lblConversionFile.Name = "lblConversionFile";
            lblConversionFile.Size = new Size(86, 25);
            lblConversionFile.TabIndex = 3;
            lblConversionFile.Text = "Current: -";
            // 
            // lblConversionProgress
            // 
            lblConversionProgress.AutoSize = true;
            lblConversionProgress.Location = new Point(6, 37);
            lblConversionProgress.Margin = new Padding(4, 0, 4, 0);
            lblConversionProgress.Name = "lblConversionProgress";
            lblConversionProgress.Size = new Size(90, 25);
            lblConversionProgress.TabIndex = 1;
            lblConversionProgress.Text = "File 0 of 0";
            // 
            // lblConversionFileProgress
            // 
            lblConversionFileProgress.AutoSize = true;
            lblConversionFileProgress.Location = new Point(6, 225);
            lblConversionFileProgress.Margin = new Padding(4, 0, 4, 0);
            lblConversionFileProgress.Name = "lblConversionFileProgress";
            lblConversionFileProgress.Size = new Size(64, 25);
            lblConversionFileProgress.TabIndex = 4;
            lblConversionFileProgress.Text = "0% (-)";
            // 
            // progressBarConversionTotal
            // 
            progressBarConversionTotal.Location = new Point(6, 112);
            progressBarConversionTotal.Margin = new Padding(4);
            progressBarConversionTotal.Name = "progressBarConversionTotal";
            progressBarConversionTotal.Size = new Size(680, 29);
            progressBarConversionTotal.TabIndex = 2;
            // 
            // progressBarConversionCurrent
            // 
            progressBarConversionCurrent.Location = new Point(6, 256);
            progressBarConversionCurrent.Margin = new Padding(4);
            progressBarConversionCurrent.Name = "progressBarConversionCurrent";
            progressBarConversionCurrent.Size = new Size(680, 29);
            progressBarConversionCurrent.TabIndex = 5;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(593, 363);
            btnCancel.Margin = new Padding(4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(125, 38);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // DownloadProgressDialog
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(730, 414);
            Controls.Add(btnCancel);
            Controls.Add(tabControl);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DownloadProgressDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Download Progress";
            tabControl.ResumeLayout(false);
            tabPageDownload.ResumeLayout(false);
            tabPageDownload.PerformLayout();
            tabPageConversion.ResumeLayout(false);
            tabPageConversion.PerformLayout();
            ResumeLayout(false);
        }
    }
}
