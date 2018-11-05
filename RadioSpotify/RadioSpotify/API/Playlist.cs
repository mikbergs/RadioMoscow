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

    //public class Channel
    //{
    //    public string Id { get; set; }
    //    public string Name { get; set; }
    //}

    public class Playlist //: INotifyPropertyChanged
    {
        //private Song _song;
        //private Song _previousSong;
        public Song PreviousSong { get; set; }
        public Song Song { get; set; }
        public Song NextSong { get; set; }
        public Channel Channel { get; set; }

           
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}