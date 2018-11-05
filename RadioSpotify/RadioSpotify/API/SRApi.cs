using RadioSpotify.API;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RadioSpotify
{
    public class SRApi
    {

        const string BaseUrl = "http://api.sr.se/api/v2";

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = new System.Uri(BaseUrl);
            //request.AddParameter(ParameterType.UrlSegment);
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var srException = new ApplicationException(message, response.ErrorException);
                throw srException;
            }
            return response.Data;
        }

        public Playlist GetStations()
        {
            var request = new RestRequest();
            request.Resource = "channels";
            request.RootElement = "sr";
            return Execute<Playlist>(request);
        }
        public Playlist GetPlaylist(int channelId)
        {
            var request = new RestRequest();
            request.Resource = "playlists/rightnow?";
            request.AddParameter("channelId", channelId);
            request.RootElement = "sr";
            return Execute<Playlist>(request);
        }

        //To be fixed with the real API call to get ALL channels
        public ChannelList GetChannels()
        {
            var request = new RestRequest();
            request.Resource = "channels";
            request.RootElement = "sr";
            request.AddParameter("pagination", false);
            return Execute<ChannelList>(request);
        }

    }

}

