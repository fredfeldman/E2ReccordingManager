# File Conversion Tab Feature

## Overview

The E2RecordingManager now includes a dedicated **File Conversion** tab that allows users to manually convert .TS files to .MP4 format. This provides flexibility to:
- Convert previously downloaded recordings
- Selectively convert specific files
- Convert files at a convenient time
- Re-convert files with different quality settings

## Features

### Two-Tab Interface

The application now has two main tabs:

1. **Device Recordings Tab**
   - Connect to Enigma2 device
   - Browse and download recordings
   - Manage device recordings
   - Automatic conversion after download (if enabled)

2. **File Conversion Tab**
   - Browse local folders for .TS files
   - Select files to convert
   - Manual conversion control
   - Status tracking for each file

### File Conversion Tab Components

#### Folder Selection
- **Text Box**: Displays currently selected folder path
- **Browse Button (...)**: Opens folder browser dialog
- **Sticky Selection**: Remembers last selected folder during session

#### File List View
- **Checkbox Selection**: Check/uncheck individual files for conversion
- **File Name**: Shows the complete filename
- **Size**: Displays file size in appropriate units (KB/MB/GB)
- **Modified Date**: Shows when file was last modified
- **Status**: Shows current state of each file
  - "Ready to convert" - File not yet converted
  - "Already converted" - MP4 version exists
  - "Converting..." - Conversion in progress
  - "Converted successfully" - Conversion completed
  - "Conversion failed" - Error occurred
  - "Error: [message]" - Specific error details

#### Control Buttons
- **Convert**: Starts conversion of selected files
- **Refresh**: Reloads file list from current folder
- **Select All**: Checks all files in the list
- **Select None**: Unchecks all files in the list
- **Settings**: Opens settings dialog for FFmpeg configuration

#### Status Colors
- **Black**: Normal, ready to convert
- **Gray**: Already converted (MP4 exists)
- **Blue**: Currently converting
- **Green**: Successfully converted
- **Red**: Failed or error

## User Workflow

### Basic Workflow

1. **Select Folder**
   - Click the **...** button next to the folder path
   - Navigate to folder containing .TS files
   - Click **OK** to load files

2. **Review Files**
   - File list automatically loads
   - Files already converted shown in gray
   - Check which files to convert

3. **Select Files**
   - Check individual files, or
   - Click **Select All** to select all files
   - Use **Select None** to clear selection

4. **Convert**
   - Click **Convert** button
   - Progress dialog shows:
     - Current file being converted
     - Overall progress (X of Y files)
     - Individual file progress percentage
     - Time elapsed for current file
   - File list updates with status for each file

5. **Review Results**
   - Progress dialog shows summary
   - File list shows final status
   - Converted .MP4 files are in same folder as .TS files

### Advanced Workflow

#### Changing Conversion Settings

Before converting, you can adjust quality settings:

1. Click **Settings** in toolbar
2. Navigate to **Conversion** tab
3. Adjust:
   - Quality preset (High/Balanced/Low)
   - Max bitrate
   - Hardware acceleration
   - TS file deletion option
4. Click **OK** to save
5. Return to File Conversion tab
6. Convert files with new settings

#### Re-Converting Files

To re-convert files with different settings:

1. Delete existing .MP4 files (Windows Explorer)
2. Click **Refresh** in File Conversion tab
3. Files will show "Ready to convert" again
4. Select and convert with new settings

#### Batch Processing

For large numbers of files:

1. Sort/filter files in folder (Windows Explorer)
2. Move files to separate folders by type/date
3. Process each folder separately
4. Use **Select All** for quick batch selection

## Technical Details

### File Detection
- Scans only top-level folder (no subfolders)
- Looks for files with `.ts` extension
- Case-insensitive file extension matching
- Checks for corresponding `.mp4` file

### Conversion Process
- Uses FFmpeg with settings from Settings dialog
- Creates .MP4 in same folder as source .TS
- Preserves original filename (only changes extension)
- Optionally deletes .TS after successful conversion
- Progress tracked in real-time
- Supports cancellation (close progress dialog)

### Status Updates
- Real-time status updates during conversion
- Color-coded status for quick visual feedback
- Detailed error messages for failed conversions
- Persistent status in list view

### FFmpeg Validation
- Tests FFmpeg before starting conversion
- Prompts to configure if not found
- Opens Settings dialog for quick configuration
- Prevents conversion attempt with invalid setup

## Interface Components

### Folder Path Section
```
Folder: [____________________________________...] 
```
- Full path display
- Browse button for selection
- Persistent across tab switches

### File List Grid
```
☐ File Name              | Size    | Modified Date       | Status
☐ recording1.ts         | 2.5 GB  | 2024-01-15 20:30   | Ready to convert
☐ recording2.ts         | 1.8 GB  | 2024-01-16 21:00   | Already converted
```

### Button Bar
```
[Convert] [Refresh] [Select All] [Select None]
```

