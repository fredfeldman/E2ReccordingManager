using E2ReccordingManager.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace E2ReccordingManager.Dialogs
{
    public partial class SettingsDialog : Form
    {
        private readonly AppSettings settings;

        public SettingsDialog(AppSettings settings)
        {
            this.settings = settings;
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            txtFFmpegPath.Text = settings.FFmpegPath;
            checkBoxAutoConvert.Checked = settings.AutoConvertToMp4;
            checkBoxDeleteAfterConversion.Checked = settings.DeleteTsAfterConversion;
            checkBoxUseHardwareAcceleration.Checked = settings.UseHardwareAcceleration;
            numericMaxBitrate.Value = settings.MaxBitrateMbps;

            switch (settings.ConversionQuality)
            {
                case "High":
                    radioHigh.Checked = true;
                    break;
                case "Low":
                    radioLow.Checked = true;
                    break;
                default:
                    radioBalanced.Checked = true;
                    break;
            }
        }

        private void BtnBrowseFFmpeg_Click(object? sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Filter = "FFmpeg Executable|ffmpeg.exe|All Files|*.*",
                Title = "Select FFmpeg Executable"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFFmpegPath.Text = openFileDialog.FileName;
            }
        }

        private void BtnTestFFmpeg_Click(object? sender, EventArgs e)
        {
            var ffmpegPath = txtFFmpegPath.Text;

            if (string.IsNullOrWhiteSpace(ffmpegPath))
            {
                MessageBox.Show("Please specify FFmpeg path first.", "Test FFmpeg",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var processInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = "-version",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                using var process = System.Diagnostics.Process.Start(processInfo);
                if (process != null)
                {
                    var output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        var firstLine = output.Split('\n')[0];
                        MessageBox.Show($"FFmpeg found!\n\n{firstLine}", "Test FFmpeg",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("FFmpeg test failed.", "Test FFmpeg",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error testing FFmpeg:\n\n{ex.Message}", "Test FFmpeg",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnOK_Click(object? sender, EventArgs e)
        {
            settings.FFmpegPath = txtFFmpegPath.Text;
            settings.AutoConvertToMp4 = checkBoxAutoConvert.Checked;
            settings.DeleteTsAfterConversion = checkBoxDeleteAfterConversion.Checked;
            settings.UseHardwareAcceleration = checkBoxUseHardwareAcceleration.Checked;
            settings.MaxBitrateMbps = (int)numericMaxBitrate.Value;

            if (radioHigh.Checked)
                settings.ConversionQuality = "High";
            else if (radioLow.Checked)
                settings.ConversionQuality = "Low";
            else
                settings.ConversionQuality = "Balanced";

            try
            {
                settings.Save();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings:\n\n{ex.Message}", "Save Settings",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
