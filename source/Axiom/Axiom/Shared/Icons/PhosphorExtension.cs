using System;
using System.Collections.Concurrent;
using System.Windows.Markup;
using System.Windows.Media;
using MahApps.Metro.IconPacks;

namespace Axiom.Shared.Icons
{
  /// <summary>
  /// A Phosphor icon as a Geometry, for use straight in XAML:
  ///
  ///   <Path Data="{icons:Phosphor GearSixBold}" Style="{StaticResource Icon}" />
  ///
  /// The package draws its icons through a control; here each one is created once, its path parsed into a
  /// frozen Geometry and cached, so every window shares the same instance. Names follow Phosphor with the
  /// weight at the end (GearSixBold, PlayFill, ...).
  /// </summary>
  [MarkupExtensionReturnType(typeof(Geometry))]
  public sealed class PhosphorExtension : MarkupExtension
  {
    private static readonly ConcurrentDictionary<PackIconPhosphorIconsKind, Geometry> Cache = new();

    public PhosphorExtension()
    {
    }

    public PhosphorExtension(PackIconPhosphorIconsKind kind)
    {
      Kind = kind;
    }

    [ConstructorArgument("kind")]
    public PackIconPhosphorIconsKind Kind { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider) => Get(Kind);

    public static Geometry Get(PackIconPhosphorIconsKind kind) => Cache.GetOrAdd(kind, k =>
    {
      // The control fills Data from the package's dictionary in its constructor; it never has to be shown
      var data = new PackIconPhosphorIcons { Kind = k }.Data;
      if (string.IsNullOrEmpty(data))
        return Geometry.Empty;

      var geometry = Geometry.Parse(data);
      geometry.Freeze();
      return geometry;
    });
  }
}
