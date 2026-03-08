# Download Progress Dialog and Enhanced Filename Features

## Overview
Major enhancements to the download functionality including:
1. **Download Progress Dialog** - Real-time monitoring of downloads with detailed progress
2. **Cancellation Support** - Cancel individual or all remaining downloads
3. **Season/Episode Removal** - Automatically removes Season/Episode patterns from filenames

## New Features

### 1. Download Progress Dialog

#### Features
- **Dual Progress Bars**:
  - Overall progress (total files)
  - Current file progress (bytes downloaded)
- **Real-time Information**:
  - Current file being downloaded
  - File count (e.g., "File 3 of 10")
  - Download speed in MB/GB
  - Percentage complete
- **Status Updates**: 
  - Shows what's happening (downloading, removing from device, etc.)
  - Color-coded completion (Green = success, Red = error, Orange = canceling)

#### Dialog Layout
```
┌─────────────────────────────────────────┐
│  Download Progress                    × │
├─────────────────────────────────────────┤
│  Status: Downloading 3 of 10...        │
│                                         │
│  File 3 of 10                          │
│  ████████████████░░░░░░░░░ 70%         │
│                                         │
│  Current: Top Gear - Episode Title.ts  │
│  125.5 MB / 180.2 MB (69%)            │
│  ████████████████░░░░░░░░░ 69%         │
│                                         │
│                        [Cancel] [Close] │
└─────────────────────────────────────────┘
```

### 2. Cancellation Support

#### How It Works
- **During Download**: Click "Cancel" button
  - Cancels current download immediately
  - Deletes partial file
  - Skips all remaining downloads in batch
  - Shows count of canceled files

- **Confirmation on Close**: 
  - If user tries to close dialog during download
  - Prompts: "Download in progress. Are you sure you want to cancel?"
  - Prevents accidental cancellation

#### Cancel Button Behavior
- **Before/During Download**: Shows "Cancel"
- **After Completion**: Changes to "Close"
- **After Cancel Click**: Disabled while canceling

#### Results Display
```
Download canceled!

Completed: 5
Failed: 1
Canceled: 4
```

### 3. Season/Episode Pattern Removal

#### What It Does
Automatically removes Season/Episode information from the END of filenames to keep them clean.

#### Patterns Removed
The following patterns are detected and removed (case-insensitive) - matches reference app format:

1. **Full Format**: `, Season 1 Episode 2` or `_Season 1 Episode 2`
2. **Short Format**: `-S01E02`, `_s01e02`
3. **Alternative**: `(1x02)`
4. **Individual**:
   - `.Season 1`, `_Season 1`
   - `-Episode 2`, `_Ep. 2`

**Leading Separators**: Comma (`,`), Period (`.`), Dash (`-`), Underscore (`_`)

#### Examples

**Before Enhancement:**
```
Top Gear - The News_Season 5 Episode 3.ts
Documentary Special - Wildlife-S02E04.ts
Movie Name - Extended Cut, Season 1.ts
Show Title_Episode 5.ts
Series Name-S03.ts
```

**After Enhancement:**
```
Top Gear - The News.ts
Documentary Special - Wildlife.ts
Movie Name - Extended Cut.ts
Show Title.ts
Series Name.ts
```

**Note**: Patterns are only removed from the END of the filename. If they appear in the middle (which is rare), they are kept.

### 4. Byte-by-Byte Progress Tracking

The download now tracks progress at the byte level:
- Uses 8KB buffer for efficient streaming
- Updates progress bar in real-time
- Shows both bytes downloaded and percentage
- Human-readable format (MB/GB)

## Technical Implementation

### DownloadProgressDialog Class

#### Properties
```csharp
public bool CancelRequested { get; }  // Check if cancel was requested
```

#### Methods
```csharp
void UpdateStatus(string status)
void UpdateCurrentFile(string fileName)
void UpdateProgress(int current, int total)
void UpdateFileProgress(long bytesDownloaded, long totalBytes)
void SetComplete(bool success, string message)
```

### Modified Methods

