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



            HScrollBar h_scrollbar = new HScrollBar();
            h_scrollbar = new HScrollBar()
            {
                Dock = DockStyle.Bottom
            };


            VScrollBar v_scrollbar = new VScrollBar();
            PictureBox picture = new PictureBox();




            Bitmap bitmap = new Bitmap(fileName);


            // Create new PictureBox for the Bitmap
            picture = new PictureBox()
            {
                SizeMode = PictureBoxSizeMode.AutoSize,     // Used to calculate the form size
                Name = fileName + "_Picture",
                Image = bitmap
            };

            // Calculate the TaskBar Height, for better image position.
            int PSBH = Screen.PrimaryScreen.Bounds.Height;
            int TaskBarHeight = PSBH - Screen.PrimaryScreen.WorkingArea.Height;


            //form.Size = new Size(file.Size.Width + 100, file.Size.Height + TaskBarHeight);
            form.Size = new Size(picture.Size.Width, picture.Size.Height + TaskBarHeight + h_scrollbar.Height);

            picture.Dock = DockStyle.Fill;
            picture.SizeMode = PictureBoxSizeMode.CenterImage;

            h_scrollbar.Anchor = picture.Anchor;
            h_scrollbar.AutoScrollOffset = picture.Location;

            // Place the PictureBox inside the new Form.
            form.Controls.Add(menuStrip);
            form.Controls.Add(h_scrollbar);
            form.Controls.Add(picture);

            form.Show();



            return new ImagePage(form, menuStrip, h_scrollbar, v_scrollbar, picture);
        }
    }
}
