using KK17413_APO.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;



namespace KK17413_APO.Image_Operations
{
    class BinaryThresholding
    {
        public static ImageData GetResult(ImageData oldOne, ref ProgressBar pbar)
        {
            if (oldOne.Ready)
                return oldOne;


            Mat img = imread("./LENA_512.jpg", 1);
            cvtColor(img, img, CV_BGR2GRAY);

            for (int i = 1; i < 10; i++)
            {
                Mat binary_img;
                double border = 100 + 10 * i;
                threshold(img, binary_img, border, 255, THRESH_BINARY);
            }

            return oldOne;
        }
    }
}
