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
            SimulateVideoSearch();
        }

        public void SimulateVideoSearch()
        {
            //string videoUrl = "https://www.youtube.com/watch?v=7I_OMwCJN5E";
            string videoUrl = "https://www.youtube.com/watch?v=9Z7STusENH0";
            var videosService = new YoutubeVideosService();
            var video = videosService.GetVideoFromUrl(videoUrl);
        }

    }
}
