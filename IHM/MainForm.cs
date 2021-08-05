using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using YoutubeAPI.Models;
using YoutubeAPI.Services;
using DAL.Models;
using System.Threading;
using System.Globalization;

namespace IHM
{
    public partial class MainForm : Form
    {
        private string _processState = Properties.Strings.ProcessState;


        #region CONSTRUCTOR
        public MainForm()
        {
            InitializeComponent();
            this.Localization();
            this.LoadUserPersonnalDatas();
            this.FormClosing += MainForm_FormClosing;
        }
        #endregion



        #region Static methods
        private static string GetDurationReadableFormat(TimeSpan durationHardToRead)
        {
            return $"{durationHardToRead.Hours.ToString("00")}:{durationHardToRead.Minutes.ToString("00")}:{durationHardToRead.Seconds.ToString("00")}";
        }
        #endregion



        #region PRIVATES METHODES
        private void Analyze()
        {
            if (string.IsNullOrEmpty(tb_urls.Text))
            {
                this.ShowError(Properties.Strings.Err_OneUrlMinimum);
                return;
            }
            if (string.IsNullOrEmpty(tb_folderPath.Text))
            {
                this.ShowError(Properties.Strings.Err_UserMustGiveOneFolderPath);
                return;
            }


            InformUserProcessIsStarting();

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
                    youtubeResponse.Channel.Videos = SortVideos(youtubeResponse.Channel.Videos);
                    foreach (var playlist in youtubeResponse.Channel.Playlists)
                    {
                        playlist.Videos = SortVideos(playlist.Videos);
                    }
                }
                if (youtubeResponse.Playlist != null)
                {
                    youtubeResponse.Playlist.Videos = SortVideos(youtubeResponse.Playlist.Videos);
                }
            }

            Options options = GetOptions();

            DAL.ExcelManager.Save(tb_folderPath.Text, youtubeResponses, options);

