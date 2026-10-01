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
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using ViewModel;
using System.Collections.ObjectModel;
// Disable XML Comment warnings
#pragma warning disable 1591
#pragma warning disable 1587
#pragma warning disable 1570

namespace Axiom
{
  public partial class MainWindow : HandyControl.Controls.Window
  {
    // Note: Container and Cut ComboBox event handlers moved to FormatControl.xaml.cs
    // Note: Cut Start/End TextBox event handlers moved to FormatControl.xaml.cs

    /// <summary>
    /// YouTube Download Check (Method)
    /// </summary>
    /// <remarks>
    /// Check if youtube-dl.exe is on Computer 
    /// </remarks>
    public static bool YouTubeDownloadCheck()
    {
      if (File.Exists(youtubedl))
      {
        return true;
      }

      return false;
    }

    /// <summary>
    /// YouTube Download - URL (Method)
    /// </summary>
    public static String YouTubeDownloadURL(string url)
    {
      // Strip URL Parameters
      int index = url.IndexOf("&");
      if (index > 0)
        url = url.Substring(0, index);

      return url;
    }

    /// <summary>
    /// YouTube Download - Format (Method)
    /// </summary>
    /// <remarks>
    /// For YouTube downloads - use mp4, as most users can play this format on their PC
    /// For Other Websites - use mkv for merging format, in case the video+audio codecs can't be merged to mp4
    /// </remarks>
    public static String YouTubeDownloadFormat(string youtubedl_SelectedItem,
                                               string videoCodec_SelectedItem,
                                               string subtitleCodec_SelectedItem,
                                               string audioCodec_SelectedItem
                                               )
    {
      // Video + Audio
      if (youtubedl_SelectedItem == "Video + Audio")
      {
        // Use mp4 for Download-Only Mode
        if (IsWebDownloadOnly(videoCodec_SelectedItem,
                              subtitleCodec_SelectedItem,
                              audioCodec_SelectedItem) == true
                              )
        {
          return "mp4";
        }

        // Use mkv for converting
        else
        {
          return "mkv";
        }
      }

      // Video Only
      else if (youtubedl_SelectedItem == "Video Only")
      {
        // Use mp4 for Download-Only Mode
        if (IsWebDownloadOnly(videoCodec_SelectedItem,
                              subtitleCodec_SelectedItem,
                              audioCodec_SelectedItem) == true
                              )
        {
          return "mp4";
        }

        // Use mkv for converting
        else
        {
          return "mkv";
        }
      }

      // Audio Only
      else if (youtubedl_SelectedItem == "Audio Only")
      {
        // Can only use m4a, not mp3

        return "m4a";
      }

      return string.Empty;
    }

