using KK17413_APO.Panels_Expanded;
using KK17413_APO.Toolbox_Tools_Expanded;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KK17413_APO.Workspaces_and_Panels_Expanded
{
    [System.ComponentModel.DesignerCategory("")]
    public class HistogramTabControl : Panel
    { 
        public int PageHeight { get => (pages.Count > 0) ? pages[0].Height : 0; }
        public int PageWidth { get => (pages.Count > 0) ? pages[0].Histogram.Width : 0; }

        public AdjustedTabControl tabControl;
        public List<HistogramPanel> pages;
        public bool Initialized;
        private Bitmap bitmap;

        public void ReloadColorSet()
        {
            foreach (var page in pages)
                page.ReloadColorSet();
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

            for (int i = 0; i < pages.Count; ++i)
                pages[i].ReloadHistogram(result[i]);

            Initialized = true;
        }

    }


    // ##########################################################################################################################
    // ##########################################################################################################################
    #region HistogramPanel_Builder
    class HistogramTabControl_Builder
    {
        public static HistogramTabControl GetResult()
        {
            HistogramTabControl result = new HistogramTabControl();


            result.data = Get_data();
            result.tabControl = Get_tabControl(result);

            result.Height = result.tabControl.Height + (30 * 6);
            result.Controls.Add(result.tabControl);

            result.Initialized = true;
            return result;
        }



        private static List<HistogramPanel> Get_data()
        {
            return new List<HistogramPanel>()
            {
                HistogramPanel_Builder.GetResult(Color.White),
                HistogramPanel_Builder.GetResult(Color.White),
                HistogramPanel_Builder.GetResult(Color.Red),
                HistogramPanel_Builder.GetResult(Color.Green),
                HistogramPanel_Builder.GetResult(Color.Blue)
            };
        }

        private static AdjustedTabControl Get_tabControl(HistogramTabControl result)
        {
            AdjustedTabControl tabControl = new AdjustedTabControl();

            tabControl.Height = result.PageHeight + tabControl.ButtonContainerHeight;
            tabControl.Dock = DockStyle.None;

            // ---------------------------------------------------------------------------
            tabControl.AddPage("General", result.data[0]);
            tabControl.AddPage("Alpha", result.data[1]);
            tabControl.AddPage("Red", result.data[2]);
            tabControl.AddPage("Green", result.data[3]);
            tabControl.AddPage("Blue", result.data[4]);

            return tabControl;
        }
    }
    #endregion
    // ##########################################################################################################################
    // ##########################################################################################################################
}
