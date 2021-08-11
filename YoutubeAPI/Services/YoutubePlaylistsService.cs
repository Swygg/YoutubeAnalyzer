using System;
using System.Collections.Generic;
using YoutubeAPI.Helpers;
using YoutubeAPI.Models;

namespace YoutubeAPI.Services
{
    internal class YoutubePlaylistsService
    {
        private YoutubeAPIService _youtubeAPIService = null;

        public YoutubePlaylistsService(string youtubeAPIKey)
        {
            if (!string.IsNullOrEmpty(youtubeAPIKey))
                _youtubeAPIService = new YoutubeAPIService(youtubeAPIKey);
        }

        public YoutubePlaylist GetYoutubePlaylist(string url)
        {
            YoutubePlaylist playlist = null;
            if (_youtubeAPIService == null)
            {
                string html = string.Empty;
                try
                {
                    html = HtmlHelper.GetHtmlFromUrl(url);
                }
                catch (Exception)
                {
                    throw;
                }

                playlist = ScrapHtml(html);
                playlist.Url = url;
                playlist.Videos = GetVideosByHtml(html);
            }
            else
            {
                var id = GetIdPlaylist(url);
                playlist = new YoutubePlaylist()
                {
                    Name = _youtubeAPIService.GetPlaylistName(id),
                    Url = url,
                    Videos = GetVideosByYoutubeAPI(id)
                };
            }
            return playlist;
        }

        private YoutubePlaylist ScrapHtml(string html)
        {
            var playlist = new YoutubePlaylist
            {
                Name = GetName(html)
            };
            return playlist;
        }

        private string GetName(string html)
        {
            var indexStartHtml = "playlist\":{\"playlist\":{\"title\":\"";
            var indexEndHtml = "\"";
            return HtmlHelper.GetInformations(html, indexStartHtml, indexEndHtml);
        }

        private List<YoutubeVideo> GetVideosByHtml(string html)
        {
            var links = GetVideoLinksFromHtml(html);
            return GetVideos(links);
        }

        private List<YoutubeVideo> GetVideosByYoutubeAPI(string channelId)
        {
            var links = GetVideoLinksFromYoutubeAPI(channelId);
            return GetVideos(links);
        }

        private List<YoutubeVideo> GetVideos(List<string> videosUrl)
        {
            var videos = new List<YoutubeVideo>();
            var youtubeVideosService = new YoutubeVideosService();
            foreach (var link in videosUrl)
            {
                videos.Add(youtubeVideosService.GetVideoFromUrl(link));
            }
            return videos;
        }

        private List<string> GetVideoLinksFromHtml(string html)
        {
            if (html == null)
                return null;

            var links = new List<string>();

            string tempo = null;
            int i = 1;
            do
            {
                var endId = $"\\u0026index={i}\"";
                var startId = "url\":\"";
                tempo = HtmlHelper.GetInformationsFromReverse(html, startId, endId);
                if (tempo != null)
                {
                    tempo = tempo.Replace("\\u0026", "&");
                    tempo = "https://www.youtube.com" + tempo + $"&index={i}";
                    if (!links.Contains(tempo))
                    {
                        links.Add(tempo);
                        i++;
                    }
                    html = html.Substring(html.IndexOf(startId) + startId.Length);
                }
            } while (tempo != null);

            return links;
        }

        private List<string> GetVideoLinksFromYoutubeAPI(string channelId)
        {
            return _youtubeAPIService.GetUrlVideosFromPlaylist(channelId);
        }

        private string GetIdPlaylist(string url)
        {
            var world = "&list=";
            var index = url.IndexOf(world);
            if (index == -1)
                throw new Exception();
            return url.Substring(index + world.Length);
        }
    }
}
