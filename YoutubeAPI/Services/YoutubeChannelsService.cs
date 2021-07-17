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
            var aboutPageUrl = GetAboutYoutubePage(url);

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
            return channel;
        }

        private string GetAboutYoutubePage(string url)
        {
            const string ABOUT = @"/about";
            if (url.Length > ABOUT.Length &&
                url.Substring(url.Length - ABOUT.Length) == ABOUT)
                return url;

            var maybeAboutPage = url + ABOUT;
            if (HtmlHelper.DoesUrlExist(maybeAboutPage))
            {
                return maybeAboutPage;
            }
            return null;
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
    }
}
