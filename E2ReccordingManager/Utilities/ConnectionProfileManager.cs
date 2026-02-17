using E2ReccordingManager.Models;
using System.Text.Json;

namespace E2ReccordingManager.Utilities
{
    public class ConnectionProfileManager
    {
        private readonly string settingsPath;
        private readonly string lastProfilePath;
        private List<ConnectionProfile> profiles;

        public ConnectionProfileManager()
        {
            var appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "E2RecordingManager");
            
            Directory.CreateDirectory(appDataPath);
            settingsPath = Path.Combine(appDataPath, "connections.json");
            lastProfilePath = Path.Combine(appDataPath, "lastprofile.txt");
            LoadProfiles();
        }

        private void LoadProfiles()
        {
            try
            {
                if (File.Exists(settingsPath))
                {
                    var json = File.ReadAllText(settingsPath);
                    profiles = JsonSerializer.Deserialize<List<ConnectionProfile>>(json) ?? new List<ConnectionProfile>();
                }
                else
                {
                    profiles = new List<ConnectionProfile>
                    {
                        new ConnectionProfile { Name = "Default" }
                    };
                    SaveProfiles();
                }
            }
            catch
            {
                profiles = new List<ConnectionProfile>
                {
                    new ConnectionProfile { Name = "Default" }
                };
            }
        }

        private void SaveProfiles()
        {
            try
            {
                var json = JsonSerializer.Serialize(profiles, new JsonSerializerOptions 
                { 
                    WriteIndented = true 
                });
                File.WriteAllText(settingsPath, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving profiles: {ex.Message}");
            }
        }

        public List<ConnectionProfile> GetAllProfiles()
        {
            return profiles.ToList();
        }

        public ConnectionProfile? GetProfile(string name)
        {
            return profiles.FirstOrDefault(p => p.Name == name);
        }

        public void SaveProfile(ConnectionProfile profile)
        {
            var existing = profiles.FirstOrDefault(p => p.Name == profile.Name);
            if (existing != null)
            {
                existing.Host = profile.Host;
                existing.Port = profile.Port;
                existing.Username = profile.Username;
                existing.Password = profile.Password;
            }
            else
            {
                profiles.Add(profile);
            }
            SaveProfiles();
        }

        public void DeleteProfile(string name)
        {
            profiles.RemoveAll(p => p.Name == name);
            SaveProfiles();
        }

        public string? GetLastUsedProfile()
        {
            try
            {
                if (File.Exists(lastProfilePath))
                {
                    return File.ReadAllText(lastProfilePath).Trim();
                }
            }
            catch
            {
                // Ignore errors
            }
            return null;
        }

        public void SaveLastUsedProfile(string profileName)
        {
            try
            {
                File.WriteAllText(lastProfilePath, profileName);
            }
            catch
            {
                // Ignore errors
            }
        }
    }
}
