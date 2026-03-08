# Local Recordings Tab - Quick Reference

## What Changed?

A new **Local Recordings** tab has been added for managing local recording files separately from device recordings.

## Tab Structure

### Before
```
[Device Recordings] [File Conversion]
     ↓
  Mixed device + local files
```

### After
```
[Device Recordings] [Local Recordings] [File Conversion]
     ↓                    ↓                   ↓
  Device only         Local only         Conversion
```

## Quick Start

### Using Local Recordings Tab

**Step 1: Select Folder**
```
1. Click "Local Recordings" tab
2. Click [...] button next to "Local Folder"
3. Select folder with .ts files
```

**Step 2: Load Recordings**
```
1. Click "Refresh" button
2. Recordings appear in list
3. Green text = has EPG data
```

**Step 3: Copy/Rename Recordings**
```
1. Select one or more recordings
2. Click "Download" button
3. Choose destination folder
4. Files copied and renamed
```

## Key Features

### Independent Operation
- ✅ Works without device connection
- ✅ Separate from device recordings
- ✅ Own refresh and download buttons

### Same Powerful Features
- ✅ EIT file parsing
- ✅ Smart copy with progress
- ✅ Skip identical files
- ✅ EIT-based renaming
- ✅ Auto MP4 conversion

### Visual Indicators
- 🟢 **Green text** = Has EPG/EIT data
- 📝 **Details pane** = Shows full info
- 📊 **Progress dialog** = Shows copy progress

## Common Tasks

### Task 1: Organize Downloaded Files
```
Problem: You have files downloaded via FTP
Solution:
  1. Go to Local Recordings tab
  2. Browse to download folder
  3. Refresh to see files
  4. Select files to organize
  5. Download to organized folder
  → Files copied with proper names
```

### Task 2: Rename Using EPG Data
```
Problem: Files have generic names
Solution:
  1. Ensure .eit files are present
  2. Load files in Local Recordings
  3. Check "Rename using EIT file data"
  4. Download to new folder
  → Files renamed from EPG data
```

### Task 3: Batch Copy to New Location
```
Problem: Need to copy many files
Solution:
  1. Load folder with recordings
  2. Select all files (Ctrl+A)
  3. Click Download
  4. Choose destination
  → All files copied with progress
```

## Comparison: Device vs Local

| Feature | Device Recordings | Local Recordings |
|---------|------------------|------------------|
| Source | Enigma2 device | Local folder |
| Connection | Required | Not required |
| Refresh | Loads from device | Loads from folder |
| Download | FTP transfer | File copy |
| Delete | Removes from device | Not available |
| EIT Data | Downloads from device | Reads local .eit |
| Rename | Yes | Yes |
| Convert | Yes | Yes |

## Tips & Tricks

### Tip 1: Keep EIT Files
```
Always keep .eit files next to .ts files:
C:\Recordings\
  ├── show1.ts
  ├── show1.eit  ← Important!
  ├── show2.ts
  └── show2.eit
```

### Tip 2: Check EPG Data
```
Before copying:
1. Select a file
2. Check details pane
3. Green text = good EPG data
4. Ready to rename properly
```

### Tip 3: Use Skip Detection
```
Re-running operations:
- Identical files skipped automatically
- Only new/changed files copied
- Saves time on large batches
```

### Tip 4: Organize by Workflow
```
Workflow folders:
C:\
├── Downloaded\      ← FTP downloads here
├── Processed\       ← Use Local tab to organize
└── Converted\       ← MP4 files here
```

## Keyboard Shortcuts

```
Ctrl+A          Select all recordings
Shift+Click     Select range
Ctrl+Click      Select multiple
```

## Troubleshooting

### No recordings shown
**Solution:**
1. Check folder has .ts files
2. Click Refresh button
3. Verify folder path is correct

### No EPG data
**Solution:**
1. Check .eit files exist
2. .eit must have same name as .ts
3. Check details pane for EPG info

### Copy seems slow
**Possible causes:**
- Network drive (use local drives)
- External USB 2.0 drive
- Many small files
**Solution:** Use SSD drives when possible

## Context Menu

Right-click on recording:
```
View Details    → Show full information
Download        → Copy and rename file
```

## Settings

**Saved automatically:**
- Local folder path
- Rename checkbox state (per session)

**From Settings dialog:**
- Auto-convert to MP4
- Delete .ts after conversion
- FFmpeg path and options

## Examples

### Example 1: First Time Setup
```
1. [Local Recordings] tab
2. Click [...] Browse button
3. Select: C:\Downloaded\Recordings
4. Path saved automatically
5. Click Refresh
6. See your recordings!
```

### Example 2: Daily Workflow
```
Morning: Download from device to C:\Downloaded
Evening:
  1. Open Local Recordings tab
  2. Folder already set (saved)
  3. Click Refresh
  4. Select recordings to keep
  5. Download to C:\Organized
  6. Files renamed and ready!
```

### Example 3: Batch Rename
```
You have 50 files with generic names:
1. Load in Local Recordings
2. Ctrl+A (select all)
3. Ensure "Rename using EIT" checked
4. Download to new folder
5. Progress shows 50 files processing
6. All files properly renamed!
```

## What Got Removed?

From Device Recordings tab:
- ❌ "Include local files" checkbox
- ❌ Local folder controls
- ❌ Mixed device + local view

**These moved to the new Local Recordings tab!**

## What Stayed The Same?

- ✅ All local file features
- ✅ EIT parsing
- ✅ Copy optimization
- ✅ Skip detection
- ✅ Progress tracking
- ✅ Auto-conversion
- ✅ Settings persistence

## Quick Decision Guide

**When to use each tab:**

### Use Device Recordings When:
- Connected to Enigma2 device
- Want to download from device
- Need to delete from device
- Managing device storage

### Use Local Recordings When:
- Working with local files
- No device available
- Organizing downloaded files
- Batch renaming needed
- Working offline

### Use File Conversion When:
- Converting .ts to .mp4
- Processing any .ts files
- Need conversion settings

## Summary

**Three simple tabs for three purposes:**

1. **Device Recordings** → Manage device
2. **Local Recordings** → Manage local files
3. **File Conversion** → Convert files

**Each tab is focused and independent!**

## Need Help?

Check these files:
- `LOCAL_RECORDINGS_TAB.md` - Complete documentation
- `LOCAL_FILES_QUICKSTART.md` - Original local files guide
- `README.md` - General application help