            InformUserProcessIsFinished();
            var endProcess = DateTime.Now;
            var time = endProcess - startProcess;
            var successMessage = string.Format(Properties.Strings.SuccessMessage,
                tb_folderPath.Text + Environment.NewLine,
                GetDurationReadableFormat(time));
            MessageBox.Show(successMessage, Properties.Strings.SuccessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private List<YoutubeVideo> SortVideos(List<YoutubeVideo> videosUnsorted)
        {
            var sort = GetVideoSorting();
            switch (sort)
            {
                case EVideoSorting.NotSorted:
                default:
                    return videosUnsorted;
                case EVideoSorting.DurationAsc:
                    return videosUnsorted.OrderBy(v => v.Duration).ToList();
                case EVideoSorting.DurationDesc:
                    return videosUnsorted.OrderByDescending(v => v.Duration).ToList();
                case EVideoSorting.DateCreationAsc:
                    return videosUnsorted.OrderBy(v => v.CreationDate).ToList();
                case EVideoSorting.DateCreationDesc:
                    return videosUnsorted.OrderByDescending(v => v.CreationDate).ToList();
                case EVideoSorting.NumberViewsAsc:
                    return videosUnsorted.OrderBy(v => v.NbViews).ToList();
                case EVideoSorting.NumberViewsDesc:
                    return videosUnsorted.OrderByDescending(v => v.NbViews).ToList();
                case EVideoSorting.NumberPositivesFeedbackAsc:
                    return videosUnsorted.OrderBy(v => v.NbPositiveFeedbacks).ToList();
                case EVideoSorting.NumberPositivesFeedbackDesc:
                    return videosUnsorted.OrderByDescending(v => v.NbPositiveFeedbacks).ToList();
                case EVideoSorting.NumberNegativesFeedbackAsc:
                    return videosUnsorted.OrderBy(v => v.NbNegativeFeedbacks).ToList();
                case EVideoSorting.NumberNegativesFeedbackDesc:
                    return videosUnsorted.OrderByDescending(v => v.NbNegativeFeedbacks).ToList();
                case EVideoSorting.NameAsc:
                    return videosUnsorted.OrderBy(v => v.Name).ToList();
                case EVideoSorting.NameDesc:
                    return videosUnsorted.OrderByDescending(v => v.Name).ToList();
            }
        }
        #endregion



        #region User choice get methods
        private List<string> GetLinks()
        {
            return tb_urls.Text.Split(Environment.NewLine).ToList();
        }

        private Options GetOptions()
        {
            return new Options()
            {
                DateFormat = GetDateFormat(),
                DurationFormat = GetDurationFormat(),
                ThousandSeparator = tb_thousandSeparator.Text,
                MillionsSeparator = tb_millionsSeparator.Text,
                BilliardSeparator = tb_billiardSeparator.Text,
            };
        }

        private string GetDateFormat()
        {
            return cb_DateFormat.SelectedItem.ToString();
        }

        private EDurationFormat GetDurationFormat()
        {
            return (EDurationFormat)cb_durationFormat.SelectedIndex;
        }

        private EVideoSorting GetVideoSorting()
        {
            return (EVideoSorting)cb_SortVideosType.SelectedIndex;
        }
        #endregion



        #region User experience functions
        private void InformUserProcessIsStarting()
        {
            lbl_ProcessState.Text = $"{_processState} {Properties.Strings.ProcessStateWorking}";
            Cursor.Current = Cursors.WaitCursor;
        }

        private void InformUserProcessIsFinished()
        {
            lbl_ProcessState.Text = $"{_processState} {Properties.Strings.ProcessStateFinished}";
            Cursor.Current = Cursors.Default;
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, Properties.Strings.Err_AnErrorOccured, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LoadUserPersonnalDatas()
        {
            tb_folderPath.Text = IHM.Properties.Settings.Default.folderPath;
            cb_DateFormat.SelectedIndex = IHM.Properties.Settings.Default.dateFormatIndex;
            cb_durationFormat.SelectedIndex = IHM.Properties.Settings.Default.durationFormatIndex;
            tb_thousandSeparator.Text = IHM.Properties.Settings.Default.thousandSeparator;
            tb_millionsSeparator.Text = IHM.Properties.Settings.Default.millionsSepartor;
            tb_billiardSeparator.Text = IHM.Properties.Settings.Default.billiarSeparator;
            cb_SortVideosType.SelectedIndex = IHM.Properties.Settings.Default.SortTypeIndex;
        }

        private void SaveUserPersonnalDatas()
        {
            IHM.Properties.Settings.Default.folderPath = tb_folderPath.Text;
            IHM.Properties.Settings.Default.dateFormatIndex = cb_DateFormat.SelectedIndex;
            IHM.Properties.Settings.Default.durationFormatIndex = cb_durationFormat.SelectedIndex;
            IHM.Properties.Settings.Default.thousandSeparator = tb_thousandSeparator.Text;
            IHM.Properties.Settings.Default.millionsSepartor = tb_millionsSeparator.Text;
            IHM.Properties.Settings.Default.billiarSeparator = tb_billiardSeparator.Text;
            IHM.Properties.Settings.Default.SortTypeIndex = cb_SortVideosType.SelectedIndex;
            IHM.Properties.Settings.Default.Save();
        }

        private void Localization()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr-FR");
            this.lbl_MainYoutubeUrlPage.Text = Properties.Strings.IHM_Lbl_MainYoutubeUrlPage;
            this.gb_ExcelOptions.Text = Properties.Strings.IHM_Gb_ExcelOptions;
            this.lbl_FolderPath.Text = Properties.Strings.IHM_Lbl_FolderPath;
            this.btn_changePath.Text = Properties.Strings.IHM_Btn_ChangePath;
            this.lbl_DateFormat.Text = Properties.Strings.IHM_Lbl_DateFormat;
            this.lbl_DurationFormat.Text = Properties.Strings.IHM_Lbl_DurationFormat;
            this.lbl_ThousandSeparator.Text = Properties.Strings.IHM_Lbl_ThousandSeparator;
            this.lbl_MillionSeparator.Text = Properties.Strings.IHM_Lbl_MillionSeparator;
            this.lbl_BilliardSeparator.Text = Properties.Strings.IHM_Lbl_BilliardSeparator;
            this.lbl_VideoSortingChoice.Text = Properties.Strings.IHM_Lbl_VideoSortingChoice;
            this.lbl_ProcessState.Text = Properties.Strings.IHM_Lbl_StateOfProcess;
            this.btn_Analyze.Text = Properties.Strings.IHM_Btn_Analyze;
        }
        #endregion



        #region EVENTS
        private void btn_Analyze_Click(object sender, EventArgs e)
        {
            Analyze();
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
