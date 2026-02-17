using E2ReccordingManager.Dialogs;
using E2ReccordingManager.Models;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;

namespace E2ReccordingManager
{
    public partial class Form1 : Form
    {
        private HttpClient? httpClient;
        private string baseUrl = string.Empty;
        private bool isConnected = false;
        private List<Recording> currentRecordings = new List<Recording>();
        private ConnectionProfile? currentProfile;

        public Form1()
        {
            InitializeComponent();
        }

        private async void BtnConnect_Click(object? sender, EventArgs e)
        {
            var dialog = new ConnectionDialog(currentProfile);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                currentProfile = dialog.Profile;
                await ConnectToDevice();
            }
        }

        private async Task ConnectToDevice()
        {
            if (currentProfile == null) return;

            try
            {
                toolStripStatusLabel.Text = "Connecting...";

                baseUrl = $"http://{currentProfile.Host}:{currentProfile.Port}";

                var handler = new HttpClientHandler
                {
                    Credentials = new System.Net.NetworkCredential(
                        currentProfile.Username, 
                        currentProfile.Password)
                };

                httpClient = new HttpClient(handler)
                {
                    Timeout = TimeSpan.FromSeconds(30)
                };

                var response = await httpClient.GetStringAsync($"{baseUrl}/web/about");

                isConnected = true;
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                btnRefreshRecordings.Enabled = true;
                lblConnectionStatus.Text = $"Connected to {currentProfile.Host}";
                toolStripStatusLabel.Text = "Connected";

                MessageBox.Show($"Successfully connected to {currentProfile.Host}", 
                    "Connected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed: {ex.Message}", 
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "Connection failed";
            }
        }

        private void BtnDisconnect_Click(object? sender, EventArgs e)
        {
            DisconnectFromDevice();
        }

        private void DisconnectFromDevice()
        {
            if (httpClient != null)
            {
                httpClient.Dispose();
                httpClient = null;
            }

            isConnected = false;
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            btnRefreshRecordings.Enabled = false;
            btnDownloadRecording.Enabled = false;
            btnDeleteRecording.Enabled = false;
            lblConnectionStatus.Text = "Not Connected";
            toolStripStatusLabel.Text = "Disconnected";
            listViewRecordings.Items.Clear();
            currentRecordings.Clear();
            txtDescription.Clear();
        }

        private async void BtnRefreshRecordings_Click(object? sender, EventArgs e)
        {
            if (httpClient == null || !isConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            StatusDialog? statusDialog = null;

            try
            {
                listViewRecordings.Items.Clear();
                currentRecordings.Clear();
                toolStripStatusLabel.Text = "Loading recordings...";
                btnRefreshRecordings.Enabled = false;

                statusDialog = new StatusDialog();
                statusDialog.Show(this);
                statusDialog.UpdateStatus("Fetching recording list from device...");
                statusDialog.UpdateProgress(0, 100);

                var recordings = await GetRecordingsFromDevice(statusDialog);
                currentRecordings = recordings;

                if (recordings.Count == 0)
                {
                    statusDialog.UpdateStatus("No recordings found on device");
                    statusDialog.SetComplete(false);

                    MessageBox.Show("No recordings found on the device.", 
                        "No Recordings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    statusDialog.UpdateStatus("Displaying recordings...");

                    foreach (var recording in recordings)
                    {
                        var item = new ListViewItem(recording.Title);
                        item.SubItems.Add(recording.ServiceName);
                        item.SubItems.Add(recording.RecordingDate.ToString("yyyy-MM-dd HH:mm"));
                        item.SubItems.Add(recording.FormattedDuration);
                        item.SubItems.Add(recording.FormattedSize);
                        item.SubItems.Add(recording.FileName);

                        if (recording.HasEITData)
                        {
                            var tooltipText = $"{recording.EITTitle}\n";
                            if (!string.IsNullOrEmpty(recording.EITShortDescription))
                            {
                                tooltipText += $"\n{recording.EITShortDescription}";
                            }
                            if (!string.IsNullOrEmpty(recording.EITExtendedDescription))
                            {
                                tooltipText += $"\n\n{recording.EITExtendedDescription}";
                            }
                            item.ToolTipText = tooltipText;
                            item.ForeColor = Color.FromArgb(0, 100, 0);
                        }

                        item.Tag = recording;
                        listViewRecordings.Items.Add(item);
                    }

                    var eitCount = recordings.Count(r => r.HasEITData);
                    statusDialog.UpdateStatus($"✓ Loaded {recordings.Count} recordings ({eitCount} with EPG data)");
                    statusDialog.SetComplete(true);

                    toolStripStatusLabel.Text = $"✓ Loaded {recordings.Count} recordings";
                }
            }
            catch (Exception ex)
            {
                if (statusDialog != null)
                {
                    statusDialog.UpdateStatus($"✗ Error: {ex.Message}");
                    statusDialog.SetComplete(false);
                }

                MessageBox.Show($"Error loading recordings:\n\n{ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "Connected";
            }
            finally
            {
                btnRefreshRecordings.Enabled = true;
            }
        }

        private async Task<List<Recording>> GetRecordingsFromDevice(StatusDialog? statusDialog = null)
        {
            var recordings = new List<Recording>();

            try
            {
                statusDialog?.UpdateStatus("Downloading recording list...");
                var response = await httpClient!.GetStringAsync($"{baseUrl}/web/movielist");

                var xml = XDocument.Parse(response);
                var movies = xml.Descendants("e2movie").ToList();

                statusDialog?.UpdateStatus($"Processing {movies.Count} recordings...");
                statusDialog?.UpdateProgress(0, movies.Count);

                int processedCount = 0;
                foreach (var movie in movies)
                {
                    try
                    {
                        var title = movie.Element("e2title")?.Value ?? "Unknown";
                        var serviceName = movie.Element("e2servicename")?.Value ?? "";
                        var serviceRef = movie.Element("e2servicereference")?.Value ?? "";
                        var description = movie.Element("e2description")?.Value ?? "";
                        var descExt = movie.Element("e2descriptionextended")?.Value ?? "";
                        var fileName = movie.Element("e2filename")?.Value ?? "";

                        var recording = new Recording
                        {
                            Title = title,
                            ServiceName = serviceName,
                            ServiceReference = serviceRef,
                            Description = description,
                            DescriptionExtended = descExt,
                            FileName = fileName
                        };

                        if (long.TryParse(movie.Element("e2filesize")?.Value, out long fileSize))
                        {
                            recording.FileSizeBytes = fileSize;
                        }

                        if (int.TryParse(movie.Element("e2length")?.Value, out int length))
                        {
                            recording.DurationSeconds = length / 60;
                        }

                        if (long.TryParse(movie.Element("e2time")?.Value, out long unixTime))
                        {
                            recording.RecordingDate = DateTimeOffset.FromUnixTimeSeconds(unixTime).LocalDateTime;
                        }

                        recordings.Add(recording);
                        processedCount++;
                        statusDialog?.UpdateProgress(processedCount, movies.Count);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error parsing recording: {ex.Message}");
                    }
                }

                statusDialog?.UpdateStatus($"Loaded {recordings.Count} recordings");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting recordings: {ex.Message}");
                throw;
            }

            return recordings;
        }

        private async void BtnDownloadRecording_Click(object? sender, EventArgs e)
        {
            if (listViewRecordings.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a recording to download.", 
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var folderDialog = new FolderBrowserDialog
            {
                Description = "Select destination folder for recordings"
            };

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedRecordings = listViewRecordings.SelectedItems
                    .Cast<ListViewItem>()
                    .Select(item => (Recording)item.Tag!)
                    .ToList();

                await BatchDownloadRecordings(selectedRecordings, folderDialog.SelectedPath);
            }
        }

        private async Task BatchDownloadRecordings(List<Recording> recordings, string destinationPath)
        {
            progressBarDownload.Visible = true;
            progressBarDownload.Maximum = recordings.Count;
            progressBarDownload.Value = 0;

            btnDownloadRecording.Enabled = false;
            btnDeleteRecording.Enabled = false;
            btnRefreshRecordings.Enabled = false;

            int successCount = 0;
            int failCount = 0;

            foreach (var recording in recordings)
            {
                try
                {
                    lblDownloadStatus.Text = $"Downloading: {recording.Title}...";
                    Application.DoEvents();

                    var sourceUrl = $"{baseUrl}/file?file={Uri.EscapeDataString(recording.FileName)}";
                    var fileName = Path.GetFileName(recording.FileName);
                    var destPath = Path.Combine(destinationPath, fileName);

                    using var response = await httpClient!.GetAsync(sourceUrl, HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode();

                    using var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write, FileShare.None);
                    await response.Content.CopyToAsync(fileStream);

                    successCount++;

                    if (checkBoxRemoveAfterDownload.Checked)
                    {
                        await DeleteRecordingFromDevice(recording);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error downloading {recording.Title}: {ex.Message}");
                    failCount++;
                }

                progressBarDownload.Value++;
            }

            lblDownloadStatus.Text = $"Download complete: {successCount} successful, {failCount} failed";
            progressBarDownload.Visible = false;

            btnDownloadRecording.Enabled = true;
            btnDeleteRecording.Enabled = true;
            btnRefreshRecordings.Enabled = true;

            if (checkBoxRemoveAfterDownload.Checked && successCount > 0)
            {
                BtnRefreshRecordings_Click(null, EventArgs.Empty);
            }

            MessageBox.Show($"Download complete!\n\nSuccessful: {successCount}\nFailed: {failCount}", 
                "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void BtnDeleteRecording_Click(object? sender, EventArgs e)
        {
            if (listViewRecordings.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a recording to delete.", 
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRecordings = listViewRecordings.SelectedItems
                .Cast<ListViewItem>()
                .Select(item => (Recording)item.Tag!)
                .ToList();

            var message = selectedRecordings.Count == 1
                ? $"Are you sure you want to delete '{selectedRecordings[0].Title}'?"
                : $"Are you sure you want to delete {selectedRecordings.Count} recordings?";

            if (MessageBox.Show(message, "Confirm Delete", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int successCount = 0;
                foreach (var recording in selectedRecordings)
                {
                    try
                    {
                        await DeleteRecordingFromDevice(recording);
                        successCount++;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error deleting {recording.Title}: {ex.Message}");
                    }
                }

                MessageBox.Show($"Deleted {successCount} of {selectedRecordings.Count} recordings.", 
                    "Delete Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                BtnRefreshRecordings_Click(null, EventArgs.Empty);
            }
        }

        private async Task DeleteRecordingFromDevice(Recording recording)
        {
            var deleteUrl = $"{baseUrl}/web/moviedelete?sRef={Uri.EscapeDataString(recording.ServiceReference)}";
            await httpClient!.GetStringAsync(deleteUrl);
        }

        private void ListViewRecordings_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (listViewRecordings.SelectedItems.Count > 0)
            {
                var recording = (Recording)listViewRecordings.SelectedItems[0].Tag!;

                var descriptionText = new StringBuilder();
                descriptionText.AppendLine($"Title: {recording.Title}");
                descriptionText.AppendLine($"Channel: {recording.ServiceName}");
                descriptionText.AppendLine($"Date: {recording.RecordingDate:yyyy-MM-dd HH:mm}");
                descriptionText.AppendLine($"Duration: {recording.FormattedDuration}");
                descriptionText.AppendLine($"Size: {recording.FormattedSize}");
                descriptionText.AppendLine($"File: {recording.FileName}");

                if (recording.HasEITData)
                {
                    descriptionText.AppendLine("\n--- EPG Information ---");
                    descriptionText.AppendLine($"Title: {recording.EITTitle}");
                    if (!string.IsNullOrEmpty(recording.EITShortDescription))
                    {
                        descriptionText.AppendLine($"\n{recording.EITShortDescription}");
                    }
                    if (!string.IsNullOrEmpty(recording.EITExtendedDescription))
                    {
                        descriptionText.AppendLine($"\n{recording.EITExtendedDescription}");
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(recording.Description))
                    {
                        descriptionText.AppendLine($"\nDescription: {recording.Description}");
                    }
                    if (!string.IsNullOrEmpty(recording.DescriptionExtended))
                    {
                        descriptionText.AppendLine($"\n{recording.DescriptionExtended}");
                    }
                }

                txtDescription.Text = descriptionText.ToString();
                btnDownloadRecording.Enabled = true;
                btnDeleteRecording.Enabled = true;
            }
            else
            {
                txtDescription.Clear();
                btnDownloadRecording.Enabled = false;
                btnDeleteRecording.Enabled = false;
            }
        }

        private void MenuItemView_Click(object? sender, EventArgs e)
        {
            if (listViewRecordings.SelectedItems.Count > 0)
            {
                var recording = (Recording)listViewRecordings.SelectedItems[0].Tag!;

                var message = new StringBuilder();
                message.AppendLine($"Title: {recording.Title}");
                message.AppendLine($"Channel: {recording.ServiceName}");
                message.AppendLine($"Date: {recording.RecordingDate:yyyy-MM-dd HH:mm}");
                message.AppendLine($"Duration: {recording.FormattedDuration}");
                message.AppendLine($"Size: {recording.FormattedSize}");
                message.AppendLine($"File: {recording.FileName}");

                if (recording.HasEITData)
                {
                    message.AppendLine("\n--- EPG Information ---");
                    message.AppendLine($"Title: {recording.EITTitle}");
                    if (!string.IsNullOrEmpty(recording.EITShortDescription))
                    {
                        message.AppendLine($"\n{recording.EITShortDescription}");
                    }
                    if (!string.IsNullOrEmpty(recording.EITExtendedDescription))
                    {
                        message.AppendLine($"\n{recording.EITExtendedDescription}");
                    }
                }

                MessageBox.Show(message.ToString(), "Recording Details", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            DisconnectFromDevice();
        }
    }
}
