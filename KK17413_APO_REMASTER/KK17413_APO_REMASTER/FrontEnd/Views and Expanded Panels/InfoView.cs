using KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace KK17413_APO_REMASTER.FrontEnd.Views_and_Expanded_Panels
{
    [System.ComponentModel.DesignerCategory("")]
    public class InfoView : Panel
    {
        public FlowLayoutPanel iwnContainer;
        public AdjustedSplitContainer histogram_iwn;
        public AdjustedSplitContainer fileInfo_iwn;
        public Panel bottomMargin_iwn;
        // *iwn - Image Workspace Nodes

        public HistogramTabControl histogramTabControl;
        public InfoPanel infoPanel;


        public void AssignEventHandlers()
        {
            this.Resize += workspace_Resize;
            histogram_iwn.ToggleButton.Click += iwn_HeightChanged;
            fileInfo_iwn.ToggleButton.Click += iwn_HeightChanged;
        }

        public void LoadHistogramPanel(ref ProgressBar pbar)
        {
            histogramTabControl.RecalculateHistograms(ref pbar);

            if (!histogram_iwn.Panel2Collapsed)
                histogramTabControl.tabControl.ShowFirstPage();

            //histogramTabControl.ShowLabels();
        }

        public void LoadInfoPanel(Bitmap bitmap, string filename)
        {
            infoPanel.ReloadImageInfo(bitmap, filename);

            fileInfo_iwn.PanelHeight = infoPanel.labelsHeight * (2 + infoPanel.labelsCount);
        }

        public int CalculateWidht()
        {
            int hist = (histogram_iwn.BodyPanelCollapsed) ? 0 : histogramTabControl.PageWidth;

            int file = (fileInfo_iwn.BodyPanelCollapsed) ? 0 : infoPanel.Width;

            return Math.Max(Math.Max(hist, file), bottomMargin_iwn.Width);
        }

        public int CalculateHeight()
        {
            return bottomMargin_iwn.Height + fileInfo_iwn.Height + histogram_iwn.Height;
        }

        public void AutoResize()
        {
            int rightPadding;

            rightPadding = (CalculateHeight() > this.Height) ? 25 : 8;

            histogram_iwn.Width = iwnContainer.Width - rightPadding;
            fileInfo_iwn.Width = iwnContainer.Width - rightPadding;
        }

        private void workspace_Resize(object sender, EventArgs e)
        {
            AutoResize();
        }

        private void iwn_HeightChanged(object sender, EventArgs e)
        {
            AutoResize();
        }



    }


    // ##########################################################################################################################
    // ##########################################################################################################################
    #region InfoWorkspace_Builder
    public class InfoView_Builder
    {
        public static InfoView GetResult()
        {
            InfoView result = new InfoView()
            {
                iwnContainer = Get_iwnContainer(),
                histogram_iwn = new AdjustedSplitContainer(),
                fileInfo_iwn = new AdjustedSplitContainer(),
                bottomMargin_iwn = Get_bottomMargin_iwn(),

                histogramTabControl = Get_histogramTabControl(),
                infoPanel = Get_infoPanel(),

                Dock = DockStyle.Fill
            };

            Configure_IWN(ref result);

            Configure_Parenthood(ref result);

            result.AssignEventHandlers();

            return result;
        }

        // ########################################################################################################
        private static FlowLayoutPanel Get_iwnContainer()
        {
            FlowLayoutPanel iwnContainer = new FlowLayoutPanel();

            iwnContainer.Dock = DockStyle.Fill;
            iwnContainer.BorderStyle = BorderStyle.FixedSingle;
            iwnContainer.FlowDirection = FlowDirection.TopDown;
            iwnContainer.WrapContents = false;
            iwnContainer.AutoScroll = true;

            return iwnContainer;
        }
        private static Panel Get_bottomMargin_iwn()
        {
            Panel bottomMargin_iwn = new Panel();

            bottomMargin_iwn.Dock = DockStyle.None;
            bottomMargin_iwn.BorderStyle = BorderStyle.None;
            bottomMargin_iwn.Height = 100;
            bottomMargin_iwn.Width = 300;

            return bottomMargin_iwn;
        }
        private static HistogramTabControl Get_histogramTabControl()
        {
            HistogramTabControl histogramPanel = HistogramTabControl_Builder.GetResult();

            histogramPanel.tabControl.Height = histogramPanel.PageHeight + histogramPanel.tabControl.ButtonContainerHeight;
            histogramPanel.tabControl.Dock = DockStyle.Fill;

            return histogramPanel;
        }
        private static InfoPanel Get_infoPanel()
        {
            InfoPanel infoPanel = new InfoPanel();

            infoPanel.Height = infoPanel.labelsHeight * (2 + infoPanel.labelsCount);

            infoPanel.infoLabelsContainer.Dock = DockStyle.Fill;
            infoPanel.infoLabelsContainer.FlowDirection = FlowDirection.TopDown;
            infoPanel.infoLabelsContainer.WrapContents = false;
            infoPanel.infoLabelsContainer.AutoScroll = true;

            return infoPanel;
        }
        // ########################################################################################################
        private static void Configure_IWN(ref InfoView result)
        {
            result.histogram_iwn.PanelHeight = result.histogramTabControl.Height;
            result.histogramTabControl.Dock = DockStyle.Fill;
            result.histogramTabControl.Visible = false;

            result.fileInfo_iwn.PanelHeight = result.infoPanel.Height;
            result.infoPanel.Dock = DockStyle.Fill;
            result.infoPanel.Visible = false;
        }
        private static void Configure_Parenthood(ref InfoView result)
        {
            result.Controls.Add(result.iwnContainer);
            result.iwnContainer.Controls.Add(result.histogram_iwn);
            result.iwnContainer.Controls.Add(result.fileInfo_iwn);
            result.iwnContainer.Controls.Add(result.bottomMargin_iwn);

            result.histogram_iwn.Panel2.Controls.Add(result.histogramTabControl);
            result.fileInfo_iwn.Panel2.Controls.Add(result.infoPanel);
            //result.infoPanel.Controls.Add(result.infoPanel.infoLabelsContainer);
        }
    }
    #endregion
    // ##########################################################################################################################
    // ##########################################################################################################################
}