    /// <summary>
    /// YouTube Download - Quality (Method)
    /// </summary>
    public static String YouTubeDownloadQuality(string input_Text,
                                                string youtubedl_SelectedItem,
                                                string youtubedl_Quality_SelectedItem
                                                )
    {
      // -------------------------
      // Video + Audio
      // -------------------------
      if (youtubedl_SelectedItem == "Video + Audio")
      {
        switch (youtubedl_Quality_SelectedItem)
        {
          // Best
          case "best":
            return "bestvideo[ext=mp4]+bestaudio[ext=m4a]/bestvideo+bestaudio/best";

          // Best 4K
          case "best 4K":
            return "bestvideo[height=2160][ext=mp4]+bestaudio[ext=m4a]/bestvideo[height=2160]+bestaudio/bestvideo[ext=mp4]+bestaudio[ext=m4a]/bestvideo+bestaudio/best";

          // Best 1080p
          case "best 1080p":
            return "bestvideo[height=1080][ext=mp4]+bestaudio[ext=m4a]/bestvideo[height=1080]+bestaudio/bestvideo[ext=mp4]+bestaudio[ext=m4a]/bestvideo+bestaudio/best";

          // Best 720p
          case "best 720p":
            return "bestvideo[height=720][ext=mp4]+bestaudio[ext=m4a]/bestvideo[height=720]+bestaudio/bestvideo[ext=mp4]+bestaudio[ext=m4a]/bestvideo+bestaudio/best";

          // Best 480p
          case "best 480p":
            return "bestvideo[height=480][ext=mp4]+bestaudio[ext=m4a]/bestvideo[height=480]+bestaudio/bestvideo[ext=mp4]+bestaudio[ext=m4a]/bestvideo+bestaudio/best";

          // Worst
          case "worst":
            return "worstvideo[ext=mp4]+worstaudio[ext=m4a]/worstvideo+worstaudio/worst";
        }
      }

      // -------------------------
      // Video Only
      // -------------------------
      else if (youtubedl_SelectedItem == "Video Only")
      {
        switch (youtubedl_Quality_SelectedItem)
        {
          // Best
          case "best":
            return "bestvideo[ext=mp4]/bestvideo";

          // Best 4K
          case "best 4K":
            return "bestvideo[height=2160][ext=mp4]/bestvideo[ext=mp4]/bestvideo";

          // Best 1080p
          case "best 1080p":
            return "bestvideo[height=1080][ext=mp4]/bestvideo[ext=mp4]/bestvideo";

          // Best 720p
          case "best 720p":
            return "bestvideo[height=720p][ext=mp4]/bestvideo[ext=mp4]/bestvideo";

          // Best 480p
          case "best 480p":
            return "bestvideo[height=480p][ext=mp4]/bestvideo[ext=mp4]/bestvideo";

          // Worst
          case "worst":
            return "worstvideo[ext=mp4]/worstvideo";

        }
      }

      // -------------------------
      // Audio Only
      // -------------------------
      else if (youtubedl_SelectedItem == "Audio Only")
      {
        // Best
        if (youtubedl_Quality_SelectedItem == "best")
        {
          return "bestaudio[ext=m4a]/bestaudio";
        }
        // Worst
        else if (youtubedl_Quality_SelectedItem == "worst")
        {
          return "worstaudio[ext=m4a]/worstaudio";
        }
      }

      return string.Empty;
    }

    /// <summary>
    /// YouTube Method - Selection Changed
    /// </summary>
    private void cboYouTube_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // Video + Audio
      // Video Only
      if (VM.FormatView.Format_YouTube_SelectedItem == "Video + Audio" ||
          VM.FormatView.Format_YouTube_SelectedItem == "Video Only")
      {
        // Change Items Source
        VM.FormatView.Format_YouTube_Quality_Items = new ObservableCollection<string>()
                {
                    "best",
                    "best 4K",
                    "best 1080p",
                    "best 720p",
                    "best 480p",
                    "worst"
                };

        // Select Default
        VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";
      }

      // Audio Only
      else if (VM.FormatView.Format_YouTube_SelectedItem == "Audio Only")
      {
        // Change Items Source
        VM.FormatView.Format_YouTube_Quality_Items = new ObservableCollection<string>()
                {
                    "best",
                    "worst"
                };

        // Select Default
        VM.FormatView.Format_YouTube_Quality_SelectedItem = "best";
      }
    }

    /// <summary>
    /// YouTube Download - Merge Output Format
    /// </summary>
    //public static String YouTubeDownload_MergeOutputFormat()
    //{

    //    if (IsWebDownloadOnly() == true)
    //    {
    //        return string.Empty;
    //    }
    //    else
    //    {
    //        return "--merge-output-format " + YouTubeDownloadFormat(VM.FormatView.Format_YouTube_SelectedItem,
    //                                                                VM.VideoView.Video_Codec_SelectedItem,
    //                                                                VM.SubtitleView.Subtitle_Codec_SelectedItem,
    //                                                                VM.AudioView.Audio_Codec_SelectedItem
    //                                                                );
    //    }
    //}

  }
}
