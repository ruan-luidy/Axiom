using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Axiom.Views
{
    /// <summary>
    /// Video encoding settings control
    /// TODO: Move event handlers from MainWindow.xaml.cs to here
    /// </summary>
    public partial class VideoEncodingControl : UserControl
    {
        public VideoEncodingControl()
        {
            InitializeComponent();
        }

        // ====================================================================
        // TODO: COPIAR ESTES MÉTODOS DO MainWindow.xaml.cs
        // ====================================================================
        //
        // - cboVideo_Codec_SelectionChanged
        // - cboEncodeSpeed_SelectionChanged
        // - cboHWAccel_SelectionChanged
        // - cboHWAccelTranscode_SelectionChanged
        // - cboVideo_Quality_SelectionChanged
        // - cboVideo_Pass_SelectionChanged
        // - cboVideo_Pass_DropDownClosed
        // - slVideo_CRF_ValueChanged
        // - slVideo_CRF_MouseDoubleClick
        // - tbxVideo_CRF_KeyDown
        // - tbxVideo_CRF_TextChanged
        // - tglVideo_VBR_Checked
        // - tglVideo_VBR_Unchecked
        // - btnVideoBitRateAdvanced_Expand_Click
        // - expVideo_BitRateAdvanced_Expander_Expanded
        // - expVideo_BitRateAdvanced_Expander_Collapsed
        // - cboVideo_PixelFormat_SelectionChanged
        // - cboVideo_FPS_SelectionChanged
        // - cboVideo_FrameRate_KeyDown
        // - cboVideo_Speed_SelectionChanged
        // - cboVideo_Speed_KeyDown
        // - cboVideo_Vsync_SelectionChanged
        // - cboVideo_Optimize_SelectionChanged
    }
}
