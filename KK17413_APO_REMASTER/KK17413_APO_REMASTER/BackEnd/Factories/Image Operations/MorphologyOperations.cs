using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using KK17413_APO_REMASTER.BackEnd.DataStructures;


namespace KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations
{
    class MorphologyOperations : IOperationsFamily
    {
        public MorphologyOperations()
        {
            operations_Dict = new Dictionary<string, IOperation>()
            {
                { "Erode_tsmi", new Erode() }
            };
        }
    }


    public class Erode : IOperation
    {
        public override string AskIfPopup()
        {
            return "Erode_Popup";
        }

        public override ImageData GetResult(ImageForm_Service service)
        => throw new NotImplementedException();

        public override ImageData GetResult(ImageForm_Service x, Bitmap bitmap, List<int> args)
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

            //Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            //Image<Bgra, byte> image2 = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\ScuiF.jpg");
            //Image<Gray, byte> gray = image.Convert<Gray, byte>();

            try
            {
                Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);

                Image<Bgra, byte> result = image.Erode(args[0]);

                return new ImageData(result.Bitmap, service.data.LastData().ID);
            }
            catch
            {
                return null;
            }
        }

        // ------------------------------------------------------
        // lab3 b) detekcji krawędzi oparte na 3maskach detekcji krawędzi: Sobel, Laplacian, Canny
        // ------------------------------------------------------
    }
}