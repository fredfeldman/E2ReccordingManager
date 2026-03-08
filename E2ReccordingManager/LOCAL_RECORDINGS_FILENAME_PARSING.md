# Local Recordings - Filename Parsing Feature

## Overview

The Local Recordings tab now intelligently parses filenames to extract metadata like title, channel/service name, description, and recording date. This provides much better organization and readability when viewing local recording files.

## Supported Filename Patterns

### Pattern 1: Date-Service-Title
```
Format: YYYYMMDD HHMM - ServiceName - Title.ts
Example: 20240115 2030 - BBC One - Doctor Who.ts

Extracted:
- Date: 2024-01-15 20:30
- Service: BBC One
- Title: Doctor Who
```

### Pattern 2: Service-Title-Description
```
Format: ServiceName - Title - Description.ts
Example: HBO - Game of Thrones - Season 8 Episode 1.ts

Extracted:
- Service: HBO
- Title: Game of Thrones
- Description: Season 8 Episode 1
```

### Pattern 3: Date-ServiceOrTitle-Details
```
Format: YYYYMMDD - ServiceName - Title.ts
Example: 20240115 - CNN - Breaking News.ts

Extracted:
- Date: 2024-01-15
- Service: CNN
- Title: Breaking News
```

### Pattern 4: Service-Title (Two Parts)
```
Format: ServiceName - Title.ts
Example: Discovery - Planet Earth.ts

Extracted:
- Service: Discovery
- Title: Planet Earth
```

### Pattern 5: Title-Description (Two Parts)
```
Format: Title - Description.ts
Example: Movie Night - The Matrix.ts

Extracted:
- Title: Movie Night
- Description: The Matrix
- Service: Local (default)
```

### Pattern 6: Embedded Date
```
Format: Any filename with YYYYMMDD or YYYY-MM-DD
Example: Recording_2024-01-15_ShowName.ts

Extracted:
- Date: 2024-01-15
- Title: Recording ShowName (date removed)
- Service: Local (default)
```

### Pattern 7: Simple Filename
```
Format: Title.ts
Example: MyRecording.ts

Extracted:
- Title: MyRecording
- Service: Local (default)
```

## How It Works

### Parsing Process

1. **Check for date-service-title pattern** (most specific)
   - Looks for: `YYYYMMDD HHMM - Service - Title`
   - Extracts all three components

2. **Check for three-part pattern**
   - Looks for: `Part1 - Part2 - Part3`
   - Intelligently determines which is service, title, description
   - Detects if Part1 is a date

3. **Check for two-part pattern**
   - Looks for: `Part1 - Part2`
   - Determines if Part1 is:
     - A date (use Part2 as title)
     - A service name (short, < 30 chars)
     - A title (Part2 is description)

4. **Check for embedded date**
   - Looks for `YYYYMMDD` or `YYYY-MM-DD` anywhere
   - Extracts date and removes from title

5. **Clean up result**
   - Replace underscores/dashes with spaces
   - Remove extra whitespace
   - Trim to reasonable length

6. **Fallback to original**
   - If all parsing fails, use original filename

### Special Handling

**Date Detection:**
- Formats supported: `YYYYMMDD`, `YYYY-MM-DD`, `YYYYMMDD HHMM`
- Invalid dates ignored
- Falls back to file modification date

**Service Name Detection:**
- Short strings (<30 chars) in first position likely service names
- Common patterns recognized (BBC, HBO, CNN, etc.)
- Defaults to "Local" if not detected

**Title Cleaning:**
- Converts `_` and `-` to spaces
- Removes multiple spaces
- Trims whitespace
- Limits length to 200 characters

## Before vs After

### Before (No Parsing)
```
ListView Display:
┌────────────────────────────────────────────────────┐
│ Title                          | Channel | Date    │
├────────────────────────────────────────────────────┤
│ 20240115_2030_BBC_One_Doctor_Who | Local | 1/15/24 │
│ HBO-Game_of_Thrones-S08E01       | Local | 1/14/24 │
│ Recording_2024-01-13_Movie       | Local | 1/13/24 │
└────────────────────────────────────────────────────┘
```

