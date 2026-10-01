using System.Windows;
using System.Windows.Controls;

namespace Axiom
{
  /// <summary>
  /// How the main window adapts to its width: the sidebar becomes an icon rail, and the script moves from
  /// beside the options to under them.
  /// </summary>
  public partial class MainWindow
  {
    // Below this the sidebar shows icons only
    private const double CompactBelow = 1000;

    // From this width up the script sits beside the options
    private const double ScriptBesideFrom = 1180;

    public static readonly DependencyProperty IsCompactProperty =
      DependencyProperty.Register(nameof(IsCompact), typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

    public bool IsCompact
    {
      get => (bool)GetValue(IsCompactProperty);
      set => SetValue(IsCompactProperty, value);
    }

    private bool ScriptBeside => ScriptColumn.Width.Value > 0;

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e) => ApplyLayout(e.NewSize.Width);

    private void ApplyLayout(double width)
    {
      IsCompact = width < CompactBelow;
      Sidebar.Width = IsCompact ? 56 : 196;

      var beside = width >= ScriptBesideFrom;
      if (beside == ScriptBeside)
        return;

      OptionsColumn.Width = new GridLength(beside ? 11 : 1, GridUnitType.Star);
      ScriptColumn.Width = beside ? new GridLength(9, GridUnitType.Star) : new GridLength(0);
      OptionsRow.Height = new GridLength(beside ? 1 : 3, GridUnitType.Star);
      ScriptRow.Height = beside ? new GridLength(0) : new GridLength(2, GridUnitType.Star);

      Grid.SetColumn(ScriptCard, beside ? 1 : 0);
      Grid.SetRow(ScriptCard, beside ? 0 : 1);
      ScriptCard.Margin = beside ? new Thickness(12, 0, 0, 0) : new Thickness(0, 12, 0, 0);
    }

    private void Nav_Checked(object sender, RoutedEventArgs e)
    {
      // Checked fires while the XAML is still loading, before Pages exists
      if (Pages != null && sender is FrameworkElement { Tag: string tag } && int.TryParse(tag, out var index))
        Pages.SelectedIndex = index;
    }

    // Read by the load check tool to confirm the layout at a given width
    internal string LayoutReport(double width)
    {
      ApplyLayout(width);
      return $"sidebar {(IsCompact ? "rail" : "full")}, script {(ScriptBeside ? "beside" : "below")}";
    }
  }
}
