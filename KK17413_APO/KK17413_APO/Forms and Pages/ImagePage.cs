using System;
using System.Windows.Forms;
using System.Drawing;
using KK17413_APO.Toolbox_Tools_Expanded;


namespace KK17413_APO.Forms_and_Pages
{
    class ImagePage
    {
        // #####################################################################
        private Form form;
        private FlowLayoutPanel containerMenu;
        private SplitContainer containerWorkspace;
        private SplitterPanel imagePanel;
        private SplitterPanel infoPanel;

        private MenuStrip menuStrip;
        private ToolStripMenuItem file_tsmi;
        private ToolStripMenuItem histogram_tsmi;
        private TextBox imageScale_tb;

        private PictureBox picture;
        private int additional_Xpos = 0;
        private int additional_Ypos = 0;
        private bool relocatePicture_permission;

        private AccordionContainer accordion;
        private AccordionNode histogram_an;
        private AccordionNode fileInfo_an;


        // #####################################################################
        private bool collapsedInfoPanel {
            get { return containerWorkspace.Panel2Collapsed; }
            set { containerWorkspace.Panel2Collapsed = value; }
        }
        private int TaskBarH {           
            get {
                // Calculate the TaskBar Height:
                return Screen.PrimaryScreen.Bounds.Height - 
                       Screen.PrimaryScreen.WorkingArea.Height;
            }
        }

        // #####################################################################
        public ImagePage(   Form form,
                            FlowLayoutPanel containerMenu,
                            SplitContainer containerWorkspace,

                            MenuStrip menuStrip,
                            ToolStripMenuItem file_tsmi,
                            ToolStripMenuItem histogram_tsmi,
                            TextBox imageScale_tb,

                            PictureBox picture,
                            AccordionContainer accordion,
                            AccordionNode histogram_an,
                            AccordionNode fileInfo_an
                        )
        {
            this.form = form;
            this.containerMenu = containerMenu;
            this.containerWorkspace = containerWorkspace;
            imagePanel = this.containerWorkspace.Panel1;
            infoPanel = this.containerWorkspace.Panel2;

            this.menuStrip = menuStrip;
            this.file_tsmi = file_tsmi;
            this.histogram_tsmi = histogram_tsmi;
            this.imageScale_tb = imageScale_tb;

            this.picture = picture;
            this.accordion = accordion;
            this.histogram_an = histogram_an;
            this.fileInfo_an = fileInfo_an;



            this.containerWorkspace.SplitterMoved += new SplitterEventHandler(workspace_SplitterMoved);
            this.form.Resize += new EventHandler(form_Resize);
            this.picture.MouseWheel += new MouseEventHandler(image_ScrollResize);
            this.histogram_tsmi.Click += new EventHandler(histogram_tsmi_Click);

            
            relocatePicture_permission = true;

            ReloadLanguage();
            ReloadColorSet();

            this.form.Show();
        }





        // ########################################################################################################
        #region Event Handlers
        #pragma warning disable IDE1006 // Naming Styles - Lowercase Methods
        private void form_Resize(object sender, EventArgs e)
        {
            RelocatePicture();
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
        #region ImagePage Operations
        public void AssignImage(string filename)
        {
            picture.Image = new Bitmap(filename);
            picture.Width = picture.Image.Width;
            picture.Height = picture.Image.Height;


            // First, resize the form:
            ResizeFormToPicture();
            form.Text = filename;

            // Then, set the pictureBox visible:
            picture.Visible = true;
        }
        private void ReloadLanguage()
        {
            file_tsmi.Text = ProgramSettings.language.GetValue("file_tsmi");
            histogram_tsmi.Text = ProgramSettings.language.GetValue("histogram_tsmi");
        }
        private void ReloadColorSet()
        {
            menuStrip.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            menuStrip.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            containerMenu.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            containerWorkspace.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");

            imagePanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");
            infoPanel.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");
            accordion.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");

            //imageScale_tb.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            //imageScale_tb.BackColor = ProgramSettings.ColorManager.GetValue("detailColor2");
        }
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
        #endregion
        // ########################################################################################################
    }
}
