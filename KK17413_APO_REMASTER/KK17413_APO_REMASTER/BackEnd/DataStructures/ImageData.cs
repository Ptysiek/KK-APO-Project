using System.Drawing;
using System.Windows.Forms;


namespace KK17413_APO_REMASTER.BackEnd.DataStructures
{
    public class ImageData
    {
        public bool Ready { get => ready; }
        public Bitmap Bitmap
        {
            get => bitmap;
            set
            {
                bitmap = value;
                ready = false;
            }
        }

        public string ID { get => id; }
        public HistogramData data;
        public HistogramData data_A;
        public HistogramData data_R;
        public HistogramData data_G;
        public HistogramData data_B;


        private string id;
        private Bitmap bitmap;
        private bool ready;


        public ImageData(Bitmap bitmap, string filename)
        {
            this.bitmap = bitmap;
            this.id = filename;

            data = new HistogramData();
            data_A = new HistogramData();
            data_R = new HistogramData();
            data_G = new HistogramData();
            data_B = new HistogramData();

            ready = false;
        }

        public ImageData()
        {
            data = new HistogramData();
            data_A = new HistogramData();
            data_R = new HistogramData();
            data_G = new HistogramData();
            data_B = new HistogramData();
        }

        public void SetReady()
        {
            ready = true;
        }
    }
}

