using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioSpotify.API
{
    public class Liveaudio
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Statkey { get; set; }
    }
    public class Channel
    {
        public string Image { get; set; }
        public string Imagetemplate { get; set; }
        public string Color { get; set; }
        public string Tagline { get; set; }
        public string Siteurl { get; set; }
        public Liveaudio Liveaudio { get; set; }
        public string Scheduleurl { get; set; }
        public string Channeltype { get; set; }
        public string xmltvid { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ChannelList
    {
        public string copyright { get; set; }
        public List<Channel> channels { get; set; }
    }
}


