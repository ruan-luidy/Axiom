using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Axiom.Shared.Controls
{
  /// <summary>
  /// Lays children out in as many columns as fit, each child going to the shortest column.
  /// </summary>
  /// <remarks>
  /// Panels of different heights pack tightly instead of leaving the holes a grid row would, and the number of
  /// columns follows the width: one on a narrow window, two or three as it grows. Collapsed children take no
  /// space, so filtering panels just hides them.
  /// </remarks>
  public class MasonryPanel : Panel
  {
    public static readonly DependencyProperty MinColumnWidthProperty =
      DependencyProperty.Register(nameof(MinColumnWidth), typeof(double), typeof(MasonryPanel),
        new FrameworkPropertyMetadata(340.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public static readonly DependencyProperty SpacingProperty =
      DependencyProperty.Register(nameof(Spacing), typeof(double), typeof(MasonryPanel),
        new FrameworkPropertyMetadata(12.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public static readonly DependencyProperty MaxColumnsProperty =
      DependencyProperty.Register(nameof(MaxColumns), typeof(int), typeof(MasonryPanel),
        new FrameworkPropertyMetadata(3, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public double MinColumnWidth
    {
      get => (double)GetValue(MinColumnWidthProperty);
      set => SetValue(MinColumnWidthProperty, value);
    }

    public double Spacing
    {
      get => (double)GetValue(SpacingProperty);
      set => SetValue(SpacingProperty, value);
    }

    public int MaxColumns
    {
      get => (int)GetValue(MaxColumnsProperty);
      set => SetValue(MaxColumnsProperty, value);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
      var width = double.IsInfinity(availableSize.Width) ? MinColumnWidth : availableSize.Width;
      var (columns, columnWidth) = Columns(width);
      var heights = new double[columns];

      foreach (UIElement child in InternalChildren)
      {
        child.Measure(new Size(columnWidth, double.PositiveInfinity));
        if (child.Visibility == Visibility.Collapsed)
          continue;

        var column = Shortest(heights);
        heights[column] += child.DesiredSize.Height + Spacing;
      }

      return new Size(width, Math.Max(0, heights.Max() - Spacing));
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
      var (columns, columnWidth) = Columns(finalSize.Width);
      var heights = new double[columns];

      foreach (UIElement child in InternalChildren)
      {
        if (child.Visibility == Visibility.Collapsed)
        {
          child.Arrange(new Rect());
          continue;
        }

        var column = Shortest(heights);
        var x = column * (columnWidth + Spacing);
        child.Arrange(new Rect(x, heights[column], columnWidth, child.DesiredSize.Height));
        heights[column] += child.DesiredSize.Height + Spacing;
      }

      return finalSize;
    }

    private (int Columns, double Width) Columns(double width)
    {
      var columns = (int)Math.Floor((width + Spacing) / (MinColumnWidth + Spacing));
      columns = Math.Clamp(columns, 1, Math.Max(1, MaxColumns));
      return (columns, Math.Max(0, (width - Spacing * (columns - 1)) / columns));
    }

    private static int Shortest(double[] heights)
    {
      var index = 0;
      for (var i = 1; i < heights.Length; i++)
      {
        if (heights[i] < heights[index] - 0.5)
          index = i;
      }

      return index;
    }
  }
}
