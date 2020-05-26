using KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace KK17413_APO_REMASTER.FrontEnd.Views_and_Expanded_Panels
{
    [System.ComponentModel.DesignerCategory("")]
    public class View_Info : Panel
    {
        public FlowLayoutPanel iwnContainer;
        public AdjustedSplitContainer histogram_iwn;
        public AdjustedSplitContainer fileInfo_iwn;
        public AdjustedSplitContainer history_iwn;
        public Panel bottomMargin_iwn;
        // *iwn - Image Workspace Nodes

        public Panel_HistogramTabControl panel_HistogramTabControl;
        public Panel_FileInfo panel_FileInfo;
        public Panel_HistoryChanges panel_historyChanges;


        #region Public Operations:
        public void ShowHistogramPanel()
        {
            if (!histogram_iwn.Panel2Collapsed)
                panel_HistogramTabControl.tabControl.ShowFirstPage();
        }

        public void LoadInfoPanel(Bitmap bitmap, string filename)
        {
            panel_FileInfo.ReloadImageInfo(bitmap, filename);

            fileInfo_iwn.PanelHeight = panel_FileInfo.LabelsHeight * (2 + panel_FileInfo.LabelsCount);
        }

        public void AutoResize()
        {
            int rightPadding;

            rightPadding = (CalculateHeight() > this.Height) ? 25 : 8;

            histogram_iwn.Width = iwnContainer.Width - rightPadding;
            fileInfo_iwn.Width = iwnContainer.Width - rightPadding;
            history_iwn.Width = iwnContainer.Width - rightPadding;
        }

        #endregion

        #region Events:
        public void Workspace_Resize(object sender, EventArgs e)
        {
            AutoResize();
        }

        public void Iwn_HeightChanged(object sender, EventArgs e)
        {
            AutoResize();
        }

        #endregion





        public int CalculateWidht()
        {
            int hist = (histogram_iwn.BodyPanelCollapsed) ? 0 : panel_HistogramTabControl.PageWidth;

            int file = (fileInfo_iwn.BodyPanelCollapsed) ? 0 : panel_FileInfo.Width;

            return Math.Max(Math.Max(hist, file), bottomMargin_iwn.Width);
        }

        public int CalculateHeight()
        {
            return bottomMargin_iwn.Height + fileInfo_iwn.Height + histogram_iwn.Height;
        }
    }


    // ##########################################################################################################################
    // ##########################################################################################################################
    #region InfoWorkspace_Builder
    public class InfoView_Builder
    {
        public static View_Info GetResult()
        {
            View_Info result = new View_Info()
            {
                iwnContainer = Get_iwnContainer(),
                histogram_iwn = new AdjustedSplitContainer(),
                fileInfo_iwn = new AdjustedSplitContainer(),
                history_iwn = new AdjustedSplitContainer(),
                bottomMargin_iwn = Get_bottomMargin_iwn(),

                panel_HistogramTabControl = Get_histogramTabControl(),
                panel_FileInfo = Get_FileInfo(),
                panel_historyChanges = Get_HistoryChanges(),

                Dock = DockStyle.Fill
            };

            Configure_IWN(ref result);

            Configure_Parenthood(ref result);

            Configure_EventHandlers(ref result);

            return result;
        }

        // ########################################################################################################
        private static FlowLayoutPanel Get_iwnContainer()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                MinimumSize = new Size(0, 0),
                BorderStyle = BorderStyle.FixedSingle,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true
            };
        }
        private static Panel Get_bottomMargin_iwn()
        {
            return new Panel
            {
                Dock = DockStyle.None,
                BorderStyle = BorderStyle.None,
                Height = 100,
                Width = 300
            };
        }
        private static Panel_HistogramTabControl Get_histogramTabControl()
        {
            Panel_HistogramTabControl histogramPanel = Panel_HistogramTabControl_Builder.GetResult();

            histogramPanel.tabControl.Height = histogramPanel.PageHeight + histogramPanel.tabControl.ButtonContainerHeight;
            histogramPanel.tabControl.Dock = DockStyle.Fill;

            return histogramPanel;
        }
        
        private static Panel_FileInfo Get_FileInfo()
        {
            Panel_FileInfo infoPanel = new Panel_FileInfo();

            infoPanel.Height = infoPanel.LabelsHeight * (2 + infoPanel.LabelsCount);

            infoPanel.infoLabelsContainer.Dock = DockStyle.Fill;
            infoPanel.infoLabelsContainer.Margin = new Padding(1, 5, 30, 1);
            infoPanel.infoLabelsContainer.FlowDirection = FlowDirection.TopDown;
            infoPanel.infoLabelsContainer.WrapContents = false;
            infoPanel.infoLabelsContainer.AutoScroll = true;

            return infoPanel;
        }
        
        private static Panel_HistoryChanges Get_HistoryChanges()
        {
            Panel_HistoryChanges historyPanel = new Panel_HistoryChanges();

            historyPanel.MinimumSize = new Size(0, 100);
            historyPanel.Height = historyPanel.LabelsHeight * (2 + historyPanel.LabelsCount);

            return historyPanel;
        }
        // ########################################################################################################
        private static void Configure_IWN(ref View_Info result)
        {
            result.histogram_iwn.PanelHeight = result.panel_HistogramTabControl.Height;
            result.panel_HistogramTabControl.Dock = DockStyle.Fill;
            result.panel_HistogramTabControl.Visible = false;

            result.fileInfo_iwn.PanelHeight = result.panel_FileInfo.Height;
            result.panel_FileInfo.Dock = DockStyle.Fill;
            result.panel_FileInfo.Visible = false;

            result.history_iwn.PanelHeight = result.panel_historyChanges.Height;
            result.panel_historyChanges.Dock = DockStyle.Fill;
            result.panel_historyChanges.Visible = false;
        }
        private static void Configure_Parenthood(ref View_Info result)
        {
            result.Controls.Add(result.iwnContainer);
            result.iwnContainer.Controls.Add(result.histogram_iwn);
            result.iwnContainer.Controls.Add(result.fileInfo_iwn);
            result.iwnContainer.Controls.Add(result.history_iwn);
            result.iwnContainer.Controls.Add(result.bottomMargin_iwn);

            result.histogram_iwn.Panel2.Controls.Add(result.panel_HistogramTabControl);
            result.fileInfo_iwn.Panel2.Controls.Add(result.panel_FileInfo);
            result.history_iwn.Panel2.Controls.Add(result.panel_historyChanges);
        }

        private static void Configure_EventHandlers(ref View_Info result)
        {
            result.Resize += result.Workspace_Resize;
            result.histogram_iwn.ToggleButton.Click += result.Iwn_HeightChanged;
            result.fileInfo_iwn.ToggleButton.Click += result.Iwn_HeightChanged;
            result.history_iwn.ToggleButton.Click += result.Iwn_HeightChanged;
        }

    }
    #endregion
    // ##########################################################################################################################
    // ##########################################################################################################################
}
