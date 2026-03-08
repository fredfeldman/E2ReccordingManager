# Organize MP4 Files Feature

## Overview
The **Organize** button automatically organizes MP4 files into folders based on their series names. This feature is located on the **File Conversion** tab and helps you maintain a clean, well-structured media library.

## Location
**Tab**: File Conversion  
**Button**: "Organize" (next to Select None button)

## How It Works

### 1. Series Name Extraction
The feature intelligently extracts the series name from MP4 filenames using multiple patterns:

**Pattern 1: Series - Episode Format**
- Example: `Doctor Who - The Time War.mp4`
- Extracted series: `Doctor Who`
- Creates folder: `Doctor Who\`

**Pattern 2: Date-Series-Episode Format**
- Example: `20240115 - BBC One - News Special.mp4`
- Extracted series: `BBC One`
- Creates folder: `BBC One\`

**Pattern 3: Cleaned Filename**
- Removes date patterns (YYYYMMDD, YYYY-MM-DD)
- Cleans up underscores and dashes
- Example: `Star_Trek_20240115.mp4`
- Extracted series: `Star Trek`
- Creates folder: `Star Trek\`

**Fallback**
- If no pattern matches, uses the entire filename as series name
- Unknown or problematic names go to: `Unknown Series\`

### 2. Folder Creation
- Automatically creates subfolders in the selected conversion folder
- Sanitizes folder names (removes invalid characters)
- Handles existing folders gracefully

### 3. File Organization
- Moves MP4 files from root folder to series-specific subfolders
- Preserves original filenames
- Handles duplicate filenames by appending `_1`, `_2`, etc.
- Only processes MP4 files (leaves TS and other files untouched)

## Usage

### Step-by-Step

1. **Select Folder**
   - Browse to the folder containing your MP4 files
   - This is typically the same folder used for conversions

2. **Click "Organize"**
   - Button is located next to "Select None"
   - Confirmation dialog appears showing number of MP4 files found

3. **Confirm Organization**
   - Review the confirmation message
   - Click "Yes" to proceed
   - Click "No" to cancel

4. **Watch Progress**
   - Status dialog shows progress
   - Displays: "Organizing file X of Y..."
   - Shows completion summary when done

5. **Review Results**
   - Success message shows organized vs failed count
   - File list automatically refreshes
   - MP4 files are now in series-specific folders

## Before and After Examples

### Before Organization
```
D:\Recordings\
├── Doctor Who - The Time War.mp4
├── Doctor Who - The Daleks.mp4
├── Star Trek - First Contact.mp4
├── Star Trek - Nemesis.mp4
├── Breaking Bad - Pilot.mp4
└── News Special.mp4
```

### After Organization
```
D:\Recordings\
├── Doctor Who\
│   ├── Doctor Who - The Time War.mp4
│   └── Doctor Who - The Daleks.mp4
├── Star Trek\
│   ├── Star Trek - First Contact.mp4
│   └── Star Trek - Nemesis.mp4
├── Breaking Bad\
│   └── Breaking Bad - Pilot.mp4
└── Unknown Series\
    └── News Special.mp4
```

## Features

### ✅ Smart Series Detection
- Recognizes multiple filename patterns
- Removes date stamps automatically
- Handles various separators (dash, underscore, space)

### ✅ Safe File Handling
- Confirmation dialog before proceeding
- Duplicate filename detection
- Automatic renaming of duplicates (_1, _2, etc.)
- Error handling for locked files

### ✅ Folder Name Sanitization
- Removes invalid characters automatically
- Replaces problematic characters with safe alternatives
- Limits folder name length to 100 characters
- Preserves readability while ensuring compatibility

### ✅ Progress Tracking
- Real-time status updates
- Progress bar showing completion percentage
- Summary of organized vs failed files

### ✅ Non-Destructive
- Only moves files (doesn't delete or modify)
- Original filenames preserved
- Easy to undo manually if needed

## Character Sanitization

Invalid characters are automatically replaced:

| Original | Replaced With |
|----------|---------------|
| `:` | `-` |
| `/` `\` | `-` |
| `?` `*` | ` ` (space) |
| `"` | `'` |
| `<` `>` | `(` `)` |
| `\|` | `-` |

## Error Handling

### Common Issues

