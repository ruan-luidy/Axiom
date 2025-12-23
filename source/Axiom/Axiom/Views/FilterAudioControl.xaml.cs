using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel;

namespace Axiom.Views
{
  public partial class FilterAudioControl : UserControl
  {
    public FilterAudioControl()
    {
      InitializeComponent();
    }

    private void slFilterAudio_Contrast_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      // Reset to default
      VM.FilterAudioView.FilterAudio_Contrast_Value = 0;

      //AudioControls.AutoCopyAudioCodec("control");
    }

    private void slFilterAudio_Contrast_PreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
      //AudioControls.AutoCopyAudioCodec("control");
    }

    private void slFilterAudio_ExtraStereo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      // Reset to default
      VM.FilterAudioView.FilterAudio_ExtraStereo_Value = 0;

      //AudioControls.AutoCopyAudioCodec("control");
    }

    private void slFilterAudio_ExtraStereo_PreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
      //AudioControls.AutoCopyAudioCodec("control");
    }

    private void slFilterAudio_Tempo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      // Reset to default
      VM.FilterAudioView.FilterAudio_Tempo_Value = 100;

      //AudioControls.AutoCopyAudioCodec("control");
    }

    private void slFilterAudio_Tempo_PreviewMouseUp(object sender, MouseButtonEventArgs e)
    {
      //AudioControls.AutoCopyAudioCodec("control");
    }

    private void tbxFilterAudio_Contrast_KeyDown(object sender, KeyEventArgs e)
    {
      Allow_Only_Number_Keys(e);
    }

    private void tbxFilterAudio_Contrast_PreviewKeyUp(object sender, KeyEventArgs e)
    {
      //AudioControls.AutoCopyAudioCodec("control");
    }

    private void tbxFilterAudio_ExtraStereo_KeyDown(object sender, KeyEventArgs e)
    {
      Allow_Only_Number_Keys(e);
    }

    private void tbxFilterAudio_ExtraStereo_PreviewKeyUp(object sender, KeyEventArgs e)
    {
      //AudioControls.AutoCopyAudioCodec("control");
    }

    private void tbxFilterAudio_Tempo_KeyDown(object sender, KeyEventArgs e)
    {
      Allow_Only_Number_Keys(e);
    }

    private void tbxFilterAudio_Tempo_PreviewKeyUp(object sender, KeyEventArgs e)
    {
      //AudioControls.AutoCopyAudioCodec("control");
    }

  }
}
