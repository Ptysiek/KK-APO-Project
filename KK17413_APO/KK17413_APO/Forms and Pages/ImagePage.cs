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
        public SplitterPanel imagePanel;
        public SplitterPanel infoPanel;

        public MenuStrip menuStrip;
        public ToolStripMenuItem file_tsmi;
        public ToolStripMenuItem histogram_tsmi;
        public TextBox imageScale_tb;

        private Bitmap bitmap;
        public PictureBox picture;
        public FlowLayoutPanel iwnContainer;   // Image Workspace Nodes Container
        public ImageWorkspaceNode histogram_iwn;
        public ImageWorkspaceNode fileInfo_iwn;
        // *iwn - Image Workspace Nodes

        public FlowLayoutPanel infoLabelsContainer;
        public List<Label> infoLabels;
        //public Histogram histogram;
        public HistogramPanel histogramPanel;
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

        // Picture Calculations:
        private string filename;
        private int additional_Xpos = 0;
        private int additional_Ypos = 0;
        private bool relocatePicture_permission;


        // ########################################################################################################
        #region ImagePage Operations      
        public void FinalInit()
        {           
            imagePanel = this.containerWorkspace.Panel1;
            infoPanel = this.containerWorkspace.Panel2;

            relocatePicture_permission = true;

            this.form.Resize += form_Resize;
            this.form.FormClosed += Form_AfterFormClosed;
            this.picture.MouseWheel += image_ScrollResize;
            this.containerWorkspace.SplitterMoved += workspace_SplitterMoved;
            this.histogram_tsmi.Click += histogram_tsmi_Click;

            RelocatePicture();
        }

        public void AssignImage(string filename)
        {
            this.filename = filename;

            bitmap = new Bitmap(filename);
            picture.Image = bitmap;
            picture.Width = picture.Image.Width;
            picture.Height = picture.Image.Height;

            // First, resize the form:
            ResizeFormToPicture();
            RelocatePicture();
            form.Text = filename;

            // Then, set the pictureBox visible:
            picture.Visible = true;

            ReloadImageInfo();
            histogramPanel.Red_hist.ReloadHistogram(bitmap);
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
            RelocatePicture();
        }

        private void Form_AfterFormClosed(object sender, FormClosedEventArgs e)
        {
            pageHandle.DetachItself();
            pageHandle = null;

            ProgramSettings.Pages.Remove(this);
        }

        private void image_ScrollResize(object sender, MouseEventArgs e)
        {
            // Decide if its Scroll_Up, or Scroll_Down:
            bool positive = (e.Delta > 0) ? true : false;

            // Calculations for picture resizement:
            int newScale = CalculatePictureScale(positive);

            // Calculations for picture relocation:
            CalculateImageShifts(positive, e.Location);


            ResizePicture(newScale);
            RelocatePicture();
        }

        private void workspace_SplitterMoved(object sender, SplitterEventArgs e)
        {
            RelocatePicture();
            histogram_iwn.Width = infoPanel.Width - 10;
            fileInfo_iwn.Width = infoPanel.Width - 10;

            ResizeInfoLabels();
        }

        private void histogram_tsmi_Click(object sender, EventArgs e)
        {
            relocatePicture_permission = false;
            ToggleInfoPanel();
            relocatePicture_permission = true;

            RelocatePicture();
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

        private void ResizePicture(int scaleValue)
        {
            /*  NOTES:
                Current Picture size:   { picture.ClientSize.Width ; picture.ClientSize.Height }
                Oryginal Picture size:  { picture.Image.Width ; picture.Image.Height }
            */

            // Calculate the proportion from the original dimensions:
            int imageSizeW = scaleValue * picture.Image.Width / 100;
            int imageSizeH = scaleValue * picture.Image.Height / 100;

            // Resize the Image:
            picture.ClientSize = new Size(imageSizeW, imageSizeH);
        }

        private void RelocatePicture()
        {
            if (!relocatePicture_permission) return;

            int X_calculation;    // Comes to the final result:
            X_calculation = (imagePanel.Width - picture.Width);   // checking the empty space between,
            X_calculation /= 2;                                   // centering by dividing,
            X_calculation += additional_Xpos;                     // sum with the shift difference

            int Y_calculation;    // Comes to the final result:
            Y_calculation = (imagePanel.Height - picture.Height); // checking the empty space between,
            Y_calculation /= 2;                                   // centering by dividing,
            Y_calculation += additional_Ypos;                     // sum with the shift difference

            picture.Left = X_calculation;
            picture.Top = Y_calculation;
        }

        private void ResizeFormToPicture()
        {
            int tmpFormW = picture.Image.Width + 16;
            int tmpFormH = picture.Image.Height + TaskBarH + containerMenu.Height - 1;

            if (picture.Image.Width < 50)
                tmpFormW = 50 + 16;
            if (picture.Image.Height < 50)
                tmpFormH = 50 + TaskBarH + containerMenu.Height - 1;

            form.Size = new Size(tmpFormW, tmpFormH);
            collapsedInfoPanel = true;
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
        private int CalculatePictureScale(bool positive)
        {
            // Take value from: imageScale_tb:
            string text = "";
            string tmp = imageScale_tb.Text;

            // Transfer this value without the percent character:
            for (int i = 0; i < tmp.Length; ++i)
                if (tmp[i] == '%') break;
                else text += tmp[i];

            // Save it into two types of values:
            float valueF = float.Parse(text);       // Float
            int valueI = Convert.ToInt32(valueF);   // Int

            // Calculate the resultScale:
            if (valueF > 490) valueI = (positive) ? 500 : 490;    // Checks the upper limit
            else if (valueF < 20) valueI = (positive) ? 20 : 10;      // Checks the lower limit
            else
            {
                // Prepare tmpvalue - the value variable without unit digit value:
                int tmpvalue = valueI;
                tmpvalue /= 10;
                tmpvalue *= 10;

                // Increase value:
                if (positive) valueI = (tmpvalue == valueF) ? (valueI + 10) : (tmpvalue + 10);
                // Decrease value:
                else valueI = (tmpvalue == valueF) ? (valueI - 10) : (tmpvalue);
            }

            // Update imageScale_tb with new scale value:
            imageScale_tb.Text = valueI.ToString() + "%";

            // Done:
            return valueI;
        }

        private void CalculateImageShifts(bool positive, Point mouseLocation)
        {
            // Todo improvements:
            // Let the zoomOut always be centered. Instead of configuring it into the mouse position.

            // Calculate the difference between two points:
            int dif_Xpos = (picture.Width / 2) - mouseLocation.X;
            int dif_Ypos = (picture.Height / 2) - mouseLocation.Y;

            int transpose_X = CalculatePictureTranspose(picture.Width, mouseLocation.X);
            int transpose_Y = CalculatePictureTranspose(picture.Height, mouseLocation.Y);

            // ====================================================================================
            // Check if Picture is widder than picturePanel:
            if (picture.Width > imagePanel.Width + transpose_X)
            {
                // Set an additional picture shift value:
                if (positive) additional_Xpos += (dif_Xpos > 0) ? (transpose_X) : (-transpose_X);
                else additional_Xpos -= (dif_Xpos > 0) ? (transpose_X) : (-transpose_X);
            }
            else { additional_Xpos = 0; }   // Image Centering, (only on X axis)

            // ====================================================================================
            // Check if Picture is higher than picturePanel:
            if (picture.Height > imagePanel.Height + transpose_Y)
            {
                // Set an additional picture shift value:
                if (positive) additional_Ypos += (dif_Ypos > 0) ? (transpose_Y) : (-transpose_Y);
                else additional_Ypos -= (dif_Ypos > 0) ? (transpose_Y) : (-transpose_Y);
            }
            else { additional_Ypos = 0; }   // Image Centering, (only on Y axis)
        }

        private static int CalculatePictureTranspose(int pictureSize, int mousePos)
        {
            // Calculate the absolute deviation between 
            // mousePos and the center of the image:
            int deviation;
            deviation = (pictureSize / 2) - mousePos;
            deviation = Math.Abs(deviation);

            // Calculate the percentage of 
            // deviation from the proportion:
            return deviation * 100 / pictureSize;
        }
        
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
