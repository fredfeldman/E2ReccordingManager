# Enigma2 Recording Manager

A Windows Forms application for managing recordings on Enigma2-based satellite receivers (like Dreambox, VU+, etc.) via the OpenWebif API.

## Features

- **Connect to Enigma2 Devices**: Connect to your satellite receiver using HTTP/OpenWebif interface
- **View Recordings**: Browse all recordings stored on your device with detailed information
- **Download Recordings**: Download recordings from your device to your local computer
  - Single or batch download support
  - Optional automatic deletion after download
- **Delete Recordings**: Remove recordings from your device
- **Recording Details**: View comprehensive information including:
  - Title, Channel, Date, Duration, File Size
  - EPG (Electronic Program Guide) data when available
  - Full descriptions

## Requirements

- .NET 8.0 or later
- Enigma2 device with OpenWebif installed and enabled
- Network connectivity to your Enigma2 device

## Usage

### Connecting to Your Device

1. Click the **Connect** button in the toolbar
2. Enter your device details:
   - **Host**: IP address or hostname of your Enigma2 device
   - **Port**: Usually 80 (default)
   - **Username**: Usually "root" (default)
   - **Password**: Your device's web interface password
3. Click **Connect**

### Managing Recordings

1. After connecting, click the **Refresh** button to load the list of recordings
2. Select one or more recordings from the list
3. Use the buttons to:
   - **Download**: Save recordings to your local computer
   - **Delete**: Remove recordings from your device
   - **View Details**: See full recording information in the details panel

### Tips

- Recordings with EPG data are displayed in dark green
- Hover over recordings to see extended descriptions (if available)
- Use "Remove after download" option to automatically delete recordings after successful download
- The details panel shows comprehensive information about the selected recording

## Based On

This application was created using the Recording tab from the Enigma2Manager project as a template, adapted to create a standalone recording management tool.

## Connection Settings

The application connects to your Enigma2 device using the OpenWebif API. Make sure:
- OpenWebif is installed and running on your device
- The web interface is accessible from your computer
- Firewall settings allow connections on the configured port

## Troubleshooting

**Can't connect to device:**
- Verify the IP address and port are correct
- Check that OpenWebif is enabled on your device
- Ensure your computer can ping the device
- Verify firewall settings

**No recordings shown:**
- Click Refresh to reload the list
- Check that recordings exist on your device
- Verify the recording path is standard (/hdd/movie/)

**Download fails:**
- Ensure sufficient disk space on your computer
- Check network connectivity during download
- Verify file permissions in the destination folder
