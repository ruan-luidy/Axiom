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

using Axiom.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using ViewModel;
using HandyControl.Controls;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Axiom
{
  public partial class MainWindow : HandyControl.Controls.Window
  {
    // Axiom Current Version
    public static Version currentVersion { get; set; }
    // Axiom GitHub Latest Version
    public static Version latestVersion { get; set; }
    // Alpha, Beta, Stable
    public static string currentBuildPhase = "alpha";
    public static string latestBuildPhase { get; set; }
    public static string[] splitVersionBuildPhase { get; set; }

    // --------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Global Variables
    /// </summary>
    // --------------------------------------------------------------------------------------------------------
    // MainWindow
    //public static double minWidth = 824;
    //public static double minHeight = 464;

    // System
    public readonly static string appRootDir = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + @"\"; // Axiom.exe directory

    public readonly static string commonProgramFilesDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles).TrimEnd('\\') + @"\";
    public readonly static string commonProgramFilesX86Dir = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFilesX86).TrimEnd('\\') + @"\";
    public readonly static string programFilesDir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).TrimEnd('\\') + @"\";
    public readonly static string programFilesX86Dir = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86).TrimEnd('\\') + @"\";
    public readonly static string programFilesX64Dir = @"C:\Program Files\";

    public readonly static string programDataDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData).TrimEnd('\\') + @"\";
    public readonly static string appDataLocalDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).TrimEnd('\\') + @"\";
    public readonly static string appDataRoamingDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).TrimEnd('\\') + @"\";
    public readonly static string tempDir = Path.GetTempPath(); // Windows AppData Temp Directory

    public readonly static string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).TrimEnd('\\') + @"\";
    public readonly static string documentsDir = userProfile + @"Documents\"; // C:\Users\Example\Documents\
    public readonly static string videosDir = userProfile + @"Videos\"; // C:\Users\Example\Videos\
    public readonly static string downloadDir = userProfile + @"Downloads\"; // C:\Users\Example\Downloads\

    public readonly static string logAppRootPath = appRootDir + "axiom.log";
    public readonly static string logAppDataLocalPath = appDataLocalDir + @"Axiom UI\axiom.log";
    public readonly static string logAppDataRoamingPath = appDataRoamingDir + @"Axiom UI\axiom.log";

    // Programs
    public static string youtubedl { get; set; } // youtube-dl.exe

    // Input
    public static string inputPreviousPath { get; set; }
    public static string inputDir { get; set; } // Input File Directory
    public static string inputFileName { get; set; } // (eg. myvideo.mp4 = myvideo)
    public static string inputExt { get; set; } // (eg. .mp4)
    public static string input { get; set; } // Single: Input Path + Filename No Ext + Input Ext (Browse Text Box) /// Batch: Input Path (Browse Text Box)

    // Output
    public static string outputPreviousPath { get; set; }
    public static string outputDir { get; set; } // Output Path
    public static string outputFileName { get; set; } // Output Directory + Filename (No Extension)
    public static string outputFileName_Original { get; set; } // Output Directory + Filename (No Extension) // Backup Original
    public static string outputFileName_Tokens { get; set; } // Output Directory + Filename (No Extension) + Settings
    public static string outputExt { get; set; } // (eg. .webm)
    public static string output { get; set; } // Single: outputDir + outputFileName + outputExt /// Batch: outputDir + %~nf
    public static string outputNewFileName { get; set; } // File Rename if File already exists

    // Batch
    public static string batchInputAuto { get; set; }

    // Volume Up Down
    // Used for Volume Up Down buttons. Integer += 1 for each tick of the timer.
    // Timer Tick in MainWindow Initialize
    public DispatcherTimer dispatcherTimerUp = new DispatcherTimer(DispatcherPriority.Render);
    public DispatcherTimer dispatcherTimerDown = new DispatcherTimer(DispatcherPriority.Render);

    // Config Read/Write Checks
    // When MainWindow initializes, conf.Read populates these global variables with imported values.
    // When MainWindow exits, conf.Write checks these variables to see if any changes have been made before writing to axiom.conf.
    // This prevents writing to glow.conf every time at exit unless necessary.
    public static double top_Read { get; set; }
    public static double left_Read { get; set; }
    public static double width_Read { get; set; }
    public static double height_Read { get; set; }
    public static bool maximized_Read { get; set; }
    public static string theme_SelectedItem_Read { get; set; }
    public static bool updateAutoCheck_IsChecked_Read { get; set; }
    public static bool cmdWindowKeep_IsChecked_Read { get; set; }
    public static bool autoSortScript_IsChecked_Read { get; set; }
    public static string configPath_SelectedItem_Read { get; set; }
    public static string customPresetsPath_Text_Read { get; set; }
    public static string ffmpegPath_Text_Read { get; set; }
    public static string ffprobePath_Text_Read { get; set; }
    public static string ffplayPath_Text_Read { get; set; }
    public static string youtubedlPath_Text_Read { get; set; }
    public static bool logCheckBox_IsChecked_Read { get; set; }
    public static string logPath_Text_Read { get; set; }
    public static string shell_SelectedItem_Read { get; set; }
    public static string shellTitle_SelectedItem_Read { get; set; }
    public static string processPriority_SelectedItem_Read { get; set; }
    public static string threads_SelectedItem_Read { get; set; }
    public static string inputFileNameTokens_SelectedItem_Read { get; set; }
    public static string inputFileNameTokensCustom_Text_Read { get; set; }
    public static string outputNaming_ItemOrder_Read { get; set; }
    public static string outputNaming_SelectedItems_Read { get; set; }
    public static string outputFileNameSpacing_SelectedItem_Read { get; set; }
    public static string outputOverwrite_SelectedItem_Read { get; set; }

    // --------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Other Windows
    /// </summary>
    // --------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Log Console
    /// </summary>
    // initiate at startup, run in background
    public LogConsole logconsole = new LogConsole();

    /// <summary>
    /// Debug Console
    /// </summary>
    public static DebugConsole debugconsole;

    /// <summary>
    /// File Properties Console
    /// </summary>
    public FilePropertiesWindow filepropwindow;

    /// <summary>
    /// Crop Window
    /// </summary>
    public static CropWindow cropwindow;

    /// <summary>
    /// Optimize Advanced Window
    /// </summary>
    public static InfoWindow infowindow;

    /// <summary>
    /// Update Window
    /// </summary>
    public static UpdateWindow updatewindow;

    // --------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Main Window Initialize
    /// </summary>
    // --------------------------------------------------------------------------------------------------------
    public MainWindow()
    {
      InitializeComponent();

      // -----------------------------------------------------------------
      /// <summary>
      /// Window & Components
      /// </summary>
      // -----------------------------------------------------------------
      //base.Closing += this.Window_Closing;

      // Set Min/Max Width/Height to prevent Tablets maximizing
      //MinWidth = MainWindow.minWidth;
      //MinHeight = MainWindow.minHeight;
      MinWidth = VM.MainView.Window_Width;
      MinHeight = VM.MainView.Window_Height;

      // -------------------------
      // Set Current Version to Assembly Version
      // -------------------------
      System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
      FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
      string assemblyVersion = fvi.FileVersion;
      currentVersion = new Version(assemblyVersion);

      // -------------------------
      // Start the Log Console (Hidden)
      // -------------------------
      StartLogConsole();

      // -------------------------
      // Title + Version
      // -------------------------
      VM.MainView.TitleVersion = "Axiom ~ FFmpeg UI (" + Convert.ToString(currentVersion) + "-" + currentBuildPhase + ")";

      // -------------------------
      // Tool Tips
      // -------------------------
      // Longer Display Time
      ToolTipService.ShowDurationProperty.OverrideMetadata(typeof(DependencyObject),
                                                           new FrameworkPropertyMetadata(Int32.MaxValue));

      // -------------------------
      // Log Text Theme SelectiveColorPreview
      // -------------------------
      switch (VM.ConfigureView.Theme_SelectedItem)
      {
        case "Axiom":
          Log.ConsoleDefault = Brushes.White; // Default
          Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#007DF2")); // Titles
          Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
          Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
          Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#72D4E8")); // Actions
          break;

        case "FFmpeg":
          Log.ConsoleDefault = Brushes.White; // Default
          Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5cb85c")); // Titles
          Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
          Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
          Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5cb85c")); // Actions
          break;

        case "Cyberpunk":
          Log.ConsoleDefault = Brushes.White; // Default
          Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9f3ed2")); // Titles
          Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
          Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
          Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#9380fd")); // Actions
          break;

        case "Onyx":
          Log.ConsoleDefault = Brushes.White; // Default
          Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999")); // Titles
          Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
          Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
          Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#777777")); // Actions
          break;

        case "Circuit":
          Log.ConsoleDefault = Brushes.White; // Default
          Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ad8a4a")); // Titles
          Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
          Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
          Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2ebf93")); // Actions
          break;

        case "System":
          Log.ConsoleDefault = Brushes.White; // Default
          Log.ConsoleTitle = (SolidColorBrush)(new BrushConverter().ConvertFrom("#007DF2")); // Titles
          Log.ConsoleWarning = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E3D004")); // Warning
          Log.ConsoleError = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F44B35")); // Error
          Log.ConsoleAction = (SolidColorBrush)(new BrushConverter().ConvertFrom("#72D4E8")); // Actions
          break;
      }

      // -----------------------------------------------------------------
      // Log Console Message ///////// 
      //-----------------------------------------------------------------
      logconsole.rtbLog.Document = new FlowDocument(Log.logParagraph); //start
      logconsole.rtbLog.BeginChange(); //begin change

      Log.logParagraph.Inlines.Add(new Bold(new Run(VM.MainView.TitleVersion)) { Foreground = Log.ConsoleTitle });

      /// <summary>
      /// System Info
      /// </summary>
      // Shows OS and Hardware information in Log Console
      SystemInfo();
      //Task<int> task = SystemInfoDisplay();

      // -----------------------------------------------------------------
      /// <summary>
      /// Load Saved Settings
      /// </summary>
      // -----------------------------------------------------------------  
      // Log Console Message /////////
      Log.logParagraph.Inlines.Add(new LineBreak());
      Log.logParagraph.Inlines.Add(new Bold(new Run("Loading Saved Settings...")) { Foreground = Log.ConsoleAction });

      // -------------------------
      // Prevent Loading Corrupt App.Config
      // -------------------------
      try
      {
        ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
      }
      catch (ConfigurationErrorsException ex)
      {
        string filename = ex.Filename;

        if (File.Exists(filename) == true)
        {
          File.Delete(filename);
          Settings.Default.Upgrade();
        }
        else
        {

        }
      }

      // -------------------------
      // Volume Up/Down Button Timer Tick
      // Dispatcher Tick
      // In Intializer to prevent Tick from doubling up every MouseDown
      // -------------------------
      dispatcherTimerUp.Tick += new EventHandler(dispatcherTimerUp_Tick);
      dispatcherTimerDown.Tick += new EventHandler(dispatcherTimerDown_Tick);

      // --------------------------
      // Input/Output Copy/Paste
      // --------------------------
      // TODO: Fix XAML compilation - tbxOutput moved to InputOutputControl UserControl
      //DataObject.AddPastingHandler(tbxOutput, OnOutputTextBoxPaste);

      // --------------------------
      // ScriptView Copy/Paste
      // --------------------------
      //DataObject.AddCopyingHandler(tbxScriptView, new DataObjectCopyingEventHandler(OnScriptCopy));
      //DataObject.AddPastingHandler(tbxScriptView, new DataObjectPastingEventHandler(OnScriptPaste));

      // --------------------------------------------------
      // Import Axiom Config axiom.conf
      // --------------------------------------------------

      // -------------------------
      // AppData Local Directory
      // -------------------------
      if (File.Exists(Controls.Configure.confAppDataLocalFilePath))
      {
        // Make changes for Program Exit
        // If Axiom finds axiom.conf in the App Directory
        // Change the Configure Directory variable to it 
        // so that it saves changes to that path on program exit
        Controls.Configure.axiomConfFile = Controls.Configure.confAppDataLocalFilePath;

        // Import Config
        //Controls.Configure.ImportConfig(this, Controls.Configure.axiomConfFile);
        AxiomConfReadActions();
        Controls.Configure.ReadAxiomConf(Controls.Configure.confAppDataLocalDir,  // Directory: AppData\Local\Axiom UI
                                         "axiom.conf",  // Filename
                                         actionsToRead  // Actions to read
                                        );
        VM.ConfigureView.ConfigPath_SelectedItem = "AppData Local";

        // Change Log Directory to App Root Directory
        //Log.logDir = appDataLocalDir + @"Axiom UI\";
        //VM.ConfigureView.LogPath_Text = Log.logDir;
        Log.axiomLogDir = Log.logAppDataLocalDir;
        VM.ConfigureView.LogPath_Text = Log.axiomLogDir;

        // These changes will be seen in Axiom's Settings Tab

        // Log Console Message /////////
        Log.logParagraph.Inlines.Add(new LineBreak());
        Log.logParagraph.Inlines.Add(new LineBreak());
        Log.logParagraph.Inlines.Add(new Bold(new Run("Config Location: ")) { Foreground = Log.ConsoleDefault });
        Log.logParagraph.Inlines.Add(new Run(Controls.Configure.confAppDataLocalFilePath) { Foreground = Log.ConsoleDefault });
      }

      // -------------------------
      // AppData Roaming Directory
      // -------------------------
      else if (File.Exists(Controls.Configure.confAppDataRoamingFilePath))
      {
        // Make changes for Program Exit
        // If Axiom finds axiom.conf in the App Directory
        // Change the Configure Directory variable to it 
        // so that it saves changes to that path on program exit
        Controls.Configure.axiomConfFile = Controls.Configure.confAppDataRoamingFilePath;

        // Import Config
        //Controls.Configure.ImportConfig(this, Controls.Configure.axiomConfFile);
        AxiomConfReadActions();
        Controls.Configure.ReadAxiomConf(Controls.Configure.confAppDataRoamingDir,  // Directory: AppData\Roaming\Axiom UI
                                         "axiom.conf",  // Filename
                                         actionsToRead  // Actions to read
                                        );
        VM.ConfigureView.ConfigPath_SelectedItem = "AppData Roaming";

        // Change Log Directory to App Root Directory
        //Log.logDir = appDataRoamingDir + @"Axiom UI\";
        //VM.ConfigureView.LogPath_Text = Log.logDir;
        Log.axiomLogDir = Log.logAppDataRoamingDir;
        VM.ConfigureView.LogPath_Text = Log.axiomLogDir;

        // These changes will be seen in Axiom's Settings Tab

        // Log Console Message /////////
        Log.logParagraph.Inlines.Add(new LineBreak());
        Log.logParagraph.Inlines.Add(new LineBreak());
        Log.logParagraph.Inlines.Add(new Bold(new Run("Config Location: ")) { Foreground = Log.ConsoleDefault });
        Log.logParagraph.Inlines.Add(new Run(Controls.Configure.confAppDataRoamingFilePath) { Foreground = Log.ConsoleDefault });
      }

      // -------------------------
      // App Root Directory
      // -------------------------
      else if (File.Exists(Controls.Configure.confAppRootFilePath))
      {
        // Make changes for Program Exit
        // If Axiom finds axiom.conf in the App Directory
        // Change the Configure Directory variable to it 
        // so that it saves changes to that path on program exit
        Controls.Configure.axiomConfFile = Controls.Configure.confAppRootFilePath;

        // Import Config
        //Controls.Configure.ImportConfig(this, Controls.Configure.axiomConfFile);

        AxiomConfReadActions();
        Controls.Configure.ReadAxiomConf(Controls.Configure.confAppRootDir,  // Directory: Path\To\Axiom.exe
                                         "axiom.conf",  // Filename
                                         actionsToRead  // Actions to read
                                        );
        VM.ConfigureView.ConfigPath_SelectedItem = "App Root";

        // Change Log Directory to App Root Directory
        //Log.logDir = appRootDir;
        //VM.ConfigureView.LogPath_Text = Log.logDir;
        Log.axiomLogDir = Log.logAppRootDir;
        VM.ConfigureView.LogPath_Text = Log.axiomLogDir;

        // These changes will be seen in Axiom's Settings Tab

        // Log Console Message /////////
        Log.logParagraph.Inlines.Add(new LineBreak());
        Log.logParagraph.Inlines.Add(new LineBreak());
        Log.logParagraph.Inlines.Add(new Bold(new Run("Config Location: ")) { Foreground = Log.ConsoleDefault });
        Log.logParagraph.Inlines.Add(new Run(Controls.Configure.confAppRootFilePath) { Foreground = Log.ConsoleDefault });
      }

      // -------------------------
      // Missing, Load Defaults
      // -------------------------
      else
      {
        VM.ConfigureView.LoadConfigDefaults();
        VM.MainView.LoadControlsDefaults();
        VM.FormatView.LoadControlsDefaults();
        VM.VideoView.LoadControlsDefaults();
        VM.SubtitleView.LoadControlsDefaults();
        VM.AudioView.LoadControlsDefaults();

        // Log Console Message /////////
        Log.logParagraph.Inlines.Add(new LineBreak());
        Log.logParagraph.Inlines.Add(new LineBreak());
        Log.logParagraph.Inlines.Add(new Run("Cannot Locate Config file axiom.conf. Loading defaults.") { Foreground = Log.ConsoleDefault });
      }

      // Window Position Center
      if ((this.Top.ToString() == "NaN" && this.Left.ToString() == "NaN") ||
          (this.Top == 0 && this.Top == 0)
         )
      {
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
      }
      //VM.MainView.ScriptView_Text = this.Top.ToString() + this.Left.ToString(); //debug

      // --------------------------------------------------
      // Log Console Messages
      // --------------------------------------------------
      // -------------------------
      // Load FFmpeg.exe Path
      // -------------------------
      // Log Console Message /////////
      Log.logParagraph.Inlines.Add(new LineBreak());
      Log.logParagraph.Inlines.Add(new LineBreak());
      Log.logParagraph.Inlines.Add(new Bold(new Run("FFmpeg: ")) { Foreground = Log.ConsoleDefault });
      Log.logParagraph.Inlines.Add(new Run(VM.ConfigureView.FFmpegPath_Text) { Foreground = Log.ConsoleDefault });

      // -------------------------
      // Load FFprobe.exe Path
      // -------------------------
      // Log Console Message /////////
      Log.logParagraph.Inlines.Add(new LineBreak());
      Log.logParagraph.Inlines.Add(new Bold(new Run("FFprobe: ")) { Foreground = Log.ConsoleDefault });
      Log.logParagraph.Inlines.Add(new Run(VM.ConfigureView.FFprobePath_Text) { Foreground = Log.ConsoleDefault });

      // -------------------------
      // Load FFplay.exe Path
      // -------------------------
      // Log Console Message /////////
      Log.logParagraph.Inlines.Add(new LineBreak());
      Log.logParagraph.Inlines.Add(new Bold(new Run("FFplay: ")) { Foreground = Log.ConsoleDefault });
      Log.logParagraph.Inlines.Add(new Run(VM.ConfigureView.FFplayPath_Text) { Foreground = Log.ConsoleDefault });

      // -------------------------
      // Load youtube-dl.exe Path
      // -------------------------
      // Log Console Message /////////
      Log.logParagraph.Inlines.Add(new LineBreak());
      Log.logParagraph.Inlines.Add(new Bold(new Run("youtube-dl: ")) { Foreground = Log.ConsoleDefault });
      Log.logParagraph.Inlines.Add(new Run(VM.ConfigureView.youtubedlPath_Text) { Foreground = Log.ConsoleDefault });

      // -------------------------
      // Shell
      // -------------------------
      // Log Console Message /////////
      Log.logParagraph.Inlines.Add(new LineBreak());
      Log.logParagraph.Inlines.Add(new LineBreak());
      Log.logParagraph.Inlines.Add(new Bold(new Run("Shell: ")) { Foreground = Log.ConsoleDefault });
      Log.logParagraph.Inlines.Add(new Run(VM.ConfigureView.Shell_SelectedItem) { Foreground = Log.ConsoleDefault });

      // -------------------------
      // Load Log Enabled
      // -------------------------
      // Log Console Message /////////
      Log.logParagraph.Inlines.Add(new LineBreak());
      Log.logParagraph.Inlines.Add(new LineBreak());
      Log.logParagraph.Inlines.Add(new Bold(new Run("Log Enabled: ")) { Foreground = Log.ConsoleDefault });
      Log.logParagraph.Inlines.Add(new Run(Convert.ToString(VM.ConfigureView.LogCheckBox_IsChecked.ToString())) { Foreground = Log.ConsoleDefault });

      // -------------------------
      // Load Log Path
      // -------------------------
      // Log Console Message /////////
      Log.logParagraph.Inlines.Add(new LineBreak());
      Log.logParagraph.Inlines.Add(new Bold(new Run("Log Path: ")) { Foreground = Log.ConsoleDefault });
      Log.logParagraph.Inlines.Add(new Run(VM.ConfigureView.LogPath_Text) { Foreground = Log.ConsoleDefault });

      // -------------------------
      // Load Threads
      // -------------------------
      // Log Console Message /////////
      Log.logParagraph.Inlines.Add(new LineBreak());
      Log.logParagraph.Inlines.Add(new LineBreak());
      Log.logParagraph.Inlines.Add(new Bold(new Run("Using CPU Threads: ")) { Foreground = Log.ConsoleDefault });
      Log.logParagraph.Inlines.Add(new Run(VM.ConfigureView.Threads_SelectedItem) { Foreground = Log.ConsoleDefault });

      // -----------------------------------------------------------------
      // end change !important
      // -----------------------------------------------------------------
      logconsole.rtbLog.EndChange();
    }

    /// <summary>
    /// Window Loaded
    /// </summary>
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      Application.Current.MainWindow = this;

      // --------------------------------------------------
      // Event Handlers
      // --------------------------------------------------
      // Attach SelectionChanged Handlers
      // Prevent Bound ComboBox from firing SelectionChanged Event at application startup
      // Format
      //cboFormat_Container.SelectionChanged += cboFormat_Container_SelectionChanged;

      // Quality
      //cboVideo_Quality.SelectionChanged += cboVideo_Quality_SelectionChanged;

      // axiom.conf Path
      // Event Handler moved to SettingsConfigControl View
      //cboConfigPath.SelectionChanged += cboConfigPath_SelectionChanged;

      // -------------------------
      // Format Controls
      // -------------------------
      //Controls.Format.Controls.FormatControls(VM.FormatView.Format_Container_SelectedItem);

      // -------------------------
      // Control Defaults
      // -------------------------
      //VM.VideoView.Video_Quality_SelectedItem = "High";
      //VM.AudioView.Audio_Quality_SelectedItem = "256";
      //VM.AudioView.Audio_VBR_IsChecked = true;

      // -------------------------
      // Load Custom Presets
      // -------------------------
      Profiles.Profiles.LoadCustomPresets();

      // -------------------------
      // Check for Available Updates
      // -------------------------
      //Task<int> update = UpdateAvailableCheck();
      //int count = await update;
      Task.Run(() => UpdateAvailableCheck());
    }

    /// <summary>
    /// On Closed
    /// </summary>
    //protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    //{
    //    // Force Exit All Executables
    //    base.OnClosed(e);
    //    System.Windows.Forms.Application.ExitThread();
    //    Application.Current.Shutdown();
    //}
    /// <summary>
    /// Window Closing
    /// </summary>
    public void Window_Closing(object sender, CancelEventArgs e)
    {
      // -------------------------
      // Export axiom.conf
      // -------------------------
      switch (VM.ConfigureView.ConfigPath_SelectedItem)
      {
        // -------------------------
        // AppData Local Directory
        // -------------------------
        case "AppData Local":
          // Save Config
          SaveConfOnExit(Controls.Configure.confAppDataLocalDir, // directory
                         "axiom.conf" // filename
                        );
          break;

        // -------------------------
        // AppData Roaming Directory
        // -------------------------
        case "AppData Roaming":
          // Save Config
          SaveConfOnExit(Controls.Configure.confAppDataRoamingDir, // directory
                         "axiom.conf" // filename
                        );
          break;

        // -------------------------
        // App Directory
        // -------------------------
        case "App Root":
          // Ignore Program Files
          if (!Controls.Configure.confAppRootDir.Contains(programFilesDir) &&
              !Controls.Configure.confAppRootDir.Contains(programFilesX86Dir) &&
              !Controls.Configure.confAppRootDir.Contains(programFilesX64Dir)
              )
          {
            // Save Config
            SaveConfOnExit(Controls.Configure.confAppRootDir, // directory
                           "axiom.conf" // filename
                          );
          }

          // -------------------------
          // Program Files Write Warning
          // -------------------------
          else
          {
            if (File.Exists(Controls.Configure.confAppRootFilePath) ||
                File.Exists(logAppRootPath))
            {
              MessageBox.Show("Cannot save axiom.conf to Program Files, Axiom does not have Administrator Privileges at this time. " +
                              "\n\nPlease select AppData Local or Roaming instead.",
                              "Notice",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
            }
          }
          break;
      }

      // Exit
      Application.Current.Shutdown();
    }

    /// <summary>
    /// axiom.conf Read Actions
    /// </summary>
    List<Action> actionsToRead = new List<Action>();
    public void AxiomConfReadActions()
    {
      // -------------------------
      // Main Window
      // -------------------------
      actionsToRead = new List<Action>
            {
                new Action(() =>
                {
                    // -------------------------
                    // Main Window
                    // -------------------------
                    // Window Position Top
                    double top = 0;
                    double.TryParse(Controls.Configure.ConfigFile.conf.Read("Main Window", "Window_Position_Top"), out top);
                    this.Top = top;
                    top_Read = top;

                    // Window Position Left
                    double left = 0;
                    double.TryParse(Controls.Configure.ConfigFile.conf.Read("Main Window", "Window_Position_Left"), out left);
                    this.Left = left;
                    left_Read =left;

                    // Window Maximized
                    bool mainwindow_WindowState_Maximized = false;
                    bool.TryParse(Controls.Configure.ConfigFile.conf.Read("Main Window", "WindowState_Maximized").ToLower(), out mainwindow_WindowState_Maximized);

                    if (mainwindow_WindowState_Maximized == true)
                    {
                        //VM.MainView.Window_State = WindowState.Maximized;
                        this.WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        //VM.MainView.Window_State = WindowState.Normal;
                        this.WindowState = WindowState.Normal;
                    }

                    // Window Width
                    //double width = MainWindow.minWidth;
                    double width = VM.MainView.Window_Width;
                    double.TryParse(Controls.Configure.ConfigFile.conf.Read("Main Window", "Window_Width"), out width);
                    this.Width = width;
                    width_Read = width;

                    // Window Height
                    //double height = MainWindow.minHeight;
                    double height = VM.MainView.Window_Height;
                    double.TryParse(Controls.Configure.ConfigFile.conf.Read("Main Window", "Window_Height"), out height);
                    this.Height = height;
                    height_Read = height;

                    // CMD Window Keep
                    bool mainwindow_CMDWindowKeep_IsChecked = true;
                    bool.TryParse(Controls.Configure.ConfigFile.conf.Read("Main Window", "CMDWindowKeep_IsChecked").ToLower(), out mainwindow_CMDWindowKeep_IsChecked);
                    VM.MainView.CMDWindowKeep_IsChecked = mainwindow_CMDWindowKeep_IsChecked;
                    cmdWindowKeep_IsChecked_Read = mainwindow_CMDWindowKeep_IsChecked;

                    // Auto Sort Script
                    bool mainwindow_AutoSortScript_IsChecked = true;
                    bool.TryParse(Controls.Configure.ConfigFile.conf.Read("Main Window", "AutoSortScript_IsChecked").ToLower(), out mainwindow_AutoSortScript_IsChecked);
                    VM.MainView.AutoSortScript_IsChecked = mainwindow_AutoSortScript_IsChecked;
                    autoSortScript_IsChecked_Read = mainwindow_AutoSortScript_IsChecked;

                    // Theme
                    string theme_SelectedItem = Controls.Configure.ConfigFile.conf.Read("Settings", "Theme_SelectedItem");
                    if (!string.IsNullOrWhiteSpace(theme_SelectedItem))
                    {
                        VM.ConfigureView.Theme_SelectedItem = theme_SelectedItem;
                    }
                    theme_SelectedItem_Read = theme_SelectedItem;

                    // Update
                    bool updateAutoCheck_IsChecked = true;
                    bool.TryParse(Controls.Configure.ConfigFile.conf.Read("Settings", "UpdateAutoCheck_IsChecked").ToLower(), out updateAutoCheck_IsChecked);
                    VM.ConfigureView.UpdateAutoCheck_IsChecked = updateAutoCheck_IsChecked;
                    updateAutoCheck_IsChecked_Read = updateAutoCheck_IsChecked;

                    // --------------------------------------------------
                    // Settings
                    // --------------------------------------------------
                    // Config Path
                    string configPath_SelectedItem = Controls.Configure.ConfigFile.conf.Read("Settings", "ConfigPath_SelectedItem");
                    if (!string.IsNullOrWhiteSpace(configPath_SelectedItem))
                    {
                        VM.ConfigureView.ConfigPath_SelectedItem = configPath_SelectedItem;
                    }
                    configPath_SelectedItem_Read = configPath_SelectedItem;

                    // Presets
                    string customPresetsPath_Text = Controls.Configure.ConfigFile.conf.Read("Settings", "CustomPresetsPath_Text");
                    if (!string.IsNullOrWhiteSpace(customPresetsPath_Text))
                    {
                        VM.ConfigureView.CustomPresetsPath_Text = customPresetsPath_Text;
                    }
                    customPresetsPath_Text_Read = customPresetsPath_Text;

                    // FFmpeg
                    string ffmpegPath_Text = Controls.Configure.ConfigFile.conf.Read("Settings", "FFmpegPath_Text");
                    if (!string.IsNullOrWhiteSpace(ffmpegPath_Text))
                    {
                        VM.ConfigureView.FFmpegPath_Text = ffmpegPath_Text;
                    }
                    ffmpegPath_Text_Read = ffmpegPath_Text;

                    // FFprobe
                    string ffprobePath_Text = Controls.Configure.ConfigFile.conf.Read("Settings", "FFprobePath_Text");
                    if (!string.IsNullOrWhiteSpace(ffprobePath_Text))
                    {
                        VM.ConfigureView.FFprobePath_Text = ffprobePath_Text;
                    }
                    ffprobePath_Text_Read = ffprobePath_Text;

                    // FFplay
                    string ffplayPath_Text = Controls.Configure.ConfigFile.conf.Read("Settings", "FFplayPath_Text");
                    if (!string.IsNullOrWhiteSpace(ffplayPath_Text))
                    {
                        VM.ConfigureView.FFplayPath_Text = ffplayPath_Text;
                    }
                    ffplayPath_Text_Read = ffplayPath_Text;

                    // youtube-dl
                    string youtubedlPath_Text = Controls.Configure.ConfigFile.conf.Read("Settings", "youtubedlPath_Text");
                    if (!string.IsNullOrWhiteSpace(youtubedlPath_Text))
                    {
                        VM.ConfigureView.youtubedlPath_Text = youtubedlPath_Text;
                    }
                    youtubedlPath_Text_Read = youtubedlPath_Text;

                    // Log CheckBox
                    bool logCheckBox_IsChecked = false;
                    bool.TryParse(Controls.Configure.ConfigFile.conf.Read("Settings", "LogCheckBox_IsChecked").ToLower(), out logCheckBox_IsChecked);
                    VM.ConfigureView.LogCheckBox_IsChecked = logCheckBox_IsChecked;
                    logCheckBox_IsChecked_Read = logCheckBox_IsChecked;
                    // Log Path
                    string logPath_Text = Controls.Configure.ConfigFile.conf.Read("Settings", "LogPath_Text");
                    if (!string.IsNullOrWhiteSpace(logPath_Text))
                    {
                        VM.ConfigureView.LogPath_Text = logPath_Text;
                    }
                    logPath_Text_Read = logPath_Text;

                    // Shell
                    string shell_SelectedItem = Controls.Configure.ConfigFile.conf.Read("Settings", "Shell_SelectedItem");
                    if (!string.IsNullOrWhiteSpace(shell_SelectedItem))
                    {
                        VM.ConfigureView.Shell_SelectedItem = shell_SelectedItem;
                    }
                    shell_SelectedItem_Read = shell_SelectedItem;

                    // Shell Title
                    string shellTitle_SelectedItem = Controls.Configure.ConfigFile.conf.Read("Settings", "ShellTitle_SelectedItem");
                    if (!string.IsNullOrWhiteSpace(shellTitle_SelectedItem))
                    {
                        VM.ConfigureView.ShellTitle_SelectedItem = shellTitle_SelectedItem;
                    }
                    shellTitle_SelectedItem_Read = shellTitle_SelectedItem;

                    // Process Priority
                    string processPriority_SelectedItem = Controls.Configure.ConfigFile.conf.Read("Settings", "ProcessPriority_SelectedItem");
                    if (!string.IsNullOrWhiteSpace(processPriority_SelectedItem))
                    {
                        VM.ConfigureView.ProcessPriority_SelectedItem = processPriority_SelectedItem;
                    }
                    processPriority_SelectedItem_Read = processPriority_SelectedItem;

                    // Threads
                    string threads_SelectedItem = Controls.Configure.ConfigFile.conf.Read("Settings", "Threads_SelectedItem");
                    if (!string.IsNullOrWhiteSpace(threads_SelectedItem))
                    {
                        // Legacy Support: Capitalize First Letter of imported value. Old values are lowercase.
                        VM.ConfigureView.Threads_SelectedItem = char.ToUpper(threads_SelectedItem[0]) + threads_SelectedItem.Substring(1);
                    }
                    threads_SelectedItem_Read = threads_SelectedItem;

                    // Input Filename Tokens
                    string inputFileNameTokens_SelectedItem = Controls.Configure.ConfigFile.conf.Read("Settings", "InputFileNameTokens_SelectedItem");
                    if (!string.IsNullOrWhiteSpace(inputFileNameTokens_SelectedItem))
                    {
                        // Legacy Values Fix
                        switch (inputFileNameTokens_SelectedItem)
                        {
                            case "Job":
                                VM.ConfigureView.InputFileNameTokens_SelectedItem = "Filename";
                                break;
                            case "Job+Tokens":
                                VM.ConfigureView.InputFileNameTokens_SelectedItem = "Filename+Tokens";
                                break;
                            default:
                                VM.ConfigureView.InputFileNameTokens_SelectedItem = inputFileNameTokens_SelectedItem;
                                break;
                        }
                    }
                    inputFileNameTokens_SelectedItem_Read = inputFileNameTokens_SelectedItem;

                    // Input Filename Tokens Custom
                    string inputFileNameTokensCustom_Text = Controls.Configure.ConfigFile.conf.Read("Settings", "InputFileNameTokensCustom_Text");
                    if (!string.IsNullOrWhiteSpace(inputFileNameTokensCustom_Text))
                    {
                        VM.ConfigureView.InputFileNameTokensCustom_Text = inputFileNameTokensCustom_Text;
                        //.Trim() // remove spaces
                        //.Replace(",", ", "); // add spaces after every comma
                    }
                    inputFileNameTokensCustom_Text_Read = inputFileNameTokensCustom_Text;

                    // Ouput Naming
                    // import full list new order
                    string outputNaming_ItemOrder = Controls.Configure.ConfigFile.conf.Read("Settings", "OutputNaming_ItemOrder");
                    // null check
                    if (!string.IsNullOrWhiteSpace(outputNaming_ItemOrder))
                    {
                        // Split the list by commas
                        string[] arrOutputNaming_ItemOrder = outputNaming_ItemOrder.Split(',');

                        // Create the new list
                        // Remove Duplicates
                        VM.ConfigureView.OutputNaming_ListView_Items = new ObservableCollection<string>(arrOutputNaming_ItemOrder
                                                                                                        .Distinct()
                                                                                                        .ToList()
                                                                                                        .AsEnumerable()
                                                                                                       );
                        //VM.ConfigureView.OutputNaming_ListView_Items = arrOutputNaming_ItemOrder.Distinct().ToList();

                        // Check the Master Default List for Missing Items
                        IEnumerable<string> missingItems = MainWindow.outputNaming_Defaults
                                                                     .Except(VM.ConfigureView.OutputNaming_ListView_Items/*arrOutputNaming_ItemOrder*/)
                                                                     .ToList()
                                                                     .AsEnumerable();
                        //List<string> missingItems = MainWindow.outputNaming_Defaults.Except(arrOutputNaming_ItemOrder).ToList();

                        foreach (string item in missingItems)
                        {
                            VM.ConfigureView.OutputNaming_ListView_Items.Add(item/*missingItems[i]*/);
                        }

                        // Selected Items String (items separated by commas)
                        string outputNaming_SelectedItems = Controls.Configure.ConfigFile.conf.Read("Settings", "OutputNaming_SelectedItems");
                        outputNaming_SelectedItems_Read = outputNaming_SelectedItems;

                        // Empty List Check
                        if (!string.IsNullOrEmpty(outputNaming_SelectedItems))
                        {
                            // Split
                            string[] arrOuputNaming_SelectedItems = outputNaming_SelectedItems.Split(',');

                            // Remove Duplicates
                            List<string> lstOuputNaming_SelectedItems = arrOuputNaming_SelectedItems.Distinct().ToList();

                            // Import Selected Items
                            for (var i = 0; i < lstOuputNaming_SelectedItems.Count; i++)
                            {
                                // If Items List Contains the Imported Item
                                if (VM.ConfigureView.OutputNaming_ListView_Items.Contains(arrOuputNaming_SelectedItems[i]))
                                {
                                    // Added Item to Selected Items List
                                    //VM.ConfigureView.OutputNaming_ListView_SelectedItems.Add(arrOuputNaming_SelectedItems[i]);

                                    // Select the Item
                                    // TODO: Fix XAML compilation - lstvOutputNaming moved to SettingsFileControl UserControl
                                    /*
                                    try
                                    {
                                        this.lstvOutputNaming.SelectedItems.Add(arrOuputNaming_SelectedItems[i]);
                                    }
                                    catch
                                    {

                                    }
                                    */
                                }
                            }
                        }
                    }
                    outputNaming_ItemOrder_Read = outputNaming_ItemOrder;

                    // Output Filename Spacing
                    string outputFileNameSpacing_SelectedItem = Controls.Configure.ConfigFile.conf.Read("Settings", "OutputFileNameSpacing_SelectedItem");
                    if (!string.IsNullOrWhiteSpace(outputFileNameSpacing_SelectedItem))
                    {
                        VM.ConfigureView.OutputFileNameSpacing_SelectedItem = outputFileNameSpacing_SelectedItem;
                    }
                    outputFileNameSpacing_SelectedItem_Read = outputFileNameSpacing_SelectedItem;

                    // Output File Overwrite
                    string outputOverwrite_SelectedItem = Controls.Configure.ConfigFile.conf.Read("Settings", "OutputOverwrite_SelectedItem");
                    if (!string.IsNullOrWhiteSpace(outputOverwrite_SelectedItem))
                    {
                        VM.ConfigureView.OutputOverwrite_SelectedItem = outputOverwrite_SelectedItem;
                    }
                    outputOverwrite_SelectedItem_Read = outputOverwrite_SelectedItem;
                 })
            };
    }

    /// <summary>
    /// Save axiom.conf on Exit (Method)
    /// </summary>
    public void SaveConfOnExit(string directory,
                               string filename
        )
    {
      Controls.Configure.ConfigFile conf = new Controls.Configure.ConfigFile(Path.Combine(directory, filename));

      // -------------------------
      // Save only if changes have been made
      // -------------------------
      if (// Main Window
          this.Top != top_Read ||
          this.Left != left_Read ||
          this.Width != width_Read ||
          this.Height != height_Read ||
          //this.WindowState != maximized_Read || //problem
          VM.MainView.CMDWindowKeep_IsChecked != cmdWindowKeep_IsChecked_Read ||
          VM.MainView.AutoSortScript_IsChecked != autoSortScript_IsChecked_Read ||
          VM.ConfigureView.Theme_SelectedItem != theme_SelectedItem_Read ||
          VM.ConfigureView.UpdateAutoCheck_IsChecked != updateAutoCheck_IsChecked_Read ||

          // Config
          VM.ConfigureView.ConfigPath_SelectedItem != configPath_SelectedItem_Read ||
          VM.ConfigureView.CustomPresetsPath_Text != customPresetsPath_Text_Read ||
          VM.ConfigureView.FFmpegPath_Text != ffmpegPath_Text_Read ||
          VM.ConfigureView.FFprobePath_Text != ffprobePath_Text_Read ||
          VM.ConfigureView.FFplayPath_Text != ffplayPath_Text_Read ||
          VM.ConfigureView.youtubedlPath_Text != youtubedlPath_Text_Read ||
          VM.ConfigureView.LogCheckBox_IsChecked != logCheckBox_IsChecked_Read ||
          VM.ConfigureView.LogPath_Text != logPath_Text_Read ||

          // Process
          VM.ConfigureView.Shell_SelectedItem != shell_SelectedItem_Read ||
          VM.ConfigureView.ShellTitle_SelectedItem != shellTitle_SelectedItem_Read ||
          VM.ConfigureView.ProcessPriority_SelectedItem != processPriority_SelectedItem_Read ||
          VM.ConfigureView.Threads_SelectedItem != threads_SelectedItem_Read ||

          // Input
          VM.ConfigureView.InputFileNameTokens_SelectedItem != inputFileNameTokens_SelectedItem_Read ||
          VM.ConfigureView.InputFileNameTokensCustom_Text != inputFileNameTokensCustom_Text_Read ||

          // Output
          string.Join(",", VM.ConfigureView.OutputNaming_ListView_Items
                      .Where(s => !string.IsNullOrWhiteSpace(s))) != outputNaming_ItemOrder_Read ||
          string.Join(",", VM.ConfigureView.OutputNaming_ListView_SelectedItems
                  .Where(s => !string.IsNullOrEmpty(s))) != outputNaming_SelectedItems_Read ||
          VM.ConfigureView.OutputFileNameSpacing_SelectedItem != outputFileNameSpacing_SelectedItem_Read ||
          VM.ConfigureView.OutputOverwrite_SelectedItem != outputOverwrite_SelectedItem_Read
          )
      {
        // -------------------------
        // axiom.conf actions to write
        // -------------------------
        List<Action> actionsToWrite = new List<Action>
                    {
                        // -------------------------
                        // Main Window
                        // -------------------------
                        new Action(() =>
                        {
                            // -------------------------
                            // Main Window
                            // -------------------------
                            // Window Position Top
                            conf.Write("Main Window", "Window_Position_Top", this.Top.ToString());

                            // Window Position Left
                            conf.Write("Main Window", "Window_Position_Left", this.Left.ToString());

                            // Window Width
                            conf.Write("Main Window", "Window_Width", this.Width.ToString());

                            // Window Height
                            conf.Write("Main Window", "Window_Height", this.Height.ToString());

                            // Window Maximized
                            if (this.WindowState == WindowState.Maximized)
                            {
                                conf.Write("Main Window", "WindowState_Maximized", "true");
                            }
                            else
                            {
                                conf.Write("Main Window", "WindowState_Maximized", "false");
                            }

                            // CMD Keep Window Open Toggle
                            conf.Write("Main Window", "CMDWindowKeep_IsChecked", VM.MainView.CMDWindowKeep_IsChecked.ToString().ToLower());

                            // Auto Sort Script Toggle
                            conf.Write("Main Window", "AutoSortScript_IsChecked", VM.MainView.AutoSortScript_IsChecked.ToString().ToLower());

                            // Theme
                            conf.Write("Settings", "Theme_SelectedItem", VM.ConfigureView.Theme_SelectedItem);

                            // Updates
                            conf.Write("Settings", "UpdateAutoCheck_IsChecked", VM.ConfigureView.UpdateAutoCheck_IsChecked.ToString().ToLower());

                            // --------------------------------------------------
                            // Settings
                            // --------------------------------------------------
                            // -------------------------
                            // Config
                            // -------------------------
                            // Config Path
                            conf.Write("Settings", "ConfigPath_SelectedItem", VM.ConfigureView.ConfigPath_SelectedItem);

                            // Presets
                            conf.Write("Settings", "CustomPresetsPath_Text", VM.ConfigureView.CustomPresetsPath_Text);

                            // FFmpeg
                            conf.Write("Settings", "FFmpegPath_Text", VM.ConfigureView.FFmpegPath_Text);
                            conf.Write("Settings", "FFprobePath_Text", VM.ConfigureView.FFprobePath_Text);
                            conf.Write("Settings", "FFplayPath_Text", VM.ConfigureView.FFplayPath_Text);
                            conf.Write("Settings", "youtubedlPath_Text", VM.ConfigureView.youtubedlPath_Text);

                            // Log
                            conf.Write("Settings", "LogCheckBox_IsChecked", VM.ConfigureView.LogCheckBox_IsChecked.ToString().ToLower());
                            conf.Write("Settings", "LogPath_Text", VM.ConfigureView.LogPath_Text);

                            // -------------------------
                            // Process
                            // -------------------------
                            // Shell
                            conf.Write("Settings", "Shell_SelectedItem", VM.ConfigureView.Shell_SelectedItem);

                            // Shell Title
                            conf.Write("Settings", "ShellTitle_SelectedItem", VM.ConfigureView.ShellTitle_SelectedItem);

                            // Process Priority
                            conf.Write("Settings", "ProcessPriority_SelectedItem", VM.ConfigureView.ProcessPriority_SelectedItem);

                            // Threads
                            conf.Write("Settings", "Threads_SelectedItem", VM.ConfigureView.Threads_SelectedItem);

                            // -------------------------
                            // Input
                            // -------------------------

                            // Input Filename Tokens
                            conf.Write("Settings", "InputFileNameTokens_SelectedItem", VM.ConfigureView.InputFileNameTokens_SelectedItem);

                            // Input Filename Tokens Custom
                            conf.Write("Settings", "InputFileNameTokensCustom_Text",
                                                    RemoveLineBreaks(
                                                        VM.ConfigureView.InputFileNameTokensCustom_Text
                                                    )

                                    );

                            // -------------------------
                            // Output
                            // -------------------------
                            // Order
                            string outputNaming_ItemOrder = string.Join(",", VM.ConfigureView.OutputNaming_ListView_Items
                                                                             .Where(s => !string.IsNullOrWhiteSpace(s)));
                            conf.Write("Settings", "OutputNaming_ItemOrder", outputNaming_ItemOrder);

                            // Selected
                            string outputNaming_SelectedItems = string.Join(",", VM.ConfigureView.OutputNaming_ListView_SelectedItems
                                                                                 .Where(s => !string.IsNullOrEmpty(s)));
                            conf.Write("Settings", "OutputNaming_SelectedItems", outputNaming_SelectedItems);

                            // Spacing
                            conf.Write("Settings", "OutputFileNameSpacing_SelectedItem", VM.ConfigureView.OutputFileNameSpacing_SelectedItem);

                            // Output File Overwrite
                            conf.Write("Settings", "OutputOverwrite_SelectedItem", VM.ConfigureView.OutputOverwrite_SelectedItem);

                            // --------------------------------------------------
                            // User
                            // --------------------------------------------------
                            // Input Previous Path
                            if (!string.IsNullOrWhiteSpace(MainWindow.inputPreviousPath))
                            {
                                if (Directory.Exists(MainWindow.inputPreviousPath))
                                {
                                    conf.Write("User", "InputPreviousPath", MainWindow.inputPreviousPath);
                                }
                            }

                            // Output Previous Path
                            if (!string.IsNullOrWhiteSpace(MainWindow.outputPreviousPath))
                            {
                                if (Directory.Exists(MainWindow.outputPreviousPath))
                                {
                                    conf.Write("User", "OutputPreviousPath", MainWindow.outputPreviousPath);
                                }
                            }
                        }),
                    };

        // -------------------------
        // Save Config
        // -------------------------
        //MessageBox.Show(Path.Combine(directory, filename)); //debug
        Controls.Configure.WriteAxiomConf(directory, // Directory: %AppData%\Axiom UI\
                                          filename,  // Filename
                                          actionsToWrite // Actions to write
                                         );
        //debug
        //MessageBox.Show("changes made " +
        //                path + " " +
        //                VM.ConfigureView.ShellTitle_SelectedItem + " " +
        //                top.ToString() + " " +
        //                left.ToString() + " " +
        //                width.ToString() + " " +
        //                height.ToString() + " ");
        //MessageBox.Show("Saved"); //debug
        //MessageBox.Show(
        //    "Current: " + 
        //    "\r\n" +
        //    "Imported: " +
        //    );
      }
    }

    /// <summary>
    /// Folder Write Access Check (Method)
    /// </summary>
    public static bool hasWriteAccessToFolder(string path)
    {
      try
      {
        var directoryInfo = new DirectoryInfo(path);
        var ds = directoryInfo.GetAccessControl();
        return true;
      }
      catch (UnauthorizedAccessException)
      {
        return false;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// Move Directory (Method)
    /// </summary>
    public static void MoveDirectory(string source, string target)
    {
      var sourcePath = source.TrimEnd('\\', ' ');
      var targetPath = target.TrimEnd('\\', ' ');
      var files = Directory.EnumerateFiles(sourcePath, "*.ini", SearchOption.AllDirectories)
                           .GroupBy(s => Path.GetDirectoryName(s));
      foreach (var folder in files)
      {
        var targetFolder = folder.Key.Replace(sourcePath, targetPath);
        Directory.CreateDirectory(targetFolder);
        foreach (var file in folder)
        {
          var targetFile = Path.Combine(targetFolder, Path.GetFileName(file));
          if (File.Exists(targetFile)) File.Delete(targetFile);
          File.Move(file, targetFile);
        }
      }
      Directory.Delete(source, true);
    }

    /// <summary>
    /// Thread Detect
    /// </summary>
    public static String ThreadDetect()
    {
      // Optimal
      if (VM.ConfigureView.Threads_SelectedItem == "Optimal")
      {
        return string.Empty; // FFmpeg auto-detects optimal thread count
      }
      // All
      else if (VM.ConfigureView.Threads_SelectedItem == "All")
      {
        return "-threads 0";
      }
      // Specific number
      else if (!string.IsNullOrWhiteSpace(VM.ConfigureView.Threads_SelectedItem))
      {
        return "-threads " + VM.ConfigureView.Threads_SelectedItem;
      }
      // Default
      else
      {
        return string.Empty;
      }
    }

    /// <summary>
    /// Path Wrap in Quotes
    /// </summary>
    public static String WrapWithQuotes(string s)
    {
      // Shell Check
      switch (VM.ConfigureView.Shell_SelectedItem)
      {
        // CMD
        case "CMD":
          // "my string"
          return "\"" + s + "\"";

        // PowerShell
        case "PowerShell":
          // Process Priority
          switch (VM.ConfigureView.ProcessPriority_SelectedItem)
          {
            // Default
            case "Default":
              // "my string"
              return "\"" + s + "\"";

            // All Other Options (escape)
            default:
              // `"my string`"
              return "`\"" + s + "`\"";
          }

        // Unknown
        default:
          return s;
      }
    }

    /// <summary>
    /// PowerShell Escape Quotes
    /// </summary>
    public static String PowerShellEscapeQuotes(string s)
    {
      // `"my string`"
      return Regex.Replace(s, "\"", "`\"");
    }

    /// <summary>
    /// Clear Global Variables (Method)
    /// </summary>
    public static void ClearGlobalVariables()
    {
      // Output
      regexTags = string.Empty;

      // FFprobe
      Analyze.FFprobe.argsVideoCodec = string.Empty;
      Analyze.FFprobe.argsAudioCodec = string.Empty;
      Analyze.FFprobe.argsVideoBitRate = string.Empty;
      Analyze.FFprobe.argsAudioBitRate = string.Empty;
      Analyze.FFprobe.argsSize = string.Empty;
      Analyze.FFprobe.argsDuration = string.Empty;
      Analyze.FFprobe.argsFrameRate = string.Empty;

      Analyze.FFprobe.inputVideoCodec = string.Empty;
      Analyze.FFprobe.inputVideoBitRate = string.Empty;
      Analyze.FFprobe.inputAudioCodec = string.Empty;
      Analyze.FFprobe.inputAudioBitRate = string.Empty;
      Analyze.FFprobe.inputSize = string.Empty;
      Analyze.FFprobe.inputDuration = string.Empty;
      Analyze.FFprobe.inputFrameRate = string.Empty;

      Analyze.FFprobe.vEntryType = string.Empty;
      Analyze.FFprobe.aEntryType = string.Empty;

      // Video
      Generate.Video.Quality.passSingle = string.Empty;
      Generate.Video.Encoding.vEncodeSpeed = string.Empty;
      Generate.Video.Encoding.hwAccelDecode = string.Empty;
      Generate.Video.Encoding.hwAccelTranscode = string.Empty;
      Generate.Video.Codec.vCodec = string.Empty;
      Generate.Video.Quality.vBitMode = string.Empty;
      Generate.Video.Quality.vQuality = string.Empty;
      Generate.Video.Quality.vBitRateNA = string.Empty;
      Generate.Video.Quality.vLossless = string.Empty;
      Generate.Video.Quality.vBitRate = string.Empty;
      Generate.Video.Quality.vMinRate = string.Empty;
      Generate.Video.Quality.vMaxRate = string.Empty;
      Generate.Video.Quality.vBufSize = string.Empty;
      Generate.Video.Quality.vOptions = string.Empty;
      Generate.Video.Quality.vCRF = string.Empty;
      Generate.Video.Quality.pix_fmt = string.Empty;
      Generate.Video.Color.colorPrimaries = string.Empty;
      Generate.Video.Color.colorTransferCharacteristics = string.Empty;
      Generate.Video.Color.colorSpace = string.Empty;
      Generate.Video.Color.colorRange = string.Empty;
      Generate.Video.Color.colorMatrix = string.Empty;
      Generate.Video.Size.vAspectRatio = string.Empty;
      Generate.Video.Size.vScalingAlgorithm = string.Empty;
      Generate.Video.Video.fps = string.Empty;
      Generate.Video.Video.vsync = string.Empty;
      Generate.Video.Quality.optTune = string.Empty;
      Generate.Video.Quality.optProfile = string.Empty;
      Generate.Video.Quality.optLevel = string.Empty;
      Generate.Video.Quality.optFlags = string.Empty;
      Generate.Video.Size.width = string.Empty;
      Generate.Video.Size.height = string.Empty;

      if (Generate.Video.Params.vParamsList != null &&
          Generate.Video.Params.vParamsList.Count > 0)
      {
        Generate.Video.Params.vParamsList.Clear();
        Generate.Video.Params.vParamsList.TrimExcess();
      }

      Generate.Video.Params.vParams = string.Empty;

      // Clear Crop if ClearCrop Button Identifier is Empty
      if (VM.VideoView.Video_CropClear_Text == "Clear")
      {
        CropWindow.crop = string.Empty;
        CropWindow.divisibleCropWidth = null; //int
        CropWindow.divisibleCropHeight = null; //int
      }

      //Format.trim = string.Empty;
      Generate.Format.trimStart = string.Empty;
      Generate.Format.trimEnd = string.Empty;

      Filters.Video.vFilter = string.Empty;
      Filters.Video.geq = string.Empty;

      if (Filters.Video.vFiltersList != null &&
          Filters.Video.vFiltersList.Count > 0)
      {
        Filters.Video.vFiltersList.Clear();
        Filters.Video.vFiltersList.TrimExcess();
      }

      Generate.Video.Quality.v2PassArgs = string.Empty;
      Generate.Video.Quality.pass1Args = string.Empty;
      Generate.Video.Quality.pass2Args = string.Empty;
      Generate.Video.Quality.pass1 = string.Empty;
      Generate.Video.Quality.pass2 = string.Empty;
      Generate.Video.Video.image = string.Empty;
      Generate.Video.Quality.optimize = string.Empty;

      // Subtitle
      Generate.Subtitle.Subtitle.sCodec = string.Empty;
      Generate.Subtitle.Subtitle.subtitles = string.Empty;

      // Audio
      Generate.Audio.Codec.aCodec = string.Empty;
      Generate.Audio.Channels.aChannel = string.Empty;
      Generate.Audio.Quality.aBitMode = string.Empty;
      Generate.Audio.Quality.aBitRate = string.Empty;
      Generate.Audio.Quality.aBitRateNA = string.Empty;
      Generate.Audio.Quality.aQuality = string.Empty;
      Generate.Audio.Quality.aCompressionLevel = string.Empty;
      Generate.Audio.Quality.aSamplerate = string.Empty;
      Generate.Audio.Quality.aBitDepth = string.Empty;
      Filters.Audio.aFilter = string.Empty;
      Filters.Audio.aVolume = string.Empty;
      Filters.Audio.aHardLimiter = string.Empty;

      if (Filters.Audio.aFiltersList != null &&
          Filters.Audio.aFiltersList.Count > 0)
      {
        Filters.Audio.aFiltersList.Clear();
        Filters.Audio.aFiltersList.TrimExcess();
      }

      Generate.Audio.Audio.audioMux = string.Empty;

      // Batch
      Analyze.FFprobe.batchFFprobeAuto = string.Empty;
      Generate.Video.Quality.batchVideoAuto = string.Empty;
      Generate.Audio.Quality.batchAudioAuto = string.Empty;

      // Streams
      Generate.Streams.vMap = string.Empty;
      Generate.Streams.cMap = string.Empty;
      Generate.Streams.sMap = string.Empty;
      Generate.Streams.aMap = string.Empty;
      Generate.Streams.mMap = string.Empty;
      Generate.Streams.mvMap = string.Empty;
      Generate.Streams.maMap = string.Empty;
      Generate.Streams.msMap = string.Empty;
      //Generate.Streams.mcMap = string.Empty;

      // Do not Empty:
      //
      //inputDir
      //inputFileName
      //inputExt
      //input
      //outputDir
      //outputFileName
      //outputFileName_Original
      //outputFileName_Tokens
      //outputNewFileName
      //FFmpeg.ffmpegArgs
      //FFmpeg.ffmpegArgsSort
      //Video.scale
      //CropWindow.divisibleCropWidth
      //CropWindow.divisibleCropHeight
      //CropWindow.cropWidth
      //CropWindow.cropHeight
      //CropWindow.cropX
      //CropWindow.cropY
      //Streams.map
      //FFmpeg.cmdWindow
    }

    /// <summary>
    /// Remove Linebreaks (Method)
    /// </summary>
    /// <remarks>
    /// Used for Selected Controls FFmpeg Arguments
    /// </remarks>
    public static String RemoveLineBreaks(string lines)
    {
      return lines.Replace(Environment.NewLine, "")
                  .Replace("\r\n\r\n", "")
                  .Replace("\r\n", "")
                  .Replace("\n\n", "")
                  .Replace("\n", "")
                  .Replace("\u2028", "")
                  .Replace("\u000A", "")
                  .Replace("\u000B", "")
                  .Replace("\u000C", "")
                  .Replace("\u000D", "")
                  .Replace("\u0085", "")
                  .Replace("\u2028", "")
                  .Replace("\u2029", "");
    }

    /// <summary>
    /// Replace Linebreaks with Space (Method)
    /// </summary>
    /// <remarks>
    /// Used for Script View Custom Edited Script
    /// </remarks>
    public static String ReplaceLineBreaksWithSpaces(string lines)
    {
      // Replace Linebreaks with Spaces to avoid arguments touching

      if (!string.IsNullOrWhiteSpace(lines))
      {
        lines = lines
                .Replace(Environment.NewLine, " ")
                .Replace("\r\n", " ")
                .Replace("\n", " ")
                .Replace("\u2028", " ")
                .Replace("\u000A", " ")
                .Replace("\u000B", " ")
                .Replace("\u000C", " ")
                .Replace("\u000D", " ")
                .Replace("\u0085", " ")
                .Replace("\u2028", " ")
                .Replace("\u2029", " ");
      }

      return lines;
    }

    /// <summary>
    /// Deny Special Keys
    /// </summary>
    public void Deny_Special_Keys(KeyEventArgs e)
    {
      if ((Keyboard.IsKeyDown(Key.LeftShift) && e.Key == Key.D8) || // *
          (Keyboard.IsKeyDown(Key.RightShift) && e.Key == Key.D8) ||  // *

          (Keyboard.IsKeyDown(Key.LeftShift) && e.Key == Key.OemPeriod) ||  // >
          (Keyboard.IsKeyDown(Key.RightShift) && e.Key == Key.OemPeriod) ||  // >

          (Keyboard.IsKeyDown(Key.LeftShift) && e.Key == Key.OemComma) ||  // <
          (Keyboard.IsKeyDown(Key.RightShift) && e.Key == Key.OemComma) ||  // <

          e.Key == Key.Tab ||  // tab
          e.Key == Key.Oem2 ||  // forward slash
          e.Key == Key.OemBackslash ||  // backslash
          e.Key == Key.OemQuestion ||  // ?
          e.Key == Key.OemQuotes ||  // "
          e.Key == Key.OemSemicolon ||  // ;
          e.Key == Key.OemPipe // |
          )
      {
        e.Handled = true;
      }
    }

    /// <summary>
    /// Deny Backspace Key
    /// </summary>
    public void Deny_Backspace_Key(KeyEventArgs e)
    {
      if (e.Key != Key.Back)
      {
        e.Handled = true;
      }
    }

    /// <summary>
    /// Allow Only Numbers & Backspace
    /// </summary>
    public static void Allow_Only_Number_Keys(KeyEventArgs e)
    {
      // Only allow Numbers
      // Deny Symbols (Shift + Number)
      if (!(e.Key >= System.Windows.Input.Key.D0 && e.Key <= System.Windows.Input.Key.D9) ||
          (e.Key >= System.Windows.Input.Key.NumPad0 && e.Key <= System.Windows.Input.Key.NumPad9) ||
          e.Key == System.Windows.Input.Key.Back ||
          e.Key == System.Windows.Input.Key.Delete ||
          (Keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift) && (e.Key >= System.Windows.Input.Key.D9)) ||
          (Keyboard.IsKeyDown(System.Windows.Input.Key.RightShift) && (e.Key >= System.Windows.Input.Key.D9))
          )
      {
        e.Handled = true;
      }
    }

    /// <summary>
    /// Start Log Console (Method)
    /// </summary>
    public void StartLogConsole()
    {
      // Open LogConsole Window
      logconsole = new LogConsole();
      logconsole.Hide();

      // Position with Show();

      logconsole.rtbLog.Cursor = Cursors.Arrow;
    }

    /// <summary>
    /// Selected Item
    /// </summary>
    // Format
    public static string Format_Container_PreviousItem { get; set; }
    // Video
    public static string Video_Codec_PreviousItem { get; set; }
    public static string Video_EncodeSpeed_PreviousItem { get; set; }
    public static string Video_Quality_PreviousItem { get; set; }
    public static string Video_Pass_PreviousItem { get; set; }
    public static string Video_Optimize_PreviousItem { get; set; }
    public static string Video_Optimize_Tune_PreviousItem { get; set; }
    public static string Video_Optimize_Profile_PreviousItem { get; set; }
    public static string Video_Optimize_Level_PreviousItem { get; set; }
    // Audio
    public static string Audio_Codec_PreviousItem { get; set; }
    public static string Audio_Stream_PreviousItem { get; set; }
    public static string Audio_Channel_PreviousItem { get; set; }
    public static string Audio_Quality_PreviousItem { get; set; }
    public static string Audio_CompressionLevel_PreviousItem { get; set; }
    public static string Audio_SampleRate_PreviousItem { get; set; }
    public static string Audio_BitDepth_PreviousItem { get; set; }
    // Selected Item
    public static String SelectedItem(List<string> controlItems,
                                      string previousItem
                                     )
    {
      // -------------------------
      // Select the Prevoius Codec's Item if available
      // -------------------------
      if (!string.IsNullOrWhiteSpace(previousItem) &&
          controlItems?.Contains(previousItem) == true)
      {
        //MessageBox.Show("4 " + previousItem); //debug
        return previousItem;
      }
      // -------------------------
      // If missing Select Default to First Item
      // -------------------------
      else
      {
        //MessageBox.Show("missing"); //debug
        return controlItems.FirstOrDefault();
      }
    }

    /// <summary>
    /// Save Previous Item - Conditions Check
    /// </summary>
    public static bool SavePreviousItemCond(string selectedItem)
    {
      // Halt
      if (string.IsNullOrWhiteSpace(selectedItem))
      {
        return false;
      }

      // Halt
      if (selectedItem.ToLower() == "default" &&
          selectedItem.ToLower() == "auto" &&
          selectedItem.ToLower() == "none")
      {
        return false;
      }
      // Pass
      else
      {
        return true;
      }
    }

    /// <summary>
    /// Checked
    /// </summary>
    // Audio
    public static bool? Audio_VBR_PreviousChecked { get; set; }
    public static void Checked(bool previousChecked)
    {

    }

    /// <summary>
    /// Is Valid Windows Path
    /// </summary>
    /// <remarks>
    /// Check for Invalid Characters
    /// </remarks>
    public static bool IsValidPath(string path)
    {
      if (!string.IsNullOrWhiteSpace(path))
      {
        // Not Valid
        string invalidChars = new string(Path.GetInvalidPathChars());
        Regex regex = new Regex("[" + Regex.Escape(invalidChars) + "]");

        if (regex.IsMatch(path)) { return false; }
        ;
      }

      // Empty
      else
      {
        return false;
      }

      // Is Valid
      return true;
    }

    /// <summary>
    /// Is Valid Windows Filename
    /// </summary>
    /// <remarks>
    /// Check for Invalid Characters
    /// </remarks>
    public static bool IsValidFilename(string filename)
    {
      // Not Valid
      string invalidChars = new string(Path.GetInvalidFileNameChars());
      Regex regex = new Regex("[" + Regex.Escape(invalidChars) + "]");

      if (regex.IsMatch(filename)) { return false; }
      ;

      // Is Valid
      return true;
    }

    /// <summary>
    /// FFmpeg Path
    /// </summary>
    public static String FFmpegPath()
    {
      // -------------------------
      // FFmpeg.exe and FFprobe.exe Paths
      // -------------------------
      // If Configure FFmpeg Path is <auto>
      if (VM.ConfigureView.FFmpegPath_Text == "<auto>")
      {
        if (File.Exists(appRootDir + @"ffmpeg\bin\ffmpeg.exe"))
        {
          // Use included binary
          // Do not use WrapWithQuotes() Method
          Generate.FFmpeg.ffmpeg = Sys.Shell.PowerShell_CallOperator_FFmpeg() + "\"" + appRootDir + @"ffmpeg\bin\ffmpeg.exe" + "\"";
        }
        else if (!File.Exists(appRootDir + @"ffmpeg\bin\ffmpeg.exe"))
        {
          // Use system installed binaries
          Generate.FFmpeg.ffmpeg = "ffmpeg";
        }
      }
      // Use User Custom Path
      else
      {
        // Do not use WrapWithQuotes() Method
        Generate.FFmpeg.ffmpeg = Sys.Shell.PowerShell_CallOperator_FFmpeg() + "\"" + VM.ConfigureView.FFmpegPath_Text + "\"";
      }

      // Return Value
      return Generate.FFmpeg.ffmpeg;
    }

    /// <summary>
    /// FFmpeg Path for YouTube-DL
    /// </summary>
    public static String YouTubeDL_FFmpegPath()
    {
      // youtube-dl
      // FFmpeg must be detected by youtube-dl to merge video+audio into a single file
      // If using Environment Variables, path will be only 'ffmpeg'
      // If defining ffmpeg location, do not use "--ffmpeg-location ffmpeg", it will fail
      // You must use a full path --ffmpeg-location "C:\Path\To\ffmpeg.exe"

      string path = FFmpegPath();

      // Environment Variables
      if (path == "ffmpeg")
      {
        // Do not specify a path if using Environment Variables
        // It will be detected by youtube-dl automatically
        return string.Empty;
      }

      // Missing
      else if (string.IsNullOrWhiteSpace(path))
      {
        // Let youtube-dl throw error
        return string.Empty;
      }

      // Specify ffmpeg.exe path
      else
      {
        return " --ffmpeg-location " + path;
      }
    }

    /// <summary>
    /// FFprobe Path
    /// </summary>
    public static void FFprobePath()
    {
      // If Configure FFprobe Path is <auto>
      if (VM.ConfigureView.FFprobePath_Text == "<auto>")
      {
        if (File.Exists(appRootDir + @"ffmpeg\bin\ffprobe.exe"))
        {
          // use included binary
          Analyze.FFprobe.ffprobe = "\"" + appRootDir + @"ffmpeg\bin\ffprobe.exe" + "\"";
        }
        else if (!File.Exists(appRootDir + @"ffmpeg\bin\ffprobe.exe"))
        {
          // use system installed binaries
          Analyze.FFprobe.ffprobe = "ffprobe";
        }
      }
      // Use User Custom Path
      else
      {
        Analyze.FFprobe.ffprobe = "\"" + VM.ConfigureView.FFprobePath_Text + "\"";
      }
    }

    /// <summary>
    /// FFplay Path
    /// </summary>
    public static void FFplayPath()
    {
      // If Configure FFprobe Path is <auto>
      if (VM.ConfigureView.FFplayPath_Text == "<auto>")
      {
        if (File.Exists(appRootDir + @"ffmpeg\bin\ffplay.exe"))
        {
          // use included binary
          Preview.FFplay.ffplay = "\"" + appRootDir + @"ffmpeg\bin\ffplay.exe" + "\"";
        }
        else if (!File.Exists(appRootDir + @"ffmpeg\bin\ffplay.exe"))
        {
          // use system installed binaries
          Preview.FFplay.ffplay = "ffplay";
        }
      }
      // Use User Custom Path
      else
      {
        Preview.FFplay.ffplay = "\"" + VM.ConfigureView.FFplayPath_Text + "\"";
      }
    }

    /// <summary>
    /// youtube-dl Path
    /// </summary>
    public static void youtubedlPath()
    {
      // If Configure youtubedl Path is <auto>
      if (VM.ConfigureView.youtubedlPath_Text == "<auto>")
      {
        // youtube-dl.exe Exists
        if (File.Exists(appRootDir + @"youtube-dl\youtube-dl.exe"))
        {
          // use included binary path
          youtubedl = appRootDir + @"youtube-dl\youtube-dl.exe";
        }
        else if (File.Exists(appRootDir + @"youtube-dl.exe"))
        {
          // moved from folder
          youtubedl = appRootDir + @"youtube-dl.exe";
        }

        // youtube-dl.exe Does Not Exist
        else if (!File.Exists(appRootDir + @"youtube-dl\youtube-dl.exe"))
        {
          // Installed
          // Environment Variable auto path
          youtubedl = @"youtube-dl";
        }
      }

      // Use User Custom Path
      else
      {
        youtubedl = VM.ConfigureView.youtubedlPath_Text;
      }
    }

    /// <summary>
    /// Is Web URL
    /// </summary>
    public static bool IsWebURL(string input_Text)
    {
      // Empty
      if (string.IsNullOrWhiteSpace(input_Text))
      {
        return false;
      }

      input_Text = input_Text.Trim();

      // URL
      if ((input_Text.StartsWith("http://") ||
        input_Text.StartsWith("https://") ||
        input_Text.StartsWith("www."))
         )
      {
        return true;
      }

      // Local File
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Is YouTube URL
    /// </summary>
    public static bool IsYouTubeURL(string input_Text)
    {
      // Empty
      if (string.IsNullOrWhiteSpace(input_Text))
      {
        return false;
      }

      // YouTube
      if (// youtube (any domain extension)
         input_Text.StartsWith("https://www.youtube.") ||
         input_Text.StartsWith("http://www.youtube.") ||
         input_Text.StartsWith("www.youtube.") ||
         input_Text.StartsWith("youtube.") ||

         // youtu.be
         input_Text.StartsWith("https://youtu.be") ||
         input_Text.StartsWith("http://youtu.be") ||
         input_Text.StartsWith("www.youtu.be") ||
         input_Text.StartsWith("youtu.be")
         )
      {
        return true;
      }

      // Other
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Is Web Download Only
    /// </summary>
    public static bool IsWebDownloadOnly(string videoCodec_SelectedItem,
                                         string subtitleCodec_SelectedItem,
                                         string audioCodec_SelectedItem
                                         )
    {
      if (// Video
          (videoCodec_SelectedItem == "Copy" &&
           subtitleCodec_SelectedItem == "Copy" &&
           audioCodec_SelectedItem == "Copy")

           ||

          (videoCodec_SelectedItem == "Copy" &&
           subtitleCodec_SelectedItem == "Copy")

           ||

          (videoCodec_SelectedItem == "Copy" &&
           subtitleCodec_SelectedItem == "None" &&
           audioCodec_SelectedItem == "Copy")

           ||

          (videoCodec_SelectedItem == "None" &&
           subtitleCodec_SelectedItem == "None" &&
           audioCodec_SelectedItem == "Copy")
          )
      {
        return true;
      }

      // Other
      else
      {
        return false;
      }
    }

    /// <summary>
    /// FFmpeg Checks
    /// </summary>
    public static bool FFcheck()
    {
      bool ready = true;

      try
      {
        // Environment Variables
        var envar = Environment.GetEnvironmentVariable("Path");

        // -------------------------
        // FFmpeg
        // -------------------------
        // If Auto Mode
        if (VM.ConfigureView.FFmpegPath_Text == "<auto>")
        {
          // Check default current directory
          if (File.Exists(appRootDir + @"ffmpeg\bin\ffmpeg.exe"))
          {
            // let pass
            return true;
          }
          else
          {
            int found = 0;

            // Check Environment Variables
            foreach (var envarPath in envar.Split(';'))
            {
              var exePath = Path.Combine(envarPath, "ffmpeg.exe");
              if (File.Exists(exePath)) { found = 1; }
            }

            if (found == 1)
            {
              // let pass
              return true;
            }
            else
            {
              // lock
              MessageBox.Show("Cannot locate FFmpeg Path in Environment Variables or Current Folder.",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);

              return false;
            }
          }
        }
        // If User Defined Path
        else if (VM.ConfigureView.FFmpegPath_Text != "<auto>" &&
                 IsValidPath(VM.ConfigureView.FFprobePath_Text))
        {
          var dirPath = Path.GetDirectoryName(VM.ConfigureView.FFmpegPath_Text).TrimEnd('\\') + @"\";
          var fullPath = Path.Combine(dirPath, "ffmpeg.exe");

          // Make Sure ffmpeg.exe Exists
          if (File.Exists(fullPath))
          {
            // let pass
            return true;
          }
          else
          {
            // lock
            MessageBox.Show("Cannot locate FFmpeg Path in User Defined Path.",
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);

            return false;
          }
        }
      }
      catch
      {
        MessageBox.Show("Unknown Error trying to locate FFmpeg Path.",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
        return false;
      }

      return ready;
    }

    /// <summary>
    /// Ready Halts
    /// </summary>
    public static bool ReadyHalts()
    {
      // -------------------------
      // Check if FFmpeg & FFprobe Exists
      // -------------------------
      if (FFcheck() == false)
      {
        // Halt
        return false;
      }

      // -------------------------
      // Input File does not exist
      // -------------------------
      if (IsWebURL(VM.MainView.Input_Text) == false) // Ignore Web URL's
      {
        if (!string.IsNullOrWhiteSpace(VM.MainView.Input_Text) &&
            VM.MainView.Batch_IsChecked == false)
        {
          if (!File.Exists(VM.MainView.Input_Text))
          {
            MessageBox.Show("Input file does not exist.",
                            "Notice",
                            MessageBoxButton.OK,
                            MessageBoxImage.Exclamation);

            // Halt
            return false;
          }
        }
      }

      // Pass
      return true;
    }

    public void SystemInfo()
    {
      //int count = 0;
      //await Task.Factory.StartNew(() =>
      //{
      // -----------------------------------------------------------------
      /// <summary>
      /// System Info
      /// </summary>
      /// <remarks>
      /// Detect and Display System Hardware
      /// </remarks>
      // -----------------------------------------------------------------
      // Log Console Message /////////
      Log.logParagraph.Inlines.Add(new LineBreak());
      Log.logParagraph.Inlines.Add(new LineBreak());
      Log.logParagraph.Inlines.Add(new Bold(new Run("System Info:")) { Foreground = Log.ConsoleAction });
      Log.logParagraph.Inlines.Add(new LineBreak());

      // -------------------------
      // OS
      // -------------------------
      try
      {
        ManagementClass os = new ManagementClass("Win32_OperatingSystem");

        foreach (ManagementObject obj in os.GetInstances())
        {
          // Log Console Message /////////
          Log.logParagraph.Inlines.Add(new Run(Convert.ToString(obj["Caption"])) { Foreground = Log.ConsoleDefault });
          Log.logParagraph.Inlines.Add(new LineBreak());

        }
        os.Dispose();
      }
      catch
      {

      }

      // -------------------------
      // CPU
      // -------------------------
      try
      {
        ManagementObjectSearcher cpu = new ManagementObjectSearcher("root\\CIMV2", "SELECT Name FROM Win32_Processor");

        foreach (ManagementObject obj in cpu.Get())
        {
          // Log Console Message /////////
          Log.logParagraph.Inlines.Add(new Run(Convert.ToString(obj["Name"])) { Foreground = Log.ConsoleDefault });
          Log.logParagraph.Inlines.Add(new LineBreak());
        }
        cpu.Dispose();

        // Max Threads
        foreach (var item in new System.Management.ManagementObjectSearcher("Select NumberOfLogicalProcessors FROM Win32_ComputerSystem").Get())
        {
          Controls.Configure.maxthreads = String.Format("{0}", item["NumberOfLogicalProcessors"]);
        }
      }
      catch
      {

      }

      // -------------------------
      // GPU
      // -------------------------
      try
      {
        ManagementObjectSearcher gpu = new ManagementObjectSearcher("root\\CIMV2", "SELECT Name, AdapterRAM FROM Win32_VideoController");

        foreach (ManagementObject obj in gpu.Get())
        {
          Log.logParagraph.Inlines.Add(new Run(Convert.ToString(obj["Name"]) + " " + Convert.ToString(Math.Round(Convert.ToDouble(obj["AdapterRAM"]) * 0.000000001, 3)) + "GB") { Foreground = Log.ConsoleDefault });
          Log.logParagraph.Inlines.Add(new LineBreak());
        }
      }
      catch
      {

      }

      // -------------------------
      // RAM
      // -------------------------
      try
      {
        Log.logParagraph.Inlines.Add(new Run("RAM ") { Foreground = Log.ConsoleDefault });

        double capacity = 0;
        //int memtype = 0;
        string type;
        int speed = 0;

        ManagementObjectSearcher ram = new ManagementObjectSearcher("root\\CIMV2", "SELECT Capacity, MemoryType, Speed FROM Win32_PhysicalMemory");

        foreach (ManagementObject obj in ram.Get())
        {
          capacity += Convert.ToDouble(obj["Capacity"]);
          //memtype = Int32.Parse(obj.GetPropertyValue("MemoryType").ToString());
          //speed = Int32.Parse(obj.GetPropertyValue("Speed").ToString());
          int.TryParse(obj.GetPropertyValue("Speed").ToString(), out speed);
        }

        capacity *= 0.000000001; // Convert Byte to GB
        capacity = Math.Round(capacity, 3); // Round to 3 decimal places

        // Select RAM Type
        type = RamInfo.MemType;
        //switch (memtype)
        //{
        //    case 20:
        //        type = "DDR";
        //        break;
        //    case 21:
        //        type = "DDR2";
        //        break;
        //    case 17:
        //        type = "SDRAM";
        //        break;
        //    default:
        //        if (memtype == 0 || memtype > 22)
        //            type = "DDR3";
        //        else
        //            type = "Unknown";
        //        break;
        //}

        // Log Console Message /////////
        Log.logParagraph.Inlines.Add(new Run(Convert.ToString(capacity) + "GB " + type + " " + Convert.ToString(speed) + "MHz") { Foreground = Log.ConsoleDefault });
        Log.logParagraph.Inlines.Add(new LineBreak());

        ram.Dispose();
      }
      catch
      {

      }

      // End System Info

      //});

      //return count;
    }

    /// <summary>
    /// RAM Type
    /// <summary>
    public class RamInfo
    {
      public static string MemType
      {
        get
        {
          int type = 0;

          ConnectionOptions connection = new ConnectionOptions();
          connection.Impersonation = ImpersonationLevel.Impersonate;
          ManagementScope scope = new ManagementScope("\\\\.\\root\\CIMV2", connection);
          scope.Connect();
          ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory");
          ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
          foreach (ManagementObject queryObj in searcher.Get())
          {
            type = Convert.ToInt32(queryObj["MemoryType"]);
          }

          return TypeString(type);
        }
      }

      private static string TypeString(int type)
      {
        string outValue = string.Empty;

        switch (type)
        {
          case 0x0: outValue = "Unknown"; break;
          case 0x1: outValue = "Other"; break;
          case 0x2: outValue = "DRAM"; break;
          case 0x3: outValue = "Synchronous DRAM"; break;
          case 0x4: outValue = "Cache DRAM"; break;
          case 0x5: outValue = "EDO"; break;
          case 0x6: outValue = "EDRAM"; break;
          case 0x7: outValue = "VRAM"; break;
          case 0x8: outValue = "SRAM"; break;
          case 0x9: outValue = "RAM"; break;
          case 0xa: outValue = "ROM"; break;
          case 0xb: outValue = "Flash"; break;
          case 0xc: outValue = "EEPROM"; break;
          case 0xd: outValue = "FEPROM"; break;
          case 0xe: outValue = "EPROM"; break;
          case 0xf: outValue = "CDRAM"; break;
          case 0x10: outValue = "3DRAM"; break;
          case 0x11: outValue = "SDRAM"; break;
          case 0x12: outValue = "SGRAM"; break;
          case 0x13: outValue = "RDRAM"; break;
          case 0x14: outValue = "DDR"; break;
          case 0x15: outValue = "DDR2"; break;
          case 0x16: outValue = "DDR2 FB-DIMM"; break;
          case 0x17: outValue = "Undefined 23"; break;
          case 0x18: outValue = "DDR3"; break;
          case 0x19: outValue = "FBD2"; break;
          case 0x1a: outValue = "DDR4"; break;
          default: outValue = "Undefined"; break;
        }

        return outValue;
      }
    }

    /// <summary>
    /// Normalize Value (Method)
    /// <summary>
    public static double NormalizeValue(double val, double valmin, double valmax, double min, double max, double midpoint)
    {
      double mid = (valmin + valmax) / 2.0;
      if (val < mid)
      {
        return (val - valmin) / (mid - valmin) * (midpoint - min) + min;
      }
      else
      {
        return (val - mid) / (valmax - mid) * (max - midpoint) + midpoint;
      }
    }
    //public static double NormalizeValue(double val, double valmin, double valmax, double min, double max, double ffdefault)
    //{
    //    // (((sliderValue - sliderValueMin) / (sliderValueMax - sliderValueMin)) * (NormalizeMax - NormalizeMin)) + NormalizeMin

    //    return (((val - valmin) / (valmax - valmin)) * (max - min)) + min;
    //}

  }
}
