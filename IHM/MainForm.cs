using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeAPI.Services;


namespace IHM
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SimulateChannelSearch();
        }

        private void SimulateVideoSearch()
        {
            string videoUrl = "https://www.youtube.com/watch?v=7I_OMwCJN5E";
            var videosService = new YoutubeVideosService();
            var video = videosService.GetVideoFromUrl(videoUrl);
        }

        private void SimulateChannelSearch()
        {
            string channelUrl = "https://www.youtube.com/user/LesTutosdeHuito";
            var channelsService = new YoutubeChannelsService();
            var channel = channelsService.GetChannelFromUrl(channelUrl);
        }

    }
}
