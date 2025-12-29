using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using ViewModel;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Axiom.Views
{
    public partial class ToolbarControl : UserControl
    {
        // Window instances
        private InfoWindow infowindow;
        private DebugConsole debugconsole;
        private FilePropertiesWindow filepropwindow;

        // Window state flags
        private Boolean IsInfoWindowOpened = false;
        private Boolean IsDebugConsoleOpened = false;
        private Boolean IsFilePropertiesOpened = false;

        public ToolbarControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Info Button
        /// </summary>
        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            // Prevent Monitor Resolution Window Crash
            //
            try
            {
                // Check if Window is already open
                if (IsInfoWindowOpened) return;

                // Start Window
                infowindow = new InfoWindow();

                // Only allow 1 Window instance
                infowindow.ContentRendered += delegate { IsInfoWindowOpened = true; };
                infowindow.Closed += delegate { IsInfoWindowOpened = false; };

                // Keep Window on Top
                infowindow.Owner = HandyControl.Controls.Window.GetWindow(this);

                // Detect which screen we're on
                var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
                var thisScreen = allScreens.SingleOrDefault(s => infowindow.Owner.Left >= s.WorkingArea.Left && infowindow.Owner.Left < s.WorkingArea.Right);
                if (thisScreen == null) thisScreen = allScreens.First();

                // Position Relative to MainWindow
                infowindow.Left = Math.Max((infowindow.Owner.Left + (infowindow.Owner.Width - infowindow.Width) / 2), thisScreen.WorkingArea.Left);
                infowindow.Top = Math.Max((infowindow.Owner.Top + (infowindow.Owner.Height - infowindow.Height) / 2), thisScreen.WorkingArea.Top);

                // Open Window
                infowindow.Show();
            }
            // Simplified
            catch
            {
                // Check if Window is already open
                if (IsInfoWindowOpened) return;

                // Start Window
                infowindow = new InfoWindow();

                // Only allow 1 Window instance
                infowindow.ContentRendered += delegate { IsInfoWindowOpened = true; };
                infowindow.Closed += delegate { IsInfoWindowOpened = false; };

                // Keep Window on Top
                infowindow.Owner = HandyControl.Controls.Window.GetWindow(this);

                // Position Relative to MainWindow
                var owner = infowindow.Owner;
                infowindow.Left = Math.Max((owner.Left + (owner.Width - infowindow.Width) / 2), owner.Left);
                infowindow.Top = Math.Max((owner.Top + (owner.Height - infowindow.Height) / 2), owner.Top);

                // Open Window
                infowindow.Show();
            }
        }

        /// <summary>
        /// Website Button
        /// </summary>
        private void btbWebsite_Click(object sender, RoutedEventArgs e)
        {
            // Open Axiom Website URL in Default Browser
            Process.Start("https://axiomui.github.io");

        }

        /// <summary>
        /// Keep Window - Toggle - Checked
        /// </summary>
        private void tglCMDWindowKeep_Checked(object sender, RoutedEventArgs e)
        {
            //// Log Console Message /////////
            //Log.logParagraph.Inlines.Add(new LineBreak());
            //Log.logParagraph.Inlines.Add(new LineBreak());
            //Log.logParagraph.Inlines.Add(new Bold(new Run("Keep FFmpeg Window Toggle: ")) { Foreground = Log.ConsoleDefault });
            //Log.logParagraph.Inlines.Add(new Run("On") { Foreground = Log.ConsoleDefault });
        }
        /// <summary>
        /// Keep Window - Toggle - Unchecked
        /// </summary>
        private void tglCMDWindowKeep_Unchecked(object sender, RoutedEventArgs e)
        {
            //// Log Console Message /////////
            //Log.logParagraph.Inlines.Add(new LineBreak());
            //Log.logParagraph.Inlines.Add(new LineBreak());
            //Log.logParagraph.Inlines.Add(new Bold(new Run("Keep FFmpeg Window Toggle: ")) { Foreground = Log.ConsoleDefault });
            //Log.logParagraph.Inlines.Add(new Run("Off") { Foreground = Log.ConsoleDefault });
        }

        /// <summary>
        /// Auto Sort Script - Toggle - Checked
        /// </summary>
        private void tglAutoSortScript_Checked(object sender, RoutedEventArgs e)
        {
            //// Log Console Message /////////
            //Log.logParagraph.Inlines.Add(new LineBreak());
            //Log.logParagraph.Inlines.Add(new LineBreak());
            //Log.logParagraph.Inlines.Add(new Bold(new Run("Auto Sort Script Toggle: ")) { Foreground = Log.ConsoleDefault });
            //Log.logParagraph.Inlines.Add(new Run("On") { Foreground = Log.ConsoleDefault });
        }
        /// <summary>
        /// Auto Sort Script - Toggle - Unchecked
        /// </summary>
        private void tglAutoSortScript_Unchecked(object sender, RoutedEventArgs e)
        {
            //// Log Console Message /////////
            //Log.logParagraph.Inlines.Add(new LineBreak());
            //Log.logParagraph.Inlines.Add(new LineBreak());
            //Log.logParagraph.Inlines.Add(new Bold(new Run("Auto Sort Script Toggle: ")) { Foreground = Log.ConsoleDefault });
            //Log.logParagraph.Inlines.Add(new Run("Off") { Foreground = Log.ConsoleDefault });
        }

        /// <summary>
        /// Debug Console Window Button
        /// </summary>
        private void btnDebugConsole_Click(object sender, RoutedEventArgs e)
        {
            // Prevent Monitor Resolution Window Crash
            //
            try
            {
                // Check if Window is already open
                if (IsDebugConsoleOpened) return;

                // Start Window
                var mainWindow = HandyControl.Controls.Window.GetWindow(this) as MainWindow;
                debugconsole = new DebugConsole(mainWindow);

                // Only allow 1 Window instance
                debugconsole.ContentRendered += delegate { IsDebugConsoleOpened = true; };
                debugconsole.Closed += delegate { IsDebugConsoleOpened = false; };

                // Detect which screen we're on
                var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
                var thisScreen = allScreens.SingleOrDefault(s => mainWindow.Left >= s.WorkingArea.Left && mainWindow.Left < s.WorkingArea.Right);
                if (thisScreen == null) thisScreen = allScreens.First();


                // Position Relative to MainWindow
                // Keep from going off screen
                debugconsole.Left = Math.Max(mainWindow.Left - debugconsole.Width - 12, thisScreen.WorkingArea.Left);
                debugconsole.Top = Math.Max(mainWindow.Top - 0, thisScreen.WorkingArea.Top);

                // Write Variables to Debug Window (Method)
                //DebugConsole.DebugWrite(debugconsole, MainView.vm);

                // Open Window
                debugconsole.Show();
            }
            // Simplified
            catch
            {
                // Check if Window is already open
                if (IsDebugConsoleOpened) return;

                // Start Window
                var mainWindow = HandyControl.Controls.Window.GetWindow(this) as MainWindow;
                debugconsole = new DebugConsole(mainWindow);

                // Only allow 1 Window instance
                debugconsole.ContentRendered += delegate { IsDebugConsoleOpened = true; };
                debugconsole.Closed += delegate { IsDebugConsoleOpened = false; };

                // Position Relative to MainWindow
                // Keep from going off screen
                debugconsole.Left = mainWindow.Left - debugconsole.Width - 12;
                debugconsole.Top = mainWindow.Top;

                // Write Variables to Debug Window (Method)
                //DebugConsole.DebugWrite(debugconsole, MainView.vm);

                // Open Window
                debugconsole.Show();
            }
        }

        /// <summary>
        /// Log Console Window Button
        /// </summary>
        private void btnLogConsole_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = HandyControl.Controls.Window.GetWindow(this) as MainWindow;
            var logconsole = mainWindow.logconsole;

            // Prevent Monitor Resolution Window Crash
            //
            try
            {
                // Detect which screen we're on
                var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
                var thisScreen = allScreens.SingleOrDefault(s => mainWindow.Left >= s.WorkingArea.Left && mainWindow.Left < s.WorkingArea.Right);
                if (thisScreen == null) thisScreen = allScreens.First();

                // Position Relative to MainWindow
                // Keep from going off screen
                logconsole.Left = Math.Min(mainWindow.Left + mainWindow.ActualWidth + 12, thisScreen.WorkingArea.Right - logconsole.Width);
                logconsole.Top = Math.Min(mainWindow.Top + 0, thisScreen.WorkingArea.Bottom - logconsole.Height);

                // Open Winndow
                logconsole.Show();
            }
            // Simplified
            catch
            {
                // Position Relative to MainWindow
                // Keep from going off screen
                logconsole.Left = mainWindow.Left + mainWindow.ActualWidth + 12;
                logconsole.Top = mainWindow.Top;

                // Open Winndow
                logconsole.Show();
            }
        }

        /// <summary>
        /// Log Button
        /// </summary>
        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            // Call Method to get Log Path
            //Log.DefineLogPath();
            if (VM.ConfigureView.LogCheckBox_IsChecked == true)
            {
                if (string.IsNullOrWhiteSpace(VM.ConfigureView.LogPath_Text))
                {
                    VM.ConfigureView.LogPath_Text = Log.axiomLogDir;
                }
            }

            //MessageBox.Show(Configure.logPath.ToString()); //debug

            // Open Log
            if (File.Exists(VM.ConfigureView.LogPath_Text + "output.log"))
            {
                Process.Start("notepad.exe", "\"" + VM.ConfigureView.LogPath_Text + "output.log" + "\"");
            }
            else
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: Output Log has not been created yet.")) { Foreground = Log.ConsoleWarning });

                MessageBox.Show("Output Log has not been created yet.",
                                "Notice",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// CMD Button
        /// </summary>
        private void btnCmd_Click(object sender, RoutedEventArgs e)
        {
            // -------------------------
            // Launch Shell
            // -------------------------
            // Default to User Profile Diretory
            switch (VM.ConfigureView.Shell_SelectedItem)
            {
                // CMD
                case "CMD":
                    Process.Start("CMD.exe", "/k cd %userprofile%");
                    break;

                // PowerShell
                case "PowerShell":
                    Process.Start("PowerShell.exe", "-NoExit cd $home");
                    break;
            }
        }

        /// <summary>
        /// File Properties Button
        /// </summary>
        private void btnProperties_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = HandyControl.Controls.Window.GetWindow(this) as MainWindow;

            // Prevent Monitor Resolution Window Crash
            //
            try
            {
                // Check if Window is already open
                if (IsFilePropertiesOpened) return;

                // Start window
                //MainWindow mainwindow = this;
                filepropwindow = new FilePropertiesWindow(mainWindow);

                // Only allow 1 Window instance
                filepropwindow.ContentRendered += delegate { IsFilePropertiesOpened = true; };
                filepropwindow.Closed += delegate { IsFilePropertiesOpened = false; };

                // Detect which screen we're on
                var allScreens = System.Windows.Forms.Screen.AllScreens.ToList();
                var thisScreen = allScreens.SingleOrDefault(s => mainWindow.Left >= s.WorkingArea.Left && mainWindow.Left < s.WorkingArea.Right);
                if (thisScreen == null) thisScreen = allScreens.First();

                // Position Relative to MainWindow
                // Keep from going off screen
                filepropwindow.Left = Math.Max((mainWindow.Left + (mainWindow.Width - filepropwindow.Width) / 2), thisScreen.WorkingArea.Left);
                filepropwindow.Top = Math.Max((mainWindow.Top + (mainWindow.Height - filepropwindow.Height) / 2), thisScreen.WorkingArea.Top);

                // Write Properties to Textbox in FilePropertiesWindow Initialize

                // Open Window
                filepropwindow.Show();
            }
            // Simplified
            catch
            {
                // Check if Window is already open
                if (IsFilePropertiesOpened) return;

                // Start window
                filepropwindow = new FilePropertiesWindow(mainWindow);

                // Only allow 1 Window instance
                filepropwindow.ContentRendered += delegate { IsFilePropertiesOpened = true; };
                filepropwindow.Closed += delegate { IsFilePropertiesOpened = false; };

                // Position Relative to MainWindow
                // Keep from going off screen
                filepropwindow.Left = Math.Max((mainWindow.Left + (mainWindow.Width - filepropwindow.Width) / 2), mainWindow.Left);
                filepropwindow.Top = Math.Max((mainWindow.Top + (mainWindow.Height - filepropwindow.Height) / 2), mainWindow.Top);

                // Write Properties to Textbox in FilePropertiesWindow Initialize

                // Open Window
                filepropwindow.Show();
            }
        }

        /// <summary>
        /// Play File Button
        /// </summary>
        private void btnPlayFile_Click(object sender, RoutedEventArgs e)
        {
            string output = MainWindow.output;

            if (File.Exists(@output))
            {
                Process.Start("\"" + output + "\"");
            }
            else
            {
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new LineBreak());
                Log.logParagraph.Inlines.Add(new Bold(new Run("Notice: File does not yet exist.")) { Foreground = Log.ConsoleWarning });

                MessageBox.Show("File does not yet exist.",
                                "Notice",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Update Button
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Call the MainWindow method
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                // Use reflection to call private method
                var methodInfo = typeof(MainWindow).GetMethod("btnUpdate_Click",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (methodInfo != null)
                {
                    methodInfo.Invoke(mainWindow, new object[] { sender, e });
                }
            }
        }
    }
}
