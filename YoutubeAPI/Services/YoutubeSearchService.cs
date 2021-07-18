using YoutubeAPI.Models;

namespace YoutubeAPI.Services
{
    public class YoutubeSearchService
    {
        private readonly YoutubeChannelsService YoutubeChannelsService;
        private readonly YoutubeVideosService YoutubeVideosService;
        private readonly YoutubePlaylistsService YoutubePlaylistsService;
        public YoutubeSearchService()
        {
            YoutubeChannelsService = new YoutubeChannelsService();
            YoutubeVideosService = new YoutubeVideosService();
            YoutubePlaylistsService = new YoutubePlaylistsService();
        }

        public YoutubeResponse GetAnswerFromLink(string url)
        {
            if (IsURLAYoutubeChannel(url))
            {
                return new YoutubeResponse()
                {
                    Channel = YoutubeChannelsService.GetChannelFromUrl(url)
                };
            }
            else if (IsURLAYoutubePlaylist(url))
            {
                return new YoutubeResponse()
                {
                    Playlist = YoutubePlaylistsService.GetYoutubePlaylist(url)
                };
            }
            else if (IsURLAYoutubeVideo(url))
            {
                return new YoutubeResponse()
                {
                    Video = YoutubeVideosService.GetVideoFromUrl(url)
                };
            }
            else
                return null;
        }

        private bool IsURLAYoutubeChannel(string url)
        {
            return
                !string.IsNullOrEmpty(url) &&
                url.Contains("youtube") &&
                !url.Contains("watch?") &&
                !url.Contains("&list");
        }

        private bool IsURLAYoutubePlaylist(string url)
        {
            return
               !string.IsNullOrEmpty(url) &&
               url.Contains("youtube") &&
               url.Contains("watch?") &&
               url.Contains("&list");
        }

        private bool IsURLAYoutubeVideo(string url)
        {
            return
               !string.IsNullOrEmpty(url) &&
               url.Contains("youtube") &&
               url.Contains("watch?") &&
               !url.Contains("&list");
        }
    }
}
