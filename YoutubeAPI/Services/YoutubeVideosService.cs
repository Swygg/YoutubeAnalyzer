using System;
using YoutubeAPI.Helpers;
using YoutubeAPI.Models;

namespace YoutubeAPI.Services
{
    internal class YoutubeVideosService
    {
        public YoutubeVideo GetVideoFromUrl(string url)
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

            var video = ScrapHtml(html);
            video.Url = url;
            return video;
        }

        private YoutubeVideo ScrapHtml(string html)
        {
            var video = new YoutubeVideo
            {
                Name = GetName(html),
                NbViews = GetNbViews(html),
                Description = GetDescription(html),
                CreationDate = GetCreationDateTime(html),
                NbPositiveFeedbacks = GetNbLike(html),
                NbNegativeFeedbacks = GetNbDislike(html),
                Duration = GetDuration(html),
                ChannelUrl = GetChannelUrl(html)
            };
            return video;
        }

        private string GetName(string html)
        {
            var indexStartHtml = "videoPrimaryInfoRenderer\":{\"title\":{\"runs\":[{\"text\":\"";
            var indexEndHtml = "\"}";
            return HtmlHelper.GetInformations(html, indexStartHtml, indexEndHtml);
        }

        private string GetDescription(string html)
        {
            var indexStartHtml = "description\":{\"runs\":[{\"text\":\"";
            var indexEndHtml = "\"}";
            return HtmlHelper.GetInformations(html, indexStartHtml, indexEndHtml);
        }

        private string GetChannelUrl(string html)
        {
            var indexStartHtml = "Person\"><link itemprop=\"url\" href=\"";
            var indexEndHtml = "\"";
            var maybeChannelUrl = HtmlHelper.GetInformations(html, indexStartHtml, indexEndHtml);
            if (maybeChannelUrl == null)
                return null;
            if (maybeChannelUrl.IndexOf("user") > -1)
                return maybeChannelUrl;
            return null;
        }

        private int GetNbViews(string html)
        {
            var indexStartHtml = "videoViewCountRenderer\":{\"viewCount\":{\"simpleText\":\"";
            var indexEndHtml = "\"}";
            return HtmlHelper.GetIntFromInformation(html, indexStartHtml, indexEndHtml);
        }

        private int GetNbDislike(string html)
        {
            var indexStartHtml = "{\"iconType\":\"DISLIKE\"},\"defaultText\":{\"accessibility\":{\"accessibilityData\":{\"label\":\"";
            var indexEndHtml = "clic";
            return HtmlHelper.GetIntFromInformation(html, indexStartHtml, indexEndHtml);
        }

        private int GetNbLike(string html)
        {
            var indexStartHtml = "{\"iconType\":\"LIKE\"},\"defaultText\":{\"accessibility\":{\"accessibilityData\":{\"label\":\"";
            var indexEndHtml = "clic";
            return HtmlHelper.GetIntFromInformation(html, indexStartHtml, indexEndHtml);
        }

        private TimeSpan? GetDuration(string html)
        {
            var indexStartHtml = "audioQuality\":\"AUDIO_QUALITY_LOW\",\"approxDurationMs\":\"";
            var indexEndHtml = "\"";
            var nbSeconds = HtmlHelper.GetIntFromInformation(html, indexStartHtml, indexEndHtml) / 1000;
            return new TimeSpan(0, 0, nbSeconds);
        }

        private DateTime? GetCreationDateTime(string html)
        {
            var indexStartHtml = "\"dateText\":{\"simpleText\":\"";
            var indexEndHtml = "\"}";
            var dateYoutubeString = HtmlHelper.GetInformations(html, indexStartHtml, indexEndHtml);
            return DateHelper.TranslateYoutubeDateInDateTime(dateYoutubeString);
        }
    }
}
