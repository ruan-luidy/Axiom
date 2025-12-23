using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel;

namespace Axiom.Views
{
    public partial class SubtitlesControl : UserControl
    {
        public SubtitlesControl()
        {
            InitializeComponent();
        }

            /// <summary>
            /// Subtitle Add
            /// </summary>
            private void btnSubtitle_Add_Click(object sender, RoutedEventArgs e)
            {
              // Open Select File Window
              //Microsoft.Win32.OpenFileDialog selectFiles = new Microsoft.Win32.OpenFileDialog();
              Microsoft.Win32.OpenFileDialog selectFiles = new Microsoft.Win32.OpenFileDialog
              {
                CheckFileExists = true,
                CheckPathExists = true,
                //RestoreDirectory = true,
                //ReadOnlyChecked = true,
                //ShowReadOnly = true
              };

              // Defaults
              selectFiles.Multiselect = true;
              selectFiles.Filter = "All files (*.*)|*.*|SRT (*.srt)|*.srt|SUB (*.sub)|*.sub|SBV (*.sbv)|*.sbv|ASS (*.ass)|*.ass|SSA (*.ssa)|*.ssa|MPSUB (*.mpsub)|*.mpsub|LRC (*.lrc)|*.lrc|CAP (*.cap)|*.cap";

              // Process Dialog Box
              //Nullable<bool> result = selectFiles.ShowDialog();
              //if (result == true)
              if (selectFiles.ShowDialog() == true)
              {
                // Reset
                //SubtitlesClear();

                // Add Selected Files to List
                for (var i = 0; i < selectFiles.FileNames.Length; i++)
                {
                  // Wrap in quotes for ffmpeg -i
                  //Generate.Subtitle.subtitleFilePathsList.Add("\"" + selectFiles.FileNames[i] + "\"");
                  Generate.Subtitle.Subtitle.subtitleFilePathsList.Add(WrapWithQuotes(selectFiles.FileNames[i]));
                  //MessageBox.Show(Video.subtitleFiles[i]); //debug

                  Generate.Subtitle.Subtitle.subtitleFileNamesList.Add(Path.GetFileName(selectFiles.FileNames[i]));

                  // ListView Display File Names + Ext
                  VM.SubtitleView.Subtitle_ListView_Items.Add(Path.GetFileName(selectFiles.FileNames[i]));

                  // Metadata Placeholders
                  // Title
                  Generate.Subtitle.Metadata.titleList.Add(string.Empty);

                  // Language
                  Generate.Subtitle.Metadata.languageList.Add(string.Empty);

                  // Delay
                  Generate.Subtitle.Metadata.delayList.Add(string.Empty);
                }
              }
            }

            /// <summary>
            /// Subtitle Clear All
            /// </summary>
            private void btnSubtitle_Clear_Click(object sender, RoutedEventArgs e)
            {
              SubtitlesClear();
            }

            /// <summary>
            /// Subtitle Remove
            /// </summary>
            private void btnSubtitle_Remove_Click(object sender, RoutedEventArgs e)
            {
              if (VM.SubtitleView.Subtitle_ListView_SelectedItems.Count > 0)
              {
                var selectedIndex = VM.SubtitleView.Subtitle_ListView_SelectedIndex;

                // -------------------------
                // List View
                // -------------------------
                // ListView Items
                var itemlsvFileNames = VM.SubtitleView.Subtitle_ListView_Items[selectedIndex];
                VM.SubtitleView.Subtitle_ListView_Items.RemoveAt(selectedIndex);

                // List File Paths
                string itemFilePaths = Generate.Subtitle.Subtitle.subtitleFilePathsList[selectedIndex];
                Generate.Subtitle.Subtitle.subtitleFilePathsList.RemoveAt(selectedIndex);

                // List File Names
                string itemFileNames = Generate.Subtitle.Subtitle.subtitleFileNamesList[selectedIndex];
                Generate.Subtitle.Subtitle.subtitleFileNamesList.RemoveAt(selectedIndex);

                // -------------------------
                // Metadata
                // -------------------------
                // Title
                if (Generate.Subtitle.Metadata.titleList.ElementAtOrDefault(selectedIndex) != null)
                {
                  Generate.Subtitle.Metadata.titleList.RemoveAt(selectedIndex);
                }

                // Language
                if (Generate.Subtitle.Metadata.languageList.ElementAtOrDefault(selectedIndex) != null)
                {
                  Generate.Subtitle.Metadata.languageList.RemoveAt(selectedIndex);
                }

                // Delay
                if (Generate.Subtitle.Metadata.delayList.ElementAtOrDefault(selectedIndex) != null)
                {
                  Generate.Subtitle.Metadata.delayList.RemoveAt(selectedIndex);
                }
              }
            }

