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

        readonly public string ID;
        public HistogramData data;
        public HistogramData data_A;
        public HistogramData data_R;
        public HistogramData data_G;
        public HistogramData data_B;

        private Bitmap bitmap;
        private bool ready;


        public ImageData(Bitmap bitmap, string filename)
        {
            this.bitmap = bitmap;
            this.ID = filename;

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

        public void RecalculateHistograms(ref ProgressBar pbar)
        {/*
            if (ready)
                return;

            ImageData tmp = Histogram_Reload.GetResult(this, ref pbar);

            if (tmp != this)
            {
                this.data = tmp.data;
                this.data_A = tmp.data_A;
                this.data_R = tmp.data_R;
                this.data_G = tmp.data_G;
                this.data_B = tmp.data_B;
            }

            pbar.Visible = false;
            tmp.ready = true;
            ready = true;
            */
        }
    }

}

