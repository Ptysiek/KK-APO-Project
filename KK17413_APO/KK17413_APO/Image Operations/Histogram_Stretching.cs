using System.Drawing;
using KK17413_APO.Data_Structures;
using System.Collections.Generic;


namespace KK17413_APO.Image_Operations
{
    [System.ComponentModel.DesignerCategory("")]
    public class Histogram_Stretching
    {
        public static ImageData GetResult(ImageData before)
        {
            before.RecalculateHistograms();

            List<int> LUTred = calculateLUT(before.data_R);
            List<int> LUTgreen = calculateLUT(before.data_G);
            List<int> LUTblue = calculateLUT(before.data_B);

            HistogramData general = new HistogramData();
            HistogramData red = new HistogramData();
            HistogramData green = new HistogramData();
            HistogramData blue = new HistogramData();

            Bitmap oldBitmap = before.Bitmap;
            Bitmap newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height, before.Bitmap.PixelFormat);

            for (int h = 0; h < oldBitmap.Height; ++h)
            {
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

            return after;
        }

        private static List<int> calculateLUT(HistogramData data)
        {
            double a = 255.0 / (data.maxValue - data.minValue);

            List<int> result = new List<int>(new int[256]);

            for (int i = 0; i < 256; ++i)
            {
                result[i] = (int)(a * (i - data.minValue));
            }

            return result;
        }
    }
}