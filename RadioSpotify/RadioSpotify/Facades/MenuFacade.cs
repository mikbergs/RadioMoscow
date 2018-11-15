using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RadioSpotify.API;
using System.Media;
using WMPLib;
using System.Diagnostics;
using System.Reflection;
using log4net;

namespace RadioSpotify
{
    public class MenuFacade
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType); //logger
        //private static SpotifyWebAPI _spotify;
        private static SpotifyAPIWrapper _spotifyWrapper;
        private static SRApi _sr;
        private WindowsMediaPlayer _srPlayer;
        private TimerWrapper _timerWrapper;
        private FullTrack replacementTrack;
        private Channel currentChannel;
        private Playlist _srPlaylist;
        private Task _songStartTask;
        private Task _songEndTask;
        private Task refreshTokenTask;
        
        public event _WMPOCXEvents_PlayStateChangeEventHandler testEvent = delegate { };
        public TimerWrapper TimerWrapper { get { return _timerWrapper; } }
        public SpotifyAPIWrapper SpotifyWrapper { get  { return _spotifyWrapper; } }
        public WindowsMediaPlayer SRPlayer { get { return _srPlayer; } } 
        public Playlist SRPlaylist { get { return _srPlaylist; } }
        
        public ChannelList ChannelList { get; set; }
        public Task SongStartTask { get { return _songStartTask; } }
        public Task SongEndTask { get { return _songEndTask; } }
        



        public delegate void PlaylistUpdateHandler();
        public event PlaylistUpdateHandler OnPlaylistUpdate;
        public event PlaylistUpdateHandler OnSpotifyUpdate;


        public MenuFacade()
        {
            while (!checkIfSpotifyIsRunning())
            {
                var response = MessageBox.Show(null, "Start Spotify before starting Radio Moscow. Start Spotify and press OK\nor press cancel to exit.", "Error", MessageBoxButtons.OKCancel);
                if (response.Equals(DialogResult.Cancel))
                    Environment.Exit(0);
            }
            _spotifyWrapper = new SpotifyAPIWrapper();
            _spotifyWrapper.OnTokenRefreshed += new SpotifyAPIWrapper.TokenRefreshedHandler(CreateScheduleForRefreshToken);
            
            _sr = new SRApi();            
            _srPlaylist = new Playlist();
            _timerWrapper = new TimerWrapper();
            InitSRPlayer();
            CreateScheduleForRefreshToken();

            _srPlaylist = _sr.GetPlaylist(132);
            ChannelList = _sr.GetChannels();
            _spotifyWrapper.GetSavedTracks();
        }
        /// <summary>
        /// fetches an replacement song from the spotify saved tracks
        /// </summary>
        /// <param name="song">A song from SR</param>
        public FullTrack GetReplacementSong(Song song, int offset = 0)
        {
            var songDuration = song.StopTimeUTC - song.StartTimeUTC;
            int songDurationMs = (int)songDuration.TotalMilliseconds - offset;

            _log.Info(String.Format("Get replacement for {0}, song duration: {1}", song.Title, songDurationMs));

            var durationMatch = _spotifyWrapper.SavedTracks.FindAll(x => x.Track.DurationMs == songDurationMs);
            //If there's >= track with matching duration, take out a random one.
            if (durationMatch.Count > 1)
            {
                return PickRandomSong(durationMatch);
            }

            var orderedTracks = _spotifyWrapper.SavedTracks.OrderBy(x => x.Track.DurationMs);      
              
            var remainingTracks = orderedTracks.SkipWhile(x => x.Track.DurationMs < songDurationMs);
            var removedTracks = orderedTracks.Count() - remainingTracks.ToList().Count() - 1;
            if (removedTracks < 0) removedTracks = 0;
            var closestMatch = GetClosestMatch(orderedTracks.ElementAt(removedTracks), remainingTracks?.First(), songDurationMs);

            var multipleMatches = _spotifyWrapper.SavedTracks.FindAll(x => x.Track.DurationMs == closestMatch.Track.DurationMs);
            if(multipleMatches.Count > 1)
            {
               
                return PickRandomSong(multipleMatches);
            }

            return closestMatch.Track;
           
        }
        /// <summary>
        /// Get's a 'random' Track from a collection
        /// </summary>
        public FullTrack PickRandomSong(List<SavedTrack> tracks)
        {
            tracks.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            var match = tracks.FirstOrDefault();
            _log.Info(String.Format("Picked random {0}, nr of matches {1}", match.Track.Uri, tracks.Count));
            return match.Track;
        }
        /// <param name="shorterSong"></param>
        /// <param name="longerSong"></param>
        /// <param name="srSongDuration"></param>
        /// <returns></returns>
        public SavedTrack GetClosestMatch(SavedTrack shorterSong, SavedTrack longerSong, double srSongDuration)
        {
            var longerThan = longerSong.Track.DurationMs - srSongDuration;
            var shorterThan = srSongDuration - shorterSong.Track.DurationMs;
            
            var result =  longerThan < shorterThan ? longerSong : shorterSong;

            _log.Info(String.Format("GetClosestMatch: longerThan: {0}, shorterThan: {1}, result: {2}, srSongDuration: {3}", longerThan, shorterThan, result.Track.DurationMs,srSongDuration));
            return result;

        }
        //Updates the playlist.
        public void UpdatePlaylist(Channel channel)
        {
            _srPlaylist = _sr.GetPlaylist(channel.Id);
        }

        //change the channel on the SR stream
        public void changeChannelonSRStream(Channel channel)
        {
            currentChannel = channel;
            _srPlayer.URL = "http://sverigesradio.se/topsy/direkt/srapi/"+channel.Id+".mp3";
        }

        private void InitSRPlayer()
        {
            _srPlayer = new WindowsMediaPlayer();
            _srPlayer.URL = "http://sverigesradio.se/topsy/direkt/srapi/164.mp3";
        }

        public string ChangeStateSRPlayer()
        {
            switch (_srPlayer.playState)
            {
                case WMPPlayState.wmppsPlaying:
                    _srPlayer.controls.stop();
                    return "Play";

                case WMPPlayState.wmppsStopped:
                    _srPlayer.controls.play();
                    return "Stop";
                case WMPPlayState.wmppsBuffering:
                    return "Buffering";
                default:
                    _srPlayer.controls.play();
                    return "Play";
            }
        }

        //Does the neccesary preparments when there's a track listed as comming.
        public void CreateScheduleForSongStart(Song song)
        {
            _log.Info((String.Format("Create schefule for song start at: {0}", song.StartTimeUTC)));
            replacementTrack = GetReplacementSong(song);
            _songStartTask = _timerWrapper.ScheduleAction(ChangeToSpotify, song.StartTimeUTC,DateTime.Now.AddSeconds(Constants.streamDelay));
        }
        public void CreateScheduleForSongEnd(Song song)
        {
            _log.Info((String.Format("Create schefule for song end at: {0}", song.StopTimeUTC)));
            _songEndTask = _timerWrapper.ScheduleAction(ChangeToSR, song.StopTimeUTC, DateTime.Now.AddSeconds(Constants.streamDelay));
        }
        //Sets up schedule for the token refreshment
        public void CreateScheduleForRefreshToken()
        {
            _log.Info((String.Format("Creating refresh schedule at {0}", _spotifyWrapper.TokenCreated.AddMinutes(59))));
            refreshTokenTask = _timerWrapper.ScheduleAction(_spotifyWrapper.RefreshSpotifyApi, _spotifyWrapper.TokenCreated.AddMinutes(59), DateTime.Now.AddSeconds(Constants.streamDelay));
        }
        
        //Change to SpotifyPlayback.
        private void ChangeToSpotify()
        {
            _log.Info(String.Format("Changing to Spotify. Track URI: {0}",replacementTrack.Uri));
            try
            {
                int retries = 0;
                //ChangeStateSRPlayer();
                _srPlayer.controls.stop();
                //_spotifyWrapper.ChangeTrack("spotify: track:2hu89E3V6LXHm46qfkLAVA"); // en som inte "finns"

                _spotifyWrapper.ChangeTrack(replacementTrack.Uri);
                while (!_spotifyWrapper.GetPlaybackState() || retries < 5)
                    retries++;
                OnSpotifyUpdate();
                _songStartTask = null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }
        private void ChangeToSR()
        {
            _log.Info(String.Format("Changing to SR. Channel: {0}",currentChannel.Name));
            var retries = 0;
            _spotifyWrapper.Spotify.PausePlayback();
            while (_spotifyWrapper.GetPlaybackState() || retries < 5)
                retries++;
            OnSpotifyUpdate();
            changeChannelonSRStream(currentChannel);
            _songEndTask = null;
        }
        public bool CheckIfPlaying(Song song)
        {
            var result = DateTime.Compare(song.StartTimeUTC, DateTime.Now.AddSeconds(Constants.streamDelay));
            _log.Info(String.Format("Checking if SR is playing a song. Result: {0}",result));
            if (result < 0)
            {
                //The song has already started
                var offset = DateTime.Now.AddSeconds(Constants.streamDelay) - song.StartTimeUTC;
                CreateScheduleForSongEnd(song);
                replacementTrack = GetReplacementSong(song, (int)offset.TotalMilliseconds);
                ChangeToSpotify();
                CreateScheduleForSongEnd(song);
                return true;
            }
            else if (result == 0) //the song starts now
            {
                replacementTrack = GetReplacementSong(song);
                CreateScheduleForSongEnd(song);
                ChangeToSpotify();
                CreateScheduleForSongEnd(song);
                return true;    
            }
            else
            {
                CreateScheduleForSongStart(song);
                CreateScheduleForSongEnd(song);
            }                
                return false;
        }

        //Checks if the song has ended or not
        public bool CheckIfEnded(Song song = null)
        {
            if (song == null)
                return true;
            return song?.StopTimeUTC < DateTime.Now.AddSeconds(Constants.streamDelay) ? true : false;
        }

        //After a Channel change, this method checks SR and prepares spotify
        public void SetupAfterChannelChange(Song prevSong = null, Song currentSong = null, Song nextSong = null)
        {
            if (!CheckIfEnded(prevSong))
                CheckIfPlaying(prevSong);
            else if (!CheckIfEnded(currentSong))
                CheckIfPlaying(currentSong);
            else if (nextSong != null)
                CheckIfPlaying(nextSong);
        }

        //Check if there's a new song listend on the SR API
        public bool checkIfNewSongListed()
        {
            var tempPlaylist = _sr.GetPlaylist(currentChannel.Id);
            var songTest = Nullable.Equals(_srPlaylist.Song?.StartTimeUTC, tempPlaylist.Song?.StartTimeUTC);
            var nextSongTest = Nullable.Equals(_srPlaylist.NextSong?.StartTimeUTC, tempPlaylist.NextSong?.StartTimeUTC);

            if(!songTest || !nextSongTest)
            {
                _srPlaylist = tempPlaylist;
                OnPlaylistUpdate();
                return true;
            }
            return false;
        }

        private bool checkIfSpotifyIsRunning()
        {
            Process[] pname = Process.GetProcessesByName("spotify");
            if (pname.Length == 0)
            {
                return false;
            }
            else
                return true;
        }
    }
}
