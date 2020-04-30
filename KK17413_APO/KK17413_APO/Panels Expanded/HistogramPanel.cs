using KK17413_APO.Toolbox_Tools_Expanded;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KK17413_APO.Panels_Expanded
{
    [System.ComponentModel.DesignerCategory("")]
    public class HistogramPanel : Panel
    {

        public int PageHeight
        {
            get => (data.Count > 0)? data[0].Page.Height : 0;
        }

        //public TabControl tabControl;
        public AdjustedTabControl tabControl;
        private List<HistogramPanel_DataStructure> data;


        public HistogramPanel()
        {
            // ---------------------------------------------------------------------------
            tabControl = new AdjustedTabControl();
            data = new List<HistogramPanel_DataStructure>();
            
            data.Add(new HistogramPanel_DataStructure(Color.White));
            data.Add(new HistogramPanel_DataStructure(Color.White));
            data.Add(new HistogramPanel_DataStructure(Color.Red));
            data.Add(new HistogramPanel_DataStructure(Color.Green));
            data.Add(new HistogramPanel_DataStructure(Color.Blue));

            tabControl.Height = PageHeight + tabControl.ButtonContainerHeight + (30 * 6);

            // ---------------------------------------------------------------------------
            tabControl.AddPage("General", data[0].Page);
            tabControl.AddPage("Alpha", data[1].Page);
            tabControl.AddPage("Red", data[2].Page);
            tabControl.AddPage("Green", data[3].Page);
            tabControl.AddPage("Blue", data[4].Page);   

            // ---------------------------------------------------------------------------
            this.Height = tabControl.Height;

            this.Controls.Add(tabControl);
        }






        public void RecalculateHistograms(Bitmap bitmap)
        {
            List<List<int>> result = new List<List<int>>
            {
                new List<int>(256),
                new List<int>(256),
                new List<int>(256),
                new List<int>(256),
                new List<int>(256)
            };

            for (int i = 0; i < result.Count; ++i)
                for (int index = 0; index < result[i].Capacity; ++index)
                    result[i].Add(0);

            for (int h = 0; h < bitmap.Height; ++h)
            {
                for (int w = 0; w < bitmap.Width; ++w)
                {
                    result[0][bitmap.GetPixel(w, h).R] += 1;
                    result[0][bitmap.GetPixel(w, h).G] += 1;
                    result[0][bitmap.GetPixel(w, h).B] += 1;

                    result[1][bitmap.GetPixel(w, h).A] += 1;
                    result[2][bitmap.GetPixel(w, h).R] += 1;
                    result[3][bitmap.GetPixel(w, h).G] += 1;
                    result[4][bitmap.GetPixel(w, h).B] += 1;
                }
            }

            for (int i = 0; i < data.Count; ++i)
                data[i].Histogram.ReloadHistogram(result[i]);
        }
    }

    class HistogramPanel_DataStructure
    {
        public Panel Page;

        public Histogram Histogram;

        public Label MaxValue;
        public Panel MaxValueColor;

        public Label MinValue;
        public Panel MinValueColor;


        public HistogramPanel_DataStructure(Color ForeColor)
        {
            Page = new Panel();
            Histogram = new Histogram(ForeColor);
            MaxValue = new Label();
            MaxValueColor = new Panel();
            MinValue = new Label();
            MinValueColor = new Panel();

            Page.Height = Histogram.Height;
            Page.Dock = DockStyle.Fill;

            Page.Controls.Add(Histogram);
        }
    }
}
