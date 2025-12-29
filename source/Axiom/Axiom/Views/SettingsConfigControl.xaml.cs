using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel;

namespace Axiom.Views
{
    public partial class SettingsConfigControl : UserControl
    {
        public SettingsConfigControl()
        {
            InitializeComponent();
        }

            /// <summary>
            /// Config Open Directory - Button
            /// </summary>
            private void btnConfigPath_Click(object sender, RoutedEventArgs e)
            {
              // Open Directory
              switch (VM.ConfigureView.ConfigPath_SelectedItem)
              {
                // AppData Local
                case "AppData Local":
                  MainWindow.ConfigDirectoryOpen(MainWindow.appDataLocalDir + @"Axiom UI\");
                  break;

                // AppData Roaming
                case "AppData Roaming":
                  MainWindow.ConfigDirectoryOpen(MainWindow.appDataRoamingDir + @"Axiom UI\");
                  break;

                // Documents
                case "Documents":
                  MainWindow.ConfigDirectoryOpen(MainWindow.documentsDir + @"Axiom UI\");
                  break;

                // App Root
                case "App Root":
                  Process.Start("explorer.exe", MainWindow.appRootDir);
                  break;
              }
            }

            /// <summary>
            /// CustomPresets Auto Path - Label Button
            /// </summary>
            private void btnCustomPresetsAuto_Click(object sender, RoutedEventArgs e)
            {
              // TODO: CustomPresetsAuto method not implemented
              //CustomPresetsAuto();
            }

            /// <summary>
            /// FFmpeg Auto Path - Button
            /// </summary>
            private void btnFFmpegAuto_Click(object sender, RoutedEventArgs e)
            {
              // Display Folder Path in Textbox
              VM.ConfigureView.FFmpegPath_Text = "<auto>";
            }

            /// <summary>
            /// FFplay Auto Path - Button
            /// </summary>
            private void btnFFplayAuto_Click(object sender, RoutedEventArgs e)
            {
              // Display Folder Path in Textbox
              VM.ConfigureView.FFplayPath_Text = "<auto>";
            }

            /// <summary>
            /// FFprobe Auto Path - Button
            /// </summary>
            private void btnFFprobeAuto_Click(object sender, RoutedEventArgs e)
            {
              // Display Folder Path in Textbox
              VM.ConfigureView.FFprobePath_Text = "<auto>";
            }

            /// <summary>
            /// Settings Default
            /// </summary>
            /// <remarks>
            /// Set all settings to their defaults.
            /// </remarks>
            private void btnSettingsDefault_Click(object sender, RoutedEventArgs e)
            {
              // Config
              // TODO: CustomPresetsAuto method not implemented
              //CustomPresetsAuto();
              VM.ConfigureView.FFmpegPath_Text = "<auto>";
              VM.ConfigureView.FFprobePath_Text = "<auto>";
              VM.ConfigureView.FFplayPath_Text = "<auto>";
              VM.ConfigureView.youtubedlPath_Text = "<auto>";
              VM.ConfigureView.LogCheckBox_IsChecked = false;
              VM.ConfigureView.LogPath_Text = Log.axiomLogDir;

              // Process
              VM.ConfigureView.Shell_SelectedItem = "CMD";
              VM.ConfigureView.ShellTitle_SelectedItem = "Disabled";
              VM.ConfigureView.ProcessPriority_SelectedItem = "Default";
              VM.ConfigureView.Threads_SelectedItem = "Optimal";

              // Input
              VM.ConfigureView.InputFileNameTokens_SelectedItem = "Keep";

              // Output
              VM.ConfigureView.OutputOverwrite_SelectedItem = "Always";
              VM.ConfigureView.OutputFileNameSpacing_SelectedItem = "Original";
              // TODO: Fix XAML compilation - lstvOutputNaming is in SettingsFileControl UserControl
              //lstvOutputNaming.SelectedIndex = -1;
              // TODO: OutputNamignDefaults method not implemented
              //OutputNamignDefaults();
            }

            /// <summary>
            /// youtubedl Auto Path - Button
            /// </summary>
            private void btnyoutubedlAuto_Click(object sender, RoutedEventArgs e)
            {
              // Display Folder Path in Textbox
              VM.ConfigureView.youtubedlPath_Text = "<auto>";
            }

            /// <summary>
            /// Reset Config - Button
            /// </summary>
            private void btnResetConfig_Click(object sender, RoutedEventArgs e)
            {
              // TODO: Implement config reset functionality
            }

            /// <summary>
            /// Config Path - Label Button
            /// </summary>
            private void lblConfigPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
            {
              if (e.LeftButton == MouseButtonState.Pressed)
              {
                switch (VM.ConfigureView.ConfigPath_SelectedItem)
                {
                  case "AppData Local":
                    MainWindow.ConfigDirectoryOpen(MainWindow.appDataLocalDir + @"Axiom UI\");
                    break;

                  case "AppData Roaming":
                    MainWindow.ConfigDirectoryOpen(MainWindow.appDataRoamingDir + @"Axiom UI\");
                    break;

                  case "Documents":
                    MainWindow.ConfigDirectoryOpen(MainWindow.documentsDir + @"Axiom UI\");
                    break;

                  case "App Root":
                    Process.Start("explorer.exe", MainWindow.appRootDir);
                    break;
                }
              }
            }

            /// <summary>
            /// Custom Presets Path - Label Button
            /// </summary>
            private void lblCustomPresetsPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
            {
              if (e.LeftButton == MouseButtonState.Pressed)
              {
                if (MainWindow.IsValidPath(VM.ConfigureView.CustomPresetsPath_Text))
                {
                  if (Directory.Exists(VM.ConfigureView.CustomPresetsPath_Text))
                  {
                    Process.Start("explorer.exe", VM.ConfigureView.CustomPresetsPath_Text);
                  }
                }
              }
            }

            /// <summary>
            /// FFmpeg Path - Label Button
            /// </summary>
            private void lblFFmpegPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
            {
              if (e.LeftButton == MouseButtonState.Pressed)
              {
                string path = VM.ConfigureView.FFmpegPath_Text;
                if (path != "<auto>" && MainWindow.IsValidPath(path))
                {
                  string directory = Path.GetDirectoryName(path);
                  if (Directory.Exists(directory))
                  {
                    Process.Start("explorer.exe", directory);
                  }
                }
              }
            }

            /// <summary>
            /// FFplay Path - Label Button
            /// </summary>
            private void lblFFplayPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
            {
              if (e.LeftButton == MouseButtonState.Pressed)
              {
                string path = VM.ConfigureView.FFplayPath_Text;
                if (path != "<auto>" && MainWindow.IsValidPath(path))
                {
                  string directory = Path.GetDirectoryName(path);
                  if (Directory.Exists(directory))
                  {
                    Process.Start("explorer.exe", directory);
                  }
                }
              }
            }

            /// <summary>
            /// FFprobe Path - Label Button
            /// </summary>
            private void lblFFprobePath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
            {
              if (e.LeftButton == MouseButtonState.Pressed)
              {
                string path = VM.ConfigureView.FFprobePath_Text;
                if (path != "<auto>" && MainWindow.IsValidPath(path))
                {
                  string directory = Path.GetDirectoryName(path);
                  if (Directory.Exists(directory))
                  {
                    Process.Start("explorer.exe", directory);
                  }
                }
              }
            }

            /// <summary>
            /// youtube-dl Path - Label Button
            /// </summary>
            private void lblyoutubedlPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
            {
              if (e.LeftButton == MouseButtonState.Pressed)
              {
                string path = VM.ConfigureView.youtubedlPath_Text;
                if (path != "<auto>" && MainWindow.IsValidPath(path))
                {
                  string directory = Path.GetDirectoryName(path);
                  if (Directory.Exists(directory))
                  {
                    Process.Start("explorer.exe", directory);
                  }
                }
              }
            }

            /// <summary>
            /// Custom Presets Path - PreviewDragOver
            /// </summary>
            private void tbxCustomPresetsPath_PreviewDragOver(object sender, DragEventArgs e)
            {
              if (e.Data.GetDataPresent(DataFormats.FileDrop))
              {
                e.Effects = DragDropEffects.Copy;
              }
              else
              {
                e.Effects = DragDropEffects.None;
              }
              e.Handled = true;
            }

            private void tbxCustomPresetsPath_PreviewDrop(object sender, DragEventArgs e)
            {
              var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];

              // If Path has file, extract Directory only
              if (Path.HasExtension(buffer.First()))
              {
                VM.ConfigureView.CustomPresetsPath_Text = Path.GetDirectoryName(buffer.First()).TrimEnd('\\') + @"\";
              }

              // Use Folder Path
              else
              {
                VM.ConfigureView.CustomPresetsPath_Text = buffer.First();
              }

              // -------------------------
              // Load Custom Presets
              // Refresh Presets ComboBox
              // -------------------------
              Profiles.Profiles.LoadCustomPresets();
            }

            /// <summary>
            /// Custom Presets Path - PreviewMouseDown
            /// </summary>
            private void tbxCustomPresetsPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
            {
              // Open folder browser dialog
              var dialog = new System.Windows.Forms.FolderBrowserDialog();
              dialog.SelectedPath = VM.ConfigureView.CustomPresetsPath_Text;

              System.Windows.Forms.DialogResult result = dialog.ShowDialog();

              if (result == System.Windows.Forms.DialogResult.OK)
              {
                VM.ConfigureView.CustomPresetsPath_Text = dialog.SelectedPath + @"\";

                // Load Custom Presets
                Profiles.Profiles.LoadCustomPresets();
              }
            }

            /// <summary>
            /// FFmpeg Path - PreviewDragOver
            /// </summary>
            private void tbxFFmpegPath_PreviewDragOver(object sender, DragEventArgs e)
            {
              if (e.Data.GetDataPresent(DataFormats.FileDrop))
              {
                e.Effects = DragDropEffects.Copy;
              }
              else
              {
                e.Effects = DragDropEffects.None;
              }
              e.Handled = true;
            }

            private void tbxFFmpegPath_PreviewDrop(object sender, DragEventArgs e)
            {
              var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
              VM.ConfigureView.FFmpegPath_Text = buffer.First();
            }

            /// <summary>
            /// FFmpeg Path - PreviewMouseDown
            /// </summary>
            private void tbxFFmpegPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
            {
              // Open file browser dialog
              var dialog = new Microsoft.Win32.OpenFileDialog();
              dialog.Filter = "Executables (*.exe)|*.exe";
              dialog.FileName = "ffmpeg.exe";

              bool? result = dialog.ShowDialog();

              if (result == true)
              {
                VM.ConfigureView.FFmpegPath_Text = dialog.FileName;
              }
            }

            /// <summary>
            /// FFplay Path - PreviewDragOver
            /// </summary>
            private void tbxFFplayPath_PreviewDragOver(object sender, DragEventArgs e)
            {
              if (e.Data.GetDataPresent(DataFormats.FileDrop))
              {
                e.Effects = DragDropEffects.Copy;
              }
              else
              {
                e.Effects = DragDropEffects.None;
              }
              e.Handled = true;
            }

            private void tbxFFplayPath_PreviewDrop(object sender, DragEventArgs e)
            {
              var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
              VM.ConfigureView.FFplayPath_Text = buffer.First();
            }

            /// <summary>
            /// FFplay Path - PreviewMouseDown
            /// </summary>
            private void tbxFFplayPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
            {
              // Open file browser dialog
              var dialog = new Microsoft.Win32.OpenFileDialog();
              dialog.Filter = "Executables (*.exe)|*.exe";
              dialog.FileName = "ffplay.exe";

              bool? result = dialog.ShowDialog();

              if (result == true)
              {
                VM.ConfigureView.FFplayPath_Text = dialog.FileName;
              }
            }

            /// <summary>
            /// FFprobe Path - PreviewDragOver
            /// </summary>
            private void tbxFFprobePath_PreviewDragOver(object sender, DragEventArgs e)
            {
              if (e.Data.GetDataPresent(DataFormats.FileDrop))
              {
                e.Effects = DragDropEffects.Copy;
              }
              else
              {
                e.Effects = DragDropEffects.None;
              }
              e.Handled = true;
            }

            private void tbxFFprobePath_PreviewDrop(object sender, DragEventArgs e)
            {
              var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
              VM.ConfigureView.FFprobePath_Text = buffer.First();
            }

            /// <summary>
            /// FFprobe Path - PreviewMouseDown
            /// </summary>
            private void tbxFFprobePath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
            {
              // Open file browser dialog
              var dialog = new Microsoft.Win32.OpenFileDialog();
              dialog.Filter = "Executables (*.exe)|*.exe";
              dialog.FileName = "ffprobe.exe";

              bool? result = dialog.ShowDialog();

              if (result == true)
              {
                VM.ConfigureView.FFprobePath_Text = dialog.FileName;
              }
            }

            /// <summary>
            /// youtube-dl Path - PreviewDragOver
            /// </summary>
            private void tbxyoutubedlPath_PreviewDragOver(object sender, DragEventArgs e)
            {
              if (e.Data.GetDataPresent(DataFormats.FileDrop))
              {
                e.Effects = DragDropEffects.Copy;
              }
              else
              {
                e.Effects = DragDropEffects.None;
              }
              e.Handled = true;
            }

            private void tbxyoutubedlPath_PreviewDrop(object sender, DragEventArgs e)
            {
              var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
              VM.ConfigureView.youtubedlPath_Text = buffer.First();
            }

            /// <summary>
            /// youtube-dl Path - PreviewMouseDown
            /// </summary>
            private void tbxyoutubedlPath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
            {
              // Open file browser dialog
              var dialog = new Microsoft.Win32.OpenFileDialog();
              dialog.Filter = "Executables (*.exe)|*.exe";
              dialog.FileName = "youtube-dl.exe";

              bool? result = dialog.ShowDialog();

              if (result == true)
              {
                VM.ConfigureView.youtubedlPath_Text = dialog.FileName;
              }
            }

            /// <summary>
            /// Updates Auto Check - Checked
            /// </summary>
            private void tglUpdateAutoCheck_Checked(object sender, RoutedEventArgs e)
            {
              // Update Toggle Text
              VM.ConfigureView.UpdateAutoCheck_Text = "On";
            }

            /// <summary>
            /// Updates Auto Check - Unchecked
            /// </summary>
            private void tglUpdateAutoCheck_Unchecked(object sender, RoutedEventArgs e)
            {
              // Update Toggle Text
              VM.ConfigureView.UpdateAutoCheck_Text = "Off";
            }

            /// <summary>
            /// Theme Select - ComboBox
            /// </summary>
            private void themeSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
              Controls.Configure.theme = VM.ConfigureView.Theme_SelectedItem;

              // Change Theme Resource
              App.Current.Resources.MergedDictionaries.Clear();
              App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
              {
                Source = new Uri("Themes/" + "Theme" + Controls.Configure.theme + ".xaml", UriKind.RelativeOrAbsolute)
              });
            }

    }
}
