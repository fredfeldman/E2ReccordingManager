using E2ReccordingManager.Models;

namespace E2ReccordingManager.Dialogs
{
    public partial class ConnectionDialog : Form
    {
        public ConnectionProfile Profile { get; private set; }

        public ConnectionDialog(ConnectionProfile? profile = null)
        {
            InitializeComponent();
            Profile = profile ?? new ConnectionProfile();
            LoadProfile();
        }

        private void LoadProfile()
        {
            txtHost.Text = Profile.Host;
            numPort.Value = Profile.Port;
            txtUsername.Text = Profile.Username;
            txtPassword.Text = Profile.Password;
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHost.Text))
            {
                MessageBox.Show("Please enter a host address.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Profile.Host = txtHost.Text.Trim();
            Profile.Port = (int)numPort.Value;
            Profile.Username = txtUsername.Text.Trim();
            Profile.Password = txtPassword.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
