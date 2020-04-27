﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KK17413_APO.Panels_Expanded
{
    [System.ComponentModel.DesignerCategory("")]
    public class ImagePanel : Panel
    {
        public PictureBox picture;
        public TextBox imageScale_tb;
        // Picture Calculations:

        private int additional_Xpos = 0;
        private int additional_Ypos = 0;
        public bool relocatePicture_permission;



        public ImagePanel()
        {
            imageScale_tb = new TextBox();
            imageScale_tb.Text = "100%";
            //imageScale_tb.Location = new Point(menuStrip.Width, 0);
            //imageScale_tb.Height = menuStrip.Height;
            imageScale_tb.Width = 40;

            picture = new PictureBox();
            picture.BorderStyle = BorderStyle.FixedSingle;
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            picture.Visible = false;

            this.Dock = DockStyle.Fill;
            this.Controls.Add(picture);

            this.picture.MouseWheel += image_ScrollResize;
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





        public void AssignImage(Bitmap bitmap)
        {
            
            picture.Image = bitmap;
            picture.Width = picture.Image.Width;
            picture.Height = picture.Image.Height;

            // First, resize the form:
            //ResizeFormToPicture();
            RelocatePicture();
            //form.Text = filename;

            // Then, set the pictureBox visible:
            picture.Visible = true;

            
            //histogramPanel.RecalculateHistograms(bitmap);
        }







        public void ResizePicture(int scaleValue)
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

        public void RelocatePicture()
        {
            if (!relocatePicture_permission) return;

            int X_calculation;    // Comes to the final result:
            X_calculation = (this.Width - picture.Width);   // checking the empty space between,
            X_calculation /= 2;                                   // centering by dividing,
            X_calculation += additional_Xpos;                     // sum with the shift difference

            int Y_calculation;    // Comes to the final result:
            Y_calculation = (this.Height - picture.Height); // checking the empty space between,
            Y_calculation /= 2;                                   // centering by dividing,
            Y_calculation += additional_Ypos;                     // sum with the shift difference

            picture.Left = X_calculation;
            picture.Top = Y_calculation;
        }







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
            if (picture.Width > this.Width + transpose_X)
            {
                // Set an additional picture shift value:
                if (positive) additional_Xpos += (dif_Xpos > 0) ? (transpose_X) : (-transpose_X);
                else additional_Xpos -= (dif_Xpos > 0) ? (transpose_X) : (-transpose_X);
            }
            else { additional_Xpos = 0; }   // Image Centering, (only on X axis)

            // ====================================================================================
            // Check if Picture is higher than picturePanel:
            if (picture.Height > this.Height + transpose_Y)
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

    }
}