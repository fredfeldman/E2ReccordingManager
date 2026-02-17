namespace E2ReccordingManager.Dialogs
{
    public partial class StatusDialog : Form
    {
        public StatusDialog()
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

        public void UpdateProgress(int current, int total)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateProgress(current, total)));
                return;
            }
            
            if (total > 0)
            {
                progressBar.Maximum = total;
                progressBar.Value = Math.Min(current, total);
            }
            Application.DoEvents();
        }

        public void SetComplete(bool success)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetComplete(success)));
                return;
            }
            
            btnClose.Enabled = true;
            if (success)
            {
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
