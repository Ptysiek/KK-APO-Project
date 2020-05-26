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



        // ----------------------------------------------------------------------------------------------------------
        #region lab2

        private void aDAPTIVEToolStripMenuItem1_Click(object sender, EventArgs e)
{
    /*// Working EmguCV version:
    Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
    Image<Gray, byte> gray = image.Convert<Gray, byte>();

    // Making image with black background:
    Image<Gray, byte> binarize = new Image<Gray, byte>(gray.Width, gray.Height, new Gray(0));

    CvInvoke.AdaptiveThreshold(gray, binarize, 255, Emgu.CV.CvEnum.AdaptiveThresholdType.MeanC, Emgu.CV.CvEnum.ThresholdType.Binary, 51, -50.50);

    pictureBox1.Image = image.Bitmap;
    pictureBox2.Image = binarize.Bitmap;
    //*/

    // PL
    // Progowanie Obrazu z zachowaniem poziomów szarości
    // Zmienia najpierw na obraz szaroodcieniowy, następnie go proguje.

    // ANG
    // Adaptive Thresholding

    //* Reczne Wersion
    int p1 = 50;
    int p2 = 150;

    //Bitmap img = new Bitmap("C:\\Users\\kptyc\\Desktop\\lena_color.png");
    Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
    Image<Gray, byte> gray = image.Convert<Gray, byte>();
    Bitmap img = gray.Bitmap;

    Bitmap result = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);

    for (int i = 0; i < img.Width; ++i)
    {
        for (int j = 0; j < img.Height; ++j)
        {
            Color val = img.GetPixel(i, j);

            result.SetPixel(i, j, Color.FromArgb((val.R >= p1 && val.R <= p2) ? val.R : 0,
                                                 (val.G >= p1 && val.G <= p2) ? val.R : 0,
                                                 (val.B >= p1 && val.B <= p2) ? val.R : 0));
        }
    }
    pictureBox1.Image = img;
    pictureBox2.Image = result;
    //*/


    /*  TO DO:
        - Pierwssze 255 to max value, zobacz co będzie jak zmenijszysz  

        - AdaptiveThresholdType.GaussianC
            posiada również meanC - sprawdź!

        - Podobnie jak w poprzednim,binaryzationToolStripMenuItem_Click,  zobacz różnice z ThresholdType.Binary

        - ostatnie 5, to block size
        - ostatnie 0.0 to param

        - zrobić bez opencv

        https://www.youtube.com/watch?v=Bjtg0RFm6po&list=PLUSwCY_ybvyLcNxZ1Q3vCkaCH9rjrRxA6&index=39
     */
}

private void posterizeToolStripMenuItem_Click(object sender, EventArgs e)
{
    // Given Arguments: 
    // - Currant bitmap
    // - int level

    // PL
    // Posteryzacja Obrazu
    // Operacja redukcji poziomów szarości 

    // ANG
    // Image Posterize
    // Conversion of a continuous gradation of tone to several regions of fewer tones.

    Bitmap img = new Bitmap("C:\\Users\\kptyc\\Desktop\\lena_color.png");
    //Bitmap img = new Bitmap("C:\\Users\\kptyc\\Desktop\\ScuiF.jpg");

    double levels = 2.0; // Posterize level two;
    levels--;
    double sr, sg, sb;
    int dr, dg, db;

    Bitmap result = new Bitmap(img.Width, img.Height, img.PixelFormat);

    for (int i = 0; i < img.Width; ++i)
    {
        for (int j = 0; j < img.Height; ++j)
        {
            Color val = img.GetPixel(i, j);

            sr = val.R / 255.0;
            sg = val.G / 255.0;
            sb = val.B / 255.0;

            dr = (int)(255 * Math.Round(sr * levels) / levels);
            dg = (int)(255 * Math.Round(sg * levels) / levels);
            db = (int)(255 * Math.Round(sb * levels) / levels);
            result.SetPixel(i, j, Color.FromArgb(dr, dg, db));
        }
    }

    pictureBox1.Image = img;
    pictureBox2.Image = result;
}

private void rozcioganieZakresuToolStripMenuItem_Click(object sender, EventArgs e)
{
    // PASS
}

#endregion
// ----------------------------------------------------------------------------------------------------------
#region lab3 a) wygładzania liniowego oparte na typowych maskach wygładzania (blur, gaussianBlur)
private void wygladzanieLinioweToolStripMenuItem_Click(object sender, EventArgs e)
{
    //wygładzania liniowego oparte na typowych maskach wygładzania (blur, gaussianBlur)
    Size k = new Size(20, 2);
    Point anchor = new Point(-1, -1);

    //* Gray Version
    Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
    Image<Gray, byte> gray = image.Convert<Gray, byte>();

    Image<Gray, byte> blur = new Image<Gray, byte>(gray.Width, gray.Height, new Gray(0));
    CvInvoke.Blur(gray, blur, k, anchor);
    //*/

    /* Color Version
    Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
    Image<Bgra, byte> blur = new Image<Bgra, byte>(image.Width, image.Height);

    CvInvoke.Blur(image, blur, k, anchor);
    //*/

    pictureBox1.Image = image.Bitmap;
    pictureBox2.Image = blur.Bitmap;
}

