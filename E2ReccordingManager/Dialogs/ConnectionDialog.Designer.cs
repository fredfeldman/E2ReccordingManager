namespace E2ReccordingManager.Dialogs
{
    partial class ConnectionDialog
    {
        private System.ComponentModel.IContainer components = null;
        private GroupBox groupBoxProfile;
        private Label lblProfile;
        private ComboBox comboBoxProfiles;
        private Label lblProfileName;
        private TextBox txtProfileName;
        private Button btnSaveProfile;
        private Button btnDeleteProfile;
        private GroupBox groupBoxConnection;
        private Label lblHost;
        private TextBox txtHost;
        private Label lblPort;
        private NumericUpDown numPort;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnConnect;
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
            groupBoxProfile = new GroupBox();
            btnDeleteProfile = new Button();
            btnSaveProfile = new Button();
            txtProfileName = new TextBox();
            lblProfileName = new Label();
            comboBoxProfiles = new ComboBox();
            lblProfile = new Label();
            groupBoxConnection = new GroupBox();
            txtPassword = new TextBox();
            lblPassword = new Label();
            txtUsername = new TextBox();
            lblUsername = new Label();
            numPort = new NumericUpDown();
            lblPort = new Label();
            txtHost = new TextBox();
            lblHost = new Label();
            btnConnect = new Button();
            btnCancel = new Button();
            groupBoxProfile.SuspendLayout();
            groupBoxConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPort).BeginInit();
            SuspendLayout();

            // groupBoxProfile
            groupBoxProfile.Controls.Add(btnDeleteProfile);
            groupBoxProfile.Controls.Add(btnSaveProfile);
            groupBoxProfile.Controls.Add(txtProfileName);
            groupBoxProfile.Controls.Add(lblProfileName);
            groupBoxProfile.Controls.Add(comboBoxProfiles);
            groupBoxProfile.Controls.Add(lblProfile);
            groupBoxProfile.Location = new Point(12, 12);
            groupBoxProfile.Name = "groupBoxProfile";
            groupBoxProfile.Size = new Size(440, 90);
            groupBoxProfile.TabIndex = 0;
            groupBoxProfile.TabStop = false;
            groupBoxProfile.Text = "Profile Management";

            // lblProfile
            lblProfile.AutoSize = true;
            lblProfile.Location = new Point(10, 25);
            lblProfile.Name = "lblProfile";
            lblProfile.Size = new Size(77, 15);
            lblProfile.TabIndex = 0;
            lblProfile.Text = "Load Profile:";

            // comboBoxProfiles
            comboBoxProfiles.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxProfiles.FormattingEnabled = true;
            comboBoxProfiles.Location = new Point(100, 22);
            comboBoxProfiles.Name = "comboBoxProfiles";
            comboBoxProfiles.Size = new Size(240, 23);
            comboBoxProfiles.TabIndex = 1;
            comboBoxProfiles.SelectedIndexChanged += ComboBoxProfiles_SelectedIndexChanged;

            // lblProfileName
            lblProfileName.AutoSize = true;
            lblProfileName.Location = new Point(10, 56);
            lblProfileName.Name = "lblProfileName";
            lblProfileName.Size = new Size(82, 15);
            lblProfileName.TabIndex = 2;
            lblProfileName.Text = "Profile Name:";

            // txtProfileName
            txtProfileName.Location = new Point(100, 53);
            txtProfileName.Name = "txtProfileName";
            txtProfileName.Size = new Size(240, 23);
            txtProfileName.TabIndex = 3;

            // btnSaveProfile
            btnSaveProfile.Location = new Point(346, 22);
            btnSaveProfile.Name = "btnSaveProfile";
            btnSaveProfile.Size = new Size(85, 23);
            btnSaveProfile.TabIndex = 4;
            btnSaveProfile.Text = "Save Profile";
            btnSaveProfile.UseVisualStyleBackColor = true;
            btnSaveProfile.Click += BtnSaveProfile_Click;

            // btnDeleteProfile
            btnDeleteProfile.Location = new Point(346, 52);
            btnDeleteProfile.Name = "btnDeleteProfile";
            btnDeleteProfile.Size = new Size(85, 23);
            btnDeleteProfile.TabIndex = 5;
            btnDeleteProfile.Text = "Delete Profile";
            btnDeleteProfile.UseVisualStyleBackColor = true;
            btnDeleteProfile.Click += BtnDeleteProfile_Click;

            // groupBoxConnection
            groupBoxConnection.Controls.Add(txtPassword);
            groupBoxConnection.Controls.Add(lblPassword);
            groupBoxConnection.Controls.Add(txtUsername);
            groupBoxConnection.Controls.Add(lblUsername);
            groupBoxConnection.Controls.Add(numPort);
            groupBoxConnection.Controls.Add(lblPort);
            groupBoxConnection.Controls.Add(txtHost);
            groupBoxConnection.Controls.Add(lblHost);
            groupBoxConnection.Location = new Point(12, 108);
            groupBoxConnection.Name = "groupBoxConnection";
            groupBoxConnection.Size = new Size(440, 130);
            groupBoxConnection.TabIndex = 1;
            groupBoxConnection.TabStop = false;
            groupBoxConnection.Text = "Connection Settings";

            // lblHost
            lblHost.AutoSize = true;
            lblHost.Location = new Point(10, 25);
            lblHost.Name = "lblHost";
            lblHost.Size = new Size(35, 15);
            lblHost.TabIndex = 0;
            lblHost.Text = "Host:";

            // txtHost
            txtHost.Location = new Point(100, 22);
            txtHost.Name = "txtHost";
            txtHost.Size = new Size(330, 23);
            txtHost.TabIndex = 1;

            // lblPort
            lblPort.AutoSize = true;
            lblPort.Location = new Point(10, 54);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(32, 15);
            lblPort.TabIndex = 2;
            lblPort.Text = "Port:";

            // numPort
            numPort.Location = new Point(100, 51);
            numPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPort.Name = "numPort";
            numPort.Size = new Size(120, 23);
            numPort.TabIndex = 3;
            numPort.Value = new decimal(new int[] { 80, 0, 0, 0 });

            // lblUsername
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(10, 83);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(63, 15);
            lblUsername.TabIndex = 4;
            lblUsername.Text = "Username:";

            // txtUsername
            txtUsername.Location = new Point(100, 80);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(330, 23);
            txtUsername.TabIndex = 5;
            txtUsername.Text = "root";

            // lblPassword
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(10, 97);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(60, 15);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Password:";

            // txtPassword
            txtPassword.Location = new Point(100, 97);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(330, 23);
            txtPassword.TabIndex = 7;

            // btnConnect
            btnConnect.Location = new Point(296, 254);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(75, 30);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += BtnConnect_Click;

            // btnCancel
            btnCancel.Location = new Point(377, 254);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 30);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;

            // ConnectionDialog
            AcceptButton = btnConnect;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(464, 296);
            Controls.Add(btnCancel);
            Controls.Add(btnConnect);
            Controls.Add(groupBoxConnection);
            Controls.Add(groupBoxProfile);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConnectionDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Connect to Enigma2 Device";
            groupBoxProfile.ResumeLayout(false);
            groupBoxProfile.PerformLayout();
            groupBoxConnection.ResumeLayout(false);
            groupBoxConnection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPort).EndInit();
            ResumeLayout(false);
        }
    }
}
