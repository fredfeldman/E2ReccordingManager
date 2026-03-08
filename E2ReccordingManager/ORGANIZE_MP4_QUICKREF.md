# Organize MP4 Files - Quick Reference

## What It Does
Automatically moves MP4 files into folders named after their series.

## Location
**File Conversion Tab** → "Organize" button (next to Select None)

## Quick Steps
1. Select folder with MP4 files
2. Click "Organize"
3. Confirm in dialog
4. Files organized into series folders!

## Example Result

**Before:**
```
Doctor Who - Episode 1.mp4
Doctor Who - Episode 2.mp4
Star Trek - Movie.mp4
```

**After:**
```
Doctor Who\
  ├── Doctor Who - Episode 1.mp4
  └── Doctor Who - Episode 2.mp4
Star Trek\
  └── Star Trek - Movie.mp4
```

## How Series Names Are Detected

| Filename | Series Folder |
|----------|---------------|
| `Doctor Who - The Time War.mp4` | `Doctor Who` |
| `20240115 - BBC One - News.mp4` | `BBC One` |
| `Star_Trek_Episode.mp4` | `Star Trek` |
| `RandomFile.mp4` | `Unknown Series` |

## Features
✅ Smart series detection from filenames  
✅ Handles duplicates (adds _1, _2, etc.)  
✅ Safe - only moves, doesn't delete  
✅ Progress tracking  
✅ Automatic folder creation  
✅ Invalid character handling  

## Best Workflow
1. Download recordings → Device Recordings tab
2. Convert to MP4 → File Conversion tab ("Convert" button)
3. Organize by series → File Conversion tab ("Organize" button)
4. Enjoy organized library! 🎉

## Tips
💡 Use EIT renaming for best series detection  
💡 Organize after converting all files  
💡 Check "Unknown Series" folder for mismatched files  
💡 Only processes root folder (not subfolders)  
💡 Only moves MP4 files (TS files remain)  

## Common Issues

**No files found?**
- Convert TS to MP4 first
- Check you're in the right folder

**Wrong series name?**
- Manually rename the folder after organizing
- Or rename MP4 file before organizing

**Files stuck in "Unknown Series"?**
- Filenames don't match patterns
- Move files manually to correct folder

## Button States
- **Enabled**: When folder selected
- **Disabled**: During organization process
- **Auto-refresh**: File list updates after organizing

## Safety
- **Non-destructive**: Only moves files
- **Confirmation**: Always asks before proceeding
- **Reversible**: Manually move files back if needed
- **Error handling**: Failed files stay in place

## Example Messages

**Confirmation:**
```
This will organize 15 MP4 file(s) into folders by series name.
Files will be moved into subfolders named after their series.
Do you want to continue?
```

**Success:**
```
Organization complete!
Organized: 15
Failed: 0
```

---

**See Also:**
- [Full Documentation](ORGANIZE_MP4_FEATURE.md)
- [File Conversion Guide](FILE_CONVERSION_TAB.md)
- [Select All Feature](SELECT_ALL_FEATURE.md)
