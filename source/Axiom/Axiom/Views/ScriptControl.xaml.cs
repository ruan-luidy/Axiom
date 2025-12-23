using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ViewModel;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Axiom.Views
{
  public partial class ScriptControl : UserControl
  {
    public ScriptControl()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Script View Drag and Drop
    /// </summary>
    private void tbxScriptView_PreviewDragOver(object sender, DragEventArgs e)
    {
      try
      {
        e.Handled = true;
        e.Effects = DragDropEffects.Copy;
      }
      catch (IOException ex)
      {
        MessageBox.Show(ex.ToString(),
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
      }
    }

    private void tbxScriptView_PreviewDrop(object sender, DragEventArgs e)
    {
      try
      {
        var buffer = e.Data.GetData(DataFormats.FileDrop, false) as string[];

        if (buffer != null && buffer.Length == 0) // prevents crash and drag and dropping in-scriptview text
        {
          string file = buffer.First();
          string ext = Path.GetExtension(file);

          // Only accept txt files
          if (ext == ".txt")
          {
            VM.MainView.ScriptView_Text = File.ReadAllText(file);
          }
        }
      }
      catch (IOException ex)
      {
        MessageBox.Show(ex.ToString(),
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
      }
    }

    // TODO: Event Handlers from XAML (not yet implemented in MainWindow.xaml.cs)
    // TODO: btnScriptSave_Click
    // TODO: btnScriptLoad_Click
    // TODO: btnScriptCopy_Click
    // TODO: btnScriptSort_Click
    // TODO: btnScriptClear_Click
    // TODO: btnScript_Click
    // TODO: btnScriptRun_Click
  }
}
