using System.Collections.Generic;
using YoutubeAPI.Models;
using ExcelServices.Interfaces;
using Ex = ExcelServices;
using System;
using ExcelServices;

namespace DAL
{
    public static class ExcelManager
    {
        public static void Save(string pathFile, List<YoutubeResponse> youtubeResponse)
        {
            for (int i = 0; i < youtubeResponse.Count; i++)
            {
                var indexChannels = 1;
                var indexPlaylists = 1;
                var indexVideos = 1;
                if(youtubeResponse[i].Channel != null)
                {
                    SaveChannel(pathFile, youtubeResponse[i].Channel, indexChannels);

                }
                else if(youtubeResponse[i].Playlist != null)
                {
                    SavePlaylist(pathFile, youtubeResponse[i].Playlist, indexPlaylists);
                }
                else if (youtubeResponse[i].Video != null)
                {
                    SaveVideo(pathFile, youtubeResponse[i].Video, indexVideos);
                }
            }
        }

        private static void SaveChannel(string pathFile, YoutubeChannel channel, int index)
        {
            var workbook = new Workbook
            {
                Name = $"Channel-{index}"
            };

            workbook.Worksheets.Add(GetPresentationWorksheet(channel));
            workbook.Worksheets.Add(GetVideosWorksheet(channel.Videos));

            IExcelService ExcelService = GetExcelService();
            ExcelService.Create(pathFile, workbook);
        }

        private static void SavePlaylist(string pathFile, YoutubePlaylist playlist, int index)
        {

        }

        private static void SaveVideo(string pathFile, YoutubeVideo video, int index)
        {

        }

        private static Worksheet GetPresentationWorksheet(YoutubeChannel youtubeChannel)
        {
            var worksheet = new Worksheet();
            worksheet.Name = "Presentation";

            var cells = new List<Cell>();

            var rowIndex = 0;

            //NAME
            cells.Add(new Cell(rowIndex, 0, "Name"));
            cells.Add(new Cell(rowIndex, 1, youtubeChannel.Name));

            //NB SUBSCRIBER
            cells.Add(new Cell(++rowIndex, 0, "Nb subscribers"));
            cells.Add(new Cell(rowIndex, 1, youtubeChannel.NbSubscribers));

            //NB VIEWS
            cells.Add(new Cell(++rowIndex, 0, "Nb views"));
            cells.Add(new Cell(rowIndex, 1, youtubeChannel.NbViews));

            //SUBSCRIPTION DATE
            cells.Add(new Cell(++rowIndex, 0, "Subscription date"));
            cells.Add(new Cell(rowIndex, 1, youtubeChannel.SubscriptionDate?.ToString("dd-MM-yyyy")));

            //FACEBOOK LINK
            cells.Add(new Cell(++rowIndex, 0, "Facebook link"));
            cells.Add(new Cell(rowIndex, 1, youtubeChannel.FacebookLink));

            //TWITTER LINK
            cells.Add(new Cell(++rowIndex, 0, "Twitter link"));
            cells.Add(new Cell(rowIndex, 1, youtubeChannel.TwitterLink));

            //DESCRIPTION
            cells.Add(new Cell(++rowIndex, 0, "Description"));
            cells.Add(new Cell(rowIndex, 1, youtubeChannel.Description));

            worksheet.Cells = cells;
            return worksheet;
        }

        private static Worksheet GetVideosWorksheet(List<YoutubeVideo> videos)
        {
            var worksheet = new Worksheet();
            worksheet.Name = "Videos";

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

            var cellStyle = new CellStyle()
            {
                IsBold = true,
                IsItalic = true,
                IsUnderline = true,
                HorizontalAlignment = EHorizontalAlignment.Center,
                VerticalAlignment = EVerticalAlignment.Center
            };

            // COLUMNS NAME
            cells.Add(new Cell(rowIndex, NAME, "Name", cellStyle));
            cells.Add(new Cell(rowIndex, DURATION, "Duration", cellStyle));
            cells.Add(new Cell(rowIndex, CREATIONDATE, "Creation date", cellStyle));
            cells.Add(new Cell(rowIndex, NBVIEW, "Nb views", cellStyle));
            cells.Add(new Cell(rowIndex, NBPOSITIVEFEEBACK, "Nb positive feedback", cellStyle));
            cells.Add(new Cell(rowIndex, NBNEGATIVEFEEBACK, "Nb negative feedback", cellStyle));
            cells.Add(new Cell(rowIndex, URL, "Url", cellStyle));
            cells.Add(new Cell(rowIndex, DESCRIPTION, "Description", cellStyle));

            foreach (var video in videos)
            {
                rowIndex++;
                cells.Add(new Cell(rowIndex, NAME, video.Name));
                cells.Add(new Cell(rowIndex, DURATION, GetDurationReadableFormat(video.Duration)));
                cells.Add(new Cell(rowIndex, CREATIONDATE, video.CreationDate?.ToString("dd-MM-yyyy")));
                cells.Add(new Cell(rowIndex, NBVIEW, video.NbViews));
                cells.Add(new Cell(rowIndex, NBPOSITIVEFEEBACK, video.NbPositiveFeedbacks));
                cells.Add(new Cell(rowIndex, NBNEGATIVEFEEBACK, video.NbNegativeFeedbacks));
                cells.Add(new Cell(rowIndex, URL, video.Url));
                cells.Add(new Cell(rowIndex, DESCRIPTION, video.Description));
            }

            worksheet.Cells = cells;
            return worksheet;
        }

        private static string GetDurationReadableFormat(TimeSpan? durationHardToRead)
        {
            if (durationHardToRead == null)
                return null;
            return $"{durationHardToRead?.Hours.ToString("00")}:{durationHardToRead?.Minutes.ToString("00")}:{durationHardToRead?.Seconds.ToString("00")}";
        }

        private static IExcelService GetExcelService()
        {
            return new NPOIService();
        }
    }
}
