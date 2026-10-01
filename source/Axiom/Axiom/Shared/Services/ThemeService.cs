using System.Windows;
using System.Windows.Media;
using HandyControl.Themes;

namespace Axiom.Shared.Services
{
  /// <summary>
  /// Light or dark. Switches HandyControl's palette and the few brushes of our own that follow it.
  /// </summary>
  /// <remarks>
  /// HandyControl's ThemeManager swaps the colour dictionaries under the controls; the control styles stay
  /// merged, so every window keeps its look and only the palette changes.
  ///
  /// The log colours are single brushes that every Run in the log points at. They are left unfrozen and
  /// recoloured in place, so text already written switches with the theme too.
  /// </remarks>
  public static class ThemeService
  {
    public const string Dark = "Dark";
    public const string Light = "Light";

    private static readonly SolidColorBrush LogDefault = new();
    private static readonly SolidColorBrush LogTitle = new();
    private static readonly SolidColorBrush LogWarning = new();
    private static readonly SolidColorBrush LogError = new();
    private static readonly SolidColorBrush LogAction = new();

    public static string Current { get; private set; } = Dark;

    public static bool IsDark => Current == Dark;

    /// <summary>
    /// The names the old versions saved (Axiom, Onyx, Cyberpunk...) map to the nearest of the two.
    /// </summary>
    public static string Normalize(string saved) => saved switch
    {
      Light or "Axiom" or "FFmpeg" or "Cyberpunk" or "Circuit" or "Prelude" or "System" => Light,
      _ => Dark,
    };

    public static void Apply(string theme)
    {
      Current = Normalize(theme);

      Log.ConsoleDefault = LogDefault;
      Log.ConsoleTitle = LogTitle;
      Log.ConsoleWarning = LogWarning;
      Log.ConsoleError = LogError;
      Log.ConsoleAction = LogAction;

      LogDefault.Color = IsDark ? Color.FromRgb(0xE4, 0xE4, 0xE7) : Color.FromRgb(0x27, 0x27, 0x2A);
      LogTitle.Color = IsDark ? Color.FromRgb(0x5B, 0x9B, 0xE0) : Color.FromRgb(0x2B, 0x63, 0xA3);
      LogWarning.Color = IsDark ? Color.FromRgb(0xE3, 0xC0, 0x4A) : Color.FromRgb(0xA1, 0x7A, 0x00);
      LogError.Color = IsDark ? Color.FromRgb(0xF2, 0x6B, 0x5B) : Color.FromRgb(0xC2, 0x36, 0x24);
      LogAction.Color = IsDark ? Color.FromRgb(0x72, 0xC8, 0xD8) : Color.FromRgb(0x1C, 0x7F, 0x92);

      if (Application.Current is null)
        return;

      ThemeManager.Current.ApplicationTheme = IsDark ? ApplicationTheme.Dark : ApplicationTheme.Light;

      var resources = Application.Current.Resources;
      resources["Workspace.BackgroundBrush"] = Frozen(IsDark ? Color.FromRgb(0x11, 0x11, 0x13) : Color.FromRgb(0xF3, 0xF3, 0xF5));
      resources["Hover.RowBrush"] = Frozen(IsDark ? Color.FromArgb(0x0F, 0xFF, 0xFF, 0xFF) : Color.FromArgb(0x0D, 0x00, 0x00, 0x00));
    }

    private static SolidColorBrush Frozen(Color color)
    {
      var brush = new SolidColorBrush(color);
      brush.Freeze();
      return brush;
    }
  }
}
