namespace E2ReccordingManager.Models
{
    public class Recording
    {
        public string Title { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string ServiceReference { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DescriptionExtended { get; set; } = string.Empty;
        public DateTime RecordingDate { get; set; }
        public int DurationSeconds { get; set; }
        public string FileName { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }
        
        public string EITTitle { get; set; } = string.Empty;
        public string EITShortDescription { get; set; } = string.Empty;
        public string EITExtendedDescription { get; set; } = string.Empty;
        public bool HasEITData { get; set; }
        public DateTime EITStartTime { get; set; }
        public int EITDuration { get; set; }
        
        public string FormattedSize
        {
            get
            {
                if (FileSizeBytes < 1024)
                    return $"{FileSizeBytes} B";
                else if (FileSizeBytes < 1024 * 1024)
                    return $"{FileSizeBytes / 1024.0:F2} KB";
                else if (FileSizeBytes < 1024 * 1024 * 1024)
                    return $"{FileSizeBytes / (1024.0 * 1024.0):F2} MB";
                else
                    return $"{FileSizeBytes / (1024.0 * 1024.0 * 1024.0):F2} GB";
            }
        }

        public string FormattedDuration
        {
            get
            {
                var duration = DurationSeconds > 0 ? DurationSeconds : EITDuration;
                var ts = TimeSpan.FromSeconds(duration);
                if (ts.TotalHours >= 1)
                    return $"{(int)ts.TotalHours}h {ts.Minutes}m";
                else
                    return $"{ts.Minutes}m {ts.Seconds}s";
            }
        }
    }
}
