using System;
using System.Net;
using System.Text;

namespace YoutubeAPI.Helpers
{
    public static class HtmlHelper
    {
        public static string GetHtmlFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new Exception("Url null or empty");

            using WebClient webclient = new WebClient();
            return webclient.DownloadString(url);
        }

        public static string GetInformations(string html, string htlmStart, string htmlEnd)
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

        public static int GetIntFromInformation(string html, string htlmStart, string htmlEnd)
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
    }
}