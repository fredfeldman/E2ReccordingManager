namespace E2ReccordingManager.Dialogs
{
    partial class ConnectionDialog
    {
        private System.ComponentModel.IContainer components = null;
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
            lblHost = new Label();
            txtHost = new TextBox();
            lblPort = new Label();
            numPort = new NumericUpDown();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnConnect = new Button();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)numPort).BeginInit();
            SuspendLayout();
            
            // lblHost
            lblHost.AutoSize = true;
            lblHost.Location = new Point(12, 15);
            lblHost.Name = "lblHost";
            lblHost.Size = new Size(35, 15);
            lblHost.TabIndex = 0;
            lblHost.Text = "Host:";
            
            // txtHost
            txtHost.Location = new Point(90, 12);
            txtHost.Name = "txtHost";
            txtHost.Size = new Size(282, 23);
            txtHost.TabIndex = 1;
            
            // lblPort
            lblPort.AutoSize = true;
            lblPort.Location = new Point(12, 44);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(32, 15);
            lblPort.TabIndex = 2;
            lblPort.Text = "Port:";
            
            // numPort
            numPort.Location = new Point(90, 41);
            numPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPort.Name = "numPort";
            numPort.Size = new Size(120, 23);
            numPort.TabIndex = 3;
            numPort.Value = new decimal(new int[] { 80, 0, 0, 0 });
            
            // lblUsername
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(12, 73);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(63, 15);
            lblUsername.TabIndex = 4;
            lblUsername.Text = "Username:";
            
            // txtUsername
            txtUsername.Location = new Point(90, 70);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(282, 23);
            txtUsername.TabIndex = 5;
            
            // lblPassword
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(12, 102);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(60, 15);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Password:";
            
            // txtPassword
            txtPassword.Location = new Point(90, 99);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(282, 23);
            txtPassword.TabIndex = 7;
            
            // btnConnect
            btnConnect.Location = new Point(216, 138);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(75, 23);
            btnConnect.TabIndex = 8;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += BtnConnect_Click;
            
            // btnCancel
            btnCancel.Location = new Point(297, 138);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            
            // ConnectionDialog
            AcceptButton = btnConnect;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(384, 181);
            Controls.Add(btnCancel);
            Controls.Add(btnConnect);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            Controls.Add(numPort);
            Controls.Add(lblPort);
            Controls.Add(txtHost);
            Controls.Add(lblHost);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConnectionDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Connect to Enigma2 Device";
            ((System.ComponentModel.ISupportInitialize)numPort).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
