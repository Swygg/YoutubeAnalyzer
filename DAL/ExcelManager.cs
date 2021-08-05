using DAL.Models;
using ExcelServices;
using ExcelServices.Interfaces;
using ExcelServices.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YoutubeAPI.Models;

namespace DAL
{
    public class ExcelManager
    {
        private static Options _options;

        public static void Save(string pathFolder, List<YoutubeResponse> youtubeResponse, Options options = null)
        {
            _options = options;

            pathFolder = CreateFolderIfNecessary(pathFolder);

            var indexChannels = 0;
            var indexPlaylists = 0;
            var indexVideos = 0;
            for (int i = 0; i < youtubeResponse.Count; i++)
            {

                if (youtubeResponse[i].Channel != null)
                {
                    indexChannels++;
                    SaveChannel(pathFolder, youtubeResponse[i].Channel, indexChannels);

                }
                else if (youtubeResponse[i].Playlist != null)
                {
                    indexPlaylists++;
                    SavePlaylist(pathFolder, youtubeResponse[i].Playlist, indexPlaylists);
                }
                else if (youtubeResponse[i].Video != null)
                {
                    indexVideos++;
                    SaveVideo(pathFolder, youtubeResponse[i].Video, indexVideos);
                }
            }
        }

        private static void SaveChannel(string pathFile, YoutubeChannel channel, int index)
        {
            var workbook = new Workbook
            {
                Name = $"{Resources.Strings.Channel}-{index}"
            };

            workbook.Worksheets.Add(GetPresentationWorksheet(channel));
            workbook.Worksheets.Add(GetVideosWorksheet(channel.Videos, Resources.Strings.Videos));
            workbook.Worksheets.Add(GetPlaylistsWorksheet(channel.Playlists));
            for (int indexPlaylist = 0; indexPlaylist < channel.Playlists.Count; indexPlaylist++)
            {
                workbook.Worksheets.Add(GetVideosWorksheet(channel.Playlists[indexPlaylist].Videos, $"{Resources.Strings.Playlist} {indexPlaylist + 1}"));
            }

            IExcelService ExcelService = GetExcelService();
            ExcelService.Create(pathFile, workbook);
        }

        private static void SavePlaylist(string pathFile, YoutubePlaylist playlist, int index)
        {
            var workbook = new Workbook
            {
                Name = $"{Resources.Strings.Playlist}-{index}"
            };

            workbook.Worksheets.Add(GetPlaylistsWorksheet(new List<YoutubePlaylist>() { playlist }));
            workbook.Worksheets.Add(GetVideosWorksheet(playlist.Videos, $"{Resources.Strings.Playlist} 1"));

            IExcelService ExcelService = GetExcelService();
            ExcelService.Create(pathFile, workbook);
        }

        private static void SaveVideo(string pathFile, YoutubeVideo video, int index)
        {
            var workbook = new Workbook
            {
                Name = $"{Resources.Strings.Video}-{index}"
            };

            workbook.Worksheets.Add(GetVideosWorksheet(new List<YoutubeVideo>() { video }, Resources.Strings.Video));

            IExcelService ExcelService = GetExcelService();
            ExcelService.Create(pathFile, workbook);
        }

        private static Worksheet GetPresentationWorksheet(YoutubeChannel youtubeChannel)
        {
            var worksheet = new Worksheet();
            worksheet.Name = Resources.Strings.Presentation;

            var cells = new List<Cell>();
            var titleStyle = GetTitleStyle();
            var rowIndex = 0;

            //NAME
            cells.Add(new Cell(rowIndex, 0, Resources.Strings.Data_Name, titleStyle));
            cells.Add(new Cell(rowIndex, 1, youtubeChannel.Name));

            //NB SUBSCRIBER
            cells.Add(new Cell(++rowIndex, 0, Resources.Strings.Data_NbSubscribers, titleStyle));
            cells.Add(new Cell(rowIndex, 1, GetFormatedNumber(youtubeChannel.NbSubscribers)));

            //NB VIEWS
            cells.Add(new Cell(++rowIndex, 0, Resources.Strings.Data_NbViews, titleStyle));
            cells.Add(new Cell(rowIndex, 1, GetFormatedNumber(youtubeChannel.NbViews)));

            //SUBSCRIPTION DATE
            cells.Add(new Cell(++rowIndex, 0, Resources.Strings.Data_SubscriptionDate, titleStyle));
            cells.Add(new Cell(rowIndex, 1, youtubeChannel.SubscriptionDate?.ToString(GetDateFormat())));

            //TRUE URL
            cells.Add(new Cell(++rowIndex, 0, Resources.Strings.Data_SubscriptionDate, titleStyle));
            cells.Add(new Cell(rowIndex, 1, youtubeChannel.TrueUrl));

            //FACEBOOK LINK
            cells.Add(new Cell(++rowIndex, 0, Resources.Strings.Data_FacebookLink, titleStyle));
            cells.Add(new Cell(rowIndex, 1, youtubeChannel.FacebookLink));

            //TWITTER LINK
            cells.Add(new Cell(++rowIndex, 0, Resources.Strings.Data_TwitterLink, titleStyle));
            cells.Add(new Cell(rowIndex, 1, youtubeChannel.TwitterLink));

            //DESCRIPTION
            cells.Add(new Cell(++rowIndex, 0, Resources.Strings.Data_Description, titleStyle));
            cells.Add(new Cell(rowIndex, 1, youtubeChannel.Description));

            worksheet.Cells = cells;
            worksheet.ColumnsSize = new List<ColumnSize>()
            {
                new ColumnSize(){Index = 0, Size = ColumnSize.AUTOSIZE},
                new ColumnSize(){Index = 1, Size = 50 },
            };
            return worksheet;
        }

