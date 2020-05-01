using System;
using System.Windows.Forms;
using System.Drawing;
using KK17413_APO.Toolbox_Tools_Expanded;
using KK17413_APO.Panels_Expanded;


namespace KK17413_APO.Forms_and_Pages
{
    public class ImageForm
    {
        // #####################################################################
        public ImageForm_Handle PageHandle { set => pageHandle = value; }

        #pragma warning disable CS0649  // Never created instance warning 
        // These fields are assigned by AutoMapper:        
        public Form form;
        public Panel containerMenu;
        public SplitContainer containerWorkspace;

        public MenuStrip menuStrip;
        public ToolStripMenuItem file_tsmi;
        public ToolStripMenuItem histogram_tsmi;
        public ToolStripMenuItem fileInfo_tsmi;

        public ImageWorkspace imagePanel;
        public InfoWorkspace infoPanel;
        #pragma warning restore CS0649  // Never created instance warning 


        // #####################################################################   
        private bool collapsedRightWing {
            get => containerWorkspace.Panel2Collapsed; 
            set => containerWorkspace.Panel2Collapsed = value; 
        }
        private int TaskBarH {           
            get {
                // Calculate the TaskBar Height:
                return Screen.PrimaryScreen.Bounds.Height - 
                       Screen.PrimaryScreen.WorkingArea.Height;
            }
        }  
        private ImageForm_Handle pageHandle;


        // ########################################################################################################
        #region ImagePageForm Public Operations      
        public void FinalInit()
        {           
            //imagePanel = this.containerWorkspace.Panel1;
            //infoPanel = this.containerWorkspace.Panel2;

            imagePanel.relocatePicture_permission = true;

            this.form.Resize += form_Resize;
            this.form.FormClosed += form_AfterFormClosed;            

            this.containerWorkspace.SplitterMoved += workspace_SplitterMoved;
            this.histogram_tsmi.Click += histogram_tsmi_Click;
            this.fileInfo_tsmi.Click += fileInfo_tsmi_Click;

            this.infoPanel.histogramPanel.VisibleChanged += histogramPanel_VisibleChanged;
            //histogram_iwn.Resize += histogram_iwn_Resize;

            imagePanel.RelocatePicture();
        }

        private void histogramPanel_VisibleChanged(object sender, EventArgs e)
        {
            infoPanel.histogramPanel.RecalculateHistograms();
        }


        public void AssignData(string filename)
        {
            form.Text = filename;

            Bitmap bitmap = new Bitmap(filename);

            imagePanel.AssignImage(bitmap);

            ResizeFormToPicture();

            infoPanel.infoPanel.ReloadImageInfo(bitmap, filename);
            //infoPanel.infoLabelsContainer.Height = infoPanel.labelsHeight * (20 + infoPanel.labelsCount);
            infoPanel.fileInfo_iwn.PanelHeight = infoPanel.infoPanel.labelsHeight * (2+infoPanel.infoPanel.labelsCount);

            infoPanel.histogramPanel.AssignBitmap(bitmap);
        }

        public void ReloadLanguage()
        {
            file_tsmi.Text = ProgramSettings.Language.GetValue("file_tsmi");
            histogram_tsmi.Text = ProgramSettings.Language.GetValue("histogram_tsmi");
            fileInfo_tsmi.Text = ProgramSettings.Language.GetValue("fileInfo_tsmi");

            infoPanel.histogram_iwn.Text = ProgramSettings.Language.GetValue("histogram_iwn");
            infoPanel.fileInfo_iwn.Text = ProgramSettings.Language.GetValue("fileInfo_iwn");
        }
        
        public void ReloadColorSet()
        {
            menuStrip.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            menuStrip.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            containerMenu.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            containerWorkspace.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");

            imagePanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");
            infoPanel.iwnContainer.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");

            //infoPanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");
            //imageScale_tb.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            //imageScale_tb.BackColor = ProgramSettings.ColorManager.GetValue("detailColor2");

            infoPanel.histogram_iwn.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            infoPanel.histogram_iwn.HeadPanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            infoPanel.histogram_iwn.BodyPanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            infoPanel.histogram_iwn.ToggleButton.BackColor = ProgramSettings.ColorManager.GetValue("detailColor2");

            infoPanel.fileInfo_iwn.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            infoPanel.fileInfo_iwn.HeadPanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            infoPanel.fileInfo_iwn.BodyPanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            infoPanel.fileInfo_iwn.ToggleButton.BackColor = ProgramSettings.ColorManager.GetValue("detailColor2");
        }
        #endregion


