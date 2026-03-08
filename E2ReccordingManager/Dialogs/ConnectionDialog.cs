using E2ReccordingManager.Models;
using E2ReccordingManager.Utilities;

namespace E2ReccordingManager.Dialogs
{
    public partial class ConnectionDialog : Form
    {
        public ConnectionProfile Profile { get; private set; }
        private readonly ConnectionProfileManager profileManager;
        private bool isLoadingProfile = false;

        public ConnectionDialog(ConnectionProfileManager manager, ConnectionProfile? profile = null)
        {
            InitializeComponent();
            profileManager = manager;
            Profile = profile ?? new ConnectionProfile();
            LoadProfiles();
            LoadProfile();
        }

        private void LoadProfiles()
        {
            isLoadingProfile = true;
            comboBoxProfiles.Items.Clear();

            var profiles = profileManager.GetAllProfiles();
            foreach (var profile in profiles)
            {
                comboBoxProfiles.Items.Add(profile.Name);
            }

            if (comboBoxProfiles.Items.Count > 0)
            {
                var lastProfile = profileManager.GetLastUsedProfile();
                if (!string.IsNullOrEmpty(lastProfile) && comboBoxProfiles.Items.Contains(lastProfile))
                {
                    comboBoxProfiles.SelectedItem = lastProfile;
                }
                else
                {
                    comboBoxProfiles.SelectedIndex = 0;
                }
            }
            else
            {
                comboBoxProfiles.Items.Add("Default");
                comboBoxProfiles.SelectedIndex = 0;
            }

            isLoadingProfile = false;
        }

        private void LoadProfile()
        {
            isLoadingProfile = true;
            txtProfileName.Text = Profile.Name;
            txtHost.Text = Profile.Host;
            numPort.Value = Profile.Port;
            txtUsername.Text = Profile.Username;
            txtPassword.Text = Profile.Password;
            isLoadingProfile = false;
        }

        private void ComboBoxProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoadingProfile) return;

            var profileName = comboBoxProfiles.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(profileName))
            {
                var profile = profileManager.GetProfile(profileName);
                if (profile != null)
                {
                    Profile = profile;
                    LoadProfile();
                }
            }
        }

        private void BtnSaveProfile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProfileName.Text))
            {
                MessageBox.Show("Please enter a profile name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtHost.Text))
            {
                MessageBox.Show("Please enter a host address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var profileName = txtProfileName.Text.Trim();
            var existingProfile = profileManager.GetProfile(profileName);

            if (existingProfile != null && existingProfile.Name != Profile.Name)
            {
                var result = MessageBox.Show(
                    $"Profile '{profileName}' already exists. Do you want to overwrite it?",
                    "Profile Exists",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            Profile.Name = profileName;
            Profile.Host = txtHost.Text.Trim();
            Profile.Port = (int)numPort.Value;
            Profile.Username = txtUsername.Text.Trim();
            Profile.Password = txtPassword.Text;

            profileManager.SaveProfile(Profile);

            LoadProfiles();
            comboBoxProfiles.SelectedItem = Profile.Name;

            MessageBox.Show($"Profile '{Profile.Name}' saved successfully!", "Profile Saved",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnDeleteProfile_Click(object sender, EventArgs e)
        {
            var profileName = comboBoxProfiles.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(profileName))
            {
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure you want to delete profile '{profileName}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                profileManager.DeleteProfile(profileName);
                LoadProfiles();
                MessageBox.Show($"Profile '{profileName}' deleted successfully!", "Profile Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHost.Text))
            {
                MessageBox.Show("Please enter a host address.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Profile.Name = string.IsNullOrWhiteSpace(txtProfileName.Text) ? "Default" : txtProfileName.Text.Trim();
            Profile.Host = txtHost.Text.Trim();
            Profile.Port = (int)numPort.Value;
            Profile.Username = txtUsername.Text.Trim();
            Profile.Password = txtPassword.Text;

            profileManager.SaveProfile(Profile);
            profileManager.SaveLastUsedProfile(Profile.Name);

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
