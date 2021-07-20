using System;
using System.Collections.Generic;
using YoutubeAPI.Helpers;
using YoutubeAPI.Models;

namespace YoutubeAPI.Services
{
    public class YoutubePlaylistsService
    {
        public YoutubePlaylist GetYoutubePlaylist(string url)
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

            var playlist = ScrapHtml(html);
            playlist.Url = url;
            playlist.Videos = GetVideos(html);
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

        private List<YoutubeVideo> GetVideos(string html)
        {
            var links = GetVideoLinks(html);
            var videos = new List<YoutubeVideo>();
            var youtubeVideosService = new YoutubeVideosService();
            foreach (var link in links)
            {
                videos.Add(youtubeVideosService.GetVideoFromUrl(link));
            }
            return videos;
        }

        private List<string> GetVideoLinks(string html)
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
    }
}
