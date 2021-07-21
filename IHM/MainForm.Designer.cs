
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_folderPath = new System.Windows.Forms.TextBox();
            this.btn_changePath = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_DateFormat = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_durationFormat = new System.Windows.Forms.ComboBox();
            this.gb_ExcelOptions = new System.Windows.Forms.GroupBox();
            this.tb_billiardSeparator = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_millionsSeparator = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_thousandSeparator = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gb_ExcelOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Analyze
            // 
            this.btn_Analyze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Analyze.Location = new System.Drawing.Point(533, 330);
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
            this.tb_urls.Size = new System.Drawing.Size(487, 167);
            this.tb_urls.TabIndex = 1;
            this.tb_urls.Text = "https://www.youtube.com/user/LesTutosdeHuito";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Main youtube url page :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Folder path :";
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Date format :";
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 15);
            this.label5.TabIndex = 101;
            this.label5.Text = "Duration format :";
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
            this.gb_ExcelOptions.Controls.Add(this.tb_billiardSeparator);
            this.gb_ExcelOptions.Controls.Add(this.label7);
            this.gb_ExcelOptions.Controls.Add(this.tb_millionsSeparator);
            this.gb_ExcelOptions.Controls.Add(this.label6);
            this.gb_ExcelOptions.Controls.Add(this.tb_thousandSeparator);
            this.gb_ExcelOptions.Controls.Add(this.label3);
            this.gb_ExcelOptions.Controls.Add(this.tb_folderPath);
            this.gb_ExcelOptions.Controls.Add(this.label5);
            this.gb_ExcelOptions.Controls.Add(this.label2);
            this.gb_ExcelOptions.Controls.Add(this.cb_durationFormat);
            this.gb_ExcelOptions.Controls.Add(this.btn_changePath);
            this.gb_ExcelOptions.Controls.Add(this.cb_DateFormat);
            this.gb_ExcelOptions.Controls.Add(this.label4);
            this.gb_ExcelOptions.Location = new System.Drawing.Point(12, 179);
            this.gb_ExcelOptions.Name = "gb_ExcelOptions";
            this.gb_ExcelOptions.Size = new System.Drawing.Size(633, 145);
            this.gb_ExcelOptions.TabIndex = 102;
            this.gb_ExcelOptions.TabStop = false;
            this.gb_ExcelOptions.Text = "Excel options :";
            // 
            // tb_billiardSeparator
            // 
            this.tb_billiardSeparator.Location = new System.Drawing.Point(499, 113);
            this.tb_billiardSeparator.MaxLength = 3;
            this.tb_billiardSeparator.Name = "tb_billiardSeparator";
            this.tb_billiardSeparator.Size = new System.Drawing.Size(51, 23);
            this.tb_billiardSeparator.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(398, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 15);
            this.label7.TabIndex = 106;
            this.label7.Text = "Billiard separator";
            // 
            // tb_millionsSeparator
            // 
            this.tb_millionsSeparator.Location = new System.Drawing.Point(331, 113);
            this.tb_millionsSeparator.MaxLength = 3;
            this.tb_millionsSeparator.Name = "tb_millionsSeparator";
            this.tb_millionsSeparator.Size = new System.Drawing.Size(51, 23);
            this.tb_millionsSeparator.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(229, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 15);
            this.label6.TabIndex = 104;
            this.label6.Text = "Million separator";
            // 
            // tb_thousandSeparator
            // 
            this.tb_thousandSeparator.Location = new System.Drawing.Point(144, 113);
            this.tb_thousandSeparator.MaxLength = 3;
            this.tb_thousandSeparator.Name = "tb_thousandSeparator";
            this.tb_thousandSeparator.Size = new System.Drawing.Size(51, 23);
            this.tb_thousandSeparator.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 15);
            this.label3.TabIndex = 102;
            this.label3.Text = "Thousand separator";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 365);
            this.Controls.Add(this.gb_ExcelOptions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_urls);
            this.Controls.Add(this.btn_Analyze);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(605, 283);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Youtube Analyzer (by Svik)";
            this.gb_ExcelOptions.ResumeLayout(false);
            this.gb_ExcelOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Analyze;
        private System.Windows.Forms.TextBox tb_urls;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_folderPath;
        private System.Windows.Forms.Button btn_changePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_DateFormat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_durationFormat;
        private System.Windows.Forms.GroupBox gb_ExcelOptions;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_thousandSeparator;
        private System.Windows.Forms.TextBox tb_billiardSeparator;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_millionsSeparator;
        private System.Windows.Forms.Label label6;
    }
}

