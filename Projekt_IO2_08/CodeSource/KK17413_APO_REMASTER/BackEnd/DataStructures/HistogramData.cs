using System.Collections.Generic;


namespace KK17413_APO_REMASTER.BackEnd.DataStructures
{
    public class HistogramData
    {
        public List<int> data;
        public int mostValueCounter;
        public int leastValueCounter;
        public int mostValueIndex;
        public int leastValueIndex;

        public int minValue;
        public int maxValue;

        public HistogramData()
        {
            data = new List<int>(new int[256]);

            // Init out of range values:
            mostValueCounter = -1;
            leastValueCounter = 257;

            // Index Vales:
            mostValueIndex = -6;
            leastValueIndex = -6;

            maxValue = -6;
            minValue = 257;
        }

        public void Clear()
        {
            data = null;
        }

        public void SumUp(int index)
        {
            data[index] += 1;

            if (data[index] > mostValueCounter)
            {
                mostValueCounter = data[index];
                mostValueIndex = index;
            }
            if (index > maxValue)
            {
                maxValue = index;
            }
            if (index < minValue)
            {
                minValue = index;
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