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
    public class ImageWindow
    {
        // #####################################################################
        //public ImageWindow_HandlePanel PageHandle { set => pageHandle = value; }
        public ImageForm_Service SERVICE;

#pragma warning disable CS0649  // Never created instance warning 
        // These fields are assigned by AutoMapper:        
        public Form form;
        public Panel MenuContainer;
        public SplitContainer containerWorkspace;

        public ProgressBar progressBar;
        public MenuStrip menuStrip;

        public List<ToolStripMenuItem> Menu_tsmis;
        public List<ToolStripMenuItem> Operations_tsmis;
        // *tsmi - Tool Strip Menu Item

        /*
        public ToolStripMenuItem file_tsmi;
        public ToolStripMenuItem histogram_tsmi;
        public ToolStripMenuItem fileInfo_tsmi;

        public ToolStripMenuItem operations_tsmi;   // Operations_tsmi:
        public ToolStripMenuItem histogram_Stretching_tsmi;
        public ToolStripMenuItem histogram_Equalization_tsmi;
        public ToolStripMenuItem negation_tsmi;
        */

        public ImageView imageLeftWingPanel;
        public InfoView infoRightWingPanel;

        #pragma warning restore CS0649  // Never created instance warning 

        private int imagePanelWIDTH;
        private bool HistogramCalculatePermision = false;


        // #####################################################################   
        //private ImageWindow_HandlePanel pageHandle;
        private bool collapsedRightWing
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

        
        // ########################################################################################################
        #region ImagePageForm Public Operations      

        public void histogramPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (infoRightWingPanel.histogramTabControl.Visible == false)
                return;

            if (HistogramCalculatePermision == false)
                return;

            infoRightWingPanel.LoadHistogramPanel(ref progressBar);
            progressBar.Value = 0;
            progressBar.Visible = false;
        }

        public void AssignData(string filename)
        {
            form.Text = filename;
            Bitmap bitmap = new Bitmap(filename);

            imageLeftWingPanel.AssignImage(bitmap);
            ResizeFormToPicture();
            imageLeftWingPanel.RelocatePicture();

            infoRightWingPanel.LoadInfoPanel(bitmap, filename);
            infoRightWingPanel.histogramTabControl.AssignBitmap(bitmap, filename);
            HistogramCalculatePermision = true;

            modifications.Add(new ImageData(bitmap, filename));
        }

        public void ReloadLanguage(Language LanguageSet)
        {
            foreach (var tsmi in Menu_tsmis)
                tsmi.Text = LanguageSet.GetValue(tsmi.Name);

            foreach (var tsmi in Operations_tsmis)
                tsmi.Text = LanguageSet.GetValue(tsmi.Name);

            infoRightWingPanel.histogram_iwn.Text = LanguageSet.GetValue("histogram_iwn");
            infoRightWingPanel.fileInfo_iwn.Text = LanguageSet.GetValue("fileInfo_iwn");
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

            infoRightWingPanel.histogramTabControl.ReloadColorSet(ColorSet);
        }
        #endregion


        // ########################################################################################################
        #region Event Handlers
#pragma warning disable IDE1006 // Naming Styles - Lowercase Methods
        public void form_Resize(object sender, EventArgs e)
        {
            imageLeftWingPanel.RelocatePicture();
            progressBar.Width = MenuContainer.Width - menuStrip.Width - 8;
            progressBar.Height = MenuContainer.Height / 2;
            progressBar.Left = menuStrip.Width;
            progressBar.Top = MenuContainer.Height / 4;
        }

        public void form_AfterFormClosed(object sender, FormClosedEventArgs e)
        {
            SERVICE.CloseWindow();
            //PROGRAM.CloseWindow(this);            
        }

        public void workspace_SplitterMoved(object sender, SplitterEventArgs e)
        {
            infoRightWingPanel.AutoResize();
            infoRightWingPanel.infoPanel.ResizeInfoLabels();
            imageLeftWingPanel.RelocatePicture();
        }


        public void ImageOperation_Tsmi_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        public void histogram_tsmi_Click(object sender, EventArgs e)
        {
            HistogramCalculatePermision = false;

            AdjustedSplitContainer selected = infoRightWingPanel.histogram_iwn;
            AdjustedSplitContainer others = infoRightWingPanel.fileInfo_iwn;

            IWN_ToggleLogic(ref selected, ref others);
            imageLeftWingPanel.RelocatePicture();

            HistogramCalculatePermision = true;
            infoRightWingPanel.LoadHistogramPanel(ref progressBar);
            progressBar.Value = 0;
            progressBar.Visible = false;
        }

        public void fileInfo_tsmi_Click(object sender, EventArgs e)
        {
            AdjustedSplitContainer selected = infoRightWingPanel.fileInfo_iwn;
            AdjustedSplitContainer others = infoRightWingPanel.histogram_iwn;

            IWN_ToggleLogic(ref selected, ref others);

            infoRightWingPanel.infoPanel.ResizeInfoLabels();
            imageLeftWingPanel.RelocatePicture();
        }
#pragma warning restore IDE1006 // Naming Styles - Lowercase Methods
        #endregion


        // ########################################################################################################
        #region ImageForm Size Modifiers - Toggle / Resize / Relocate
        private void IWN_ToggleLogic(ref AdjustedSplitContainer selected, ref AdjustedSplitContainer others)
        {
            if (!collapsedRightWing)
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
            if (collapsedRightWing)
                ResizeRightWing();

            // Toggle the infoPanel:
            collapsedRightWing = !collapsedRightWing;

            // Change the width of the form when we hide the RightWingPanel:     (After toggle)
            if (collapsedRightWing)
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

        private void ResizeFormToPicture()
        {
            int tmpFormW = imageLeftWingPanel.picture.Image.Width + 16;
            int tmpFormH = imageLeftWingPanel.picture.Image.Height + TaskBarH + menuStrip.Height - 1;

            collapsedRightWing = true;
            form.Size = new Size(tmpFormW, tmpFormH);
        }
        #endregion
        // ########################################################################################################
    }
}