private void gussianBlurToolStripMenuItem_Click(object sender, EventArgs e)
{
    //wygładzania liniowego oparte na typowych maskach wygładzania (blur, gaussianBlur)
    Size k = new Size(21, 3);
    double s = 30; // Sigma
    Point anchor = new Point(-1, -1);

    /* Gray Version
    Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
    Image<Gray, byte> gray = image.Convert<Gray, byte>();

    Image<Gray, byte> blur = new Image<Gray, byte>(gray.Width, gray.Height, new Gray(0));
    CvInvoke.GaussianBlur(gray, blur, k, s);
    //*/

    //* Color Version
    Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
    Image<Bgra, byte> blur = new Image<Bgra, byte>(image.Width, image.Height);

    CvInvoke.GaussianBlur(image, blur, k, s);
    //*/

    pictureBox1.Image = image.Bitmap;
    pictureBox2.Image = blur.Bitmap;
}

private void medianBlurToolStripMenuItem_Click(object sender, EventArgs e)
{
    //wygładzania liniowego oparte na typowych maskach wygładzania (blur, gaussianBlur)
    int ksize = 21;

    //* Gray Version
    Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
    Image<Gray, byte> gray = image.Convert<Gray, byte>();

    Image<Gray, byte> blur = new Image<Gray, byte>(gray.Width, gray.Height, new Gray(0));
    CvInvoke.MedianBlur(gray, blur, ksize);
    //*/

    /* Color Version
    Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
    Image<Bgra, byte> blur = new Image<Bgra, byte>(image.Width, image.Height);

    CvInvoke.MedianBlur(image, blur, ksize);
    //*/

    pictureBox1.Image = image.Bitmap;
    pictureBox2.Image = blur.Bitmap;
}

#endregion
// ----------------------------------------------------------------------------------------------------------
#region lab3 b) detekcji krawędzi oparte na 3maskach detekcji krawędzi: Sobel, Laplacian, Canny
private void cannyDetectionToolStripMenuItem_Click(object sender, EventArgs e)
{
    double thresh = 50;
    double threshLinking = 20;

    Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
    //Image<Gray, byte> gray = image.Convert<Gray, byte>();

    Image<Gray, byte> canny;// = new Image<Gray, byte>(gray.Width, gray.Height, new Gray(0));
    canny = image.Canny(thresh, threshLinking);

    pictureBox1.Image = image.Bitmap;

    pictureBox2.Image = canny.Bitmap;
}

private void sobelDetectionToolStripMenuItem_Click(object sender, EventArgs e)
{
    int xOrder = 1;
    int yOrder = 1;
    int apertureSize = 3;

    Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");

    Image<Bgra, float> sobel;
    sobel = image.Sobel(xOrder, yOrder, apertureSize);

    pictureBox1.Image = image.Bitmap;
    pictureBox2.Image = sobel.Bitmap;
}

