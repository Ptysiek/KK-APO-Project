#pragma warning disable
using System;
using System.Drawing;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing.Imaging;


namespace KK17413_APO_REMASTER
{
    public partial class Form1 : Form
    {
        public PictureBox pictureBox1 = new PictureBox();
        public PictureBox pictureBox2 = new PictureBox();




        #region lab3 e) Uniwersajna liniowa sąsiedztwa
        // W zasadzie to poprzednie zadania...
        // Ale z fajniejszym formem który powadza wprowadzać maski 
        // Nazwę to Custom, nie uniwersalna
        // Panel podobny do działania histogramu.
        // I git
        #endregion
        // ----------------------------------------------------------------------------------------------------------
        #region lab3 Zad 2
        //Filtr medianowy
        // Opracja ltracji medianowej również może być w prosty sposób zrealizowana przy użyciu biblioteki OpenCV.Podobnie jak w
        // powyżej, argumenty wejściowe, prócz obrazu wejściowego, to rozmiar ltra.Uwaga! Należy pamiętać, że rozmiar okna mus
        // wartość całkowitą większą od 1, np.: 3, 5, 7 itd.
        // Ponadto, w obrazach wielokanałowych każdy kanał jest przetwarzany odrębnie.
        // medianBlured_img = cv2.medianBlur(img, 11)
        // cv2_imshow(medianBlured_img)

        // Czyli, zrobić poprzednie zadanie jeszcze bardziej uniwersalnie ZA PIERWSZYM RAZEM. cool
        #endregion
        // ----------------------------------------------------------------------------------------------------------
        #region lab3 Zad 3
        private void BlendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            Image<Bgra, byte> image2 = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\ScuiF.jpg");
            //Image<Gray, byte> gray = image.Convert<Gray, byte>();

            double alpha = 0.1;

            pictureBox1.Image = image.AddWeighted(image2, alpha, (1 - alpha), 0).Bitmap;
            pictureBox2.Image = image.Bitmap;
        }
        #endregion
        // ------------------------------------------------------------------------------------------------------
        #region MyStuff
        private void ErosionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            //Image<Bgra, byte> image2 = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\ScuiF.jpg");
            //Image<Gray, byte> gray = image.Convert<Gray, byte>();


            //double alpha = 0.1;

            pictureBox1.Image = image.Bitmap;
            pictureBox2.Image = image.Erode(50).Bitmap;
        }
        private void DialationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");

            pictureBox1.Image = image.Bitmap;
            pictureBox2.Image = image.Dilate(50).Bitmap;
        }
        private void OpeningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");

            Size ksize = new Size(1, 100);
            Point anchor = new Point(-1, -1);

            Mat kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, ksize, anchor);

            pictureBox1.Image = image.Bitmap;
            pictureBox2.Image = image.MorphologyEx(Emgu.CV.CvEnum.MorphOp.Open, kernel, anchor, 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1.0)).Bitmap;
        }

        private void ClosingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");

            Size ksize = new Size(1, 100);
            Point anchor = new Point(-1, -1);

            Mat kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, ksize, anchor);

            pictureBox1.Image = image.Bitmap;
            pictureBox2.Image = image.MorphologyEx(Emgu.CV.CvEnum.MorphOp.Close, kernel, anchor, 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1.0)).Bitmap;
        }

        private void GradientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            Image<Gray, byte> gray = image.Convert<Gray, byte>();

            Size ksize = new Size(5, 5);
            Point anchor = new Point(-1, -1);

            Mat kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, ksize, anchor);

            pictureBox1.Image = gray.Bitmap;
            pictureBox2.Image = gray.MorphologyEx(Emgu.CV.CvEnum.MorphOp.Gradient, kernel, anchor, 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1.0)).Bitmap;
        }

        private void TopHatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            Image<Gray, byte> gray = image.Convert<Gray, byte>();

            Size ksize = new Size(5, 5);
            Point anchor = new Point(-1, -1);

            Mat kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, ksize, anchor);

            pictureBox1.Image = gray.Bitmap;
            pictureBox2.Image = gray.MorphologyEx(Emgu.CV.CvEnum.MorphOp.Tophat, kernel, anchor, 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1.0)).Bitmap;
        }

        private void BlackHatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            Image<Gray, byte> gray = image.Convert<Gray, byte>();

            Size ksize = new Size(5, 5);
            Point anchor = new Point(-1, -1);

            Mat kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, ksize, anchor);

            pictureBox1.Image = gray.Bitmap;
            pictureBox2.Image = gray.MorphologyEx(Emgu.CV.CvEnum.MorphOp.Blackhat, kernel, anchor, 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1.0)).Bitmap;
        }
        #endregion
        // ------------------------------------------------------------------------------------------------------
    }

}
#pragma warning restore
