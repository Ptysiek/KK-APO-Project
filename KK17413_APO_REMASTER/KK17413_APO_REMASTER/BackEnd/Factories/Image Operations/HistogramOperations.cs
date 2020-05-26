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
            if (service.data.Last() == null)
                return null;                

            if (service.data.Last().Bitmap == null)
                return null;

            if (service.data.Last().Ready)
                return null;

            service.imageWindow.StartProgressBar();
            Bitmap bitmap = service.data.Last().Bitmap;
            ImageData updatedData = service.data.Last();

            for (int h = 0; h < bitmap.Height; ++h)
            {
                service.imageWindow.SetProgressBarValue(h * 100 / bitmap.Height);

                for (int w = 0; w < bitmap.Width; ++w)
                {
                    updatedData.data.SumUp(bitmap.GetPixel(w, h).R);
                    updatedData.data.SumUp(bitmap.GetPixel(w, h).G);
                    updatedData.data.SumUp(bitmap.GetPixel(w, h).B);

                    updatedData.data_A.SumUp(bitmap.GetPixel(w, h).A);
                    updatedData.data_R.SumUp(bitmap.GetPixel(w, h).R);
                    updatedData.data_G.SumUp(bitmap.GetPixel(w, h).G);
                    updatedData.data_B.SumUp(bitmap.GetPixel(w, h).B);
                }
            }

            updatedData.data.SetLeast();
            updatedData.data_A.SetLeast();
            updatedData.data_R.SetLeast();
            updatedData.data_G.SetLeast();
            updatedData.data_B.SetLeast();

            updatedData.SetReady();
            service.data.UpdateLast(updatedData);
            service.imageWindow.CloseProgressBar();

            return updatedData;
        }
    }

    
    public class Histogram_Stretching : IOperation
    {
        public override ImageData GetResult(ImageForm_Service service)
        {
            if (service.data.Last() == null)
                return null;                

            if (service.data.Last().Bitmap == null)
                return null;

            if (!service.data.Last().Ready)            
                new RecalculateHistogramData().GetResult(service);            

            if (!DemandTest(service.data.Last()))
                return null;

            List<int> LUTred = CalculateLUT(service.data.Last().data_R);
            List<int> LUTgreen = CalculateLUT(service.data.Last().data_G);
            List<int> LUTblue = CalculateLUT(service.data.Last().data_B);

            HistogramData general = new HistogramData();
            HistogramData red = new HistogramData();
            HistogramData green = new HistogramData();
            HistogramData blue = new HistogramData();

            service.imageWindow.StartProgressBar();
            Bitmap oldBitmap = service.data.Last().Bitmap;
            Bitmap newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height, service.data.Last().Bitmap.PixelFormat);

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

            ImageData after = new ImageData(newBitmap, service.data.Last().ID)
            {
                data = general,
                data_A = service.data.Last().data_A,
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












}


