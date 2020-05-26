using System;
using System.Collections.Generic;
using System.Drawing;

using KK17413_APO_REMASTER.BackEnd.DataStructures;


namespace KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations
{
    class LogicalOperations : OperationsFamily
    {
        public LogicalOperations()
        {
            operations_Dict = new Dictionary<string, IOperation>()
            {
                { "Negation_tsmi", new Negation() }
            };
        }
    }



    public class Negation : IOperation
    {
        public override ImageData GetResult(ImageForm_Service service)
        {
            if (service.data.LastData() == null)
                return null;

            if (service.data.LastData().Bitmap == null)
                return null;

            //if (service.data.LastData().Ready)
            //    return null;


            //if (!before.Ready)
            //    before.RecalculateHistograms(ref pbar);

            service.imageWindow.StartProgressBar();

            Bitmap bitmap = service.data.LastData().Bitmap;
            ImageData result = new ImageData();

            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height, bitmap.PixelFormat);

            for (int h = 0; h < bitmap.Height; ++h)
            {
                service.imageWindow.SetProgressBarValue(h * 100 / bitmap.Height);

                for (int w = 0; w < bitmap.Width; ++w)
                {
                    Color newPixel = Color.FromArgb(bitmap.GetPixel(w, h).A, (255 - bitmap.GetPixel(w, h).R), (255 - bitmap.GetPixel(w, h).G), (255 - bitmap.GetPixel(w, h).B));
                    newBitmap.SetPixel(w, h, newPixel);

                    result.data.SumUp(newPixel.R);
                    result.data.SumUp(newPixel.G);
                    result.data.SumUp(newPixel.B);

                    result.data_A.SumUp(newPixel.A);
                    result.data_R.SumUp(newPixel.R);
                    result.data_G.SumUp(newPixel.G);
                    result.data_B.SumUp(newPixel.B);
                }
            }
            result.data.SetLeast();
            result.data_A.SetLeast();
            result.data_R.SetLeast();
            result.data_G.SetLeast();
            result.data_B.SetLeast();
            result.Bitmap = newBitmap;

            result.SetReady();
            service.imageWindow.CloseProgressBar();

            return result;
        }
    }
}