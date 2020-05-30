using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;
using KK17413_APO_REMASTER.BackEnd.DataStructures;


namespace KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations
{
    class LinearSharpeningOperations : IOperationsFamily
    {
        public LinearSharpeningOperations()
        {
            operations_Dict = new Dictionary<string, IOperation>()
            {
                { "laplaceMasksSharpening_tsmi", new LaplaceMasksSharpening() }
            };
        }
    }


    public class LaplaceMasksSharpening : IOperation
    {
        public override string AskIfPopup()
        {
            return "CustomMatrix_Popup";
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

            int choice = args[0];
            Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);
            Image<Gray, byte> gray = image.Convert<Gray, byte>();

            //Image<Gray, Byte> image = new Image<Gray, byte>(300, 400);
            //gray.SetRandUniform(new MCvScalar(0.0), new MCvScalar(255.0)); // <<<<<<<<<<< Ciekawe

            //int apertureSize = 11;
            //Image<Gray, float> laplace = gray.Laplace(apertureSize);

            float[,] k;

            switch (choice)
            {
                case 0:
                    // Preset 1) laplacian mask:
                    k = new float[,] { { 0, -1,  0},
                                       {-1,  4, -1},
                                       { 0, -1,  0} };
                    break;

                case 1:
                    // Preset 2) laplacian mask:
                    k = new float[,] { {-1, -1, -1},
                                       {-1,  8, -1},
                                       {-1, -1, -1} };
                    break;

                default:
                    // Preset 3) laplacian mask:
                    k = new float[,] { { 1, -2,  1},
                                       {-2,  4, -2},
                                       { 1, -2,  1} };
                    break;
            }

            ConvolutionKernelF kernel = new ConvolutionKernelF(k);
            Image<Gray, float> convoluted = gray * kernel;
            //Image<Bgra, float> convoluted = image * kernel;

            return new ImageData(convoluted.Bitmap, service.data.LastData().ID);
        }

        // ------------------------------------------------------
        // lab3 c) Image Sharpaning with Laplacian Filter

        // WYOSTRZANIE LINIOWE
        //Image Sharpaning with Laplacian Filter

        // Wyostrzanie obrazu podobnie jak wygładzanie może być zrealizowane przez zastosowanie ltracji i odpowiedniej maski.Uż
        // zanczym stopniu ułatwia zadanie.W bibliotece OpenCV ltrację dowolną maską można wykonać przy użyciu funkcji lter2D
        // ------------------------------------------------------
    }


}

