using KK17413_APO_REMASTER.BackEnd.DataStructures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations
{
    class HistogramOperations : OperationsFamily
    {
        public HistogramOperations()
        {
            operations_Dict = new Dictionary<string, IOperation>()
            {
                { "RecalculateHistogramData_tsmi", new RecalculateHistogramData() },
                { "Histogram_Stretching_tsmi", new Histogram_Stretching()  }
            };
        }
    }



    public class RecalculateHistogramData : IOperation
    {
        public override ImageData GetResult(ImageForm_Service service)
        {
            if (service.data.LastData() == null)
                return null;                

            if (service.data.LastData().Bitmap == null)
                return null;

            if (service.data.LastData().Ready)
                return null;

            service.imageWindow.StartProgressBar();
            Bitmap bitmap = service.data.LastData().Bitmap;
            ImageData newImageData = service.data.LastData();

            for (int h = 0; h < bitmap.Height; ++h)
            {
                service.imageWindow.SetProgressBarValue(h * 100 / bitmap.Height);

                for (int w = 0; w < bitmap.Width; ++w)
                {
                    newImageData.data.SumUp(bitmap.GetPixel(w, h).R);
                    newImageData.data.SumUp(bitmap.GetPixel(w, h).G);
                    newImageData.data.SumUp(bitmap.GetPixel(w, h).B);

                    newImageData.data_A.SumUp(bitmap.GetPixel(w, h).A);
                    newImageData.data_R.SumUp(bitmap.GetPixel(w, h).R);
                    newImageData.data_G.SumUp(bitmap.GetPixel(w, h).G);
                    newImageData.data_B.SumUp(bitmap.GetPixel(w, h).B);
                }
            }
            newImageData.data.SetLeast();
            newImageData.data_A.SetLeast();
            newImageData.data_R.SetLeast();
            newImageData.data_G.SetLeast();
            newImageData.data_B.SetLeast();

            newImageData.SetReady();

            service.imageWindow.CloseProgressBar();

            //service.data.UpdateLastData(newImageData);
            //service.imageWindow.ReloadImageData_All(service.data.Last().data);
            //service.imageWindow.ReloadModificationsList(service.data.modifications);
            return newImageData;
        }
    }

    
    public class Histogram_Stretching : IOperation
    {
        public override ImageData GetResult(ImageForm_Service service)
        {
            if (service.data.LastData() == null)
                return null;                

            if (service.data.LastData().Bitmap == null)
                return null;

            if (!service.data.LastData().Ready)
                service.ImageOperation("RecalculateHistogramData_tsmi");

            if (!DemandTest(service.data.LastData()))
                return null;

            List<int> LUTred = CalculateLUT(service.data.LastData().data_R);
            List<int> LUTgreen = CalculateLUT(service.data.LastData().data_G);
            List<int> LUTblue = CalculateLUT(service.data.LastData().data_B);

            HistogramData general = new HistogramData();
            HistogramData red = new HistogramData();
            HistogramData green = new HistogramData();
            HistogramData blue = new HistogramData();

            service.imageWindow.StartProgressBar();
            Bitmap oldBitmap = service.data.LastData().Bitmap;
            Bitmap newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height, service.data.LastData().Bitmap.PixelFormat);

            for (int h = 0; h < oldBitmap.Height; ++h)
            {
                service.imageWindow.SetProgressBarValue(h * 100 / oldBitmap.Height);

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

            ImageData after = new ImageData(newBitmap, service.data.LastData().ID)
            {
                data = general,
                data_A = service.data.LastData().data_A,
                data_R = red,
                data_G = green,
                data_B = blue
            };
            after.SetReady();

            service.imageWindow.CloseProgressBar();

            return after;
        }

        private static List<int> CalculateLUT(HistogramData data)
        {
            double a = 255.0 / (data.maxValue - data.minValue);

            List<int> result = new List<int>(new int[256]);

            for (int i = 0; i < 256; ++i)
            {
                result[i] = (int)(a * (i - data.minValue));
            }

            return result;
        }

        private static bool DemandTest(ImageData data)
        {
            if (data.data.minValue != 0) return true;
            if (data.data.maxValue != 255) return true;

            if (data.data_R.minValue != 0) return true;
            if (data.data_R.maxValue != 255) return true;

            if (data.data_G.minValue != 0) return true;
            if (data.data_G.maxValue != 255) return true;

            if (data.data_B.minValue != 0) return true;
            if (data.data_B.maxValue != 255) return true;

            return false;
        }
    }




    /*
    public static Bitmap EqualGray(Bitmap bitmap)
        {
            Bitmap newBitmap = new Bitmap(bitmap);

            Dictionary<Color, int> map = Tools.HistogramMap(bitmap);
            int[] GrayLut = Tools.HistogramLUT(map);
            double[] D = new double[GrayLut.Length];
            int sum = 0;

            foreach (var x in GrayLut) { sum += x; }

            for (int i = 0; i < GrayLut.Length; ++i)
            {
                for (int j = 0; j < i; ++j)
                {
                    D[i] += GrayLut[j];
                }
                D[i] = D[i] / sum;
            }

            double D0 = 0;
            for (int i = D.Length - 1; i > 0; --i)
            {
                if (D[i] != 0) { D0 = D[i]; }
            }

            // ta tablica to wskażnik przejscia na nowy kolor / wartośc koloru!!! 

            Dictionary<int, int> LUT = new Dictionary<int, int>();

            for (int i = 0; i < D.Length; ++i)
            {
                LUT.Add(i, (int)(((D[i] - D0) / (1 - D0)) * (256 - 1)));
            }

            for (int x = 0; x < bitmap.Width; ++x)
            {
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    Color newColor = Color.FromArgb(LUT[bitmap.GetPixel(x, y).R], LUT[bitmap.GetPixel(x, y).R], LUT[bitmap.GetPixel(x, y).R]);
                    newBitmap.SetPixel(x, y, newColor);
                }
            }
            return newBitmap;
        }

    */



    /*

    public static Bitmap SelectiveEqualGray(Bitmap bitmap)
        {
            Bitmap newBitmap = new Bitmap(bitmap);

            Dictionary<Color, int> map = Tools.HistogramMap(bitmap);
            int[] GrayLut = Tools.HistogramLUT(map);
            double[] D = new double[GrayLut.Length];
            int sum = 0;

            foreach (var x in GrayLut) { sum += x; }

            for (int i = 0; i < GrayLut.Length; ++i)
            {
                for (int j = 0; j < i; ++j)
                {
                    D[i] += GrayLut[j];
                }
                D[i] = D[i] / sum;
            }

            double D0 = 0;
            for (int i = D.Length - 1; i > 0; --i)
            {
                if (D[i] != 0) { D0 = D[i]; }
            }

            // ta tablica to wskażnik przejscia na nowy kolor / wartośc koloru!!! 

            Dictionary<int, int> LUT = new Dictionary<int, int>();

            for (int i = 0; i < D.Length; ++i)
            {
                LUT.Add(i, (int)Math.Ceiling(255 * D[i])); // ew zamiast 5 - 255
            }

            for (int x = 0; x < bitmap.Width; ++x)
            {
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    Color newColor = Color.FromArgb(LUT[bitmap.GetPixel(x, y).R], LUT[bitmap.GetPixel(x, y).R], LUT[bitmap.GetPixel(x, y).R]);
                    newBitmap.SetPixel(x, y, newColor);
                }
            }
            return newBitmap;
        }

    */
}


