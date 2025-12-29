using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ViewModel;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Axiom.Views
{
  /// <summary>
  /// Preset management and process control panel
  /// </summary>
  public partial class ProcessControlPanel : UserControl
  {
    public ProcessControlPanel()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Save Preset - Button
    /// </summary>
    private void btnSavePreset_Click(object sender, RoutedEventArgs e)
    {
      // Check if Profiles Directory exists
      // Check if Custom Presets Path is valid
      if (MainWindow.IsValidPath(VM.ConfigureView.CustomPresetsPath_Text) == false)
      {
        return;
      }

      // If not, create it
      if (!Directory.Exists(VM.ConfigureView.CustomPresetsPath_Text))
      {
        // Yes/No Dialog Confirmation
        //
        MessageBoxResult resultExport = MessageBox.Show("Presets folder does not exist. Automatically create it?",
                                                        "Directory Not Found",
                                                        MessageBoxButton.YesNo,
                                                        MessageBoxImage.Information);
        switch (resultExport)
        {
          // Create
          case MessageBoxResult.Yes:
            try
            {
              Directory.CreateDirectory(VM.ConfigureView.CustomPresetsPath_Text);
            }
            catch
            {
              MessageBox.Show("Could not create Profiles folder. May require Administrator privileges.",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
            break;
          // Use Default
          case MessageBoxResult.No:
            break;
        }
      }

      // Open 'Save File'
      Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog();

      // Defaults
      saveFile.InitialDirectory = VM.ConfigureView.CustomPresetsPath_Text;
      saveFile.RestoreDirectory = true;
      saveFile.Filter = "Initialization Files (*.ini)|*.ini";
      saveFile.DefaultExt = "ini";
      saveFile.FileName = "Custom Preset.ini";

      // Process dialog box
      if (saveFile.ShowDialog() == true)
      {
        // Set Input Dir, Name, Ext
        string presetDir = Path.GetDirectoryName(saveFile.FileName).TrimEnd('\\') + @"\";
        string presetFileName = Path.GetFileNameWithoutExtension(saveFile.FileName);
        string presetExt = Path.GetExtension(saveFile.FileName);
        string preset = Path.Combine(presetDir, presetFileName + presetExt);

        // -------------------------
        // Overwriting doesn't work properly with INI Writer
        // Delete File instead before saving new
        // -------------------------
        if (File.Exists(preset))
        {
          if (MainWindow.hasWriteAccessToFolder(presetDir))
          {
            try
            {
              File.Delete(preset);
            }
            catch
            {
              MessageBox.Show("Could not replace old custom preset. May require Administrator privileges.",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
          }
        }

        // -------------------------
        // Save Custom Preset ini file
        // -------------------------
        //MessageBox.Show(preset); //debug
        Profiles.Profiles.ExportPreset(preset);

        // -------------------------
        // Load Custom Presets
        // Refresh Presets ComboBox
        // -------------------------
        Profiles.Profiles.LoadCustomPresets();

        // -------------------------
        // Select the Preset
        // -------------------------
        VM.MainView.Preset_SelectedItem = presetFileName;
      }
      else
      {
        // -------------------------
        // Load Custom Presets
        // Refresh Presets ComboBox
        // -------------------------
        Profiles.Profiles.LoadCustomPresets();

        if (string.IsNullOrWhiteSpace(VM.MainView.Preset_SelectedItem))
        {
          VM.MainView.Preset_SelectedItem = "Preset";
        }
      }
    }

    /// <summary>
    /// Delete Preset - Button
    /// </summary>
    private void btnDeletePreset_Click(object sender, RoutedEventArgs e)
    {
      // TODO: Implement preset deletion functionality
    }

    /// <summary>
    /// Preset - ComboBox SelectionChanged
    /// </summary>
    private void cboPreset_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // TODO: Implement preset selection changed functionality
    }

    /// <summary>
    /// Preview - Button
    /// </summary>
    private void btnPreview_Click(object sender, RoutedEventArgs e)
    {
      // TODO: Implement preview functionality
    }

    /// <summary>
    /// Convert - Button
    /// </summary>
    private void btnConvert_Click(object sender, RoutedEventArgs e)
    {
      // TODO: Implement convert functionality
    }

    /// <summary>
    /// Batch - Toggle Checked
    /// </summary>
    private void tglBatch_Checked(object sender, RoutedEventArgs e)
    {
      // TODO: Implement batch checked functionality
    }

    /// <summary>
    /// Batch - Toggle Unchecked
    /// </summary>
    private void tglBatch_Unchecked(object sender, RoutedEventArgs e)
    {
      // TODO: Implement batch unchecked functionality
    }

    /// <summary>
    /// Batch Extension - TextChanged
    /// </summary>
    private void batchExtension_TextChanged(object sender, TextChangedEventArgs e)
    {
      // TODO: Implement batch extension text changed functionality
    }
  }
}
