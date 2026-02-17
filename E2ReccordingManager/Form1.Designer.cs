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
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
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
            checkBoxRemoveAfterDownload = new CheckBox();
            btnDeleteRecording = new Button();
            btnDownloadRecording = new Button();
            btnRefreshRecordings = new Button();
            toolStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            contextMenuStripRecordings.SuspendLayout();
            panelRecordingDetails.SuspendLayout();
            panelControls.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip
            // 
            toolStrip.ImageScalingSize = new Size(24, 24);
            toolStrip.Items.AddRange(new ToolStripItem[] { btnConnect, btnDisconnect, toolStripSeparator1, lblConnectionStatus });
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
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 34);
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
            splitContainer.Size = new Size(1429, 934);
            splitContainer.SplitterDistance = 591;
            splitContainer.SplitterWidth = 7;
            splitContainer.TabIndex = 2;
            // 
            // listViewRecordings
            // 
            listViewRecordings.Columns.AddRange(new ColumnHeader[] { colTitle, colServiceName, colDate, colDuration, colSize, colFileName });
            listViewRecordings.ContextMenuStrip = contextMenuStripRecordings;
            listViewRecordings.Dock = DockStyle.Fill;
            listViewRecordings.FullRowSelect = true;
            listViewRecordings.Location = new Point(0, 0);
            listViewRecordings.Margin = new Padding(4, 5, 4, 5);
            listViewRecordings.Name = "listViewRecordings";
            listViewRecordings.Size = new Size(1429, 591);
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
            panelRecordingDetails.Size = new Size(1429, 203);
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
            txtDescription.Size = new Size(1415, 162);
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
            panelControls.Controls.Add(checkBoxRemoveAfterDownload);
            panelControls.Controls.Add(btnDeleteRecording);
            panelControls.Controls.Add(btnDownloadRecording);
            panelControls.Controls.Add(btnRefreshRecordings);
            panelControls.Dock = DockStyle.Bottom;
            panelControls.Location = new Point(0, 203);
            panelControls.Margin = new Padding(4, 5, 4, 5);
            panelControls.Name = "panelControls";
            panelControls.Size = new Size(1429, 133);
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1429, 1000);
            Controls.Add(splitContainer);
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
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            contextMenuStripRecordings.ResumeLayout(false);
            panelRecordingDetails.ResumeLayout(false);
            panelRecordingDetails.PerformLayout();
            panelControls.ResumeLayout(false);
            panelControls.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip;
        private ToolStripButton btnConnect;
        private ToolStripButton btnDisconnect;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripLabel lblConnectionStatus;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel;
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
        private CheckBox checkBoxRemoveAfterDownload;
        private Button btnDeleteRecording;
        private Button btnDownloadRecording;
        private Button btnRefreshRecordings;
    }
}
