using System.Text.Json;

namespace E2ReccordingManager.Models
{
    public class AppSettings
    {
        public string FFmpegPath { get; set; } = "ffmpeg";
        public bool AutoConvertToMp4 { get; set; } = false;
        public bool DeleteTsAfterConversion { get; set; } = false;
        public string ConversionQuality { get; set; } = "Balanced";
        public int MaxBitrateMbps { get; set; } = 8;
        public bool UseHardwareAcceleration { get; set; } = true;
        public string LocalFolderPath { get; set; } = string.Empty;
        public bool IncludeLocalFiles { get; set; } = false;

        private static readonly string SettingsFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "E2ReccordingManager",
            "settings.json"
        );

        public static AppSettings Load()
        {
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    var json = File.ReadAllText(SettingsFilePath);
                    var settings = JsonSerializer.Deserialize<AppSettings>(json);
                    return settings ?? new AppSettings();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading settings: {ex.Message}");
            }

            return new AppSettings();
        }

        public void Save()
        {
            try
            {
                var directory = Path.GetDirectoryName(SettingsFilePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var json = JsonSerializer.Serialize(this, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(SettingsFilePath, json);
                System.Diagnostics.Debug.WriteLine($"Settings saved to: {SettingsFilePath}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving settings: {ex.Message}");
                throw;
            }
        }

        public int GetQualityCQ()
        {
            return ConversionQuality switch
            {
                "High" => 18,
                "Balanced" => 23,
                "Low" => 28,
                _ => 23
            };
        }

        public string GetPresetName()
        {
            return ConversionQuality switch
            {
                "High" => "p6",
                "Balanced" => "p4",
                "Low" => "p2",
                _ => "p4"
            };
        }
    }
}