            /// <summary>
            /// Subtitle Sort Down
            /// </summary>
            private void btnSubtitle_SortDown_Click(object sender, RoutedEventArgs e)
            {
              if (VM.SubtitleView.Subtitle_ListView_SelectedItems.Count > 0)
              {
                var selectedIndex = VM.SubtitleView.Subtitle_ListView_SelectedIndex;

                if (selectedIndex + 1 < VM.SubtitleView.Subtitle_ListView_Items.Count)
                {
                  // -------------------------
                  // List View
                  // -------------------------
                  // ListView Items
                  var itemlsvFileNames = VM.SubtitleView.Subtitle_ListView_Items[selectedIndex];
                  VM.SubtitleView.Subtitle_ListView_Items.RemoveAt(selectedIndex);
                  VM.SubtitleView.Subtitle_ListView_Items.Insert(selectedIndex + 1, itemlsvFileNames);

                  // List FilePaths
                  string itemFilePaths = Generate.Subtitle.Subtitle.subtitleFilePathsList[selectedIndex];
                  Generate.Subtitle.Subtitle.subtitleFilePathsList.RemoveAt(selectedIndex);
                  Generate.Subtitle.Subtitle.subtitleFilePathsList.Insert(selectedIndex + 1, itemFilePaths);

                  // List File Names
                  string itemFileNames = Generate.Subtitle.Subtitle.subtitleFileNamesList[selectedIndex];
                  Generate.Subtitle.Subtitle.subtitleFileNamesList.RemoveAt(selectedIndex);
                  Generate.Subtitle.Subtitle.subtitleFileNamesList.Insert(selectedIndex + 1, itemFileNames);

                  // -------------------------
                  // Metadata
                  // -------------------------
                  // Title
                  if (Generate.Subtitle.Metadata.titleList.ElementAtOrDefault(selectedIndex) != null)
                  {
                    var titleItem = Generate.Subtitle.Metadata.titleList[selectedIndex];
                    Generate.Subtitle.Metadata.titleList.RemoveAt(selectedIndex);
                    Generate.Subtitle.Metadata.titleList.Insert(selectedIndex + 1, titleItem);
                  }

                  // Language
                  if (Generate.Subtitle.Metadata.languageList.ElementAtOrDefault(selectedIndex) != null)
                  {
                    var titleItem = Generate.Subtitle.Metadata.languageList[selectedIndex];
                    Generate.Subtitle.Metadata.languageList.RemoveAt(selectedIndex);
                    Generate.Subtitle.Metadata.languageList.Insert(selectedIndex + 1, titleItem);
                  }

                  // Delay
                  if (Generate.Subtitle.Metadata.delayList.ElementAtOrDefault(selectedIndex) != null)
                  {
                    var titleItem = Generate.Subtitle.Metadata.delayList[selectedIndex];
                    Generate.Subtitle.Metadata.delayList.RemoveAt(selectedIndex);
                    Generate.Subtitle.Metadata.delayList.Insert(selectedIndex + 1, titleItem);
                  }

                  // -------------------------
                  // Highlight Selected Index
                  // -------------------------
                  VM.SubtitleView.Subtitle_ListView_SelectedIndex = selectedIndex + 1;
                }
              }
            }

