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

            /*  TO DO:
                - Zrób osobny form na którym widać histogram obecnego obrazka
                - Wybieraj zakres na tym histogramie - wartości 50, 150
                
                - Daj opis na topie forma, co dana operacja robi.
                  Opis musi zmieniać język.Adaptive ThresholdingAdaptive Thresholding


                - odświeżaj prawdziwy obrazek zgodnie z tym co ustawisz na histogramie forma.
                  Ale go nie modyfikuj. Modyfikuj dopiero po przycisku ok lub aply.

                - Emgu.CV.CvEnum.ThresholdType posiada co najmeniej 4 inne tryby.
                  Pozwól przełączać między nimi w tym samym formie do tej operacji.

                https://www.youtube.com/watch?v=KpCQp_rd-Nk&list=PLUSwCY_ybvyLcNxZ1Q3vCkaCH9rjrRxA6&index=38
             */
        }

        private void adaptiveThresholdingToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void tresholdingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void adaptiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            Image<Gray, byte> gray = image.Convert<Gray, byte>();

            // Making image with black background:
            Image<Gray, byte> binarize = new Image<Gray, byte>(gray.Width, gray.Height, new Gray(0));

            CvInvoke.AdaptiveThreshold(gray, binarize, 255, Emgu.CV.CvEnum.AdaptiveThresholdType.MeanC, Emgu.CV.CvEnum.ThresholdType.Binary, 51, -50.0);

            pictureBox1.Image = image.Bitmap;
            pictureBox2.Image = binarize.Bitmap;

            /*  TO DO:
                - Pierwssze 255 to max value, zobacz co będzie jak zmenijszysz  
             
                - AdaptiveThresholdType.GaussianC
                    posiada również meanC - sprawdź!

                - Podobnie jak w poprzednim,binaryzationToolStripMenuItem_Click,  zobacz różnice z ThresholdType.Binary

                - ostatnie 5, to block size
                - ostatnie 0.0 to param

                https://www.youtube.com/watch?v=Bjtg0RFm6po&list=PLUSwCY_ybvyLcNxZ1Q3vCkaCH9rjrRxA6&index=39
             */
        }
    }
}
