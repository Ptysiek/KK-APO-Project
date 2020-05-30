using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;
using KK17413_APO_REMASTER.BackEnd.DataStructures;


namespace KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations
{
    class SmoothingOperations : IOperationsFamily
    {
        public SmoothingOperations()
        {
            operations_Dict = new Dictionary<string, IOperation>()
            {
                { "Blur_tsmi", new Blur() },
                { "GaussianBlur_tsmi", new GaussianBlur() },
                { "MedianBlur_tsmi", new MedianBlur() }
            };
        }
    }




    public class Blur : IOperation
    {
        public override string AskIfPopup()
        {
            return "Blur_Popup";
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

            if (args.Count < 4)
                return null;

            Size k = new Size(args[0], args[1]);
            Point anchor = new Point(args[2], args[3]); 

            Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);
            //Image<Gray, byte> gray = image.Convert<Gray, byte>();
            //Image<Gray, byte> blur = new Image<Gray, byte>(gray.Width, gray.Height, new Gray(0));
            //CvInvoke.Blur(gray, blur, k, anchor);
  
            Image<Bgra, byte> blur = new Image<Bgra, byte>(image.Width, image.Height);
            CvInvoke.Blur(image, blur, k, anchor);

            return new ImageData(blur.Bitmap, service.data.LastData().ID);
        }

        // ------------------------------------------------------
        // lab3 a) wygładzania liniowego oparte na typowych maskach wygładzania (blur, gaussianBlur)
        //wygładzania liniowego oparte na typowych maskach wygładzania (blur, gaussianBlur)
        // ------------------------------------------------------
    }









    public class GaussianBlur : IOperation
    {
        public override string AskIfPopup()
        {
            return "GaussianBlur_Popup";
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

            if (args.Count < 4)
                return null;

            Size k = new Size(args[0], args[1]);
            double sigmaX = args[2];
            double sigmaY = args[3];

            // Gray Version
            //Image<Bgra, byte> image = new Image<Bgra, byte>("C:\\Users\\kptyc\\Desktop\\lena_color.png");
            //Image<Gray, byte> gray = image.Convert<Gray, byte>();
            //Image<Gray, byte> blur = new Image<Gray, byte>(gray.Width, gray.Height, new Gray(0));
            //CvInvoke.GaussianBlur(gray, blur, k, s);

            Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);
            Image<Bgra, byte> blur = new Image<Bgra, byte>(image.Width, image.Height);

            CvInvoke.GaussianBlur(image, blur, k, sigmaX, sigmaY);


            return new ImageData(blur.Bitmap, service.data.LastData().ID);
        }

        // ------------------------------------------------------
        // lab3 a) wygładzania liniowego oparte na typowych maskach wygładzania (blur, gaussianBlur)
        //wygładzania liniowego oparte na typowych maskach wygładzania (blur, gaussianBlur)
        // ------------------------------------------------------
    }









    public class MedianBlur : IOperation
    {
        public override string AskIfPopup()
        {
            return "MedianBlur_Popup";
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



            int ksize = args[0];

            Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);
            Image<Bgra, byte> blur = new Image<Bgra, byte>(image.Width, image.Height);

            CvInvoke.MedianBlur(image, blur, ksize);

            return new ImageData(blur.Bitmap, service.data.LastData().ID);
        }

        // ------------------------------------------------------
        // lab3 a) wygładzania liniowego oparte na typowych maskach wygładzania (blur, gaussianBlur)
        //wygładzania liniowego oparte na typowych maskach wygładzania (blur, gaussianBlur)
        // ------------------------------------------------------
    }

}




