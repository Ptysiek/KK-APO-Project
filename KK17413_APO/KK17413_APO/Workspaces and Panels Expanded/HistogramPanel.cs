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
        public int PageWidth
        {
            get => (data.Count > 0)? data[0].Histogram.Width : 0;
        }

        //public TabControl tabControl;
        public AdjustedTabControl tabControl;
        private List<HistogramPanel_DataStructure> data;
        private bool Initialized;
        private Bitmap bitmap;

        private Label pipe_0;
        private Label pipe_64;
        private Label pipe_128;
        private Label pipe_192;
        private Label pipe_255;
        
        public HistogramPanel()
        {
            // ---------------------------------------------------------------------------
            pipe_0 = new Label()
            {
                Text = "0",
                AutoSize = true,
                AutoEllipsis = false,
                Visible = false
            };
            pipe_64 = new Label()
            {
                Text = "64",
                AutoSize = true,
                AutoEllipsis = false,
                Visible = false
            };
            pipe_128 = new Label()
            {
                Text = "128",
                AutoSize = true,
                AutoEllipsis = false,
                Visible = false
            };
            pipe_192 = new Label()
            {
                Text = "192",
                AutoSize = true,
                AutoEllipsis = false,
                Visible = false
            };
            pipe_255 = new Label()
            {
                Text = "255",
                AutoSize = true,
                AutoEllipsis = false,
                Visible = false
            };

            this.Controls.Add(pipe_0);
            this.Controls.Add(pipe_64);
            this.Controls.Add(pipe_128);
            this.Controls.Add(pipe_192);
            this.Controls.Add(pipe_255);

            // ---------------------------------------------------------------------------
            tabControl = new AdjustedTabControl();
            data = new List<HistogramPanel_DataStructure>();
            
            data.Add(new HistogramPanel_DataStructure(Color.White));
            data.Add(new HistogramPanel_DataStructure(Color.White));
            data.Add(new HistogramPanel_DataStructure(Color.Red));
            data.Add(new HistogramPanel_DataStructure(Color.Green));
            data.Add(new HistogramPanel_DataStructure(Color.Blue));

            //tabControl.Height = PageHeight + tabControl.ButtonContainerHeight + (30 * 6);
            tabControl.Height = PageHeight + tabControl.ButtonContainerHeight;
            tabControl.Dock = DockStyle.None;

            // ---------------------------------------------------------------------------
            tabControl.AddPage("General", data[0].Page);
            tabControl.AddPage("Alpha", data[1].Page);
            tabControl.AddPage("Red", data[2].Page);
            tabControl.AddPage("Green", data[3].Page);
            tabControl.AddPage("Blue", data[4].Page);   

            // ---------------------------------------------------------------------------
            this.Height = tabControl.Height + (30 * 6);
            this.Controls.Add(tabControl);

            // ---------------------------------------------------------------------------
            pipe_0.Left = 0;
            pipe_0.Top = tabControl.Height;

            pipe_255.Left = PageWidth - (pipe_255.Width / 2) - 6;
            pipe_255.Top = tabControl.Height;

            pipe_128.Left = PageWidth/2 - (pipe_128.Width / 2) - 6;
            pipe_128.Top = tabControl.Height;

            pipe_64.Left = PageWidth/4 - (pipe_64.Width / 2) - 6;
            pipe_64.Top = tabControl.Height;

            pipe_192.Left = PageWidth/2 + PageWidth/4 - (pipe_192.Width / 2) - 6;
            pipe_192.Top = tabControl.Height;

            // ---------------------------------------------------------------------------
            Initialized = true;
        }

        public void ShowLabels()
        {
            pipe_0.Visible = true;
            pipe_64.Visible = true;
            pipe_128.Visible = true;
            pipe_192.Visible = true;
            pipe_255.Visible = true;
        }

        public void AssignBitmap(Bitmap bitmap)
        {
            Initialized = false;
            this.bitmap = bitmap;
        }

        public void RecalculateHistograms()
        {
            if (Initialized) return;

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

            Initialized = true;
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
