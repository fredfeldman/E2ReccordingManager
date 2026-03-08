# Select All Feature

## Overview
Added "Select All" buttons to all three tabs (Device Recordings, Local Recordings, and File Conversion) to enable quick selection of multiple recordings via checkboxes.

## Changes Made

### 1. Enabled Checkboxes on ListViews
- **Device Recordings** (`listViewRecordings`): Added `CheckBoxes = true`
- **Local Recordings** (`listViewLocalRecordings`): Added `CheckBoxes = true`
- **File Conversion** (`listViewLocalFiles`): Already had checkboxes enabled

### 2. Added Select All Buttons

#### Device Recordings Tab
- **Button**: `btnSelectAllRecordings`
- **Location**: Right side of control panel (X: 849, Y: 18)
- **Behavior**: 
  - Disabled by default
  - Enabled after successful recording list load
  - Disabled on disconnect
- **Event Handler**: `BtnSelectAllRecordings_Click()`

#### Local Recordings Tab
- **Button**: `btnSelectAllLocalRecordings`
- **Location**: After "Rename using EIT" checkbox (X: 497, Y: 18)
- **Behavior**: 
  - Enabled after successful local recordings load
- **Event Handler**: `BtnSelectAllLocalRecordings_Click()`

#### File Conversion Tab
- **Button**: `btnSelectAll` (already existed)
- **Event Handler**: `BtnSelectAll_Click()` (already implemented)

### 3. Enhanced Download Functionality

Both Device Recordings and Local Recordings download handlers now support:

**Smart Selection Logic**:
```csharp
// Priority: Checked items > Selected items
var itemsToDownload = listView.CheckedItems.Count > 0
    ? listView.CheckedItems.Cast<ListViewItem>().ToList()
    : listView.SelectedItems.Cast<ListViewItem>().ToList();
```

**Benefits**:
- If items are checked, download all checked items
- If no items are checked, download selected items (backward compatible)
- Supports both single-selection and multi-selection workflows

### 4. Enabled Local Folder Controls

- `txtLocalFolderPath`: Now enabled by default
- `btnBrowseLocalFolder`: Now enabled by default
- These controls work independently of device connection

## User Workflow

### Device Recordings
1. Connect to device and load recordings
2. Click "Select All" to check all recordings
3. Optionally uncheck specific recordings
4. Click "Download" to download all checked recordings

### Local Recordings
1. Browse and select local folder
2. Click "Refresh" to load local recordings
3. Click "Select All" to check all recordings
4. Optionally uncheck specific recordings
5. Click "Download" to copy all checked recordings

### File Conversion
1. Browse and select folder with .TS files
2. Click "Refresh Files" to load file list
3. Click "Select All" to check all files
4. Click "Select None" to uncheck all files
5. Click "Convert Selected" to convert checked files

## Technical Details

### Event Handlers
```csharp
private void BtnSelectAllRecordings_Click(object? sender, EventArgs e)
{
    foreach (ListViewItem item in listViewRecordings.Items)
    {
        item.Checked = true;
    }
}

private void BtnSelectAllLocalRecordings_Click(object? sender, EventArgs e)
{
    foreach (ListViewItem item in listViewLocalRecordings.Items)
    {
        item.Checked = true;
    }
}
```

### Button States

**Device Recordings - btnSelectAllRecordings**:
- Enabled: After successful recording list load
- Disabled: On disconnect or no connection

**Local Recordings - btnSelectAllLocalRecordings**:
- Enabled: After successful local recordings load
- Initial state: Not explicitly disabled

## Benefits

1. **Efficiency**: Quickly select multiple recordings without manual clicking
2. **Batch Operations**: Easy batch downloads/conversions
3. **Flexibility**: Supports both checked and selected items
4. **Consistency**: All three tabs now have similar selection capabilities
5. **User-Friendly**: Clear visual indication of selected items via checkboxes

## Files Modified

1. **E2ReccordingManager\Form1.Designer.cs**
   - Added `btnSelectAllRecordings` and `btnSelectAllLocalRecordings` declarations
   - Enabled CheckBoxes on `listViewRecordings` and `listViewLocalRecordings`
   - Added button controls to panels
   - Configured button properties and event handlers
   - Enabled `txtLocalFolderPath` and `btnBrowseLocalFolder`

2. **E2ReccordingManager\Form1.cs**
   - Added `BtnSelectAllRecordings_Click()` event handler
   - Added `BtnSelectAllLocalRecordings_Click()` event handler
   - Enhanced `BtnDownloadRecording_Click()` to support checked items
   - Enhanced `BtnDownloadLocalRecording_Click()` to support checked items
   - Updated `DisconnectFromDevice()` to disable Select All button
   - Updated `BtnRefreshRecordings_Click()` to enable Select All button
   - Updated `BtnRefreshLocalRecordings_Click()` to enable Select All button

## Usage Tips

1. **Select All + Individual Unchecking**: Use "Select All" then uncheck specific items you don't want
2. **Mixed Selection**: You can still select items without checking them for viewing details
3. **Download Priority**: Checked items take precedence over selected items for downloads
4. **Keyboard Shortcuts**: ListView still supports Ctrl+Click and Shift+Click for multi-selection

## Backward Compatibility

The enhanced download functionality maintains backward compatibility:
- If you select items without checking them, download will still work
- Existing workflows continue to function as before
- Checkboxes are purely additive functionality
