using System.Windows;
using System.Windows.Controls;

namespace Axiom.Shared.Controls
{
  /// <summary>
  /// One option: its label on the left, the field filling the rest of the row.
  /// </summary>
  /// <remarks>
  /// Every label column is the same width inside a panel, so the fields of a panel line up. A row with two
  /// fields (a value and a unit, a start and an end) puts a Grid or StackPanel as its content.
  /// </remarks>
  public class OptionRow : ContentControl
  {
    public static readonly DependencyProperty LabelProperty =
      DependencyProperty.Register(nameof(Label), typeof(string), typeof(OptionRow), new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty LabelWidthProperty =
      DependencyProperty.Register(nameof(LabelWidth), typeof(double), typeof(OptionRow), new PropertyMetadata(96.0));

    public string Label
    {
      get => (string)GetValue(LabelProperty);
      set => SetValue(LabelProperty, value);
    }

    public double LabelWidth
    {
      get => (double)GetValue(LabelWidthProperty);
      set => SetValue(LabelWidthProperty, value);
    }
  }
}
