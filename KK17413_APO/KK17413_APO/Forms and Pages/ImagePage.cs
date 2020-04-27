using System;
using System.Windows.Forms;
using System.Drawing;
using KK17413_APO.Toolbox_Tools_Expanded;
using System.Windows.Forms.VisualStyles;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using KK17413_APO.Panels_Expanded;

namespace KK17413_APO.Forms_and_Pages
{
    public class ImagePage
    {
        // #####################################################################
        public PageHandle PageHandle { set => pageHandle = value; }

        #pragma warning disable CS0649    // These fields are assigned by AutoMapper:        
        public Form form;
        public Panel containerMenu;
        public SplitContainer containerWorkspace;
        //public SplitterPanel imagePanel;
        //public SplitterPanel infoPanel;

        public MenuStrip menuStrip;
        public ToolStripMenuItem file_tsmi;
        public ToolStripMenuItem histogram_tsmi;

        //public PictureBox picture;
        public FlowLayoutPanel iwnContainer;   // Image Workspace Nodes Container
        public AdjustedSplitContainer histogram_iwn;
        public AdjustedSplitContainer fileInfo_iwn;
        // *iwn - Image Workspace Nodes


        public ImagePanel imagePanel;
        public HistogramPanel histogramPanel;
        public InfoPanel infoPanel;
        #pragma warning restore CS0649


        // #####################################################################   
        private bool collapsedRightWing
        {
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


        // #####################################################################   
        private PageHandle pageHandle;
        private Bitmap bitmap;
        private string filename;


        // ########################################################################################################
        #region ImagePage Operations      
        public void FinalInit()
        {           
            //imagePanel = this.containerWorkspace.Panel1;
            //infoPanel = this.containerWorkspace.Panel2;

            imagePanel.relocatePicture_permission = true;

            this.form.Resize += form_Resize;
            this.form.FormClosed += form_AfterFormClosed;            

            this.containerWorkspace.SplitterMoved += workspace_SplitterMoved;
            this.histogram_tsmi.Click += histogram_tsmi_Click;

            imagePanel.RelocatePicture();
        }

        public void AssignData(string filename)
        {
            this.filename = filename;
            form.Text = filename;

            bitmap = new Bitmap(filename);

            imagePanel.AssignImage(bitmap);

            ResizeFormToPicture();



            infoPanel.ReloadImageInfo(bitmap, filename);
        }


        public void ReloadLanguage()
        {
            file_tsmi.Text = ProgramSettings.language.GetValue("file_tsmi");
            histogram_tsmi.Text = ProgramSettings.language.GetValue("histogram_tsmi");

            histogram_iwn.Text = ProgramSettings.language.GetValue("histogram_iwn");
            fileInfo_iwn.Text = ProgramSettings.language.GetValue("fileInfo_iwn");
        }
        
        public void ReloadColorSet()
        {
            menuStrip.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            menuStrip.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            containerMenu.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            containerWorkspace.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");

            imagePanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");
            infoPanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");
            iwnContainer.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");

            //imageScale_tb.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            //imageScale_tb.BackColor = ProgramSettings.ColorManager.GetValue("detailColor2");

            histogram_iwn.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            histogram_iwn.HeadPanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            histogram_iwn.BodyPanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            histogram_iwn.ToggleButton.BackColor = ProgramSettings.ColorManager.GetValue("detailColor2");

            fileInfo_iwn.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            fileInfo_iwn.HeadPanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            fileInfo_iwn.BodyPanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            fileInfo_iwn.ToggleButton.BackColor = ProgramSettings.ColorManager.GetValue("detailColor2");
        }
        #endregion


        // ########################################################################################################
        #region Event Handlers
        #pragma warning disable IDE1006 // Naming Styles - Lowercase Methods
        private void form_Resize(object sender, EventArgs e)
        {
            imagePanel.RelocatePicture();
        }

        private void form_AfterFormClosed(object sender, FormClosedEventArgs e)
        {
            pageHandle.DetachItself();
            pageHandle = null;

            ProgramSettings.Pages.Remove(this);
        }

        private void workspace_SplitterMoved(object sender, SplitterEventArgs e)
        {
            imagePanel.RelocatePicture();
            histogram_iwn.Width = iwnContainer.Width - 8;
            fileInfo_iwn.Width = iwnContainer.Width - 8;

            infoPanel.ResizeInfoLabels();
        }

        private void histogram_tsmi_Click(object sender, EventArgs e)
        {
            IWN_ToggleLogic(ref histogram_iwn, ref fileInfo_iwn);

        }


        private void IWN_ToggleLogic(ref AdjustedSplitContainer selected, ref AdjustedSplitContainer others)
        {
            if (!collapsedRightWing)
            {
                // RightWing jest rozwinięty

                if (!others.Panel2Collapsed)
                {
                    // WERSJA GDY FOKUSUJEMY -----------------------------------------------
                    // - RightWing jest rozwinięty
                    // - Nie tylko histogram jest rozwinięty
                    // - Histogram jest rozwinięty lub nie


                    // zwiń wszystko inne
                    others.HideBody();

                    // Rozwiń histogram
                    selected.ShowBody();


                    return;
                }
                else if (!selected.Panel2Collapsed)
                {
                    // WERSJA GDY ZAMYKAMY -------------------------------------------------
                    // - RightWing jest rozwinięty
                    // - Tylko Histogram jest rozwinienty

                    // Zwin histogram
                    selected.HideBody();

                    // Togluj RIght wing:
                    ToggleRightWing();

                    return;
                }
                else
                {
                    // WERSJA GDY OTWIERAMY -------------------------------------------------
                    // - RightWing jest rozwinięty
                    // - WSZYSTKO jest zwinięte
                    selected.ShowBody();

                    return;
                }
            }
            else
            {
                // RightWing jest zwinięty
                // dla pewności zwińmy wszystko
                others.HideBody();

                // Togglujmy Rightwing:
                ToggleRightWing();

                // Rozwiń histogram:
                selected.ShowBody();

                return;
            }

        }


        #pragma warning restore IDE1006 // Naming Styles - Lowercase Methods
        #endregion


        // ########################################################################################################
        #region ImagePage Size Modifiers - Toggle / Resize / Relocate
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

            infoPanel.ResizeInfoLabels();

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
        #region ImagePage Private Calculations
        
        




        #endregion
        // ########################################################################################################
    }
}
