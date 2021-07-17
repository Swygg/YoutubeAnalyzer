using System;
using YoutubeAPI.Helpers;
using YoutubeAPI.Models;

namespace YoutubeAPI.Services
{
    public class YoutubeChannelsService
    {
        public YoutubeChannel GetChannelFromUrl(string url)
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

            var channel = ScrapHtml(html);
            return channel;
        }

        private YoutubeChannel ScrapHtml(string html)
        {
            var youtubeChannel = new YoutubeChannel()
            {

            };
            return youtubeChannel;
        }

    }
}
