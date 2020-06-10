using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using KK17413_APO_REMASTER.BackEnd.Factories;
using KK17413_APO_REMASTER.BackEnd.DataStructures;
using KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded;
using System;

namespace KK17413_APO_REMASTER.FrontEnd.Views_and_Expanded_Panels
{
    [System.ComponentModel.DesignerCategory("")]
    public class Panel_HistogramTabControl : Panel
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

    #region Panel_HistogramTabControl_Builder
    class Panel_HistogramTabControl_Builder
    {
        public static Panel_HistogramTabControl GetResult()
        {
            Panel_HistogramTabControl result = new Panel_HistogramTabControl
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
                Panel_HistogramInfoPanel_Builder.GetResult(Color.White),
                Panel_HistogramInfoPanel_Builder.GetResult(Color.White),
                Panel_HistogramInfoPanel_Builder.GetResult(Color.Red),
                Panel_HistogramInfoPanel_Builder.GetResult(Color.Green),
                Panel_HistogramInfoPanel_Builder.GetResult(Color.Blue)
            };
        }

        private static AdjustedTabControl Get_tabControl(Panel_HistogramTabControl result)
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
