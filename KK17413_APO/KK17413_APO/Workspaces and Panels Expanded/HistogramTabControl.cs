﻿using KK17413_APO.Data_Structures;
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
        private Bitmap bitmap;

        ImageData imageData;
        bool Initialized = false;

        public void ReloadColorSet()
        {
            foreach (var page in pages)
                page.ReloadColorSet();
        }

        public void RecalculateHistograms()
        {
            if (!Initialized) return;

            imageData.RecalculateHistograms();

            pages[0].ReloadHistogram(imageData.data);
            pages[1].ReloadHistogram(imageData.data_A);
            pages[2].ReloadHistogram(imageData.data_R);
            pages[3].ReloadHistogram(imageData.data_G);
            pages[4].ReloadHistogram(imageData.data_B);
        }

        public void AssignBitmap(Bitmap bitmap)
        {
            imageData = new ImageData(bitmap);
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