        // ########################################################################################################
        #region Event Handlers
        #pragma warning disable IDE1006 // Naming Styles - Lowercase Methods
        private void form_Resize(object sender, EventArgs e)
        {
            imagePanel.RelocatePicture();
        }

        private void workspace_SplitterMoved(object sender, SplitterEventArgs e)
        {
            imagePanel.RelocatePicture();
            infoPanel.histogram_iwn.Width = infoPanel.iwnContainer.Width - 8;
            infoPanel.fileInfo_iwn.Width = infoPanel.iwnContainer.Width - 8;

            infoPanel.infoPanel.ResizeInfoLabels();
        }

        private void form_AfterFormClosed(object sender, FormClosedEventArgs e)
        {
            pageHandle.DetachItself();
            pageHandle = null;

            ProgramSettings.Pages.Remove(this);
        }

        private void histogram_tsmi_Click(object sender, EventArgs e)
        {
            IWN_ToggleLogic(ref infoPanel.histogram_iwn, ref infoPanel.fileInfo_iwn);
            if (!infoPanel.histogram_iwn.Panel2Collapsed)
                infoPanel.histogramPanel.tabControl.ShowFirstPage();
        }

        private void fileInfo_tsmi_Click(object sender, EventArgs e)
        {
            IWN_ToggleLogic(ref infoPanel.fileInfo_iwn, ref infoPanel.histogram_iwn);
        }
        #pragma warning restore IDE1006 // Naming Styles - Lowercase Methods
        #endregion


        // ########################################################################################################
        #region ImagePageForm Size Modifiers - Toggle / Resize / Relocate
        private void IWN_ToggleLogic(ref AdjustedSplitContainer selected, ref AdjustedSplitContainer others)
        {
            if (!collapsedRightWing)
            {   // RightWingPanel is extended

                if (!others.Panel2Collapsed)
                {   // RightWingPanel is extended
                    // OTHERS are extended
                    // SELECTED is eiter extended or not

                    // SET FOCUS ON SELECTED:                 
                    others.HideBody();      // [1] Collapse OTHERS
                    selected.ShowBody();    // [2] Extend SELECTED
                }
                else if (!selected.Panel2Collapsed)
                {   // RightWingPanel is extended
                    // Only SELECTED is extended

                    // CLOSE THE EXTENDED:
                    selected.HideBody();    // [1] Collapse SELECTED
                    ToggleRightWing();      // [2] Collapse RightWingPanel
                }
                else
                {   // RightWingPanel is extended
                    // Both SELECTED and OTHERS are collapsed

                    // EXTEND THE SELECTED ONE ONLY:
                    selected.ShowBody();
                }
            }
            else
            {   // RightWingPanel is collapsed

                others.HideBody();      // [1] Collapse OTHERS
                ToggleRightWing();      // [2] Extended RightWingPanel
                selected.ShowBody();    // [3] Extend SELECTED
            }
        }

        private void ToggleRightWing()
        {
            imagePanel.relocatePicture_permission = false;

            // Change the width of the form when we hide the RightWingPanel:     (Before toggle)
            if (collapsedRightWing)
                form.Width += infoPanel.ClientRectangle.Width + containerWorkspace.SplitterWidth + 1;

            // Toggle the infoPanel:
            collapsedRightWing = !collapsedRightWing;

            // Change the width of the form when we show the RightWingPanel:     (After toggle)
            if (collapsedRightWing)
                form.Width -= infoPanel.ClientRectangle.Width + containerWorkspace.SplitterWidth + 1;

            imagePanel.relocatePicture_permission = true;
            imagePanel.RelocatePicture();
        }

        private void ResizeFormToPicture()
        {
            int tmpFormW = imagePanel.picture.Image.Width + 16;
            int tmpFormH = imagePanel.picture.Image.Height + TaskBarH + containerMenu.Height - 1;

            collapsedRightWing = true;
            form.Size = new Size(tmpFormW, tmpFormH);
        }
        #endregion


        // ########################################################################################################
    }
}
