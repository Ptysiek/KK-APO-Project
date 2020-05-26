using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

using KK17413_APO_REMASTER.BackEnd;
using KK17413_APO_REMASTER.BackEnd.Factories;
using KK17413_APO_REMASTER.BackEnd.DataStructures;
using KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded;
using KK17413_APO_REMASTER.FrontEnd.Views_and_Expanded_Panels;


namespace KK17413_APO_REMASTER.FrontEnd.Forms_and_Popups
{
    public class ImageForm
    {
        public ImageForm_Service SERVICE;

        public Form form;
        public Panel MenuContainer;
        public SplitContainer containerWorkspace;

        public ProgressBar progressBar;
        public MenuStrip menuStrip;

        public List<ToolStripMenuItem> Menu_tsmis;
        public List<ToolStripMenuItem> Operations_tsmis;
        public List<ToolStripMenuItem> OperationsFamily_tsmis;
        // *tsmi - Tool Strip Menu Item

        public View_Image imageLeftWingPanel;
        public View_Info infoRightWingPanel;


        private bool CollapsedRightWing
        {
            get => containerWorkspace.Panel2Collapsed;
            set => containerWorkspace.Panel2Collapsed = value;
        }
        private int TaskBarH
        {
            get
            {
                // Calculate the TaskBar Height:
                return Screen.PrimaryScreen.Bounds.Height -
                       Screen.PrimaryScreen.WorkingArea.Height;
            }
        }

        private int imagePanelWIDTH;
        private bool HistogramCalculatePermision = false;

        
        // ########################################################################################################
        #region ImageForm Public Operations      
        public void StartProgressBar()
        {
            progressBar.Value = 0;
            progressBar.Visible = true;
        }
        public void SetProgressBarValue(int value)
        {
            progressBar.Value = value;
        }

        public void CloseProgressBar()
        {
            progressBar.Value = 0;
            progressBar.Visible = false;
        }





        public void ReloadImageData_All(ImageData data)
        {
            if (data == null)
                return;

            imageLeftWingPanel.AssignImage(data.Bitmap);

            infoRightWingPanel.panel_HistogramTabControl.GiveHistogramNewData(data);            
        }

        public void ReloadModificationsList(List<Modification> modifications)
        {
            infoRightWingPanel.panel_historyChanges.ReloadInfo(modifications);
        }





        public void HistogramPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (infoRightWingPanel.panel_HistogramTabControl.Visible == false)
                return;

            if (HistogramCalculatePermision == false)
                return;

            infoRightWingPanel.ShowHistogramPanel();
            progressBar.Value = 0;
            progressBar.Visible = false;
        }

        public void ReloadLanguage(Language LanguageSet)
        {
            foreach (var tsmi in Menu_tsmis)
                tsmi.Text = LanguageSet.GetValue(tsmi.Name);

            foreach (var tsmi in OperationsFamily_tsmis)
                tsmi.Text = LanguageSet.GetValue(tsmi.Name);

            foreach (var tsmi in Operations_tsmis)
                tsmi.Text = LanguageSet.GetValue(tsmi.Name);

            infoRightWingPanel.histogram_iwn.Text = LanguageSet.GetValue("histogram_iwn");
            infoRightWingPanel.fileInfo_iwn.Text = LanguageSet.GetValue("fileInfo_iwn");
            infoRightWingPanel.history_iwn.Text = LanguageSet.GetValue("history_iwn");
        }

        public void ReloadColorSet(ColorSet ColorSet)
        {
            MenuContainer.BackColor = ColorSet.GetValue("bgColorLayer1");
            progressBar.BackColor = ColorSet.GetValue("bgColorLayer1");
            menuStrip.ForeColor = ColorSet.GetValue("fontColor");
            menuStrip.BackColor = ColorSet.GetValue("bgColorLayer1");
            containerWorkspace.BackColor = ColorSet.GetValue("bgColorLayer1");

            imageLeftWingPanel.BackColor = ColorSet.GetValue("bgColorLayer2");
            infoRightWingPanel.iwnContainer.BackColor = ColorSet.GetValue("bgColorLayer2");

            //infoPanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");
            //imageScale_tb.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            //imageScale_tb.BackColor = ProgramSettings.ColorManager.GetValue("detailColor2");

            infoRightWingPanel.histogram_iwn.ForeColor = ColorSet.GetValue("fontColor");
            infoRightWingPanel.histogram_iwn.HeadPanel.BackColor = ColorSet.GetValue("bgColorLayer1");
            infoRightWingPanel.histogram_iwn.BodyPanel.BackColor = ColorSet.GetValue("bgColorLayer1");
            infoRightWingPanel.histogram_iwn.ToggleButton.BackColor = ColorSet.GetValue("detailColor2");

            infoRightWingPanel.fileInfo_iwn.ForeColor = ColorSet.GetValue("fontColor");
            infoRightWingPanel.fileInfo_iwn.HeadPanel.BackColor = ColorSet.GetValue("bgColorLayer1");
            infoRightWingPanel.fileInfo_iwn.BodyPanel.BackColor = ColorSet.GetValue("bgColorLayer1");
            infoRightWingPanel.fileInfo_iwn.ToggleButton.BackColor = ColorSet.GetValue("detailColor2");

            infoRightWingPanel.history_iwn.ForeColor = ColorSet.GetValue("fontColor");
            infoRightWingPanel.history_iwn.HeadPanel.BackColor = ColorSet.GetValue("bgColorLayer1");
            infoRightWingPanel.history_iwn.BodyPanel.BackColor = ColorSet.GetValue("bgColorLayer1");
            infoRightWingPanel.history_iwn.ToggleButton.BackColor = ColorSet.GetValue("detailColor2");

            infoRightWingPanel.panel_HistogramTabControl.ReloadColorSet(ColorSet);
        }
        #endregion


