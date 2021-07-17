using System;
using System.Text;
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
                Duration = GetDuration(html)
            };
            return video;
        }

        private string GetName(string html)
        {
            var indexStartHtml = "videoPrimaryInfoRenderer\":{\"title\":{\"runs\":[{\"text\":\"";
            var indexEndHtml = "\"}";
            return GetInformations(html, indexStartHtml, indexEndHtml);
        }

        private string GetDescription(string html)
        {
            var indexStartHtml = "description\":{\"runs\":[{\"text\":\"";
            var indexEndHtml = "\"}";
            return GetInformations(html, indexStartHtml, indexEndHtml);
        }

        private int GetNbViews(string html)
        {
            var indexStartHtml = "videoViewCountRenderer\":{\"viewCount\":{\"simpleText\":\"";
            var indexEndHtml = "\"}";
            return GetIntFromInformation(html, indexStartHtml, indexEndHtml);
        }

        private int GetNbDislike(string html)
        {
            var indexStartHtml = "{\"iconType\":\"DISLIKE\"},\"defaultText\":{\"accessibility\":{\"accessibilityData\":{\"label\":\"";
            var indexEndHtml = "clic";
            return GetIntFromInformation(html, indexStartHtml, indexEndHtml);
        }

        private int GetNbLike(string html)
        {
            var indexStartHtml = "{\"iconType\":\"LIKE\"},\"defaultText\":{\"accessibility\":{\"accessibilityData\":{\"label\":\"";
            var indexEndHtml = "clic";
            return GetIntFromInformation(html, indexStartHtml, indexEndHtml);
        }

        private TimeSpan? GetDuration(string html)
        {
            var indexStartHtml = "audioQuality\":\"AUDIO_QUALITY_LOW\",\"approxDurationMs\":\"";
            var indexEndHtml = "\"";
            var nbSeconds = GetIntFromInformation(html, indexStartHtml, indexEndHtml) / 1000;
            return new TimeSpan(0, 0, nbSeconds);
        }

        private DateTime? GetCreationDateTime(string html)
        {
            var indexStartHtml = "\"dateText\":{\"simpleText\":\"";
            var indexEndHtml = "\"}";
            var dateYoutubeString = GetInformations(html, indexStartHtml, indexEndHtml);
            return TranslateYoutubeDateInDateTime(dateYoutubeString);
        }

        private string GetInformations(string html, string htlmStart, string htmlEnd)
        {
            var indexStart = html.IndexOf(htlmStart);
            if (indexStart == -1)
                return null;
            indexStart += htlmStart.Length;
            html = html.Substring(indexStart);
            var result = html.Substring(0, html.IndexOf(htmlEnd));
            result.Replace(@"\n", Environment.NewLine);
            return result;
        }

        private int GetIntFromInformation(string html, string htlmStart, string htmlEnd)
        {
            var tempo = GetInformations(html, htlmStart, htmlEnd);

            var stringWithOnlyNumber = new StringBuilder();
            foreach (var caracter in tempo)
            {
                if (char.IsNumber(caracter))
                    stringWithOnlyNumber.Append(caracter);
            }

            if (int.TryParse(stringWithOnlyNumber.ToString(), out int realNbView))
                return realNbView;
            else
                return -1;
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
