
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
            this.urls_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.folderPath_tb = new System.Windows.Forms.TextBox();
            this.changePath_btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Analyze
            // 
            this.btn_Analyze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Analyze.Location = new System.Drawing.Point(440, 136);
            this.btn_Analyze.Name = "btn_Analyze";
            this.btn_Analyze.Size = new System.Drawing.Size(112, 23);
            this.btn_Analyze.TabIndex = 4;
            this.btn_Analyze.Text = "Analyze !";
            this.btn_Analyze.UseVisualStyleBackColor = true;
            this.btn_Analyze.Click += new System.EventHandler(this.btn_Analyze_Click);
            // 
            // urls_tb
            // 
            this.urls_tb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.urls_tb.Location = new System.Drawing.Point(158, 6);
            this.urls_tb.Multiline = true;
            this.urls_tb.Name = "urls_tb";
            this.urls_tb.Size = new System.Drawing.Size(394, 86);
            this.urls_tb.TabIndex = 1;
            this.urls_tb.Text = "https://www.youtube.com/user/LesTutosdeHuito";
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
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Folder path :";
            // 
            // folderPath_tb
            // 
            this.folderPath_tb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.folderPath_tb.Location = new System.Drawing.Point(158, 96);
            this.folderPath_tb.Name = "folderPath_tb";
            this.folderPath_tb.Size = new System.Drawing.Size(276, 23);
            this.folderPath_tb.TabIndex = 2;
            this.folderPath_tb.Text = "C:/YoutubeAnalyzerTest/";
            // 
            // changePath_btn
            // 
            this.changePath_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.changePath_btn.Location = new System.Drawing.Point(440, 96);
            this.changePath_btn.Name = "changePath_btn";
            this.changePath_btn.Size = new System.Drawing.Size(112, 23);
            this.changePath_btn.TabIndex = 3;
            this.changePath_btn.Text = "Change path";
            this.changePath_btn.UseVisualStyleBackColor = true;
            this.changePath_btn.Click += new System.EventHandler(this.changePath_btn_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(382, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "For now, we only work with channel. Videos and playlists are incoming.";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 171);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.changePath_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.folderPath_tb);
            this.Controls.Add(this.urls_tb);
            this.Controls.Add(this.btn_Analyze);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Youtube Analyzer (by Svik)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Analyze;
        private System.Windows.Forms.TextBox urls_tb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox folderPath_tb;
        private System.Windows.Forms.Button changePath_btn;
        private System.Windows.Forms.Label label3;
    }
}

