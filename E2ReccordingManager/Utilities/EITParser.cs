using System.Text;

namespace E2ReccordingManager.Utilities
{
    /// <summary>
    /// Parser for Enigma2 EIT (Event Information Table) files
    /// EIT files contain EPG metadata for recordings
    /// </summary>
    public class EITParser
    {
        public class EITData
        {
            public string Title { get; set; } = string.Empty;
            public string ShortDescription { get; set; } = string.Empty;
            public string ExtendedDescription { get; set; } = string.Empty;
            public DateTime StartTime { get; set; }
            public int DurationSeconds { get; set; }
        }

        /// <summary>
        /// Parse EIT file content and extract EPG metadata
        /// </summary>
        public static EITData? ParseEIT(byte[] eitData)
        {
            if (eitData == null || eitData.Length < 12)
            {
                System.Diagnostics.Debug.WriteLine("EIT data is null or too short");
                return null;
            }

            try
            {
                int offset = 0;
                
                // Skip to event data (usually starts after table header)
                // Look for the short event descriptor (0x4D) or extended event descriptor (0x4E)
                while (offset < eitData.Length - 10)
                {
                    // Check for descriptors
                    byte descriptorTag = eitData[offset];
                    
                    if (descriptorTag == 0x4D) // Short Event Descriptor
                    {
                        return ParseShortEventDescriptor(eitData, offset);
                    }
                    
                    offset++;
                }
                
                System.Diagnostics.Debug.WriteLine("No event descriptors found in EIT data");
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error parsing EIT: {ex.Message}");
                return null;
            }
        }

        private static EITData? ParseShortEventDescriptor(byte[] data, int startOffset)
        {
            try
            {
                int offset = startOffset;
                
                // Skip descriptor tag (0x4D)
                offset++;
                
                if (offset >= data.Length)
                    return null;
                
                // Read descriptor length
                int descriptorLength = data[offset++];
                
                if (offset + descriptorLength > data.Length)
                    return null;
                
                // Skip language code (3 bytes)
                offset += 3;
                
                if (offset >= data.Length)
                    return null;
                
                // Read event name length
                int eventNameLength = data[offset++];
                
                if (offset + eventNameLength > data.Length)
                    return null;
                
                // Read event name (title)
                string title = Encoding.UTF8.GetString(data, offset, eventNameLength);
                offset += eventNameLength;
                
                if (offset >= data.Length)
                {
                    return new EITData { Title = title };
                }
                
                // Read short description length
                int shortDescLength = data[offset++];
                
                if (offset + shortDescLength > data.Length)
                {
                    return new EITData { Title = title };
                }
                
                // Read short description
                string shortDescription = Encoding.UTF8.GetString(data, offset, shortDescLength);
                offset += shortDescLength;
                
                var eitData = new EITData
                {
                    Title = CleanString(title),
                    ShortDescription = CleanString(shortDescription)
                };
                
                // Look for extended event descriptor (0x4E) after this
                while (offset < data.Length - 5)
                {
                    if (data[offset] == 0x4E)
                    {
                        var extended = ParseExtendedEventDescriptor(data, offset);
                        if (!string.IsNullOrEmpty(extended))
                        {
                            eitData.ExtendedDescription = extended;
                        }
                        break;
                    }
                    offset++;
                }
                
                return eitData;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error parsing short event descriptor: {ex.Message}");
                return null;
            }
        }

        private static string ParseExtendedEventDescriptor(byte[] data, int startOffset)
        {
            try
            {
                int offset = startOffset;
                
                // Skip descriptor tag (0x4E)
                offset++;
                
                if (offset >= data.Length)
                    return string.Empty;
                
                // Read descriptor length
                int descriptorLength = data[offset++];
                
                if (offset + descriptorLength > data.Length)
                    return string.Empty;
                
                // Skip descriptor number, last descriptor number (1 byte)
                offset++;
                
                // Skip language code (3 bytes)
                offset += 3;
                
                if (offset >= data.Length)
                    return string.Empty;
                
                // Read length of items
                int itemsLength = data[offset++];
                
                // Skip items
                offset += itemsLength;
                
                if (offset >= data.Length)
                    return string.Empty;
                
                // Read text length
                int textLength = data[offset++];
                
                if (offset + textLength > data.Length)
                    return string.Empty;
                
                // Read extended description text
                string extendedText = Encoding.UTF8.GetString(data, offset, textLength);
                
                return CleanString(extendedText);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error parsing extended event descriptor: {ex.Message}");
                return string.Empty;
            }
        }

        private static string CleanString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            
            // Remove control characters and clean up the string
            var cleaned = new StringBuilder();
            foreach (char c in input)
            {
                if (c >= 32 && c != 127) // Printable characters only
                {
                    cleaned.Append(c);
                }
                else if (c == '\n' || c == '\r')
                {
                    cleaned.Append(c);
                }
            }
            
            return cleaned.ToString().Trim();
        }
    }
}
