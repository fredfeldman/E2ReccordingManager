# TS to MP4 Conversion - Quick Reference

## Quick Setup (3 Steps)

1. **Install FFmpeg**
   - Download from: https://ffmpeg.org/download.html
   - Extract and note the location of `ffmpeg.exe`

2. **Configure Settings**
   - Open **File → Settings**
   - Click **Browse (...)** next to FFmpeg Path
   - Select `ffmpeg.exe`
   - Click **Test** to verify
   - Check **Automatically convert .TS files to .MP4**
   - Click **OK**

3. **Download & Convert**
   - Select recordings
   - Click **Download**
   - Conversion starts automatically after download
   - Watch progress in **Conversion** tab

## Common Settings

### Recommended for Most Users
- **Quality**: Balanced
- **Max Bitrate**: 8 Mbps
- **Hardware Acceleration**: Enabled (if you have NVIDIA GPU)
- **Delete .TS files**: Personal preference

### High Quality / Large Files
- **Quality**: High
- **Max Bitrate**: 12-15 Mbps
- **Hardware Acceleration**: Enabled

### Small Files / Storage Limited
- **Quality**: Low
- **Max Bitrate**: 4-6 Mbps
- **Delete .TS files**: Enabled

## Keyboard Shortcuts

None specific to conversion, but useful:
- **Download Dialog**: ESC to cancel (or click Cancel button)
- **Settings Dialog**: Enter to save, ESC to cancel

## File Locations

- **Settings**: `%AppData%\E2ReccordingManager\settings.json`
- **Output Files**: Same location as selected download folder

## FFmpeg Test Command

To verify FFmpeg from command line:
```cmd
ffmpeg -version
```

To check NVENC support:
```cmd
ffmpeg -encoders | findstr nvenc
```

## Typical File Sizes (1 hour HD recording)

| Quality | Original .TS | Converted .MP4 | Savings |
|---------|-------------|----------------|---------|
| High    | 3-5 GB      | 2-3 GB         | 30-40%  |
| Balanced| 3-5 GB      | 1.5-2.5 GB     | 40-50%  |
| Low     | 3-5 GB      | 1-1.5 GB       | 60-70%  |

*Note: Actual sizes vary based on content complexity*

## Conversion Speed

| Method | Time for 1 hour video | CPU Usage |
|--------|----------------------|-----------|
| NVENC (Hardware) | 3-8 minutes | Low (~10-20%) |
| Software (libx264) | 15-30 minutes | High (80-100%) |

*Note: Times vary based on hardware and quality settings*

## Troubleshooting Quick Fixes

| Problem | Solution |
|---------|----------|
| FFmpeg not found | Set full path in Settings, test it |
| Conversion slow | Enable hardware acceleration |
| NVENC fails | Update GPU drivers or disable hardware accel |
| Out of space | Enable "Delete .TS files" or free up disk space |
| Poor quality | Use High quality preset, increase max bitrate |

## Command-Line Equivalent

What the app does (for reference):
```cmd
# Hardware (NVENC)
ffmpeg -i "recording.ts" -c:v h264_nvenc -preset p4 -cq 23 -b:v 0 -maxrate 8M -c:a copy -movflags +faststart -y "recording.mp4"

# Software
ffmpeg -i "recording.ts" -c:v libx264 -preset medium -crf 23 -maxrate 8M -c:a copy -movflags +faststart -y "recording.mp4"
```

## Best Practices

1. ✅ **Test FFmpeg** before first use
2. ✅ **Use NVENC** if you have NVIDIA GPU
3. ✅ **Start with Balanced** quality setting
4. ✅ **Keep .TS files** until you verify .MP4 quality
5. ✅ **Monitor first conversion** to ensure settings are good
6. ✅ **Ensure adequate disk space** (need space for both .TS and .MP4 during conversion)

## Support

For issues or questions:
- Check Debug output (if running from Visual Studio)
- Verify FFmpeg installation with Test button
- Ensure sufficient disk space
- Try disabling hardware acceleration
- Check that .TS file is not corrupted

## Related Documentation

- Full documentation: `TS_TO_MP4_CONVERSION.md`
- Download feature: `DOWNLOAD_PROGRESS_FEATURE.md`
- Settings: Accessible via File → Settings menu
