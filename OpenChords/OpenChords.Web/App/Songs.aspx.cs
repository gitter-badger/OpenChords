﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenChords.Web
{
    public partial class Songs : System.Web.UI.Page
    {


        private string SongName
        { get { return (string)this.Request.Params["Song"]; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SongList.DataBind();
                if (SongName != null)
                    SongList.SelectedValue = SongName;
            }
        }

        protected void SongList_NewSongSelected(object sender, EventArgs e)
        {
            var songName = SongList.SelectedValue;
            var song = OpenChords.Entities.Song.loadSong(songName);
            setGuiFromSongObject(song);
        }

        protected void cmdGoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }

        private void setGuiFromSongObject(OpenChords.Entities.Song song)
        {
            SongMetaData.fillInSongMeta(song);
            txtSongEditLyrics.Text = song.lyrics;
            txtSongEditNotes.Text = song.notes;
            pnlSongEdit.Visible = true;
        }

        private OpenChords.Entities.Song getSongObjectFromGui()
        {
            var song = SongMetaData.getSongObject();
            song.lyrics = txtSongEditLyrics.Text;
            song.notes = txtSongEditNotes.Text;
            return song;
        }

        protected void SongMetaData_PresentationOrderChanged(object sender, EventArgs e)
        {
            var song = getSongObjectFromGui();
            song.fixLyricsOrdering();
            song.fixNoteOrdering();
            setGuiFromSongObject(song);
        }

        protected void SongMetaData_SongKeyIncreased(object sender, EventArgs e)
        {
            var song = getSongObjectFromGui();
            song.transposeKeyUp();
            setGuiFromSongObject(song);
        }

        protected void SongMetaData_SongKeyDecreased(object sender, EventArgs e)
        {
            var song = getSongObjectFromGui();
            song.transposeKeyDown();
            setGuiFromSongObject(song);
        }

        protected void SongMetaData_SongCapoDecreased(object sender, EventArgs e)
        {
            var song = getSongObjectFromGui();
            song.capoDown();
            setGuiFromSongObject(song);
        }

        protected void SongMetaData_SongCapoIncreased(object sender, EventArgs e)
        {
            var song = getSongObjectFromGui();
            song.capoDown();
            setGuiFromSongObject(song);
        }

        protected void imgSave_Click(object sender, ImageClickEventArgs e)
        {
          
            var song = getSongObjectFromGui();
            song.saveSong();
        }

        protected void imgCancel_Click(object sender, ImageClickEventArgs e)
        {
            pnlSongEdit.Visible = false;
        }

        protected void imgHtml_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Display.aspx?Song=" + SongList.SelectedValue);
        }

        


 


    }
}