# TS to MP4 Conversion Feature

## Overview

The E2RecordingManager now includes optional automatic conversion of downloaded .TS (Transport Stream) files to .MP4 format using FFmpeg. This feature provides high-quality video conversion with hardware acceleration support and flexible quality settings.

## Features

### Automatic Conversion
- **Auto-Convert Option**: Downloaded .TS files can be automatically converted to .MP4 format after download completes
- **Optional TS Deletion**: Original .TS files can be automatically deleted after successful conversion
- **Batch Processing**: Multiple files are converted sequentially with progress tracking

### Hardware Acceleration
- **NVIDIA NVENC Support**: Uses h264_nvenc encoder when hardware acceleration is enabled
- **Fallback to Software**: Automatically falls back to libx264 (software encoding) if hardware acceleration is disabled
- **High Performance**: NVENC provides faster encoding with minimal CPU usage (requires NVIDIA GPU)

### Quality Settings

Three quality presets are available:

1. **High Quality**
   - Best video quality
   - Largest file size
   - CQ: 18 (NVENC) / CRF: 18 (software)
   - Preset: p6 (NVENC) / medium (software)

2. **Balanced Quality** (Default)
   - Good quality with moderate file size
   - CQ: 23 (NVENC) / CRF: 23 (software)
   - Preset: p4 (NVENC) / medium (software)

3. **Low Quality**
   - Lower quality for smaller files
   - CQ: 28 (NVENC) / CRF: 28 (software)
   - Preset: p2 (NVENC) / medium (software)

### Configurable Settings

- **FFmpeg Path**: Specify location of ffmpeg.exe (defaults to "ffmpeg" if in PATH)
- **Max Bitrate**: Set maximum bitrate in Mbps (1-50, default: 8)
- **Hardware Acceleration**: Enable/disable NVIDIA NVENC
- **Auto-Convert**: Enable/disable automatic conversion
- **Delete TS Files**: Enable/disable deletion of original .TS files after conversion

## User Interface

### Download Progress Dialog

The download progress dialog includes two tabs:

1. **Download Tab**
   - Shows download progress
   - Current file being downloaded
   - Overall progress (files completed / total files)
   - Individual file progress with bytes downloaded

2. **Conversion Tab** (Automatically switches after downloads complete)
   - Shows conversion progress
   - Current file being converted
   - Overall conversion progress (files completed / total files)
   - Individual file conversion progress with time elapsed

### Settings Dialog

Access settings via **File → Settings** menu:

- **FFmpeg Settings**
  - Browse button to locate ffmpeg.exe
  - Test button to verify FFmpeg installation
  - Path input field

- **Conversion Options**
  - Auto-convert checkbox
  - Delete .TS files after conversion checkbox
  - Hardware acceleration checkbox (NVIDIA NVENC)

- **Quality Settings**
  - Quality preset radio buttons (High/Balanced/Low)
  - Max bitrate numeric input
  - Informational text about presets

## How It Works

### Workflow

1. User selects recordings to download
2. Files are downloaded via HTTP from the Enigma2 device
3. If auto-convert is enabled:
   - Dialog switches to Conversion tab
   - Each .TS file is converted to .MP4
   - FFmpeg process is monitored for progress
   - Original .TS files are optionally deleted
4. Summary shows download and conversion statistics

### FFmpeg Command Structure

**With Hardware Acceleration (NVENC):**
```
ffmpeg -i "input.ts" -c:v h264_nvenc -preset p4 -cq 23 -b:v 0 -maxrate 8M -c:a copy -movflags +faststart -y "output.mp4"
```

**Software Encoding:**
```
ffmpeg -i "input.ts" -c:v libx264 -preset medium -crf 23 -maxrate 8M -c:a copy -movflags +faststart -y "output.mp4"
```

### Progress Tracking

- FFmpeg output is parsed in real-time
- Duration is determined from input file
- Current time is extracted from FFmpeg stderr
- Percentage calculated: (current_time / total_duration) × 100
- UI updated every 500ms to avoid excessive updates

## Setup Instructions

### Prerequisites

