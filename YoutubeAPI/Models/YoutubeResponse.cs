namespace YoutubeAPI.Models
{
    public class YoutubeResponse
    {
        public YoutubeVideo Video { get; set; }
        public YoutubePlaylist Playlist { get; set; }
        public YoutubeChannel Channel { get; set; }
    }
}
