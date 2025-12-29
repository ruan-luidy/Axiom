using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel;

namespace Axiom.Views
{
  public partial class FormatControl : UserControl
  {
    public FormatControl()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Container - ComboBox
    /// </summary>
    private void cboFormat_Container_PreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
      // Save Previouis Item
      MainWindow.Format_Container_PreviousItem = VM.FormatView.Format_Container_SelectedItem;
      //MessageBox.Show(MainWindow.Format_Container_PreviousItem);
    }

    private void cboFormat_Container_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // -------------------------
      // Format Controls
      // -------------------------
      Controls.Format.Controls.FormatControls(VM.FormatView.Format_Container_SelectedItem);

      // -------------------------
      // Set HW Accel Codec
      // -------------------------
      MainWindow.SetHWAccelVideoCodecs();
      MainWindow.SelectHWAccelVideoCodec();
      MainWindow.ChangeHWAccelTranscode();

      // -------------------------
      // Get Output Extension
      // -------------------------
      Controls.Format.Controls.OutputFormatExt();

      // -------------------------
      // Video Encoding Pass
      // -------------------------
      Controls.Video.Controls.EncodingPassControls();

      // -------------------------
      // Pixel Format
      // -------------------------
      //VideoControls.PixelFormatControls(VM.FormatView.Format_MediaType_SelectedItem,
      //                                  VM.VideoView.Video_Codec_SelectedItem,
      //                                  VM.VideoView.Video_Quality_SelectedItem);

      // -------------------------
      // Optimize Controls
      // -------------------------
      Controls.Video.Controls.OptimizeControls();


      //// -------------------------
      //// File Renamer
      //// -------------------------
      //// Add (1) if File Names are the same
      //if (VM.MainView.Batch_IsChecked == false) // Ignore batch
      //{
      //    if (!string.IsNullOrWhiteSpace(inputDir) &&
      //        string.Equals(inputFileName, outputFileName, StringComparison.OrdinalIgnoreCase))
      //    {
      //        outputFileName = FileRenamer(inputFileName);
      //        outputFileName_Tokens = FileRenamer(TokenAppender(inputFileName));
      //    }
      //}

      // --------------------------------------------------
      // Default Auto if Input Extension matches Output Extsion
      // This will trigger Auto Codec Copy
      // --------------------------------------------------
      //ExtensionMatchLoadAutoValues();
      //MessageBox.Show(inputExt + " " + outputExt); //debug

      // -------------------------
      // Update Ouput Textbox with current Format extension
      // -------------------------
      //OutputPath_UpdateDisplay();
      if (!string.IsNullOrWhiteSpace(VM.MainView.Output_Text) &&
          VM.MainView.Batch_IsChecked == false &&
          MainWindow.IsValidPath(VM.MainView.Output_Text) == true)
      {
        string outputDir = Path.GetDirectoryName(VM.MainView.Output_Text);
        string outputFileName = Path.GetFileNameWithoutExtension(VM.MainView.Output_Text);
        VM.MainView.Output_Text = Path.Combine(outputDir, outputFileName + MainWindow.outputExt);
      }

      // -------------------------
      // Force MediaTypeControls ComboBox to fire SelectionChanged Event
      // to update Format changes such as Audio_Stream_SelectedItem
      // -------------------------
      // TODO: Fix XAML compilation - cboFormat_MediaType is in this UserControl but .g.cs not generated
      //cboFormat_MediaType_SelectionChanged(cboFormat_MediaType, null);

