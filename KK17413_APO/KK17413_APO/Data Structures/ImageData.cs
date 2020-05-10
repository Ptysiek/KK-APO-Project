﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK17413_APO.Data_Structures
{
    class ImageData
    {
        public string ID;
        public HistogramData data;
        public HistogramData data_A;
        public HistogramData data_R;
        public HistogramData data_G;
        public HistogramData data_B;

        private Bitmap bitmap;

        public ImageData(Bitmap bitmap)
        {
            this.bitmap = bitmap;

            data = new HistogramData();
            data_A = new HistogramData();
            data_R = new HistogramData();
            data_G = new HistogramData();
            data_B = new HistogramData();
        }


        public void RecalculateHistograms()
        {
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
        }
    }

}

