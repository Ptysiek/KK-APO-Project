using KK17413_APO.Data_Structures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KK17413_APO.Image_Operations
{
    class Histogram_Equalization
    {
        public static ImageData GetResult(ImageData before, ref ProgressBar pbar)
        {

            if (!before.Ready)
                before.RecalculateHistograms(ref pbar);

            Bitmap oldBitmap = before.Bitmap;
            Bitmap newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height, before.Bitmap.PixelFormat);

            List<int> LUTred = calculateLUT(before.data_R, oldBitmap.Width * oldBitmap.Height);
            List<int> LUTgreen = calculateLUT(before.data_G, oldBitmap.Width * oldBitmap.Height);
            List<int> LUTblue = calculateLUT(before.data_B, oldBitmap.Width * oldBitmap.Height);

            HistogramData general = new HistogramData();
            HistogramData red = new HistogramData();
            HistogramData green = new HistogramData();
            HistogramData blue = new HistogramData();

            pbar.Value = 0;
            pbar.Visible = true;


            //Tablice LUT dla skladowych
            //int[] LUTred = calculateLUT(red, picture.Image.Width * picture.Image.Height);
            //int[] LUTgreen = calculateLUT(green, picture.Image.Width * picture.Image.Height);
            //int[] LUTblue = calculateLUT(blue, picture.Image.Width * picture.Image.Height);

            //Przetworz obraz i oblicz nowy histogram
            //red = new int[256];
            //green = new int[256];
            //blue = new int[256];
            //Bitmap oldBitmap = (Bitmap)picture.Image;
            //Bitmap newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height, PixelFormat.Format24bppRgb);



            for (int h = 0; h < oldBitmap.Height; ++h)
            {
                pbar.Value = h * 100 / oldBitmap.Height;

                for (int w = 0; w < oldBitmap.Width; ++w)
                {
                    Color pixel = oldBitmap.GetPixel(w, h);
                    Color newPixel = Color.FromArgb(pixel.A, LUTred[pixel.R], LUTgreen[pixel.G], LUTblue[pixel.B]);
                    newBitmap.SetPixel(w, h, newPixel);

                    general.SumUp(newPixel.R);
                    general.SumUp(newPixel.G);
                    general.SumUp(newPixel.B);

                    red.SumUp(newPixel.R);
                    green.SumUp(newPixel.G);
                    blue.SumUp(newPixel.B);
                }
            }
            general.SetLeast();
            red.SetLeast();
            green.SetLeast();
            blue.SetLeast();

            ImageData after = new ImageData(newBitmap, before.ID)
            {
                data = general,
                data_A = before.data_A,
                data_R = red,
                data_G = green,
                data_B = blue
            };

            pbar.Value = 100;
            pbar.Visible = false;
            return after;
        }



        private static List<int> calculateLUT(HistogramData data, int size)
        {
            double minValue = data.minValue;
            List<int> result = new List<int>(new int[256]);

            double sum = 0;
            for (int i = 0; i < 256; i++)
            {
                sum += data.data[i];
                result[i] = (int)(((sum - minValue) / (size - minValue)) * 255.0);
            }

            return result;
        }
    }
}
