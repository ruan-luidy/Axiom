using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel;

namespace Axiom.Views
{
  public partial class VideoSizeControl : UserControl
  {
    public VideoSizeControl()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Crop Clear Button
    /// </summary>
    private void btnVideo_CropClear_Click(object sender, RoutedEventArgs e)
    {
      // Clear Crop Values
      CropWindow.CropClear();
    }

    /// <summary>
    /// Crop Window - Button
    /// </summary>
    private void btnVideo_Crop_Click(object sender, RoutedEventArgs e)
    {
      // Get MainWindow reference
      var mainWindow = Window.GetWindow(this) as MainWindow;
      if (mainWindow == null) return;

      // Start Window
      MainWindow.cropwindow = new CropWindow(mainWindow);

      // Detect which screen we're on
      var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
      var thisScreen = allScreens.SingleOrDefault(s => mainWindow.Left >= s.WorkingArea.Left && mainWindow.Left < s.WorkingArea.Right);

      // Position Relative to MainWindow
      // Keep from going off screen
      MainWindow.cropwindow.Left = Math.Max((mainWindow.Left + (mainWindow.Width - MainWindow.cropwindow.Width) / 2), thisScreen.WorkingArea.Left);
      MainWindow.cropwindow.Top = Math.Max(mainWindow.Top - MainWindow.cropwindow.Height - 12, thisScreen.WorkingArea.Top);

      // Keep Window on Top
      MainWindow.cropwindow.Owner = mainWindow;

      // Open Window
      MainWindow.cropwindow.ShowDialog();
    }

    /// <summary>
    /// Video Aspect Ratio
    /// </summary>
    private void cboVideo_AspectRatio_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void cboVideo_Scale_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // --------------------------------------------------
      // Enable/Disable Size
      // --------------------------------------------------
      // -------------------------
      // Custom
      // -------------------------
      if (VM.VideoView.Video_Scale_SelectedItem == "Custom")
      {
        VM.VideoView.Video_Width_IsEnabled = true;
        VM.VideoView.Video_Height_IsEnabled = true;

        VM.VideoView.Video_ScalingAlgorithm_IsEnabled = true;
      }

      // -------------------------
      // Source
      // -------------------------
      else if (VM.VideoView.Video_Scale_SelectedItem == "Source")
      {
        VM.VideoView.Video_Width_IsEnabled = false;
        VM.VideoView.Video_Height_IsEnabled = false;

        VM.VideoView.Video_ScalingAlgorithm_IsEnabled = false;
      }

      // -------------------------
      // All Other Sizes
      // -------------------------
      else
      {
        VM.VideoView.Video_Width_IsEnabled = false;
        VM.VideoView.Video_Height_IsEnabled = false;

        VM.VideoView.Video_ScalingAlgorithm_IsEnabled = true;
      }


      // -------------------------
      // Update Width/Height TextBox Display
      // -------------------------
      MainWindow.VideoScaleDisplay();

      // -------------------------
      // Output Path Update Display
      // -------------------------
      //OutputPath_UpdateDisplay();
    }

    /// <summary>
    /// Video Scaling Algorithm
    /// </summary>
    private void cboVideo_ScalingAlgorithm_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // -------------------------
      // Output Path Update Display
      // -------------------------
      //OutputPath_UpdateDisplay();
    }

    /// <summary>
    /// Video Screen Format
    /// </summary>
    private void cboVideo_ScreenFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (VM.VideoView.Video_Scale_SelectedItem != "Custom")
      {
        MainWindow.VideoScaleDisplay();
      }
    }

    private void tbxVideo_Height_GotFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxVideo_Height not being generated
      // Clear textbox on focus if default text "auto"
      /*
      if (tbxVideo_Height.Focus() == true &&
          VM.VideoView.Video_Height_Text == "auto")
      {
        VM.VideoView.Video_Height_Text = string.Empty;
      }
      */
    }

    private void tbxVideo_Height_LostFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxVideo_Height not being generated
      /*
      VM.VideoView.Video_Height_Text = tbxVideo_Height.Text;

      // Change textbox back to "height" if left empty
      if (string.IsNullOrWhiteSpace(VM.VideoView.Video_Height_Text))
      {
        VM.VideoView.Video_Height_Text = "auto";
      }
      */
    }

    private void tbxVideo_Width_GotFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxVideo_Width not being generated
      // Clear textbox on focus if default text "auto"
      /*
      if (tbxVideo_Width.Focus() == true &&
          VM.VideoView.Video_Width_Text == "auto")
      {
        VM.VideoView.Video_Width_Text = string.Empty;
      }
      */
    }

    private void tbxVideo_Width_LostFocus(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - tbxVideo_Width not being generated
      /*
      VM.VideoView.Video_Width_Text = tbxVideo_Width.Text;

      // Change textbox back to "auto" if left empty
      if (string.IsNullOrWhiteSpace(VM.VideoView.Video_Width_Text))
      {
        VM.VideoView.Video_Width_Text = "auto";
      }
      */
    }

  }
}
