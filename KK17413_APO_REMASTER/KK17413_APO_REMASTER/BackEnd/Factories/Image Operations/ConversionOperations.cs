
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;
using KK17413_APO_REMASTER.BackEnd.DataStructures;

namespace KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations
{
    class ConversionOperations : IOperationsFamily
    {
        public ConversionOperations()
        {
            operations_Dict = new Dictionary<string, IOperation>()
            {
                { "ToBlackGrayWhite_tsmi", new ToBlackGrayWhite() }
            };
        }
    }


    public class ToBlackGrayWhite : IOperation
    {
        public override string AskIfPopup()
        {
            return "NONE";
        }

        public override ImageData GetResult(ImageForm_Service service, List<int> args)
        => throw new NotImplementedException();

        public override ImageData GetResult(ImageForm_Service x, Bitmap bitmap, List<int> args)
        => throw new NotImplementedException();

        public override ImageData GetResult(ImageForm_Service service)
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

            return Operation(service);
        }

        private ImageData Operation(ImageForm_Service service)
        {
            try
            {
                Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);
                Image<Gray, byte> gray = image.Convert<Gray, byte>();

                return new ImageData(gray.Bitmap, service.data.LastData().ID);
            }
            catch 
            {
                return null;
            }
        }
    }
}






