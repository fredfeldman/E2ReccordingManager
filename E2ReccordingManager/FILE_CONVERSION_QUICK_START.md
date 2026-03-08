# File Conversion Tab - Quick Start Guide

## 5-Second Overview
New **File Conversion** tab lets you manually convert .TS files to .MP4 from any folder.

## 3 Steps to Convert Files

### Step 1: Select Folder
Click **...** button → Browse to folder with .TS files → Click OK

### Step 2: Select Files
- Click **Select All** (or check individual files)
- Files already converted shown in gray

### Step 3: Convert
Click **Convert** button → Watch progress → Done!

## Tab Layout

```
┌─ Enigma2 Recording Manager ──────────────────────────────┐
│ [Connect] [Disconnect] │ Not Connected │ [Settings]      │
├──────────────────────────────────────────────────────────┤
│ [Device Recordings] [File Conversion]                    │
│                                                           │
│ Folder: [C:\My Recordings\_________________] [...]       │
│                                                           │
│ ☐ recording1.ts    2.5 GB   2024-01-15   Ready          │
│ ☐ recording2.ts    1.8 GB   2024-01-16   Already conv.. │
│ ☐ recording3.ts    3.2 GB   2024-01-17   Ready          │
│                                                           │
│ [Convert] [Refresh] [Select All] [Select None]          │
│                                                           │
│ Select .TS files to convert to .MP4...                   │
└───────────────────────────────────────────────────────────┘
```

## Button Reference

| Button | Function |
|--------|----------|
| **...** | Browse for folder |
| **Convert** | Start converting selected files |
| **Refresh** | Reload file list |
| **Select All** | Check all files |
| **Select None** | Uncheck all files |
| **Settings** | Configure FFmpeg/quality |

## Status Colors

| Color | Meaning |
|-------|---------|
| Black | Ready to convert |
| Gray | Already converted (MP4 exists) |
| Blue | Converting now |
| Green | Converted successfully |
| Red | Failed or error |

## Common Tasks

### Convert All Files in Folder
1. Browse to folder
2. Click **Select All**
3. Click **Convert**

### Convert Specific Files
1. Browse to folder
2. Check desired files manually
3. Click **Convert**

### Re-convert with Different Quality
1. Open **Settings** → Change quality
2. Delete old .MP4 files (Windows Explorer)
3. Click **Refresh** in app
4. Select and convert

### Check What's Already Converted
1. Browse to folder
2. Gray files = Already have .MP4

## Quick Settings

Click **Settings** in toolbar for:
- FFmpeg path configuration
- Quality: High / Balanced / Low
- Hardware acceleration on/off
- Auto-delete .TS files option
- Max bitrate adjustment

## Tips

💡 **First Time**: Configure FFmpeg in Settings first  
💡 **Fast Conversion**: Enable hardware acceleration  
💡 **Save Space**: Enable "Delete .TS after conversion"  
💡 **Test Quality**: Convert one file first, check quality  
💡 **Batch Process**: Use Select All for efficiency  

## Troubleshooting

| Problem | Solution |
|---------|----------|
| No files show up | Click Browse, select correct folder |
| "FFmpeg not found" | Click Settings, configure FFmpeg path |
| Nothing selected | Check at least one file |
| Conversion fails | Check Settings, test FFmpeg |
| Already converted | Delete .MP4, click Refresh |

## Workflow Comparison

### Device Recordings Tab
- Download from Enigma2 device
- Auto-convert after download
- For new recordings

### File Conversion Tab
- Convert local files
- Manual selection
- Any .TS files, any time

## Keyboard Shortcuts

- **Space**: Toggle checkbox (when item selected)
- **Ctrl+A**: Select all items
- **F5**: Refresh (when folder box focused)

## Integration with Auto-Convert

Auto-convert (after download):
- ✅ New downloads
- ✅ Hands-off
- ✅ Immediate

Manual convert (this tab):
- ✅ Old files
- ✅ Selective
- ✅ Flexible timing

Both use the same quality settings from Settings dialog!

## Status Messages

- **"Ready to convert"** → File is new, hasn't been converted
- **"Already converted"** → .MP4 file exists in same folder
- **"Converting..."** → Conversion in progress
- **"Converted successfully"** → Done, check folder for .MP4
- **"Conversion failed"** → Error occurred, check settings
- **"Error: [message]"** → Specific error details

## Where Are Converted Files?

Converted .MP4 files are created in the **same folder** as the source .TS files.

Example:
- Source: `C:\Recordings\movie.ts`
- Output: `C:\Recordings\movie.mp4`

## Before First Use

1. ✅ Install FFmpeg ([ffmpeg.org](https://ffmpeg.org))
2. ✅ Click **Settings** → Configure FFmpeg path
3. ✅ Click **Test** to verify
4. ✅ Choose quality preset
5. ✅ Click **OK** to save

Now you're ready to convert!

## Quick Reference Card

```
╔══════════════════════════════════════════════════════════╗
║           FILE CONVERSION TAB QUICK REFERENCE            ║
╠══════════════════════════════════════════════════════════╣
║ 1. Select Folder        → Browse button (...)           ║
║ 2. Select Files         → Check boxes / Select All      ║
║ 3. Convert              → Convert button                ║
╠══════════════════════════════════════════════════════════╣
║ Settings      → Configure FFmpeg, quality                ║
║ Refresh       → Reload file list                         ║
║ Select All    → Check all files                          ║
║ Select None   → Uncheck all files                        ║
╠══════════════════════════════════════════════════════════╣
║ Black   → Ready    │ Blue   → Converting                 ║
║ Gray    → Already  │ Green  → Success                    ║
║ Red     → Failed   │                                     ║
╚══════════════════════════════════════════════════════════╝
```

## Need More Help?

See full documentation:
- `FILE_CONVERSION_TAB.md` - Complete feature guide
- `TS_TO_MP4_CONVERSION.md` - Conversion details
- `TS_TO_MP4_QUICK_REFERENCE.md` - Conversion quick ref

## One-Liner Summary

**Browse → Select → Convert** 🎬