private void laplaceDetectionToolStripMenuItem_Click(object sender, EventArgs e)
{
    int apertureSize = 31;

    Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");

    Image<Bgra, float> laplace;
    laplace = image.Laplace(apertureSize);

    pictureBox1.Image = image.Bitmap;
    pictureBox2.Image = laplace.Bitmap;
}
#endregion
// ----------------------------------------------------------------------------------------------------------
#region lab3 c) Image Sharpaning with Laplacian Filter
private void imageSharpeningWithLaplacianFitlerToolStripMenuItem_Click(object sender, EventArgs e)
{
    // WYOSTRZANIE LINIOWE
    //Image Sharpaning with Laplacian Filter

    // Wyostrzanie obrazu podobnie jak wygładzanie może być zrealizowane przez zastosowanie ltracji i odpowiedniej maski.Uż
    // zanczym stopniu ułatwia zadanie.W bibliotece OpenCV ltrację dowolną maską można wykonać przy użyciu funkcji lter2D
    Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
    Image<Gray, byte> gray = image.Convert<Gray, byte>();

    //Image<Gray, Byte> image = new Image<Gray, byte>(300, 400);
    //gray.SetRandUniform(new MCvScalar(0.0), new MCvScalar(255.0)); // <<<<<<<<<<< Ciekawe

    //int apertureSize = 11;
    //Image<Gray, float> laplace = gray.Laplace(apertureSize);

    float[,] k;
    // Preset 1) laplacian mask:
    k = new float[,] { { 0, -1,  0},
                               {-1,  4, -1},
                               { 0, -1,  0} };

    // Preset 2) laplacian mask:
    k = new float[,] { {-1, -1, -1},
                               {-1,  8, -1},
                               {-1, -1, -1} };

    // Preset 3) laplacian mask:
    k = new float[,] { { 1, -2,  1},
                               {-2,  4, -2},
                               { 1, -2,  1} };

    ConvolutionKernelF kernel = new ConvolutionKernelF(k);
    Image<Gray, float> convoluted = gray * kernel;

    pictureBox1.Image = image.Bitmap;
    pictureBox2.Image = convoluted.Bitmap;
}
#endregion
// ----------------------------------------------------------------------------------------------------------
#region lab3 d) Detekcja krawędzi na masce PREWITT
private void detekcjaKrawedziPrewittToolStripMenuItem_Click(object sender, EventArgs e)
{
    // Prewitt mask edge detection

    Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
    Image<Gray, byte> gray = image.Convert<Gray, byte>();

    float[,] k;
    // Prewitt dla kiernku NE:
    k = new float[,] { { 0,  1,  1},
                               {-1,  0,  1},
                               {-1, -1,  0} };

    // Prewitt dla kiernku E:
    k = new float[,] { {-1,  0,  1},
                               {-1,  0,  1},
                               {-1,  0,  1} };

    // Prewitt dla kiernku SE:
    k = new float[,] { {-1, -1,  0},
                               {-1,  0,  1},
                               { 0,  1,  1} };

    // Prewitt dla kiernku S:
    k = new float[,] { {-1, -1, -1},
                               { 0,  0,  0},
                               { 1,  1,  1} };

    // Prewitt dla kiernku SW:
    k = new float[,] { { 0, -1, -1},
                               { 1,  0, -1},
                               { 1,  1,  0} };

    // Prewitt dla kiernku W:
    k = new float[,] { { 1,  0, -1},
                               { 1,  0, -1},
                               { 1,  0, -1} };

    // Prewitt dla kiernku NW:
    k = new float[,] { { 1,  1,  0},
                               { 1,  0, -1},
                               { 0, -1, -1} };

    // Prewitt dla kiernku N:
    k = new float[,] { { 1,  1,  1},
                               { 0,  0,  0},
                               {-1, -1, -1} };

    ConvolutionKernelF kernel = new ConvolutionKernelF(k);
    Image<Gray, float> convoluted = gray * kernel;

    pictureBox1.Image = image.Bitmap;
    pictureBox2.Image = convoluted.Bitmap;
}
#endregion

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
private void blendingToolStripMenuItem_Click(object sender, EventArgs e)
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
private void erosionToolStripMenuItem_Click(object sender, EventArgs e)
{
    Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
    //Image<Bgra, byte> image2 = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\ScuiF.jpg");
    //Image<Gray, byte> gray = image.Convert<Gray, byte>();


    //double alpha = 0.1;

    pictureBox1.Image = image.Bitmap;
    pictureBox2.Image = image.Erode(50).Bitmap;
}
private void dialationToolStripMenuItem_Click(object sender, EventArgs e)
{
    Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");

    pictureBox1.Image = image.Bitmap;
    pictureBox2.Image = image.Dilate(50).Bitmap;
}
private void openingToolStripMenuItem_Click(object sender, EventArgs e)
{
    Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");

    Size ksize = new Size(1, 100);
    Point anchor = new Point(-1, -1);

    Mat kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, ksize, anchor);

    pictureBox1.Image = image.Bitmap;
    pictureBox2.Image = image.MorphologyEx(Emgu.CV.CvEnum.MorphOp.Open, kernel, anchor, 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1.0)).Bitmap;
}

private void closingToolStripMenuItem_Click(object sender, EventArgs e)
{
    Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");

    Size ksize = new Size(1, 100);
    Point anchor = new Point(-1, -1);

    Mat kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, ksize, anchor);

    pictureBox1.Image = image.Bitmap;
    pictureBox2.Image = image.MorphologyEx(Emgu.CV.CvEnum.MorphOp.Close, kernel, anchor, 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1.0)).Bitmap;
}

private void gradientToolStripMenuItem_Click(object sender, EventArgs e)
{
    Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
    Image<Gray, byte> gray = image.Convert<Gray, byte>();

    Size ksize = new Size(5, 5);
    Point anchor = new Point(-1, -1);

    Mat kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, ksize, anchor);

    pictureBox1.Image = gray.Bitmap;
    pictureBox2.Image = gray.MorphologyEx(Emgu.CV.CvEnum.MorphOp.Gradient, kernel, anchor, 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1.0)).Bitmap;
}

private void topHatToolStripMenuItem_Click(object sender, EventArgs e)
{
    Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
    Image<Gray, byte> gray = image.Convert<Gray, byte>();

    Size ksize = new Size(5, 5);
    Point anchor = new Point(-1, -1);

    Mat kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, ksize, anchor);

    pictureBox1.Image = gray.Bitmap;
    pictureBox2.Image = gray.MorphologyEx(Emgu.CV.CvEnum.MorphOp.Tophat, kernel, anchor, 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1.0)).Bitmap;
}

private void blackHatToolStripMenuItem_Click(object sender, EventArgs e)
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



