using System.Collections.Generic;

namespace YoutubeAPI.Models
{
    public class YoutubePlaylist
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public List<YoutubeVideo> Videos {get;set;}
    }
}
