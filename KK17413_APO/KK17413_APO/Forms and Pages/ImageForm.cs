using System;
using System.Windows.Forms;
using System.Drawing;
using KK17413_APO.Toolbox_Tools_Expanded;
using KK17413_APO.Panels_Expanded;
using KK17413_APO.Image_Operations;
using System.Collections.Generic;
using KK17413_APO.Data_Structures;
using System.Linq;

namespace KK17413_APO.Forms_and_Pages
{
    public class ImageForm
    {
        // #####################################################################
        public ImageForm_Handle PageHandle { set => pageHandle = value; }

        #pragma warning disable CS0649  // Never created instance warning 
        // These fields are assigned by AutoMapper:        
        public Form form;
        public Panel MenuContainer;
        public SplitContainer containerWorkspace;

        public ProgressBar progressBar;

        public MenuStrip menuStrip;
        public ToolStripMenuItem file_tsmi;
        public ToolStripMenuItem histogram_tsmi;
        public ToolStripMenuItem fileInfo_tsmi;
       
        public ToolStripMenuItem operations_tsmi;   // Operations_tsmi:
        public ToolStripMenuItem histogram_Stretching_tsmi;

        public ImageWorkspace imageLeftWingPanel;
        public InfoWorkspace infoRightWingPanel;

        public List<ImageData> modifications;
        #pragma warning restore CS0649  // Never created instance warning 

        private int imagePanelWIDTH;
        private bool HistogramCalculatePermision = false;


        // #####################################################################   
        private ImageForm_Handle pageHandle;
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


        // ########################################################################################################
        #region ImagePageForm Public Operations      
        public void AssignEventHandlers()
        {          
            form.Resize += form_Resize;
            form.FormClosed += form_AfterFormClosed;            

            containerWorkspace.SplitterMoved += workspace_SplitterMoved;
            histogram_tsmi.Click += histogram_tsmi_Click;
            fileInfo_tsmi.Click += fileInfo_tsmi_Click;

            histogram_Stretching_tsmi.Click += histogram_Stretching_tsmi_Click;

            infoRightWingPanel.histogramTabControl.VisibleChanged += histogramPanel_VisibleChanged;
        }

        private void histogramPanel_VisibleChanged(object sender, EventArgs e)
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

        public void ReloadLanguage()
        {
            file_tsmi.Text = ProgramSettings.Language.GetValue("file_tsmi");
            histogram_tsmi.Text = ProgramSettings.Language.GetValue("histogram_tsmi");
            fileInfo_tsmi.Text = ProgramSettings.Language.GetValue("fileInfo_tsmi");

            operations_tsmi.Text = ProgramSettings.Language.GetValue("operations_tsmi");
            histogram_Stretching_tsmi.Text = ProgramSettings.Language.GetValue("histogram_Stretching_tsmi");

            infoRightWingPanel.histogram_iwn.Text = ProgramSettings.Language.GetValue("histogram_iwn");
            infoRightWingPanel.fileInfo_iwn.Text = ProgramSettings.Language.GetValue("fileInfo_iwn");
        }
        
        public void ReloadColorSet()
        {
            if (pageHandle != null)
                pageHandle.ReloadColorSet();

            MenuContainer.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            progressBar.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            menuStrip.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            menuStrip.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            containerWorkspace.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");

            imageLeftWingPanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");
            infoRightWingPanel.iwnContainer.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");

            //infoPanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");
            //imageScale_tb.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            //imageScale_tb.BackColor = ProgramSettings.ColorManager.GetValue("detailColor2");

            infoRightWingPanel.histogram_iwn.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            infoRightWingPanel.histogram_iwn.HeadPanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            infoRightWingPanel.histogram_iwn.BodyPanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            infoRightWingPanel.histogram_iwn.ToggleButton.BackColor = ProgramSettings.ColorManager.GetValue("detailColor2");

            infoRightWingPanel.fileInfo_iwn.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            infoRightWingPanel.fileInfo_iwn.HeadPanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            infoRightWingPanel.fileInfo_iwn.BodyPanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            infoRightWingPanel.fileInfo_iwn.ToggleButton.BackColor = ProgramSettings.ColorManager.GetValue("detailColor2");

            infoRightWingPanel.histogramTabControl.ReloadColorSet();
        }
        #endregion


        // ########################################################################################################
        #region Event Handlers
        #pragma warning disable IDE1006 // Naming Styles - Lowercase Methods
        private void form_Resize(object sender, EventArgs e)
        {
            imageLeftWingPanel.RelocatePicture();
            progressBar.Width = MenuContainer.Width - menuStrip.Width - 8;
            progressBar.Height = MenuContainer.Height / 2;
            progressBar.Left = menuStrip.Width;
            progressBar.Top = MenuContainer.Height / 4;
        }

        private void form_AfterFormClosed(object sender, FormClosedEventArgs e)
        {
            pageHandle.DetachItself();
            pageHandle = null;

            ProgramSettings.Pages.Remove(this);
        }

        private void workspace_SplitterMoved(object sender, SplitterEventArgs e)
        {
            infoRightWingPanel.AutoResize();
            infoRightWingPanel.infoPanel.ResizeInfoLabels();
            imageLeftWingPanel.RelocatePicture();
        }

        private void histogram_Stretching_tsmi_Click(object sender, EventArgs e)
        {
            if (modifications.Count < 1) return;


            // ImageData refka = modifications.Last();
            // for (int i =0; i < 256; ++i)
            //     Console.WriteLine(refka.data.data[i]);


            progressBar.Width = MenuContainer.Width - menuStrip.Width - 8;
            progressBar.Height = MenuContainer.Height / 2;
            progressBar.Left = menuStrip.Width;
            progressBar.Top = MenuContainer.Height / 4;
            //progressBar.Visible = true;

            modifications.Add(Histogram_Stretching.GetResult(modifications.Last(), ref progressBar));
            //modifications.Add(Histogram_Stretching.GetResult(modifications[modifications.Count - 1]));

            // for (int i =0; i < 256; ++i)
            //     Console.WriteLine(refka.data.data[i] + "   " + modifications.Last().data.data[i]);

            imageLeftWingPanel.AssignImage(modifications.Last().Bitmap);
            //ResizeFormToPicture();
            imageLeftWingPanel.RelocatePicture();

            //infoRightWingPanel.LoadInfoPanel(modifications.Last().bitmap, modifications.Last().ID);
            infoRightWingPanel.histogramTabControl.AssignBitmap(modifications.Last().Bitmap, modifications.Last().ID);

            infoRightWingPanel.histogramTabControl.RecalculateHistograms(ref progressBar);
            HistogramCalculatePermision = true;

            progressBar.Value = 0;
            progressBar.Visible = false;
        }


        private void histogram_tsmi_Click(object sender, EventArgs e)
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

        private void fileInfo_tsmi_Click(object sender, EventArgs e)
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

            form.Width = imagePanelWIDTH + (containerWorkspace.SplitterWidth/2) + rightPanelWidth + extraPadding;

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








































