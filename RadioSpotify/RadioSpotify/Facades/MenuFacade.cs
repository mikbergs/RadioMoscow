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

namespace RadioSpotify
{
    public class MenuFacade
    {
        //private static SpotifyWebAPI _spotify;
        private static SpotifyAPIWrapper _spotifyWrapper;
        private static SRApi _sr;
        private WindowsMediaPlayer _srPlayer;
        private TimerWrapper _timerWrapper;
        private FullTrack replacementTrack;
        private Channel currentChannel;
        private Playlist _srPlaylist;
        private Task songStart;
        private Task songEnd;
        //private bool _spotifyPlaying = false;
        public event _WMPOCXEvents_PlayStateChangeEventHandler testEvent = delegate { };
        public TimerWrapper TimerWrapper { get { return _timerWrapper; } }
        public SpotifyAPIWrapper SpotifyWrapper { get  { return _spotifyWrapper; } }
        public WindowsMediaPlayer SRPlayer { get { return _srPlayer; } } 
        public Playlist SRPlaylist { get { return _srPlaylist; } }
        //public List<Channel> ChannelList { get; set; }
        public ChannelList ChannelList { get; set; }
        //public bool SpotifyPlaying { get { return _spotifyPlaying; } }



        public delegate void PlaylistUpdateHandler();
        public event PlaylistUpdateHandler OnPlaylistUpdate;
        public event PlaylistUpdateHandler OnSpotifyUpdate;

        

        public MenuFacade()
        {
            _spotifyWrapper = new SpotifyAPIWrapper();
            _sr = new SRApi();            
            _srPlaylist = new Playlist();
            _timerWrapper = new TimerWrapper();
            InitSRPlayer();
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
            var durationMatch = _spotifyWrapper.SavedTracks.FindAll(x => x.Track.DurationMs == songDurationMs);
            //If there's >= track with matching duration, take out a random one.
            if (durationMatch.Count != 0)
            {
                durationMatch.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                var match = durationMatch.FirstOrDefault();
                return match.Track;
            }

            var orderedTracks = _spotifyWrapper.SavedTracks.OrderBy(x => x.Track.DurationMs);      
              
            var remainingTracks = orderedTracks.SkipWhile(x => x.Track.DurationMs < songDurationMs);
            var removedTracks = orderedTracks.Count() - remainingTracks.ToList().Count() - 1;
            if (removedTracks < 0) removedTracks = 0;
            var closestMatch = getClosestMatch(orderedTracks.ElementAt(removedTracks), remainingTracks?.First(), songDurationMs);

            //_spotifyWrapper.changeTrack(closestMatch.Track.Uri);
            return closestMatch.Track;
           
        }
        public SavedTrack getClosestMatch(SavedTrack shorterSong, SavedTrack longerSong, double srSongDuration)
        {
            var longerThan = longerSong.Track.DurationMs - srSongDuration;
            var shorterThan = srSongDuration - shorterSong.Track.DurationMs;

            return longerThan < shorterThan ? longerSong : shorterSong;
            
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
//            _srPlayer.controls.play();
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
            replacementTrack = GetReplacementSong(song);           
            songStart = _timerWrapper.ScheduleAction(ChangeToSpotify, song.StartTimeUTC,DateTime.Now.AddSeconds(Constants.streamDelay));
        }
        public void CreateScheduleForSongEnd(Song song)
        {
            songEnd = _timerWrapper.ScheduleAction(ChangeToSR, song.StopTimeUTC, DateTime.Now.AddSeconds(Constants.streamDelay));
        }
        
        //Change to SpotifyPlayback.
        private void ChangeToSpotify()
        {
            try
            {
                int retries = 0;
                //ChangeStateSRPlayer();
                _srPlayer.controls.stop();

                //_spotifyWrapper.ChangeTrack("spotify:track:6KOUGH6yvyWDbX1mgulkBM"); // en som inte "finns"
                _spotifyWrapper.ChangeTrack(replacementTrack.Uri);
                while (!_spotifyWrapper.GetPlaybackState() || retries < 5)
                    retries++;
                OnSpotifyUpdate();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }
        private void ChangeToSR()
        {
            var retries = 0;
            _spotifyWrapper.Spotify.PausePlayback();
            while (_spotifyWrapper.GetPlaybackState() || retries < 5)
                retries++;
            OnSpotifyUpdate();
            changeChannelonSRStream(currentChannel);
        }
        public bool CheckIfPlaying(Song song)
        {
            var result = DateTime.Compare(song.StartTimeUTC, DateTime.Now.AddSeconds(Constants.streamDelay));
            if (result < 0)
            {
                //The song has already started
                var offset = DateTime.Now.AddSeconds(Constants.streamDelay) - song.StartTimeUTC;
                CreateScheduleForSongEnd(song);
                replacementTrack = GetReplacementSong(song, (int)offset.TotalMilliseconds);
                ChangeToSpotify();
                return true;
            }
            else if (result == 0) //the song starts now
            {
                replacementTrack = GetReplacementSong(song);
                CreateScheduleForSongEnd(song);
                ChangeToSpotify();
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
    }
}
