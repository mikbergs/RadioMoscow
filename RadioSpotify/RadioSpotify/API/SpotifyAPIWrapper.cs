using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using System.Windows.Forms;

namespace RadioSpotify.API
{
    public class SpotifyAPIWrapper
    {
        private List<SavedTrack> _savedTracks;
        private static SpotifyWebAPI _spotify;

        //private Device _activeDevice;
        //public Device ActiveDevice { get; set; }
        public SpotifyWebAPI Spotify { get { return _spotify; } }
        public SpotifyAPIWrapper()
        {
            _spotify = new SpotifyWebAPI();
            _savedTracks = new List<SavedTrack>();
            initSpotify();            
        }
        public List<SavedTrack> SavedTracks { get { return _savedTracks; } }
        private async static void initSpotify()
        {
            WebAPIFactory webApiFactory = new WebAPIFactory(
                "http://localhost",
                8000,
                "3ea2752e8d2a43368ab6cf9efc56a0c4",
                Scope.UserReadPrivate | Scope.UserReadEmail | Scope.PlaylistReadPrivate | Scope.UserLibraryRead |
                Scope.UserReadPrivate | Scope.UserFollowRead | Scope.UserReadBirthdate | Scope.UserTopRead | Scope.PlaylistReadCollaborative |
                Scope.UserReadRecentlyPlayed | Scope.UserReadPlaybackState | Scope.UserModifyPlaybackState);

            try
            {
                _spotify = await webApiFactory.GetWebApi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The program needs to be run in admin mode\n" + ex.Message);
            }
            if (_spotify == null)
                return;
        }

        public string GetSpecificTrackName(string trackId)
        {
            FullTrack track = _spotify.GetTrack(trackId);
            return track.Name;
        }
        //Get's the currently playing track from spotify
        public string GetTrackName()
        {
            PlaybackContext track = _spotify.GetPlayingTrack();
            if (track?.Item != null)
                return track.Item.Name.ToString();
            else
                return "No track playing";
        }
        public void ChangeTrack(string trackUri)
        {
            ErrorResponse error = _spotify.ResumePlayback(uris: new List<string> { trackUri });
            if(error.Error != null)
            {
                AvailabeDevices devices = _spotify.GetDevices();
                _spotify.ResumePlayback(deviceId: devices.Devices.FirstOrDefault().Id,uris: new List<string> { trackUri });
            }
        }
        public bool GetPlaybackState()
        {
            PlaybackContext context = _spotify.GetPlayback();
            if(context.IsPlaying)
            {
                return true;
            }
            return false;
        }

        //Get all saved tracks
        public void GetSavedTracks()
        {
            Paging<SavedTrack> savedTracks = new Paging<SavedTrack>();
           
            //savedTracks.
            int offset = 0;
            savedTracks = _spotify.GetSavedTracks(50);
            //trackList = savedTracks.Items;
            int loops = savedTracks.Total / savedTracks.Limit;
            int rest = savedTracks.Total % savedTracks.Limit;

            //Fetch all saved tracks from spotify
            for (int i = 0; i < loops; ++i, offset += 50)
            {
                savedTracks = _spotify.GetSavedTracks(50, offset);
                
                _savedTracks.AddRange(savedTracks.Items);
            }
            //Fetch the ones from the modulo operation
            if (rest != 0)
            {
                savedTracks = _spotify.GetSavedTracks(rest, offset);
                _savedTracks.AddRange(savedTracks.Items);
            }
        }
    }
}
