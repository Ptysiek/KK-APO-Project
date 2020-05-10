using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK17413_APO.Data_Structures
{
    public class ImageData
    {
        public bool Ready { get => ready; }
        public Bitmap Bitmap { get => bitmap; }

        readonly public string ID;
        public HistogramData data;
        public HistogramData data_A;
        public HistogramData data_R;
        public HistogramData data_G;
        public HistogramData data_B;

        private Bitmap bitmap;
        private bool ready;


        public ImageData(Bitmap bitmap, string filename)
        {
            this.bitmap = bitmap;
            this.ID = filename;

            data = new HistogramData();
            data_A = new HistogramData();
            data_R = new HistogramData();
            data_G = new HistogramData();
            data_B = new HistogramData();

            ready = false;
        }

        public void AssignBitmap(Bitmap bitmap)
        {
            this.bitmap = bitmap;
            ready = false;
        }

        public void RecalculateHistograms()
        {
            if (ready)
                return;

            for (int h = 0; h < bitmap.Height; ++h)
            {
                for (int w = 0; w < bitmap.Width; ++w)
                {
                    data.SumUp(bitmap.GetPixel(w, h).R);
                    data.SumUp(bitmap.GetPixel(w, h).G);
                    data.SumUp(bitmap.GetPixel(w, h).B);

                    data_A.SumUp(bitmap.GetPixel(w, h).A);
                    data_R.SumUp(bitmap.GetPixel(w, h).R);
                    data_G.SumUp(bitmap.GetPixel(w, h).G);
                    data_B.SumUp(bitmap.GetPixel(w, h).B);
                }
            }

            data.SetLeast();
            data_A.SetLeast();
            data_R.SetLeast();
            data_G.SetLeast();
            data_B.SetLeast();

            ready = true;
        }
    }

}

