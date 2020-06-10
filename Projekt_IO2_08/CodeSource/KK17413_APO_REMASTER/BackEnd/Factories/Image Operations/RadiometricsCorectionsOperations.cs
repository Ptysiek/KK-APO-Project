using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;
using KK17413_APO_REMASTER.BackEnd.DataStructures;


namespace KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations
{
    class RadiometricsCorectionsOperations : IOperationsFamily
    {
        public RadiometricsCorectionsOperations()
        {
            operations_Dict = new Dictionary<string, IOperation>()
            {
                { "PointAutoCorrection_tsmi", new PointAutoCorrection() },
            };
        }
    }

    // ----------------------------------------------------------------------
    public class PointAutoCorrection : IOperation
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
            if (service.data.LastData() == null)
                return null;

            if (service.data.LastData().Bitmap == null)
                return null;

            Image<Bgra, byte> image = new Image<Bgra, byte>(service.data.LastData().Bitmap);
            Bitmap conversionbitmap = image.Bitmap;

            List<List<List<int>>> referenceMatrix = CalculateReferenceMatrix(conversionbitmap);

            List<List<int>> averageVector = CalculateAverageValueVector(referenceMatrix);




            Bitmap bitmap = service.data.LastData().Bitmap;
            Bitmap result = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);

            Point center = new Point(bitmap.Width / 2, bitmap.Height / 2);

            List<List<int>> correctionVector = CalculateCorrectionVector(averageVector, bitmap.GetPixel(center.X, center.Y));



            for (int w = 0; w < bitmap.Width; ++w)
            {
                for (int h = 0; h < bitmap.Height; ++h)
                {
                    int currentRadius = CalculateRadius(new Point(w, h), center);
                    Color pixel = bitmap.GetPixel(w, h);

                    int r, g, b;

                    /*
                    if (pixel.R - correctionVector[0][currentRadius] > 255)         r = 255;
                    else if (pixel.R - correctionVector[0][currentRadius] < 0)      r = 0;
                    else                                                            r = pixel.R - correctionVector[0][currentRadius];    
                    
                    if (pixel.G - correctionVector[1][currentRadius] > 255)         g = 255;
                    else if (pixel.G - correctionVector[1][currentRadius] < 0)      g = 0;
                    else                                                            g = pixel.G - correctionVector[1][currentRadius];                    

                    if (pixel.B - correctionVector[2][currentRadius] > 255)         b = 255;
                    else if (pixel.B - correctionVector[2][currentRadius] < 0)      b = 0;
                    else                                                            b = pixel.B - correctionVector[2][currentRadius];
                    */

                    int value = correctionVector[0][currentRadius] +
                                correctionVector[1][currentRadius] +
                                correctionVector[2][currentRadius];

                    value /= 3;

                    if (pixel.R - value > 255)      r = 255;
                    else if (pixel.R - value < 0)   r = 0;
                    else                            r = pixel.R - value;                    

                    if (pixel.G - value > 255)      g = 255;
                    else if (pixel.G - value < 0)   g = 0;
                    else                            g = pixel.G - value;                    

                    if (pixel.B - value > 255)      b = 255;
                    else if (pixel.B - value < 0)   b = 0;
                    else                            b = pixel.B - value;

                    result.SetPixel(w, h, Color.FromArgb(pixel.A, r, g, b));
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

        private List<List<List<int>>> CalculateReferenceMatrix(Bitmap bitmap) 
        {
            List<List<List<int>>> result = new List<List<List<int>>>(3);

            for (int i=0; i<3; ++i)
            {
                result.Add(new List<List<int>>(bitmap.Width * bitmap.Height * 2));

                for (int j=0; j<bitmap.Width*bitmap.Height*2; ++j)
                {
                    result[i].Add(new List<int>());
                }
            }

            Point center = new Point(bitmap.Width / 2, bitmap.Height / 2);
            for (int w = 0; w < bitmap.Width; ++w)
            {
                for (int h = 0; h < bitmap.Height; ++h)
                {
                    int currentRadius = CalculateRadius(new Point(w, h), center);
                    Color pixel = bitmap.GetPixel(w, h);

                    result[0][currentRadius].Add(pixel.R);
                    result[1][currentRadius].Add(pixel.G);
                    result[2][currentRadius].Add(pixel.B);
                }
            }
            return result;
        }

        private List<List<int>> CalculateAverageValueVector(List<List<List<int>>> referenceMatrix)
        {
            List<List<int>> result = new List<List<int>>(3);

            for (int i = 0; i < 3; ++i)
            {
                result.Add(new List<int>(referenceMatrix[i].Count));

                for (int j = 0; j < referenceMatrix[i].Count; ++j)
                {
                    result[i].Add(new int());
                }
            }

            for (int c = 0; c < 3; ++c)
            {
                for (int r=0; r<referenceMatrix[c].Count; ++r)
                {
                    int sum = 0;
                    int count = 0;

                    foreach(int value in referenceMatrix[c][r])
                    {
                        
                        sum += value;
                        ++count;
                    }

                    if (count != 0)
                        result[c][r] = sum / count;
                }
            }

            return result;
        }

        private List<List<int>> CalculateCorrectionVector(List<List<int>> averageVector, Color pixel)
        {
            List<List<int>> result = new List<List<int>>(3);

            for (int c = 0; c < 3; ++c)
            {
                result.Add(new List<int>(averageVector[c].Count));

                for (int j = 0; j < averageVector[c].Count; ++j)
                {
                    result[c].Add(new int());
                }
            }

            List<int> color = new List<int>()
            {
                pixel.R,
                pixel.G,
                pixel.B
            };


            for (int c = 0; c < 3; ++c)
            {
                //result[c][0] = averageVector[c][0];

                for (int r = 0; r < averageVector[c].Count; ++r)
                {
                    /*
                    if (result[c][r - 1] == 0)
                        result[c][r] = (averageVector[c][r] - averageVector[c][r - 1]);
                    else
                        result[c][r] = (averageVector[c][r] - averageVector[c][r - 1]) / result[c][r - 1];
                    */

                    result[c][r] = averageVector[c][r] - color[c];

                }
            }

            return result;
        }

    }


}
