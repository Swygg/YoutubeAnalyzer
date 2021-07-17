using System;
using System.Net;
using System.Text;

namespace YoutubeAPI.Helpers
{
    public static class HtmlHelper
    {
        public static bool DoesUrlExist(string url)
        {
            using var client = new MyClient();
            client.HeadOnly = true;
            try
            {
                client.DownloadString(url);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetHtmlFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new Exception("Url null or empty");

            using WebClient webclient = new();
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
            if (tempo == null)
                return -1;

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

        public static string GetInformationsFromReverse(string html, string htlmStart, string htmlEnd)
        {
            var indexEnd = html.IndexOf(htmlEnd);
            if (indexEnd == -1)
                return null;
            html = html.Substring(0, indexEnd);

            var lastStartIndex = html.LastIndexOf(htlmStart);
            if (lastStartIndex == -1)
                return null;

            var result = html.Substring(lastStartIndex + htlmStart.Length);
            result.Replace(@"\n", Environment.NewLine);
            return result;
        }

        public static int GetIntFromInformationFromReverse(string html, string htlmStart, string htmlEnd)
        {
            var tempo = GetInformationsFromReverse(html, htlmStart, htmlEnd);
            var stringWithOnlyNumber = KeepOnlyNumbers(tempo);

            if (int.TryParse(stringWithOnlyNumber, out int realNbView))
                return realNbView;
            else
                return -1;
        }

        public static string KeepOnlyNumbers(string str, bool keepComa = false)
        {
            var stringWithOnlyNumber = new StringBuilder();
            foreach (var caracter in str)
            {
                if (char.IsNumber(caracter) || (keepComa && caracter ==','))
                    stringWithOnlyNumber.Append(caracter);
            }

            return stringWithOnlyNumber.ToString();
        }

        private class MyClient : WebClient
        {
            public bool HeadOnly { get; set; }
            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest req = base.GetWebRequest(address);
                if (HeadOnly && req.Method == "GET")
                {
                    req.Method = "HEAD";
                }
                return req;
            }
        }
    }
}