        private static Worksheet GetPlaylistsWorksheet(List<YoutubePlaylist> playlists)
        {
            var worksheet = new Worksheet
            {
                Name = Resources.Strings.Playlists
            };

            var cells = new List<Cell>();

            var rowIndex = 0;

            const int NAME = 0;
            const int NBVIDEOS = 1;
            const int PLAYLISTINDEX = 2;
            const int URL = 3;


            var titleStyle = GetTitleStyle();

            var newStyle = new CellStyle()
            {
                HorizontalAlignment = EHorizontalAlignment.Center
            };

            // COLUMNS NAME
            cells.Add(new Cell(rowIndex, NAME, Resources.Strings.Data_Name, titleStyle));
            cells.Add(new Cell(rowIndex, NBVIDEOS, Resources.Strings.Data_NbVideos, titleStyle));
            cells.Add(new Cell(rowIndex, PLAYLISTINDEX, Resources.Strings.Data_PlaylistIndex, titleStyle));
            cells.Add(new Cell(rowIndex, URL, Resources.Strings.Url, titleStyle));

            int indexPlaylist = 1;
            foreach (var playlist in playlists)
            {
                rowIndex++;
                cells.Add(new Cell(rowIndex, NAME, playlist.Name));
                cells.Add(new Cell(rowIndex, NBVIDEOS, playlist.Videos.Count, newStyle));
                cells.Add(new Cell(rowIndex, PLAYLISTINDEX, indexPlaylist++, newStyle));
                cells.Add(new Cell(rowIndex, URL, playlist.Url));
            }

            worksheet.Cells = cells;

            worksheet.ColumnsSize = new List<ColumnSize>()
            {
                new ColumnSize(){Index = NAME, Size = ColumnSize.AUTOSIZE},
                new ColumnSize(){Index = NBVIDEOS, Size = ColumnSize.AUTOSIZE},
                new ColumnSize(){Index = PLAYLISTINDEX, Size = ColumnSize.AUTOSIZE},
            };
            return worksheet;
        }

