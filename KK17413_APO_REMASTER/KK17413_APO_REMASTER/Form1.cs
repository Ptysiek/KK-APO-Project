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
using Emgu.CV.Cuda;
using System.Drawing.Imaging;

namespace KK17413_APO_REMASTER
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
        }

        // ----------------------------------------------------------------------------------------------------------
        #region lab2
        private void binaryzationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // PL
            // Progowanie

            // ANG
            // Thresholding


            // Given Arguments: 
            // - Currant bitmap

            /*// Working EmguCV version:
            int p1 = 50;
            int p2 = 150;

            Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            Image<Gray, byte> gray = image.Convert<Gray, byte>();

            // Making image with black background:
            Image<Gray, byte>  binarize = new Image<Gray, byte>(gray.Width, gray.Height, new Gray(0));

            CvInvoke.Threshold(gray, binarize, p1, p2, Emgu.CV.CvEnum.ThresholdType.Binary);

            pictureBox1.Image = image.Bitmap;
            pictureBox2.Image = binarize.Bitmap;
            //*/


            //* Reczne Wersion
            int p1 = 50;

            int lmin = 0;
            int lmax = 0;

            //Bitmap img = new Bitmap("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            Image<Gray, byte> gray = image.Convert<Gray, byte>();
            Bitmap img = gray.Bitmap;

            for (int i = 0; i < img.Width; ++i)
            {
                for (int j = 0; j < img.Height; ++j)
                {
                    Color val = img.GetPixel(i, j);
                    
                    if (val.R > 0)
                    {
                        if (lmin == 0)
                        {
                            lmin = val.R;
                        }
                        else if (val.R < lmin)
                        {
                            lmin = val.R;
                        }

                        if (lmax == 0)
                        {
                            lmax = val.R;
                        }
                        else if (val.R > lmax)
                        {
                            lmax = val.R;
                        }
                    }
                }
            }

            Bitmap result = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);

            for (int i = 0; i < img.Width; ++i)
            {
                for (int j = 0; j < img.Height; ++j)
                {
                    Color val = img.GetPixel(i, j);

                    result.SetPixel(i, j, Color.FromArgb((val.R <= p1) ? lmin : lmax,
                                                         (val.G <= p1) ? lmin : lmax,
                                                         (val.B <= p1) ? lmin : lmax));
                }
            }
            pictureBox1.Image = img;
            pictureBox2.Image = result;
            //*/

            /*  TO DO:
                - Zrób osobny form na którym widać histogram obecnego obrazka
                - Wybieraj zakres na tym histogramie - wartości 50, 150
                
                - Daj opis na topie forma, co dana operacja robi.
                  Opis musi zmieniać język.Adaptive ThresholdingAdaptive Thresholding


                - odświeżaj prawdziwy obrazek zgodnie z tym co ustawisz na histogramie forma.
                  Ale go nie modyfikuj. Modyfikuj dopiero po przycisku ok lub aply.

                - Emgu.CV.CvEnum.ThresholdType posiada co najmeniej 4 inne tryby.
                  Pozwól przełączać między nimi w tym samym formie do tej operacji.

                - zrobić bez opencv

                https://www.youtube.com/watch?v=KpCQp_rd-Nk&list=PLUSwCY_ybvyLcNxZ1Q3vCkaCH9rjrRxA6&index=38
             */
        }

        private void aDAPTIVEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            /*// Working EmguCV version:
            Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            Image<Gray, byte> gray = image.Convert<Gray, byte>();

            // Making image with black background:
            Image<Gray, byte> binarize = new Image<Gray, byte>(gray.Width, gray.Height, new Gray(0));

            CvInvoke.AdaptiveThreshold(gray, binarize, 255, Emgu.CV.CvEnum.AdaptiveThresholdType.MeanC, Emgu.CV.CvEnum.ThresholdType.Binary, 51, -50.50);

            pictureBox1.Image = image.Bitmap;
            pictureBox2.Image = binarize.Bitmap;
            //*/

            // PL
            // Progowanie Obrazu z zachowaniem poziomów szarości
            // Zmienia najpierw na obraz szaroodcieniowy, następnie go proguje.

            // ANG
            // Adaptive Thresholding

            //* Reczne Wersion
            int p1 = 50;
            int p2 = 150;

            //Bitmap img = new Bitmap("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            Image<Gray, byte> gray = image.Convert<Gray, byte>();
            Bitmap img = gray.Bitmap;   

            Bitmap result = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);

            for (int i = 0; i < img.Width; ++i)
            {
                for (int j = 0; j < img.Height; ++j)
                {
                    Color val = img.GetPixel(i, j);
                    
                    result.SetPixel(i, j, Color.FromArgb((val.R >= p1 && val.R <= p2) ? val.R : 0,
                                                         (val.G >= p1 && val.G <= p2) ? val.R : 0,
                                                         (val.B >= p1 && val.B <= p2) ? val.R : 0));                                                        
                }
            }          
            pictureBox1.Image = img;
            pictureBox2.Image = result;
            //*/


            /*  TO DO:
                - Pierwssze 255 to max value, zobacz co będzie jak zmenijszysz  
             
                - AdaptiveThresholdType.GaussianC
                    posiada również meanC - sprawdź!

                - Podobnie jak w poprzednim,binaryzationToolStripMenuItem_Click,  zobacz różnice z ThresholdType.Binary

                - ostatnie 5, to block size
                - ostatnie 0.0 to param

                - zrobić bez opencv

                https://www.youtube.com/watch?v=Bjtg0RFm6po&list=PLUSwCY_ybvyLcNxZ1Q3vCkaCH9rjrRxA6&index=39
             */
        }

        private void posterizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Given Arguments: 
            // - Currant bitmap
            // - int level

            // PL
            // Posteryzacja Obrazu
            // Operacja redukcji poziomów szarości 

            // ANG
            // Image Posterize
            // Conversion of a continuous gradation of tone to several regions of fewer tones.

            Bitmap img = new Bitmap("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            //Bitmap img = new Bitmap("C:\\Users\\kptyc\\Desktop\\ScuiF.jpg");

            double levels = 2.0; // Posterize level two;
            levels--;
            double sr, sg, sb;
            int dr, dg, db;

            Bitmap result = new Bitmap(img.Width, img.Height, img.PixelFormat);

            for (int i = 0; i < img.Width; ++i)
            {
                for (int j = 0; j < img.Height; ++j)
                {
                    Color val = img.GetPixel(i, j);

                    sr = val.R / 255.0;
                    sg = val.G / 255.0;
                    sb = val.B / 255.0;

                    dr = (int)(255 * Math.Round(sr * levels) / levels);
                    dg = (int)(255 * Math.Round(sg * levels) / levels);
                    db = (int)(255 * Math.Round(sb * levels) / levels);
                    result.SetPixel(i, j, Color.FromArgb(dr, dg, db));
                }
            }

            pictureBox1.Image = img;
            pictureBox2.Image = result;
        }

        private void rozcioganieZakresuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // PASS
        }



        #endregion
        // ----------------------------------------------------------------------------------------------------------
        #region lab3
        private void wygladzanieLinioweToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //wygładzania liniowego oparte na typowych maskach wygładzania (blur, gaussianBlur)
            Size k = new Size (20,2);
            Point anchor = new Point(-1, -1);

            //* Gray Version
            Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            Image<Gray, byte> gray = image.Convert<Gray, byte>();

            Image<Gray, byte> blur = new Image<Gray, byte>(gray.Width, gray.Height, new Gray(0));
            CvInvoke.Blur(gray, blur, k, anchor);
            //*/

            /* Color Version
            Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            Image<Bgra, byte> blur = new Image<Bgra, byte>(image.Width, image.Height);

            CvInvoke.Blur(image, blur, k, anchor);
            //*/

            pictureBox1.Image = image.Bitmap;
            pictureBox2.Image = blur.Bitmap;
        }




        #endregion
        // ----------------------------------------------------------------------------------------------------------
    }
}
