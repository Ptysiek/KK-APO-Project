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
            return modifications.Last();
        }



        public void Clear()
        {
            for (int i=0; i<modifications.Count; ++i)
            {
                modifications[i].Clear();
                modifications[i] = null;
            }
            modifications = null;
        }

        public void AssignData(string filename)
        {
            if (filename == null) return;

            Bitmap bitmap = new Bitmap(filename);

            //HistogramCalculatePermision = true;

            modifications.Add(new ImageData(bitmap, filename));
        }
    }
}