1. **FFmpeg Installation**
   - Download FFmpeg from https://ffmpeg.org/download.html
   - Extract to a known location
   - Either:
     - Add FFmpeg to system PATH, OR
     - Configure full path in Settings dialog

2. **Hardware Acceleration (Optional)**
   - NVIDIA GPU required for NVENC support
   - Install latest NVIDIA drivers
   - FFmpeg must be built with NVENC support

### Configuration

1. Open **File → Settings**
2. Navigate to **Conversion** tab
3. Configure FFmpeg path:
   - Click **Browse (...)** to select ffmpeg.exe
   - Click **Test** to verify installation
4. Set conversion options:
   - Check **Automatically convert .TS files to .MP4**
   - Optionally check **Delete .TS files after conversion**
   - Check **Use Hardware Acceleration** if you have NVIDIA GPU
5. Select quality preset (High/Balanced/Low)
6. Set max bitrate (recommended: 8 Mbps for HD content)
7. Click **OK** to save

## Technical Details

### Code Structure

**AppSettings.cs** - Stores conversion preferences:
- FFmpegPath
- AutoConvertToMp4
- DeleteTsAfterConversion
- ConversionQuality
- MaxBitrateMbps
- UseHardwareAcceleration

**Form1.cs** - Main conversion logic:
- `ConvertDownloadedFiles()` - Orchestrates batch conversion
- `ConvertToMp4()` - Converts single file
- `BuildFFmpegArguments()` - Constructs FFmpeg command
- `GetVideoDuration()` - Extracts video duration
- `TrackConversionProgress()` - Monitors FFmpeg process

**DownloadProgressDialog.cs** - UI for progress:
- `UpdateConversionStatus()` - Updates status text
- `UpdateConversionFile()` - Shows current file name
- `UpdateConversionProgress()` - Updates overall progress bar
- `UpdateConversionFileProgress()` - Updates per-file progress
- `SwitchToConversionTab()` - Switches to conversion view

**SettingsDialog.cs** - Configuration UI:
- Load/save conversion settings
- FFmpeg path selection and testing
- Quality preset selection

### Error Handling

- Missing FFmpeg: Conversion silently fails, .TS file retained
- FFmpeg errors: Exit code checked, incomplete .MP4 deleted
- Conversion errors logged to Debug output
- Failed conversions counted in summary

### Performance

- **NVENC**: ~5-10x faster than software encoding
- **CPU Usage**: NVENC minimal, software encoding high
- **Quality**: NVENC comparable to software at similar settings
- **File Size**: Typically 60-80% smaller than original .TS

## Troubleshooting

### FFmpeg Not Found
- Verify FFmpeg is installed
- Check FFmpeg path in settings
- Use **Test** button to verify
- Check FFmpeg is in system PATH

### Hardware Acceleration Fails
- Verify NVIDIA GPU is present
- Update NVIDIA drivers
- Check FFmpeg has NVENC support: `ffmpeg -encoders | findstr nvenc`
- Disable hardware acceleration in settings

### Conversion Fails
- Check Debug output for error messages
- Verify input .TS file is valid
- Ensure sufficient disk space
- Try software encoding instead of NVENC

### Slow Conversion
- Enable hardware acceleration (NVENC)
- Lower quality preset (Balanced or Low)
- Reduce max bitrate
- Close other applications

## Future Enhancements

Possible future improvements:
- Additional codec support (HEVC, AV1)
- AMD/Intel hardware acceleration
- Audio format conversion options
- Subtitle extraction/embedding
- Custom FFmpeg parameter support
- Conversion queue management
- Parallel conversion support

## Credits

This conversion feature is implemented using:
- **FFmpeg**: Cross-platform video processing
- **NVIDIA NVENC**: Hardware-accelerated encoding
- Based on implementation from Enigma2Manager project

## See Also

- FFmpeg Documentation: https://ffmpeg.org/documentation.html
- NVENC Documentation: https://developer.nvidia.com/nvidia-video-codec-sdk
- H.264 Encoding Guide: https://trac.ffmpeg.org/wiki/Encode/H.264
