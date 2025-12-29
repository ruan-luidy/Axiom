using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel;

namespace Axiom.Views
{
  public partial class SettingsFileControl : UserControl
  {
    public SettingsFileControl()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Input FileName Tokens Default
    /// </summary>
    private void btnInputFileNameTokensDefault_Click(object sender, RoutedEventArgs e)
    {
      VM.ConfigureView.InputFileNameTokens_SelectedItem = "Keep";
    }

    /// <summary>
    /// Output FileName Spacing
    /// </summary>
    private void btnOutputFileNameSpacingDefault_Click(object sender, RoutedEventArgs e)
    {
      VM.ConfigureView.OutputFileNameSpacing_SelectedItem = "Original";
    }

    /// <summary>
    /// Output Naming Load Defaults
    /// </summary>
    private void btnOutputNamingDefaults_Click(object sender, RoutedEventArgs e)
    {
      // Deselect All
      // TODO: Fix XAML compilation - lstvOutputNaming .g.cs not generated
      //lstvOutputNaming.SelectedIndex = -1;

      // TODO: OutputNamignDefaults method not implemented
      //OutputNamignDefaults();
    }

    /// <summary>
    /// Output Naming Deselect All
    /// </summary>
    private void btnOutputNaming_DeselectAll_Click(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - lstvOutputNaming .g.cs not generated
      //lstvOutputNaming.SelectedIndex = -1;
    }

    /// <summary>
    /// Output Naming Select All
    /// </summary>
    private void btnOutputNaming_SelectAll_Click(object sender, RoutedEventArgs e)
    {
      // TODO: Fix XAML compilation - lstvOutputNaming .g.cs not generated
      //lstvOutputNaming.SelectAll();
    }

    /// <summary>
    /// Output Naming Sort Down
    /// </summary>
    private void btnOutputNaming_SortDown_Click(object sender, RoutedEventArgs e)
    {
      if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Count > 0)
      {
        var selectedIndex = VM.ConfigureView.OutputNaming_ListView_SelectedIndex;

        if (selectedIndex + 1 < VM.ConfigureView.OutputNaming_ListView_Items.Count)
        {
          // ListView Items
          var itemlsvItems = VM.ConfigureView.OutputNaming_ListView_Items[selectedIndex];
          VM.ConfigureView.OutputNaming_ListView_Items.RemoveAt(selectedIndex);
          VM.ConfigureView.OutputNaming_ListView_Items.Insert(selectedIndex + 1, itemlsvItems);

          // Highlight Selected Index
          VM.ConfigureView.OutputNaming_ListView_SelectedIndex = selectedIndex + 1;
        }
      }
    }

    /// <summary>
    /// Output Naming Sort Up
    /// </summary>
    private void btnOutputNaming_SortUp_Click(object sender, RoutedEventArgs e)
    {
      if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Count > 0)
      {
        var selectedIndex = VM.ConfigureView.OutputNaming_ListView_SelectedIndex;

        if (selectedIndex > 0)
        {
          // ListView Items
          var itemlsvItems = VM.ConfigureView.OutputNaming_ListView_Items[selectedIndex];
          VM.ConfigureView.OutputNaming_ListView_Items.RemoveAt(selectedIndex);
          VM.ConfigureView.OutputNaming_ListView_Items.Insert(selectedIndex - 1, itemlsvItems);

          // Highlight Selected Index
          VM.ConfigureView.OutputNaming_ListView_SelectedIndex = selectedIndex - 1;
        }
      }
    }

    /// <summary>
    /// Output Overwrite Default
    /// </summary>
    private void btnOutputOverwriteDefault_Click(object sender, RoutedEventArgs e)
    {
      VM.ConfigureView.OutputOverwrite_SelectedItem = "Always";
    }

    /// <summary>
    /// Input Filename Tokens - ComboBox
    /// </summary>
    private void cboInputFileNameTokens_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      switch (VM.ConfigureView.InputFileNameTokens_SelectedItem)
      {
        case "Keep":
          VM.ConfigureView.InputFileNameTokensCustom_IsEnabled = false;
          break;

        case "Remove":
          VM.ConfigureView.InputFileNameTokensCustom_IsEnabled = true;
          break;
      }
    }

    /// <summary>
    /// Output Naming ListView
    /// </summary>
    private void lstvOutputNaming_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // If ListView Selected Items Contains Any Items
      // Clear before adding new Selected Items
      if (VM.ConfigureView.OutputNaming_ListView_SelectedItems.Any())
      {
        VM.ConfigureView.OutputNaming_ListView_SelectedItems.Clear();
        VM.ConfigureView.OutputNaming_ListView_SelectedItems.TrimExcess();
      }

      // Create Selected Items List for ViewModel
      //VM.ConfigureView.OutputNaming_ListView_SelectedItems = lstvOutputNaming.SelectedItems
      //                                                                       .Cast<string>()
      //                                                                       .ToList();

      // Remove ListView Items Duplicates
      VM.ConfigureView.OutputNaming_ListView_Items = new ObservableCollection<string>(VM.ConfigureView.OutputNaming_ListView_Items
                                                                                                      .Distinct()
                                                                                                      .ToList()
                                                                                                      .AsEnumerable()
                                                                                                      );
      //VM.ConfigureView.OutputNaming_ListView_Items = VM.ConfigureView.OutputNaming_ListView_Items.Distinct().ToList();

      // Build the list by Order Arranged
      // TODO: Fix XAML compilation - lstvOutputNaming .g.cs not generated
      /*
      for (var i = 0; i < VM.ConfigureView.OutputNaming_ListView_Items.Count; i++)
      {
        if (lstvOutputNaming.SelectedItems
                            .Cast<string>()
                            .ToList()
                            .Contains(VM.ConfigureView.OutputNaming_ListView_Items[i]))
        {
          VM.ConfigureView.OutputNaming_ListView_SelectedItems.Add(VM.ConfigureView.OutputNaming_ListView_Items[i]);
        }
      }
      */

      // Remove ListView Selected Items Duplicates
      VM.ConfigureView.OutputNaming_ListView_SelectedItems = VM.ConfigureView.OutputNaming_ListView_SelectedItems
                                                                             .Distinct()
                                                                             .ToList();
      //MessageBox.Show(string.Join("\n", lstvOutputNaming.SelectedItems.Cast<string>().ToList())); //debug

      // -------------------------
      // Update Ouput Textbox with Name Settings
      // -------------------------
      //OutputPath_UpdateDisplay();
    }

  }
}
