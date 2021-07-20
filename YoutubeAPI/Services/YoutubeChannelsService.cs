using System;
using System.Collections.Generic;
using YoutubeAPI.Helpers;
using YoutubeAPI.Models;

namespace YoutubeAPI.Services
{
    public class YoutubeChannelsService
    {
        public YoutubeChannel GetChannelFromUrl(string url)
        {
            var aboutPageUrl = GetYoutubeAboutAccountUrl(url);

            string html = string.Empty;
            try
            {
                html = HtmlHelper.GetHtmlFromUrl(aboutPageUrl);
            }
            catch (Exception)
            {
                throw;
            }
            var channel = ScrapHtml(html);
            channel.Videos = GetVideos(url);
            channel.Playlists = GetPlayList(url);
            return channel;
        }

        private string GetYoutubeAboutAccountUrl(string url)
        {
            const string ABOUT = @"/about";
            if (url.Length > ABOUT.Length &&
                url.Substring(url.Length - ABOUT.Length) == ABOUT)
                return url;

            url = SanityzeChannelUrl(url);
            var maybeAboutPage = url + ABOUT;
            if (HtmlHelper.DoesUrlExist(maybeAboutPage))
            {
                return maybeAboutPage;
            }
            return null;
        }

        private string SanityzeChannelUrl(string url)
        {
            url = CutUrlEnd(url, "/featured");
            url = CutUrlEnd(url, "/videos");
            url = CutUrlEnd(url, "/playlists");
            url = CutUrlEnd(url, "/community");
            url = CutUrlEnd(url, "/channels");
            return url;
        }

        private string CutUrlEnd(string url, string stringToRemove)
        {
            if (url.Length > stringToRemove.Length &&
                 url.Substring(url.Length - stringToRemove.Length) == stringToRemove)
            {
                url = url.Substring(0, url.Length - stringToRemove.Length);
            }
            return url;
        }

        private YoutubeChannel ScrapHtml(string html)
        {
            var youtubeChannel = new YoutubeChannel()
            {
                Name = GetName(html),
                NbViews = GetNbViews(html),
                NbSubscribers = GetNbSubscribers(html),
                Description = GetDescription(html),
                FacebookLink = GetFacebookLink(html),
                TwitterLink = GetTwitterLink(html),
                SubscriptionDate = GetSubscriptionDate(html),
                TrueUrl = GetTrueUrl(html),
                Playlists = new List<YoutubePlaylist>(),
                Videos = new List<YoutubeVideo>()

            };
            return youtubeChannel;
        }

        private string GetName(string html)
        {
            var indexStartHtml = "bypassBusinessEmailCaptcha\":false,\"title\":{\"simpleText\":\"";
            var indexEndHtml = "\"}";
            return HtmlHelper.GetInformations(html, indexStartHtml, indexEndHtml);
        }

        private string GetDescription(string html)
        {
            var indexStartHtml = "{\"channelAboutFullMetadataRenderer\":{\"description\":{\"simpleText\":\"";
            var indexEndHtml = "\"}";
            return HtmlHelper.GetInformations(html, indexStartHtml, indexEndHtml);
        }

        private string GetFacebookLink(string html)
        {
            var indexStartHtml = "https%3A%2F%2Fwww.facebook.com%2Fpages%2F";
            var indexEndHtml = "\"";
            var maybePartialLink = HtmlHelper.GetInformations(html, indexStartHtml, indexEndHtml);
            if (string.IsNullOrEmpty(maybePartialLink))
                return null;
            return "https://www.facebook.com/pages/" + maybePartialLink;
        }

        private string GetTwitterLink(string html)
        {
            var indexStartHtml = "https%3A%2F%2Ftwitter.com%2F";
            var indexEndHtml = "\"";
            var maybePartialLink = HtmlHelper.GetInformations(html, indexStartHtml, indexEndHtml);
            if (string.IsNullOrEmpty(maybePartialLink))
                return null;
            return "twitter.com/" + maybePartialLink;
        }

        private int GetNbViews(string html)
        {
            var indexStartHtml = "viewCountText\":{\"simpleText\":\"";
            var indexEndHtml = "vues";
            return HtmlHelper.GetIntFromInformation(html, indexStartHtml, indexEndHtml);
        }

