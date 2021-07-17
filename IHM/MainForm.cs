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

            var channelsLinks = GetLinks();
            var channels = new List<YoutubeChannel>();


            var channelsService = new YoutubeChannelsService();
            foreach (var link in channelsLinks)
            {
                var maybeChannel = channelsService.GetChannelFromUrl(link);
                if (maybeChannel != null)
                    channels.Add(maybeChannel);
            }
            ExcelManager.Save(folderPath_tb.Text, channels);
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
