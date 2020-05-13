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
    class Histogram_Reload
    {
        public static ImageData GetResult(ImageData oldOne, ref ProgressBar pbar)
        {
            if (oldOne.Ready)
                return oldOne;

            pbar.Value = 0;
            pbar.Visible = true;

            Bitmap bitmap = oldOne.Bitmap;
            ImageData result = new ImageData();

            for (int h = 0; h < bitmap.Height; ++h)
            {
                pbar.Value = (h * 100 / bitmap.Height);

                for (int w = 0; w < bitmap.Width; ++w)
                {
                    result.data.SumUp(bitmap.GetPixel(w, h).R);
                    result.data.SumUp(bitmap.GetPixel(w, h).G);
                    result.data.SumUp(bitmap.GetPixel(w, h).B);

                    result.data_A.SumUp(bitmap.GetPixel(w, h).A);
                    result.data_R.SumUp(bitmap.GetPixel(w, h).R);
                    result.data_G.SumUp(bitmap.GetPixel(w, h).G);
                    result.data_B.SumUp(bitmap.GetPixel(w, h).B);
                }
            }
            pbar.Value = 90;

            result.data.SetLeast();
            result.data_A.SetLeast();
            result.data_R.SetLeast();
            pbar.Value = 96;
            result.data_G.SetLeast();
            result.data_B.SetLeast();

            pbar.Value = 100;
            pbar.Visible = false;

            return result;
        }
    }
}
