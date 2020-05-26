﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;
using KK17413_APO_REMASTER.BackEnd.DataStructures;


namespace KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations
{
    class ThresholdingOperations : IOperationsFamily
    {
        public ThresholdingOperations()
        {
            operations_Dict = new Dictionary<string, IOperation>()
            {
                { "BinaryThresholding_tsmi", new BinaryThresholding() }
            };
        }
    }


    public class BinaryThresholding : IOperation
    {
        public override ImageData GetResult(ImageForm_Service service)
        {
            if (service.data.LastData() == null)
                return null;

            if (service.data.LastData().Bitmap == null)
                return null;

            if (service.data.LastData().Ready)
                return null;

            int p1 = 50;

            int lmin = 0;
            int lmax = 0;

            //Bitmap img = new Bitmap("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            // Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");

            service.imageWindow.StartProgressBar();

            Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);
            Image<Gray, byte> gray = image.Convert<Gray, byte>();
            Bitmap img = gray.Bitmap;

            for (int i = 0; i < img.Width; ++i)
            {
                service.imageWindow.SetProgressBarValue(i * 100 / img.Width);

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

            Bitmap result = new Bitmap(img.Width, img.Height, service.data.LastData().Bitmap.PixelFormat);

            for (int i = 0; i < img.Width; ++i)
            {
                service.imageWindow.SetProgressBarValue(i * 100 / img.Width);

                for (int j = 0; j < img.Height; ++j)
                {
                    Color val = img.GetPixel(i, j);

                    result.SetPixel(i, j, Color.FromArgb((val.R <= p1) ? lmin : lmax,
                                                         (val.G <= p1) ? lmin : lmax,
                                                         (val.B <= p1) ? lmin : lmax));
                }
            }

            service.imageWindow.CloseProgressBar();
            return new ImageData(result, service.data.LastData().ID);
        }
    }
}


          //  private void binaryzationToolStripMenuItem_Click(object sender, EventArgs e)
//{
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
Image<Gray, byte> binarize = new Image<Gray, byte>(gray.Width, gray.Height, new Gray(0));

CvInvoke.Threshold(gray, binarize, p1, p2, Emgu.CV.CvEnum.ThresholdType.Binary);

    pictureBox1.Image = image.Bitmap;
    pictureBox2.Image = binarize.Bitmap;
    //*/


    //* Reczne Wersion
    
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

