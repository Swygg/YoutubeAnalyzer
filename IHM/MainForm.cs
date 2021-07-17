using DAL;
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
using YoutubeAPI.Models;


namespace IHM
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_Analyze_Click(object sender, EventArgs e)
        {
            var channelsLinks = GetLinks();
            var channels = new List<YoutubeChannel>();


            var channelsService = new YoutubeChannelsService();
            foreach (var link in channelsLinks)
            {
                var maybeChannel = channelsService.GetChannelFromUrl(link);
                if (maybeChannel != null)
                    channels.Add(maybeChannel);
            }
            ExcelManager.Save("",channels);
        }

        private List<string> GetLinks()
        {
            return urls_tb.Text.Split(Environment.NewLine).ToList();
        }



        #region FOR MANUAL TESTS ONLY
        private void SimulateVideoSearch()
        {
            string videoUrl = "https://www.youtube.com/watch?v=7I_OMwCJN5E";
            var videosService = new YoutubeVideosService();
            var video = videosService.GetVideoFromUrl(videoUrl);
        }

        private void SimulateChannelSearch()
        {
            string channelUrl = "https://www.youtube.com/user/LesTutosdeHuito";
            //string channelUrl = "https://www.youtube.com/c/metallica/about";
            var channelsService = new YoutubeChannelsService();
            var channel = channelsService.GetChannelFromUrl(channelUrl);
        } 
        #endregion
    }
}
