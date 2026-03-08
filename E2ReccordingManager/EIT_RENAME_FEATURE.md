# EIT-Based File Renaming Feature

## Overview
Added a checkbox option to rename downloaded recording files based on EIT (Event Information Table) file data, matching the format used in the Enigma2Manager reference project.

## Changes Made

### 1. User Interface (Form1.Designer.cs)
- **Added new checkbox control**: `checkBoxRenameFromEIT`
  - Label: "Rename using EIT file data"
  - Position: Next to the "Remove after download" checkbox
  - TabIndex: 6

### 2. Download Logic (Form1.cs)

#### Modified `BatchDownloadRecordings` Method
- Added logic to check if the `checkBoxRenameFromEIT` checkbox is checked
- If checked and EIT data is available, generates a new filename based on EIT metadata
- Falls back to original filename if:
  - Checkbox is not checked
  - Recording doesn't have EIT data
  - EIT title is empty

#### Added `GenerateEITBasedFilename` Method
Creates a clean, descriptive filename from EIT data using the format from the reference project:

**Filename Format:**
```
[Series Name] - [Description].[Extension]
```

**Series Name Selection Priority:**
1. EIT Title (from .eit file metadata)
2. Recording Title (from device recording list)
3. Original filename (as last resort)

**Description Selection Priority:**
1. EIT Extended Description
2. EIT Short Description
3. Recording Extended Description
4. Recording Description
5. None (just series name if no description available)

**Features:**
- Sanitizes title by removing invalid filesystem characters
- Replaces problematic characters with safe alternatives:
  - `:` → `-`
  - `/`, `\` → `-`
  - `?`, `*` → ` ` (space)
  - `"` → `'`
  - `<`, `>` → `(`, `)`
  - `|` → `-`
- Removes multiple consecutive spaces
- Preserves original file extension (.ts, .mkv, etc.)
- Limits filename length to 200 characters to avoid filesystem issues
- Does NOT include date/time in filename (matches reference project format)

#### Added `SanitizeFileName` Method
Helper method that cleans filenames by:
- Removing all invalid filesystem characters
- Replacing common problematic characters
- Normalizing whitespace

**Examples:**
```
Original: 20231215_1830_1-0-1-1234_ABCD_0_0_0.ts
EIT Title: "Top Gear"
EIT Description: "Season 5 Episode 3"
Renamed: Top Gear - Season 5 Episode 3.ts

Original: 20240110_2100_1-0-19-1234_ABCD_0_0_0.ts
EIT Title: "The News"
No Description
Renamed: The News.ts

Original: recording_20240115.ts
No EIT Data
Title: "Documentary Special"
Description: "Wildlife in Africa"
Renamed: Documentary Special - Wildlife in Africa.ts
```

## How to Use

1. **Connect to Enigma2 Device**: Click "Connect" and enter device credentials
2. **Refresh Recordings**: Click "Refresh" to load the recording list (EIT data is loaded automatically)
3. **Select Recording(s)**: Choose one or more recordings to download
4. **Enable EIT Renaming**: Check the "Rename using EIT file data" checkbox
5. **Download**: Click "Download" button
6. **Choose Destination**: Select the folder where files should be saved

The files will be downloaded with descriptive names based on the EPG metadata instead of the cryptic original filenames.

## Technical Details

### EIT Data Requirements
The feature works best when:
1. EIT file exists on the device (same name as recording with .eit extension)
2. EIT file was successfully parsed during the "Refresh" operation
3. `Recording.HasEITData` is `true`
4. `Recording.EITTitle` is not empty

### Fallback Behavior
The naming logic gracefully falls back through multiple sources:
- If EIT title is unavailable → uses Recording.Title
- If both unavailable → uses original filename
- If no description available → uses only the series name
- If checkbox is unchecked → always uses original filename

### File Overwrite Protection
The download process uses `FileMode.Create`, which will overwrite existing files. Users should be careful when downloading to folders with existing recordings.

## Benefits

1. **Better Organization**: Files have meaningful names instead of technical identifiers
2. **Easier Identification**: Can see what the recording is without playing it
3. **Consistent Format**: Matches the naming convention used in Enigma2Manager
4. **Cross-Platform Compatibility**: Sanitized filenames work on Windows, macOS, and Linux
5. **Optional**: Can be disabled to keep original filenames if preferred
6. **Flexible**: Works even when EIT data is partially missing

## Comparison with Reference Project

This implementation matches the file naming format used in Enigma2Manager:
- **Format**: `{Series Name} - {Description}.{ext}`
- **No date/time stamps**: Keeps filenames cleaner and shorter
- **Prefers EIT data**: Uses metadata when available
- **Smart fallbacks**: Works gracefully when data is missing
- **Same sanitization**: Uses identical character replacement rules

## Related Files
- `E2ReccordingManager\Form1.Designer.cs` - UI definition
- `E2ReccordingManager\Form1.cs` - Download logic
- `E2ReccordingManager\Utilities\EITParser.cs` - EIT parsing (existing)
- `E2ReccordingManager\Models\Recording.cs` - Recording model with EIT data (existing)
