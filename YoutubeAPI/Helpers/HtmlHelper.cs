using System;
using System.Net;

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
    }
}