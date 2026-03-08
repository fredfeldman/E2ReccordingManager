namespace E2ReccordingManager
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            toolStrip = new ToolStrip();
            btnConnect = new ToolStripButton();
            btnDisconnect = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            lblConnectionStatus = new ToolStripLabel();
            toolStripSeparator3 = new ToolStripSeparator();
            btnSettings = new ToolStripButton();
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            tabControl = new TabControl();
            tabPageRecordings = new TabPage();
            splitContainer = new SplitContainer();
            listViewRecordings = new ListView();
            colTitle = new ColumnHeader();
            colServiceName = new ColumnHeader();
            colDate = new ColumnHeader();
            colDuration = new ColumnHeader();
            colSize = new ColumnHeader();
            colFileName = new ColumnHeader();
            contextMenuStripRecordings = new ContextMenuStrip(components);
            menuItemView = new ToolStripMenuItem();
            menuItemDownload = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            menuItemDelete = new ToolStripMenuItem();
            panelRecordingDetails = new Panel();
            txtDescription = new TextBox();
            lblDescriptionLabel = new Label();
            panelControls = new Panel();
            lblDownloadStatus = new Label();
            progressBarDownload = new ProgressBar();
            checkBoxRenameFromEIT = new CheckBox();
            checkBoxRemoveAfterDownload = new CheckBox();
            btnDeleteRecording = new Button();
            btnDownloadRecording = new Button();
            btnRefreshRecordings = new Button();
            btnSelectAllRecordings = new Button();
            tabPageLocalRecordings = new TabPage();
            splitContainerLocal = new SplitContainer();
            listViewLocalRecordings = new ListView();
            colLRTitle = new ColumnHeader();
            colLRServiceName = new ColumnHeader();
            colLRDate = new ColumnHeader();
            colLRDuration = new ColumnHeader();
            colLRSize = new ColumnHeader();
            colLRFileName = new ColumnHeader();
            contextMenuStripLocalRecordings = new ContextMenuStrip(components);
            menuItemLocalView = new ToolStripMenuItem();
            menuItemLocalDownload = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            panelLocalRecordingDetails = new Panel();
            txtLocalDescription = new TextBox();
            lblLocalDescriptionLabel = new Label();
            panelLocalControls = new Panel();
            checkBoxLocalRenameFromEIT = new CheckBox();
            btnDownloadLocalRecording = new Button();
            btnRefreshLocalRecordings = new Button();
            btnSelectAllLocalRecordings = new Button();
            btnBrowseLocalFolder = new Button();
            txtLocalFolderPath = new TextBox();
            lblLocalFolder = new Label();
            tabPageConversion = new TabPage();
            splitContainerConversion = new SplitContainer();
            listViewLocalFiles = new ListView();
            colLocalFileName = new ColumnHeader();
            colLocalSize = new ColumnHeader();
            colLocalDate = new ColumnHeader();
            colLocalStatus = new ColumnHeader();
            panelConversionControls = new Panel();
            lblConversionInfo = new Label();
            btnSelectAll = new Button();
            btnSelectNone = new Button();
            btnRefreshFiles = new Button();
            btnBrowseFolder = new Button();
            txtConversionFolder = new TextBox();
            lblConversionFolder = new Label();
            btnConvertSelected = new Button();
            btnOrganizeMp4Files = new Button();
            toolStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            tabControl.SuspendLayout();
            tabPageRecordings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            contextMenuStripRecordings.SuspendLayout();
            panelRecordingDetails.SuspendLayout();
            panelControls.SuspendLayout();
            tabPageLocalRecordings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerLocal).BeginInit();
            splitContainerLocal.Panel1.SuspendLayout();
            splitContainerLocal.Panel2.SuspendLayout();
            splitContainerLocal.SuspendLayout();
            contextMenuStripLocalRecordings.SuspendLayout();
            panelLocalRecordingDetails.SuspendLayout();
            panelLocalControls.SuspendLayout();
            tabPageConversion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerConversion).BeginInit();
            splitContainerConversion.Panel1.SuspendLayout();
            splitContainerConversion.Panel2.SuspendLayout();
            splitContainerConversion.SuspendLayout();
            panelConversionControls.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.ImageScalingSize = new Size(24, 24);
            toolStrip.Items.AddRange(new ToolStripItem[] { btnConnect, btnDisconnect, toolStripSeparator1, lblConnectionStatus, toolStripSeparator3, btnSettings });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Padding = new Padding(0, 0, 3, 0);
            toolStrip.Size = new Size(1429, 34);
            toolStrip.TabIndex = 0;
            toolStrip.Text = "toolStrip1";
            // 
            // btnConnect
            // 
            btnConnect.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(81, 29);
            btnConnect.Text = "Connect";
            btnConnect.Click += BtnConnect_Click;
            // 
            // btnDisconnect
            // 
            btnDisconnect.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnDisconnect.Enabled = false;
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(103, 29);
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.Click += BtnDisconnect_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 34);
            // 
            // lblConnectionStatus
            // 
            lblConnectionStatus.Name = "lblConnectionStatus";
            lblConnectionStatus.Size = new Size(132, 29);
            lblConnectionStatus.Text = "Not Connected";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 34);
            // 
            // btnSettings
            // 
            btnSettings.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(80, 29);
            btnSettings.Text = "Settings";
            btnSettings.Click += BtnSettings_Click;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(24, 24);
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel });
            statusStrip.Location = new Point(0, 968);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 20, 0);
            statusStrip.Size = new Size(1429, 32);
            statusStrip.TabIndex = 1;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(60, 25);
            toolStripStatusLabel.Text = "Ready";
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPageRecordings);
            tabControl.Controls.Add(tabPageLocalRecordings);
            tabControl.Controls.Add(tabPageConversion);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 34);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1429, 934);
            tabControl.TabIndex = 3;
            // 
            // tabPageRecordings
            // 
            tabPageRecordings.Controls.Add(splitContainer);
            tabPageRecordings.Location = new Point(4, 34);
            tabPageRecordings.Name = "tabPageRecordings";
            tabPageRecordings.Padding = new Padding(3);
            tabPageRecordings.Size = new Size(1421, 896);
            tabPageRecordings.TabIndex = 0;
            tabPageRecordings.Text = "Device Recordings";
            tabPageRecordings.UseVisualStyleBackColor = true;
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(3, 3);
            splitContainer.Margin = new Padding(4, 5, 4, 5);
            splitContainer.Name = "splitContainer";
            splitContainer.Orientation = Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(listViewRecordings);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(panelRecordingDetails);
            splitContainer.Panel2.Controls.Add(panelControls);
            splitContainer.Size = new Size(1415, 890);
            splitContainer.SplitterDistance = 565;
            splitContainer.SplitterWidth = 7;
            splitContainer.TabIndex = 0;
            // 
            // listViewRecordings
            // 
            listViewRecordings.CheckBoxes = true;
            listViewRecordings.Columns.AddRange(new ColumnHeader[] { colTitle, colServiceName, colDate, colDuration, colSize, colFileName });
            listViewRecordings.ContextMenuStrip = contextMenuStripRecordings;
            listViewRecordings.Dock = DockStyle.Fill;
            listViewRecordings.FullRowSelect = true;
            listViewRecordings.Location = new Point(0, 0);
            listViewRecordings.Margin = new Padding(4, 5, 4, 5);
            listViewRecordings.Name = "listViewRecordings";
            listViewRecordings.Size = new Size(1415, 565);
            listViewRecordings.TabIndex = 0;
            listViewRecordings.UseCompatibleStateImageBehavior = false;
            listViewRecordings.View = View.Details;
            listViewRecordings.SelectedIndexChanged += ListViewRecordings_SelectedIndexChanged;
            // 
            // colTitle
            // 
            colTitle.Text = "Title";
            colTitle.Width = 300;
            // 
            // colServiceName
            // 
            colServiceName.Text = "Channel";
            colServiceName.Width = 150;
            // 
            // colDate
            // 
            colDate.Text = "Date";
            colDate.Width = 150;
            // 
            // colDuration
            // 
            colDuration.Text = "Duration";
            colDuration.Width = 100;
            // 
            // colSize
            // 
            colSize.Text = "Size";
            colSize.Width = 100;
            // 
            // colFileName
            // 
            colFileName.Text = "File Name";
            colFileName.Width = 200;
            // 
            // contextMenuStripRecordings
            // 
            contextMenuStripRecordings.ImageScalingSize = new Size(24, 24);
            contextMenuStripRecordings.Items.AddRange(new ToolStripItem[] { menuItemView, menuItemDownload, toolStripSeparator2, menuItemDelete });
            contextMenuStripRecordings.Name = "contextMenuStripRecordings";
            contextMenuStripRecordings.Size = new Size(180, 106);
            // 
            // menuItemView
            // 
            menuItemView.Name = "menuItemView";
            menuItemView.Size = new Size(179, 32);
            menuItemView.Text = "View Details";
            menuItemView.Click += MenuItemView_Click;
            // 
            // menuItemDownload
            // 
            menuItemDownload.Name = "menuItemDownload";
            menuItemDownload.Size = new Size(179, 32);
            menuItemDownload.Text = "Download";
            menuItemDownload.Click += BtnDownloadRecording_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(176, 6);
            // 
            // menuItemDelete
            // 
            menuItemDelete.Name = "menuItemDelete";
            menuItemDelete.Size = new Size(179, 32);
            menuItemDelete.Text = "Delete";
            menuItemDelete.Click += BtnDeleteRecording_Click;
            // 
            // panelRecordingDetails
            // 
            panelRecordingDetails.Controls.Add(txtDescription);
            panelRecordingDetails.Controls.Add(lblDescriptionLabel);
            panelRecordingDetails.Dock = DockStyle.Fill;
            panelRecordingDetails.Location = new Point(0, 0);
            panelRecordingDetails.Margin = new Padding(4, 5, 4, 5);
            panelRecordingDetails.Name = "panelRecordingDetails";
            panelRecordingDetails.Padding = new Padding(7, 8, 7, 8);
            panelRecordingDetails.Size = new Size(1415, 185);
            panelRecordingDetails.TabIndex = 1;
            // 
            // txtDescription
            // 
            txtDescription.Dock = DockStyle.Fill;
            txtDescription.Location = new Point(7, 33);
            txtDescription.Margin = new Padding(4, 5, 4, 5);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ReadOnly = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(1401, 144);
            txtDescription.TabIndex = 1;
            // 
            // lblDescriptionLabel
            // 
            lblDescriptionLabel.AutoSize = true;
            lblDescriptionLabel.Dock = DockStyle.Top;
            lblDescriptionLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDescriptionLabel.Location = new Point(7, 8);
            lblDescriptionLabel.Margin = new Padding(4, 0, 4, 0);
            lblDescriptionLabel.Name = "lblDescriptionLabel";
            lblDescriptionLabel.Size = new Size(114, 25);
            lblDescriptionLabel.TabIndex = 0;
            lblDescriptionLabel.Text = "Description:";
            // 
            // panelControls
            // 
            panelControls.Controls.Add(lblDownloadStatus);
            panelControls.Controls.Add(progressBarDownload);
            panelControls.Controls.Add(checkBoxRenameFromEIT);
            panelControls.Controls.Add(checkBoxRemoveAfterDownload);
            panelControls.Controls.Add(btnDeleteRecording);
            panelControls.Controls.Add(btnDownloadRecording);
            panelControls.Controls.Add(btnRefreshRecordings);
            panelControls.Controls.Add(btnSelectAllRecordings);
            panelControls.Dock = DockStyle.Bottom;
            panelControls.Location = new Point(0, 185);
            panelControls.Margin = new Padding(4, 5, 4, 5);
            panelControls.Name = "panelControls";
            panelControls.Size = new Size(1415, 133);
            panelControls.TabIndex = 0;
            // 
            // lblDownloadStatus
            // 
            lblDownloadStatus.AutoSize = true;
            lblDownloadStatus.Location = new Point(17, 87);
            lblDownloadStatus.Margin = new Padding(4, 0, 4, 0);
            lblDownloadStatus.Name = "lblDownloadStatus";
            lblDownloadStatus.Size = new Size(0, 25);
            lblDownloadStatus.TabIndex = 5;
            // 
            // progressBarDownload
            // 
            progressBarDownload.Location = new Point(17, 78);
            progressBarDownload.Margin = new Padding(4, 5, 4, 5);
            progressBarDownload.Name = "progressBarDownload";
            progressBarDownload.Size = new Size(1394, 38);
            progressBarDownload.TabIndex = 4;
            progressBarDownload.Visible = false;
            // 
            // checkBoxRenameFromEIT
            // 
            checkBoxRenameFromEIT.AutoSize = true;
            checkBoxRenameFromEIT.Checked = true;
            checkBoxRenameFromEIT.CheckState = CheckState.Checked;
            checkBoxRenameFromEIT.Location = new Point(596, 25);
            checkBoxRenameFromEIT.Margin = new Padding(4, 5, 4, 5);
            checkBoxRenameFromEIT.Name = "checkBoxRenameFromEIT";
            checkBoxRenameFromEIT.Size = new Size(245, 29);
            checkBoxRenameFromEIT.TabIndex = 6;
            checkBoxRenameFromEIT.Text = "Rename using EIT file data";
            checkBoxRenameFromEIT.UseVisualStyleBackColor = true;
            // 
            // checkBoxRemoveAfterDownload
            // 
            checkBoxRemoveAfterDownload.AutoSize = true;
            checkBoxRemoveAfterDownload.Location = new Point(360, 25);
            checkBoxRemoveAfterDownload.Margin = new Padding(4, 5, 4, 5);
            checkBoxRemoveAfterDownload.Name = "checkBoxRemoveAfterDownload";
            checkBoxRemoveAfterDownload.Size = new Size(228, 29);
            checkBoxRemoveAfterDownload.TabIndex = 3;
            checkBoxRemoveAfterDownload.Text = "Remove after download";
            checkBoxRemoveAfterDownload.UseVisualStyleBackColor = true;
            // 
            // btnDeleteRecording
            // 
            btnDeleteRecording.Enabled = false;
            btnDeleteRecording.Location = new Point(244, 18);
            btnDeleteRecording.Margin = new Padding(4, 5, 4, 5);
            btnDeleteRecording.Name = "btnDeleteRecording";
            btnDeleteRecording.Size = new Size(107, 38);
            btnDeleteRecording.TabIndex = 2;
            btnDeleteRecording.Text = "Delete";
            btnDeleteRecording.UseVisualStyleBackColor = true;
            btnDeleteRecording.Click += BtnDeleteRecording_Click;
            // 
            // btnDownloadRecording
            // 
            btnDownloadRecording.Enabled = false;
            btnDownloadRecording.Location = new Point(129, 18);
            btnDownloadRecording.Margin = new Padding(4, 5, 4, 5);
            btnDownloadRecording.Name = "btnDownloadRecording";
            btnDownloadRecording.Size = new Size(107, 38);
            btnDownloadRecording.TabIndex = 1;
            btnDownloadRecording.Text = "Download";
            btnDownloadRecording.UseVisualStyleBackColor = true;
            btnDownloadRecording.Click += BtnDownloadRecording_Click;
            // 
            // btnRefreshRecordings
            // 
            btnRefreshRecordings.Enabled = false;
            btnRefreshRecordings.Location = new Point(13, 18);
            btnRefreshRecordings.Margin = new Padding(4, 5, 4, 5);
            btnRefreshRecordings.Name = "btnRefreshRecordings";
            btnRefreshRecordings.Size = new Size(107, 38);
            btnRefreshRecordings.TabIndex = 0;
            btnRefreshRecordings.Text = "Refresh";
            btnRefreshRecordings.UseVisualStyleBackColor = true;
            btnRefreshRecordings.Click += BtnRefreshRecordings_Click;
            // 
            // btnSelectAllRecordings
            // 
            btnSelectAllRecordings.Enabled = false;
            btnSelectAllRecordings.Location = new Point(849, 18);
            btnSelectAllRecordings.Margin = new Padding(4, 5, 4, 5);
            btnSelectAllRecordings.Name = "btnSelectAllRecordings";
            btnSelectAllRecordings.Size = new Size(107, 38);
            btnSelectAllRecordings.TabIndex = 7;
            btnSelectAllRecordings.Text = "Select All";
            btnSelectAllRecordings.UseVisualStyleBackColor = true;
            btnSelectAllRecordings.Click += BtnSelectAllRecordings_Click;
            // 
            // tabPageLocalRecordings
            // 
            tabPageLocalRecordings.Controls.Add(splitContainerLocal);
            tabPageLocalRecordings.Location = new Point(4, 34);
            tabPageLocalRecordings.Name = "tabPageLocalRecordings";
            tabPageLocalRecordings.Padding = new Padding(3);
            tabPageLocalRecordings.Size = new Size(1421, 896);
            tabPageLocalRecordings.TabIndex = 1;
            tabPageLocalRecordings.Text = "Local Recordings";
            tabPageLocalRecordings.UseVisualStyleBackColor = true;
            // 
            // splitContainerLocal
            // 
            splitContainerLocal.Dock = DockStyle.Fill;
            splitContainerLocal.Location = new Point(3, 3);
            splitContainerLocal.Margin = new Padding(4, 5, 4, 5);
            splitContainerLocal.Name = "splitContainerLocal";
            splitContainerLocal.Orientation = Orientation.Horizontal;
            // 
            // splitContainerLocal.Panel1
            // 
            splitContainerLocal.Panel1.Controls.Add(listViewLocalRecordings);
            // 
            // splitContainerLocal.Panel2
            // 
            splitContainerLocal.Panel2.Controls.Add(panelLocalRecordingDetails);
            splitContainerLocal.Panel2.Controls.Add(panelLocalControls);
            splitContainerLocal.Size = new Size(1415, 890);
            splitContainerLocal.SplitterDistance = 565;
            splitContainerLocal.SplitterWidth = 7;
            splitContainerLocal.TabIndex = 0;
            // 
            // listViewLocalRecordings
            // 
            listViewLocalRecordings.CheckBoxes = true;
            listViewLocalRecordings.Columns.AddRange(new ColumnHeader[] { colLRTitle, colLRServiceName, colLRDate, colLRDuration, colLRSize, colLRFileName });
            listViewLocalRecordings.ContextMenuStrip = contextMenuStripLocalRecordings;
            listViewLocalRecordings.Dock = DockStyle.Fill;
            listViewLocalRecordings.FullRowSelect = true;
            listViewLocalRecordings.Location = new Point(0, 0);
            listViewLocalRecordings.Margin = new Padding(4, 5, 4, 5);
            listViewLocalRecordings.Name = "listViewLocalRecordings";
            listViewLocalRecordings.ShowItemToolTips = true;
            listViewLocalRecordings.Size = new Size(1415, 565);
            listViewLocalRecordings.TabIndex = 0;
            listViewLocalRecordings.UseCompatibleStateImageBehavior = false;
            listViewLocalRecordings.View = View.Details;
            listViewLocalRecordings.SelectedIndexChanged += ListViewLocalRecordings_SelectedIndexChanged;
            // 
            // colLRTitle
            // 
            colLRTitle.Text = "Title";
            colLRTitle.Width = 300;
            // 
            // colLRServiceName
            // 
            colLRServiceName.Text = "Channel";
            colLRServiceName.Width = 150;
            // 
            // colLRDate
            // 
            colLRDate.Text = "Date";
            colLRDate.Width = 150;
            // 
            // colLRDuration
            // 
            colLRDuration.Text = "Duration";
            colLRDuration.Width = 100;
            // 
            // colLRSize
            // 
            colLRSize.Text = "Size";
            colLRSize.Width = 100;
            // 
            // colLRFileName
            // 
            colLRFileName.Text = "File Name";
            colLRFileName.Width = 200;
            // 
            // contextMenuStripLocalRecordings
            // 
            contextMenuStripLocalRecordings.ImageScalingSize = new Size(24, 24);
            contextMenuStripLocalRecordings.Items.AddRange(new ToolStripItem[] { menuItemLocalView, menuItemLocalDownload, toolStripSeparator4 });
            contextMenuStripLocalRecordings.Name = "contextMenuStripLocalRecordings";
            contextMenuStripLocalRecordings.Size = new Size(180, 74);
            // 
            // menuItemLocalView
            // 
            menuItemLocalView.Name = "menuItemLocalView";
            menuItemLocalView.Size = new Size(179, 32);
            menuItemLocalView.Text = "View Details";
            menuItemLocalView.Click += MenuItemLocalView_Click;
            // 
            // menuItemLocalDownload
            // 
            menuItemLocalDownload.Name = "menuItemLocalDownload";
            menuItemLocalDownload.Size = new Size(179, 32);
            menuItemLocalDownload.Text = "Download";
            menuItemLocalDownload.Click += BtnDownloadLocalRecording_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(176, 6);
            // 
            // panelLocalRecordingDetails
            // 
            panelLocalRecordingDetails.Controls.Add(txtLocalDescription);
            panelLocalRecordingDetails.Controls.Add(lblLocalDescriptionLabel);
            panelLocalRecordingDetails.Dock = DockStyle.Fill;
            panelLocalRecordingDetails.Location = new Point(0, 0);
            panelLocalRecordingDetails.Margin = new Padding(4, 5, 4, 5);
            panelLocalRecordingDetails.Name = "panelLocalRecordingDetails";
            panelLocalRecordingDetails.Padding = new Padding(7, 8, 7, 8);
            panelLocalRecordingDetails.Size = new Size(1415, 185);
            panelLocalRecordingDetails.TabIndex = 1;
            // 
            // txtLocalDescription
            // 
            txtLocalDescription.Dock = DockStyle.Fill;
            txtLocalDescription.Location = new Point(7, 33);
            txtLocalDescription.Margin = new Padding(4, 5, 4, 5);
            txtLocalDescription.Multiline = true;
            txtLocalDescription.Name = "txtLocalDescription";
            txtLocalDescription.ReadOnly = true;
            txtLocalDescription.ScrollBars = ScrollBars.Vertical;
            txtLocalDescription.Size = new Size(1401, 144);
            txtLocalDescription.TabIndex = 1;
            // 
            // lblLocalDescriptionLabel
            // 
            lblLocalDescriptionLabel.AutoSize = true;
            lblLocalDescriptionLabel.Dock = DockStyle.Top;
            lblLocalDescriptionLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLocalDescriptionLabel.Location = new Point(7, 8);
            lblLocalDescriptionLabel.Margin = new Padding(4, 0, 4, 0);
            lblLocalDescriptionLabel.Name = "lblLocalDescriptionLabel";
            lblLocalDescriptionLabel.Size = new Size(114, 25);
            lblLocalDescriptionLabel.TabIndex = 0;
            lblLocalDescriptionLabel.Text = "Description:";
            // 
            // panelLocalControls
            // 
            panelLocalControls.Controls.Add(checkBoxLocalRenameFromEIT);
            panelLocalControls.Controls.Add(btnDownloadLocalRecording);
            panelLocalControls.Controls.Add(btnRefreshLocalRecordings);
            panelLocalControls.Controls.Add(btnSelectAllLocalRecordings);
            panelLocalControls.Controls.Add(btnBrowseLocalFolder);
            panelLocalControls.Controls.Add(txtLocalFolderPath);
            panelLocalControls.Controls.Add(lblLocalFolder);
            panelLocalControls.Dock = DockStyle.Bottom;
            panelLocalControls.Location = new Point(0, 185);
            panelLocalControls.Margin = new Padding(4, 5, 4, 5);
            panelLocalControls.Name = "panelLocalControls";
            panelLocalControls.Size = new Size(1415, 133);
            panelLocalControls.TabIndex = 0;
            // 
            // checkBoxLocalRenameFromEIT
            // 
            checkBoxLocalRenameFromEIT.AutoSize = true;
            checkBoxLocalRenameFromEIT.Checked = true;
            checkBoxLocalRenameFromEIT.CheckState = CheckState.Checked;
            checkBoxLocalRenameFromEIT.Location = new Point(244, 25);
            checkBoxLocalRenameFromEIT.Margin = new Padding(4, 5, 4, 5);
            checkBoxLocalRenameFromEIT.Name = "checkBoxLocalRenameFromEIT";
            checkBoxLocalRenameFromEIT.Size = new Size(245, 29);
            checkBoxLocalRenameFromEIT.TabIndex = 3;
            checkBoxLocalRenameFromEIT.Text = "Rename using EIT file data";
            checkBoxLocalRenameFromEIT.UseVisualStyleBackColor = true;
            // 
            // btnDownloadLocalRecording
            // 
            btnDownloadLocalRecording.Enabled = false;
            btnDownloadLocalRecording.Location = new Point(129, 18);
            btnDownloadLocalRecording.Margin = new Padding(4, 5, 4, 5);
            btnDownloadLocalRecording.Name = "btnDownloadLocalRecording";
            btnDownloadLocalRecording.Size = new Size(107, 38);
            btnDownloadLocalRecording.TabIndex = 2;
            btnDownloadLocalRecording.Text = "Download";
            btnDownloadLocalRecording.UseVisualStyleBackColor = true;
            btnDownloadLocalRecording.Click += BtnDownloadLocalRecording_Click;
            // 
            // btnRefreshLocalRecordings
            // 
            btnRefreshLocalRecordings.Location = new Point(13, 18);
            btnRefreshLocalRecordings.Margin = new Padding(4, 5, 4, 5);
            btnRefreshLocalRecordings.Name = "btnRefreshLocalRecordings";
            btnRefreshLocalRecordings.Size = new Size(107, 38);
            btnRefreshLocalRecordings.TabIndex = 1;
            btnRefreshLocalRecordings.Text = "Refresh";
            btnRefreshLocalRecordings.UseVisualStyleBackColor = true;
            btnRefreshLocalRecordings.Click += BtnRefreshLocalRecordings_Click;
            // 
            // btnSelectAllLocalRecordings
            // 
            btnSelectAllLocalRecordings.Location = new Point(497, 18);
            btnSelectAllLocalRecordings.Margin = new Padding(4, 5, 4, 5);
            btnSelectAllLocalRecordings.Name = "btnSelectAllLocalRecordings";
            btnSelectAllLocalRecordings.Size = new Size(107, 38);
            btnSelectAllLocalRecordings.TabIndex = 7;
            btnSelectAllLocalRecordings.Text = "Select All";
            btnSelectAllLocalRecordings.UseVisualStyleBackColor = true;
            btnSelectAllLocalRecordings.Click += BtnSelectAllLocalRecordings_Click;
            // 
            // btnBrowseLocalFolder
            // 
            btnBrowseLocalFolder.Location = new Point(558, 63);
            btnBrowseLocalFolder.Margin = new Padding(4, 5, 4, 5);
            btnBrowseLocalFolder.Name = "btnBrowseLocalFolder";
            btnBrowseLocalFolder.Size = new Size(60, 35);
            btnBrowseLocalFolder.TabIndex = 6;
            btnBrowseLocalFolder.Text = "...";
            btnBrowseLocalFolder.UseVisualStyleBackColor = true;
            btnBrowseLocalFolder.Click += BtnBrowseLocalFolder_Click;
            // 
            // txtLocalFolderPath
            // 
            txtLocalFolderPath.Location = new Point(132, 65);
            txtLocalFolderPath.Margin = new Padding(4, 5, 4, 5);
            txtLocalFolderPath.Name = "txtLocalFolderPath";
            txtLocalFolderPath.Size = new Size(418, 31);
            txtLocalFolderPath.TabIndex = 5;
            // 
            // lblLocalFolder
            // 
            lblLocalFolder.AutoSize = true;
            lblLocalFolder.Location = new Point(17, 70);
            lblLocalFolder.Margin = new Padding(4, 0, 4, 0);
            lblLocalFolder.Name = "lblLocalFolder";
            lblLocalFolder.Size = new Size(111, 25);
            lblLocalFolder.TabIndex = 4;
            lblLocalFolder.Text = "Local Folder:";
            // 
            // tabPageConversion
            // 
            tabPageConversion.Controls.Add(splitContainerConversion);
            tabPageConversion.Location = new Point(4, 34);
            tabPageConversion.Name = "tabPageConversion";
            tabPageConversion.Padding = new Padding(3);
            tabPageConversion.Size = new Size(1421, 896);
            tabPageConversion.TabIndex = 1;
            tabPageConversion.Text = "File Conversion";
            tabPageConversion.UseVisualStyleBackColor = true;
            // 
            // splitContainerConversion
            // 
            splitContainerConversion.Dock = DockStyle.Fill;
            splitContainerConversion.FixedPanel = FixedPanel.Panel2;
            splitContainerConversion.Location = new Point(3, 3);
            splitContainerConversion.Name = "splitContainerConversion";
            splitContainerConversion.Orientation = Orientation.Horizontal;
            // 
            // splitContainerConversion.Panel1
            // 
            splitContainerConversion.Panel1.Controls.Add(listViewLocalFiles);
            // 
            // splitContainerConversion.Panel2
            // 
            splitContainerConversion.Panel2.Controls.Add(panelConversionControls);
            splitContainerConversion.Size = new Size(1415, 890);
            splitContainerConversion.SplitterDistance = 720;
            splitContainerConversion.TabIndex = 0;
            // 
            // listViewLocalFiles
            // 
            listViewLocalFiles.CheckBoxes = true;
            listViewLocalFiles.Columns.AddRange(new ColumnHeader[] { colLocalFileName, colLocalSize, colLocalDate, colLocalStatus });
            listViewLocalFiles.Dock = DockStyle.Fill;
            listViewLocalFiles.FullRowSelect = true;
            listViewLocalFiles.Location = new Point(0, 0);
            listViewLocalFiles.Name = "listViewLocalFiles";
            listViewLocalFiles.Size = new Size(1415, 720);
            listViewLocalFiles.TabIndex = 0;
            listViewLocalFiles.UseCompatibleStateImageBehavior = false;
            listViewLocalFiles.View = View.Details;
            // 
            // colLocalFileName
            // 
            colLocalFileName.Text = "File Name";
            colLocalFileName.Width = 500;
            // 
            // colLocalSize
            // 
            colLocalSize.Text = "Size";
            colLocalSize.Width = 120;
            // 
            // colLocalDate
            // 
            colLocalDate.Text = "Modified Date";
            colLocalDate.Width = 180;
            // 
            // colLocalStatus
            // 
            colLocalStatus.Text = "Status";
            colLocalStatus.Width = 200;
            // 
            // panelConversionControls
            // 
            panelConversionControls.Controls.Add(lblConversionInfo);
            panelConversionControls.Controls.Add(btnSelectAll);
            panelConversionControls.Controls.Add(btnSelectNone);
            panelConversionControls.Controls.Add(btnRefreshFiles);
            panelConversionControls.Controls.Add(btnBrowseFolder);
            panelConversionControls.Controls.Add(txtConversionFolder);
            panelConversionControls.Controls.Add(lblConversionFolder);
            panelConversionControls.Controls.Add(btnConvertSelected);
            panelConversionControls.Controls.Add(btnOrganizeMp4Files);
            panelConversionControls.Dock = DockStyle.Fill;
            panelConversionControls.Location = new Point(0, 0);
            panelConversionControls.Name = "panelConversionControls";
            panelConversionControls.Size = new Size(1415, 166);
            panelConversionControls.TabIndex = 0;
            // 
            // lblConversionInfo
            // 
            lblConversionInfo.AutoSize = true;
            lblConversionInfo.ForeColor = SystemColors.GrayText;
            lblConversionInfo.Location = new Point(13, 110);
            lblConversionInfo.Name = "lblConversionInfo";
            lblConversionInfo.Size = new Size(662, 25);
            lblConversionInfo.TabIndex = 7;
            lblConversionInfo.Text = "Select .TS files to convert to .MP4. Conversion settings can be changed in Settings.";
            // 
            // btnSelectAll
            // 
            btnSelectAll.Location = new Point(245, 60);
            btnSelectAll.Name = "btnSelectAll";
            btnSelectAll.Size = new Size(107, 38);
            btnSelectAll.TabIndex = 6;
            btnSelectAll.Text = "Select All";
            btnSelectAll.UseVisualStyleBackColor = true;
            btnSelectAll.Click += BtnSelectAll_Click;
            // 
            // btnSelectNone
            // 
            btnSelectNone.Location = new Point(358, 60);
            btnSelectNone.Name = "btnSelectNone";
            btnSelectNone.Size = new Size(107, 38);
            btnSelectNone.TabIndex = 5;
            btnSelectNone.Text = "Select None";
            btnSelectNone.UseVisualStyleBackColor = true;
            btnSelectNone.Click += BtnSelectNone_Click;
            // 
            // btnRefreshFiles
            // 
            btnRefreshFiles.Location = new Point(132, 60);
            btnRefreshFiles.Name = "btnRefreshFiles";
            btnRefreshFiles.Size = new Size(107, 38);
            btnRefreshFiles.TabIndex = 4;
            btnRefreshFiles.Text = "Refresh";
            btnRefreshFiles.UseVisualStyleBackColor = true;
            btnRefreshFiles.Click += BtnRefreshFiles_Click;
            // 
            // btnBrowseFolder
            // 
            btnBrowseFolder.Location = new Point(1360, 16);
            btnBrowseFolder.Name = "btnBrowseFolder";
            btnBrowseFolder.Size = new Size(35, 30);
            btnBrowseFolder.TabIndex = 3;
            btnBrowseFolder.Text = "...";
            btnBrowseFolder.UseVisualStyleBackColor = true;
            btnBrowseFolder.Click += BtnBrowseFolder_Click;
            // 
            // txtConversionFolder
            // 
            txtConversionFolder.Location = new Point(92, 16);
            txtConversionFolder.Name = "txtConversionFolder";
            txtConversionFolder.Size = new Size(1262, 31);
            txtConversionFolder.TabIndex = 2;
            // 
            // lblConversionFolder
            // 
            lblConversionFolder.AutoSize = true;
            lblConversionFolder.Location = new Point(13, 19);
            lblConversionFolder.Name = "lblConversionFolder";
            lblConversionFolder.Size = new Size(66, 25);
            lblConversionFolder.TabIndex = 1;
            lblConversionFolder.Text = "Folder:";
            // 
            // btnConvertSelected
            // 
            btnConvertSelected.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnConvertSelected.Location = new Point(13, 60);
            btnConvertSelected.Name = "btnConvertSelected";
            btnConvertSelected.Size = new Size(113, 38);
            btnConvertSelected.TabIndex = 0;
            btnConvertSelected.Text = "Convert";
            btnConvertSelected.UseVisualStyleBackColor = true;
            btnConvertSelected.Click += BtnConvertSelected_Click;
            // 
            // btnOrganizeMp4Files
            // 
            btnOrganizeMp4Files.Location = new Point(471, 60);
            btnOrganizeMp4Files.Name = "btnOrganizeMp4Files";
            btnOrganizeMp4Files.Size = new Size(107, 38);
            btnOrganizeMp4Files.TabIndex = 8;
            btnOrganizeMp4Files.Text = "Organize";
            btnOrganizeMp4Files.UseVisualStyleBackColor = true;
            btnOrganizeMp4Files.Click += BtnOrganizeMp4Files_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1429, 1000);
            Controls.Add(tabControl);
            Controls.Add(statusStrip);
            Controls.Add(toolStrip);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "Enigma2 Recording Manager";
            FormClosing += Form1_FormClosing;
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            tabControl.ResumeLayout(false);
            tabPageRecordings.ResumeLayout(false);
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            contextMenuStripRecordings.ResumeLayout(false);
            panelRecordingDetails.ResumeLayout(false);
            panelRecordingDetails.PerformLayout();
            panelControls.ResumeLayout(false);
            panelControls.PerformLayout();
            tabPageLocalRecordings.ResumeLayout(false);
            splitContainerLocal.Panel1.ResumeLayout(false);
            splitContainerLocal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerLocal).EndInit();
            splitContainerLocal.ResumeLayout(false);
            contextMenuStripLocalRecordings.ResumeLayout(false);
            panelLocalRecordingDetails.ResumeLayout(false);
            panelLocalRecordingDetails.PerformLayout();
            panelLocalControls.ResumeLayout(false);
            panelLocalControls.PerformLayout();
            tabPageConversion.ResumeLayout(false);
            splitContainerConversion.Panel1.ResumeLayout(false);
            splitContainerConversion.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerConversion).EndInit();
            splitContainerConversion.ResumeLayout(false);
            panelConversionControls.ResumeLayout(false);
            panelConversionControls.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip;
        private ToolStripButton btnConnect;
        private ToolStripButton btnDisconnect;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripLabel lblConnectionStatus;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton btnSettings;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel;
        private TabControl tabControl;
        private TabPage tabPageRecordings;
        private SplitContainer splitContainer;
        private ListView listViewRecordings;
        private ColumnHeader colTitle;
        private ColumnHeader colServiceName;
        private ColumnHeader colDate;
        private ColumnHeader colDuration;
        private ColumnHeader colSize;
        private ColumnHeader colFileName;
        private ContextMenuStrip contextMenuStripRecordings;
        private ToolStripMenuItem menuItemView;
        private ToolStripMenuItem menuItemDownload;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem menuItemDelete;
        private Panel panelRecordingDetails;
        private TextBox txtDescription;
        private Label lblDescriptionLabel;
        private Panel panelControls;
        private Label lblDownloadStatus;
        private ProgressBar progressBarDownload;
        private CheckBox checkBoxRenameFromEIT;
        private CheckBox checkBoxRemoveAfterDownload;
        private Button btnDeleteRecording;
        private Button btnDownloadRecording;
        private Button btnRefreshRecordings;
        private Button btnSelectAllRecordings;
        private TabPage tabPageLocalRecordings;
        private SplitContainer splitContainerLocal;
        private ListView listViewLocalRecordings;
        private ColumnHeader colLRTitle;
        private ColumnHeader colLRServiceName;
        private ColumnHeader colLRDate;
        private ColumnHeader colLRDuration;
        private ColumnHeader colLRSize;
        private ColumnHeader colLRFileName;
        private ContextMenuStrip contextMenuStripLocalRecordings;
        private ToolStripMenuItem menuItemLocalView;
        private ToolStripMenuItem menuItemLocalDownload;
        private ToolStripSeparator toolStripSeparator4;
        private Panel panelLocalRecordingDetails;
        private TextBox txtLocalDescription;
        private Label lblLocalDescriptionLabel;
        private Panel panelLocalControls;
        private CheckBox checkBoxLocalRenameFromEIT;
        private Button btnDownloadLocalRecording;
        private Button btnRefreshLocalRecordings;
        private Button btnSelectAllLocalRecordings;
        private Button btnBrowseLocalFolder;
        private TextBox txtLocalFolderPath;
        private Label lblLocalFolder;
        private TabPage tabPageConversion;
        private SplitContainer splitContainerConversion;
        private ListView listViewLocalFiles;
        private ColumnHeader colLocalFileName;
        private ColumnHeader colLocalSize;
        private ColumnHeader colLocalDate;
        private ColumnHeader colLocalStatus;
        private Panel panelConversionControls;
        private Label lblConversionFolder;
        private Button btnConvertSelected;
        private TextBox txtConversionFolder;
        private Button btnBrowseFolder;
        private Button btnRefreshFiles;
        private Button btnSelectAll;
        private Button btnSelectNone;
        private Label lblConversionInfo;
        private Button btnOrganizeMp4Files;
    }
}
