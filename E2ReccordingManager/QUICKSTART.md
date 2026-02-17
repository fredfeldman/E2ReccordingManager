# Enigma2 Recording Manager - Quick Start Guide

## Getting Started

### Step 1: Launch the Application
- Run `E2ReccordingManager.exe`
- You'll see the main window with a toolbar and empty recording list

### Step 2: Connect to Your Device
1. Click the **Connect** button in the toolbar
2. In the connection dialog, enter:
   ```
   Host: 192.168.1.xxx (your device IP)
   Port: 80
   Username: root
   Password: (your device password)
   ```
3. Click **Connect**
4. You should see "Connected to [your IP]" in the toolbar

### Step 3: Load Recordings
1. Click the **Refresh** button
2. Wait while the application loads your recordings
3. A status dialog will show progress
4. Recordings will appear in the list

### Step 4: Manage Your Recordings

#### To View Details:
- Click on any recording in the list
- Details appear in the panel below

#### To Download:
1. Select one or more recordings
2. Click **Download** button
3. Choose destination folder
4. Wait for download to complete
5. Optional: Check "Remove after download" to auto-delete

#### To Delete:
1. Select one or more recordings
2. Click **Delete** button
3. Confirm the deletion
4. List will refresh automatically

## Main Window Layout

```
┌─────────────────────────────────────────────────────┐
│ [Connect] [Disconnect] | Not Connected              │ ← Toolbar
├─────────────────────────────────────────────────────┤
│ Title        | Channel  | Date       | Duration ... │ ← Column Headers
│─────────────────────────────────────────────────────│
│ Recording 1  | BBC One  | 2024-01-15 | 1h 30m  ...│
│ Recording 2  | ITV      | 2024-01-14 | 45m     ...│ ← Recording List
│ ...                                                  │
├─────────────────────────────────────────────────────┤
│ Description:                                         │
│ ┌─────────────────────────────────────────────────┐ │
│ │ Title: Recording Name                           │ │
│ │ Channel: BBC One                                │ │ ← Details Panel
│ │ Date: 2024-01-15 20:00                         │ │
│ │ Duration: 1h 30m                               │ │
│ └─────────────────────────────────────────────────┘ │
├─────────────────────────────────────────────────────┤
│ [Refresh] [Download] [Delete] □ Remove after dl    │ ← Controls
│ [================================] 75%              │ ← Progress Bar
├─────────────────────────────────────────────────────┤
│ Ready                                               │ ← Status Bar
└─────────────────────────────────────────────────────┘
```

## Keyboard Shortcuts

- **Enter**: Connect (when in connection dialog)
- **Delete**: Delete selected recordings
- **F5**: Refresh recording list (when implemented)
- **Ctrl+A**: Select all recordings

## Right-Click Menu

Right-click on any recording to:
- View Details
- Download
- Delete

## Status Indicators

- **Green text**: Recording has EPG (Electronic Program Guide) data
- **Progress bar**: Shows download progress
- **Status bar**: Shows current operation status

## Tips & Tricks

1. **Multiple Selection**: 
   - Hold Ctrl and click to select multiple recordings
   - Hold Shift and click to select a range

2. **Quick Download**:
   - Right-click on recording
   - Select "Download"

3. **Safe Delete**:
   - Always uses confirmation dialog
   - Can't be undone!

4. **Auto-cleanup**:
   - Enable "Remove after download"
   - Recordings deleted only after successful download

5. **Reconnection**:
   - If connection lost, click Disconnect then Connect
   - Previous settings are remembered

## Common Operations

### Download All Recordings
1. Click on first recording
2. Press Ctrl+A to select all
3. Click Download
4. Choose folder
5. Wait for completion

### Clean Up Old Recordings
1. Sort by date (click Date column)
2. Select old recordings
3. Click Delete
4. Confirm

### Find Specific Recording
1. Look through the list
2. Check details panel for more info
3. Use tooltip (hover over recording) for description

## Troubleshooting

### "Please connect to device first"
- Click Connect button
- Enter correct device details
- Make sure device is on and accessible

### "No recordings found"
- Check if recordings exist on device
- Verify recording path is standard
- Click Refresh to reload

### Download fails
- Check disk space
- Verify network connection
- Check file permissions
- Try downloading individually

### Can't connect
- Verify IP address
- Check port number (usually 80)
- Confirm OpenWebif is enabled
- Check firewall settings

## Support

For issues or questions:
- Check README.md for detailed documentation
- Check IMPLEMENTATION.md for technical details
- Verify OpenWebif is working (test in browser: http://device-ip/)

## Device Requirements

Your Enigma2 device must have:
- OpenWebif installed and enabled
- Network connectivity
- Web interface accessible
- Standard recording path (/hdd/movie/)

## Safety Notes

⚠️ **Important:**
- Delete operations cannot be undone
- Always confirm before deleting
- Keep backups of important recordings
- Test download before enabling auto-delete
- Ensure stable network during operations
