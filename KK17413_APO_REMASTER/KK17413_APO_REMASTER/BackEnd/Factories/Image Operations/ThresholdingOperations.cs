using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;
using KK17413_APO_REMASTER.BackEnd.DataStructures;


namespace KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations
{
    class ThresholdingOperations : IOperationsFamily
    {
        public ThresholdingOperations()
        {
            operations_Dict = new Dictionary<string, IOperation>()
            {
                { "BinaryThresholding_tsmi", new BinaryThresholding() },
                { "Thresholding_tsmi", new Thresholding() },
                { "AdaptiveThresholding_tsmi", new AdaptiveThresholding() }
            };
        }
    }


    public class BinaryThresholding : IOperation
    {
        public override string AskIfPopup()
        {
            return "Histogram_Popup";
        }

        public override ImageData GetResult(ImageForm_Service service)
        => throw new NotImplementedException();

        public override ImageData GetResult(ImageForm_Service service, List<int> args)
        {
            if (service == null)
                return null;

            if (service.data == null)
                return null;

            if (service.data.LastData() == null)
                return null;

            if (service.data.LastData().Bitmap == null)
                return null;

            //if (service.data.LastData().Ready)
            //    return null;

            return Operation_Optimized(service, args);

            // Not Optimized: - Deprecated:
            //return Operation(service, args);
        }

        private ImageData Operation_Optimized(ImageForm_Service service, List<int> args)
        {
            if (args == null)
                return null;

            if (args.Count < 1)
                return null;            
            
            int p1 = args[0];
            int p2 = args[1];

            Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);
            Image<Gray, byte> gray = image.Convert<Gray, byte>();

            Image<Gray, byte> binarize = new Image<Gray, byte>(gray.Width, gray.Height, new Gray(0));

            CvInvoke.Threshold(gray, binarize, p1, p2, Emgu.CV.CvEnum.ThresholdType.Binary);

            return new ImageData(binarize.Bitmap, service.data.LastData().ID);
        }

        #pragma warning disable IDE0051
        // OPERATION NOT FAST ENOUGH - DEPRECATED
        private ImageData Operation(ImageForm_Service service, List<int> args)
        {
            int value = args[0];

            int lmin = 0;
            int lmax = 0;

            service.imageWindow.StartProgressBar();

            Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);
            Image<Gray, byte> gray = image.Convert<Gray, byte>();
            Bitmap img = gray.Bitmap;

            if (service.data.LastData().Ready)
            {
                lmin = service.data.LastData().data.minValue;
                lmax = service.data.LastData().data.maxValue;
            }
            else
            {
                for (int i = 0; i < img.Width; ++i)
                {
                    service.imageWindow.SetProgressBarValue(i * 100 / img.Width);

                    for (int j = 0; j < img.Height; ++j)
                    {
                        Color val = img.GetPixel(i, j);

                        if (val.R > 0)
                        {
                            if (lmin == 0)
                            {
                                lmin = val.R;
                            }
                            else if (val.R < lmin)
                            {
                                lmin = val.R;
                            }

                            if (lmax == 0)
                            {
                                lmax = val.R;
                            }
                            else if (val.R > lmax)
                            {
                                lmax = val.R;
                            }
                        }
                    }
                }
            }

            Bitmap result = new Bitmap(img.Width, img.Height, service.data.LastData().Bitmap.PixelFormat);

            for (int i = 0; i < img.Width; ++i)
            {
                service.imageWindow.SetProgressBarValue(i * 100 / img.Width);

                for (int j = 0; j < img.Height; ++j)
                {
                    Color val = img.GetPixel(i, j);

                    result.SetPixel(i, j, Color.FromArgb((val.R <= value) ? lmin : lmax,
                                                         (val.G <= value) ? lmin : lmax,
                                                         (val.B <= value) ? lmin : lmax));
                }
            }

