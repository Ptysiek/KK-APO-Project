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
        public List<Histogram_InfoPanel> pages;

        public void ReloadColorSet(ColorSet ColorSet)
        {
            foreach (var page in pages)
                page.ReloadColorSet(ColorSet);
        }


        public void ShowHistogram()
        {

        }

        public void ShowHistogram_Unready()
        {

        }        

        public void ShowHistogram_NoImage()
        {

        }                

        public void GiveHistogramNewData(ImageData fulldata)
        {
            if (fulldata == null) return;
            if (!fulldata.Ready) return;

            pages[0].ReloadData(fulldata.data);
            pages[1].ReloadData(fulldata.data_A);
            pages[2].ReloadData(fulldata.data_R);
            pages[3].ReloadData(fulldata.data_G);
            pages[4].ReloadData(fulldata.data_B);
        }
    }


    // ##########################################################################################################################
    // ##########################################################################################################################

    #region HistogramTabControlPanel_Builder
    class HistogramTabControl_Builder
    {
        public static HistogramTabControl GetResult()
        {
            HistogramTabControl result = new HistogramTabControl
            {
                pages = Get_pages()
            };
            result.tabControl = Get_tabControl(result);

            result.Height = result.tabControl.Height + (30 * 6);
            result.Controls.Add(result.tabControl);

            //result.Initialized = true;
            return result;
        }

        private static List<Histogram_InfoPanel> Get_pages()
        {
            return new List<Histogram_InfoPanel>()
            {
                Histogram_InfoPanel_Builder.GetResult(Color.White),
                Histogram_InfoPanel_Builder.GetResult(Color.White),
                Histogram_InfoPanel_Builder.GetResult(Color.Red),
                Histogram_InfoPanel_Builder.GetResult(Color.Green),
                Histogram_InfoPanel_Builder.GetResult(Color.Blue)
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