### Information Label
```
Select .TS files to convert to .MP4. Conversion settings can be changed in Settings.
```

## Keyboard Shortcuts

- **Space**: Toggle checkbox for selected item
- **Ctrl+A**: Select all items (then space to check)
- **F5**: Refresh file list (when folder textbox has focus)

## Error Handling

### Common Errors and Solutions

| Error | Cause | Solution |
|-------|-------|----------|
| FFmpeg not found | FFmpeg not configured | Click Settings, configure FFmpeg path |
| Folder not found | Invalid folder path | Browse to valid folder |
| No files selected | No checkboxes checked | Check at least one file |
| Conversion failed | Invalid file or FFmpeg error | Check file integrity, try different file |
| Access denied | File locked or permission issue | Close programs using file, check permissions |
| Out of disk space | Insufficient storage | Free up disk space |

### Error Messages

The application provides clear error messages:
- **FFmpeg Configuration**: "FFmpeg is not properly configured. Would you like to configure it now?"
- **No Selection**: "Please select at least one file to convert."
- **File Errors**: "Error: [specific error message]"

## Best Practices

### Before Converting
1. ✅ Ensure FFmpeg is configured (test in Settings)
2. ✅ Check available disk space (MP4 ~60-80% of TS size)
3. ✅ Close other programs using video files
4. ✅ Review quality settings
5. ✅ Verify source .TS files are not corrupted

### During Conversion
1. ✅ Don't close the application
2. ✅ Monitor progress in progress dialog
3. ✅ Watch for error messages
4. ✅ Keep computer powered on (disable sleep mode for large batches)

### After Conversion
1. ✅ Verify .MP4 files play correctly
2. ✅ Compare quality with original if needed
3. ✅ Check file sizes are reasonable
4. ✅ Delete .TS files manually if auto-delete disabled
5. ✅ Refresh file list to see updated status

## Tips and Tricks

### Organizing Files
- **Folders by Date**: Create folders like "2024-01" for monthly organization
- **Folders by Type**: Separate movies, TV shows, sports, etc.
- **Naming Convention**: Use consistent naming for easy identification

### Efficient Conversion
- **Use Hardware Acceleration**: Enable NVENC for faster conversion
- **Batch by Quality**: Convert similar content with same quality preset
- **Convert During Idle**: Start batch conversion when not using computer
- **Monitor First File**: Watch first conversion to ensure quality is acceptable

### Quality vs Size Tradeoffs
- **High Quality**: Best for archival, action scenes, sports
- **Balanced**: Good for most TV shows, documentaries
- **Low Quality**: Acceptable for talk shows, older content

## Integration with Auto-Convert

The File Conversion tab complements the auto-convert feature:

### Auto-Convert (After Download)
- Automatic, hands-off
- Processes immediately after download
- Uses current settings
- Best for: New downloads, consistent workflow

### Manual Convert (File Conversion Tab)
- Manual selection and control
- Process at convenient time
- Can adjust settings per batch
- Best for: Old files, selective conversion, different settings

### Recommended Workflow
1. Enable auto-convert for new downloads
2. Use File Conversion tab for:
   - Old recordings not yet converted
   - Re-converting with different quality
   - Selectively converting specific files
   - Converting files from other sources

## Comparison with Device Recordings Tab

| Feature | Device Recordings | File Conversion |
|---------|-------------------|-----------------|
| **Purpose** | Manage device recordings | Convert local files |
| **Connection** | Requires device connection | No connection needed |
| **Source** | Enigma2 device | Local disk |
| **Selection** | Download selected recordings | Convert selected files |
| **Automatic** | Can auto-convert after download | Manual only |
| **Batch** | Downloads then converts | Converts only |
| **Offline** | No | Yes |

## Troubleshooting

### Files Not Appearing
- Check folder path is correct
- Verify files have `.ts` extension
- Click Refresh button
- Check file permissions

### Conversion Slow
- Enable hardware acceleration in Settings
- Lower quality preset
- Close other applications
- Check CPU/GPU usage

### Progress Dialog Closes Immediately
- Check if FFmpeg is properly configured
- Verify source files are valid
- Check Debug output for error messages

### Files Show "Already Converted"
- .MP4 file exists in same folder
- Delete .MP4 to reconvert
- Click Refresh to update status

## Future Enhancements

Possible improvements for future versions:
- Subfolder scanning option
- File filtering by date/size
- Drag-and-drop file/folder support
- Conversion queue management
- Preset profiles for different use cases
- Progress in main window (not just dialog)
- Remember last folder path
- Export/import file lists
- Scheduled/deferred conversion

## See Also

- **Automatic Conversion**: See `TS_TO_MP4_CONVERSION.md`
- **Settings**: See Settings Dialog documentation
- **Download**: See `DOWNLOAD_PROGRESS_FEATURE.md`
- **Quick Reference**: See `TS_TO_MP4_QUICK_REFERENCE.md`
