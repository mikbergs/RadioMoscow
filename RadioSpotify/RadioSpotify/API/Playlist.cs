using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioSpotify.API
{
    public class Song 
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Artist { get; set; }
        public string Composer { get; set; }
        public string AlbumName { get; set; }
        public string RecordLabel { get; set; }
        public DateTime StartTimeUTC { get; set; }
        public DateTime StopTimeUTC { get; set; }
    }

    public class Playlist
    {
        public Song PreviousSong { get; set; }
        public Song Song { get; set; }
        public Song NextSong { get; set; }
        public Channel Channel { get; set; }        
    }
}