        private int GetNbSubscribers(string html)
        {
            var indexStartHtml = "simpleText\":\"";
            var indexEndHtml = "abonnés\"},\"tvBanner\":{";
            var tempo = HtmlHelper.GetInformationsFromReverse(html, indexStartHtml, indexEndHtml);

            bool IsThousands = tempo.Contains('k');
            bool IsMillions = tempo.Contains('M');

            tempo = HtmlHelper.KeepOnlyNumbers(tempo, true);

            if (double.TryParse(tempo, out double result))
            {
                if (IsThousands)
                    result *= 1000;
                else if (IsMillions)
                    result *= 1000000;
                return (int)result;
            }
            else
                return -1;
        }

        private DateTime? GetSubscriptionDate(string html)
        {
            var indexStartHtml = "Actif depuis le \"},{\"text\":\"";
            var indexEndHtml = "\"}";
            var stringDate = HtmlHelper.GetInformations(html, indexStartHtml, indexEndHtml);
            return DateHelper.TranslateYoutubeDateInDateTime(stringDate);
        }

        private string GetTrueUrl(string html)
        {
            var indexStartHtml = "canonicalBaseUrl\":\"";
            var indexEndHtml = "\"}";
            var urlPart = HtmlHelper.GetInformations(html, indexStartHtml, indexEndHtml);
            return urlPart == null ? null : $"https://www.youtube.com/user/{urlPart}";
        }

        private List<YoutubeVideo> GetVideos(string url)
        {
            var links = GetVideoLinks(url);
            var videos = new List<YoutubeVideo>();
            var youtubeVideosService = new YoutubeVideosService();
            foreach (var link in links)
            {
                videos.Add(youtubeVideosService.GetVideoFromUrl(link));
            }
            return videos;
        }

        private List<string> GetVideoLinks(string url)
        {
            url = GetYoutubeVideoAccountUrl(url);
            if (url == null)
                return null;
            string html = string.Empty;
            try
            {
                html = HtmlHelper.GetHtmlFromUrl(url);
            }
            catch (Exception)
            {
                throw;
            }

            var links = new List<string>();

            string tempo = null;
            do
            {
                var startId = "gridVideoRenderer\":{\"videoId\":\"";
                var endId = "\"";

                tempo = HtmlHelper.GetInformations(html, startId, endId);
                if (tempo != null)
                {
                    links.Add("https://www.youtube.com/watch?v=" + tempo);
                    html = html.Substring(html.IndexOf(startId) + startId.Length);
                }
            } while (tempo != null);

            return links;
        }

        private string GetYoutubeVideoAccountUrl(string url)
        {
            const string VIDEO = @"/videos";
            if (url.Length > VIDEO.Length &&
                url.Substring(url.Length - VIDEO.Length) == VIDEO)
                return url;

            var maybePage = url + VIDEO;
            if (HtmlHelper.DoesUrlExist(maybePage))
            {
                return maybePage;
            }
            return null;
        }

        private List<YoutubePlaylist> GetPlayList(string url)
        {
            var links = GetPlayListLinks(url);
            var playlists = new List<YoutubePlaylist>();
            var youtubePlaylistService = new YoutubePlaylistsService();
            foreach (var link in links)
            {
                playlists.Add(youtubePlaylistService.GetYoutubePlaylist(link));
            }
            return playlists;
        }

        private List<string> GetPlayListLinks(string url)
        {
            url = GetYoutubePlayListAccountUrl(url);
            if (url == null)
                return null;
            string html = string.Empty;
            try
            {
                html = HtmlHelper.GetHtmlFromUrl(url);
            }
            catch (Exception)
            {
                throw;
            }

            var links = new List<string>();
            var middleIndex = -1;
            do
            {
                var middle = "\\u0026list=";
                var start = "url\":\"";
                var stop = "\"";

                middleIndex = html.IndexOf(middle);

                if (middleIndex > -1)
                {
                    var startIndex = html.Substring(0, middleIndex).LastIndexOf(start) + start.Length;
                    var stopIndex = html.Substring(middleIndex).IndexOf(stop);
                    var partUrl = html.Substring(startIndex, (middleIndex - startIndex) + stopIndex).Replace("\\u0026","&");
                    var newUrl = $"https://www.youtube.com{partUrl}";
                    if (!links.Contains(newUrl))
                    {
                        links.Add(newUrl);
                    }
                    html = html.Substring(startIndex + partUrl.Length);
                }
            } while (middleIndex>-1);

            return links;
        }

        private string GetYoutubePlayListAccountUrl(string url)
        {
            const string PLAYLIST = @"/playlists";
            if (url.Length > PLAYLIST.Length &&
                url.Substring(url.Length - PLAYLIST.Length) == PLAYLIST)
                return url;

            var maybePage = url + PLAYLIST;
            if (HtmlHelper.DoesUrlExist(maybePage))
            {
                return maybePage;
            }
            return null;
        }
    }
}