            service.imageWindow.CloseProgressBar();
            return new ImageData(result, service.data.LastData().ID);
        }
        
        #pragma warning restore IDE0051
        // ------------------------------------------------------
        #region TODO
        // PL
        // Progowanie

        // ANG
        // Thresholding

        /*  TO DO:
            - Zrób osobny form na którym widać histogram obecnego obrazka
            - Wybieraj zakres na tym histogramie - wartości 50, 150

            - Daj opis na topie forma, co dana operacja robi.
              Opis musi zmieniać język.Adaptive ThresholdingAdaptive Thresholding


            - odświeżaj prawdziwy obrazek zgodnie z tym co ustawisz na histogramie forma.
              Ale go nie modyfikuj. Modyfikuj dopiero po przycisku ok lub aply.

            - Emgu.CV.CvEnum.ThresholdType posiada co najmeniej 4 inne tryby.
              Pozwól przełączać między nimi w tym samym formie do tej operacji.

            - zrobić bez opencv

            https://www.youtube.com/watch?v=KpCQp_rd-Nk&list=PLUSwCY_ybvyLcNxZ1Q3vCkaCH9rjrRxA6&index=38
         */
        #endregion
        // ------------------------------------------------------
    }





    public class Thresholding : IOperation
    {
        public override string AskIfPopup()
        {
            return "Histogram_Popup";
        }

        public override ImageData GetResult(ImageForm_Service service)
        => throw new NotImplementedException();

        public override ImageData GetResult(ImageForm_Service service, List<int> args)
        {
            if (service == null)
                return null;

            if (service.data == null)
                return null;

            if (service.data.LastData() == null)
                return null;

            if (service.data.LastData().Bitmap == null)
                return null;

            //if (service.data.LastData().Ready)
            //    return null;

            return Operation(service, args);
        }

        private ImageData Operation(ImageForm_Service service, List<int> args)
        {
            int p1 = args[0];
            int p2 = args[1];

            Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);
            Image<Gray, byte> gray = image.Convert<Gray, byte>();
            Bitmap img = gray.Bitmap;

            Bitmap result = new Bitmap(img.Width, img.Height, PixelFormat.Format32bppArgb);


            service.imageWindow.StartProgressBar();

            for (int i = 0; i < img.Width; ++i)
            {
                service.imageWindow.SetProgressBarValue(i * 100 / img.Width);

                for (int j = 0; j < img.Height; ++j)
                {
                    Color val = img.GetPixel(i, j);

                    result.SetPixel(i, j, Color.FromArgb((val.R >= p1 && val.R <= p2) ? val.R : 0,
                                                         (val.G >= p1 && val.G <= p2) ? val.R : 0,
                                                         (val.B >= p1 && val.B <= p2) ? val.R : 0));
                }
            }

            service.imageWindow.CloseProgressBar();
            return new ImageData(result, service.data.LastData().ID);
        }

        // ------------------------------------------------------
        #region TODO
        // PL
        // Progowanie Obrazu z zachowaniem poziomów szarości
        // Zmienia najpierw na obraz szaroodcieniowy, następnie go proguje.

        // ANG
        // Adaptive Thresholding

        /*  TO DO:
            - Pierwssze 255 to max value, zobacz co będzie jak zmenijszysz  
         */
        #endregion
        // ------------------------------------------------------
    }




    public class AdaptiveThresholding : IOperation
    {
        public override string AskIfPopup()
        {
            return "DoubleParam_Popup";
        }

        public override ImageData GetResult(ImageForm_Service service)
        => throw new NotImplementedException();

        public override ImageData GetResult(ImageForm_Service service, List<int> args)
        {
            if (service == null)
                return null;

            if (service.data == null)
                return null;

            if (service.data.LastData() == null)
                return null;

            if (service.data.LastData().Bitmap == null)
                return null;

            //if (service.data.LastData().Ready)
            //    return null;

            return Operation(service, args);
        }

        private ImageData Operation(ImageForm_Service service, List<int> args)
        {
            if (args == null)
                return null;

            if (args.Count < 1)
                return null;

            int BlockSize = args[0];
            double param2 = args[1];

            Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);
            Image<Gray, byte> gray = image.Convert<Gray, byte>();

            Image<Gray, byte> binarize = new Image<Gray, byte>(gray.Width, gray.Height, new Gray(0));

            // BlockSize > 1
            // blocksize % 2 == 1

            if (BlockSize <= 1)
                BlockSize = 3;

            if (BlockSize % 2 != 1)
                ++BlockSize;

            CvInvoke.AdaptiveThreshold(gray, binarize, 255, Emgu.CV.CvEnum.AdaptiveThresholdType.MeanC, Emgu.CV.CvEnum.ThresholdType.Binary, BlockSize, param2);

            return new ImageData(binarize.Bitmap, service.data.LastData().ID);
        }

        // ------------------------------------------------------
        #region TODO
        // PL
        // Progowanie Obrazu z zachowaniem poziomów szarości
        // Zmienia najpierw na obraz szaroodcieniowy, następnie go proguje.

        // ANG
        // Adaptive Thresholding

        /*  TO DO:
            - Pierwssze 255 to max value, zobacz co będzie jak zmenijszysz  

            - AdaptiveThresholdType.GaussianC
                posiada również meanC - sprawdź!

            - Podobnie jak w poprzednim,binaryzationToolStripMenuItem_Click,  zobacz różnice z ThresholdType.Binary

            - ostatnie 5, to block size
            - ostatnie 0.0 to param

            https://www.youtube.com/watch?v=Bjtg0RFm6po&list=PLUSwCY_ybvyLcNxZ1Q3vCkaCH9rjrRxA6&index=39
         */
        #endregion
        // ------------------------------------------------------
    }
}
