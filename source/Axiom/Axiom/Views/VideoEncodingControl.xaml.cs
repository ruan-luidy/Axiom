using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel;
using Axiom;

namespace Axiom.Views
{
  public partial class VideoEncodingControl : UserControl
  {
    public VideoEncodingControl()
    {
      InitializeComponent();
    }

    private void btnVideoBitRateAdvanced_Expand_Click(object sender, RoutedEventArgs e)
    {
      // Expand
      if (VM.VideoView.Video_BitRateAdvanced_IsExpanded == false)
      {
        VM.VideoView.Video_BitRateAdvanced_IsExpanded = true;
      }
      // Collapse
      else if (VM.VideoView.Video_BitRateAdvanced_IsExpanded == true)
      {
        VM.VideoView.Video_BitRateAdvanced_IsExpanded = false;
      }
    }

    /// <summary>
    /// Encode Speed Presets - ComboBox
    /// </summary>
    private void cboEncodeSpeed_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // -------------------------
      // Output Path Update Display
      // -------------------------
      //OutputPath_UpdateDisplay();
    }

    /// <summary>
    /// HW Accel Transcode - ComboBox
    /// </summary>
    private void cboHWAccelTranscode_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // -------------------------
      // Output Path Update Display
      // -------------------------
      //OutputPath_UpdateDisplay();

