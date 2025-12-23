using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel;

namespace Axiom.Views
{
  public partial class FilterVideoControl : UserControl
  {
    public FilterVideoControl()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Filter Video - Deband
    /// </summary>
    private void cboFilterVideo_Deband_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      //VideoControls.AutoCopyVideoCodec("control");
    }

    /// <summary>
    /// Filter Video - Deblock
    /// </summary>
    private void cboFilterVideo_Deblock_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      //VideoControls.AutoCopyVideoCodec("control");
    }

    /// <summary>
    /// Filter Video - Deflicker
    /// </summary>
    private void cboFilterVideo_Deflicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      //VideoControls.AutoCopyVideoCodec("control");
    }

    /// <summary>
    /// Filter Video - Deinterlace
    /// </summary>
    private void cboFilterVideo_Deinterlace_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      //VideoControls.AutoCopyVideoCodec("control");
    }

    /// <summary>
    /// Filter Video - Dejudder
    /// </summary>
    private void cboFilterVideo_Dejudder_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      //VideoControls.AutoCopyVideoCodec("control");
    }

    /// <summary>
    /// Filter Video - Denoise
    /// </summary>
    private void cboFilterVideo_Denoise_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      //VideoControls.AutoCopyVideoCodec("control");
    }

    /// <summary>
    /// Filter Video - Deshake
    /// </summary>
    private void cboFilterVideo_Deshake_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      //VideoControls.AutoCopyVideoCodec("control");
    }

    /// <summary>
    /// Filter Video - Drop Frames
    /// </summary>
    private void cboFilterVideo_DropFrames_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    /// <summary>
    /// Filter Video - Flip
    /// </summary>
    private void cboFilterVideo_Flip_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      //VideoControls.AutoCopyVideoCodec("control");
    }

    /// <summary>
    /// Filter Video - Rotate
    /// </summary>
    private void cboFilterVideo_Rotate_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      //VideoControls.AutoCopyVideoCodec("control");
    }

  }
}
