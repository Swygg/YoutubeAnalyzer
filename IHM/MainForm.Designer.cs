
namespace IHM
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Analyze = new System.Windows.Forms.Button();
            this.tb_urls = new System.Windows.Forms.TextBox();
            this.lbl_MainYoutubeUrlPage = new System.Windows.Forms.Label();
            this.lbl_FolderPath = new System.Windows.Forms.Label();
            this.tb_folderPath = new System.Windows.Forms.TextBox();
            this.btn_changePath = new System.Windows.Forms.Button();
            this.lbl_DateFormat = new System.Windows.Forms.Label();
            this.cb_DateFormat = new System.Windows.Forms.ComboBox();
            this.lbl_DurationFormat = new System.Windows.Forms.Label();
            this.cb_durationFormat = new System.Windows.Forms.ComboBox();
            this.gb_ExcelOptions = new System.Windows.Forms.GroupBox();
            this.cb_SortVideosType = new System.Windows.Forms.ComboBox();
            this.lbl_VideoSortingChoice = new System.Windows.Forms.Label();
            this.tb_billiardSeparator = new System.Windows.Forms.TextBox();
            this.lbl_BilliardSeparator = new System.Windows.Forms.Label();
            this.tb_millionsSeparator = new System.Windows.Forms.TextBox();
            this.lbl_MillionSeparator = new System.Windows.Forms.Label();
            this.tb_thousandSeparator = new System.Windows.Forms.TextBox();
            this.lbl_ThousandSeparator = new System.Windows.Forms.Label();
            this.lbl_ProcessState = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblYoutubeAPIKey = new System.Windows.Forms.Label();
            this.tb_YoutubeApiKey = new System.Windows.Forms.TextBox();
            this.gb_ExcelOptions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Analyze
            // 
            this.btn_Analyze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Analyze.Location = new System.Drawing.Point(533, 361);
            this.btn_Analyze.Name = "btn_Analyze";
            this.btn_Analyze.Size = new System.Drawing.Size(112, 23);
            this.btn_Analyze.TabIndex = 99;
            this.btn_Analyze.Text = "Analyze !";
            this.btn_Analyze.UseVisualStyleBackColor = true;
            this.btn_Analyze.Click += new System.EventHandler(this.btn_Analyze_Click);
            // 
            // tb_urls
            // 
            this.tb_urls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_urls.Location = new System.Drawing.Point(158, 6);
            this.tb_urls.Multiline = true;
            this.tb_urls.Name = "tb_urls";
            this.tb_urls.Size = new System.Drawing.Size(487, 100);
            this.tb_urls.TabIndex = 0;
            // 
            // lbl_MainYoutubeUrlPage
            // 
            this.lbl_MainYoutubeUrlPage.AutoSize = true;
            this.lbl_MainYoutubeUrlPage.Location = new System.Drawing.Point(12, 9);
            this.lbl_MainYoutubeUrlPage.Name = "lbl_MainYoutubeUrlPage";
            this.lbl_MainYoutubeUrlPage.Size = new System.Drawing.Size(133, 15);
            this.lbl_MainYoutubeUrlPage.TabIndex = 0;
            this.lbl_MainYoutubeUrlPage.Text = "Main youtube url page :";
            // 
            // lbl_FolderPath
            // 
            this.lbl_FolderPath.AutoSize = true;
            this.lbl_FolderPath.Location = new System.Drawing.Point(6, 34);
            this.lbl_FolderPath.Name = "lbl_FolderPath";
            this.lbl_FolderPath.Size = new System.Drawing.Size(73, 15);
            this.lbl_FolderPath.TabIndex = 3;
            this.lbl_FolderPath.Text = "Folder path :";
            // 
            // tb_folderPath
            // 
            this.tb_folderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_folderPath.Location = new System.Drawing.Point(146, 31);
            this.tb_folderPath.Name = "tb_folderPath";
            this.tb_folderPath.Size = new System.Drawing.Size(354, 23);
            this.tb_folderPath.TabIndex = 2;
            this.tb_folderPath.Text = "C:/YoutubeAnalyzerTest/";
            // 
            // btn_changePath
            // 
            this.btn_changePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_changePath.Location = new System.Drawing.Point(506, 31);
            this.btn_changePath.Name = "btn_changePath";
            this.btn_changePath.Size = new System.Drawing.Size(112, 23);
            this.btn_changePath.TabIndex = 3;
            this.btn_changePath.Text = "Change path";
            this.btn_changePath.UseVisualStyleBackColor = true;
            this.btn_changePath.Click += new System.EventHandler(this.changePath_btn_Click);
            // 
            // lbl_DateFormat
            // 
            this.lbl_DateFormat.AutoSize = true;
            this.lbl_DateFormat.Location = new System.Drawing.Point(6, 63);
            this.lbl_DateFormat.Name = "lbl_DateFormat";
            this.lbl_DateFormat.Size = new System.Drawing.Size(76, 15);
            this.lbl_DateFormat.TabIndex = 6;
            this.lbl_DateFormat.Text = "Date format :";
            // 
            // cb_DateFormat
            // 
            this.cb_DateFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_DateFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_DateFormat.FormattingEnabled = true;
            this.cb_DateFormat.ItemHeight = 15;
            this.cb_DateFormat.Items.AddRange(new object[] {
            "dd-MM-yyyy",
            "dd-MM-yy",
            "MM-dd-yyyy",
            "MM-dd-yy",
            "yyyy-MM-dd",
            "yy-MM-dd"});
            this.cb_DateFormat.Location = new System.Drawing.Point(146, 55);
            this.cb_DateFormat.Name = "cb_DateFormat";
            this.cb_DateFormat.Size = new System.Drawing.Size(472, 23);
            this.cb_DateFormat.TabIndex = 4;
            // 
            // lbl_DurationFormat
            // 
            this.lbl_DurationFormat.AutoSize = true;
            this.lbl_DurationFormat.Location = new System.Drawing.Point(6, 92);
            this.lbl_DurationFormat.Name = "lbl_DurationFormat";
            this.lbl_DurationFormat.Size = new System.Drawing.Size(98, 15);
            this.lbl_DurationFormat.TabIndex = 101;
            this.lbl_DurationFormat.Text = "Duration format :";
            // 
            // cb_durationFormat
            // 
            this.cb_durationFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_durationFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_durationFormat.FormattingEnabled = true;
            this.cb_durationFormat.Items.AddRange(new object[] {
            "01:02:03",
            "01-02-03",
            "01h02m03s",
            "01H02M03S"});
            this.cb_durationFormat.Location = new System.Drawing.Point(146, 84);
            this.cb_durationFormat.Name = "cb_durationFormat";
            this.cb_durationFormat.Size = new System.Drawing.Size(472, 23);
            this.cb_durationFormat.TabIndex = 5;
            // 
            // gb_ExcelOptions
            // 
            this.gb_ExcelOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_ExcelOptions.Controls.Add(this.cb_SortVideosType);
            this.gb_ExcelOptions.Controls.Add(this.lbl_VideoSortingChoice);
            this.gb_ExcelOptions.Controls.Add(this.tb_billiardSeparator);
            this.gb_ExcelOptions.Controls.Add(this.lbl_BilliardSeparator);
            this.gb_ExcelOptions.Controls.Add(this.tb_millionsSeparator);
            this.gb_ExcelOptions.Controls.Add(this.lbl_MillionSeparator);
            this.gb_ExcelOptions.Controls.Add(this.tb_thousandSeparator);
            this.gb_ExcelOptions.Controls.Add(this.lbl_ThousandSeparator);
            this.gb_ExcelOptions.Controls.Add(this.tb_folderPath);
            this.gb_ExcelOptions.Controls.Add(this.lbl_DurationFormat);
            this.gb_ExcelOptions.Controls.Add(this.lbl_FolderPath);
            this.gb_ExcelOptions.Controls.Add(this.cb_durationFormat);
            this.gb_ExcelOptions.Controls.Add(this.btn_changePath);
            this.gb_ExcelOptions.Controls.Add(this.cb_DateFormat);
            this.gb_ExcelOptions.Controls.Add(this.lbl_DateFormat);
            this.gb_ExcelOptions.Location = new System.Drawing.Point(12, 180);
            this.gb_ExcelOptions.Name = "gb_ExcelOptions";
            this.gb_ExcelOptions.Size = new System.Drawing.Size(633, 175);
            this.gb_ExcelOptions.TabIndex = 102;
            this.gb_ExcelOptions.TabStop = false;
            this.gb_ExcelOptions.Text = "Excel options :";
            // 
            // cb_SortVideosType
            // 
            this.cb_SortVideosType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_SortVideosType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_SortVideosType.FormattingEnabled = true;
            this.cb_SortVideosType.Location = new System.Drawing.Point(146, 142);
            this.cb_SortVideosType.Name = "cb_SortVideosType";
            this.cb_SortVideosType.Size = new System.Drawing.Size(472, 23);
            this.cb_SortVideosType.TabIndex = 9;
            // 
            // lbl_VideoSortingChoice
            // 
            this.lbl_VideoSortingChoice.AutoSize = true;
            this.lbl_VideoSortingChoice.Location = new System.Drawing.Point(6, 148);
            this.lbl_VideoSortingChoice.Name = "lbl_VideoSortingChoice";
            this.lbl_VideoSortingChoice.Size = new System.Drawing.Size(87, 15);
            this.lbl_VideoSortingChoice.TabIndex = 107;
            this.lbl_VideoSortingChoice.Text = "Sort videos by :";
            // 
            // tb_billiardSeparator
            // 
            this.tb_billiardSeparator.Location = new System.Drawing.Point(567, 113);
            this.tb_billiardSeparator.MaxLength = 3;
            this.tb_billiardSeparator.Name = "tb_billiardSeparator";
            this.tb_billiardSeparator.Size = new System.Drawing.Size(51, 23);
            this.tb_billiardSeparator.TabIndex = 8;
            // 
            // lbl_BilliardSeparator
            // 
            this.lbl_BilliardSeparator.AutoSize = true;
            this.lbl_BilliardSeparator.Location = new System.Drawing.Point(433, 116);
            this.lbl_BilliardSeparator.Name = "lbl_BilliardSeparator";
            this.lbl_BilliardSeparator.Size = new System.Drawing.Size(101, 15);
            this.lbl_BilliardSeparator.TabIndex = 106;
            this.lbl_BilliardSeparator.Text = "Billiard separator :";
            // 
            // tb_millionsSeparator
            // 
            this.tb_millionsSeparator.Location = new System.Drawing.Point(356, 113);
            this.tb_millionsSeparator.MaxLength = 3;
            this.tb_millionsSeparator.Name = "tb_millionsSeparator";
            this.tb_millionsSeparator.Size = new System.Drawing.Size(51, 23);
            this.tb_millionsSeparator.TabIndex = 7;
            // 
            // lbl_MillionSeparator
            // 
            this.lbl_MillionSeparator.AutoSize = true;
            this.lbl_MillionSeparator.Location = new System.Drawing.Point(229, 116);
            this.lbl_MillionSeparator.Name = "lbl_MillionSeparator";
            this.lbl_MillionSeparator.Size = new System.Drawing.Size(102, 15);
            this.lbl_MillionSeparator.TabIndex = 104;
            this.lbl_MillionSeparator.Text = "Million separator :";
            // 
            // tb_thousandSeparator
            // 
            this.tb_thousandSeparator.Location = new System.Drawing.Point(146, 113);
            this.tb_thousandSeparator.MaxLength = 3;
            this.tb_thousandSeparator.Name = "tb_thousandSeparator";
            this.tb_thousandSeparator.Size = new System.Drawing.Size(51, 23);
            this.tb_thousandSeparator.TabIndex = 6;
            // 
            // lbl_ThousandSeparator
            // 
            this.lbl_ThousandSeparator.AutoSize = true;
            this.lbl_ThousandSeparator.Location = new System.Drawing.Point(4, 116);
            this.lbl_ThousandSeparator.Name = "lbl_ThousandSeparator";
            this.lbl_ThousandSeparator.Size = new System.Drawing.Size(117, 15);
            this.lbl_ThousandSeparator.TabIndex = 102;
            this.lbl_ThousandSeparator.Text = "Thousand separator :";
            // 
            // lbl_ProcessState
            // 
            this.lbl_ProcessState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_ProcessState.AutoSize = true;
            this.lbl_ProcessState.Location = new System.Drawing.Point(18, 369);
            this.lbl_ProcessState.Name = "lbl_ProcessState";
            this.lbl_ProcessState.Size = new System.Drawing.Size(217, 15);
            this.lbl_ProcessState.TabIndex = 103;
            this.lbl_ProcessState.Text = "State of process : waiting for user action";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblYoutubeAPIKey);
            this.groupBox1.Controls.Add(this.tb_YoutubeApiKey);
            this.groupBox1.Location = new System.Drawing.Point(12, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(633, 54);
            this.groupBox1.TabIndex = 104;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Youtube API options :";
            // 
            // lblYoutubeAPIKey
            // 
            this.lblYoutubeAPIKey.AutoSize = true;
            this.lblYoutubeAPIKey.Location = new System.Drawing.Point(6, 26);
            this.lblYoutubeAPIKey.Name = "lblYoutubeAPIKey";
            this.lblYoutubeAPIKey.Size = new System.Drawing.Size(100, 15);
            this.lblYoutubeAPIKey.TabIndex = 1;
            this.lblYoutubeAPIKey.Text = "Youtube API Key :";
            // 
            // tb_YoutubeApiKey
            // 
            this.tb_YoutubeApiKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_YoutubeApiKey.Location = new System.Drawing.Point(146, 23);
            this.tb_YoutubeApiKey.Name = "tb_YoutubeApiKey";
            this.tb_YoutubeApiKey.Size = new System.Drawing.Size(472, 23);
            this.tb_YoutubeApiKey.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 396);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_ProcessState);
            this.Controls.Add(this.gb_ExcelOptions);
            this.Controls.Add(this.lbl_MainYoutubeUrlPage);
            this.Controls.Add(this.tb_urls);
            this.Controls.Add(this.btn_Analyze);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(673, 340);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Youtube Analyzer (by Svik)";
            this.gb_ExcelOptions.ResumeLayout(false);
            this.gb_ExcelOptions.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Analyze;
        private System.Windows.Forms.TextBox tb_urls;
        private System.Windows.Forms.Label lbl_MainYoutubeUrlPage;
        private System.Windows.Forms.Label lbl_FolderPath;
        private System.Windows.Forms.TextBox tb_folderPath;
        private System.Windows.Forms.Button btn_changePath;
        private System.Windows.Forms.Label lbl_DateFormat;
        private System.Windows.Forms.ComboBox cb_DateFormat;
        private System.Windows.Forms.Label lbl_DurationFormat;
        private System.Windows.Forms.ComboBox cb_durationFormat;
        private System.Windows.Forms.GroupBox gb_ExcelOptions;
        private System.Windows.Forms.Label lbl_ThousandSeparator;
        private System.Windows.Forms.TextBox tb_thousandSeparator;
        private System.Windows.Forms.TextBox tb_billiardSeparator;
        private System.Windows.Forms.Label lbl_BilliardSeparator;
        private System.Windows.Forms.TextBox tb_millionsSeparator;
        private System.Windows.Forms.Label lbl_MillionSeparator;
        private System.Windows.Forms.Label lbl_ProcessState;
        private System.Windows.Forms.ComboBox cb_SortVideosType;
        private System.Windows.Forms.Label lbl_VideoSortingChoice;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblYoutubeAPIKey;
        private System.Windows.Forms.TextBox tb_YoutubeApiKey;
    }
}

