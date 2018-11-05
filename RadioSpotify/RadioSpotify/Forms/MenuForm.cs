using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using WMPLib;

namespace RadioSpotify
{
    public partial class MenuForm : Form
    {

        MenuFacade menuFacade = new MenuFacade();
        public MenuForm()
        {
            InitializeComponent();
            menuFacade.SRPlayer.PlayStateChange += SRPlayer_PlayStateChange;
            menuFacade.TimerWrapper.Timer.Tick += OnTimedEvent;
            menuFacade.OnPlaylistUpdate += new MenuFacade.PlaylistUpdateHandler(UpdateGUI);
            menuFacade.OnSpotifyUpdate += new MenuFacade.PlaylistUpdateHandler(UpdateSpotifyLabels);
            //Spotify labels
            lblSpotifyStatusCaption.Text = menuFacade.SpotifyWrapper.GetPlaybackState() == true ? menuFacade.SpotifyWrapper.GetTrackName() : "not playing";
            //lblSpotifyConnectionStatus.Text = menuFacade.SpotifyWrapper.Spotify.GetPrivateProfile().Error?.Message;

            //SR labels
            lblSRPrevCaption.Text = menuFacade.SRPlaylist.PreviousSong?.Title != null ? menuFacade.SRPlaylist.PreviousSong?.Description : null;
            lblSRCurrentCaption.Text = menuFacade.SRPlaylist.Song?.Title != null ? menuFacade.SRPlaylist.Song?.Description : "No song right now";
            lblSRNextCaption.Text = menuFacade.SRPlaylist.NextSong?.Title != null ? menuFacade.SRPlaylist.NextSong?.Description : null;

            
            //ComboBox for SR
            cbChannelSelector.DataSource = menuFacade.ChannelList.channels;
            cbChannelSelector.DisplayMember = "Name";
            cbChannelSelector.ValueMember = "Id";
            cbChannelSelector.DropDownStyle = ComboBoxStyle.DropDownList;

            //ComboBox for Spotify
            //cbDeviceList.DataSource = menuFacade.SpotifyWrapper.Spotify.GetDevices().Devices;
            //cbDeviceList.DisplayMember = "Name";
            //cbDeviceList.ValueMember = "Id";
            //cbDeviceList.DropDownStyle = ComboBoxStyle.DropDownList;
            
        }
        private void UpdateSpotifyLabels()
        {
            lblSpotifyStatusCaption.Text = menuFacade.SpotifyWrapper.GetPlaybackState() == true ? menuFacade.SpotifyWrapper.GetTrackName() : "not playing";
        }
        private void UpdateGUI()
        {            

            lblSRPrevCaption.Text = menuFacade.SRPlaylist.PreviousSong != null ? menuFacade.SRPlaylist.PreviousSong?.Description : null;
            lblSRCurrentCaption.Text = menuFacade.SRPlaylist.Song != null ? menuFacade.SRPlaylist.Song?.Description : "No song right now";
            lblSRNextCaption.Text = menuFacade.SRPlaylist.NextSong != null ? menuFacade.SRPlaylist.NextSong?.Description : null;

            lblSRPrevStartTimeCaption.Text = menuFacade.SRPlaylist.PreviousSong != null ? menuFacade.SRPlaylist.PreviousSong?.StartTimeUTC.ToString() : null;
            lblSRCurrentStartTimeCaption.Text = menuFacade.SRPlaylist.Song != null ? menuFacade.SRPlaylist.Song?.StartTimeUTC.ToString() : null;
            lblSRNextStartTimeCaption.Text = menuFacade.SRPlaylist.NextSong != null ? menuFacade.SRPlaylist.NextSong?.StartTimeUTC.ToString() : null;

            lblSRPrevStopTimeCaption.Text = menuFacade.SRPlaylist.PreviousSong != null ? menuFacade.SRPlaylist.PreviousSong?.StopTimeUTC.ToString() : null;
            lblSRCurrentStopTimeCaption.Text = menuFacade.SRPlaylist.Song != null ? menuFacade.SRPlaylist.Song?.StopTimeUTC.ToString() : null;
            lblSRNextStopTimeCaption.Text = menuFacade.SRPlaylist.NextSong != null ? menuFacade.SRPlaylist.NextSong?.StopTimeUTC.ToString() : null;

        }

        //Update if the SRPlayer state changes.
        private void SRPlayer_PlayStateChange(int newState)
        {
            switch (newState)
            {
                case (int)WMPPlayState.wmppsPlaying:
                    btnChangeSRMode.Text = "Stop";
                    return;
                case (int)WMPPlayState.wmppsPaused:
                    btnChangeSRMode.Text = "Play";
                    return;
            }
        }

        //If the channel combobox changes, this is the event.
        private void cbChannelSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            menuFacade.SpotifyWrapper.Spotify.PausePlayback();
            menuFacade.UpdatePlaylist(cbChannelSelector.SelectedItem as RadioSpotify.API.Channel);
            menuFacade.changeChannelonSRStream(cbChannelSelector.SelectedItem as RadioSpotify.API.Channel);
            menuFacade.SetupAfterChannelChange(menuFacade.SRPlaylist.PreviousSong, menuFacade.SRPlaylist.Song, menuFacade.SRPlaylist.NextSong);
            UpdateGUI();
        }

        //Changes the string on the SR button
        private void btnChangeSRMode_Click(object sender, EventArgs e)
        {
            btnChangeSRMode.Text = menuFacade.ChangeStateSRPlayer();
        }

        private void btnReplacement_Click(object sender, EventArgs e)
        {
            if (sender.Equals(btnPrevReplacement)) { lblPrevReplacement.Text = menuFacade.GetReplacementSong(menuFacade.SRPlaylist.PreviousSong).Uri; }
            if (sender.Equals(btnCurrentReplacement)) { lblCurrentReplacement.Text = menuFacade.GetReplacementSong(menuFacade.SRPlaylist.Song).Uri; }
            if (sender.Equals(btnNextReplacement)) { lblNextReplacement.Text = menuFacade.GetReplacementSong(menuFacade.SRPlaylist.NextSong).Uri; }
        }
        private void OnTimedEvent(Object source, EventArgs e)
        {
            toolStripTime.Text = ("Radio Moscow's time zone is UTC+0.9999977... - " + DateTime.Now.AddSeconds(Constants.streamDelay));
            if (!menuFacade.SpotifyWrapper.GetPlaybackState() && menuFacade.checkIfNewSongListed()) 
            {
                UpdateGUI();
                menuFacade.SetupAfterChannelChange(currentSong: menuFacade.SRPlaylist.Song, nextSong: menuFacade.SRPlaylist.NextSong);
            }
        }

        private void btnSpotifyConnect_Click(object sender, EventArgs e)
        {
            menuFacade.SpotifyWrapper.GetPlaybackState();
        }
    }
}
     
