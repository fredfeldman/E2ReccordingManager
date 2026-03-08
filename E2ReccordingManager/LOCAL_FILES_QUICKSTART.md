# Local Files Quick Start Guide

## What is This Feature?

This feature allows you to include recording files from a local folder (files already downloaded via FTP) in the Device Recordings tab. The download button will rename these files based on EIT data without needing to transfer them again.

## Quick Start

### 1. Enable Local Files (One-Time Setup)
```
Device Recordings Tab → Check "Include local files" checkbox
```

### 2. Select Your Local Folder
```
Device Recordings Tab → Click "..." button next to "Local Folder" → Select folder with .ts files
```

### 3. Load Files
```
Click "Refresh" button
```

### 4. Copy and Rename Files
```
Select file(s) → Click "Download" → Choose destination → Files are copied and renamed
```

## Visual Guide

### What You'll See

**In the Recordings List:**
- Device files: Normal white background
- Local files: Light blue background
- Files with EIT data: Green text

**In the Details Pane:**
```
Title: Show Title
Channel: Local              ← Shows "Local" for local files
Date: 2024-01-15 20:00
Duration: 1h 30m
Size: 2.45 GB
File: recording.ts
Source: Local File          ← Confirms it's a local file
Path: C:\Recordings\...     ← Shows full path
```

**Button States:**
- Download: Enabled (copies and renames)
- Delete: Disabled (cannot delete local files)

## Common Scenarios

### Scenario 1: Rename Previously Downloaded Files
```
1. ✓ Include local files
2. Browse to folder with downloaded .ts files
3. Refresh
4. Select files
5. Download (will copy and rename based on EIT)
```

### Scenario 2: Work with Device and Local Files Together
```
1. Connect to device
2. ✓ Include local files
3. Select local folder
4. Refresh
5. See both device (white) and local (blue) files
6. Download/copy any files
7. Delete device files if needed
```

### Scenario 3: Organize Local Archive
```
1. ✓ Include local files
2. Select archive folder
3. Refresh
4. Review EIT data (green = has EPG info)
5. Select files to organize
6. Download to organized destination
```

## Important Notes

### ✅ What Works
- Loading .ts files from local folder
- Parsing .eit files for EPG data
- Renaming based on EIT data
- Copying files to new location
- Progress tracking during copy
- Mixed device + local view
- Works without device connection

### ❌ What Doesn't Work
- Cannot delete local files
- Only .ts files shown (no .mp4, etc.)
- No recursive folder scanning
- Cannot edit local file metadata

## Tips & Tricks

### Tip 1: Keep EIT Files
Always keep the .eit file next to each .ts file:
```
Recordings/
  ├── show1.ts
  ├── show1.eit  ← Needed for renaming
  ├── show2.ts
  └── show2.eit
```

### Tip 2: Check Before Renaming
1. Enable "Rename using EIT file data" checkbox
2. Select a file
3. Check details pane for EPG info (green = good)
4. Files with EPG data will rename properly

### Tip 3: Use with Auto-Convert
If you have "Auto-convert to MP4" enabled:
1. Local files are copied to destination
2. Renamed based on EIT data
3. Automatically converted to MP4
4. Original .ts deleted (if enabled)

### Tip 4: Folder Organization
Organize by status:
```
C:\Recordings\
  ├── Downloaded\     ← Point local folder here
  ├── Processed\      ← Use as download destination
  └── Converted\      ← For MP4 files
```

## Keyboard Shortcuts

```
Ctrl+A           Select all recordings
Shift+Click      Select range
Ctrl+Click       Select multiple
```

## Troubleshooting

### Problem: Local files not showing
**Solution:**
1. Verify folder contains .ts files
2. Click Refresh button
3. Check "Include local files" is checked

### Problem: No EPG data for renaming
**Solution:**
1. Ensure .eit files are present
2. .eit must have same name as .ts
3. Check details pane for EPG info

### Problem: Cannot delete local file
**Expected:**
- Delete is disabled for local files
- Only device files can be deleted
- Use Windows Explorer to delete local files

### Problem: Slow loading
**Cause:**
- Large folder with many files
**Solution:**
- Use smaller folders
- Split files into multiple folders

## Example Workflow

### Daily Workflow: Download and Organize
```
Morning:
1. Connect to device
2. Download new recordings
3. Files saved to C:\Recordings\Downloaded\

Evening:
1. Open application
2. ✓ Include local files
3. Browse to C:\Recordings\Downloaded\
4. Refresh
5. Review recordings (check EPG data)
6. Select recordings to keep
7. Download to C:\Recordings\Processed\
8. Files renamed with proper titles
9. Auto-converted to MP4 (if enabled)
```

## Settings Location

Settings are automatically saved to:
```
%AppData%\E2ReccordingManager\settings.json
```

Includes:
- Local folder path
- Include local files checkbox state
- All other application settings

## What Gets Copied

When you click Download for a local file:
```
Source Folder:
  ├── recording.ts       → Copied and renamed
  └── recording.eit      → Copied with same name

Destination Folder:
  ├── Show Name - Episode Title.ts
  └── Show Name - Episode Title.eit
```

## Next Steps

1. ✅ Try loading a local folder
2. ✅ Check that EIT files are detected
3. ✅ Test the copy/rename operation
4. ✅ Try mixed mode (device + local)
5. ✅ Configure auto-convert if desired

## Need Help?

Check these files for more information:
- `LOCAL_FILES_FEATURE.md` - Complete feature documentation
- `LOCAL_FILES_IMPLEMENTATION.md` - Technical details
- `README.md` - General application help