**No MP4 Files Found**
- Ensure you're in the correct folder
- Check that conversion has completed
- Verify files have .mp4 extension (lowercase)

**File Move Failed**
- Check file isn't open in media player
- Verify folder permissions
- Ensure sufficient disk space

**Duplicate Filenames**
- Automatically handled by appending numbers
- Example: `Episode_1.mp4`, `Episode_1_1.mp4`

### Failed Files
- Failed count shown in summary
- Original files remain untouched
- Check debug log for specific errors

## Technical Details

### File Operations
```csharp
// Extract series name
var seriesName = ExtractSeriesName(filename);

// Sanitize for filesystem
seriesName = SanitizeFolderName(seriesName);

// Create folder if needed
Directory.CreateDirectory(seriesFolderPath);

// Move file to series folder
File.Move(sourcePath, destPath);
```

### Series Name Extraction Logic
1. Look for " - " separator (extracts text before first dash)
2. Remove date patterns (YYYYMMDD, YYYY-MM-DD)
3. Clean up underscores and dashes
4. Validate result is reasonable (>3 characters)
5. Fallback to full filename if needed

### Folder Name Length
- Maximum: 100 characters
- Automatically truncated if longer
- Ensures compatibility with all filesystems

## Best Practices

### 1. Organize After Conversion
- Convert all TS files to MP4 first
- Then organize all MP4 files at once
- More efficient than organizing individually

### 2. Check Folder Structure
- Review created folders after organization
- Manually rename folders if needed
- Move files between folders if categorization incorrect

### 3. Consistent Naming
- Use EIT-based renaming for best results
- Consistent format = better series detection
- Format: `SeriesName - EpisodeTitle.mp4`

### 4. Backup Before First Use
- Test on a copy of files first
- Verify organization works as expected
- Then use on main library

### 5. Regular Maintenance
- Organize after each batch of conversions
- Keep folder structure clean
- Periodically review "Unknown Series" folder

## Integration with Other Features

### Works With
- **Convert Selected**: Convert TS to MP4, then organize
- **Auto Convert**: Auto-converts after download, organize manually
- **EIT Renaming**: Better filenames = better series detection

### Workflow Example
1. Download recordings from device
2. Convert to MP4 with EIT-based naming
3. Click "Organize" to sort into series folders
4. Result: Clean, organized media library

## Keyboard Shortcuts

None currently assigned. Use mouse to click "Organize" button.

## Limitations

### Current Limitations
- Only processes MP4 files (by design)
- Only processes files in root folder (not subfolders)
- Series detection based on filename only (no metadata reading)
- Manual folder renaming if series name incorrect

### Future Enhancements (Potential)
- Read series metadata from MP4 tags
- Option to copy instead of move
- Undo functionality
- Custom series name mapping
- Process subfolders recursively

## Troubleshooting

### "No MP4 files found"
**Cause**: Folder doesn't contain MP4 files  
**Solution**: Convert TS files first, or select correct folder

### "Error organizing files"
**Cause**: Permission issues, locked files, or disk full  
**Solution**: Close media players, check permissions, free up space

### Files in "Unknown Series" folder
**Cause**: Filename doesn't match any pattern  
**Solution**: Manually rename folder, or move files to correct series folder

### Duplicate file numbers (_1, _2)
**Cause**: Multiple files with same name in different series  
**Solution**: Normal behavior for duplicates, or manually rename files

## Related Features

- **File Conversion Tab**: Convert TS to MP4
- **EIT Renaming**: Create meaningful filenames
- **Auto Convert**: Automatic TS to MP4 conversion
- **Settings**: Configure conversion quality and options

## Files Modified

1. **Form1.Designer.cs**
   - Added `btnOrganizeMp4Files` button
   - Positioned next to Select None button
   - Configured click event handler

2. **Form1.cs**
   - Added `BtnOrganizeMp4Files_Click()` event handler
   - Added `ExtractSeriesName()` method
   - Added `SanitizeFolderName()` method
   - Integrated with StatusDialog for progress tracking

## Summary

The **Organize** feature provides automatic, intelligent organization of MP4 files into series-specific folders. It extracts series names from filenames, creates appropriate folder structures, and safely moves files while handling duplicates and errors gracefully. This feature significantly improves media library organization and makes it easier to navigate large collections of recordings.

**Result**: A clean, well-organized media library where each series has its own folder! 📁✨
