using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using KK17413_APO_REMASTER.BackEnd.DataStructures;
using System.Drawing;

namespace KK17413_APO_REMASTER.BackEnd.ImageFormComponents
{
    public class ImageForm_Data
    {

        public List<ImageData> modifications = new List<ImageData>();


        public ImageData Last()
        {
            if (modifications == null) return null;
            if (modifications.Count < 1) return null;

            return modifications.Last();
        }


        public void CreateNewData(string filename)
        {
            if (filename == null) return;

            Bitmap bitmap = new Bitmap(filename);

            //HistogramCalculatePermision = true;

            modifications.Add(new ImageData(bitmap, filename));
        }

        public void Add(ImageData newData)
        {
            if (newData == null)
                return;

            modifications.Add(newData);
        }

    }
}
