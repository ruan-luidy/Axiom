using System.Windows;
using System.Windows.Controls.Primitives;

namespace Axiom.Shared.Controls
{
  /// <summary>
  /// Opens a dropdown popup with the same gap to its field whether it goes below or, near the bottom of the
  /// screen, above it.
  /// </summary>
  /// <remarks>
  /// The popup picks the side itself from the monitor's work area (left to choose, WPF keeps the popup below
  /// and slides it up over the field) and offsets the card Gap away from the field.
  ///
  /// The popup content keeps a margin around the card for its shadow (ShadowRoom, the same Thickness as that
  /// margin): a popup draws nothing outside its bounds. The size WPF hands the callback is the card's, without
  /// that margin, while the point it gets back places the margin's corner, so the placement moves back by the
  /// margin to keep the card itself lined up with the field.
  /// </remarks>
  public static class DropDownPlacement
  {
    public static readonly DependencyProperty GapProperty =
      DependencyProperty.RegisterAttached("Gap", typeof(double), typeof(DropDownPlacement),
        new PropertyMetadata(double.NaN, OnGapChanged));

    public static readonly DependencyProperty ShadowRoomProperty =
      DependencyProperty.RegisterAttached("ShadowRoom", typeof(Thickness), typeof(DropDownPlacement), new PropertyMetadata(default(Thickness)));

    public static double GetGap(DependencyObject popup) => (double)popup.GetValue(GapProperty);

    public static void SetGap(DependencyObject popup, double value) => popup.SetValue(GapProperty, value);

    public static Thickness GetShadowRoom(DependencyObject popup) => (Thickness)popup.GetValue(ShadowRoomProperty);

    public static void SetShadowRoom(DependencyObject popup, Thickness value) => popup.SetValue(ShadowRoomProperty, value);

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
        var room = GetShadowRoom(popup);
        var (left, top, bottom) = (room.Left * scale, room.Top * scale, room.Bottom * scale);

        var below = new CustomPopupPlacement(new Point(-left, targetSize.Height + gap - top), PopupPrimaryAxis.None);
        var above = new CustomPopupPlacement(new Point(-left, -popupSize.Height - gap - top), PopupPrimaryAxis.None);
        if (target == null || PresentationSource.FromVisual(target) == null)
          return new[] { below, above };

        var origin = target.PointToScreen(new Point(0, 0));
        var screen = System.Windows.Forms.Screen.FromPoint(new System.Drawing.Point((int)origin.X, (int)origin.Y)).WorkingArea;
        var fitsBelow = origin.Y + targetSize.Height + gap + popupSize.Height + bottom <= screen.Bottom;
        var roomAbove = origin.Y - screen.Top;
        var roomBelow = screen.Bottom - (origin.Y + targetSize.Height);
        return new[] { fitsBelow || roomBelow >= roomAbove ? below : above };
      };
    }
  }
}
