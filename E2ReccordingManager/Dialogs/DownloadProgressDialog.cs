using System;
using System.Windows.Forms;

namespace E2ReccordingManager.Dialogs
{
    public partial class DownloadProgressDialog : Form
    {
        private bool cancelRequested = false;

        public bool CancelRequested => cancelRequested;

        public DownloadProgressDialog()
        {
            InitializeComponent();
        }

        public void UpdateStatus(string status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateStatus(status)));
                return;
            }
            lblStatus.Text = status;
            Application.DoEvents();
        }

        public void UpdateCurrentFile(string fileName)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateCurrentFile(fileName)));
                return;
            }
            lblCurrentFile.Text = $"Current: {fileName}";
            Application.DoEvents();
        }

        public void UpdateProgress(int current, int total)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateProgress(current, total)));
                return;
            }

            if (total > 0)
            {
                progressBarTotal.Maximum = total;
                progressBarTotal.Value = Math.Min(current, total);
                lblProgress.Text = $"File {current} of {total}";
            }
            Application.DoEvents();
        }

        public void UpdateFileProgress(long bytesDownloaded, long totalBytes)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateFileProgress(bytesDownloaded, totalBytes)));
                return;
            }

            if (totalBytes > 0)
            {
                int percentage = (int)((bytesDownloaded * 100) / totalBytes);
                progressBarCurrent.Value = Math.Min(percentage, 100);

                string downloadedStr = FormatBytes(bytesDownloaded);
                string totalStr = FormatBytes(totalBytes);
                lblFileProgress.Text = $"{downloadedStr} / {totalStr} ({percentage}%)";
            }
            Application.DoEvents();
        }

        public void UpdateConversionStatus(string status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateConversionStatus(status)));
                return;
            }
            lblConversionStatus.Text = status;
            Application.DoEvents();
        }

        public void UpdateConversionFile(string fileName)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateConversionFile(fileName)));
                return;
            }
            lblConversionFile.Text = $"Current: {fileName}";
            Application.DoEvents();
        }

        public void UpdateConversionProgress(int current, int total)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateConversionProgress(current, total)));
                return;
            }

            if (total > 0)
            {
                progressBarConversionTotal.Maximum = total;
                progressBarConversionTotal.Value = Math.Min(current, total);
                lblConversionProgress.Text = $"File {current} of {total}";
            }
            Application.DoEvents();
        }

        public void UpdateConversionFileProgress(int percentage, string timeInfo)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateConversionFileProgress(percentage, timeInfo)));
                return;
            }

            progressBarConversionCurrent.Value = Math.Min(Math.Max(percentage, 0), 100);
            lblConversionFileProgress.Text = $"{percentage}% ({timeInfo})";
            Application.DoEvents();
        }

        public void SwitchToConversionTab()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SwitchToConversionTab()));
                return;
            }
            tabControl.SelectedTab = tabPageConversion;
        }

        public void SetComplete(bool success, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetComplete(success, message)));
                return;
            }

            btnCancel.Text = "Close";
            btnCancel.Enabled = true;
            lblStatus.Text = message;

            if (success)
            {
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                lblStatus.ForeColor = Color.Red;
            }
        }

        private string FormatBytes(long bytes)
        {
            if (bytes < 1024)
                return $"{bytes} B";
            else if (bytes < 1024 * 1024)
                return $"{bytes / 1024.0:F2} KB";
            else if (bytes < 1024 * 1024 * 1024)
                return $"{bytes / (1024.0 * 1024.0):F2} MB";
            else
                return $"{bytes / (1024.0 * 1024.0 * 1024.0):F2} GB";
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (btnCancel.Text == "Close")
            {
                Close();
            }
            else
            {
                cancelRequested = true;
                btnCancel.Enabled = false;
                lblStatus.Text = "Canceling downloads...";
                lblStatus.ForeColor = Color.Orange;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!cancelRequested && btnCancel.Text != "Close")
            {
                var result = MessageBox.Show(
                    "Download in progress. Are you sure you want to cancel?",
                    "Cancel Download",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    cancelRequested = true;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            base.OnFormClosing(e);
        }
    }
}
