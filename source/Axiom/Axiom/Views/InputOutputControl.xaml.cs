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

      MainWindow.inputDir = string.Empty;
      MainWindow.inputFileName = string.Empty;
      MainWindow.inputExt = string.Empty;
      MainWindow.input = string.Empty;
    }

    /// <summary>
    /// Output TetBox Clear - Button
    /// </summary>
    private void btnOutputClear_Click(object sender, RoutedEventArgs e)
    {
      VM.MainView.Output_Text = string.Empty;

      MainWindow.outputDir = string.Empty;
      MainWindow.outputFileName_Original = string.Empty;
      MainWindow.outputFileName_Tokens = string.Empty;
      MainWindow.outputFileName = string.Empty;
      MainWindow.outputExt = string.Empty;
      MainWindow.output = string.Empty;
    }

    /// <summary>
    /// Open Input Folder - Button
    /// </summary>
    private void openLocationInput_Click(object sender, RoutedEventArgs e)
    {
      if (MainWindow.IsValidPath(MainWindow.inputDir))
      {
        if (Directory.Exists(@MainWindow.inputDir))
        {
          Process.Start("explorer.exe", @MainWindow.inputDir);
        }
      }
    }

    /// <summary>
    /// Open Output Folder - Button
    /// </summary>
    private void openLocationOutput_Click(object sender, RoutedEventArgs e)
    {
      if (MainWindow.IsValidPath(MainWindow.outputDir))
      {
        if (Directory.Exists(@MainWindow.outputDir))
        {
          Process.Start("explorer.exe", @MainWindow.outputDir);
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
        MainWindow.inputDir = Path.GetDirectoryName(VM.MainView.Input_Text).TrimEnd('\\') + @"\";
        MainWindow.inputFileName = Path.GetFileNameWithoutExtension(VM.MainView.Input_Text);
        MainWindow.inputExt = Path.GetExtension(VM.MainView.Input_Text);

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
        MainWindow.outputFileName_Original = MainWindow.TokenRemover(Path.GetFileNameWithoutExtension(VM.MainView.Output_Text));
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
      MainWindow.outputFileName_Original = MainWindow.TokenRemover(Path.GetFileNameWithoutExtension(VM.MainView.Output_Text));
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
          MainWindow.IsValidPath(VM.MainView.Output_Text) == true && // Detect Invalid Characters
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
        if (MainWindow.IsValidPath(VM.MainView.Output_Text) == true)
        {
          MainWindow.outputDir = Path.GetDirectoryName(VM.MainView.Output_Text);
          if (!string.IsNullOrWhiteSpace(MainWindow.outputDir))
          {
            MainWindow.outputDir = MainWindow.outputDir.TrimEnd('\\') + @"\";
          }
        }
        else
        {
          MainWindow.outputDir = string.Empty;
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

    /// <summary>
    /// Input Button
    /// </summary>
    private void btnInput_Click(object sender, RoutedEventArgs e)
    {
      switch (VM.MainView.Batch_IsChecked)
      {
        // -------------------------
        // Single File
        // -------------------------
        case false:
          Microsoft.Win32.OpenFileDialog selectFile = new Microsoft.Win32.OpenFileDialog
          {
            CheckFileExists = true,
            CheckPathExists = true,
          };

          if (selectFile.ShowDialog() == true)
          {
            // Display path and file in Output Textbox
            VM.MainView.Input_Text = selectFile.FileName;

            // Set Input Dir, Name, Ext
            MainWindow.inputDir = Path.GetDirectoryName(selectFile.FileName).TrimEnd('\\') + @"\";
            MainWindow.inputFileName = Path.GetFileNameWithoutExtension(selectFile.FileName);
            MainWindow.inputExt = Path.GetExtension(selectFile.FileName);

            // Clear Output TextBox
            VM.MainView.Output_Text = string.Empty;
          }
          break;

        // -------------------------
        // Batch
        // -------------------------
        case true:
          // Open Batch Folder
          System.Windows.Forms.FolderBrowserDialog inputFolder = new System.Windows.Forms.FolderBrowserDialog();
          System.Windows.Forms.DialogResult resultBatch = inputFolder.ShowDialog();

          // Show Input Dialog Box
          if (resultBatch == System.Windows.Forms.DialogResult.OK)
          {
            // Display Folder Path in Textbox
            VM.MainView.Input_Text = inputFolder.SelectedPath.TrimEnd('\\') + @"\";

            // Input Directory
            MainWindow.inputDir = VM.MainView.Input_Text.TrimEnd('\\') + @"\";
          }
          break;
      }
    }

    /// <summary>
    /// Input Textbox
    /// </summary>
    private void tbxInput_TextChanged(object sender, TextChangedEventArgs e)
    {
      MainWindow.input = VM.MainView.Input_Text;

      // -------------------------
      // Local File
      // -------------------------
      if (MainWindow.IsWebURL(VM.MainView.Input_Text) == false)
      {
        // -------------------------
        // Single File
        // -------------------------
        if (VM.MainView.Batch_IsChecked == false)
        {
          // Has Text
          if (!string.IsNullOrWhiteSpace(VM.MainView.Input_Text))
          {
            // Remove stray slash if closed out early
            if (MainWindow.input == @"\")
            {
              MainWindow.input = string.Empty;
            }

            // Input Extension
            MainWindow.inputExt = Path.GetExtension(VM.MainView.Input_Text);
          }
          // No Text
          else
          {
            MainWindow.input = string.Empty;
            MainWindow.inputDir = string.Empty;
          }
        }
        // -------------------------
        // Batch
        // -------------------------
        else
        {
          // Has Text
          if (!string.IsNullOrWhiteSpace(VM.MainView.Input_Text))
          {
            // Remove stray slash if closed out early
            if (MainWindow.input == @"\")
            {
              MainWindow.input = string.Empty;
            }

            MainWindow.inputDir = VM.MainView.Input_Text.TrimEnd('\\') + @"\";
            MainWindow.inputExt = VM.MainView.BatchExtension_Text;
          }
          // No Text
          else
          {
            MainWindow.input = string.Empty;
            MainWindow.inputDir = string.Empty;
            MainWindow.inputExt = string.Empty;
          }
        }

        // -------------------------
        // Enable / Disable "Open Input Location" Button
        // -------------------------
        if (MainWindow.IsValidPath(VM.MainView.Input_Text) == true &&
            Path.IsPathRooted(VM.MainView.Input_Text) == true)
        {
          bool exists = Directory.Exists(Path.GetDirectoryName(VM.MainView.Input_Text));

          // Path exists
          if (exists)
          {
            VM.MainView.Input_Location_IsEnabled = true;
          }
          // Path does not exist
          else
          {
            VM.MainView.Input_Location_IsEnabled = false;
          }
        }
        // Disable for Web URL
        else
        {
          VM.MainView.Input_Location_IsEnabled = false;
        }
      }
      // -------------------------
      // Web URL
      // -------------------------
      else
      {
        MainWindow.inputDir = string.Empty;
        MainWindow.inputExt = string.Empty;
        VM.MainView.Input_Location_IsEnabled = false;
      }

      // -------------------------
      // Enable / Disable "Input Clear" Button
      // -------------------------
      // Disable
      if (string.IsNullOrWhiteSpace(VM.MainView.Input_Text))
      {
        VM.MainView.Input_Clear_IsEnabled = false;
      }
      // Enable
      else
      {
        VM.MainView.Input_Clear_IsEnabled = true;
      }

      // -------------------------
      // Convert Button Text Change
      // -------------------------
      MainWindow.ConvertButtonText();
    }

    /// <summary>
    /// Input Textbox - Drag and Drop
    /// </summary>
    private void tbxInput_PreviewDragOver(object sender, DragEventArgs e)
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

    /// <summary>
    /// Output Button
    /// </summary>
    private void btnOutput_Click(object sender, RoutedEventArgs e)
    {
      // Get Output File Extension
      Controls.Format.Controls.OutputFormatExt();

      switch (VM.MainView.Batch_IsChecked)
      {
        // -------------------------
        // Single File
        // -------------------------
        case false:
          // Initialize 'Save File' Dialog Window
          Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog();
          // File Filter
          saveFile.Filter = MainWindow.Output_SaveFileDialog_Filter();
          // Set Output Extension
          saveFile.DefaultExt = MainWindow.outputExt;

          // -------------------------
          // Output TextBox is Empty
          // -------------------------
          if (string.IsNullOrWhiteSpace(VM.MainView.Output_Text))
          {
            // Input File Name is Empty
            if (string.IsNullOrWhiteSpace(MainWindow.inputFileName))
            {
              // Load InitialDirectory from axiom.conf
              try
              {
                if (File.Exists(Controls.Configure.axiomConfFile))
                {
                  Controls.Configure.ConfigFile conf = new Controls.Configure.ConfigFile(Controls.Configure.axiomConfFile);
                  MainWindow.outputPreviousPath = conf.Read("User", "OutputPreviousPath");

                  if (!string.IsNullOrWhiteSpace(MainWindow.outputPreviousPath))
                  {
                    saveFile.InitialDirectory = MainWindow.outputPreviousPath;
                  }
                }
              }
              catch
              {

              }

              saveFile.FileName = "File";
            }
            // Input has File Name
            else
            {
              // Set Output to same as Input
              saveFile.InitialDirectory = MainWindow.inputDir;
              saveFile.FileName = MainWindow.TokenRemover(
                                          MainWindow.FileRenamer(MainWindow.inputDir,
                                              MainWindow.inputDir,
                                              MainWindow.inputFileName,
                                              MainWindow.inputFileName
                                             )
                                      );
            }
          }

          // -------------------------
          // Output TextBox has Text
          // -------------------------
          else
          {
            // Set Initial Directory
            saveFile.InitialDirectory = Path.GetDirectoryName(VM.MainView.Output_Text);
            saveFile.FileName = Path.GetFileNameWithoutExtension(VM.MainView.Output_Text);
          }

          // -------------------------
          // Show 'Save File' Dialog Window
          // -------------------------
          if (saveFile.ShowDialog() == true)
          {
            if (MainWindow.IsValidPath(saveFile.FileName))
            {
              // Set Output to Dialog Window entered
              MainWindow.outputDir = Path.GetDirectoryName(Path.Combine(saveFile.InitialDirectory, saveFile.FileName + MainWindow.outputExt));
              MainWindow.outputFileName_Original = MainWindow.TokenRemover(Path.GetFileNameWithoutExtension(saveFile.FileName));

              // -------------------------
              // Update Output TextBox
              // -------------------------
              MainWindow.outputFileName = MainWindow.outputFileName_Original;

              // Update Output TextBox Display
              if (!string.IsNullOrEmpty(MainWindow.outputDir))
              {
                VM.MainView.Output_Text = Path.Combine(MainWindow.outputDir, MainWindow.outputFileName + MainWindow.outputExt);
              }

              // Save Previous Path
              if (File.Exists(Controls.Configure.axiomConfFile))
              {
                try
                {
                  Controls.Configure.ConfigFile conf = new Controls.Configure.ConfigFile(Controls.Configure.axiomConfFile);
                  conf.Write("User", "OutputPreviousPath", MainWindow.outputDir);
                }
                catch
                {

                }
              }
            }
          }
          break;

        // -------------------------
        // Batch
        // -------------------------
        case true:
          // Initialize 'Select Folder' Dialog Window
          System.Windows.Forms.FolderBrowserDialog outputFolder = new System.Windows.Forms.FolderBrowserDialog();

          // Show 'Save File' Dialog Window
          System.Windows.Forms.DialogResult resultBatch = outputFolder.ShowDialog();

          // Process Dialog Window
          if (resultBatch == System.Windows.Forms.DialogResult.OK)
          {
            try
            {
              if (!string.IsNullOrWhiteSpace(outputFolder.SelectedPath))
              {
                if (MainWindow.IsValidPath(outputFolder.SelectedPath.TrimEnd('\\') + @"\"))
                {
                  // Set Output Path
                  MainWindow.outputDir = outputFolder.SelectedPath.TrimEnd('\\') + @"\";

                  // Update Output TextBox Display
                  VM.MainView.Output_Text = outputFolder.SelectedPath.TrimEnd('\\') + @"\";
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
          break;
      }
    }

    /// <summary>
    /// Output Textbox - Drag and Drop
    /// </summary>
    private void tbxOutput_PreviewDragOver(object sender, DragEventArgs e)
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

  }
}