            /// <summary>
            /// Subtitle Sort Up
            /// </summary>
            private void btnSubtitle_SortUp_Click(object sender, RoutedEventArgs e)
            {
              if (VM.SubtitleView.Subtitle_ListView_SelectedItems.Count > 0)
              {
                var selectedIndex = VM.SubtitleView.Subtitle_ListView_SelectedIndex;

                if (selectedIndex > 0)
                {
                  // -------------------------
                  // List View
                  // -------------------------
                  // ListView Items
                  var itemlsvFileNames = VM.SubtitleView.Subtitle_ListView_Items[selectedIndex];
                  VM.SubtitleView.Subtitle_ListView_Items.RemoveAt(selectedIndex);
                  VM.SubtitleView.Subtitle_ListView_Items.Insert(selectedIndex - 1, itemlsvFileNames);

                  // List File Paths
                  string itemFilePaths = Generate.Subtitle.Subtitle.subtitleFilePathsList[selectedIndex];
                  Generate.Subtitle.Subtitle.subtitleFilePathsList.RemoveAt(selectedIndex);
                  Generate.Subtitle.Subtitle.subtitleFilePathsList.Insert(selectedIndex - 1, itemFilePaths);

                  // List File Names
                  string itemFileNames = Generate.Subtitle.Subtitle.subtitleFileNamesList[selectedIndex];
                  Generate.Subtitle.Subtitle.subtitleFileNamesList.RemoveAt(selectedIndex);
                  Generate.Subtitle.Subtitle.subtitleFileNamesList.Insert(selectedIndex - 1, itemFileNames);

                  // -------------------------
                  // Metadata
                  // -------------------------
                  // Title
                  if (Generate.Subtitle.Metadata.titleList.ElementAtOrDefault(selectedIndex) != null)
                  {
                    var titleItem = Generate.Subtitle.Metadata.titleList[selectedIndex];
                    Generate.Subtitle.Metadata.titleList.RemoveAt(selectedIndex);
                    Generate.Subtitle.Metadata.titleList.Insert(selectedIndex - 1, titleItem);
                  }

                  // Language
                  if (Generate.Subtitle.Metadata.languageList.ElementAtOrDefault(selectedIndex) != null)
                  {
                    var titleItem = Generate.Subtitle.Metadata.languageList[selectedIndex];
                    Generate.Subtitle.Metadata.languageList.RemoveAt(selectedIndex);
                    Generate.Subtitle.Metadata.languageList.Insert(selectedIndex - 1, titleItem);
                  }

                  // Delay
                  if (Generate.Subtitle.Metadata.delayList.ElementAtOrDefault(selectedIndex) != null)
                  {
                    var titleItem = Generate.Subtitle.Metadata.delayList[selectedIndex];
                    Generate.Subtitle.Metadata.delayList.RemoveAt(selectedIndex);
                    Generate.Subtitle.Metadata.delayList.Insert(selectedIndex - 1, titleItem);
                  }

                  // -------------------------
                  // Highlight Selected Index
                  // -------------------------
                  VM.SubtitleView.Subtitle_ListView_SelectedIndex = selectedIndex - 1;
                }
              }
            }

            /// <summary>
            /// Subtitle Codec - ComboBox
            /// </summary>
            private void cboSubtitle_Codec_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
              string subtitle_Codec_SelectedItem = (sender as ComboBox).SelectedItem as string;

              // -------------------------
              // Halt if Selected Codec is Null
              // -------------------------
              if (string.IsNullOrWhiteSpace(subtitle_Codec_SelectedItem))
              {
                return;
              }

              // -------------------------
              // Codec Controls
              // -------------------------
              Controls.Subtitles.Controls.CodecControls(subtitle_Codec_SelectedItem);

              // -------------------------
              // Media Type Controls
              // Overrides Codec Controls
              // -------------------------
              // Must be after Codec Controls
              Controls.Format.Controls.MediaTypeControls();

              // -------------------------
              // Convert Button Text Change
              // -------------------------
              ConvertButtonText();
            }

            /// <summary>
            /// Subtitle Language Metadata - ComboBox
            /// </summary>
            private void cboSubtitle_Metadata_Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
              // -------------------------
              // Halts
              // -------------------------
              if (VM.SubtitleView.Subtitle_Stream_SelectedItem != "mux")
              {
                return;
              }

