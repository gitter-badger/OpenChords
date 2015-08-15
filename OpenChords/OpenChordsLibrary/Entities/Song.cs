﻿/*
 * Created by SharpDevelop.
 * User: vana
 * Date: 2010/11/12
 * Time: 05:40 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OpenChords.Functions;
using OpenChords.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;

// 
// This source code was auto-generated by xsd, Version=2.0.50727.3038.
// 

namespace OpenChords.Entities
{

    [System.SerializableAttribute()]
    [XmlRoot("song")]
    public partial class Song
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string title { get; set; }
        public string author { get; set; }
        public string presentation { get; set; }
        public string capo { get; set; }
        public string key { get; set; }
        public string lyrics { get; set; }
        public string ccli { get; set; }
        public string preferFlats { get; set; }
        public string tempo { get; set; }
        public string time_sig { get; set; }
        public string copyright { get; set; }
        public string hymn_number { get; set; }
        public string notes { get; set; }
        public string bbm { get; set; }

        [XmlIgnore]
        public string SongFileName { get; set; }


        [XmlIgnore]
        internal string songFilePath { get; set; }

        [XmlIgnore]
        public int Capo
        {
            get
            {
                var capoValue = 0;
                int.TryParse(capo, out capoValue);
                return capoValue;
            }
            set
            {
                this.capo = value.ToString();
            }
        }

        [XmlIgnore]
        public bool PreferFlats
        {
            get
            {
                var value = false;
                if (!string.IsNullOrEmpty(preferFlats))
                    bool.TryParse(preferFlats, out value);
                return value;
            }
            set
            {
                preferFlats = value.ToString();
            }
        }

        [XmlIgnore]
        public int BeatsPerMinute
        {
            get
            {
                int value = 100;
                if (!string.IsNullOrEmpty(bbm))
                    int.TryParse(bbm, out value);
                return value;
            }
            set
            {
                if (value < 0) value = 0;
                bbm = value.ToString();
            }
        }

        public Song()
        {
            title = "";
            author = "";
            key = "";
            capo = "0";
            presentation = "";

            lyrics = "";
            notes = "";
            preferFlats = "false";
            BeatsPerMinute = 100;
        }




        public static Song loadSong(string SongName)
        {
            string filename = Settings.ExtAppsAndDir.songsFolder + SongName;
            if (!File.Exists(filename))
                throw new Exception(string.Format("Song: {0} does not exist", SongName));
            Song song = XmlReaderWriter.readSong(filename);
            return song;

        }

        /// <summary>
        /// returns a list of all songs in the system
        /// </summary>
        /// <returns></returns>
        public static List<string> listOfAllSongs()
        {
            return IO.FileFolderFunctions.getDirectoryListingAsList(Settings.ExtAppsAndDir.songsFolder);
        }

        public void saveSong()
        {
            if (this.title != "")
            {
                XmlReaderWriter.writeSong(Settings.ExtAppsAndDir.songsFolder + this.title, this);
            }
        }

        internal void saveSong(string destination)
        {
            XmlReaderWriter.writeSong(destination, this);
        }

        public void deleteSong()
        {
            if (this.songFilePath != "")
            {
                FileFolderFunctions.deleteFile(this.songFilePath);
            }
        }

        public void revertToSaved()
        {
            Song oldSave = loadSong(title);
            this.title = oldSave.title;
            this.notes = oldSave.notes;
            this.author = oldSave.author;
            this.Capo = oldSave.Capo;
            this.ccli = oldSave.ccli;
            this.key = oldSave.key;
            this.lyrics = oldSave.lyrics;
            this.presentation = oldSave.presentation;
        }

        public void transposeKeyUp()
        {
            SongProcessor.transposeKeyUp(this);
            this.key = SongProcessor.transposeChord(this.key, this.PreferFlats).TrimEnd();
        }

        public void transposeKeyDown()
        {
            for (int i = 0; i < 11; i++)
            {
                transposeKeyUp();
            }
        }

        public void capoUp()
        {
            for (int i = 0; i < 11; i++)
            {
                capoDown();
            }

        }

        public void capoDown()
        {
            SongProcessor.transposeKeyUp(this);
            int tempcapo = 12 + this.Capo - 1;
            this.Capo = tempcapo % 12;
        }

        /// <summary>
        /// generate long title
        /// </summary>
        /// <param name="song"></param>
        /// <returns></returns>
        public string generateLongTitle()
        {
            StringBuilder songTitleLine = new StringBuilder(this.title.Trim() + " ");

            //fill in key and capo
            if (!string.IsNullOrEmpty(this.key))
            {
                songTitleLine.Append("(Key - " + this.key.Trim());

                if (this.Capo != 0)
                {
                    songTitleLine.Append(" Capo - " + this.Capo);
                }

                songTitleLine.Append(")");
            }
            if (!string.IsNullOrEmpty(this.time_sig))
                songTitleLine.Append(" " + this.time_sig);
            if (!string.IsNullOrEmpty(this.tempo))
                songTitleLine.Append(" " + this.tempo);

            return songTitleLine.ToString();
        }

        /// <summary>
        /// returns true if the song exists
        /// </summary>
        /// <param name="songName"></param>
        /// <returns></returns>
        public static bool Exists(string songName)
        {
            string filename = Settings.ExtAppsAndDir.songsFolder + songName;
            return File.Exists(filename);
        }

        /// <summary>
        /// generate long title
        /// </summary>
        /// <param name="song"></param>
        /// <returns></returns>
        public string generateShortTitle()
        {
            StringBuilder songTitleLine = new StringBuilder(this.title.Trim() + " ");

            //fill in key and capo
            if (!string.IsNullOrEmpty(this.key))
            {
                songTitleLine.Append("(Key - " + this.key.Trim());

                if (this.Capo != 0)
                {
                    songTitleLine.Append(" Capo - " + this.Capo);
                }

                songTitleLine.Append(")");
            }
            return songTitleLine.ToString();
        }

        public void fixFormatting()
        {
            if (!this.lyrics.Contains("["))
            {
                var importedLyrics = Functions.ImportSong.importLyrics(this.lyrics);
                this.lyrics = importedLyrics;
                this.presentation = Functions.ImportSong.importPresentationList(importedLyrics);
                fixNoteOrdering();
                fixLyricsOrdering();
            }

            SongProcessor.fixFormatting(this);
        }

        public void fixNoteOrdering()
        {
            SongProcessor.fixNoteOrdering(this);
        }

        public void fixLyricsOrdering()
        {
            SongProcessor.fixLyricsOrdering(this);
            SongProcessor.fixFormatting(this);
        }



        public bool isMp3Available()
        {

            string songName = Settings.ExtAppsAndDir.mediaFolder + this.title;

            if (FileFolderFunctions.isFilePresent(songName + ".mp3"))
                return true;
            else if (FileFolderFunctions.isFilePresent(songName + ".mp4"))
                return true;
            else if (FileFolderFunctions.isFilePresent(songName + ".avi"))
                return true;
            else if (FileFolderFunctions.isFilePresent(songName + ".ogg"))
                return true;
            else if (FileFolderFunctions.isFilePresent(songName + ".flv"))
                return true;
            else if (FileFolderFunctions.isFilePresent(songName + ".mkv"))
                return true;

            return false;

        }

        public string getMp3Filename()
        {
            string songName = Settings.ExtAppsAndDir.mediaFolder + this.title;

            if (FileFolderFunctions.isFilePresent(songName + ".mp3"))
                return songName + ".mp3";
            else if (FileFolderFunctions.isFilePresent(songName + ".mp4"))
                return songName + ".mp4";
            else if (FileFolderFunctions.isFilePresent(songName + ".avi"))
                return songName + ".avi";
            else if (FileFolderFunctions.isFilePresent(songName + ".ogg"))
                return songName + ".ogg";
            else if (FileFolderFunctions.isFilePresent(songName + ".flv"))
                return songName + ".flv";
            else if (FileFolderFunctions.isFilePresent(songName + ".mkv"))
                return songName + ".mkv";

            return null;

        }

        public List<SongVerse> getSongVerses()
        {
            return SongVerse.getSongVerses(this);
        }

        public void displayPdf(DisplayAndPrintSettingsType settingsType)
        {
            string pdfPath = getPdfPath(settingsType);

            //get the filemanager for the filesystem
            string fileManager = Settings.ExtAppsAndDir.fileManager;
            if (string.IsNullOrEmpty(fileManager))
                System.Diagnostics.Process.Start(pdfPath);
            else
                System.Diagnostics.Process.Start(fileManager, pdfPath);


        }

        /// <summary>
        /// returns the song in html format
        /// </summary>
        /// <returns></returns>
        public string getHtml(DisplayAndPrintSettings settings)
        {
            Export.ExportToHtml htmlExporter = new Export.ExportToHtml(this, settings);
            var result = htmlExporter.GenerateHtml();
            return result;
        }

        public string ExportToHtml(DisplayAndPrintSettings settings)
        {
            Export.ExportToHtml htmlExporter = new Export.ExportToHtml(this, settings);
            string htmlText = htmlExporter.GenerateHtml();
            string folder = null;
            if (settings.settingsType == DisplayAndPrintSettingsType.TabletSettings)
                folder = Settings.ExtAppsAndDir.tabletFolder;
            else
                folder = Settings.ExtAppsAndDir.printFolder;
            string destination = String.Format("{0}/{1}.html", folder, this.generateShortTitle());
            File.WriteAllText(destination, htmlText);
            return destination;
        }

        /// <summary>
        /// create the pdf and return the path to the pdf
        /// </summary>
        /// <param name="settingsType"></param>
        /// <returns></returns>
        public string getPdfPath(DisplayAndPrintSettingsType settingsType)
        {
            string pdfPath;
            pdfPath = Export.ExportToPdf.exportSong(this, settingsType);
            pdfPath = Settings.ExtAppsAndDir.printFolder + pdfPath;
            return pdfPath;
        }

        /// <summary>
        /// create the pdf and return the path to the pdf
        /// </summary>
        /// <param name="settingsType"></param>
        /// <returns></returns>
        public string getPdfPath(DisplayAndPrintSettingsType settingsType, string settingsPath)
        {
            string pdfPath;
            pdfPath = Export.ExportToPdf.exportSong(this, settingsType, settingsPath);
            pdfPath = Settings.ExtAppsAndDir.printFolder + pdfPath;
            return pdfPath;
        }

        public static List<string> TimeSignatureOptions()
        {
            var options = new List<string>();
            options.Add("-Select time signature-");
            options.Add("2/4");
            options.Add("3/4");
            options.Add("4/4");
            options.Add("3/8");
            options.Add("6/8");
            return options;
        }

        public static List<string> TempoOptions()
        {
            var options = new List<string>();
            options.Add("-Select tempo-");
            options.Add("Very Fast");
            options.Add("Fast");
            options.Add("Moderate");
            options.Add("Slow");
            options.Add("Very Slow");
            return options;
        }






        public void ToggleSharpsAndFlats()
        {
            this.PreferFlats = !this.PreferFlats;
            this.transposeKeyUp();
            this.transposeKeyDown();
           
        }

        public string getFullPath()
        {
            return Settings.ExtAppsAndDir.songsFolder + this.title;
        }
    }





}
