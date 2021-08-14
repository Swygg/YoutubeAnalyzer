using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeAPI.Models;
using YoutubeAPI.Services;

namespace IHM
{
    public partial class MainForm : Form
    {
        private string _processState = Properties.Strings.ProcessState;
        private bool _working = false;
        private Task<List<YoutubeResponse>> _task;
        CancellationTokenSource _tokenSource;
        CancellationToken _token;

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
        private List<YoutubeResponse> Analyze()
        {
            if (string.IsNullOrEmpty(tb_urls.Text))
            {
                this.ShowError(Properties.Strings.Err_OneUrlMinimum);
                return null;
            }
            if (string.IsNullOrEmpty(tb_folderPath.Text))
            {
                this.ShowError(Properties.Strings.Err_UserMustGiveOneFolderPath);
                return null;
            }

            var youtubeResponses = new List<YoutubeResponse>();
            var links = GetLinks();
            var youtubeSearchService = new YoutubeSearchService(tb_YoutubeApiKey.Text);

            Parallel.ForEach(links, link =>
            {
                if (_token.IsCancellationRequested)
                    return;
                var response = youtubeSearchService.GetAnswerFromLink(link);
                if (response != null)
                {
                    youtubeResponses.Add(response);
                }
            });

            return youtubeResponses;
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
            tb_YoutubeApiKey.Text = IHM.Properties.Settings.Default.YoutubeAPIKey;
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
            IHM.Properties.Settings.Default.YoutubeAPIKey = tb_YoutubeApiKey.Text;
            IHM.Properties.Settings.Default.Save();
        }

        private void Localization()
        {
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
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

            cb_SortVideosType.Items.Add(Properties.VideosSorting.NotSorted);
            cb_SortVideosType.Items.Add(Properties.VideosSorting.DurationAsc);
            cb_SortVideosType.Items.Add(Properties.VideosSorting.DurationDesc);
            cb_SortVideosType.Items.Add(Properties.VideosSorting.DateCreationAsc);
            cb_SortVideosType.Items.Add(Properties.VideosSorting.DateCreationDesc);
            cb_SortVideosType.Items.Add(Properties.VideosSorting.NumberViewsAsc);
            cb_SortVideosType.Items.Add(Properties.VideosSorting.NumberViewsDesc);
            cb_SortVideosType.Items.Add(Properties.VideosSorting.NumberPositivesFeedbackAsc);
            cb_SortVideosType.Items.Add(Properties.VideosSorting.NumberPositivesFeedbackDesc);
            cb_SortVideosType.Items.Add(Properties.VideosSorting.NumberNegativesFeedbackAsc);
            cb_SortVideosType.Items.Add(Properties.VideosSorting.NumberNegativesFeedbackDesc);
            cb_SortVideosType.Items.Add(Properties.VideosSorting.NameAsc);
            cb_SortVideosType.Items.Add(Properties.VideosSorting.NameDesc);
        }
        #endregion



        #region EVENTS
        private async void btn_Analyze_Click(object sender, EventArgs e)
        {
            if (_working)
            {
                btn_Analyze.Enabled = false;
                _tokenSource.Cancel();
                _tokenSource.Dispose();
                _working = false;
                btn_Analyze.Text = Properties.Strings.IHM_Btn_Analyze;
            }
            else
            {
                _working = true;
                btn_Analyze.Text = Properties.Strings.IHM_Btn_AnalyzeStop;

                InformUserProcessIsStarting();
                var startProcess = DateTime.Now;

                _tokenSource = new CancellationTokenSource();
                _token = _tokenSource.Token;

                _task = Task.Run(() =>
                {
                    Thread.Sleep(2000);
                    return Analyze();
                }, _token);

                var youtubeResponses = await _task;

                string message;
                if (!_token.IsCancellationRequested)
                {

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
                    message = string.Format(Properties.Strings.SuccessMessage,
                        tb_folderPath.Text + Environment.NewLine,
                        GetDurationReadableFormat(time));
                }
                else
                {
                    message = Properties.Strings.UserAskForStop;
                    btn_Analyze.Enabled = true;
                }
                MessageBox.Show(message, Properties.Strings.SuccessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            _working = !_working;
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
    }
}
