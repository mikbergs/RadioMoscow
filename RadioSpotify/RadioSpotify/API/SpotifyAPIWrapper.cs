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
using System.Threading;
using System.Net;
using System.Collections.Specialized;
using System.Diagnostics;

namespace RadioSpotify.API
{
    public class SpotifyAPIWrapper
    {
        private List<SavedTrack> _savedTracks;
        private static SpotifyWebAPI _spotify;
        private static string _refreshToken;
        private static bool responseRecieved;
        private static DateTime _tokenCreated;
        

        private static string _clientId = "3ea2752e8d2a43368ab6cf9efc56a0c4"; //this is
        private static string _secretId = "57531c17ff964f8799443819e48d3840";

        public delegate void TokenRefreshedHandler();
        public event TokenRefreshedHandler OnTokenRefreshed;
        
        public SpotifyWebAPI Spotify { get { return _spotify; } }
        public DateTime TokenCreated { get { return _tokenCreated; } }
        //public String RefreshToken { get { return _refreshToken; } }
        public SpotifyAPIWrapper()
        {
            _spotify = new SpotifyWebAPI();
            _savedTracks = new List<SavedTrack>();
            //getKeys();
            initSpotify();            
        }
        public List<SavedTrack> SavedTracks { get { return _savedTracks; } }
        private static AutorizationCodeAuth auth;

        public void RefreshSpotifyApi()
        {
            Token token = auth.RefreshToken(_refreshToken, _secretId);
            _spotify.AccessToken = token.AccessToken;
            _tokenCreated = token.CreateDate;
            OnTokenRefreshed();

        }
        private static void initSpotify()
        {
            auth = new AutorizationCodeAuth();
            
            auth.ClientId = _clientId;
            auth.Scope = Scope.UserReadPrivate | Scope.UserReadEmail | Scope.PlaylistReadPrivate | Scope.UserLibraryRead |
                Scope.UserReadPrivate | Scope.UserFollowRead | Scope.UserReadBirthdate | Scope.UserTopRead | Scope.PlaylistReadCollaborative |
                Scope.UserReadRecentlyPlayed | Scope.UserReadPlaybackState | Scope.UserModifyPlaybackState;
            
            auth.RedirectUri = "http://localhost:4002/";
            auth.ShowDialog = true;
            auth.StartHttpServer(4002);
            auth.OnResponseReceivedEvent += Auth_OnResponseReceivedEvent;
            auth.State = "123";
            
            
            //auth.StartHttpServer();
            auth.DoAuth();
            Stopwatch connectionTimer = new Stopwatch();
            while (!responseRecieved && connectionTimer.ElapsedMilliseconds < 10000)
            {
                ;
            }
            auth.StopHttpServer();
        }

        private static void Auth_OnResponseReceivedEvent(AutorizationCodeAuthResponse response)
        {
            
            responseRecieved = false;
            String payload = response.Code;
            Token token = auth.ExchangeAuthCode(response.Code, _secretId);
            _tokenCreated = token.CreateDate;
            _refreshToken = token.RefreshToken;

            _spotify = new SpotifyWebAPI
            {
                AccessToken = token.AccessToken,
                TokenType = token.TokenType,
            };
            responseRecieved = true;            
        }

        public string GetSpecificTrackName(string trackId)
        {
            FullTrack track = _spotify.GetTrack(trackId);
            return track.Name;
        }
        //Get's the currently playing track and artist from spotify
        public string GetTrackString()
        {
            
            PlaybackContext track = _spotify.GetPlayingTrack();
            SimpleArtist artist = track.Item.Artists.FirstOrDefault();
            if (track?.Item != null)
                return artist.Name.ToString() + " - "+track.Item.Name.ToString();
            else
                return "No track playing";
        }
        public void ChangeTrack(string trackUri)
        {
            try
            {
                ErrorResponse error = _spotify.ResumePlayback(uris: new List<string> { trackUri });
                if (error.Error != null)
                {
                    AvailabeDevices devices = _spotify.GetDevices();
                    _spotify.ResumePlayback(deviceId: devices.Devices.FirstOrDefault().Id, uris: new List<string> { trackUri });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
           
            int offset = 0;
            savedTracks = _spotify.GetSavedTracks(50);            
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
        private static void getKeys()
        {
            _clientId = string.IsNullOrEmpty(_clientId)
                ? Environment.GetEnvironmentVariable("SPOTIFY_CLIENT_ID")
                : _clientId;

            _secretId = string.IsNullOrEmpty(_secretId)
                ? Environment.GetEnvironmentVariable("SPOTIFY_SECRET_ID")
                : _secretId;
        }

    }
}
