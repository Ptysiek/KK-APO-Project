using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK17413_APO.Image_Operations
{
    class ImageData
    {
        public string ID;
        public HistogramData data;
        public HistogramData data_A;
        public HistogramData data_R;
        public HistogramData data_G;
        public HistogramData data_B;
    }


    public class HistogramData
    {
        public List<int> data;
        public int mostValueCounter;
        public int leastValueCounter;
        public int mostValueIndex;
        public int leastValueIndex;

        public HistogramData()
        {
            data = new List<int>(new int[256]);

            // Init out of range values:
            mostValueCounter = -1;
            leastValueCounter = 257;

            // Index Vales:
            mostValueIndex = -6;
            leastValueIndex = -6;
        }

        public void SumUp(int index)
        {
            data[index] += 1;

            if (data[index] > mostValueCounter)
            {
                mostValueCounter = data[index];
                mostValueIndex = index;
            }
        }

        public void SetLeast()
        {
            leastValueCounter = mostValueCounter;
            leastValueIndex = mostValueIndex;

            for (int i = 0; i < data.Count; ++i)
            {
                if (data[i] == 0)
                    continue;

                if (data[i] < leastValueCounter)
                {
                    leastValueCounter = data[i];
                    leastValueIndex = i;
                }
            }
        }
    }
}

