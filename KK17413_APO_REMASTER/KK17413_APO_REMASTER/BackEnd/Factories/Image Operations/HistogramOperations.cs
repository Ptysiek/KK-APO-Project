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
                { "stuff", null }
            };
        }
    }



    public class RecalculateHistogramData : IOperation
    {
        public override ImageData GetResult(ImageForm_Service service)
        {
            if (service.data.Last() == null)
                return null;                

            if (service.data.Last().Ready)
                return null;

            Bitmap bitmap = service.data.Last().Bitmap;
            ImageData newData = new ImageData();

            for (int h = 0; h < bitmap.Height; ++h)
            {
                service.imageWindow.SetProgressBarValue(h * 100 / bitmap.Height);

                for (int w = 0; w < bitmap.Width; ++w)
                {
                    newData.data.SumUp(bitmap.GetPixel(w, h).R);
                    newData.data.SumUp(bitmap.GetPixel(w, h).G);
                    newData.data.SumUp(bitmap.GetPixel(w, h).B);

                    newData.data_A.SumUp(bitmap.GetPixel(w, h).A);
                    newData.data_R.SumUp(bitmap.GetPixel(w, h).R);
                    newData.data_G.SumUp(bitmap.GetPixel(w, h).G);
                    newData.data_B.SumUp(bitmap.GetPixel(w, h).B);
                }
            }

            newData.data.SetLeast();
            newData.data_A.SetLeast();
            newData.data_R.SetLeast();
            newData.data_G.SetLeast();
            newData.data_B.SetLeast();

            newData.SetReady();

            return newData;
        }
    }
}


