using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;
using KK17413_APO_REMASTER.BackEnd.DataStructures;


namespace KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations
{
    class SimpleRadiometricsOperations : IOperationsFamily
    {
        public SimpleRadiometricsOperations()
        {
            operations_Dict = new Dictionary<string, IOperation>()
            {
                { "RadiometricDarkening_tsmi", new RadiometricDarkening() },
                { "RadiometricLightening_tsmi", new RadiometricLightening() },
                { "RadiometricExternLightening_tsmi", new RadiometricExternLightening () },
                { "RadiometricExternDarkening_tsmi", new RadiometricExternDarkening () },

                { "RadiometricSmoothDarkening_tsmi", new RadiometricSmoothDarkening() },
                { "RadiometricInvertedSmoothDarkening_tsmi", new RadiometricInvertedSmoothDarkening() },

                { "RadiometricSmoothLightening_tsmi", new RadiometricSmoothLightening() },
                { "RadiometricInvertedSmoothLightening_tsmi", new RadiometricInvertedSmoothLightening() },

                { "RadiometricExternSmoothLightening_tsmi", new RadiometricExternSmoothLightening() },
                { "RadiometricExternSmoothDarkening_tsmi", new RadiometricExternSmoothDarkening() }
            };
        }
    }

    // ----------------------------------------------------------------------
    public class RadiometricDarkening : IOperation
    {
        public override string AskIfPopup()
        {
            return "DoubleParamRadiometricCustom_Popup";
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

            if (args.Count < 2)
                return null;

            if (service.data.LastData() == null)
                return null;

            if (service.data.LastData().Bitmap == null)
                return null;

            int radio = args[0];
            int value = args[1];

            Bitmap bitmap = service.data.LastData().Bitmap;
            Bitmap result = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);

            Point center = new Point(bitmap.Width / 2, bitmap.Height / 2);

            for (int w = 0; w < bitmap.Width; ++w)
            {
                for (int h = 0; h < bitmap.Height; ++h)
                {
                    int currentRadio = CalculateRadio(new Point(w,h),center);
                    Color pixel = bitmap.GetPixel(w, h);

                    if (currentRadio > radio)
                    {
                        result.SetPixel(w, h, pixel);
                        continue;
                    }

                    
                    result.SetPixel(w, h, Color.FromArgb(
                        pixel.A,
                        (pixel.R - value > 0) ? pixel.R - value : 0,
                        (pixel.G - value > 0) ? pixel.G - value : 0,
                        (pixel.B - value > 0) ? pixel.B - value : 0
                        ));
                }
            }

            return new ImageData(result, service.data.LastData().ID);
        }


