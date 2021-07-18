using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using YoutubeAPI.Models;
using YoutubeAPI.Services;


namespace IHM
{
    public partial class MainForm : Form
    {
        #region CONSTRUCTOR
        public MainForm()
        {
            InitializeComponent();
            this.LoadUserPersonnalData();
            this.FormClosing += MainForm_FormClosing;
        }
        #endregion



        #region PRIVATES METHODES
        private List<string> GetLinks()
        {
            return urls_tb.Text.Split(Environment.NewLine).ToList();
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LoadUserPersonnalData()
        {
            folderPath_tb.Text = IHM.Properties.Settings.Default.folderPath;
        }

        private static string GetDurationReadableFormat(TimeSpan durationHardToRead)
        {
            return $"{durationHardToRead.Hours.ToString("00")}:{durationHardToRead.Minutes.ToString("00")}:{durationHardToRead.Seconds.ToString("00")}";
        }
        #endregion



        #region EVENTS
        private void btn_Analyze_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(urls_tb.Text))
            {
                this.ShowError("You must give at least one url");
                return;
            }
            if (string.IsNullOrEmpty(folderPath_tb.Text))
            {
                this.ShowError("You must give a folder path");
                return;
            }




            var startProcess = DateTime.Now;
            var youtubeResponses = new List<YoutubeResponse>();
            var links = GetLinks();
            var youtubeSearchService = new YoutubeSearchService();




            foreach (var link in links)
            {
                var response = youtubeSearchService.GetAnswerFromLink(link);
                if (response != null)
                {
                    youtubeResponses.Add(response);
                }
            }

            //ORDER VIDEO BY NB VIEWS
            foreach (var youtubeResponse in youtubeResponses)
            {
                if (youtubeResponse.Playlist != null)
                {
                    youtubeResponse.Channel.Videos = youtubeResponse.Channel.Videos.OrderByDescending(x => x.NbViews).ToList();
                }
            }



            DAL.ExcelManager.Save(folderPath_tb.Text, youtubeResponses);
            var endProcess = DateTime.Now;
            var time = endProcess - startProcess;
            var successMessage = $"The datas have been saved in {folderPath_tb.Text}" + Environment.NewLine +
                $"Work done in {GetDurationReadableFormat(time)}";
            MessageBox.Show(successMessage, "Succss", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void changePath_btn_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folderPath_tb.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IHM.Properties.Settings.Default.folderPath = folderPath_tb.Text;
            IHM.Properties.Settings.Default.Save();
        }
        #endregion



        #region FOR MANUAL TESTS ONLY
        private void SimulateVideoSearch()
        {
            string videoUrl = "https://www.youtube.com/watch?v=7I_OMwCJN5E";
            var videosService = new YoutubeVideosService();
            var maybeVideo = videosService.GetVideoFromUrl(videoUrl);
        }

        private void SimulatePlaylistSearch()
        {
            string url = "https://www.youtube.com/watch?v=RcLxoPz1dDY&list=PLmntgUDCubzjE-DaiCrVXv9LfsJU61aJ7";
            var playlistService = new YoutubePlaylistsService();
            var maybePlaylist = playlistService.GetYoutubePlaylist(url);
        }

        private void SimulateChannelSearch()
        {
            string channelUrl = "https://www.youtube.com/user/LesTutosdeHuito";
            //string channelUrl = "https://www.youtube.com/c/metallica/about";
            var channelsService = new YoutubeChannelsService();
            var maybeChannel = channelsService.GetChannelFromUrl(channelUrl);
        }
        #endregion

       
    }
}
