namespace E2ReccordingManager.Models
{
    public class ConnectionProfile
    {
        public string Name { get; set; } = "Default";
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 80;
        public string Username { get; set; } = "root";
        public string Password { get; set; } = string.Empty;
    }
}
