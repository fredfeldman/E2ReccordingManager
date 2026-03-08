# Enigma2 Recording Manager

A Windows Forms application for managing recordings on Enigma2-based satellite receivers (like Dreambox, VU+, etc.) via the OpenWebif API.

## Features

- **Multiple Device Profiles**: Save and manage connection profiles for multiple Enigma2 devices
- **Persistent Settings**: Your connection details are saved and automatically loaded
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

### Managing Connection Profiles

1. Click the **Connect** button in the toolbar
2. In the connection dialog, you'll see two sections:
   - **Profile Management**: Manage saved connection profiles
   - **Connection Settings**: Enter device connection details

#### Creating a New Profile

1. In the "Profile Name" field, enter a name for your device (e.g., "Living Room", "Bedroom", "VU+ Solo")
2. Enter your device details in the Connection Settings section:
   - **Host**: IP address or hostname of your Enigma2 device
   - **Port**: Usually 80 (default)
   - **Username**: Usually "root" (default)
   - **Password**: Your device's web interface password
3. Click **Save Profile** to save these settings
4. Click **Connect** to connect to the device

#### Using Saved Profiles

1. Click the **Connect** button
2. In the "Load Profile" dropdown, select your saved profile
3. The connection settings will automatically load
4. Click **Connect**

#### Managing Multiple Devices

- Save a separate profile for each of your Enigma2 devices
- Switch between devices by selecting different profiles
- Edit existing profiles by selecting them, modifying the settings, and clicking Save Profile
- Delete profiles you no longer need with the **Delete Profile** button

#### Last Used Profile

- The application automatically remembers your last used profile
- When you open the connection dialog, it will pre-load the last profile you connected to

### Managing Recordings

1. After connecting, click the **Refresh** button to load the list of recordings
2. The application will:
   - Fetch all recordings from your device
   - **Automatically download and parse EIT files** for EPG metadata
   - Display recordings with enhanced information
3. Select one or more recordings from the list
4. Use the buttons to:
   - **Download**: Save recordings to your local computer
   - **Delete**: Remove recordings from your device
   - **View Details**: See full recording information in the details panel

### Understanding EIT/EPG Data

**What are EIT files?**
- EIT (Event Information Table) files contain EPG (Electronic Program Guide) metadata
- Created automatically by Enigma2 when recording
- Stored alongside recording files with `.eit` extension
- Contain detailed program information

**How the application uses EIT data:**
- Automatically downloads EIT files when refreshing recordings
- Parses EPG metadata including:
  - Enhanced program titles
  - Short descriptions
  - Extended descriptions with full details
  - Accurate start times
  - Program durations
- Recordings with EPG data are shown in **dark green** in the list
- Hover over recordings to see extended descriptions in tooltips

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

**Zero recordings with EPG data:**
- Check if EIT files exist alongside recordings on your device
- EIT files should have the same name as recordings with `.eit` extension
- Check the Output window in Visual Studio for detailed EIT parsing logs
- Some older recordings may not have EIT files
- Timer recordings created without EPG data won't have EIT files

**Download fails:**
- Ensure sufficient disk space on your computer
- Check network connectivity during download
- Verify file permissions in the destination folder
