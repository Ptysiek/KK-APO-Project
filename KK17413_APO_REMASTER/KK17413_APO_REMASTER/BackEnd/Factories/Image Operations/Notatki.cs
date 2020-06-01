﻿#pragma warning disable
using System;
using System.Drawing;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing.Imaging;


namespace KK17413_APO_REMASTER
{
    public partial class Form1 : Form
    {
        public PictureBox pictureBox1 = new PictureBox();
        public PictureBox pictureBox2 = new PictureBox();
        
        
        // ------------------------------------------------------------------------------------------------------
        #region MyStuff



        private void BlackHatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            Image<Gray, byte> gray = image.Convert<Gray, byte>();

            Size ksize = new Size(5, 5);
            Point anchor = new Point(-1, -1);

            Mat kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, ksize, anchor);

            pictureBox1.Image = gray.Bitmap;
            pictureBox2.Image = gray.MorphologyEx(Emgu.CV.CvEnum.MorphOp.Blackhat, kernel, anchor, 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1.0)).Bitmap;
        }
        #endregion
        // ------------------------------------------------------------------------------------------------------
    }

}
#pragma warning restore
