using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel;

namespace Axiom.Views
{
  public partial class InputOutputControl : UserControl
  {
    public InputOutputControl()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Input TextBox Clear - Button
    /// </summary>
    private void btnInputClear_Click(object sender, RoutedEventArgs e)
    {
      VM.MainView.Input_Text = string.Empty;

      inputDir = string.Empty;
      inputFileName = string.Empty;
      inputExt = string.Empty;
      input = string.Empty;
    }

    /// <summary>
    /// Output TetBox Clear - Button
    /// </summary>
    private void btnOutputClear_Click(object sender, RoutedEventArgs e)
    {
      VM.MainView.Output_Text = string.Empty;

      outputDir = string.Empty;
      outputFileName_Original = string.Empty;
      outputFileName_Tokens = string.Empty;
      outputFileName = string.Empty;
      outputExt = string.Empty;
      output = string.Empty;
    }

    /// <summary>
    /// Open Input Folder - Button
    /// </summary>
    private void openLocationInput_Click(object sender, RoutedEventArgs e)
    {
      if (IsValidPath(inputDir))
      {
        if (Directory.Exists(@inputDir))
        {
          Process.Start("explorer.exe", @inputDir);
        }
      }
    }

    /// <summary>
    /// Open Output Folder - Button
    /// </summary>
    private void openLocationOutput_Click(object sender, RoutedEventArgs e)
    {
      if (IsValidPath(outputDir))
      {
        if (Directory.Exists(@outputDir))
        {
          Process.Start("explorer.exe", @outputDir);
        }
      }
    }

    private void tbxInput_PreviewDrop(object sender, DragEventArgs e)
    {
      try
      {
        var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
        VM.MainView.Input_Text = buffer.First();

        // Set Input Dir, Name, Ext
        inputDir = Path.GetDirectoryName(VM.MainView.Input_Text).TrimEnd('\\') + @"\";
        inputFileName = Path.GetFileNameWithoutExtension(VM.MainView.Input_Text);
        inputExt = Path.GetExtension(VM.MainView.Input_Text);

        // Clear Output TextBox
        VM.MainView.Output_Text = string.Empty;

        // -------------------------
        // Set Video and AudioCodec Combobox to "Copy" if 
        // Input File Extension is Same as Output File Extension 
        // and Quality is Auto
        // -------------------------
        //VideoControls.AutoCopyVideoCodec("input");
        //SubtitleControls.AutoCopySubtitleCodec("input");
        //AudioControls.AutoCopyAudioCodec("input");
      }
      catch (IOException ex)
      {
        MessageBox.Show(ex.ToString(),
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
      }
    }

    private void tbxOutput_PreviewDrop(object sender, DragEventArgs e)
    {
      try
      {
        var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];
        VM.MainView.Output_Text = buffer.First();

        // Save Output File Name
        outputFileName_Original = TokenRemover(Path.GetFileNameWithoutExtension(VM.MainView.Output_Text));
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
    /// Output Textbox - KeyUp
    /// </summary>
    private void tbxOutput_PreviewKeyUp(object sender, KeyEventArgs e)
    {
      outputFileName_Original = TokenRemover(Path.GetFileNameWithoutExtension(VM.MainView.Output_Text));
    }

    /// <summary>
    /// Output Textbox - TextChanged
    /// </summary>
    private void tbxOutput_TextChanged(object sender, TextChangedEventArgs e)
    {
      // Remove stray slash if closed out early
      if (VM.MainView.Output_Text == "\\")
      {
        VM.MainView.Output_Text = string.Empty;
      }

      // -------------------------
      // Enable / Disable "Open Output Location" Button
      // -------------------------
      if (!string.IsNullOrWhiteSpace(VM.MainView.Output_Text) && // null check
          IsValidPath(VM.MainView.Output_Text) == true && // Detect Invalid Characters
          Path.IsPathRooted(VM.MainView.Output_Text) == true // TrimEnd('\\') + @"\" is adding a backslash to 
                                                             // Iput text 'http' until it is detected as Web URL
          )
      {
        bool exists = Directory.Exists(Path.GetDirectoryName(VM.MainView.Output_Text));

        // Path exists
        if (exists)
        {
          VM.MainView.Output_Location_IsEnabled = true;
        }
        // Path does not exist
        else
        {
          VM.MainView.Output_Location_IsEnabled = false;
        }
      }
      // Disable Button for Web URL
      else
      {
        //VM.MainView.Output_Clear_IsEnabled = false;
        VM.MainView.Output_Location_IsEnabled = false;
      }

      // -------------------------
      // Enable / Disable "Output Clear" Button
      // -------------------------
      // Disable
      if (string.IsNullOrWhiteSpace(VM.MainView.Output_Text))
      {
        VM.MainView.Output_Clear_IsEnabled = false;
      }
      // Enable
      else
      {
        VM.MainView.Output_Clear_IsEnabled = true;
      }

      // -------------------------
      // Set Output
      // -------------------------
      if (VM.MainView.BatchExtension_IsEnabled == true)
      {
        // e.g. C:\Output\Path\
        if (IsValidPath(VM.MainView.Output_Text) == true)
        {
          outputDir = Path.GetDirectoryName(VM.MainView.Output_Text);
          if (!string.IsNullOrWhiteSpace(outputDir))
          {
            outputDir = outputDir.TrimEnd('\\') + @"\";
          }
        }
        else
        {
          outputDir = string.Empty;
        }

        // e.g. C:\Output\Path\MyFile.mp4
        //outputFileName = TokenRemover(Path.GetFileNameWithoutExtension(VM.MainView.Output_Text));
        // Set Original Name
        //outputFileName_Original = outputFileName;
        // Disable Tokens
        //outputFileName_Tokens = outputFileName;
        // Output
        //output = VM.MainView.Output_Text;
      }
    }
  }
}
