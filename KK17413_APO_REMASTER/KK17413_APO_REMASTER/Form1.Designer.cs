namespace KK17413_APO_REMASTER
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.operationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tresholdingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binaryzationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aDAPTIVEToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.posterizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rozcioganieZakresuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.laby3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wygladzanieLinioweToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gussianBlurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medianBlurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cannyDetectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobelDetectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.laplaceDetectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageSharpeningWithLaplacianFitlerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detekcjaKrawedziPrewittToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zad3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blendingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myStuffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.erosionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dialationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gradientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topHatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.blackHatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 63);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(375, 375);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.operationsToolStripMenuItem,
            this.laby3ToolStripMenuItem,
            this.myStuffToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // operationsToolStripMenuItem
            // 
            this.operationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tresholdingToolStripMenuItem,
            this.posterizeToolStripMenuItem,
            this.rozcioganieZakresuToolStripMenuItem});
            this.operationsToolStripMenuItem.Name = "operationsToolStripMenuItem";
            this.operationsToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.operationsToolStripMenuItem.Text = "Operations";
            // 
            // tresholdingToolStripMenuItem
            // 
            this.tresholdingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.binaryzationToolStripMenuItem,
            this.aDAPTIVEToolStripMenuItem1});
            this.tresholdingToolStripMenuItem.Name = "tresholdingToolStripMenuItem";
            this.tresholdingToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.tresholdingToolStripMenuItem.Text = "Tresholding";
            // 
            // binaryzationToolStripMenuItem
            // 
            this.binaryzationToolStripMenuItem.Name = "binaryzationToolStripMenuItem";
            this.binaryzationToolStripMenuItem.Size = new System.Drawing.Size(174, 26);
            this.binaryzationToolStripMenuItem.Text = "Binaryzation";
            this.binaryzationToolStripMenuItem.Click += new System.EventHandler(this.binaryzationToolStripMenuItem_Click);
            // 
            // aDAPTIVEToolStripMenuItem1
            // 
            this.aDAPTIVEToolStripMenuItem1.Name = "aDAPTIVEToolStripMenuItem1";
            this.aDAPTIVEToolStripMenuItem1.Size = new System.Drawing.Size(174, 26);
            this.aDAPTIVEToolStripMenuItem1.Text = "ADAPTIVE";
            this.aDAPTIVEToolStripMenuItem1.Click += new System.EventHandler(this.aDAPTIVEToolStripMenuItem1_Click);
            // 
            // posterizeToolStripMenuItem
            // 
            this.posterizeToolStripMenuItem.Name = "posterizeToolStripMenuItem";
            this.posterizeToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.posterizeToolStripMenuItem.Text = "Posterize";
            this.posterizeToolStripMenuItem.Click += new System.EventHandler(this.posterizeToolStripMenuItem_Click);
            // 
            // rozcioganieZakresuToolStripMenuItem
            // 
            this.rozcioganieZakresuToolStripMenuItem.Name = "rozcioganieZakresuToolStripMenuItem";
            this.rozcioganieZakresuToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.rozcioganieZakresuToolStripMenuItem.Text = "Rozcioganie zakresu";
            this.rozcioganieZakresuToolStripMenuItem.Click += new System.EventHandler(this.rozcioganieZakresuToolStripMenuItem_Click);
            // 
            // laby3ToolStripMenuItem
            // 
            this.laby3ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wygladzanieLinioweToolStripMenuItem,
            this.gussianBlurToolStripMenuItem,
            this.medianBlurToolStripMenuItem,
            this.cannyDetectionToolStripMenuItem,
            this.sobelDetectionToolStripMenuItem,
            this.laplaceDetectionToolStripMenuItem,
            this.imageSharpeningWithLaplacianFitlerToolStripMenuItem,
            this.detekcjaKrawedziPrewittToolStripMenuItem,
            this.zad3ToolStripMenuItem});
            this.laby3ToolStripMenuItem.Name = "laby3ToolStripMenuItem";
            this.laby3ToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.laby3ToolStripMenuItem.Text = "laby 3";
            // 
            // wygladzanieLinioweToolStripMenuItem
            // 
            this.wygladzanieLinioweToolStripMenuItem.Name = "wygladzanieLinioweToolStripMenuItem";
            this.wygladzanieLinioweToolStripMenuItem.Size = new System.Drawing.Size(349, 26);
            this.wygladzanieLinioweToolStripMenuItem.Text = "Wygladzanie liniowe";
            this.wygladzanieLinioweToolStripMenuItem.Click += new System.EventHandler(this.wygladzanieLinioweToolStripMenuItem_Click);
            // 
            // gussianBlurToolStripMenuItem
            // 
            this.gussianBlurToolStripMenuItem.Name = "gussianBlurToolStripMenuItem";
            this.gussianBlurToolStripMenuItem.Size = new System.Drawing.Size(349, 26);
            this.gussianBlurToolStripMenuItem.Text = "gussianBlur";
            this.gussianBlurToolStripMenuItem.Click += new System.EventHandler(this.gussianBlurToolStripMenuItem_Click);
            // 
            // medianBlurToolStripMenuItem
            // 
            this.medianBlurToolStripMenuItem.Name = "medianBlurToolStripMenuItem";
            this.medianBlurToolStripMenuItem.Size = new System.Drawing.Size(349, 26);
            this.medianBlurToolStripMenuItem.Text = "MedianBlur";
            this.medianBlurToolStripMenuItem.Click += new System.EventHandler(this.medianBlurToolStripMenuItem_Click);
            // 
            // cannyDetectionToolStripMenuItem
            // 
            this.cannyDetectionToolStripMenuItem.Name = "cannyDetectionToolStripMenuItem";
            this.cannyDetectionToolStripMenuItem.Size = new System.Drawing.Size(349, 26);
            this.cannyDetectionToolStripMenuItem.Text = "CannyDetection";
            this.cannyDetectionToolStripMenuItem.Click += new System.EventHandler(this.cannyDetectionToolStripMenuItem_Click);
            // 
            // sobelDetectionToolStripMenuItem
            // 
            this.sobelDetectionToolStripMenuItem.Name = "sobelDetectionToolStripMenuItem";
            this.sobelDetectionToolStripMenuItem.Size = new System.Drawing.Size(349, 26);
            this.sobelDetectionToolStripMenuItem.Text = "SobelDetection";
            this.sobelDetectionToolStripMenuItem.Click += new System.EventHandler(this.sobelDetectionToolStripMenuItem_Click);
            // 
            // laplaceDetectionToolStripMenuItem
            // 
            this.laplaceDetectionToolStripMenuItem.Name = "laplaceDetectionToolStripMenuItem";
            this.laplaceDetectionToolStripMenuItem.Size = new System.Drawing.Size(349, 26);
            this.laplaceDetectionToolStripMenuItem.Text = "LaplaceDetection";
            this.laplaceDetectionToolStripMenuItem.Click += new System.EventHandler(this.laplaceDetectionToolStripMenuItem_Click);
            // 
            // imageSharpeningWithLaplacianFitlerToolStripMenuItem
            // 
            this.imageSharpeningWithLaplacianFitlerToolStripMenuItem.Name = "imageSharpeningWithLaplacianFitlerToolStripMenuItem";
            this.imageSharpeningWithLaplacianFitlerToolStripMenuItem.Size = new System.Drawing.Size(349, 26);
            this.imageSharpeningWithLaplacianFitlerToolStripMenuItem.Text = "Image Sharpening with Laplacian Fitler";
            this.imageSharpeningWithLaplacianFitlerToolStripMenuItem.Click += new System.EventHandler(this.imageSharpeningWithLaplacianFitlerToolStripMenuItem_Click);
            // 
            // detekcjaKrawedziPrewittToolStripMenuItem
            // 
            this.detekcjaKrawedziPrewittToolStripMenuItem.Name = "detekcjaKrawedziPrewittToolStripMenuItem";
            this.detekcjaKrawedziPrewittToolStripMenuItem.Size = new System.Drawing.Size(349, 26);
            this.detekcjaKrawedziPrewittToolStripMenuItem.Text = "Detekcja KrawedziPrewitt";
            this.detekcjaKrawedziPrewittToolStripMenuItem.Click += new System.EventHandler(this.detekcjaKrawedziPrewittToolStripMenuItem_Click);
            // 
            // zad3ToolStripMenuItem
            // 
            this.zad3ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blendingToolStripMenuItem});
            this.zad3ToolStripMenuItem.Name = "zad3ToolStripMenuItem";
            this.zad3ToolStripMenuItem.Size = new System.Drawing.Size(349, 26);
            this.zad3ToolStripMenuItem.Text = "zad3";
            // 
            // blendingToolStripMenuItem
            // 
            this.blendingToolStripMenuItem.Name = "blendingToolStripMenuItem";
            this.blendingToolStripMenuItem.Size = new System.Drawing.Size(151, 26);
            this.blendingToolStripMenuItem.Text = "Blending";
            this.blendingToolStripMenuItem.Click += new System.EventHandler(this.blendingToolStripMenuItem_Click);
            // 
            // myStuffToolStripMenuItem
            // 
            this.myStuffToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.erosionToolStripMenuItem,
            this.openingToolStripMenuItem,
            this.dialationToolStripMenuItem,
            this.closingToolStripMenuItem,
            this.gradientToolStripMenuItem,
            this.topHatToolStripMenuItem,
            this.blackHatToolStripMenuItem});
            this.myStuffToolStripMenuItem.Name = "myStuffToolStripMenuItem";
            this.myStuffToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.myStuffToolStripMenuItem.Text = "MyStuff";
            // 
            // erosionToolStripMenuItem
            // 
            this.erosionToolStripMenuItem.Name = "erosionToolStripMenuItem";
            this.erosionToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.erosionToolStripMenuItem.Text = "Erosion";
            this.erosionToolStripMenuItem.Click += new System.EventHandler(this.erosionToolStripMenuItem_Click);
            // 
            // openingToolStripMenuItem
            // 
            this.openingToolStripMenuItem.Name = "openingToolStripMenuItem";
            this.openingToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.openingToolStripMenuItem.Text = "Opening";
            this.openingToolStripMenuItem.Click += new System.EventHandler(this.openingToolStripMenuItem_Click);
            // 
            // dialationToolStripMenuItem
            // 
            this.dialationToolStripMenuItem.Name = "dialationToolStripMenuItem";
            this.dialationToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.dialationToolStripMenuItem.Text = "Dialation";
            this.dialationToolStripMenuItem.Click += new System.EventHandler(this.dialationToolStripMenuItem_Click);
            // 
            // closingToolStripMenuItem
            // 
            this.closingToolStripMenuItem.Name = "closingToolStripMenuItem";
            this.closingToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.closingToolStripMenuItem.Text = "Closing";
            this.closingToolStripMenuItem.Click += new System.EventHandler(this.closingToolStripMenuItem_Click);
            // 
            // gradientToolStripMenuItem
            // 
            this.gradientToolStripMenuItem.Name = "gradientToolStripMenuItem";
            this.gradientToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.gradientToolStripMenuItem.Text = "Gradient";
            this.gradientToolStripMenuItem.Click += new System.EventHandler(this.gradientToolStripMenuItem_Click);
            // 
            // topHatToolStripMenuItem
            // 
            this.topHatToolStripMenuItem.Name = "topHatToolStripMenuItem";
            this.topHatToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.topHatToolStripMenuItem.Text = "TopHat";
            this.topHatToolStripMenuItem.Click += new System.EventHandler(this.topHatToolStripMenuItem_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(393, 63);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(395, 375);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // blackHatToolStripMenuItem
            // 
            this.blackHatToolStripMenuItem.Name = "blackHatToolStripMenuItem";
            this.blackHatToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.blackHatToolStripMenuItem.Text = "BlackHat";
            this.blackHatToolStripMenuItem.Click += new System.EventHandler(this.blackHatToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem operationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tresholdingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem binaryzationToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ToolStripMenuItem aDAPTIVEToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem posterizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rozcioganieZakresuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem laby3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wygladzanieLinioweToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gussianBlurToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medianBlurToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cannyDetectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobelDetectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem laplaceDetectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageSharpeningWithLaplacianFitlerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detekcjaKrawedziPrewittToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zad3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blendingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem myStuffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem erosionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dialationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gradientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topHatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blackHatToolStripMenuItem;
    }
}