      // -------------------------
      // Select HW Accel Video Codec
      // -------------------------
      SelectHWAccelVideoCodec();
    }

    /// <summary>
    /// HW Accel
    /// </summary>
    private void cboHWAccel_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      switch (VM.VideoView.Video_HWAccel_SelectedItem)
      {
        case "Auto":
          VM.VideoView.Video_HWAccel_Decode_IsEnabled = false;
          VM.VideoView.Video_HWAccel_Decode_SelectedItem = "auto";
          VM.VideoView.Video_HWAccel_Transcode_IsEnabled = false;
          VM.VideoView.Video_HWAccel_Transcode_SelectedItem = "auto";
          break;

        case "On":
          VM.VideoView.Video_HWAccel_Decode_IsEnabled = true;
          VM.VideoView.Video_HWAccel_Decode_SelectedItem = "auto";
          VM.VideoView.Video_HWAccel_Transcode_IsEnabled = true;
          VM.VideoView.Video_HWAccel_Transcode_SelectedItem = "auto";
          break;

        case "Off":
          VM.VideoView.Video_HWAccel_Decode_IsEnabled = false;
          VM.VideoView.Video_HWAccel_Decode_SelectedItem = "off";
          VM.VideoView.Video_HWAccel_Transcode_IsEnabled = false;
          VM.VideoView.Video_HWAccel_Transcode_SelectedItem = "off";
          break;

        default:
          VM.VideoView.Video_HWAccel_Decode_IsEnabled = true;
          VM.VideoView.Video_HWAccel_Decode_SelectedItem = "off";
          VM.VideoView.Video_HWAccel_Transcode_IsEnabled = true;
          VM.VideoView.Video_HWAccel_Transcode_SelectedItem = "off";
          break;
      }

      // Add HW Accel Format Container Codecs
      SetHWAccelVideoCodecs();
    }

    /// <summary>
    /// Video Codec - ComboBox
    /// </summary>
    private void cboVideo_Codec_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      string video_Codec_SelectedItem = (sender as ComboBox).SelectedItem as string;

      // -------------------------
      // Change HW Accel Transcode
      // -------------------------
      ChangeHWAccelTranscode();

      // -------------------------
      // Halt if Selected Codec is Null
      // -------------------------
      //if (string.IsNullOrWhiteSpace(video_Codec_SelectedItem))
      //{
      //    return;
      //}

      // -------------------------
      // Get Input/Output Extensions
      // -------------------------
      //string inputExt = Path.GetExtension(VM.MainView.Input_Text).ToLower();
      //string outputExt = "." + VM.FormatView.Format_Container_SelectedItem.ToLower();

      // -------------------------
      // Save Selected Quality
      // When you change the Quality ComboBox from Auto to 320, it triggers the Codec ComboBox to change from Copy to x264,
      // in turn changing the Quality ComboBox back to Auto on the Codec switch
      // -------------------------
      //string userSelected_VideoQuality = string.Empty;
      //if (string.IsNullOrWhiteSpace(inputExt) ||
      //    string.Equals(inputExt, outputExt, StringComparison.OrdinalIgnoreCase))
      //{
      //    if (video_Codec_SelectedItem != "Copy" &&
      //        VM.VideoView.Video_Quality_SelectedItem != "Auto")
      //    {
      //        userSelected_VideoQuality = VM.VideoView.Video_Quality_SelectedItem;
      //    }
      //}

      // -------------------------
      // Set Copy Quality to Auto
      // -------------------------
      //if (string.IsNullOrWhiteSpace(inputExt) ||
      //    string.Equals(inputExt, outputExt, StringComparison.OrdinalIgnoreCase))
      //{
      //    if (video_Codec_SelectedItem == "Copy")
      //    {
      //        VM.VideoView.Video_Quality_SelectedItem = "Auto";
      //    }
      //}

      // -------------------------
      // Codec Controls
      // -------------------------
      Controls.Video.Controls.CodecControls(video_Codec_SelectedItem);

      // -------------------------
      // Media Type Controls
      // Overrides Codec Controls
      // -------------------------
      // Must be after Codec Controls
      Controls.Format.Controls.MediaTypeControls();

      // -------------------------
      // Re-Select the Quality Preset
      // -------------------------
      // Copy Codec -> VP8, x264, etc Codec
      //if (string.IsNullOrWhiteSpace(inputExt) ||
      //    string.Equals(inputExt, outputExt, StringComparison.OrdinalIgnoreCase))
      //{
      //    if (video_Codec_SelectedItem != "Copy")
      //    {
      //        if (!string.IsNullOrWhiteSpace(userSelected_VideoQuality))
      //        {
      //            VM.VideoView.Video_Quality_SelectedItem = userSelected_VideoQuality;
      //        }
      //    }
      //    // Set to Top of Item List: Auto or None
      //    else
      //    {
      //        VM.VideoView.Video_Quality_SelectedIndex = 0;
      //    }
      //}

      // -------------------------
      // Audio Stream Controls
      // -------------------------
      Controls.Format.Controls.AudioStreamControls();

      // -------------------------
      // Video Encoding Pass
      // -------------------------
      Controls.Video.Controls.EncodingPassControls();

      // -------------------------
      // Pixel Format
      // -------------------------
      //Controls.Video.Controls.PixelFormatControls(VM.FormatView.Format_MediaType_SelectedItem,
      //                                            VM.VideoView.Video_Codec_SelectedItem,
      //                                            VM.VideoView.Video_Quality_SelectedItem);

      // -------------------------
      // Optimize Controls
      // -------------------------
      Controls.Video.Controls.OptimizeControls();

      // -------------------------
      // Output Path Update Display
      // -------------------------
      //OutputPath_UpdateDisplay();

      // -------------------------
      // Convert Button Text Change
      // -------------------------
      ConvertButtonText();
    }

    /// <summary>
    /// FPS ComboBox
    /// </summary>
    private void cboVideo_FPS_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // -------------------------
      // Custom ComboBox Editable
      // -------------------------
      if (VM.VideoView.Video_FPS_SelectedItem == "Custom" ||
          string.IsNullOrWhiteSpace(VM.VideoView.Video_FPS_SelectedItem))
      {
        VM.VideoView.Video_FPS_IsEditable = true;
      }

      // -------------------------
      // Other Items Disable Editable
      // -------------------------
      if (VM.VideoView.Video_FPS_SelectedItem != "Custom" &&
          !string.IsNullOrWhiteSpace(VM.VideoView.Video_FPS_SelectedItem))
      {
        VM.VideoView.Video_FPS_IsEditable = false;
      }

      // -------------------------
      // Maintain Editable Combobox while typing
      // -------------------------
      if (VM.VideoView.Video_FPS_IsEditable == true)
      {
        VM.VideoView.Video_FPS_IsEditable = true;

        // Clear Custom Text
        VM.VideoView.Video_FPS_SelectedIndex = -1;
      }

      // -------------------------
      // Output Path Update Display
      // -------------------------
      //OutputPath_UpdateDisplay();
    }

    private void cboVideo_FrameRate_KeyDown(object sender, KeyEventArgs e)
    {
      // Only allow Numbers and Backspace
      // Deny Symbols (Shift + Number)
      // Allow Forward Slash
      if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && e.Key != Key.Back ||
          Keyboard.IsKeyDown(Key.LeftShift) && (e.Key >= Key.D0 && e.Key <= Key.D9) ||
          Keyboard.IsKeyDown(Key.RightShift) && (e.Key >= Key.D0 && e.Key <= Key.D9)
          )
      {
        e.Handled = true;
      }

      //if (e.Key != Key.OemQuestion)
      //{
      //    e.Handled = true;
      //}
    }

    /// <summary>
    /// Video Optimize ComboBox
    /// </summary>
    private void cboVideo_Optimize_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // -------------------------
      // Optimize Controls
      // -------------------------
      Controls.Video.Controls.OptimizeControls();
    }

    /// <summary>
    /// Pass - ComboBox
    /// </summary>
    private void cboVideo_Pass_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // -------------------------
      // Set Controls
      // -------------------------
      //VideoControls.SetControls(VM.VideoView.Video_Quality_SelectedItem);

      // -------------------------
      // Pass Controls
      // -------------------------
      Controls.Video.Controls.EncodingPassControls();

      // -------------------------
      // Display Bit Rate in TextBox
      // -------------------------
      Controls.Video.Controls.VideoBitRateDisplay(VM.VideoView.Video_Quality_Items,
                                                  VM.VideoView.Video_Quality_SelectedItem,
                                                  VM.VideoView.Video_Pass_SelectedItem);
      // -------------------------
      // Output Path Update Display
      // -------------------------
      //OutputPath_UpdateDisplay();
    }

    /// <summary>
    /// Pixel Format
    /// </summary>
    private void cboVideo_PixelFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // -------------------------
      // Output Path Update Display
      // -------------------------
      //OutputPath_UpdateDisplay();
    }

    /// <summary>
    /// Video Quality - ComboBox
    /// </summary>
    private void cboVideo_Quality_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // -------------------------
      // Halt if: Selected Codec is Null
      //          Selected Quality is Null
      // -------------------------
      //if (string.IsNullOrWhiteSpace(VM.VideoView.Video_Codec_SelectedItem) ||
      //    string.IsNullOrWhiteSpace(VM.VideoView.Video_Quality_SelectedItem))
      //{
      //    return;
      //}

      // -------------------------
      // Quality Controls
      // -------------------------
      Controls.Video.Controls.QualityControls();

      // -------------------------
      // Display Bit Rate in TextBox
      // -------------------------
      Controls.Video.Controls.VideoBitRateDisplay(VM.VideoView.Video_Quality_Items,
                                                  VM.VideoView.Video_Quality_SelectedItem,
                                                  VM.VideoView.Video_Pass_SelectedItem);

      // -------------------------
      // Video Encoding Pass
      // -------------------------
      Controls.Video.Controls.EncodingPassControls();

      // Custom
      if (VM.VideoView.Video_Quality_SelectedItem == "Custom")
      {
        // Default to CRF
        if (VM.VideoView.Video_Pass_Items?.Contains("CRF") == true)
        {
          VM.VideoView.Video_Pass_SelectedItem = "CRF";
        }
        // Select first available (1 Pass, 2 Pass, auto)
        else
        {
          VM.VideoView.Video_Pass_SelectedItem = VM.VideoView.Video_Pass_Items.FirstOrDefault();
        }
      }

      // -------------------------
      // Pixel Format
      // -------------------------
      Controls.Video.Controls.PixelFormatControls(VM.FormatView.Format_MediaType_SelectedItem,
                                                  VM.VideoView.Video_Codec_SelectedItem,
                                                  VM.VideoView.Video_Quality_SelectedItem);

      // -------------------------
      // Output Path Update Display
      // -------------------------
      //OutputPath_UpdateDisplay();

      // -------------------------
      // Set Audio Codec Combobox to "Copy" if 
      // Input File Extension is Same as Output File Extension 
      // and Quality is Auto
      // -------------------------
      //VideoControls.AutoCopyVideoCodec("control");
    }

    private void cboVideo_Speed_KeyDown(object sender, KeyEventArgs e)
    {
      // Only allow Numbers and Backspace
      Allow_Only_Number_Keys(e);
    }

    /// <summary>
    /// Speed ComboBox
    /// </summary>
    private void cboVideo_Speed_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // -------------------------
      // Custom ComboBox Editable
      // -------------------------
      if (VM.VideoView.Video_Speed_SelectedItem == "Custom" ||
          string.IsNullOrWhiteSpace(VM.VideoView.Video_Speed_SelectedItem))
      {
        VM.VideoView.Video_Speed_IsEditable = true;
      }

      // -------------------------
      // Other Items Disable Editable
      // -------------------------
      if (VM.VideoView.Video_Speed_SelectedItem != "Custom" &&
          !string.IsNullOrWhiteSpace(VM.VideoView.Video_Speed_SelectedItem))
      {
        VM.VideoView.Video_Speed_IsEditable = false;
      }

      // -------------------------
      // Maintain Editable Combobox while typing
      // -------------------------
      if (VM.VideoView.Video_Speed_IsEditable == true)
      {
        VM.VideoView.Video_Speed_IsEditable = true;

        // Clear Custom Text
        VM.VideoView.Video_Speed_SelectedIndex = -1;
      }
    }

    /// <summary>
    /// Vsync ComboBox
    /// </summary>
    private void cboVideo_Vsync_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void slVideo_CRF_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      // Reset to default
      VM.VideoView.Video_CRF_Value = 23;
    }

    private void tbxVideo_CRF_KeyDown(object sender, KeyEventArgs e)
    {
      // Only allow Numbers and Backspace
      Allow_Only_Number_Keys(e);
    }

    private void tbxVideo_CRF_TextChanged(object sender, TextChangedEventArgs e)
    {
      // Update Slider with entered value
      if (!string.IsNullOrWhiteSpace(VM.VideoView.Video_CRF_Text))
      {
        VM.VideoView.Video_CRF_Value = Convert.ToDouble(VM.VideoView.Video_CRF_Text);
      }

      // TextBox Empty
      //else if (string.IsNullOrWhiteSpace(VM.VideoView.Video_CRF_Text))
      //{
      //  VM.VideoView.Video_CRF_Value = 0;
      //  VM.VideoView.Video_CRF_Text = string.Empty;
      //}

      // -------------------------
      // Output Path Update Display
      // -------------------------
      //OutputPath_UpdateDisplay();

    }

    /// <summary>
    /// Video VBR Toggle - Checked
    /// </summary>
    private void tglVideo_VBR_Checked(object sender, RoutedEventArgs e)
    {
      // -------------------------
      // Quality Controls
      // -------------------------
      Controls.Video.Controls.QualityControls();

      // -------------------------
      // MPEG-4 VBR can only use 1 Pass
      // -------------------------
      if (VM.VideoView.Video_Codec_SelectedItem == "MPEG-2" ||
          VM.VideoView.Video_Codec_SelectedItem == "MPEG-4")
      {
        // Change ItemsSource
        VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                        {
                            "1 Pass",
                        };

        // Populate ComboBox from ItemsSource
        //VM.VideoView.Video_Pass_Items = VideoControls.Video_Pass_ItemsSource;

        // Select Item
        VM.VideoView.Video_Pass_SelectedItem = "1 Pass";
      }


      // -------------------------
      // Display Bit Rate in TextBox
      // -------------------------
      Controls.Video.Controls.VideoBitRateDisplay(VM.VideoView.Video_Quality_Items,
                                                  VM.VideoView.Video_Quality_SelectedItem,
                                                  VM.VideoView.Video_Pass_SelectedItem);
    }

    /// <summary>
    /// Video VBR Toggle - Unchecked
    /// </summary>
    private void tglVideo_VBR_Unchecked(object sender, RoutedEventArgs e)
    {
      // -------------------------
      // Quality Controls
      // -------------------------
      Controls.Video.Controls.QualityControls();

      // -------------------------
      // MPEG-2 / MPEG-4 CBR Reset
      // -------------------------
      if (VM.VideoView.Video_Codec_SelectedItem == "MPEG-2" ||
          VM.VideoView.Video_Codec_SelectedItem == "MPEG-4")
      {
        // Change ItemsSource
        VM.VideoView.Video_Pass_Items = new ObservableCollection<string>()
                        {
                            "2 Pass",
                            "1 Pass",
                        };

        // Populate ComboBox from ItemsSource
        //cboVideo_Pass.ItemsSource = VideoControls.Video_Pass_ItemsSource;

        // Select Item
        VM.VideoView.Video_Pass_SelectedItem = "2 Pass";
      }

      // -------------------------
      // Display Bit Rate in TextBox
      // -------------------------
      Controls.Video.Controls.VideoBitRateDisplay(VM.VideoView.Video_Quality_Items,
                                                  VM.VideoView.Video_Quality_SelectedItem,
                                                  VM.VideoView.Video_Pass_SelectedItem);
    }

  }
}
