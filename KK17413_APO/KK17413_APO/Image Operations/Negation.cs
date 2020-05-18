using KK17413_APO.Data_Structures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KK17413_APO.Image_Operations
{
    class Negation
    {
        public static ImageData GetResult(ImageData before, ref ProgressBar pbar)
        {
            if (!before.Ready)
                before.RecalculateHistograms(ref pbar);

            pbar.Value = 0;
            pbar.Visible = true;

            Bitmap bitmap = before.Bitmap;
            ImageData result = new ImageData();

            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height, bitmap.PixelFormat);

            for (int h = 0; h < bitmap.Height; ++h)
            {
                pbar.Value = (h * 100 / bitmap.Height);

                for (int w = 0; w < bitmap.Width; ++w)
                {
                    Color newPixel = Color.FromArgb(bitmap.GetPixel(w, h).A, (255 - bitmap.GetPixel(w, h).R), (255 - bitmap.GetPixel(w, h).G), (255 - bitmap.GetPixel(w, h).B));
                    newBitmap.SetPixel(w, h, newPixel);

                    result.data.SumUp(newPixel.R);
                    result.data.SumUp(newPixel.G);
                    result.data.SumUp(newPixel.B);

                    result.data_R.SumUp(newPixel.R);
                    result.data_G.SumUp(newPixel.G);
                    result.data_B.SumUp(newPixel.B);
                }
            }
            pbar.Value = 90;

            result.data.SetLeast();
            result.data_A = before.data_A;
            result.Bitmap = newBitmap;
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
