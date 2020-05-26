using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.Generic;
using System.Globalization;

using KK17413_APO_REMASTER.BackEnd.DataStructures;


namespace KK17413_APO_REMASTER.BackEnd.ImageFormComponents
{
    public class ImageForm_Data
    {

        public List<Modification> modifications = new List<Modification>();



        public Modification Last()
        {
            if (modifications == null) return null;
            if (modifications.Count < 1) return null;

            return modifications.Last();
        }

        public ImageData LastData()
        {
            if (modifications == null) return null;
            if (modifications.Count < 1) return null;

            return modifications.Last().data;
        }
        
        public ImageData UpdateLastData(ImageData updatedData)
        {
            if (modifications == null) return null;
            if (modifications.Count < 1) return null;

            //modifications.RemoveAt(modifications.Count() - 1);
            //modifications.Add(updatedData);
            modifications.Last().data = updatedData;
            return modifications.Last().data;
        }


        public void CreateNewData(string filename)
        {
            if (filename == null) return;

            Bitmap bitmap = new Bitmap(filename);

            Add(new ImageData(bitmap, filename), "Create New Data");
        }

        public Modification Add(ImageData newData, string info)
        {
            if (newData == null)
                return null;

            DateTime localDate = DateTime.Now;

            Modification newModification = new Modification
            {
                data = newData,
                time = localDate.ToString(),
                info = info
            };

            modifications.Add(newModification);
            return newModification;
        }

    }
}