        // ########################################################################################################
        #region Event Handlers
        public void Form_Resize(object sender, EventArgs e)
        {
            imageLeftWingPanel.RelocatePicture();
            progressBar.Width = MenuContainer.Width - menuStrip.Width - 8;
            progressBar.Height = MenuContainer.Height / 2;
            progressBar.Left = menuStrip.Width;
            progressBar.Top = MenuContainer.Height / 4;
        }

        public void Form_AfterFormClosed(object sender, FormClosedEventArgs e)
        {
            SERVICE.CloseWindow(this);          
        }

        public void Workspace_SplitterMoved(object sender, SplitterEventArgs e)
        {
            infoRightWingPanel.AutoResize();
            infoRightWingPanel.panel_FileInfo.ResizeInfoLabels();
            infoRightWingPanel.panel_historyChanges.ResizeInfoLabels();
            imageLeftWingPanel.RelocatePicture();
        }


        public void ImageOperation_Tsmi_Click(object sender, EventArgs e)
        {
            foreach (var obj in Operations_tsmis)
            {
                if (sender.Equals(obj))
                {
                    SERVICE.ImageOperation(obj.Name);
                    return;
                }
            }
        }


        public void Histogram_tsmi_Click(object sender, EventArgs e)
        {
            HistogramCalculatePermision = false;

            AdjustedSplitContainer selected = infoRightWingPanel.histogram_iwn;
            AdjustedSplitContainer others = infoRightWingPanel.fileInfo_iwn;

            IWN_ToggleLogic(ref selected, ref others);
            infoRightWingPanel.panel_historyChanges.ResizeInfoLabels();
            imageLeftWingPanel.RelocatePicture();

            HistogramCalculatePermision = true;
            infoRightWingPanel.ShowHistogramPanel();
            progressBar.Value = 0;
            progressBar.Visible = false;
        }

        public void FileInfo_tsmi_Click(object sender, EventArgs e)
        {
            AdjustedSplitContainer selected = infoRightWingPanel.fileInfo_iwn;
            AdjustedSplitContainer others = infoRightWingPanel.histogram_iwn;

            IWN_ToggleLogic(ref selected, ref others);

            infoRightWingPanel.panel_FileInfo.ResizeInfoLabels();
            infoRightWingPanel.panel_historyChanges.ResizeInfoLabels();
            imageLeftWingPanel.RelocatePicture();
        }
        #endregion


        // ########################################################################################################
        #region ImageForm Size Modifiers - Toggle / Resize / Relocate
        private void IWN_ToggleLogic(ref AdjustedSplitContainer selected, ref AdjustedSplitContainer others)
        {
            if (!CollapsedRightWing)
            {   // (RightWingPanel EXTENDED)
                if (!others.Panel2Collapsed)
                {   // (RightWingPanel EXTENDED) (others EXTENDED) (selected IRRELEVANT)
                    // SET FOCUS ON SELECTED:                 
                    others.HideBody();
                    selected.ShowBody();
                    ResizeRightWing();
                }
                else if (!selected.Panel2Collapsed)
                {   // (RightWingPanel EXTENDED) (others COLLAPSED) (selected EXTENDED)
                    // CLOSE THE EXTENDED ONE:
                    selected.HideBody();
                    ToggleRightWing();
                }
                else
                {   // (RightWingPanel EXTENDED) (others COLLAPSED) (selected COLLAPSED)
                    // EXTEND THE SELECTED ONE ONLY:
                    selected.ShowBody();
                    ResizeRightWing();
                }
            }
            else
            {   // (RightWingPanel COLLAPSED)
                others.HideBody();
                selected.ShowBody();
                ToggleRightWing();
            }
        }

        private void ToggleRightWing()
        {
            imageLeftWingPanel.relocatePicture_permission = false;

            // Change the width of the form when we show the RightWingPanel:     (Before toggle)
            if (CollapsedRightWing)
                ResizeRightWing();

            // Toggle the infoPanel:
            CollapsedRightWing = !CollapsedRightWing;

            // Change the width of the form when we hide the RightWingPanel:     (After toggle)
            if (CollapsedRightWing)
                form.Width = imagePanelWIDTH + 16;

            imageLeftWingPanel.relocatePicture_permission = true;
            imageLeftWingPanel.RelocatePicture();
        }

        private void ResizeRightWing()
        {
            imagePanelWIDTH = imageLeftWingPanel.Width;
            int rightPanelWidth = infoRightWingPanel.CalculateWidht();
            int extraPadding = 45;

            form.Width = imagePanelWIDTH + (containerWorkspace.SplitterWidth / 2) + rightPanelWidth + extraPadding;

            if (form.Width - imagePanelWIDTH < rightPanelWidth)
                containerWorkspace.SplitterDistance = form.Width - containerWorkspace.SplitterWidth - rightPanelWidth - extraPadding;
            else
                containerWorkspace.SplitterDistance = imagePanelWIDTH;
        }

        public void ResizeFormToPicture()
        {
            int tmpFormW = imageLeftWingPanel.picture.Image.Width + 16;
            int tmpFormH = imageLeftWingPanel.picture.Image.Height + TaskBarH + menuStrip.Height - 1;

            CollapsedRightWing = true;
            form.Size = new Size(tmpFormW, tmpFormH);
        }
        #endregion
        // ########################################################################################################
    }
}

