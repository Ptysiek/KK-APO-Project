using System;
using System.Collections.Generic;
using System.Drawing;

using Emgu.CV;
using Emgu.CV.Structure;
using KK17413_APO_REMASTER.BackEnd.DataStructures;


namespace KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations
{
    class LogicalOperations : IOperationsFamily
    {
        public LogicalOperations()
        {
            operations_Dict = new Dictionary<string, IOperation>()
            {
                { "Negation_tsmi", new Negation() },
                { "And_tsmi", new And() },
                { "Blending_tsmi", new Blending() }
            };
        }
    }



    public class Negation : IOperation
    {
        public override string AskIfPopup()
        {
            return "NONE";
        }
        public override ImageData GetResult(ImageForm_Service x, List<int> args)
        => throw new NotImplementedException();

        public override ImageData GetResult(ImageForm_Service x, Bitmap bitmap, List<int> args)
        => throw new NotImplementedException();

        public override ImageData GetResult(ImageForm_Service service)
        {
            if (service.data.LastData() == null)
                return null;

            if (service.data.LastData().Bitmap == null)
                return null;

            //if (service.data.LastData().Ready)
            //    return null;


            //if (!before.Ready)
            //    before.RecalculateHistograms(ref pbar);

            service.imageWindow.StartProgressBar();

            Bitmap bitmap = service.data.LastData().Bitmap;
            ImageData result = new ImageData();

            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height, bitmap.PixelFormat);

            for (int h = 0; h < bitmap.Height; ++h)
            {
                service.imageWindow.SetProgressBarValue(h * 100 / bitmap.Height);

                for (int w = 0; w < bitmap.Width; ++w)
                {
                    Color newPixel = Color.FromArgb(bitmap.GetPixel(w, h).A, (255 - bitmap.GetPixel(w, h).R), (255 - bitmap.GetPixel(w, h).G), (255 - bitmap.GetPixel(w, h).B));
                    newBitmap.SetPixel(w, h, newPixel);

                    result.data.SumUp(newPixel.R);
                    result.data.SumUp(newPixel.G);
                    result.data.SumUp(newPixel.B);

                    result.data_A.SumUp(newPixel.A);
                    result.data_R.SumUp(newPixel.R);
                    result.data_G.SumUp(newPixel.G);
                    result.data_B.SumUp(newPixel.B);
                }
            }
            result.data.SetLeast();
            result.data_A.SetLeast();
            result.data_R.SetLeast();
            result.data_G.SetLeast();
            result.data_B.SetLeast();
            result.Bitmap = newBitmap;

            result.SetReady();
            service.imageWindow.CloseProgressBar();

            return result;
        }

    }



    public class And : IOperation
    {
        public override string AskIfPopup()
        {
            return "ChooseSecondImagePopup_AND";
        }
        public override ImageData GetResult(ImageForm_Service x)
        => throw new NotImplementedException();

        public override ImageData GetResult(ImageForm_Service x, List<int> args)
        => throw new NotImplementedException();
        

        public override ImageData GetResult(ImageForm_Service service, Bitmap argBitmap, List<int> args)
        {
            if (service.data.LastData() == null)
                return null;

            if (service.data.LastData().Bitmap == null)
                return null;

            if (argBitmap == null)
                return null;

            Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);
            Image<Bgra, byte> image2;

            if ((image.Bitmap.Width != argBitmap.Width) ||
                (image.Bitmap.Height != argBitmap.Height))
            {
                //Console.WriteLine("Przeliczam");

                Bitmap tmpbitmap = new Bitmap(image.Size.Width, image.Size.Height);

                for (int w = 0; w < tmpbitmap.Width; ++w)
                {
                    for (int h = 0; h < tmpbitmap.Height; ++h)
                    {
                        if (argBitmap.Width <= w || argBitmap.Height <= h)
                        {
                            tmpbitmap.SetPixel(w, h, Color.White);
                        }
                        else
                        {
                            tmpbitmap.SetPixel(w, h, argBitmap.GetPixel(w, h));
                        }
                    }
                }
                image2 = new Image<Bgra, byte>(tmpbitmap);
            }
            else
            {
                image2 = new Image<Bgra, byte>(argBitmap);
            }
            //Image<Bgra, byte> image2 = new Image<Bgra, byte>(tmpbitmap);

            try
            {
                Image<Bgra, byte> result = image.Add(image2);

                return new ImageData(result.Bitmap, service.data.LastData().ID);
            }
            catch
            {
                return null;
            }
        }

        // lab3 Zad 3
    }


    public class Blending : IOperation
    {
        public override string AskIfPopup()
        {
            return "ChooseSecondImage_Popup";
        }
        public override ImageData GetResult(ImageForm_Service x)
        => throw new NotImplementedException();

        public override ImageData GetResult(ImageForm_Service x, List<int> args)
        => throw new NotImplementedException();

        public override ImageData GetResult(ImageForm_Service service, Bitmap argBitmap, List<int> args)
        {
            if (service.data.LastData() == null)
                return null;

            if (service.data.LastData().Bitmap == null)
                return null;

            if (argBitmap == null)
                return null;

            if (args == null)
                return null;

            if (args.Count < 1)
                return null;


            double alpha = args[0];
            alpha /= 100;

            Image<Bgra, byte> image;
            Image<Bgra, byte> image2;

            image = new Image<Bgra, byte>(service.data.LastData().Bitmap);

            if ((image.Bitmap.Width != argBitmap.Width) ||
                (image.Bitmap.Height != argBitmap.Height))
            {
                //Console.WriteLine("Przeliczam");

                Bitmap tmpbitmap = new Bitmap(image.Size.Width, image.Size.Height);

                for (int w = 0; w < tmpbitmap.Width; ++w)
                {
                    for (int h = 0; h < tmpbitmap.Height; ++h)
                    {
                        if (argBitmap.Width <= w || argBitmap.Height <= h)
                        {
                            tmpbitmap.SetPixel(w, h, Color.White);
                        }
                        else
                        {
                            tmpbitmap.SetPixel(w, h, argBitmap.GetPixel(w, h));
                        }
                    }
                }
                image2 = new Image<Bgra, byte>(tmpbitmap);
            }
            else
            {
                image2 = new Image<Bgra, byte>(argBitmap);
            }

            try
            {
                Image<Bgra, byte> result = image.AddWeighted(image2, alpha, (1 - alpha), 0);

                return new ImageData(result.Bitmap, service.data.LastData().ID);
            }
            catch
            {
                return null;
            }
        }
        
        // lab3 Zad 3
    }
}








