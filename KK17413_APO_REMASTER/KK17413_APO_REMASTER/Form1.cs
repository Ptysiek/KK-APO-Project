using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;


namespace KK17413_APO_REMASTER
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void binaryzationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            Image<Gray, byte> gray = image.Convert<Gray, byte>();

            // Making image with black background:
            Image<Gray, byte>  binarize = new Image<Gray, byte>(gray.Width, gray.Height, new Gray(0));

            CvInvoke.Threshold(gray, binarize, 50, 150, Emgu.CV.CvEnum.ThresholdType.Binary);

            pictureBox1.Image = image.Bitmap;
            pictureBox2.Image = binarize.Bitmap;
        }
    }
}
