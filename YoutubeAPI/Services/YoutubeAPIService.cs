using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Collections.Generic;

namespace YoutubeAPI.Services
{
    class YoutubeAPIService
    {
        private YouTubeService _youtubeService;
        private const string _applicationName = "YoutubeAnalyzer";

        public YoutubeAPIService(string apiKey) : this(apiKey, _applicationName)
        {
        }

        public YoutubeAPIService(string apiKey, string applicationName)
        {
            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = applicationName
            });
        }

        public List<string> GetPlayLists(string channelId)
        {
            var playlistListRequest = _youtubeService.Playlists.List("snippet");
            playlistListRequest.ChannelId = channelId;
            playlistListRequest.MaxResults = 100;
            var playlistListResponse = playlistListRequest.Execute();

            var playlists = new List<string>();
            foreach (var item in playlistListResponse.Items)
            {
                playlists.Add(item.Id);
            }
            return playlists;
        }

        public List<string> GetUrlVideosFromPlaylist(string playlistId, string nextPageToken = null)
        {
            var playListItemsListRequest = _youtubeService.PlaylistItems.List("snippet");
            playListItemsListRequest.PlaylistId = playlistId;
            playListItemsListRequest.MaxResults = 100;
            if (nextPageToken != null)
                playListItemsListRequest.PageToken = nextPageToken;
            var playListItemsListResults = playListItemsListRequest.Execute();

            var result = new List<string>();
            foreach (var item in playListItemsListResults.Items)
            {
                result.Add(item.Id);
            }
            if (!string.IsNullOrEmpty(playListItemsListResults.NextPageToken))
            {
                result.AddRange(this.GetUrlVideosFromPlaylist(playlistId, playListItemsListResults.NextPageToken));
            }
            return result;
        }

        public List<string> GetUrlVideosFromChannel(string channelId, string nextPageToken = null)
        {
            var searchListRequest = _youtubeService.Search.List("snippet");
            searchListRequest.ChannelId = channelId;
            searchListRequest.Type = "video";
            searchListRequest.MaxResults = 1000;
            if (nextPageToken != null)
                searchListRequest.PageToken = nextPageToken;
            var searchListResult = searchListRequest.Execute();
            var result = new List<string>();
            foreach (var item in searchListResult.Items)
            {
                result.Add(item.Id.VideoId);
            }
            if (!string.IsNullOrEmpty(searchListResult.NextPageToken))
            {
                result.AddRange(this.GetUrlVideosFromChannel(channelId, searchListResult.NextPageToken));
            }

            return result;
        }
    }
}