### After (With Parsing)
```
ListView Display:
┌────────────────────────────────────────────────────┐
│ Title           | Channel  | Date             │
├────────────────────────────────────────────────────┤
│ Doctor Who      | BBC One  | 2024-01-15 20:30 │
│ Game of Thrones | HBO      | 1/14/24          │
│ Recording Movie | Local    | 2024-01-13       │
└────────────────────────────────────────────────────┘
```

## Examples

### Example 1: Enigma2 Default Format
```
Filename: 20240115 2030 - BBC One HD - Doctor Who.ts

Parsed Result:
- Title: "Doctor Who"
- Channel: "BBC One HD"  
- Date: 2024-01-15 20:30:00
- Description: ""

Display:
Doctor Who | BBC One HD | 2024-01-15 20:30
```

### Example 2: Manual Naming
```
Filename: HBO - Succession - The Final Season.ts

Parsed Result:
- Title: "Succession"
- Channel: "HBO"
- Date: (file modification date)
- Description: "The Final Season"

Display:
Succession | HBO | (file date)
```

### Example 3: Simple Name
```
Filename: Family_Movie_Night.ts

Parsed Result:
- Title: "Family Movie Night"
- Channel: "Local"
- Date: (file modification date)
- Description: ""

Display:
Family Movie Night | Local | (file date)
```

### Example 4: Date in Filename
```
Filename: Recording_2024-01-15_Sports_Game.ts

Parsed Result:
- Title: "Recording Sports Game"
- Channel: "Local"
- Date: 2024-01-15
- Description: ""

Display:
Recording Sports Game | Local | 2024-01-15
```

## Benefits

### 1. **Better Readability**
- Clean titles instead of raw filenames
- Proper service/channel names displayed
- Dates extracted and formatted

### 2. **Better Organization**
- Sort by actual title, not filename
- Filter by channel/service
- Group by date

### 3. **Consistent Display**
- Local recordings look like device recordings
- Same column structure
- Professional appearance

### 4. **Smart Fallback**
- If parsing fails, shows original filename
- No data loss
- Graceful degradation

### 5. **Works with Any Naming**
- Handles various formats
- Adapts to your naming convention
- Doesn't require specific format

## Technical Details

### Method Signature
```csharp
private (string Title, string ServiceName, string Description, DateTime? Date) 
    ParseRecordingFilename(string filename)
```

### Return Values
- **Title**: Extracted or cleaned title
- **ServiceName**: Detected service/channel or "Local"
- **Description**: Additional info from filename
- **Date**: Parsed date or null (falls back to file date)

### Regex Patterns Used
```csharp
// Date-Service-Title
@"^(\d{8})\s+(\d{4})\s*-\s*(.+?)\s*-\s*(.+)$"

// Three parts
@"^(.+?)\s*-\s*(.+?)\s*-\s*(.+)$"

// Two parts
@"^(.+?)\s*-\s*(.+)$"

// Embedded date
@"(\d{4})-?(\d{2})-?(\d{2})"
```

### Error Handling
- Try/catch around all parsing
- Falls back to original filename on error
- Logs errors to debug output
- Never throws exceptions

## Use Cases

### Use Case 1: Downloaded from Device
```
Original device filename format preserved:
"20240115 2030 - BBC One - Doctor Who.ts"

Parsing extracts all metadata properly.
```

### Use Case 2: Manually Renamed Files
```
User renamed to: "HBO - Last of Us - Episode 1.ts"

Parsing extracts service and title.
```

### Use Case 3: Random Naming
```
User named: "my_recording_jan_15.ts"

Parsing cleans up underscores, extracts date if possible.
```

### Use Case 4: Already Well-Named
```
Filename: "Planet Earth Documentary.ts"

Parsing leaves it mostly as-is, just cleans spacing.
```

