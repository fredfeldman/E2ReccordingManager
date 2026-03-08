# Quick Reference: New Download Features

## ✅ What's New

### 1. Professional Download Dialog
- Real-time progress for each file
- Overall batch progress
- File size and percentage display
- Cancel button that works on entire batch

### 2. Smart Filename Cleanup
- Removes "Season # Episode #" patterns
- Removes "S01E02" style codes
- Removes "1x02" format
- Only from END of filename

### 3. Full Cancellation Support
- Cancel button stops ALL remaining downloads
- Cleans up partial files automatically
- Shows canceled count in results

## 🎯 Quick Examples

### Filename Cleanup
```
❌ Before: Top Gear - The News - Season 5 Episode 3.ts
✅ After:  Top Gear - The News.ts

❌ Before: Documentary - Wildlife - S02E04.ts
✅ After:  Documentary - Wildlife.ts

❌ Before: Movie Name - 1x12.ts
✅ After:  Movie Name.ts
```

### Progress Dialog Display
```
Download Progress
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Status: Downloading 5 of 20...

Overall: File 5 of 20
████████████░░░░░░░░░░░░░░░░░ 25%

Current: Show Title - Episode Name.ts
850.2 MB / 1.2 GB (70%)
████████████████░░░░░░░░░ 70%

                    [Cancel]
```

### Cancellation Results
```
Download canceled!

Completed: 5
Failed: 1  
Canceled: 14

[Close]
```

## 🚀 How to Use

### Start Download
1. Select recording(s)
2. Click "Download"
3. Choose folder
4. Dialog appears automatically

### Cancel Downloads
1. Click "Cancel" button
   - Stops current file
   - Skips all remaining files
   - Shows canceled count

### After Download
- Click "Close" to dismiss dialog
- Files are in your chosen folder
- Clean filenames without Season/Episode codes

## 📋 Patterns Removed (Case-Insensitive)

Matches the reference app format exactly. Removes patterns with leading separators (, . - _):

| Pattern | Example | Result |
|---------|---------|--------|
| , Season # Episode # | , Season 5 Episode 3 | *(removed)* |
| _Season # Episode # | _Season 5 Episode 3 | *(removed)* |
| -S##E## | -S05E03 | *(removed)* |
| _s##e## | _s05e03 | *(removed)* |
| .Season # | .Season 5 | *(removed)* |
| -Episode # | -Episode 3 | *(removed)* |
| _Ep. # | _Ep. 1 | *(removed)* |
| (#x#) | (5x03) | *(removed)* |

**Leading Separators Supported**: `,` `.` `-` `_`

**Note**: Only removed from the END of filenames

## ⚡ Key Features

### Progress Tracking
- ✅ Overall progress (all files)
- ✅ Current file progress (bytes)
- ✅ File size display (MB/GB)
- ✅ Percentage complete
- ✅ Current filename shown

### Cancellation
- ✅ Cancel button available anytime
- ✅ Cancels ALL remaining downloads
- ✅ Deletes partial files
- ✅ Shows canceled count
- ✅ Confirmation if closing dialog

### Filename Cleanup
- ✅ Automatic Season/Episode removal
- ✅ Works with EIT renaming
- ✅ Multiple format support
- ✅ Only affects end of filename
- ✅ Preserves episode descriptions

## 🔍 What Gets Kept vs Removed

### ✅ KEPT (Description Content)
```
"The Grand Tour"
"Episode about cars"
"Winter Special"
"Part 1 of 2"
"Documentary: Season in the wild"  ← "Season" in middle is kept
```

### ❌ REMOVED (Format Codes at End)
```
"- Season 1 Episode 2"
"- S01E02"
"- 1x02"
"- Episode 5"
"- S03"
```

## 💡 Tips

1. **Batch Downloads**: Cancel works for all remaining files
2. **Large Files**: Progress shows in GB automatically
3. **Rename Option**: Works together with Season/Episode removal
4. **Close Dialog**: Confirms if download is in progress
5. **Results Summary**: Dialog stays open to show final counts

## 🛠️ Technical Notes

- Buffer size: 8KB for efficient streaming
- Progress updates: Real-time during download
- Partial files: Automatically deleted on cancel
- File size: Supports any size (displays in appropriate unit)
- Regex patterns: 8 different Season/Episode formats detected

## 📊 Dialog States

| State | Cancel Button | Behavior |
|-------|---------------|----------|
| Starting | "Cancel" | Can cancel |
| Downloading | "Cancel" | Can cancel |
| Canceling | *disabled* | Processing |
| Complete | "Close" | Dismisses dialog |
| Failed | "Close" | Dismisses dialog |

## ⚠️ Important Notes

1. **Cancel = Cancel All**: One click stops entire batch
2. **Partial Files**: Automatically cleaned up
3. **Pattern Location**: Only END of filename is checked
4. **Case Insensitive**: S01E02 = s01e02 = S01e02
5. **Dialog Stays Open**: Close manually after reviewing results

---

**Need More Details?** See `DOWNLOAD_PROGRESS_FEATURE.md`
