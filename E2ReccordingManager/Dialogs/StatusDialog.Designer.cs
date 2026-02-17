namespace E2ReccordingManager.Dialogs
{
    partial class StatusDialog
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblStatus;
        private ProgressBar progressBar;
        private Button btnClose;

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
            lblStatus = new Label();
            progressBar = new ProgressBar();
            btnClose = new Button();
            SuspendLayout();
            
            // lblStatus
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 15);
            lblStatus.MaximumSize = new Size(460, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(100, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Processing...";
            
            // progressBar
            progressBar.Location = new Point(12, 45);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(460, 23);
            progressBar.TabIndex = 1;
            
            // btnClose
            btnClose.Enabled = false;
            btnClose.Location = new Point(397, 84);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 2;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += BtnClose_Click;
            
            // StatusDialog
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 121);
            Controls.Add(btnClose);
            Controls.Add(progressBar);
            Controls.Add(lblStatus);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StatusDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Status";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
