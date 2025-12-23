/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017-2021 Matt McManis
https://github.com/MattMcManis/Axiom
https://axiomui.github.io
mattmcmanis@outlook.com

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.If not, see <http://www.gnu.org/licenses/>.
---------------------------------------------------------------------- */

using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Axiom
{
  public partial class MainWindow : HandyControl.Controls.Window
  {
    /// <summary>
    /// Config Directory Open - Helper Method
    /// </summary>
    public static void ConfigDirectoryOpen(string path)
    {
      if (Directory.Exists(path))
      {
        Process.Start("explorer.exe", path);
      }
      else
      {
        MessageBoxResult resultExport = MessageBox.Show(
          "The Axiom UI directory does not exist in this location yet. Automatically create it?",
          "Directory Not Found",
          MessageBoxButton.YesNo,
          MessageBoxImage.Information);

        switch (resultExport)
        {
          case MessageBoxResult.Yes:
            try
            {
              Directory.CreateDirectory(path);
              Process.Start("explorer.exe", path);
            }
            catch
            {
              MessageBox.Show("Could not create directory. May require Administrator privileges.",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
            break;
          case MessageBoxResult.No:
            break;
        }
      }
    }

    /// <summary>
    /// Move Config File - Helper Method
    /// </summary>
    public void confMove(string source, string target)
    {
      if (File.Exists(target))
      {
        File.Delete(target);
      }
      File.Move(source, target);
    }

    /// <summary>
    /// Move Log File - Helper Method
    /// </summary>
    public void logMove(string source, string target)
    {
      if (File.Exists(target))
      {
        File.Delete(target);
      }
      File.Move(source, target);
    }
  }
}
