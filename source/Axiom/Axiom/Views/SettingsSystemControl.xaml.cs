using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel;

namespace Axiom.Views
{
    public partial class SettingsSystemControl : UserControl
    {
        public SettingsSystemControl()
        {
            InitializeComponent();
        }

            /// <summary>
            /// Log Auto Path - Button
            /// </summary>
            private void btnLogPathAuto_Click(object sender, RoutedEventArgs e)
            {
              // Uncheck Log Checkbox
              VM.ConfigureView.LogCheckBox_IsChecked = false;

              // Clear Path in Textbox
              VM.ConfigureView.LogPath_Text = Log.axiomLogDir;
            }

            /// <summary>
            /// Process Priority Default
            /// </summary>
            private void btnProcessPriorityDefault_Click(object sender, RoutedEventArgs e)
            {
              VM.ConfigureView.ProcessPriority_SelectedItem = "Default";
            }

            /// <summary>
            /// Shell
            /// </summary>
            private void btnShellDefault_Click(object sender, RoutedEventArgs e)
            {
              VM.ConfigureView.Shell_SelectedItem = "CMD";
            }

            /// <summary>
            /// Shell Title Default
            /// </summary>
            private void btnShellTitleDefault_Click(object sender, RoutedEventArgs e)
            {
              VM.ConfigureView.ShellTitle_SelectedItem = "Disabled";
            }

            /// <summary>
            /// Threads Default
            /// </summary>
            private void btnThreadsDefault_Click(object sender, RoutedEventArgs e)
            {
              VM.ConfigureView.Threads_SelectedItem = "Optimal";
            }

            /// <summary>
            /// Shell Title - ComboBox
            /// </summary>
            private void cboShellTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {

            }

            /// <summary>
            /// Shell - ComboBox
            /// </summary>
            private void cboShell_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
              // -------------------------
              // Re-Set YouTube-DL File Name in the Output TextBox
              // -------------------------
              // Without it, on cboShell_SelectionChanged,
              // in Generate.FFmpeg.YouTubeDL.Generate_FFmpegArgs(),
              // the outputFileName gets stuck with the old value
              // because it reads it from VM.MainView.Output_Text
              if (!string.IsNullOrWhiteSpace(VM.MainView.Output_Text))
              {
                switch (VM.ConfigureView.Shell_SelectedItem)
                {
                  // CMD
                  case "CMD":
                    VM.MainView.Output_Text = VM.MainView.Output_Text.Replace("$name", "%f"); // eg. C:\Output Folder\$f.mp4
                    break;

                  // PowerShell
                  case "PowerShell":
                    VM.MainView.Output_Text = VM.MainView.Output_Text.Replace("%f", "$name"); // eg. C:\Output Folder\$name.mp4
                    break;
                }
              }
            }

            /// <summary>
            /// Log Checkbox - Checked
            /// </summary>
            private void cbxLog_Checked(object sender, RoutedEventArgs e)
            {
              VM.ConfigureView.LogPath_IsEnabled = true;
            }

            /// <summary>
            /// Log Checkbox - Unchecked
            /// </summary>
            private void cbxLog_Unchecked(object sender, RoutedEventArgs e)
            {
              VM.ConfigureView.LogPath_IsEnabled = false;
            }

            private void tbxLogPath_PreviewDrop(object sender, DragEventArgs e)
            {
              var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];

              // If Path has file, extract Directory only
              if (Path.HasExtension(buffer.First()))
              {
                VM.ConfigureView.LogPath_Text = Path.GetDirectoryName(buffer.First()).TrimEnd('\\') + @"\";
              }

              // Use Folder Path
              else
              {
                VM.ConfigureView.LogPath_Text = buffer.First();
              }
            }

            private void threadSelect_KeyDown(object sender, KeyEventArgs e)
            {
              // Only allow Numbers and Backspace
              MainWindow.Allow_Only_Number_Keys(e);
            }

            /// <summary>
            /// Threads - ComboBox
            /// </summary>
            private void threadSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
              // Set the threads to pass to MainWindow
              Controls.Configure.threads = VM.ConfigureView.Threads_SelectedItem;
            }

    }
}
