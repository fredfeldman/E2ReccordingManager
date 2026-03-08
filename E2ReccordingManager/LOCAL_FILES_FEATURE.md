# Local Files Feature

## Overview
The Local Files feature allows you to include recording files from a local folder in the Device Recordings tab, alongside actual device recordings. This is useful for files that have already been transferred from the device via FTP.

## Key Features

### 1. **Include Local Files**
- Enable the "Include local files" checkbox in the Device Recordings tab
- Browse and select a folder containing local recording files (.ts files)
- Local files will appear in the recordings list with a light blue background
- Local files are labeled as "Local" in the Channel column

### 2. **EIT File Support**
- If EIT files (.eit) are present in the local folder, they will be automatically loaded
- EIT data is used for renaming just like device recordings
- Files with EIT data are displayed in green color

### 3. **Download/Rename Operation**
- For local files, the "Download" button performs a copy and rename operation
- No FTP transfer is required since files are already local
- The rename functionality based on EIT data works the same as device files
- Both .ts and .eit files are copied to the destination folder

### 4. **File Management**
- Local files are displayed with their file size, date, and duration (if available)
- Delete button is disabled for local files (only device files can be deleted)
- "Remove after download" option is ignored for local files

## How to Use

### Step 1: Enable Local Files
1. Open the Device Recordings tab
2. Check the "Include local files" checkbox
3. The local folder controls will become enabled

### Step 2: Select Local Folder
1. Click the "..." browse button next to the Local Folder field
2. Select the folder containing your .ts recording files
3. The folder path will be saved in settings for future use

### Step 3: Refresh Recordings
1. Click the "Refresh" button
2. The list will show both device recordings (if connected) and local files
3. Local files have a light blue background to distinguish them

### Step 4: Download/Rename Local Files
1. Select one or more local files from the list
2. Click the "Download" button
3. Choose a destination folder
4. Files will be copied and renamed based on EIT data (if "Rename using EIT file data" is checked)
5. Both .ts and .eit files are copied to the destination

## Visual Indicators

- **Light Blue Background**: Indicates a local file
- **Green Text**: File has EIT/EPG data available
- **Source: Local File**: Shown in the details pane for local files
- **Delete Button**: Disabled when local files are selected

## Settings Persistence

The following settings are automatically saved:
- Include local files (enabled/disabled)
- Local folder path
- These settings are remembered between sessions

## Use Cases

1. **Previously Downloaded Files**: Include files that were already downloaded via FTP in an earlier session
2. **Bulk Renaming**: Use the EIT-based rename feature on files that are already local
3. **Organizing Archives**: Work with both device and local files in a single interface
4. **Backup Management**: Include files from backup locations

## Technical Details

- Supported file format: .ts (MPEG Transport Stream)
- Companion files: .eit (Electronic Information Table)
- Local files are read-only in the device recordings view
- File copying includes progress tracking
- EIT parsing works the same for local and device files

## Limitations

- Local files cannot be deleted from this interface
- Only .ts files are displayed
- Folder scanning is non-recursive (only top-level files)
- Local files don't support the "Remove after download" option

## Tips

- Keep .eit files alongside .ts files for best results
- Use the same folder structure as the device for consistency
- Local files can be mixed with device recordings in the same view
- The feature works without connecting to a device
