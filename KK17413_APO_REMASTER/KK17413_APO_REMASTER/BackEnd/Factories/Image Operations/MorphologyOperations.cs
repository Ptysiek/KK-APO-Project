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
                { "Erode_tsmi", new Erode() },
                { "Dilate_tsmi", new Dilate() },
                { "Open_tsmi", new Open() },
                { "Close_tsmi", new Close() }
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
    }


    public class Dilate : IOperation
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

            try
            {
                Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);

                Image<Bgra, byte> result = image.Dilate(args[0]);

                return new ImageData(result.Bitmap, service.data.LastData().ID);
            }
            catch
            {
                return null;
            }
        }
    }

    public class Open : IOperation
    {
        public override string AskIfPopup()
        {
            return "Blur_Popup";
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

            if (args.Count < 4)
                return null;

            Size ksize = new Size(args[0], args[1]);
            Point anchor = new Point(args[2], args[3]);

            try
            {
                Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);

                Mat kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, ksize, anchor);

                Image<Bgra, byte> result = image.MorphologyEx(Emgu.CV.CvEnum.MorphOp.Open, kernel, anchor, 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1.0));

                return new ImageData(result.Bitmap, service.data.LastData().ID);
            }
            catch
            {
                return null;
            }
        }
    }
    
    public class Close : IOperation
    {
        public override string AskIfPopup()
        {
            return "Blur_Popup";
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

            if (args.Count < 4)
                return null;

            Size ksize = new Size(args[0], args[1]);
            Point anchor = new Point(args[2], args[3]);

            try
            {
                Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);

                Mat kernel = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, ksize, anchor);

                Image<Bgra, byte> result = image.MorphologyEx(Emgu.CV.CvEnum.MorphOp.Close, kernel, anchor, 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1.0));

                return new ImageData(result.Bitmap, service.data.LastData().ID);
            }
            catch
            {
                return null;
            }
        }
    }

}

