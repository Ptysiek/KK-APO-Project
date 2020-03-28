using System;
using System.Windows.Forms;
using System.Drawing;


namespace KK17413_APO
{
    class ImagePage_Builder
    {
        public static ImagePage GetResult(string fileName)
        {
            //Form form = new Form();
            Form form = new Form()
            {
                Name = fileName + "_Form",
                Text = fileName
            };


            MenuStrip menuStrip = new MenuStrip();            
            menuStrip.Items.Add("File");



            Bitmap bitmap = new Bitmap(fileName);


            // Create new PictureBox for the Bitmap
            PictureBox picture;
            picture = new PictureBox()
            {
                SizeMode = PictureBoxSizeMode.AutoSize,     // Used to calculate the form size
                Name = fileName + "_Picture",
                Image = bitmap,
                BorderStyle = BorderStyle.FixedSingle

            };

            // Calculate the TaskBar Height, for better image position.
            int PSBH = Screen.PrimaryScreen.Bounds.Height;
            int TaskBarHeight = PSBH - Screen.PrimaryScreen.WorkingArea.Height;



            //form.Size = new Size(file.Size.Width + 100, file.Size.Height + TaskBarHeight);
            form.Size = new Size(picture.Size.Width, picture.Size.Height + TaskBarHeight);

            //picture.Dock = DockStyle.Fill;
            picture.SizeMode = PictureBoxSizeMode.CenterImage;




            // Place the PictureBox inside the new Form.
            form.Controls.Add(menuStrip);
            form.Controls.Add(picture);

            form.Show();





            TextBox imageScale_tb;


            imageScale_tb = new TextBox()
            {
                //Dock = DockStyle.Bottom
                Name = "hScrollBar1",
                Location = new Point(711, 18),
                Size = new Size(80, 21),
                Text = "100%"
                //Enabled = false
            };

            form.Controls.Add(imageScale_tb);
            


            ImagePage resultPage = new ImagePage(form, menuStrip, picture, imageScale_tb);


            return resultPage;
        }
    }
}
