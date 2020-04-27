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

        public FlowLayoutPanel infoLabelsContainer;
        public List<Label> infoLabels;
        //public Histogram histogram;

        public HistogramPanel histogramPanel;
        public ImagePanel imagePanel;
        #pragma warning restore CS0649


        // #####################################################################   
        private bool collapsedInfoPanel {
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
            infoPanel = this.containerWorkspace.Panel2;

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
        }

        public void ReloadImageInfo()
        {
            infoLabels = new List<Label>
            {
                new Label() { Text = "image width:  " + picture.Image.Width.ToString() },
                new Label() { Text = "image height:  " + picture.Image.Height.ToString() },
                new Label() { Text = "horizontal resolution:  " + picture.Image.HorizontalResolution.ToString() },
                new Label() { Text = "vertical resolution:  " + picture.Image.VerticalResolution.ToString() },
                new Label() { Text = "" }     
            };

            infoLabels.Add(new Label() { Text = "image pixel format:  " } );
            CalculatePixelFormat(picture.Image.PixelFormat.ToString(), ref infoLabels);

            infoLabels.Add(new Label() { Text = " " } );

            infoLabels.Add(new Label() { Text = "image flags:  " } );
            CalculatePictureFlags(picture.Image.Flags, ref infoLabels);


            int labelsHEIGHT = infoLabels[0].Font.Height + 3;
            int labelsWIDTH = infoLabelsContainer.Width - 20;

            foreach (var label in infoLabels)
            {
                label.AutoEllipsis = true;
                label.AutoSize = false;
                label.Height = labelsHEIGHT;
                label.Width = labelsWIDTH;
                infoLabelsContainer.Controls.Add(label);
            }

            fileInfo_iwn.PanelHeight = labelsHEIGHT * (2 + infoLabels.Count);
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
            histogram_iwn.Width = infoPanel.Width - 10;
            fileInfo_iwn.Width = infoPanel.Width - 10;

            ResizeInfoLabels();
        }

        private void histogram_tsmi_Click(object sender, EventArgs e)
        {
            imagePanel.relocatePicture_permission = false;
            ToggleInfoPanel();
            imagePanel.relocatePicture_permission = true;

            imagePanel.RelocatePicture();
        }
        #pragma warning restore IDE1006 // Naming Styles - Lowercase Methods
        #endregion


        // ########################################################################################################
        #region ImagePage Size Modifiers - Toggle / Resize / Relocate
        private void ToggleInfoPanel()
        {
            // Change the width of the form when we hide the infoPanel:     (Before toggle)
            if (collapsedInfoPanel)
                form.Width += infoPanel.ClientRectangle.Width + containerWorkspace.SplitterWidth + 1;

            // Toggle the infoPanel:
            collapsedInfoPanel = !collapsedInfoPanel;

            // Change the width of the form when we show the infoPanel:     (After toggle)
            if (collapsedInfoPanel)
                form.Width -= infoPanel.ClientRectangle.Width + containerWorkspace.SplitterWidth + 1;

            ResizeInfoLabels();
        }

        private void ResizeFormToPicture()
        {
            int tmpFormW = imagePanel.picture.Image.Width + 16;
            int tmpFormH = imagePanel.picture.Image.Height + TaskBarH + containerMenu.Height - 1;

            collapsedInfoPanel = true;
            form.Size = new Size(tmpFormW, tmpFormH);
        }
        
        private void ResizeInfoLabels()
        {
            if (infoLabels != null)
                foreach (var label in infoLabels)
                {
                    label.Width = infoLabelsContainer.Width - 20;
                }
        }
        #endregion


        // ########################################################################################################
        #region ImagePage Private Calculations
        
        






        private void CalculatePixelFormat(string value, ref List<Label> flags)
        {
            var inf = new FileInfo(filename);

            string tmp = "    ";

            for (int i=1; i< inf.Extension.Length; ++i)
                tmp += inf.Extension[i];

            tmp += " ";

            int ind = 6;
            while (value[ind] != 'b')
            {
                tmp += value[ind];
                ++ind;
            }
            ++ind; // b
            ++ind; // p
            ++ind; // p
            tmp += " bpp [ BitsPerPixel ]";

            flags.Add(new Label() { Text = tmp});

            tmp = "    ";
            for (; ind < value.Length; ++ind)
                tmp += value[ind];

            flags.Add(new Label() { Text = tmp});
        }

        private void CalculatePictureFlags(int value, ref List<Label> flags)
        {
            if (value == 0)
            {
                flags.Add(new Label() { Text = "    [ ImageFlagsNone ]" });
            }
            else
            {
                if (value >= 131072)
                {
                    flags.Add(new Label() { Text = "    [ ImageFlagsCaching ]" });
                    value -= 131072;
                }
                if (value >= 65536)
                {
                    flags.Add(new Label() { Text = "    [ ImageFlagsReadOnly ]" });
                    value -= 65536;
                }
                if (value >= 8192)
                {
                    flags.Add(new Label() { Text = "    [ ImageFlagsHasRealPixelSize ]" });
                    value -= 8192;
                }
                if (value >= 4096)
                {
                    flags.Add(new Label() { Text = "    [ ImageFlagsHasRealDPI ]" });
                    value -= 4096;
                }
                if (value >= 256)
                {
                    flags.Add(new Label() { Text = "    [ ImageFlagsColorSpaceYCCK ]" });
                    value -= 256;
                }
                if (value >= 128)
                {
                    flags.Add(new Label() { Text = "    [ ImageFlagsColorSpaceYCBCR ]" });
                    value -= 128;
                }
                if (value >= 64)
                {
                    flags.Add(new Label() { Text = "    [ ImageFlagsColorSpaceGRAY ]" });
                    value -= 64;
                }
                if (value >= 32)
                {
                    flags.Add(new Label() { Text = "    [ ImageFlagsColorSpaceCMYK ]" });
                    value -= 32;
                }
                if (value >= 16)
                {
                    flags.Add(new Label() { Text = "    [ ImageFlagsColorSpaceRGB ]" });
                    value -= 16;
                }
                if (value >= 8)
                {
                    flags.Add(new Label() { Text = "    [ ImageFlagsPartiallyScalable ]" });
                    value -= 8;
                }
                if (value >= 4)
                {
                    flags.Add(new Label() { Text = "    [ ImageFlagsHasTranslucent ]" });
                    value -= 4;
                }
                if (value >= 2)
                {
                    flags.Add(new Label() { Text = "    [ ImageFlagsHasAlpha ]" });
                    value -= 2;
                }
                if (value >= 1)
                {
                    flags.Add(new Label() { Text = "    [ ImageFlagsScalable ]" });
                }
            }
        }
        #endregion
        // ########################################################################################################
    }
}