        private static Worksheet GetVideosWorksheet(List<YoutubeVideo> videos, string title)
        {
            var worksheet = new Worksheet();
            worksheet.Name = title;

            var cells = new List<Cell>();

            var rowIndex = 0;

            const int NAME = 0;
            const int DURATION = 1;
            const int CREATIONDATE = 2;
            const int NBVIEW = 3;
            const int NBPOSITIVEFEEBACK = 4;
            const int NBNEGATIVEFEEBACK = 5;
            const int URL = 6;
            const int DESCRIPTION = 7;

            var titleStyle = GetTitleStyle();

            var newStyle = new CellStyle()
            {
                HorizontalAlignment = EHorizontalAlignment.Center
            };

            // COLUMNS NAME
            cells.Add(new Cell(rowIndex, NAME, Resources.Strings.Data_Name, titleStyle));
            cells.Add(new Cell(rowIndex, DURATION, Resources.Strings.Data_Duration, titleStyle));
            cells.Add(new Cell(rowIndex, CREATIONDATE, Resources.Strings.Data_CreationDate, titleStyle));
            cells.Add(new Cell(rowIndex, NBVIEW, Resources.Strings.Data_NbViews, titleStyle));
            cells.Add(new Cell(rowIndex, NBPOSITIVEFEEBACK, Resources.Strings.Data_NbPositiveFeedback, titleStyle));
            cells.Add(new Cell(rowIndex, NBNEGATIVEFEEBACK, Resources.Strings.Data_NbNegativeFeedback, titleStyle));
            cells.Add(new Cell(rowIndex, URL, Resources.Strings.Url, titleStyle)); ;
            cells.Add(new Cell(rowIndex, DESCRIPTION, Resources.Strings.Data_Description, titleStyle));

            foreach (var video in videos)
            {
                rowIndex++;
                cells.Add(new Cell(rowIndex, NAME, video.Name));
                cells.Add(new Cell(rowIndex, DURATION, GetDateFormat(video.Duration), newStyle));
                cells.Add(new Cell(rowIndex, CREATIONDATE, video.CreationDate?.ToString(GetDateFormat()), newStyle));
                cells.Add(new Cell(rowIndex, NBVIEW, GetFormatedNumber(video.NbViews), newStyle));
                cells.Add(new Cell(rowIndex, NBPOSITIVEFEEBACK, GetFormatedNumber(video.NbPositiveFeedbacks), newStyle));
                cells.Add(new Cell(rowIndex, NBNEGATIVEFEEBACK, GetFormatedNumber(video.NbNegativeFeedbacks), newStyle));
                cells.Add(new Cell(rowIndex, URL, video.Url));
                cells.Add(new Cell(rowIndex, DESCRIPTION, video.Description));
            }

            worksheet.Cells = cells;

            worksheet.ColumnsSize = new List<ColumnSize>()
            {
                new ColumnSize(){Index = NAME, Size = ColumnSize.AUTOSIZE},
                new ColumnSize(){Index = DURATION, Size = ColumnSize.AUTOSIZE},
                new ColumnSize(){Index = CREATIONDATE, Size = ColumnSize.AUTOSIZE},
                new ColumnSize(){Index = NBVIEW, Size = ColumnSize.AUTOSIZE},
                new ColumnSize(){Index = NBPOSITIVEFEEBACK, Size = ColumnSize.AUTOSIZE},
                new ColumnSize(){Index = NBNEGATIVEFEEBACK, Size = ColumnSize.AUTOSIZE},
            };
            return worksheet;
        }

        private static IExcelService GetExcelService()
        {
            return new NPOIService();
        }

        private static CellStyle GetTitleStyle()
        {
            return new CellStyle()
            {
                IsBold = true,
                IsItalic = true,
                IsUnderline = true,
                HorizontalAlignment = EHorizontalAlignment.Center,
                VerticalAlignment = EVerticalAlignment.Center
            };
        }

        private static string GetDateFormat()
        {
            return _options?.DateFormat ?? "dd-MM-yyyy";
        }

        private static string GetDateFormat(TimeSpan? time)
        {
            if (time == null)
                return null;
            if (_options == null)
                return $"{time?.Hours.ToString("00")}:{time?.Minutes.ToString("00")}:{time?.Seconds.ToString("00")}";

            switch (_options.DurationFormat)
            {
                case EDurationFormat.TwoPointsTwoPoint:
                default:
                    return $"{time?.Hours.ToString("00")}:{time?.Minutes.ToString("00")}:{time?.Seconds.ToString("00")}";
                case EDurationFormat.HyphenHyphen:
                    return $"{time?.Hours.ToString("00")}-{time?.Minutes.ToString("00")}-{time?.Seconds.ToString("00")}";
                case EDurationFormat.LettersLowerCase:
                    return $"{time?.Hours.ToString("00")}h{time?.Minutes.ToString("00")}m{time?.Seconds.ToString("00")}s";
                case EDurationFormat.LettersUpperCase:
                    return $"{time?.Hours.ToString("00")}H{time?.Minutes.ToString("00")}M{time?.Seconds.ToString("00")}S";
            }
        }

        private static string GetNewUniqueFolderName()
        {
            return DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + '/';
        }

        private static string CreateFolderIfNecessary(string pathFolder)
        {
            var folderName = GetNewUniqueFolderName();
            if (pathFolder[pathFolder.Length - 1] != '/')
                pathFolder += "/";
            pathFolder += folderName;
            if (!Directory.Exists(pathFolder))
                Directory.CreateDirectory(pathFolder);
            return pathFolder;
        }

        private static string GetFormatedNumber(int number)
        {
            var sb = new StringBuilder();
            var value = string.Empty;
            foreach (var item in number.ToString())
            {
                value = item + value;
            }

            for (int i = value.Length - 1; i >= 0; i--)
            {
                sb.Append(value[i]);
                if (i == 10 && _options.BilliardSeparator != null)
                    sb.Append(_options.BilliardSeparator);
                else if (i == 6 && _options.MillionsSeparator != null)
                    sb.Append(_options.MillionsSeparator);
                else if (i == 3 && _options.ThousandSeparator != null)
                    sb.Append(_options.ThousandSeparator);
            }
            return sb.ToString();
        }
    }
}
