using KK17413_APO_REMASTER.BackEnd.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations
{
    class HistogramOperations : OperationsFamily
    {
        public HistogramOperations()
        {
            operations_Dict = new Dictionary<string, IOperation>()
            {
                { "RecalculateHistogramData_tsmi", new RecalculateHistogramData() },
                { "stuff", null }
            };
        }
    }



    public class RecalculateHistogramData : IOperation
    {
        public static ImageData GetResult(ImageData lastData)
        {
            ImageData newData = new ImageData();





            return newData;
        }
    }
}


