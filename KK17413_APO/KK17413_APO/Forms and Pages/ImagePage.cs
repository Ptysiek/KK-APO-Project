using System;
using System.Windows.Forms;
using System.Drawing;


namespace KK17413_APO
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


            this.form.Resize += new EventHandler(form_Resize);
            this.picture.MouseWheel += new MouseEventHandler(image_ScrollResize);
            this.histogram_tsmi.Click += new EventHandler(histogram_tsmi_Click);


            //ResizeForm();

            this.form.Show();
        }

        public void AssignImage(string filename)
        {
            picture.Image = new Bitmap(filename);

            picture.Width = picture.Image.Width;
            picture.Height = picture.Image.Height;
            picture.Visible = true;



            int tmpFormW = picture.Image.Width + 16;
            int tmpFormH = picture.Image.Height + TaskBarH + containerMenu.Height - 1;

            if (picture.Image.Width < 50)
                tmpFormW = 50 + 16;
            if (picture.Image.Height < 50)
                tmpFormH = 50 + TaskBarH + containerMenu.Height - 1;

            form.Size = new Size(tmpFormW, tmpFormH);
            form.Text = filename;
            collapsedInfoPanel = true;
        }


        // ##########################################################################
        public void form_Resize(object sender, EventArgs e)
        {
            //ResizeForm();
        }


        public void histogram_tsmi_Click(object sender, EventArgs e)
        {
            collapsedInfoPanel = !collapsedInfoPanel;
        }




        public void image_ScrollResize(object sender, MouseEventArgs e)
        {
            // Decide if its Scroll_Up, or Scroll_Down:
            bool positive = (e.Delta > 0) ? true : false;

            int newScale = CalculatePictureScale(positive);

            ResizePicture(newScale);

            RelocatePicture(positive, e.Location);
        }





        // ##########################################################################
        //bool _expendWindow;
        /*
        private void TogleFormExpandWindow()
        {
            if (!initialized) return;

            expendWindow = !expendWindow;

            if (expendWindow)
            {                
                //form.Width
                int tmpW = containerImage.Width;
                int tmpH = containerImage.Height;

                lastKnownContainerImage_W = containerImage.Width;
                lastKnownContainerImage_H = containerImage.Height;

                containerInfo.Width = 30 * containerImage.Width / 70;

                // Resize Form:
                form.Width = containerImage.Width + containerInfo.Width;

                containerWorkspace.ColumnCount = 2;
                containerWorkspace.RowCount = 1;

                containerInfo.Visible = true;

                containerImage.Width = tmpW;
            }
            else
            {
                containerInfo.Visible = false;

                containerWorkspace.ColumnCount = 1;
                containerWorkspace.RowCount = 1;

                // Calculate the TaskBar Height, for better image position.
                //int PSBH = Screen.PrimaryScreen.Bounds.Height;
                //int TaskBarHeight = PSBH - Screen.PrimaryScreen.WorkingArea.Height;

                //form.Size = new Size(file.Size.Width + 100, file.Size.Height + TaskBarHeight);
                //form.Size = new Size(lastKnownContainerImage_W, lastKnownContainerImage_H + containerMenu.Height);
                form.Width = lastKnownContainerImage_W + 20;
            }
            ResizeForm();
            //containerInfo.Width = 0;
        }

        int lastKnownContainerImage_W = 0;
        int lastKnownContainerImage_H = 0;

        private void ResizeForm()
        {
            containerImage.SuspendLayout();

            if (containerImage.Width > form.Width)
                containerImage.Width = form.Width - 20;

            picture.Left = (containerImage.Width - picture.Width) / 2;
            picture.Top = (containerImage.Height - picture.Height + containerMenu.Height) / 2;
            
            containerImage.PerformLayout();
        }
        */


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
        

        private void ResizePicture(int scaleValue)
        {
            /*  NOTES:
                Current Picture size:
                    picture.ClientSize.Width
                    picture.ClientSize.Height

                Oryginal Picture size:
                    picture.Image.Width
                    picture.Image.Height
            */

            // Calculate the proportion from the original dimensions:
            int imageSizeW = scaleValue * picture.Image.Width / 100;
            int imageSizeH = scaleValue * picture.Image.Height / 100;

            // Resize the Image:
            picture.ClientSize = new Size(imageSizeW, imageSizeH);
        }







        private void RelocatePicture(bool positive, Point mouseLocation)
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
                if (positive)   additional_Xpos += (dif_Xpos > 0) ? (transpose_X) : (-transpose_X);
                else            additional_Xpos -= (dif_Xpos > 0) ? (transpose_X) : (-transpose_X);
            }
            else { additional_Xpos = 0; }   // Image Centering, (only on X axis)


            // ====================================================================================
            // Check if Picture is higher than picturePanel:
            if (picture.Height > imagePanel.Height + transpose_Y)
            {
                // Set an additional picture shift value:
                if (positive)   additional_Ypos += (dif_Ypos > 0) ? (transpose_Y) : (-transpose_Y);
                else            additional_Ypos -= (dif_Ypos > 0) ? (transpose_Y) : (-transpose_Y);
            }
            else { additional_Ypos = 0; }   // Image Centering, (only on Y axis)


            // ====================================================================================
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

    }
}
