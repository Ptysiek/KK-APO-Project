using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using KK17413_APO_REMASTER.BackEnd.Factories;
using KK17413_APO_REMASTER.BackEnd.DataStructures;
using KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded;


namespace KK17413_APO_REMASTER.FrontEnd.Views_and_Expanded_Panels
{
    [System.ComponentModel.DesignerCategory("")]
    public class HistogramTabControl : Panel
    {
        public int PageHeight { get => (pages.Count > 0) ? pages[0].Height : 0; }
        public int PageWidth { get => (pages.Count > 0) ? pages[0].Histogram.Width : 0; }

        public AdjustedTabControl tabControl;
        public List<HistogramPanel> pages;


        ImageData imageData;

        public void ReloadColorSet(ColorSet ColorSet)
        {
            foreach (var page in pages)
                page.ReloadColorSet(ColorSet);
        }

        public void RecalculateHistograms(ref ProgressBar pbar)
        {
            if (imageData == null) return;
            if (!imageData.Ready)
            {
                imageData.RecalculateHistograms(ref pbar);
                pbar.Visible = false;
            }
            pages[0].ReloadHistogram(imageData.data);
            pages[1].ReloadHistogram(imageData.data_A);
            pages[2].ReloadHistogram(imageData.data_R);
            pages[3].ReloadHistogram(imageData.data_G);
            pages[4].ReloadHistogram(imageData.data_B);
        }

        public void AssignBitmap(Bitmap bitmap, string filename)
        {
            imageData = new ImageData(bitmap, filename);
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

            //result.Initialized = true;
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
