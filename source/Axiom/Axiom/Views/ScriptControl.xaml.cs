using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ViewModel;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Axiom.Views
{
  public partial class ScriptControl : UserControl
  {
    public ScriptControl()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Script View Drag and Drop
    /// </summary>
    private void tbxScriptView_PreviewDragOver(object sender, DragEventArgs e)
    {
      try
      {
        e.Handled = true;
        e.Effects = DragDropEffects.Copy;
      }
      catch (IOException ex)
      {
        MessageBox.Show(ex.ToString(),
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
      }
    }

    private void tbxScriptView_PreviewDrop(object sender, DragEventArgs e)
    {
      try
      {
        var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];

        if (buffer != null && buffer.Length == 0) // prevents crash and drag and dropping in-scriptview text
        {
          string file = buffer.First();
          string ext = Path.GetExtension(file);

          // Only accept txt files
          if (ext == ".txt")
          {
            VM.MainView.ScriptView_Text = File.ReadAllText(file);
          }
        }
      }
      catch (IOException ex)
      {
        MessageBox.Show(ex.ToString(),
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
      }
    }

    /// <summary>
    /// Script - Button
    /// </summary>
    private void btnScript_Click(object sender, RoutedEventArgs e)
    {
      // Call MainWindow method
      MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
      if (mainWindow != null)
      {
        mainWindow.ScriptButtonAsync();
      }
    }

    /// <summary>
    /// Load Script Button
    /// </summary>
    private void btnScriptLoad_Click(object sender, RoutedEventArgs e)
    {
      Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
      {
        Title = "Browse Script Text Files",
        CheckFileExists = true,
        CheckPathExists = true,
        DefaultExt = "txt",
        Filter = "txt files (*.txt)|*.txt",
        FilterIndex = 2,
        RestoreDirectory = true,
        ReadOnlyChecked = true,
        ShowReadOnly = true
      };

      if (openFileDialog.ShowDialog() == true)
      {
        try
        {
          VM.MainView.ScriptView_Text = File.ReadAllText(openFileDialog.FileName);
        }
        catch (IOException ex)
        {
          MessageBox.Show(ex.ToString(),
                          "Error",
                          MessageBoxButton.OK,
                          MessageBoxImage.Error);
        }
      }
    }

    /// <summary>
    /// Save Script Button
    /// </summary>
    private void btnScriptSave_Click(object sender, RoutedEventArgs e)
    {
      Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog
      {
        Title = "Save Script Text File",
        RestoreDirectory = true,
        DefaultExt = "txt",
        Filter = "Text file (*.txt)|*.txt",
        FilterIndex = 2,
        FileName = "Script"
      };

      if (saveFile.ShowDialog() == true)
      {
        try
        {
          File.WriteAllText(saveFile.FileName, VM.MainView.ScriptView_Text, Encoding.Unicode);
        }
        catch (IOException ex)
        {
          MessageBox.Show(ex.ToString(),
                          "Error",
                          MessageBoxButton.OK,
                          MessageBoxImage.Error);
        }
      }
    }

    /// <summary>
    /// Copy All Button
    /// </summary>
    private void btnScriptCopy_Click(object sender, RoutedEventArgs e)
    {
      if (!string.IsNullOrWhiteSpace(VM.MainView.ScriptView_Text))
      {
        Clipboard.SetText(VM.MainView.ScriptView_Text, TextDataFormat.UnicodeText);
      }
    }

    /// <summary>
    /// Clear Button
    /// </summary>
    private void btnScriptClear_Click(object sender, RoutedEventArgs e)
    {
      Controls.ScriptView.ClearScriptView();
    }

    /// <summary>
    /// Sort Button
    /// </summary>
    private void btnScriptSort_Click(object sender, RoutedEventArgs e)
    {
      MainWindow.scriptText = VM.MainView.ScriptView_Text; // Prevents ScriptView Flicker
      MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
      if (mainWindow != null)
      {
        MainWindow.Sort();
      }
    }

    /// <summary>
    /// Run Button
    /// </summary>
    private void btnScriptRun_Click(object sender, RoutedEventArgs e)
    {
      if (!string.IsNullOrWhiteSpace(VM.MainView.ScriptView_Text))
      {
        // -------------------------
        // Use Arguments from Script TextBox
        // -------------------------
        MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        if (mainWindow != null)
        {
          Generate.FFmpeg.ffmpegArgs = MainWindow.ReplaceLineBreaksWithSpaces(
                                  VM.MainView.ScriptView_Text
                              );

          // -------------------------
          // Start FFmpeg
          // -------------------------
          Encode.FFmpeg.FFmpegStart(Generate.FFmpeg.ffmpegArgs);

          // -------------------------
          // Create output.log
          // -------------------------
          Log.CreateOutputLog(mainWindow);
        }
      }
    }
  }
}
