using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using YoutubeAPI.Models;
using YoutubeAPI.Services;
using DAL.Models;


namespace IHM
{
    public partial class MainForm : Form
    {
        #region CONSTRUCTOR
        public MainForm()
        {
            InitializeComponent();
            this.LoadUserPersonnalDatas();
            this.FormClosing += MainForm_FormClosing;
        }
        #endregion



        #region PRIVATES METHODES
        private List<string> GetLinks()
        {
            return tb_urls.Text.Split(Environment.NewLine).ToList();
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static string GetDurationReadableFormat(TimeSpan durationHardToRead)
        {
            return $"{durationHardToRead.Hours.ToString("00")}:{durationHardToRead.Minutes.ToString("00")}:{durationHardToRead.Seconds.ToString("00")}";
        }

        private string GetDateFormat()
        {
            return cb_DateFormat.SelectedItem.ToString();
        }

        private EDurationFormat GetDurationFormat()
        {
            return (EDurationFormat)cb_durationFormat.SelectedIndex;
        }

        private void LoadUserPersonnalDatas()
        {
            tb_folderPath.Text = IHM.Properties.Settings.Default.folderPath;
            cb_DateFormat.SelectedIndex = IHM.Properties.Settings.Default.dateFormatIndex;
            cb_durationFormat.SelectedIndex = IHM.Properties.Settings.Default.durationFormatIndex;
        }

        private void SaveUserPersonnalDatas()
        {
            IHM.Properties.Settings.Default.folderPath = tb_folderPath.Text;
            IHM.Properties.Settings.Default.dateFormatIndex = cb_DateFormat.SelectedIndex;
            IHM.Properties.Settings.Default.durationFormatIndex = cb_durationFormat.SelectedIndex;
            IHM.Properties.Settings.Default.Save();
        }
        #endregion



        #region EVENTS
        private void btn_Analyze_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_urls.Text))
            {
                this.ShowError("You must give at least one url");
                return;
            }
            if (string.IsNullOrEmpty(tb_folderPath.Text))
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
                if (youtubeResponse.Channel != null)
                {
                    youtubeResponse.Channel.Videos = youtubeResponse.Channel.Videos.OrderByDescending(x => x.NbViews).ToList();
                    foreach (var playlist in youtubeResponse.Channel.Playlists)
                    {
                        playlist.Videos = playlist.Videos.OrderByDescending(x => x.NbViews).ToList();
                    }
                }
                if(youtubeResponse.Playlist!=null)
                {
                    youtubeResponse.Playlist.Videos = youtubeResponse.Playlist.Videos.OrderByDescending(x => x.NbViews).ToList();
                }                
            }

            Options options = new Options()
            {
                DateFormat = GetDateFormat(),
                DurationFormat = GetDurationFormat()
            };

            DAL.ExcelManager.Save(tb_folderPath.Text, youtubeResponses, options);
            var endProcess = DateTime.Now;
            var time = endProcess - startProcess;
            var successMessage = $"The datas have been saved in {tb_folderPath.Text}" + Environment.NewLine +
                $"Work done in {GetDurationReadableFormat(time)}";
            MessageBox.Show(successMessage, "Succss", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void changePath_btn_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tb_folderPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserPersonnalDatas();
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