              // -------------------------
              // Language
              // -------------------------
              if (Generate.Subtitle.Metadata.languageList != null &&
                  Generate.Subtitle.Metadata.languageList.Count > 0)
              {
                // Set selected index
                int selectedIndex = VM.SubtitleView.Subtitle_ListView_SelectedIndex;

                // Remove previous from the list at selected track index
                if (Generate.Subtitle.Metadata.languageList.ElementAtOrDefault(selectedIndex) != null)
                {
                  try
                  {
                    Generate.Subtitle.Metadata.languageList.RemoveAt(selectedIndex);
                  }
                  catch
                  {

                  }
                }

                // Add to list
                try
                {
                  Generate.Subtitle.Metadata.languageList.Insert(selectedIndex, VM.SubtitleView.Subtitle_Metadata_Language_SelectedItem);
                }
                catch
                {

                }
              }
            }

            /// <summary>
            /// Subtitle Stream - ComboBox
            /// </summary>
            private void cboSubtitle_Stream_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
              // -------------------------
              // Mux
              // -------------------------
              // ListView Opacity
              if (VM.SubtitleView.Subtitle_Stream_SelectedItem == "mux" ||
                  VM.SubtitleView.Subtitle_Stream_SelectedItem == "external")
              {
                // Show
                VM.SubtitleView.Subtitle_ListView_Opacity = 1;
              }
              else
              {
                // Hide
                VM.SubtitleView.Subtitle_ListView_Opacity = 0.15;
              }

              // Enable Metadata
              // Mux
              if (VM.SubtitleView.Subtitle_Stream_SelectedItem == "mux")
              {
                // Enable ListView
                VM.SubtitleView.Subtitle_ListView_IsEnabled = true;
                // Enable Metadata
                VM.SubtitleView.Subtitle_Metadata_Title_IsEnabled = true;
                VM.SubtitleView.Subtitle_Metadata_Language_IsEnabled = true;
              }
              // External (Burn)
              else if (VM.SubtitleView.Subtitle_Stream_SelectedItem == "external")
              {
                // Enable ListView
                VM.SubtitleView.Subtitle_ListView_IsEnabled = true;
                // Disable Metadata
                VM.SubtitleView.Subtitle_Metadata_Title_IsEnabled = false;
                VM.SubtitleView.Subtitle_Metadata_Language_IsEnabled = false;
              }
              // Other Streams
              else
              {
                // Disable Subtitle Mux ListView and Buttons
                VM.SubtitleView.Subtitle_ListView_IsEnabled = false;
                VM.SubtitleView.Subtitle_Metadata_Title_IsEnabled = false;
                VM.SubtitleView.Subtitle_Metadata_Language_IsEnabled = false;
              }
            }

            /// <summary>
            /// Subtitle ListView
            /// </summary>
            private void lstvSubtitles_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
              // -------------------------
              // ListView
              // -------------------------
              // Clear before adding new selected items
              if (VM.SubtitleView.Subtitle_ListView_SelectedItems != null &&
                  VM.SubtitleView.Subtitle_ListView_SelectedItems.Count > 0)
              {
                VM.SubtitleView.Subtitle_ListView_SelectedItems.Clear();
                VM.SubtitleView.Subtitle_ListView_SelectedItems.TrimExcess();
              }

              // Create Selected Items List for ViewModel
              VM.SubtitleView.Subtitle_ListView_SelectedItems = lstvSubtitles.SelectedItems
                                                                             .Cast<string>()
                                                                             .ToList();

              // -------------------------
              // Set Metadata
              // -------------------------
              int selectedIndex = VM.SubtitleView.Subtitle_ListView_SelectedIndex;

              // Title
              if (Generate.Subtitle.Metadata.titleList.ElementAtOrDefault(selectedIndex) != null)
              {
                tbxSubtitle_Metadata_Title.Text = Generate.Subtitle.Metadata.titleList[selectedIndex];
              }
              else
              {
                tbxSubtitle_Metadata_Title.Text = string.Empty;
              }

              // Language
              if (Generate.Subtitle.Metadata.titleList.ElementAtOrDefault(selectedIndex) != null)
              {
                VM.SubtitleView.Subtitle_Metadata_Language_SelectedItem = Generate.Subtitle.Metadata.languageList[selectedIndex];

                // default
                if (string.IsNullOrWhiteSpace(VM.SubtitleView.Subtitle_Metadata_Language_SelectedItem))
                {
                  VM.SubtitleView.Subtitle_Metadata_Language_SelectedItem = "none";
                }
              }
              else
              {
                VM.SubtitleView.Subtitle_Metadata_Language_SelectedItem = "none";
              }

              // Delay
              if (Generate.Subtitle.Metadata.delayList.ElementAtOrDefault(selectedIndex) != null)
              {
                VM.SubtitleView.Subtitle_Delay_Text = Generate.Subtitle.Metadata.delayList[selectedIndex];
              }
              else
              {
                VM.SubtitleView.Subtitle_Delay_Text = string.Empty;
              }
            }

            private void tbxSubtitle_Metadata_Title_LostFocus(object sender, RoutedEventArgs e)
            {
              SaveMetadata_Subtitle_Title();
            }

    }
}
