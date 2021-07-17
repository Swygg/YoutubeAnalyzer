using System;

namespace YoutubeAPI.Models
{
    public class YoutubeVideo
    {
        public string Name { get; set; }
        public TimeSpan? Duration { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int NbViews { get; set; }
        public int NbPositiveFeedbacks { get; set; }
        public int NbNegativeFeedbacks { get; set; }
        public string ChannelUrl { get; set; }

        public YoutubeVideo()
        {
            this.Name = null;
            this.Duration = null;
            this.NbViews = -1;
            this.CreationDate = null;
            this.Url = null;
            this.Description = null;
            this.NbPositiveFeedbacks = -1;
            this.NbNegativeFeedbacks = -1;
            this.ChannelUrl = null;
        }
    }
}
