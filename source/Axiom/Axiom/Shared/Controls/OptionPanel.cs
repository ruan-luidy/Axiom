using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Axiom.Shared.Controls
{
  /// <summary>
  /// One group of options: a card with an icon, a small uppercase header and its rows.
  /// </summary>
  /// <remarks>
  /// <code>
  ///   &lt;controls:OptionPanel Header="QUALITY" Icon="{icons:Phosphor GaugeBold}"&gt;
  ///     &lt;StackPanel&gt;
  ///       &lt;controls:OptionRow Label="Bitrate"&gt;...&lt;/controls:OptionRow&gt;
  ///     &lt;/StackPanel&gt;
  ///   &lt;/controls:OptionPanel&gt;
  /// </code>
  /// The pages put their panels in a MasonryPanel, so they stay narrow and pack into as many columns as fit.
  /// </remarks>
  public class OptionPanel : HeaderedContentControl
  {
    public static readonly DependencyProperty IconProperty =
      DependencyProperty.Register(nameof(Icon), typeof(Geometry), typeof(OptionPanel));

    public Geometry Icon
    {
      get => (Geometry)GetValue(IconProperty);
      set => SetValue(IconProperty, value);
    }
  }
}