## Configuration

### No Configuration Needed
- Automatic parsing
- Always enabled
- No settings to adjust

### Customization (Future)
Could add settings for:
- Preferred date format
- Default service name
- Custom parsing rules
- Disable parsing (use raw filename)

## Limitations

### Current Limitations

1. **No 100% Accuracy**
   - Ambiguous filenames may parse incorrectly
   - Heuristics used for detection
   - Complex names may confuse parser

2. **Limited Date Formats**
   - Only supports: YYYYMMDD, YYYY-MM-DD, YYYYMMDD HHMM
   - Other formats use file modification date

3. **Service Name Guessing**
   - Based on position and length
   - May misidentify long service names
   - Always safe: defaults to "Local"

4. **No Learning**
   - Doesn't adapt to user's naming convention
   - Same rules for all files
   - No pattern detection across files

### Workaround for Issues

If parsing gives wrong results:
1. **Rename file** to supported format
2. **Use EIT files** (overrides parsed data)
3. **Check file format** (maybe needs separator)

## Comparison with EIT Data

### Parsing vs EIT

| Aspect | Filename Parsing | EIT File |
|--------|-----------------|----------|
| **Source** | Filename string | Separate .eit file |
| **Accuracy** | Heuristic/guess | Official EPG data |
| **Availability** | Always (from filename) | Optional (.eit must exist) |
| **Priority** | Lower | Higher |
| **Data** | Basic (title, service, date) | Complete (description, times, etc.) |

### Priority Order

When loading local recordings:
1. **Parse filename** → Get basic info
2. **Load EIT file** → Override with official data
3. **Final result** → Best available data

## Tips for Best Results

### Tip 1: Use Standard Format
```
Recommended: YYYYMMDD HHMM - ServiceName - Title.ts
Example: 20240115 2030 - BBC One - Doctor Who.ts
Result: Perfect parsing, all fields extracted
```

### Tip 2: Use Separators
```
Good: ServiceName - Title - Description.ts
Bad: ServiceNameTitleDescription.ts
Result: Separators help parser identify parts
```

### Tip 3: Keep Service Names Short
```
Good: HBO - Show Title.ts
Bad: Home Box Office Premium Channel - Show.ts
Result: Short names detected as service, long as title
```

### Tip 4: Include Dates
```
Good: 20240115 - Show Title.ts
Better: 20240115 2030 - Channel - Show.ts
Result: Date extracted and displayed properly
```

### Tip 5: Avoid Complex Names
```
Avoid: Show - S01E01 - Title - Channel - Date.ts
Better: Channel - Show - S01E01 Title.ts
Result: Simpler = better parsing
```

## Debug Output

### Enable Debug Logging
When parsing fails or gives unexpected results:

```csharp
System.Diagnostics.Debug.WriteLine($"Error parsing filename '{filename}': {ex.Message}");
```

Check Visual Studio Output window for details.

### What Gets Logged
- Parsing errors
- Fallback to original filename
- Failed date parsing
- Unexpected patterns

## Future Enhancements

Possible improvements:
1. **Custom patterns** - User-defined regex patterns
2. **Learning mode** - Detect user's naming convention
3. **More date formats** - Support DD-MM-YYYY, etc.
4. **Language support** - Non-English date formats
5. **Batch rename** - Standardize naming
6. **Pattern library** - Common recording software formats

## Summary

### What It Does
✅ Parses local recording filenames
✅ Extracts title, service, description, date
✅ Cleans up formatting
✅ Falls back gracefully on errors

### Benefits
✅ Better display of local recordings
✅ Easier to find files
✅ Professional appearance
✅ Consistent with device recordings

### Automatic
✅ No configuration needed
✅ Always enabled
✅ Works with any naming convention
✅ Safe fallback behavior

The filename parsing makes local recordings look and behave just like device recordings, providing a consistent and professional user experience!
