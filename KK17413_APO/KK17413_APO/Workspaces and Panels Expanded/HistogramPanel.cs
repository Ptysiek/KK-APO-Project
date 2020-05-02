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
        public int PageHeight { get => (data.Count > 0)? data[0].Page.Height : 0; }
        public int PageWidth { get => (data.Count > 0)? data[0].Histogram.Width : 0; }

        public AdjustedTabControl tabControl;
        public List<HistogramPanel_DataStructure> data;
        public bool Initialized;
        private Bitmap bitmap;

        public Label pipe_0;
        public Label pipe_64;
        public Label pipe_128;
        public Label pipe_192;
        public Label pipe_255;
        

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
        
        public void Configure_PipePosition()
        {
            pipe_0.Left = 0;
            pipe_0.Top = tabControl.Height;

            pipe_255.Left = PageWidth - (pipe_255.Width / 2) - 6;
            pipe_255.Top = tabControl.Height;

            pipe_128.Left = PageWidth / 2 - (pipe_128.Width / 2) - 6;
            pipe_128.Top = tabControl.Height;

            pipe_64.Left = PageWidth / 4 - (pipe_64.Width / 2) - 6;
            pipe_64.Top = tabControl.Height;

            pipe_192.Left = PageWidth / 2 + PageWidth / 4 - (pipe_192.Width / 2) - 6;
            pipe_192.Top = tabControl.Height;
        }
    }

    // ##########################################################################################################################
    // ##########################################################################################################################
    #region HistogramPanel_DataStructure
    public class HistogramPanel_DataStructure
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
    #endregion
    // ##########################################################################################################################
    // ##########################################################################################################################
    #region HistogramPanel_Builder
    class HistogramPanel_Builder
    {
        public static HistogramPanel GetResult()
        {
            HistogramPanel result = new HistogramPanel()
            {
                pipe_0 = Get_pipe("0"),
                pipe_64 = Get_pipe("64"),
                pipe_128 = Get_pipe("128"),
                pipe_192 = Get_pipe("192"),
                pipe_255 = Get_pipe("255")
            };

            result.Controls.AddRange(new Control[]{
                result.pipe_0,
                result.pipe_64,
                result.pipe_128,
                result.pipe_192,
                result.pipe_255
            });

            result.data = Get_data();
            result.tabControl = Get_tabControl(result);

            result.Height = result.tabControl.Height + (30 * 6);
            result.Controls.Add(result.tabControl);

            result.Configure_PipePosition();

            result.Initialized = true;
            return result;
        }

        private static Label Get_pipe(string value)
        {
            return new Label()
            {
                Text = value,
                AutoSize = true,
                AutoEllipsis = false,
                Visible = false
            };
        }

        private static List<HistogramPanel_DataStructure> Get_data()
        {
            return new List<HistogramPanel_DataStructure>()
            {
                new HistogramPanel_DataStructure(Color.White),
                new HistogramPanel_DataStructure(Color.White),
                new HistogramPanel_DataStructure(Color.Red),
                new HistogramPanel_DataStructure(Color.Green),
                new HistogramPanel_DataStructure(Color.Blue)
            };
        }
        
        private static AdjustedTabControl Get_tabControl(HistogramPanel result)
        {
            AdjustedTabControl tabControl = new AdjustedTabControl();

            tabControl.Height = result.PageHeight + tabControl.ButtonContainerHeight;
            tabControl.Dock = DockStyle.None;

            // ---------------------------------------------------------------------------
            tabControl.AddPage("General", result.data[0].Page);
            tabControl.AddPage("Alpha", result.data[1].Page);
            tabControl.AddPage("Red", result.data[2].Page);
            tabControl.AddPage("Green", result.data[3].Page);
            tabControl.AddPage("Blue", result.data[4].Page);

            return tabControl;
        }
    }
    #endregion
    // ##########################################################################################################################
    // ##########################################################################################################################
}
