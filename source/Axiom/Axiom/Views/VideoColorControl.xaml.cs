using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel;

namespace Axiom.Views
{
    public partial class VideoColorControl : UserControl
    {
        public VideoColorControl()
        {
            InitializeComponent();
        }

            /// <summary>
            /// Color Reset - Button
            /// </summary>
            private void btnColor_Reset_Click(object sender, RoutedEventArgs e)
            {
              VM.VideoView.Video_Color_Range_SelectedItem = "auto";
              VM.VideoView.Video_Color_Space_SelectedItem = "auto";
              VM.VideoView.Video_Color_Primaries_SelectedItem = "auto";
              VM.VideoView.Video_Color_TransferCharacteristics_SelectedItem = "auto";
              VM.VideoView.Video_Color_Matrix_SelectedItem = "auto";
            }

            private void btnFilterVideo_EQ_Reset_Click(object sender, RoutedEventArgs e)
            {
              // Reset to default
              Filters.Video.FilterVideo_EQ_ResetAll();

              //// Brightness
              //VM.FilterVideoView.FilterVideo_EQ_Brightness_Value = 0;
              //// Contrast
              //VM.FilterVideoView.FilterVideo_EQ_Contrast_Value = 0;
              //// Saturation
              //VM.FilterVideoView.FilterVideo_EQ_Saturation_Value = 0;
              //// Gamma
              //VM.FilterVideoView.FilterVideo_EQ_Gamma_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            /// <summary>
            /// Filter Video - Selective Color Reset
            /// </summary>
            private void btnFilterVideo_SelectiveColorReset_Click(object sender, RoutedEventArgs e)
            {
              // Reset to default
              Filters.Video.FilterVideo_SelectiveColor_ResetAll();

              //VideoControls.AutoCopyVideoCodec("control");
            }

            /// <summary>
            /// Video Color Range Combobox
            /// </summary>
            private void cboColorRange_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void cboFilterVideo_SelectiveColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
              // Switch Tab SelectiveColorPreview
              //tabControl_SelectiveColor.SelectedIndex = 0;

              //var selectedItem = (Filters.Video.FilterVideoSelectiveColor)cboFilterVideo_SelectiveColor.SelectedItem;
              //string color = selectedItem.SelectiveColorName;

              string selectedItem = VM.FilterVideoView.FilterVideo_SelectiveColor_SelectedItem;

              // TODO: Fix XAML compilation - controls not being generated
              /*
              switch (selectedItem)
              {
                case "Reds":
                  tabControl_SelectiveColor.SelectedItem = selectedItem;
                  tabItem_SelectiveColor_Reds.IsSelected = true;
                  break;

                case "Yellows":
                  tabControl_SelectiveColor.SelectedItem = selectedItem;
                  tabItem_SelectiveColor_Yellows.IsSelected = true;
                  break;

                case "Greens":
                  tabControl_SelectiveColor.SelectedItem = selectedItem;
                  tabItem_SelectiveColor_Greens.IsSelected = true;
                  break;

                case "Cyans":
                  tabControl_SelectiveColor.SelectedItem = selectedItem;
                  tabItem_SelectiveColor_Cyans.IsSelected = true;
                  break;

                case "Blues":
                  tabControl_SelectiveColor.SelectedItem = selectedItem;
                  tabItem_SelectiveColor_Blues.IsSelected = true;
                  break;

                case "Magentas":
                  tabControl_SelectiveColor.SelectedItem = selectedItem;
                  tabItem_SelectiveColor_Magentas.IsSelected = true;
                  break;

                case "Whites":
                  tabControl_SelectiveColor.SelectedItem = selectedItem;
                  tabItem_SelectiveColor_Whites.IsSelected = true;
                  break;

                case "Neutrals":
                  tabControl_SelectiveColor.SelectedItem = selectedItem;
                  tabItem_SelectiveColor_Neutrals.IsSelected = true;
                  break;

                case "Blacks":
                  tabControl_SelectiveColor.SelectedItem = selectedItem;
                  tabItem_SelectiveColor_Blacks.IsSelected = true;
                  break;
              }
              */
            }

