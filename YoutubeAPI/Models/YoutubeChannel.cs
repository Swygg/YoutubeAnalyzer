using System;
using System.Collections.Generic;

namespace YoutubeAPI.Models
{
    public class YoutubeChannel
    {
        public string Name { get; set; }
        public DateTime? SubscriptionDate { get; set; }
        public int NbSubscribers { get; set; }
        public string Description { get; set; }
        public int NbViews { get; set; }
        public List<YoutubePlaylist> Playlists { get; set; }
        public List<YoutubeVideo> Videos { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string TrueUrl { get; set; }
    }
}