      // -------------------------
      // Set Video and AudioCodec Combobox to "Copy" if 
      // Input File Extension is Same as Output File Extension 
      // and Quality is Auto
      // -------------------------
      //VideoControls.AutoCopyVideoCodec("control");
      //SubtitleControls.AutoCopySubtitleCodec("control");
      //AudioControls.AutoCopyAudioCodec("control");
    }

    /// <summary>
    /// Cut Combobox
    /// </summary>
    private void cboFormat_Cut_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      Controls.Format.Controls.CutControls();
    }

    /// <summary>
    /// Media Type - Combobox
    /// </summary>
    private void cboFormat_MediaType_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      Controls.Format.Controls.MediaTypeControls(); // Enabled / Disabled

      Controls.Format.Controls.MediaTypeControls_SelectedItems(); // Selected Items
    }

    /// <summary>
    /// YouTube Method - Selection Changed
    /// </summary>
    private void cboYouTube_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // Video + Audio
      // Video Only
      if (VM.FormatView.Format_YouTube_SelectedItem == "Video + Audio" ||
          VM.FormatView.Format_YouTube_SelectedItem == "Video Only")
      {
        // Change Items Source
        VM.FormatView.Format_YouTube_Quality_Items = new ObservableCollection<string>()
                        {
                            "best",
                            "best 4K",
                            "best 1080p",
                            "best 720p",
                            "best 480p",
                            "worst"
                        };

        // Select Default
        VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";
      }

      // Audio Only
      else if (VM.FormatView.Format_YouTube_SelectedItem == "Audio Only")
      {
        // Change Items Source
        VM.FormatView.Format_YouTube_Quality_Items = new ObservableCollection<string>()
                        {
                            "best",
                            "worst"
                        };

        // Select Default
        VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";
      }
    }

    private void tbxCutEndHours_GotFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxCutEndHours .g.cs not generated
      // Clear textbox on focus if default text "auto"
      /*
      if (tbxCutEndHours.Focus() == true &&
          VM.FormatView.Format_CutEnd_Hours_Text == "00")
      {
        VM.FormatView.Format_CutEnd_Hours_Text = string.Empty;
      }
      */
    }

    private void tbxCutEndHours_KeyDown(object sender, KeyEventArgs e)
    {
      MainWindow.Allow_Only_Number_Keys(e);
    }

    private void tbxCutEndHours_LostFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxCutEndHours .g.cs not generated
      /*
      VM.FormatView.Format_CutEnd_Hours_Text = tbxCutEndHours.Text;

      // Change textbox back to "00" if left empty
      if (string.IsNullOrWhiteSpace(VM.FormatView.Format_CutEnd_Hours_Text))
      {
        VM.FormatView.Format_CutEnd_Hours_Text = "00";
      }
      */
    }

    private void tbxCutEndMilliseconds_GotFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxCutEndMilliseconds .g.cs not generated
      // Clear textbox on focus if default text "auto"
      /*
      if (tbxCutEndMilliseconds.Focus() == true &&
          VM.FormatView.Format_CutEnd_Milliseconds_Text == "000")
      {
        VM.FormatView.Format_CutEnd_Milliseconds_Text = string.Empty;
      }
      */
    }

    private void tbxCutEndMilliseconds_KeyDown(object sender, KeyEventArgs e)
    {
      MainWindow.Allow_Only_Number_Keys(e);
    }

    private void tbxCutEndMilliseconds_LostFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxCutEndMilliseconds .g.cs not generated
      /*
      VM.FormatView.Format_CutEnd_Milliseconds_Text = tbxCutEndMilliseconds.Text;

      // Change textbox back to "00" if left empty
      if (string.IsNullOrWhiteSpace(VM.FormatView.Format_CutEnd_Milliseconds_Text))
      {
        VM.FormatView.Format_CutEnd_Milliseconds_Text = "000";
      }
      */
    }

    private void tbxCutEndMinutes_GotFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxCutEndMinutes .g.cs not generated
      // Clear textbox on focus if default text "auto"
      /*
      if (tbxCutEndMinutes.Focus() == true &&
          VM.FormatView.Format_CutEnd_Minutes_Text == "00")
      {
        VM.FormatView.Format_CutEnd_Minutes_Text = string.Empty;
      }
      */
    }

    private void tbxCutEndMinutes_KeyDown(object sender, KeyEventArgs e)
    {
      MainWindow.Allow_Only_Number_Keys(e);
    }

    private void tbxCutEndMinutes_LostFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxCutEndMinutes .g.cs not generated
      /*
      VM.FormatView.Format_CutEnd_Minutes_Text = tbxCutEndMinutes.Text;

      // Change textbox back to "00" if left empty
      if (string.IsNullOrWhiteSpace(VM.FormatView.Format_CutEnd_Minutes_Text))
      {
        VM.FormatView.Format_CutEnd_Minutes_Text = "00";
      }
      */
    }

    private void tbxCutEndSeconds_GotFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxCutEndSeconds .g.cs not generated
      // Clear textbox on focus if default text "auto"
      /*
      if (tbxCutEndSeconds.Focus() == true &&
          VM.FormatView.Format_CutEnd_Seconds_Text == "00")
      {
        VM.FormatView.Format_CutEnd_Seconds_Text = string.Empty;
      }
      */
    }

    private void tbxCutEndSeconds_KeyDown(object sender, KeyEventArgs e)
    {
      MainWindow.Allow_Only_Number_Keys(e);
    }

    private void tbxCutEndSeconds_LostFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxCutEndSeconds .g.cs not generated
      /*
      VM.FormatView.Format_CutEnd_Seconds_Text = tbxCutEndSeconds.Text;

      // Change textbox back to "00" if left empty
      if (string.IsNullOrWhiteSpace(VM.FormatView.Format_CutEnd_Seconds_Text))
      {
        VM.FormatView.Format_CutEnd_Seconds_Text = "00";
      }
      */
    }

    private void tbxCutStartHours_GotFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxCutStartHours .g.cs not generated
      // Clear textbox on focus if default text "auto"
      /*
      if (tbxCutStartHours.Focus() == true &&
          VM.FormatView.Format_CutStart_Hours_Text == "00")
      {
        VM.FormatView.Format_CutStart_Hours_Text = string.Empty;
      }
      */
    }

    private void tbxCutStartHours_KeyDown(object sender, KeyEventArgs e)
    {
      MainWindow.Allow_Only_Number_Keys(e);
    }

    private void tbxCutStartHours_LostFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxCutStartHours .g.cs not generated
      /*
      VM.FormatView.Format_CutStart_Hours_Text = tbxCutStartHours.Text;

      // Change textbox back to "00" if left empty
      if (string.IsNullOrWhiteSpace(VM.FormatView.Format_CutStart_Hours_Text))
      {
        VM.FormatView.Format_CutStart_Hours_Text = "00";
      }
      */
    }

    private void tbxCutStartMilliseconds_GotFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxCutStartMilliseconds .g.cs not generated
      // Clear textbox on focus if default text "auto"
      /*
      if (tbxCutStartMilliseconds.Focus() == true &&
          VM.FormatView.Format_CutStart_Milliseconds_Text == "000")
      {
        VM.FormatView.Format_CutStart_Milliseconds_Text = string.Empty;
      }
      */
    }

    private void tbxCutStartMilliseconds_KeyDown(object sender, KeyEventArgs e)
    {
      MainWindow.Allow_Only_Number_Keys(e);
    }

    private void tbxCutStartMilliseconds_LostFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxCutStartMilliseconds .g.cs not generated
      /*
      VM.FormatView.Format_CutStart_Milliseconds_Text = tbxCutStartMilliseconds.Text;

      // Change textbox back to "00" if left empty
      if (string.IsNullOrWhiteSpace(VM.FormatView.Format_CutStart_Milliseconds_Text))
      {
        VM.FormatView.Format_CutStart_Milliseconds_Text = "000";
      }
      */
    }

    private void tbxCutStartMinutes_GotFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxCutStartMinutes .g.cs not generated
      // Clear textbox on focus if default text "auto"
      /*
      if (tbxCutStartMinutes.Focus() == true &&
          VM.FormatView.Format_CutStart_Minutes_Text == "00")
      {
        VM.FormatView.Format_CutStart_Minutes_Text = string.Empty;
      }
      */
    }

    private void tbxCutStartMinutes_KeyDown(object sender, KeyEventArgs e)
    {
      MainWindow.Allow_Only_Number_Keys(e);
    }

    private void tbxCutStartMinutes_LostFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxCutStartMinutes .g.cs not generated
      /*
      VM.FormatView.Format_CutStart_Minutes_Text = tbxCutStartMinutes.Text;

      // Change textbox back to "00" if left empty
      if (string.IsNullOrWhiteSpace(VM.FormatView.Format_CutStart_Minutes_Text))
      {
        VM.FormatView.Format_CutStart_Minutes_Text = "00";
      }
      */
    }

    private void tbxCutStartSeconds_GotFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxCutStartSeconds .g.cs not generated
      // Clear textbox on focus if default text "auto"
      /*
      if (tbxCutStartSeconds.Focus() == true &&
          VM.FormatView.Format_CutStart_Seconds_Text == "00")
      {
        VM.FormatView.Format_CutStart_Seconds_Text = string.Empty;
      }
      */
    }

    private void tbxCutStartSeconds_KeyDown(object sender, KeyEventArgs e)
    {
      MainWindow.Allow_Only_Number_Keys(e);
    }

    private void tbxCutStartSeconds_LostFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxCutStartSeconds .g.cs not generated
      /*
      VM.FormatView.Format_CutStart_Seconds_Text = tbxCutStartSeconds.Text;

      // Change textbox back to "00" if left empty
      if (string.IsNullOrWhiteSpace(VM.FormatView.Format_CutStart_Seconds_Text))
      {
        VM.FormatView.Format_CutStart_Seconds_Text = "00";
      }
      */
    }

    private void tbxFrameEnd_GotFocus(object sender, RoutedEventArgs e)
    {
    }

    private void tbxFrameEnd_KeyDown(object sender, KeyEventArgs e)
    {
      MainWindow.Allow_Only_Number_Keys(e);
    }

    private void tbxFrameEnd_LostFocus(object sender, RoutedEventArgs e)
    {
    }

    private void tbxFrameStart_GotFocus(object sender, RoutedEventArgs e)
    {
    }

    private void tbxFrameStart_KeyDown(object sender, KeyEventArgs e)
    {
      MainWindow.Allow_Only_Number_Keys(e);
    }

    private void tbxFrameStart_LostFocus(object sender, RoutedEventArgs e)
    {
    }

  }
}
