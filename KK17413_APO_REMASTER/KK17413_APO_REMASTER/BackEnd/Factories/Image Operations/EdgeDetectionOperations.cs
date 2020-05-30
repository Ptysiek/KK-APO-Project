using System;
using System.Collections.Generic;
using Emgu.CV;
using Emgu.CV.Structure;
using KK17413_APO_REMASTER.BackEnd.DataStructures;


namespace KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations
{
    class EdgeDetectionOperations : IOperationsFamily
    {
        public EdgeDetectionOperations()
        {
            operations_Dict = new Dictionary<string, IOperation>()
            {
                { "CannyDetection_tsmi", new CannyDetection() },
                { "SobelDetection_tsmi", new SobelDetection() },
                { "LaplaceDetection_tsmi", new LaplaceDetection() },
                { "PrewittMasks_tsmi", new PrewittMasks() }
            };
        }
    }


    public class CannyDetection : IOperation
    {
        public override string AskIfPopup()
        {
            return "EdgeDetection_Popup";
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

            if (args.Count < 2)
                return null;

            double thresh = args[0];
            double threshLinking = args[1];

            Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);

            Image<Gray, byte> canny;// = new Image<Gray, byte>(gray.Width, gray.Height, new Gray(0));
            canny = image.Canny(thresh, threshLinking);

            return new ImageData(canny.Bitmap, service.data.LastData().ID);
        }

        // ------------------------------------------------------
        // lab3 b) detekcji krawędzi oparte na 3maskach detekcji krawędzi: Sobel, Laplacian, Canny
        // ------------------------------------------------------
    }



    public class SobelDetection : IOperation
    {
        public override string AskIfPopup()
        {
            return "EdgeDetection_Sobel_Popup";
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

            if (args.Count < 3)
                return null;

            int xOrder = args[0];
            int yOrder = args[1];
            int apertureSize = args[2];

            Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);
            Image<Bgra, float> sobel;

            sobel = image.Sobel(xOrder, yOrder, apertureSize);
            // xOrder >= 0
            // yOrder >= 0
            // xOrder + yOrder == 1   ???

            // apertureSize % 2 == 1
            // apertureSize <= 31

            return new ImageData(sobel.Bitmap, service.data.LastData().ID);
        }

        // ------------------------------------------------------
        // lab3 b) detekcji krawędzi oparte na 3maskach detekcji krawędzi: Sobel, Laplacian, Canny
        // ------------------------------------------------------
    }


    



    public class LaplaceDetection : IOperation
    {
        public override string AskIfPopup()
        {
            return "EdgeDetection_Laplace_Popup";
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

            int apertureSize = args[0];

            Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);
            Image<Bgra, float> laplace;

            laplace = image.Laplace(apertureSize);

            return new ImageData(laplace.Bitmap, service.data.LastData().ID);
        }

        // ------------------------------------------------------
        // lab3 b) detekcji krawędzi oparte na 3maskach detekcji krawędzi: Sobel, Laplacian, Canny
        // ------------------------------------------------------
    }


    
    



    public class PrewittMasks : IOperation
    {
        public override string AskIfPopup()
        {
            return "PrewittMasks_Popup";
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


            float[,] k;
            switch (choice)
            {
                case 0:
                    // Prewitt dla kiernku N:
                    k = new float[,] { { 1,  1,  1},
                                       { 0,  0,  0},
                                       {-1, -1, -1} };
                    break;
                case 1:
                    // Prewitt dla kiernku NE:
                    k = new float[,] { { 0,  1,  1},
                                       {-1,  0,  1},
                                       {-1, -1,  0} };
                    break;
                case 2:
                    // Prewitt dla kiernku E:
                    k = new float[,] { {-1,  0,  1},
                                       {-1,  0,  1},
                                       {-1,  0,  1} };
                    break;
                case 3:
                    // Prewitt dla kiernku SE:
                    k = new float[,] { {-1, -1,  0},
                                       {-1,  0,  1},
                                       { 0,  1,  1} };
                    break;
                case 4:
                    // Prewitt dla kiernku S:
                    k = new float[,] { {-1, -1, -1},
                                       { 0,  0,  0},
                                       { 1,  1,  1} };
                    break;
                case 5:
                    // Prewitt dla kiernku SW:
                    k = new float[,] { { 0, -1, -1},
                                       { 1,  0, -1},
                                       { 1,  1,  0} };
                    break;
                case 6:
                    // Prewitt dla kiernku W:
                    k = new float[,] { { 1,  0, -1},
                                       { 1,  0, -1},
                                       { 1,  0, -1} };
                    break;
                default:
                    // Prewitt dla kiernku NW:
                    k = new float[,] { { 1,  1,  0},
                                       { 1,  0, -1},
                                       { 0, -1, -1} };
                    break;
            }

            Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);
            Image<Gray, byte> gray = image.Convert<Gray, byte>();

            ConvolutionKernelF kernel = new ConvolutionKernelF(k);
            //Image<Gray, float> convoluted = gray * kernel;
            Image<Bgra, float> convoluted = image * kernel;

            return new ImageData(convoluted.Bitmap, service.data.LastData().ID);
        }

        // ------------------------------------------------------
        // lab3 d) Kierunkowa Detekcja krawędzi na masce PREWITT
        // ------------------------------------------------------
    }




}




