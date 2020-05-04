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

            List<HistogramPanel_Data> result = new List<HistogramPanel_Data>()
            {
                new HistogramPanel_Data(),
                new HistogramPanel_Data(),
                new HistogramPanel_Data(),
                new HistogramPanel_Data(),
                new HistogramPanel_Data()
            };


            //for (int i = 0; i < result.Count; ++i)
                //for (int index = 0; index < result[i].data.Capacity; ++index)
                    //result[i].Add(0);

            for (int h = 0; h < bitmap.Height; ++h)
            {
                for (int w = 0; w < bitmap.Width; ++w)
                {
                    result[0].SumUp(bitmap.GetPixel(w, h).R);
                    result[0].SumUp(bitmap.GetPixel(w, h).G);
                    result[0].SumUp(bitmap.GetPixel(w, h).B);

                    result[1].SumUp(bitmap.GetPixel(w, h).A);
                    result[2].SumUp(bitmap.GetPixel(w, h).R);
                    result[3].SumUp(bitmap.GetPixel(w, h).G);
                    result[4].SumUp(bitmap.GetPixel(w, h).B);

                    //result[0].data[bitmap.GetPixel(w, h).R] += 1;
                    //result[0].data[bitmap.GetPixel(w, h).G] += 1;
                    //result[0].data[bitmap.GetPixel(w, h).B] += 1;

                    //result[1].data[bitmap.GetPixel(w, h).A] += 1;
                    //result[2].data[bitmap.GetPixel(w, h).R] += 1;
                    //result[3].data[bitmap.GetPixel(w, h).G] += 1;
                    //result[4].data[bitmap.GetPixel(w, h).B] += 1;
                }
            }

            for (int i = 0; i < pages.Count; ++i)
            {
                result[i].SetLeast();
                pages[i].ReloadHistogram(result[i]);
            }

            //*/

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


            result.pages = Get_pages();
            result.tabControl = Get_tabControl(result);

            result.Height = result.tabControl.Height + (30 * 6);
            result.Controls.Add(result.tabControl);

            result.Initialized = true;
            return result;
        }



        private static List<HistogramPanel> Get_pages()
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
            tabControl.AddPage("General", result.pages[0]);
            tabControl.AddPage("Alpha", result.pages[1]);
            tabControl.AddPage("Red", result.pages[2]);
            tabControl.AddPage("Green", result.pages[3]);
            tabControl.AddPage("Blue", result.pages[4]);

            return tabControl;
        }
    }
    #endregion
    // ##########################################################################################################################
    // ##########################################################################################################################
}