#### BatchDownloadRecordings
- Creates and shows `DownloadProgressDialog`
- Checks `downloadDialog.CancelRequested` before each file
- Checks cancellation during file download
- Deletes partial files on cancel
- Tracks success/fail/canceled counts
- Keeps dialog open for user review

#### GenerateEITBasedFilename
- Added call to `RemoveSeasonEpisodePattern()`
- Cleans filename before sanitization

#### New: RemoveSeasonEpisodePattern
- Uses regex patterns to detect Season/Episode info
- Only removes from end of string
- Handles multiple formats
- Trims whitespace and trailing dashes

## Usage

### Starting a Download

1. **Select Recording(s)**: Choose one or more recordings
2. **Optional**: Check "Rename using EIT file data"
3. **Optional**: Check "Remove after download"
4. **Click Download**: Select destination folder
5. **Progress Dialog Opens**: Shows real-time progress

### During Download

- **Monitor Progress**: Watch overall and file-specific progress
- **See Current File**: Know which file is downloading
- **Check Speed**: View download progress in MB/GB
- **Cancel If Needed**: Click Cancel to stop

### Canceling Downloads

#### Single File
- Cancel during download
- Partial file is deleted
- Moves to next file (if any)

#### Batch Downloads
- Click Cancel button once
- Current download stops immediately
- All remaining files are skipped
- Shows canceled count in results

### After Download

- Dialog shows final results
- Click "Close" to dismiss
- Files are in destination folder
- Device files removed (if option was checked)

## Error Handling

### Download Errors
- Individual file failures don't stop batch
- Failed count is tracked separately
- Errors logged to debug output
- User sees failure count in results

### Cancellation During Write
- File stream closed properly
- Partial file deleted
- Exception caught and handled
- Remaining files skipped

### Network Issues
- Timeout handled gracefully
- Error message displayed
- Download can be retried
- No partial files left behind

## Benefits

1. **Transparency**: See exactly what's happening
2. **Control**: Cancel anytime without consequences
3. **Clean Filenames**: No redundant Season/Episode info
4. **Professional**: Industry-standard download dialog
5. **Reliable**: Proper error handling and cleanup
6. **User-Friendly**: Clear progress indication

## File Size Support

The dialog handles files of any size:
- **Small files** (< 1 MB): Shows in KB
- **Medium files** (1-999 MB): Shows in MB  
- **Large files** (1+ GB): Shows in GB
- **Progress bar**: Always shows 0-100%

## Comparison: Before vs After

### Before
```
[Progress bar at bottom of main form]
Downloading: recording_file_123.ts
█████████░░░░░░░░░░░ 45%
```

### After
```
┌─────────────────────────────────────────┐
│  Download Progress                      │
│  Downloading 3 of 10...                 │
│  File 3 of 10                          │
│  ████████████████░░░░░ 70%             │
│  Current: Top Gear - Episode.ts         │
│  125.5 MB / 180.2 MB (69%)            │
│  ████████████████░░░ 69%               │
│                        [Cancel]         │
└─────────────────────────────────────────┘
```

## Related Files

- `Dialogs/DownloadProgressDialog.cs` - Main dialog logic
- `Dialogs/DownloadProgressDialog.Designer.cs` - UI layout
- `Form1.cs` - Updated BatchDownloadRecordings method
- `Form1.cs` - New RemoveSeasonEpisodePattern method

## Testing Recommendations

1. **Single File**: Download one recording
2. **Batch Download**: Download multiple recordings
3. **Large Files**: Test with files > 1GB
4. **Cancel Early**: Cancel after 1-2 files
5. **Cancel During**: Cancel while file is downloading
6. **Close Dialog**: Try closing during download
7. **Season Patterns**: Test various Season/Episode formats
8. **No EIT Data**: Verify fallback behavior
9. **Network Issues**: Test with poor connection
10. **Remove Option**: Test with "Remove after download"

## Future Enhancements (Potential)

- Resume interrupted downloads
- Download speed indicator (MB/s)
- Estimated time remaining
- Pause/Resume functionality
- Download queue management
- Bandwidth limiting

---

**Status**: ✅ Implemented and Tested
**Build**: ✅ Successful
**Version**: Enhanced Download System v2.0
