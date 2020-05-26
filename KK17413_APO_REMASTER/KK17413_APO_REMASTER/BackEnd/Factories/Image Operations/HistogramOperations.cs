using System;
using System.Collections.Generic;
using System.Drawing;

using KK17413_APO_REMASTER.BackEnd.DataStructures;


namespace KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations
{
    class HistogramOperations : IOperationsFamily
    {
        public HistogramOperations()
        {
            operations_Dict = new Dictionary<string, IOperation>()
            {
                { "RecalculateHistogramData_tsmi", new RecalculateHistogramData() },
                { "Histogram_Stretching_tsmi", new Histogram_Stretching() },
                { "Histogram_Equalization_tsmi", new Histogram_Equalization() },
                { "Histogram_SelectiveEqualization_tsmi", new Histogram_SelectiveEqualization() }
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


    public class Histogram_Equalization : IOperation
    {
        public override ImageData GetResult(ImageForm_Service service)
        {
            if (service.data.LastData() == null)
                return null;

            if (service.data.LastData().Bitmap == null)
                return null;

            if (!service.data.LastData().Ready)
                service.ImageOperation("RecalculateHistogramData_tsmi");


            Bitmap oldBitmap = service.data.LastData().Bitmap;
            Bitmap newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height, service.data.LastData().Bitmap.PixelFormat);

            List<int> LUTred = CalculateLUT(service.data.LastData().data_R, oldBitmap.Width * oldBitmap.Height);
            List<int> LUTgreen = CalculateLUT(service.data.LastData().data_G, oldBitmap.Width * oldBitmap.Height);
            List<int> LUTblue = CalculateLUT(service.data.LastData().data_B, oldBitmap.Width * oldBitmap.Height);

            HistogramData general = new HistogramData();
            HistogramData red = new HistogramData();
            HistogramData green = new HistogramData();
            HistogramData blue = new HistogramData();

            service.imageWindow.StartProgressBar();

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

        private static List<int> CalculateLUT(HistogramData data, int size)
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


    public class Histogram_SelectiveEqualization : IOperation
    {
        public override ImageData GetResult(ImageForm_Service service)
        {
            if (service.data.LastData() == null)
                return null;

            if (service.data.LastData().Bitmap == null)
                return null;

            if (!service.data.LastData().Ready)
                service.ImageOperation("RecalculateHistogramData_tsmi");


            Bitmap oldBitmap = service.data.LastData().Bitmap;
            Bitmap newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height, service.data.LastData().Bitmap.PixelFormat);

            List<int> LUTred = CalculateLUT(service.data.LastData().data_R);
            List<int> LUTgreen = CalculateLUT(service.data.LastData().data_G);
            List<int> LUTblue = CalculateLUT(service.data.LastData().data_B);

            HistogramData general = new HistogramData();
            HistogramData red = new HistogramData();
            HistogramData green = new HistogramData();
            HistogramData blue = new HistogramData();

            service.imageWindow.StartProgressBar();

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
            List<int> result = new List<int>(new int[256]);
            double[] D = new double[256];

            double sum = 0;
            foreach (var x in data.data) { sum += x; }

            for (int i = 0; i < 256; ++i)
            {
                for (int j = 0; j < i; ++j)
                {
                    D[i] += data.data[j];
                }
                D[i] = D[i] / sum;
            }

            for (int i = 0; i < 256; ++i)
            {
                result[i] = (int)Math.Ceiling(255 * D[i]);
            }

            return result;
        }
    }
}

