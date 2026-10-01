using System.Windows;
using System.Windows.Input;

namespace Axiom.Shared.Controls
{
  /// <summary>
  /// Stops a list from scrolling when the mouse merely passes over a half-visible row.
  /// </summary>
  /// <remarks>
  /// A ComboBoxItem asks to be brought into view when the mouse enters it. On the half-visible row at the edge
  /// of the dropdown that scrolls the list, which puts the next half-visible row under the cursor, which
  /// scrolls again, so the list runs away on its own. The request is dropped while the row is only hovered;
  /// keyboard navigation and selection still scroll as usual.
  /// </remarks>
  public static class HoverScroll
  {
    public static readonly DependencyProperty IsSuppressedProperty =
      DependencyProperty.RegisterAttached("IsSuppressed", typeof(bool), typeof(HoverScroll),
        new PropertyMetadata(false, OnIsSuppressedChanged));

    public static bool GetIsSuppressed(DependencyObject element) => (bool)element.GetValue(IsSuppressedProperty);

    public static void SetIsSuppressed(DependencyObject element, bool value) => element.SetValue(IsSuppressedProperty, value);

    private static void OnIsSuppressedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is not FrameworkElement element)
        return;

      element.RequestBringIntoView -= OnRequestBringIntoView;
      if ((bool)e.NewValue)
        element.RequestBringIntoView += OnRequestBringIntoView;
    }

    private static void OnRequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
    {
      if (sender is UIElement { IsMouseOver: true } && !IsNavigatingByKeyboard())
        e.Handled = true;
    }

    private static bool IsNavigatingByKeyboard() =>
      Keyboard.IsKeyDown(Key.Up) || Keyboard.IsKeyDown(Key.Down) || Keyboard.IsKeyDown(Key.PageUp) ||
      Keyboard.IsKeyDown(Key.PageDown) || Keyboard.IsKeyDown(Key.Home) || Keyboard.IsKeyDown(Key.End);
  }
}
