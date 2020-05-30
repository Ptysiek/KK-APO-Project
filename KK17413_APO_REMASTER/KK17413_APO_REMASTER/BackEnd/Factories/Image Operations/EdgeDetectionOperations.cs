using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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






}