            private void slFilterVideo_EQ_Brightness_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_EQ_Brightness_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_EQ_Brightness_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_EQ_Contrast_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_EQ_Contrast_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_EQ_Contrast_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_EQ_Gamma_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_EQ_Gamma_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_EQ_Gamma_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_EQ_Saturation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_EQ_Saturation_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_EQ_Saturation_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Blacks_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Cyan_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Blacks_Cyan_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Blacks_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Magenta_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Blacks_Magenta_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Blacks_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Blacks_Yellow_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Blacks_Yellow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Blues_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Cyan_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Blues_Cyan_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Blues_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Magenta_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Blues_Magenta_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Blues_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Blues_Yellow_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Blues_Yellow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Cyans_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Cyan_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Cyans_Cyan_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Cyans_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Magenta_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Cyans_Magenta_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Cyans_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Cyans_Yellow_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Cyans_Yellow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Greens_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Cyan_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Greens_Cyan_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Greens_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Magenta_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Greens_Magenta_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Greens_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Greens_Yellow_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Greens_Yellow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Magentas_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Cyan_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Magentas_Cyan_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Magentas_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Magenta_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Magentas_Magenta_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Magentas_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Magentas_Yellow_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Magentas_Yellow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Neutrals_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Cyan_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Neutrals_Cyan_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Neutrals_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Magenta_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Neutrals_Magenta_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Neutrals_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Neutrals_Yellow_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Neutrals_Yellow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Reds_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Cyan_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Reds_Cyan_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Reds_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Magenta_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Reds_Magenta_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Reds_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Reds_Yellow_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Reds_Yellow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Whites_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Cyan_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Whites_Cyan_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Whites_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Magenta_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Whites_Magenta_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Whites_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Whites_Yellow_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Whites_Yellow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Yellows_Cyan_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Cyan_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Yellows_Cyan_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Yellows_Magenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Magenta_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Yellows_Magenta_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Yellows_Yellow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
              // Reset to default
              VM.FilterVideoView.FilterVideo_SelectiveColor_Yellows_Yellow_Value = 0;

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void slFilterVideo_SelectiveColor_Yellows_Yellow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_EQ_Brightness_KeyDown(object sender, KeyEventArgs e)
            {
              MainWindow.Allow_Only_Number_Keys(e);
            }

            private void tbxFilterVideo_EQ_Brightness_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              // Reset Empty to 0
              // TODO: Fix XAML compilation - tbxFilterVideo_EQ_Brightness .g.cs not generated
              /*
              if (string.IsNullOrWhiteSpace(tbxFilterVideo_EQ_Brightness.Text))
              {
                VM.FilterVideoView.FilterVideo_EQ_Brightness_Value = 0;
              }
              */

              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_EQ_Contrast_KeyDown(object sender, KeyEventArgs e)
            {
              MainWindow.Allow_Only_Number_Keys(e);
            }

            private void tbxFilterVideo_EQ_Contrast_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_EQ_Gamma_KeyDown(object sender, KeyEventArgs e)
            {
              MainWindow.Allow_Only_Number_Keys(e);
            }

            private void tbxFilterVideo_EQ_Gamma_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_EQ_Saturation_KeyDown(object sender, KeyEventArgs e)
            {
              MainWindow.Allow_Only_Number_Keys(e);
            }

            private void tbxFilterVideo_EQ_Saturation_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Blacks_Cyan_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Blacks_Magenta_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Blacks_Yellow_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Blues_Cyan_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Blues_Magenta_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Blues_Yellow_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Cyans_Cyan_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Cyans_Magenta_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Cyans_Yellow_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Greens_Cyan_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Greens_Magenta_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Greens_Yellow_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Magentas_Cyan_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Magentas_Magenta_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Magentas_Yellow_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Neutrals_Cyan_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Neutrals_Magenta_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Neutrals_Yellow_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Reds_Cyan_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Whites_Cyan_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Whites_Magenta_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Whites_Yellow_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Yellows_Cyan_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Yellows_Magenta_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

            private void tbxFilterVideo_SelectiveColor_Yellows_Yellow_PreviewKeyUp(object sender, KeyEventArgs e)
            {
              //VideoControls.AutoCopyVideoCodec("control");
            }

    }
}