        private int CalculateRadio(Point current, Point center)
        {
            double a = (center.X - current.X) * (center.X - current.X);
            double b = (center.Y - current.Y) * (center.Y - current.Y);

            return (int)Math.Sqrt(a + b);
        }
    }

    public class RadiometricLightening : IOperation
    {
        public override string AskIfPopup()
        {
            return "DoubleParamRadiometricCustom_Popup";
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

            if (args.Count < 2)
                return null;

            int radio = args[0];
            int value = args[1];

            Bitmap bitmap = service.data.LastData().Bitmap;
            Bitmap result = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);

            Point center = new Point(bitmap.Width / 2, bitmap.Height / 2);

            for (int w = 0; w < bitmap.Width; ++w)
            {
                for (int h = 0; h < bitmap.Height; ++h)
                {
                    int currentRadio = CalculateRadio(new Point(w, h), center);
                    Color pixel = bitmap.GetPixel(w, h);

                    if (currentRadio > radio)
                    {
                        result.SetPixel(w, h, pixel);
                        continue;
                    }

                    result.SetPixel(w, h, Color.FromArgb(
                        pixel.A,
                        (pixel.R + value < 255) ? pixel.R + value : 255,
                        (pixel.G + value < 255) ? pixel.G + value : 255,
                        (pixel.B + value < 255) ? pixel.B + value : 255
                        ));
                }
            }
            return new ImageData(result, service.data.LastData().ID);
        }

        private int CalculateRadio(Point current, Point center)
        {
            double a = (center.X - current.X) * (center.X - current.X);
            double b = (center.Y - current.Y) * (center.Y - current.Y);

            return (int)Math.Sqrt(a + b);
        }
    }

    public class RadiometricExternLightening : IOperation
    {
        public override string AskIfPopup()
        {
            return "DoubleParamRadiometricCustom_Popup";
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

            if (args.Count < 2)
                return null;

            int radio = args[0];
            int value = args[1];

            Bitmap bitmap = service.data.LastData().Bitmap;
            Bitmap result = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);

            Point center = new Point(bitmap.Width / 2, bitmap.Height / 2);

            for (int w = 0; w < bitmap.Width; ++w)
            {
                for (int h = 0; h < bitmap.Height; ++h)
                {
                    int currentRadio = CalculateRadio(new Point(w, h), center);
                    Color pixel = bitmap.GetPixel(w, h);

                    if (currentRadio < radio)
                    {
                        result.SetPixel(w, h, pixel);
                        continue;
                    }

                    result.SetPixel(w, h, Color.FromArgb(
                        pixel.A,
                        (pixel.R + value < 255) ? pixel.R + value : 255,
                        (pixel.G + value < 255) ? pixel.G + value : 255,
                        (pixel.B + value < 255) ? pixel.B + value : 255
                        ));
                }
            }
            return new ImageData(result, service.data.LastData().ID);
        }

        private int CalculateRadio(Point current, Point center)
        {
            double a = (center.X - current.X) * (center.X - current.X);
            double b = (center.Y - current.Y) * (center.Y - current.Y);

            return (int)Math.Sqrt(a + b);
        }
    }

    public class RadiometricExternDarkening : IOperation
    {
        public override string AskIfPopup()
        {
            return "DoubleParamRadiometricCustom_Popup";
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

            if (args.Count < 2)
                return null;

            int radio = args[0];
            int value = args[1];

            Bitmap bitmap = service.data.LastData().Bitmap;
            Bitmap result = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);

            Point center = new Point(bitmap.Width / 2, bitmap.Height / 2);

            for (int w = 0; w < bitmap.Width; ++w)
            {
                for (int h = 0; h < bitmap.Height; ++h)
                {
                    int currentRadio = CalculateRadio(new Point(w, h), center);
                    Color pixel = bitmap.GetPixel(w, h);

                    if (currentRadio < radio)
                    {
                        result.SetPixel(w, h, pixel);
                        continue;
                    }

                    result.SetPixel(w, h, Color.FromArgb(
                        pixel.A,
                        (pixel.R - value > 0) ? pixel.R - value : 0,
                        (pixel.G - value > 0) ? pixel.G - value : 0,
                        (pixel.B - value > 0) ? pixel.B - value : 0
                        ));
                }
            }

            return new ImageData(result, service.data.LastData().ID);
        }


        private int CalculateRadio(Point current, Point center)
        {
            double a = (center.X - current.X) * (center.X - current.X);
            double b = (center.Y - current.Y) * (center.Y - current.Y);

            return (int)Math.Sqrt(a + b);
        }
    }

    // ----------------------------------------------------------------------
    public class RadiometricSmoothDarkening : IOperation
    {
        public override string AskIfPopup()
        {
            return "DoubleParamRadiometricCustom_Popup";
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

            if (args.Count < 2)
                return null;

            int radius = args[0];
            int value = args[1];

            Bitmap bitmap = service.data.LastData().Bitmap;
            Bitmap result = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);

            Point center = new Point(bitmap.Width / 2, bitmap.Height / 2);

            for (int w = 0; w < bitmap.Width; ++w)
            {
                for (int h = 0; h < bitmap.Height; ++h)
                {
                    int currentRadius = CalculateRadius(new Point(w,h),center);
                    Color pixel = bitmap.GetPixel(w, h);

                    if (currentRadius > radius)
                    {
                        result.SetPixel(w, h, pixel);
                        continue;
                    }

                    int tmpvalue = CalculateValue(value, radius, currentRadius);
                    
                    result.SetPixel(w, h, Color.FromArgb(
                        pixel.A,
                        (pixel.R - tmpvalue > 0) ? pixel.R - tmpvalue : 0,
                        (pixel.G - tmpvalue > 0) ? pixel.G - tmpvalue : 0,
                        (pixel.B - tmpvalue > 0) ? pixel.B - tmpvalue : 0
                        ));
                }
            }

            return new ImageData(result, service.data.LastData().ID);
        }


        private int CalculateRadius(Point current, Point center)
        {
            double a = (center.X - current.X) * (center.X - current.X);
            double b = (center.Y - current.Y) * (center.Y - current.Y);

            return (int)Math.Sqrt(a + b);
        }

        private int CalculateValue(int value, int radius, int currentradius)
        {
            // First:
            //       radius  -  100%
            // currentradius  -  x%
            int x = (currentradius * 100) / radius;

            // Next:
            //  value  -  100%
            // result  -  x%
            int result = (value * x) / 100;

            // Final Invertion:
            return value - result;
        }
    }

    public class RadiometricInvertedSmoothDarkening : IOperation
    {
        public override string AskIfPopup()
        {
            return "DoubleParamRadiometricCustom_Popup";
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

            if (args.Count < 2)
                return null;

            int radius = args[0];
            int value = args[1];

            Bitmap bitmap = service.data.LastData().Bitmap;
            Bitmap result = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);

            Point center = new Point(bitmap.Width / 2, bitmap.Height / 2);

            for (int w = 0; w < bitmap.Width; ++w)
            {
                for (int h = 0; h < bitmap.Height; ++h)
                {
                    int currentRadius = CalculateRadius(new Point(w, h), center);
                    Color pixel = bitmap.GetPixel(w, h);

                    if (currentRadius > radius)
                    {
                        result.SetPixel(w, h, pixel);
                        continue;
                    }

                    int tmpvalue = CalculateValue(value, radius, currentRadius);

                    result.SetPixel(w, h, Color.FromArgb(
                        pixel.A,
                        (pixel.R - tmpvalue > 0) ? pixel.R - tmpvalue : 0,
                        (pixel.G - tmpvalue > 0) ? pixel.G - tmpvalue : 0,
                        (pixel.B - tmpvalue > 0) ? pixel.B - tmpvalue : 0
                        ));
                }
            }

            return new ImageData(result, service.data.LastData().ID);
        }


        private int CalculateRadius(Point current, Point center)
        {
            double a = (center.X - current.X) * (center.X - current.X);
            double b = (center.Y - current.Y) * (center.Y - current.Y);

            return (int)Math.Sqrt(a + b);
        }

        private int CalculateValue(int value, int radius, int currentradius)
        {
            // First:
            //       radius  -  100%
            // currentradius  -  x%
            int x = (currentradius * 100) / radius;

            // Next:
            //  value  -  100%
            // result  -  x%
            return (value * x) / 100;
        }
    }

    // ----------------------------------------------------------------------
    public class RadiometricSmoothLightening : IOperation
    {
        public override string AskIfPopup()
        {
            return "DoubleParamRadiometricCustom_Popup";
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

            if (args.Count < 2)
                return null;

            int radius = args[0];
            int value = args[1];

            Bitmap bitmap = service.data.LastData().Bitmap;
            Bitmap result = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);

            Point center = new Point(bitmap.Width / 2, bitmap.Height / 2);

            for (int w = 0; w < bitmap.Width; ++w)
            {
                for (int h = 0; h < bitmap.Height; ++h)
                {
                    int currentRadius = CalculateRadio(new Point(w, h), center);
                    Color pixel = bitmap.GetPixel(w, h);

                    if (currentRadius > radius)
                    {
                        result.SetPixel(w, h, pixel);
                        continue;
                    }

                    int tmpvalue = CalculateValue(value, radius, currentRadius);

                    result.SetPixel(w, h, Color.FromArgb(
                        pixel.A,
                        (pixel.R + tmpvalue < 255) ? pixel.R + tmpvalue : 255,
                        (pixel.G + tmpvalue < 255) ? pixel.G + tmpvalue : 255,
                        (pixel.B + tmpvalue < 255) ? pixel.B + tmpvalue : 255
                        ));
                }
            }
            return new ImageData(result, service.data.LastData().ID);
        }

        private int CalculateRadio(Point current, Point center)
        {
            double a = (center.X - current.X) * (center.X - current.X);
            double b = (center.Y - current.Y) * (center.Y - current.Y);

            return (int)Math.Sqrt(a + b);
        }
        private int CalculateValue(int value, int radius, int currentradius)
        {
            // First:
            //       radius  -  100%
            // currentradius  -  x%
            int x = (currentradius * 100) / radius;

            // Next:
            //  value  -  100%
            // result  -  x%
            int result = (value * x) / 100;

            // Final Invertion:
            return value - result;
        }
    }

    public class RadiometricInvertedSmoothLightening : IOperation
    {
        public override string AskIfPopup()
        {
            return "DoubleParamRadiometricCustom_Popup";
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

            if (args.Count < 2)
                return null;

            int radius = args[0];
            int value = args[1];

            Bitmap bitmap = service.data.LastData().Bitmap;
            Bitmap result = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);

            Point center = new Point(bitmap.Width / 2, bitmap.Height / 2);

            for (int w = 0; w < bitmap.Width; ++w)
            {
                for (int h = 0; h < bitmap.Height; ++h)
                {
                    int currentRadius = CalculateRadio(new Point(w, h), center);
                    Color pixel = bitmap.GetPixel(w, h);

                    if (currentRadius > radius)
                    {
                        result.SetPixel(w, h, pixel);
                        continue;
                    }

                    int tmpvalue = CalculateValue(value, radius, currentRadius);

                    result.SetPixel(w, h, Color.FromArgb(
                        pixel.A,
                        (pixel.R + tmpvalue < 255) ? pixel.R + tmpvalue : 255,
                        (pixel.G + tmpvalue < 255) ? pixel.G + tmpvalue : 255,
                        (pixel.B + tmpvalue < 255) ? pixel.B + tmpvalue : 255
                        ));
                }
            }
            return new ImageData(result, service.data.LastData().ID);
        }

        private int CalculateRadio(Point current, Point center)
        {
            double a = (center.X - current.X) * (center.X - current.X);
            double b = (center.Y - current.Y) * (center.Y - current.Y);

            return (int)Math.Sqrt(a + b);
        }
        private int CalculateValue(int value, int radius, int currentradius)
        {
            // First:
            //       radius  -  100%
            // currentradius  -  x%
            int x = (currentradius * 100) / radius;

            // Next:
            //  value  -  100%
            // result  -  x%
            return (value * x) / 100;
        }
    }

    // ----------------------------------------------------------------------
    public class RadiometricExternSmoothLightening : IOperation
    {
        public override string AskIfPopup()
        {
            return "DoubleParamRadiometricCustom_Popup";
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

            if (args.Count < 2)
                return null;

            int radius = args[0];
            int value = args[1];

            Bitmap bitmap = service.data.LastData().Bitmap;
            Bitmap result = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);

            Point center = new Point(bitmap.Width / 2, bitmap.Height / 2);

            for (int w = 0; w < bitmap.Width; ++w)
            {
                for (int h = 0; h < bitmap.Height; ++h)
                {
                    int currentRadius = CalculateRadio(new Point(w, h), center);
                    Color pixel = bitmap.GetPixel(w, h);

                    if (currentRadius < radius)
                    {
                        result.SetPixel(w, h, pixel);
                        continue;
                    }

                    int tmpvalue = CalculateValue(value, radius, currentRadius);

                    result.SetPixel(w, h, Color.FromArgb(
                        pixel.A,
                        (pixel.R + tmpvalue < 255) ? pixel.R + tmpvalue : 255,
                        (pixel.G + tmpvalue < 255) ? pixel.G + tmpvalue : 255,
                        (pixel.B + tmpvalue < 255) ? pixel.B + tmpvalue : 255
                        ));
                }
            }
            return new ImageData(result, service.data.LastData().ID);
        }

        private int CalculateRadio(Point current, Point center)
        {
            double a = (center.X - current.X) * (center.X - current.X);
            double b = (center.Y - current.Y) * (center.Y - current.Y);

            return (int)Math.Sqrt(a + b);
        }
        private int CalculateValue(int value, int radius, int currentradius)
        {
            // First:
            //        radius  -  100%
            // currentradius  -  x%
            int x = (currentradius * 100) / radius;

            // Next:
            //  value  -  100%
            // result  -  x%
            int result = (value * x) / 100;

            // Final Invertion:
            return result - value;
        }
    }

    public class RadiometricExternSmoothDarkening : IOperation
    {
        public override string AskIfPopup()
        {
            return "DoubleParamRadiometricCustom_Popup";
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

            if (args.Count < 2)
                return null;

            int radius = args[0];
            int value = args[1];

            Bitmap bitmap = service.data.LastData().Bitmap;
            Bitmap result = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);

            Point center = new Point(bitmap.Width / 2, bitmap.Height / 2);

            for (int w = 0; w < bitmap.Width; ++w)
            {
                for (int h = 0; h < bitmap.Height; ++h)
                {
                    int currentRadius = CalculateRadio(new Point(w, h), center);
                    Color pixel = bitmap.GetPixel(w, h);

                    if (currentRadius < radius)
                    {
                        result.SetPixel(w, h, pixel);
                        continue;
                    }

                    int tmpvalue = CalculateValue(value, radius, currentRadius);

                    result.SetPixel(w, h, Color.FromArgb(
                        pixel.A,
                        (pixel.R - tmpvalue > 0) ? pixel.R - tmpvalue : 0,
                        (pixel.G - tmpvalue > 0) ? pixel.G - tmpvalue : 0,
                        (pixel.B - tmpvalue > 0) ? pixel.B - tmpvalue : 0
                        ));
                }
            }

            return new ImageData(result, service.data.LastData().ID);
        }


        private int CalculateRadio(Point current, Point center)
        {
            double a = (center.X - current.X) * (center.X - current.X);
            double b = (center.Y - current.Y) * (center.Y - current.Y);

            return (int)Math.Sqrt(a + b);
        }
        private int CalculateValue(int value, int radius, int currentradius)
        {
            // First:
            //       radius  -  100%
            // currentradius  -  x%
            int x = (currentradius * 100) / radius;

            // Next:
            //  value  -  100%
            // result  -  x%
            int result = (value * x) / 100;

            // Final Invertion:
            return result - value;
        }
    }
}
