using System.Windows;
using System.Windows.Controls.Primitives;

namespace Axiom.Shared.Controls
{
  /// <summary>
  /// Opens a dropdown popup with the same gap to its field whether it goes below or, near the bottom of the
  /// screen, above it.
  /// </summary>
  /// <remarks>
  /// The popup content keeps room for its shadow below the card (ShadowRoom). Plain Bottom placement puts
  /// that room between card and field when the popup flips above, and leaves no gap when it opens below.
  /// Here the popup offers two custom placements, below then above, each offset so the card itself sits Gap
  /// away from the field; WPF takes the first that fits on screen.
  /// </remarks>
  public static class DropDownPlacement
  {
    public static readonly DependencyProperty GapProperty =
      DependencyProperty.RegisterAttached("Gap", typeof(double), typeof(DropDownPlacement),
        new PropertyMetadata(double.NaN, OnGapChanged));

    public static readonly DependencyProperty ShadowRoomProperty =
      DependencyProperty.RegisterAttached("ShadowRoom", typeof(double), typeof(DropDownPlacement), new PropertyMetadata(0.0));

    public static double GetGap(DependencyObject popup) => (double)popup.GetValue(GapProperty);

    public static void SetGap(DependencyObject popup, double value) => popup.SetValue(GapProperty, value);

    public static double GetShadowRoom(DependencyObject popup) => (double)popup.GetValue(ShadowRoomProperty);

    public static void SetShadowRoom(DependencyObject popup, double value) => popup.SetValue(ShadowRoomProperty, value);

    private static void OnGapChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is not Popup popup)
        return;

      popup.Placement = PlacementMode.Custom;
      popup.CustomPopupPlacementCallback = (popupSize, targetSize, _) =>
      {
        // The sizes come in device pixels; the gap and shadow room are in DIPs
        var target = popup.PlacementTarget as FrameworkElement;
        var scale = target is { ActualHeight: > 0 } ? targetSize.Height / target.ActualHeight : 1.0;
        var gap = GetGap(popup) * scale;
        var shadow = GetShadowRoom(popup) * scale;

        var below = new CustomPopupPlacement(new Point(0, targetSize.Height + gap), PopupPrimaryAxis.None);
        var above = new CustomPopupPlacement(new Point(0, shadow - popupSize.Height - gap), PopupPrimaryAxis.None);
        if (target == null || PresentationSource.FromVisual(target) == null)
          return new[] { below, above };

        // Pick the side ourselves: left to choose, WPF slides the popup up over the field instead of flipping it
        var top = target.PointToScreen(new Point(0, 0)).Y;
        var screen = System.Windows.Forms.Screen.FromPoint(new System.Drawing.Point(
          (int)target.PointToScreen(new Point(0, 0)).X, (int)top)).WorkingArea;
        var fitsBelow = top + targetSize.Height + gap + popupSize.Height <= screen.Bottom;
        var roomAbove = top - screen.Top;
        var roomBelow = screen.Bottom - (top + targetSize.Height);
        return new[] { fitsBelow || roomBelow >= roomAbove ? below : above };
      };
    }
  }
}
