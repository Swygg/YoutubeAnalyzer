using System;
using YoutubeAPI.Helpers;
using YoutubeAPI.Models;

namespace YoutubeAPI.Services
{
    public class YoutubeVideosService
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
            return TranslateYoutubeDateInDateTime(dateYoutubeString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="youtubeDate">Exemple : 5 avr. 2014</param>
        /// <returns></returns>
        private DateTime? TranslateYoutubeDateInDateTime(string youtubeDate)
        {
            try
            {
                var dateParts = youtubeDate.Split(' ');
                var daysInNumber = dateParts[0];
                var monthInString = dateParts[1].Replace(".", string.Empty);
                var yearhInNumber = dateParts[2];

                var monthInNumber = -1;
                switch (monthInString)
                {
                    case "jan":
                        monthInNumber = 1;
                        break;
                    case "fev":
                        monthInNumber = 2;
                        break;
                    case "mar":
                        monthInNumber = 3;
                        break;
                    case "avr":
                        monthInNumber = 4;
                        break;
                    case "mai":
                        monthInNumber = 5;
                        break;
                    case "jui":
                        monthInNumber = 6;
                        break;
                    case "juil":
                        monthInNumber = 7;
                        break;
                    case "aou":
                        monthInNumber = 8;
                        break;
                    case "sep":
                        monthInNumber = 9;
                        break;
                    case "oct":
                        monthInNumber = 10;
                        break;
                    case "nov":
                        monthInNumber = 11;
                        break;
                    case "dec":
                        monthInNumber = 12;
                        break;
                    default:
                        throw new Exception($"Erreur durant le mappage de la date. La valeur \"{monthInString}\" ne peut être mappée.");
                }

                return new DateTime(int.Parse(yearhInNumber), monthInNumber, int.Parse(daysInNumber));
            }
            catch
            {
                return null;
            }
        }
    }
}
