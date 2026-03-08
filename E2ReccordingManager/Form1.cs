using E2ReccordingManager.Dialogs;
using E2ReccordingManager.Models;
using E2ReccordingManager.Utilities;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace E2ReccordingManager
{
    public partial class Form1 : Form
    {
        private HttpClient? httpClient;
        private string baseUrl = string.Empty;
        private bool isConnected = false;
        private List<Recording> currentRecordings = new List<Recording>();
        private List<Recording> currentLocalRecordings = new List<Recording>();
        private ConnectionProfile? currentProfile;
        private readonly ConnectionProfileManager profileManager;
        private readonly AppSettings appSettings;

        public Form1()
        {
            InitializeComponent();
            profileManager = new ConnectionProfileManager();
            appSettings = AppSettings.Load();
            LoadLastProfile();
            LoadSettings();
        }

        private void LoadSettings()
        {
            // Load local folder settings for Local Recordings tab
            txtLocalFolderPath.Text = appSettings.LocalFolderPath;
        }

        private void LoadLastProfile()
        {
            var lastProfileName = profileManager.GetLastUsedProfile();
            if (!string.IsNullOrEmpty(lastProfileName))
            {
                currentProfile = profileManager.GetProfile(lastProfileName);
            }
        }

        private async void BtnConnect_Click(object? sender, EventArgs e)
        {
            var dialog = new ConnectionDialog(profileManager, currentProfile);
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

                // Automatically refresh recordings after successful connection
                await Task.Delay(100); // Small delay to allow UI to update
                BtnRefreshRecordings_Click(null, EventArgs.Empty);
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
            btnSelectAllRecordings.Enabled = false;
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
                    btnSelectAllRecordings.Enabled = true;
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

                        // Try to download and parse EIT file for EPG data
                        await TryLoadEITData(recording);

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

        private async Task TryLoadEITData(Recording recording)
        {
            try
            {
                // EIT file has the same name as the recording file but with .eit extension
                var eitFileName = Path.ChangeExtension(recording.FileName, ".eit");

                System.Diagnostics.Debug.WriteLine($"Attempting to download EIT file: {eitFileName}");

                // Download EIT file
                var eitUrl = $"{baseUrl}/file?file={Uri.EscapeDataString(eitFileName)}";

                var response = await httpClient!.GetAsync(eitUrl);

                if (response.IsSuccessStatusCode)
                {
                    var eitData = await response.Content.ReadAsByteArrayAsync();

                    System.Diagnostics.Debug.WriteLine($"Downloaded EIT file, size: {eitData.Length} bytes");

                    // Parse EIT data
                    var eitInfo = EITParser.ParseEIT(eitData);

                    if (eitInfo != null)
                    {
                        recording.HasEITData = true;
                        recording.EITTitle = eitInfo.Title;
                        recording.EITShortDescription = eitInfo.ShortDescription;
                        recording.EITExtendedDescription = eitInfo.ExtendedDescription;
                        recording.EITStartTime = eitInfo.StartTime;
                        recording.EITDuration = eitInfo.DurationSeconds;

                        System.Diagnostics.Debug.WriteLine($"✓ Parsed EIT data - Title: {eitInfo.Title}");

                        // If recording date is not set or incorrect, use EIT data
                        if (recording.RecordingDate == DateTime.MinValue && eitInfo.StartTime != DateTime.MinValue)
                        {
                            recording.RecordingDate = eitInfo.StartTime;
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("✗ EIT parsing failed - parser returned null");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"✗ EIT file not found or not accessible (Status: {response.StatusCode})");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"✗ Error loading EIT data: {ex.Message}");
                // Don't throw - EIT data is optional
            }
        }

        private async Task<List<Recording>> GetLocalRecordings(string folderPath, StatusDialog? statusDialog = null)
        {
            var recordings = new List<Recording>();

            try
            {
                statusDialog?.UpdateStatus("Scanning local folder...");

                // Get .ts files
                var tsFiles = Directory.GetFiles(folderPath, "*.ts", SearchOption.TopDirectoryOnly);

                statusDialog?.UpdateStatus($"Processing {tsFiles.Length} local files...");
                statusDialog?.UpdateProgress(0, tsFiles.Length);

                int processedCount = 0;
                foreach (var tsFile in tsFiles)
                {
                    try
                    {
                        var fileInfo = new FileInfo(tsFile);
                        var fileName = fileInfo.Name;
                        var fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);

                        // Parse filename to extract metadata
                        var parsedInfo = ParseRecordingFilename(fileNameWithoutExt);

                        var recording = new Recording
                        {
                            Title = parsedInfo.Title,
                            ServiceName = parsedInfo.ServiceName,
                            ServiceReference = string.Empty,
                            Description = parsedInfo.Description,
                            DescriptionExtended = string.Empty,
                            FileName = fileName,
                            FileSizeBytes = fileInfo.Length,
                            RecordingDate = parsedInfo.Date ?? fileInfo.LastWriteTime,
                            DurationSeconds = 0,
                            IsLocalFile = true,
                            LocalFilePath = tsFile
                        };

                        // Try to load EIT data from local .eit file
                        await TryLoadLocalEITData(recording, folderPath);

                        recordings.Add(recording);
                        processedCount++;
                        statusDialog?.UpdateProgress(processedCount, tsFiles.Length);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error processing local file {tsFile}: {ex.Message}");
                    }
                }

                statusDialog?.UpdateStatus($"Loaded {recordings.Count} local recordings");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting local recordings: {ex.Message}");
                throw;
            }

            return recordings;
        }

        private (string Title, string ServiceName, string Description, DateTime? Date) ParseRecordingFilename(string filename)
        {
            // Common Enigma2 filename patterns:
            // 1. "YYYYMMDD HHMM - ServiceName - Title.ts"
            // 2. "Title - Description.ts"
            // 3. "ServiceName - YYYYMMDD - Title.ts"
            // 4. "Title.ts" (simple)

            string title = filename;
            string serviceName = "Local";
            string description = string.Empty;
            DateTime? date = null;

            try
            {
                // Pattern 1: Date at start (YYYYMMDD HHMM - ServiceName - Title)
                var datePattern = @"^(\d{8})\s+(\d{4})\s*-\s*(.+?)\s*-\s*(.+)$";
                var match = Regex.Match(filename, datePattern);
                if (match.Success)
                {
                    var dateStr = match.Groups[1].Value; // YYYYMMDD
                    var timeStr = match.Groups[2].Value; // HHMM
                    serviceName = match.Groups[3].Value.Trim();
                    title = match.Groups[4].Value.Trim();

                    // Parse date: YYYYMMDD HHMM
                    if (DateTime.TryParseExact($"{dateStr} {timeStr}", "yyyyMMdd HHmm", 
                        System.Globalization.CultureInfo.InvariantCulture, 
                        System.Globalization.DateTimeStyles.None, out var parsedDate))
                    {
                        date = parsedDate;
                    }

                    return (title, serviceName, description, date);
                }

                // Pattern 2: ServiceName - Title - Description
                var servicePattern = @"^(.+?)\s*-\s*(.+?)\s*-\s*(.+)$";
                match = Regex.Match(filename, servicePattern);
                if (match.Success)
                {
                    serviceName = match.Groups[1].Value.Trim();
                    title = match.Groups[2].Value.Trim();
                    description = match.Groups[3].Value.Trim();

                    // Check if first part is actually a date
                    if (DateTime.TryParseExact(serviceName, "yyyyMMdd", 
                        System.Globalization.CultureInfo.InvariantCulture, 
                        System.Globalization.DateTimeStyles.None, out var parsedDate))
                    {
                        date = parsedDate;
                        serviceName = title;
                        title = description;
                        description = string.Empty;
                    }

                    return (title, serviceName, description, date);
                }

                // Pattern 3: ServiceName - Title (two parts)
                var twoPartPattern = @"^(.+?)\s*-\s*(.+)$";
                match = Regex.Match(filename, twoPartPattern);
                if (match.Success)
                {
                    var part1 = match.Groups[1].Value.Trim();
                    var part2 = match.Groups[2].Value.Trim();

                    // Check if first part is a date
                    if (DateTime.TryParseExact(part1, "yyyyMMdd", 
                        System.Globalization.CultureInfo.InvariantCulture, 
                        System.Globalization.DateTimeStyles.None, out var parsedDate))
                    {
                        date = parsedDate;
                        title = part2;
                    }
                    // Check if it looks like ServiceName - Title
                    else if (part1.Length < 30) // Service names are typically shorter
                    {
                        serviceName = part1;
                        title = part2;
                    }
                    else
                    {
                        // Assume it's Title - Description
                        title = part1;
                        description = part2;
                    }

                    return (title, serviceName, description, date);
                }

                // Pattern 4: Look for embedded date in filename (YYYYMMDD or YYYY-MM-DD)
                var embeddedDatePattern = @"(\d{4})-?(\d{2})-?(\d{2})";
                match = Regex.Match(filename, embeddedDatePattern);
                if (match.Success)
                {
                    var year = match.Groups[1].Value;
                    var month = match.Groups[2].Value;
                    var day = match.Groups[3].Value;

                    if (DateTime.TryParse($"{year}-{month}-{day}", out var parsedDate))
                    {
                        date = parsedDate;
                        // Remove date from title
                        title = Regex.Replace(filename, embeddedDatePattern, "").Trim(' ', '-', '_');
                    }
                }

                // Clean up title - remove common separators and extra spaces
                title = Regex.Replace(title, @"[-_]+", " ").Trim();
                title = Regex.Replace(title, @"\s+", " ");

                // If title is still empty or very long, use original filename
                if (string.IsNullOrWhiteSpace(title) || title.Length > 200)
                {
                    title = filename;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error parsing filename '{filename}': {ex.Message}");
                title = filename;
            }

            return (title, serviceName, description, date);
        }

        private async Task TryLoadLocalEITData(Recording recording, string folderPath)
        {
            try
            {
                // EIT file has the same name as the recording file but with .eit extension
                var eitFilePath = Path.Combine(folderPath, Path.ChangeExtension(recording.FileName, ".eit"));

                if (File.Exists(eitFilePath))
                {
                    System.Diagnostics.Debug.WriteLine($"Found local EIT file: {eitFilePath}");

                    var eitData = await File.ReadAllBytesAsync(eitFilePath);

                    System.Diagnostics.Debug.WriteLine($"Read EIT file, size: {eitData.Length} bytes");

                    // Parse EIT data
                    var eitInfo = EITParser.ParseEIT(eitData);

                    if (eitInfo != null)
                    {
                        recording.HasEITData = true;
                        recording.EITTitle = eitInfo.Title;
                        recording.EITShortDescription = eitInfo.ShortDescription;
                        recording.EITExtendedDescription = eitInfo.ExtendedDescription;
                        recording.EITStartTime = eitInfo.StartTime;
                        recording.EITDuration = eitInfo.DurationSeconds;

                        System.Diagnostics.Debug.WriteLine($"✓ Parsed local EIT data - Title: {eitInfo.Title}");

                        // If recording date is not set or incorrect, use EIT data
                        if (recording.RecordingDate == DateTime.MinValue && eitInfo.StartTime != DateTime.MinValue)
                        {
                            recording.RecordingDate = eitInfo.StartTime;
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("✗ EIT parsing failed - parser returned null");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"✗ Local EIT file not found: {eitFilePath}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"✗ Error loading local EIT data: {ex.Message}");
                // Don't throw - EIT data is optional
            }
        }

        private async void BtnDownloadRecording_Click(object? sender, EventArgs e)
        {
            // Get checked items; if none checked, fall back to selected items
            var itemsToDownload = listViewRecordings.CheckedItems.Count > 0
                ? listViewRecordings.CheckedItems.Cast<ListViewItem>().ToList()
                : listViewRecordings.SelectedItems.Cast<ListViewItem>().ToList();

            if (itemsToDownload.Count == 0)
            {
                MessageBox.Show("Please select or check recordings to download.", 
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var folderDialog = new FolderBrowserDialog
            {
                Description = "Select destination folder for recordings"
            };

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedRecordings = itemsToDownload
                    .Select(item => (Recording)item.Tag!)
                    .ToList();

                await BatchDownloadRecordings(selectedRecordings, folderDialog.SelectedPath);
            }
        }

        private async Task BatchDownloadRecordings(List<Recording> recordings, string destinationPath)
        {
            var downloadDialog = new Dialogs.DownloadProgressDialog();
            downloadDialog.Show(this);

            btnDownloadRecording.Enabled = false;
            btnDeleteRecording.Enabled = false;
            btnRefreshRecordings.Enabled = false;

            int successCount = 0;
            int failCount = 0;
            int canceledCount = 0;
            int currentIndex = 0;
            var downloadedFiles = new List<string>();

            try
            {
                downloadDialog.UpdateProgress(0, recordings.Count);
                downloadDialog.UpdateStatus($"Starting download of {recordings.Count} recording(s)...");

                foreach (var recording in recordings)
                {
                    // Check for cancellation
                    if (downloadDialog.CancelRequested)
                    {
                        canceledCount = recordings.Count - currentIndex;
                        break;
                    }

                    currentIndex++;

                    try
                    {
                        // Generate filename
                        var fileName = Path.GetFileName(recording.FileName);
                        if (checkBoxRenameFromEIT.Checked)
                        {
                            fileName = GenerateEITBasedFilename(recording);
                        }

                        downloadDialog.UpdateCurrentFile(fileName);
                        downloadDialog.UpdateStatus($"Processing {currentIndex} of {recordings.Count}...");
                        downloadDialog.UpdateProgress(currentIndex - 1, recordings.Count);

                        var destPath = Path.Combine(destinationPath, fileName);

                        if (recording.IsLocalFile)
                        {
                            // For local files, just copy/rename
                            downloadDialog.UpdateStatus($"Copying local file {currentIndex} of {recordings.Count}...");

                            if (!string.IsNullOrEmpty(recording.LocalFilePath) && File.Exists(recording.LocalFilePath))
                            {
                                // Check if destination already exists and is identical (optimization)
                                if (await ShouldSkipCopy(recording.LocalFilePath, destPath))
                                {
                                    System.Diagnostics.Debug.WriteLine($"Skipping copy - file already exists: {destPath}");
                                    downloadDialog.UpdateFileProgress(100, 100);
                                }
                                else
                                {
                                    // Copy file with progress tracking
                                    await CopyFileWithProgress(recording.LocalFilePath, destPath, downloadDialog);
                                }

                                // Also copy EIT file if it exists
                                var sourceEitPath = Path.ChangeExtension(recording.LocalFilePath, ".eit");
                                if (File.Exists(sourceEitPath))
                                {
                                    var destEitPath = Path.ChangeExtension(destPath, ".eit");
                                    await CopyEitFileAsync(sourceEitPath, destEitPath);
                                }

                                successCount++;
                                downloadedFiles.Add(destPath);
                            }
                            else
                            {
                                throw new FileNotFoundException($"Local file not found: {recording.LocalFilePath}");
                            }
                        }
                        else
                        {
                            // For device files, download via FTP
                            downloadDialog.UpdateStatus($"Downloading {currentIndex} of {recordings.Count}...");

                            var sourceUrl = $"{baseUrl}/file?file={Uri.EscapeDataString(recording.FileName)}";

                            // Download with progress tracking
                            using var response = await httpClient!.GetAsync(sourceUrl, HttpCompletionOption.ResponseHeadersRead);
                            response.EnsureSuccessStatusCode();

                            var totalBytes = response.Content.Headers.ContentLength ?? 0;
                            var buffer = new byte[8192];
                            long totalBytesRead = 0;

                            using var contentStream = await response.Content.ReadAsStreamAsync();
                            using var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write, FileShare.None, buffer.Length, true);

                            int bytesRead;
                            while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                            {
                                // Check for cancellation during download
                                if (downloadDialog.CancelRequested)
                                {
                                    // Delete partial file
                                    fileStream.Close();
                                    if (File.Exists(destPath))
                                    {
                                        File.Delete(destPath);
                                    }
                                    throw new OperationCanceledException("Download canceled by user");
                                }

                                await fileStream.WriteAsync(buffer, 0, bytesRead);
                                totalBytesRead += bytesRead;

                                // Update file progress
                                if (totalBytes > 0)
                                {
                                    downloadDialog.UpdateFileProgress(totalBytesRead, totalBytes);
                                }
                            }

                            successCount++;
                            downloadedFiles.Add(destPath);

                            // Remove from device if requested
                            if (checkBoxRemoveAfterDownload.Checked)
                            {
                                downloadDialog.UpdateStatus($"Removing {fileName} from device...");
                                await DeleteRecordingFromDevice(recording);
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        canceledCount = recordings.Count - currentIndex + 1;
                        break;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error downloading {recording.Title}: {ex.Message}");
                        failCount++;
                    }

                    downloadDialog.UpdateProgress(currentIndex, recordings.Count);
                    downloadDialog.UpdateFileProgress(0, 100); // Reset file progress
                }

                // Show completion status
                downloadDialog.UpdateProgress(currentIndex, recordings.Count);

                string message;
                bool success = failCount == 0 && canceledCount == 0;

                if (canceledCount > 0)
                {
                    message = $"Download canceled!\n\nCompleted: {successCount}\nFailed: {failCount}\nCanceled: {canceledCount}";
                }
                else
                {
                    message = $"Download complete!\n\nSuccessful: {successCount}\nFailed: {failCount}";
                }

                downloadDialog.UpdateStatus(message);

                // Convert TS files to MP4 if enabled
                if (appSettings.AutoConvertToMp4 && downloadedFiles.Count > 0 && canceledCount == 0)
                {
                    await ConvertDownloadedFiles(downloadedFiles, downloadDialog);
                }

                downloadDialog.SetComplete(success, message);

                // Refresh if files were removed
                if (checkBoxRemoveAfterDownload.Checked && successCount > 0)
                {
                    BtnRefreshRecordings_Click(null, EventArgs.Empty);
                }
            }
            finally
            {
                btnDownloadRecording.Enabled = true;
                btnDeleteRecording.Enabled = true;
                btnRefreshRecordings.Enabled = true;

                // Keep the dialog open so user can see the results
                // User will close it manually
            }
        }

        private async Task CopyFileWithProgress(string sourcePath, string destPath, DownloadProgressDialog dialog)
        {
            // Use larger buffer for better performance (1MB for large files, 64KB for smaller)
            var fileInfo = new FileInfo(sourcePath);
            var bufferSize = fileInfo.Length > 100 * 1024 * 1024 ? 1024 * 1024 : 64 * 1024; // 1MB for >100MB files, 64KB otherwise
            var buffer = new byte[bufferSize];

            using var sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, FileOptions.Asynchronous | FileOptions.SequentialScan);
            using var destStream = new FileStream(destPath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, FileOptions.Asynchronous | FileOptions.WriteThrough);

            long totalBytes = sourceStream.Length;
            long totalBytesRead = 0;
            int bytesRead;
            var lastUpdate = DateTime.Now;

            while ((bytesRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                // Check for cancellation
                if (dialog.CancelRequested)
                {
                    // Delete partial file
                    destStream.Close();
                    if (File.Exists(destPath))
                    {
                        File.Delete(destPath);
                    }
                    throw new OperationCanceledException("Copy canceled by user");
                }

                await destStream.WriteAsync(buffer, 0, bytesRead);
                totalBytesRead += bytesRead;

                // Update progress (throttle updates to every 100ms for better performance)
                if (totalBytes > 0 && (DateTime.Now - lastUpdate).TotalMilliseconds > 100)
                {
                    dialog.UpdateFileProgress(totalBytesRead, totalBytes);
                    lastUpdate = DateTime.Now;
                }
            }

            // Final progress update
            if (totalBytes > 0)
            {
                dialog.UpdateFileProgress(totalBytes, totalBytes);
            }
        }

        private async Task<bool> ShouldSkipCopy(string sourcePath, string destPath)
        {
            // If destination doesn't exist, don't skip
            if (!File.Exists(destPath))
                return false;

            try
            {
                // Compare file sizes first (quick check)
                var sourceInfo = new FileInfo(sourcePath);
                var destInfo = new FileInfo(destPath);

                if (sourceInfo.Length != destInfo.Length)
                    return false;

                // If same size and recent timestamp, consider it the same
                // (Avoid expensive content comparison for large files)
                if (Math.Abs((destInfo.LastWriteTime - sourceInfo.LastWriteTime).TotalSeconds) < 2)
                {
                    System.Diagnostics.Debug.WriteLine($"File appears identical based on size and timestamp: {destPath}");
                    return true;
                }

                // For small files (<10MB), do a quick hash comparison
                if (sourceInfo.Length < 10 * 1024 * 1024)
                {
                    return await FilesAreIdenticalAsync(sourcePath, destPath);
                }

                // For large files, assume different to be safe
                return false;
            }
            catch
            {
                // If any error, don't skip
                return false;
            }
        }

        private async Task<bool> FilesAreIdenticalAsync(string file1, string file2)
        {
            try
            {
                using var stream1 = new FileStream(file1, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous);
                using var stream2 = new FileStream(file2, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous);

                if (stream1.Length != stream2.Length)
                    return false;

                var buffer1 = new byte[4096];
                var buffer2 = new byte[4096];

                while (true)
                {
                    var read1 = await stream1.ReadAsync(buffer1, 0, buffer1.Length);
                    var read2 = await stream2.ReadAsync(buffer2, 0, buffer2.Length);

                    if (read1 != read2)
                        return false;

                    if (read1 == 0)
                        break;

                    if (!buffer1.AsSpan(0, read1).SequenceEqual(buffer2.AsSpan(0, read2)))
                        return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task CopyEitFileAsync(string sourcePath, string destPath)
        {
            try
            {
                // For small EIT files, use optimized File.Copy with async wrapper
                await Task.Run(() => File.Copy(sourcePath, destPath, true));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error copying EIT file: {ex.Message}");
                // Don't throw - EIT copy failure shouldn't stop the main operation
            }
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
                .Where(r => !r.IsLocalFile) // Only allow deleting device files
                .ToList();

            if (selectedRecordings.Count == 0)
            {
                MessageBox.Show("Cannot delete local files from this view. Only device recordings can be deleted.", 
                    "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

        private string GenerateEITBasedFilename(Recording recording)
        {
            // Get series name - prefer EIT title, fallback to recording title
            string seriesName;
            if (!string.IsNullOrEmpty(recording.EITTitle))
            {
                seriesName = recording.EITTitle;
            }
            else if (!string.IsNullOrEmpty(recording.Title))
            {
                seriesName = recording.Title;
            }
            else
            {
                // Fallback to original filename without extension
                seriesName = Path.GetFileNameWithoutExtension(recording.FileName);
            }

            // Get description - prefer EIT extended, then short, then recording descriptions
            string description = string.Empty;
            if (recording.HasEITData)
            {
                if (!string.IsNullOrEmpty(recording.EITExtendedDescription))
                {
                    description = recording.EITExtendedDescription;
                }
                else if (!string.IsNullOrEmpty(recording.EITShortDescription))
                {
                    description = recording.EITShortDescription;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(recording.DescriptionExtended))
                {
                    description = recording.DescriptionExtended;
                }
                else if (!string.IsNullOrEmpty(recording.Description))
                {
                    description = recording.Description;
                }
            }

            // Combine series name and description
            string fileName = string.IsNullOrEmpty(description) 
                ? seriesName 
                : $"{seriesName} - {description}";

            // Remove Season/Episode patterns from the end
            fileName = RemoveSeasonEpisodePattern(fileName);

            // Sanitize for file system
            fileName = SanitizeFileName(fileName);

            // Limit total length (Windows max path is 260, leave room for path + extension)
            const int maxFileNameLength = 200;
            if (fileName.Length > maxFileNameLength)
            {
                fileName = fileName.Substring(0, maxFileNameLength);
            }

            // Add original file extension
            string originalExtension = Path.GetExtension(recording.FileName);
            return fileName + originalExtension;
        }

        private string RemoveSeasonEpisodePattern(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            // Remove Season X, Episode Y patterns from end of text
            // Matches reference app format: supports leading separators [, . - _]
            var patterns = new[]
            {
                @"\s*[,\.\-_]\s*Season\s+\d+\s*,?\s*Episode\s+\d+\s*$",  // , Season 3, Episode 1 or _Season 3 Episode 1
                @"\s*[,\.\-_]\s*S\d+E\d+\s*$",                            // -S03E01 or _S03E01
                @"\s*[,\.\-_]\s*s\d+e\d+\s*$",                            // -s03e01 or _s03e01
                @"\s*[,\.\-_]\s*Season\s+\d+\s*$",                        // -Season 3 or _Season 3
                @"\s*[,\.\-_]\s*Episode\s+\d+\s*$",                       // -Episode 1 or _Episode 1
                @"\s*[,\.\-_]\s*Ep\.\s*\d+\s*$",                          // -Ep. 1 or _Ep. 1
                @"\s*\(\d+x\d+\)\s*$",                                    // (3x1)
            };

            foreach (var pattern in patterns)
            {
                text = System.Text.RegularExpressions.Regex.Replace(
                    text, 
                    pattern, 
                    string.Empty, 
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            }

            return text.Trim();
        }

        private string SanitizeFileName(string fileName)
        {
            // Remove invalid filename characters
            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char c in invalidChars)
            {
                fileName = fileName.Replace(c, '_');
            }

            // Replace additional problematic characters
            fileName = fileName.Replace(':', '-');
            fileName = fileName.Replace('/', '-');
            fileName = fileName.Replace('\\', '-');
            fileName = fileName.Replace('?', ' ');
            fileName = fileName.Replace('*', ' ');
            fileName = fileName.Replace('"', '\'');
            fileName = fileName.Replace('<', '(');
            fileName = fileName.Replace('>', ')');
            fileName = fileName.Replace('|', '-');

            // Trim and remove multiple spaces
            fileName = System.Text.RegularExpressions.Regex.Replace(fileName.Trim(), @"\s+", " ");

            return fileName;
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

                if (recording.IsLocalFile)
                {
                    descriptionText.AppendLine($"Source: Local File");
                    descriptionText.AppendLine($"Path: {recording.LocalFilePath}");
                }

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

                // Only enable delete for device files
                btnDeleteRecording.Enabled = !recording.IsLocalFile && isConnected;
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

        private void BtnSelectAllRecordings_Click(object? sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewRecordings.Items)
            {
                item.Checked = true;
            }
        }

        private async Task ConvertDownloadedFiles(List<string> files, DownloadProgressDialog dialog)
        {
            var tsFiles = files.Where(f => f.EndsWith(".ts", StringComparison.OrdinalIgnoreCase)).ToList();
            if (tsFiles.Count == 0) return;

            dialog.SwitchToConversionTab();
            dialog.UpdateConversionStatus($"Starting conversion of {tsFiles.Count} file(s)...");
            dialog.UpdateConversionProgress(0, tsFiles.Count);

            int converted = 0;
            int failed = 0;

            for (int i = 0; i < tsFiles.Count; i++)
            {
                var tsPath = tsFiles[i];
                var fileName = Path.GetFileName(tsPath);

                dialog.UpdateConversionFile(fileName);
                dialog.UpdateConversionStatus($"Converting {i + 1} of {tsFiles.Count}...");
                dialog.UpdateConversionProgress(i, tsFiles.Count);

                try
                {
                    var mp4Path = await ConvertToMp4(tsPath, dialog);
                    if (!string.IsNullOrEmpty(mp4Path))
                    {
                        converted++;
                        System.Diagnostics.Debug.WriteLine($"✓ Converted: {Path.GetFileName(mp4Path)}");
                    }
                    else
                    {
                        failed++;
                        System.Diagnostics.Debug.WriteLine($"✗ Conversion failed: {fileName}");
                    }
                }
                catch (Exception ex)
                {
                    failed++;
                    System.Diagnostics.Debug.WriteLine($"✗ Conversion error for {fileName}: {ex.Message}");
                }

                dialog.UpdateConversionProgress(i + 1, tsFiles.Count);
            }

            var summary = $"Conversion complete!\n\nSuccessful: {converted}\nFailed: {failed}";
            dialog.UpdateConversionStatus(summary);
            System.Diagnostics.Debug.WriteLine($"=== Conversion Summary ===");
            System.Diagnostics.Debug.WriteLine($"Converted: {converted}, Failed: {failed}");
        }

        private async Task<string?> ConvertToMp4(string tsFilePath, DownloadProgressDialog dialog)
        {
            try
            {
                if (!File.Exists(tsFilePath))
                {
                    System.Diagnostics.Debug.WriteLine($"TS file not found: {tsFilePath}");
                    return null;
                }

                var mp4FilePath = Path.ChangeExtension(tsFilePath, ".mp4");

                System.Diagnostics.Debug.WriteLine($"=== Starting Video Conversion ===");
                System.Diagnostics.Debug.WriteLine($"Input:  {Path.GetFileName(tsFilePath)}");
                System.Diagnostics.Debug.WriteLine($"Output: {Path.GetFileName(mp4FilePath)}");
                System.Diagnostics.Debug.WriteLine($"Codec:  H.264 (NVENC)");

                var arguments = BuildFFmpegArguments(tsFilePath, mp4FilePath);
                System.Diagnostics.Debug.WriteLine($"FFmpeg command: {appSettings.FFmpegPath} {arguments}");

                var processInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = appSettings.FFmpegPath,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = new System.Diagnostics.Process { StartInfo = processInfo })
                {
                    var duration = await GetVideoDuration(tsFilePath);
                    System.Diagnostics.Debug.WriteLine($"Video duration: {duration}");

                    process.Start();

                    var progressTask = Task.Run(() => TrackConversionProgress(process, duration, dialog));

                    await process.WaitForExitAsync();
                    await progressTask;

                    if (process.ExitCode == 0)
                    {
                        System.Diagnostics.Debug.WriteLine($"✓ Conversion successful!");

                        if (appSettings.DeleteTsAfterConversion)
                        {
                            try
                            {
                                File.Delete(tsFilePath);
                                System.Diagnostics.Debug.WriteLine($"✓ Deleted original TS file");
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"⚠ Could not delete TS file: {ex.Message}");
                            }
                        }

                        return mp4FilePath;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"✗ FFmpeg exited with code {process.ExitCode}");

                        if (File.Exists(mp4FilePath))
                        {
                            try { File.Delete(mp4FilePath); } catch { }
                        }

                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"✗ Conversion error: {ex.Message}");
                return null;
            }
        }

        private string BuildFFmpegArguments(string inputPath, string outputPath)
        {
            var cq = appSettings.GetQualityCQ();
            var preset = appSettings.GetPresetName();
            var maxrate = appSettings.MaxBitrateMbps;

            if (appSettings.UseHardwareAcceleration)
            {
                return $"-i \"{inputPath}\" " +
                       $"-c:v h264_nvenc " +
                       $"-preset {preset} " +
                       $"-cq {cq} " +
                       $"-b:v 0 " +
                       $"-maxrate {maxrate}M " +
                       $"-c:a copy " +
                       $"-movflags +faststart " +
                       $"-y \"{outputPath}\"";
            }
            else
            {
                return $"-i \"{inputPath}\" " +
                       $"-c:v libx264 " +
                       $"-preset medium " +
                       $"-crf {cq} " +
                       $"-maxrate {maxrate}M " +
                       $"-c:a copy " +
                       $"-movflags +faststart " +
                       $"-y \"{outputPath}\"";
            }
        }

        private async Task<TimeSpan> GetVideoDuration(string videoPath)
        {
            try
            {
                var processInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = appSettings.FFmpegPath,
                    Arguments = $"-i \"{videoPath}\"",
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = new System.Diagnostics.Process { StartInfo = processInfo })
                {
                    process.Start();
                    var output = await process.StandardError.ReadToEndAsync();
                    await process.WaitForExitAsync();

                    var match = Regex.Match(output, @"Duration: (\d{2}):(\d{2}):(\d{2})\.(\d{2})");
                    if (match.Success)
                    {
                        int hours = int.Parse(match.Groups[1].Value);
                        int minutes = int.Parse(match.Groups[2].Value);
                        int seconds = int.Parse(match.Groups[3].Value);
                        return new TimeSpan(hours, minutes, seconds);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting duration: {ex.Message}");
            }

            return TimeSpan.Zero;
        }

        private async Task TrackConversionProgress(System.Diagnostics.Process process, TimeSpan duration, DownloadProgressDialog dialog)
        {
            try
            {
                var reader = process.StandardError;
                string? line;
                var lastUpdate = DateTime.Now;

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var timeMatch = Regex.Match(line, @"time=(\d{2}):(\d{2}):(\d{2})\.(\d{2})");
                    if (timeMatch.Success && duration.TotalSeconds > 0)
                    {
                        int hours = int.Parse(timeMatch.Groups[1].Value);
                        int minutes = int.Parse(timeMatch.Groups[2].Value);
                        int seconds = int.Parse(timeMatch.Groups[3].Value);

                        var currentTime = new TimeSpan(hours, minutes, seconds);
                        var progress = (int)((currentTime.TotalSeconds / duration.TotalSeconds) * 100);
                        progress = Math.Min(Math.Max(progress, 0), 100);

                        if ((DateTime.Now - lastUpdate).TotalMilliseconds > 500)
                        {
                            dialog.UpdateConversionFileProgress(progress, $"{currentTime:hh\\:mm\\:ss} / {duration:hh\\:mm\\:ss}");
                            lastUpdate = DateTime.Now;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error tracking progress: {ex.Message}");
            }
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            DisconnectFromDevice();
        }

        #region Local Recordings Tab

        private async void BtnRefreshLocalRecordings_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLocalFolderPath.Text) || !Directory.Exists(txtLocalFolderPath.Text))
            {
                MessageBox.Show("Please select a valid local folder first.", "No Folder Selected", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            StatusDialog? statusDialog = null;

            try
            {
                listViewLocalRecordings.Items.Clear();
                currentLocalRecordings.Clear();
                toolStripStatusLabel.Text = "Loading local recordings...";
                btnRefreshLocalRecordings.Enabled = false;

                statusDialog = new StatusDialog();
                statusDialog.Show(this);
                statusDialog.UpdateStatus("Loading local recordings...");
                statusDialog.UpdateProgress(0, 100);

                var localRecordings = await GetLocalRecordings(txtLocalFolderPath.Text, statusDialog);
                currentLocalRecordings = localRecordings;

                if (localRecordings.Count == 0)
                {
                    statusDialog.UpdateStatus("No recordings found in local folder");
                    statusDialog.SetComplete(false);

                    MessageBox.Show("No .ts files found in the selected folder.", 
                        "No Recordings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    statusDialog.UpdateStatus("Displaying local recordings...");

                    foreach (var recording in localRecordings)
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
                        listViewLocalRecordings.Items.Add(item);
                    }

                    var eitCount = localRecordings.Count(r => r.HasEITData);
                    statusDialog.UpdateStatus($"✓ Loaded {localRecordings.Count} local recordings ({eitCount} with EPG data)");
                    statusDialog.SetComplete(true);

                    toolStripStatusLabel.Text = $"✓ Loaded {localRecordings.Count} local recordings";
                    btnSelectAllLocalRecordings.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                if (statusDialog != null)
                {
                    statusDialog.UpdateStatus($"✗ Error: {ex.Message}");
                    statusDialog.SetComplete(false);
                }

                MessageBox.Show($"Error loading local recordings:\n\n{ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "Ready";
            }
            finally
            {
                btnRefreshLocalRecordings.Enabled = true;
            }
        }

        private void BtnBrowseLocalFolder_Click(object? sender, EventArgs e)
        {
            using var folderDialog = new FolderBrowserDialog
            {
                Description = "Select folder containing local recording files",
                UseDescriptionForTitle = true,
                ShowNewFolderButton = false
            };

            if (!string.IsNullOrEmpty(txtLocalFolderPath.Text) && Directory.Exists(txtLocalFolderPath.Text))
            {
                folderDialog.SelectedPath = txtLocalFolderPath.Text;
            }

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                txtLocalFolderPath.Text = folderDialog.SelectedPath;
                appSettings.LocalFolderPath = folderDialog.SelectedPath;
                appSettings.Save();
            }
        }

        private async void BtnDownloadLocalRecording_Click(object? sender, EventArgs e)
        {
            // Get checked items; if none checked, fall back to selected items
            var itemsToDownload = listViewLocalRecordings.CheckedItems.Count > 0
                ? listViewLocalRecordings.CheckedItems.Cast<ListViewItem>().ToList()
                : listViewLocalRecordings.SelectedItems.Cast<ListViewItem>().ToList();

            if (itemsToDownload.Count == 0)
            {
                MessageBox.Show("Please select or check recordings to download.", 
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var folderDialog = new FolderBrowserDialog
            {
                Description = "Select destination folder for recordings"
            };

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedRecordings = itemsToDownload
                    .Select(item => (Recording)item.Tag!)
                    .ToList();

                await BatchDownloadLocalRecordings(selectedRecordings, folderDialog.SelectedPath);
            }
        }

        private async Task BatchDownloadLocalRecordings(List<Recording> recordings, string destinationPath)
        {
            var downloadDialog = new Dialogs.DownloadProgressDialog();
            downloadDialog.Show(this);

            btnDownloadLocalRecording.Enabled = false;
            btnRefreshLocalRecordings.Enabled = false;

            int successCount = 0;
            int failCount = 0;
            int canceledCount = 0;
            int currentIndex = 0;
            var downloadedFiles = new List<string>();

            try
            {
                downloadDialog.UpdateProgress(0, recordings.Count);
                downloadDialog.UpdateStatus($"Starting copy of {recordings.Count} recording(s)...");

                foreach (var recording in recordings)
                {
                    if (downloadDialog.CancelRequested)
                    {
                        canceledCount = recordings.Count - currentIndex;
                        break;
                    }

                    currentIndex++;

                    try
                    {
                        var fileName = Path.GetFileName(recording.FileName);
                        if (checkBoxLocalRenameFromEIT.Checked)
                        {
                            fileName = GenerateEITBasedFilename(recording);
                        }

                        downloadDialog.UpdateCurrentFile(fileName);
                        downloadDialog.UpdateStatus($"Copying {currentIndex} of {recordings.Count}...");
                        downloadDialog.UpdateProgress(currentIndex - 1, recordings.Count);

                        var destPath = Path.Combine(destinationPath, fileName);

                        if (!string.IsNullOrEmpty(recording.LocalFilePath) && File.Exists(recording.LocalFilePath))
                        {
                            if (await ShouldSkipCopy(recording.LocalFilePath, destPath))
                            {
                                System.Diagnostics.Debug.WriteLine($"Skipping copy - file already exists: {destPath}");
                                downloadDialog.UpdateFileProgress(100, 100);
                            }
                            else
                            {
                                await CopyFileWithProgress(recording.LocalFilePath, destPath, downloadDialog);
                            }

                            // Do NOT copy EIT file for local recordings - leave it in original location

                            successCount++;
                            downloadedFiles.Add(destPath);
                        }
                        else
                        {
                            throw new FileNotFoundException($"Local file not found: {recording.LocalFilePath}");
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        canceledCount = recordings.Count - currentIndex + 1;
                        break;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error copying {recording.Title}: {ex.Message}");
                        failCount++;
                    }

                    downloadDialog.UpdateProgress(currentIndex, recordings.Count);
                    downloadDialog.UpdateFileProgress(0, 100);
                }

                downloadDialog.UpdateProgress(currentIndex, recordings.Count);

                string message;
                bool success = failCount == 0 && canceledCount == 0;

                if (canceledCount > 0)
                {
                    message = $"Copy canceled!\n\nCompleted: {successCount}\nFailed: {failCount}\nCanceled: {canceledCount}";
                }
                else
                {
                    message = $"Copy complete!\n\nSuccessful: {successCount}\nFailed: {failCount}";
                }

                downloadDialog.UpdateStatus(message);

                if (appSettings.AutoConvertToMp4 && downloadedFiles.Count > 0 && canceledCount == 0)
                {
                    await ConvertDownloadedFiles(downloadedFiles, downloadDialog);
                }

                downloadDialog.SetComplete(success, message);
            }
            finally
            {
                btnDownloadLocalRecording.Enabled = true;
                btnRefreshLocalRecordings.Enabled = true;
            }
        }

        private void ListViewLocalRecordings_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (listViewLocalRecordings.SelectedItems.Count > 0)
            {
                var recording = (Recording)listViewLocalRecordings.SelectedItems[0].Tag!;

                var descriptionText = new StringBuilder();
                descriptionText.AppendLine($"Title: {recording.Title}");
                descriptionText.AppendLine($"Channel: {recording.ServiceName}");
                descriptionText.AppendLine($"Date: {recording.RecordingDate:yyyy-MM-dd HH:mm}");
                descriptionText.AppendLine($"Duration: {recording.FormattedDuration}");
                descriptionText.AppendLine($"Size: {recording.FormattedSize}");
                descriptionText.AppendLine($"File: {recording.FileName}");
                descriptionText.AppendLine($"Source: Local File");
                descriptionText.AppendLine($"Path: {recording.LocalFilePath}");

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
                        descriptionText.AppendLine($"\n\n{recording.EITExtendedDescription}");
                    }
                }

                txtLocalDescription.Text = descriptionText.ToString();
                btnDownloadLocalRecording.Enabled = true;
            }
            else
            {
                txtLocalDescription.Clear();
                btnDownloadLocalRecording.Enabled = false;
            }
        }

        private void MenuItemLocalView_Click(object? sender, EventArgs e)
        {
            if (listViewLocalRecordings.SelectedItems.Count > 0)
            {
                var recording = (Recording)listViewLocalRecordings.SelectedItems[0].Tag!;

                var message = new StringBuilder();
                message.AppendLine($"Title: {recording.Title}");
                message.AppendLine($"Channel: {recording.ServiceName}");
                message.AppendLine($"Date: {recording.RecordingDate:yyyy-MM-dd HH:mm}");
                message.AppendLine($"Duration: {recording.FormattedDuration}");
                message.AppendLine($"Size: {recording.FormattedSize}");
                message.AppendLine($"File: {recording.FileName}");
                message.AppendLine($"Source: Local File");
                message.AppendLine($"Path: {recording.LocalFilePath}");

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
                        message.AppendLine($"\n\n{recording.EITExtendedDescription}");
                    }
                }

                MessageBox.Show(message.ToString(), "Recording Details", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnSelectAllLocalRecordings_Click(object? sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewLocalRecordings.Items)
            {
                item.Checked = true;
            }
        }

        #endregion

        #region File Conversion Tab

        private void BtnSettings_Click(object? sender, EventArgs e)
        {
            var dialog = new SettingsDialog(appSettings);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                // Settings have been saved, no need to do anything
                MessageBox.Show("Settings saved successfully!", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnBrowseFolder_Click(object? sender, EventArgs e)
        {
            using var folderDialog = new FolderBrowserDialog
            {
                Description = "Select folder containing .TS files",
                UseDescriptionForTitle = true,
                ShowNewFolderButton = false
            };

            if (!string.IsNullOrEmpty(txtConversionFolder.Text) && Directory.Exists(txtConversionFolder.Text))
            {
                folderDialog.SelectedPath = txtConversionFolder.Text;
            }

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                txtConversionFolder.Text = folderDialog.SelectedPath;
                LoadLocalTSFiles();
            }
        }

        private void BtnRefreshFiles_Click(object? sender, EventArgs e)
        {
            LoadLocalTSFiles();
        }

        private void LoadLocalTSFiles()
        {
            listViewLocalFiles.Items.Clear();

            if (string.IsNullOrEmpty(txtConversionFolder.Text) || !Directory.Exists(txtConversionFolder.Text))
            {
                toolStripStatusLabel.Text = "Please select a valid folder";
                return;
            }

            try
            {
                var tsFiles = Directory.GetFiles(txtConversionFolder.Text, "*.ts", SearchOption.TopDirectoryOnly);

                foreach (var file in tsFiles)
                {
                    var fileInfo = new FileInfo(file);
                    var item = new ListViewItem(fileInfo.Name);
                    item.SubItems.Add(FormatFileSize(fileInfo.Length));
                    item.SubItems.Add(fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));

                    // Check if MP4 already exists
                    var mp4Path = Path.ChangeExtension(file, ".mp4");
                    if (File.Exists(mp4Path))
                    {
                        item.SubItems.Add("Already converted");
                        item.ForeColor = Color.Gray;
                    }
                    else
                    {
                        item.SubItems.Add("Ready to convert");
                    }

                    item.Tag = file;
                    listViewLocalFiles.Items.Add(item);
                }

                toolStripStatusLabel.Text = $"Found {tsFiles.Length} .TS file(s)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading files:\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "Error loading files";
            }
        }

        private string FormatFileSize(long bytes)
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

        private void BtnSelectAll_Click(object? sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewLocalFiles.Items)
            {
                item.Checked = true;
            }
        }

        private void BtnSelectNone_Click(object? sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewLocalFiles.Items)
            {
                item.Checked = false;
            }
        }

        private async void BtnConvertSelected_Click(object? sender, EventArgs e)
        {
            var selectedFiles = listViewLocalFiles.Items.Cast<ListViewItem>()
                .Where(item => item.Checked)
                .Select(item => item.Tag as string)
                .Where(path => !string.IsNullOrEmpty(path))
                .Cast<string>()
                .ToList();

            if (selectedFiles.Count == 0)
            {
                MessageBox.Show("Please select at least one file to convert.", "No Files Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Test FFmpeg first
            if (!TestFFmpeg())
            {
                var result = MessageBox.Show(
                    "FFmpeg is not properly configured. Would you like to configure it now?",
                    "FFmpeg Not Found",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    BtnSettings_Click(sender, e);
                }
                return;
            }

            await ConvertLocalFiles(selectedFiles);
        }

        private bool TestFFmpeg()
        {
            try
            {
                var processInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = appSettings.FFmpegPath,
                    Arguments = "-version",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                using var process = System.Diagnostics.Process.Start(processInfo);
                if (process != null)
                {
                    process.WaitForExit();
                    return process.ExitCode == 0;
                }
            }
            catch
            {
                // FFmpeg not found
            }

            return false;
        }

        private async Task ConvertLocalFiles(List<string> filePaths)
        {
            var progressDialog = new DownloadProgressDialog();
            progressDialog.Show(this);
            progressDialog.SwitchToConversionTab();

            btnConvertSelected.Enabled = false;
            btnBrowseFolder.Enabled = false;
            btnRefreshFiles.Enabled = false;

            int converted = 0;
            int failed = 0;

            try
            {
                progressDialog.UpdateConversionStatus($"Starting conversion of {filePaths.Count} file(s)...");
                progressDialog.UpdateConversionProgress(0, filePaths.Count);

                for (int i = 0; i < filePaths.Count; i++)
                {
                    var tsPath = filePaths[i];
                    var fileName = Path.GetFileName(tsPath);

                    progressDialog.UpdateConversionFile(fileName);
                    progressDialog.UpdateConversionStatus($"Converting {i + 1} of {filePaths.Count}...");
                    progressDialog.UpdateConversionProgress(i, filePaths.Count);

                    // Update status in list view
                    UpdateFileStatus(tsPath, "Converting...");

                    try
                    {
                        var mp4Path = await ConvertToMp4(tsPath, progressDialog);
                        if (!string.IsNullOrEmpty(mp4Path))
                        {
                            converted++;
                            UpdateFileStatus(tsPath, "Converted successfully");
                            System.Diagnostics.Debug.WriteLine($"✓ Converted: {Path.GetFileName(mp4Path)}");
                        }
                        else
                        {
                            failed++;
                            UpdateFileStatus(tsPath, "Conversion failed");
                            System.Diagnostics.Debug.WriteLine($"✗ Conversion failed: {fileName}");
                        }
                    }
                    catch (Exception ex)
                    {
                        failed++;
                        UpdateFileStatus(tsPath, $"Error: {ex.Message}");
                        System.Diagnostics.Debug.WriteLine($"✗ Conversion error for {fileName}: {ex.Message}");
                    }

                    progressDialog.UpdateConversionProgress(i + 1, filePaths.Count);
                }

                var summary = $"Conversion complete!\n\nSuccessful: {converted}\nFailed: {failed}";
                progressDialog.UpdateConversionStatus(summary);
                progressDialog.SetComplete(failed == 0, summary);

                System.Diagnostics.Debug.WriteLine($"=== Conversion Summary ===");
                System.Diagnostics.Debug.WriteLine($"Converted: {converted}, Failed: {failed}");
            }
            finally
            {
                btnConvertSelected.Enabled = true;
                btnBrowseFolder.Enabled = true;
                btnRefreshFiles.Enabled = true;
                LoadLocalTSFiles(); // Refresh the list
            }
        }

        private void UpdateFileStatus(string filePath, string status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateFileStatus(filePath, status)));
                return;
            }

            foreach (ListViewItem item in listViewLocalFiles.Items)
            {
                if (item.Tag as string == filePath)
                {
                    item.SubItems[3].Text = status;

                    if (status.Contains("success", StringComparison.OrdinalIgnoreCase))
                    {
                        item.ForeColor = Color.Green;
                    }
                    else if (status.Contains("fail", StringComparison.OrdinalIgnoreCase) || status.Contains("error", StringComparison.OrdinalIgnoreCase))
                    {
                        item.ForeColor = Color.Red;
                    }
                    else if (status.Contains("Converting", StringComparison.OrdinalIgnoreCase))
                    {
                        item.ForeColor = Color.Blue;
                    }

                    break;
                }
            }
        }

        private async void BtnOrganizeMp4Files_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtConversionFolder.Text) || !Directory.Exists(txtConversionFolder.Text))
            {
                MessageBox.Show("Please select a valid folder first.", "No Folder Selected", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var folderPath = txtConversionFolder.Text;
                var mp4Files = Directory.GetFiles(folderPath, "*.mp4", SearchOption.TopDirectoryOnly);

                if (mp4Files.Length == 0)
                {
                    MessageBox.Show("No .MP4 files found in the selected folder.", "No Files", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var result = MessageBox.Show(
                    $"This will organize {mp4Files.Length} MP4 file(s) into folders by series name.\n\n" +
                    "Files will be moved into subfolders named after their series.\n\n" +
                    "Do you want to continue?",
                    "Organize MP4 Files",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                btnOrganizeMp4Files.Enabled = false;
                btnConvertSelected.Enabled = false;
                btnBrowseFolder.Enabled = false;
                btnRefreshFiles.Enabled = false;

                var statusDialog = new StatusDialog();
                statusDialog.Show(this);
                statusDialog.UpdateStatus("Organizing MP4 files...");
                statusDialog.UpdateProgress(0, mp4Files.Length);

                int organized = 0;
                int failed = 0;

                await Task.Run(() =>
                {
                    for (int i = 0; i < mp4Files.Length; i++)
                    {
                        try
                        {
                            var filePath = mp4Files[i];
                            var fileName = Path.GetFileName(filePath);
                            var fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);

                            // Extract series name from filename
                            var seriesName = ExtractSeriesName(fileNameWithoutExt);

                            if (string.IsNullOrEmpty(seriesName))
                            {
                                seriesName = "Unknown Series";
                            }

                            // Sanitize folder name
                            seriesName = SanitizeFolderName(seriesName);

                            // Create series folder
                            var seriesFolderPath = Path.Combine(folderPath, seriesName);
                            if (!Directory.Exists(seriesFolderPath))
                            {
                                Directory.CreateDirectory(seriesFolderPath);
                            }

                            // Move file
                            var destPath = Path.Combine(seriesFolderPath, fileName);

                            // Handle duplicate filenames
                            if (File.Exists(destPath))
                            {
                                var counter = 1;
                                var baseFileName = Path.GetFileNameWithoutExtension(fileName);
                                var extension = Path.GetExtension(fileName);

                                while (File.Exists(destPath))
                                {
                                    destPath = Path.Combine(seriesFolderPath, $"{baseFileName}_{counter}{extension}");
                                    counter++;
                                }
                            }

                            File.Move(filePath, destPath);
                            organized++;

                            Invoke(new Action(() =>
                            {
                                statusDialog.UpdateStatus($"Organizing file {i + 1} of {mp4Files.Length}...");
                                statusDialog.UpdateProgress(i + 1, mp4Files.Length);
                            }));
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Error organizing file: {ex.Message}");
                            failed++;
                        }
                    }
                });

                var message = $"Organization complete!\n\n" +
                              $"Organized: {organized}\n" +
                              $"Failed: {failed}";

                statusDialog.UpdateStatus(message);
                statusDialog.SetComplete(failed == 0);

                MessageBox.Show(message, "Organize Complete", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh the file list
                LoadLocalTSFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error organizing files:\n\n{ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnOrganizeMp4Files.Enabled = true;
                btnConvertSelected.Enabled = true;
                btnBrowseFolder.Enabled = true;
                btnRefreshFiles.Enabled = true;
            }
        }

        private string ExtractSeriesName(string filename)
        {
            // Remove common patterns to extract series name
            // Pattern 1: "SeriesName - Episode Title" or "SeriesName - Description"
            var dashIndex = filename.IndexOf(" - ");
            if (dashIndex > 0)
            {
                var seriesName = filename.Substring(0, dashIndex).Trim();

                // Remove date patterns from start (YYYYMMDD, YYYY-MM-DD, etc.)
                seriesName = Regex.Replace(seriesName, @"^\d{8}\s*-?\s*", "").Trim();
                seriesName = Regex.Replace(seriesName, @"^\d{4}-\d{2}-\d{2}\s*-?\s*", "").Trim();

                return seriesName;
            }

            // Pattern 2: Remove date patterns anywhere in filename
            var cleaned = Regex.Replace(filename, @"\d{8}", "").Trim();
            cleaned = Regex.Replace(cleaned, @"\d{4}-\d{2}-\d{2}", "").Trim();
            cleaned = Regex.Replace(cleaned, @"[-_]+", " ").Trim();

            // If we have something reasonable, return it
            if (!string.IsNullOrWhiteSpace(cleaned) && cleaned.Length > 3)
            {
                return cleaned;
            }

            // Fallback: use the whole filename
            return filename;
        }

        private string SanitizeFolderName(string folderName)
        {
            // Remove invalid folder name characters
            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char c in invalidChars)
            {
                folderName = folderName.Replace(c, '_');
            }

            // Replace additional problematic characters
            folderName = folderName.Replace(':', '-');
            folderName = folderName.Replace('/', '-');
            folderName = folderName.Replace('\\', '-');
            folderName = folderName.Replace('?', ' ');
            folderName = folderName.Replace('*', ' ');
            folderName = folderName.Replace('"', '\'');
            folderName = folderName.Replace('<', '(');
            folderName = folderName.Replace('>', ')');
            folderName = folderName.Replace('|', '-');

            // Trim and remove multiple spaces
            folderName = Regex.Replace(folderName.Trim(), @"\s+", " ");

            // Limit length
            const int maxLength = 100;
            if (folderName.Length > maxLength)
            {
                folderName = folderName.Substring(0, maxLength);
            }

            return folderName;
        }

        #endregion
    }
}
