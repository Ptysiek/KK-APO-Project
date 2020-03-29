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


            ListBox containerBOX = new ListBox()
            {
                Enabled = false,
                Name = "BookMark_ContainerBOX",
                BorderStyle = BorderStyle.Fixed3D,
                Location = new Point(50, 0),
                Height = 43,
                //BackColor = Color.Silver
            };


            MenuStrip menuStrip = new MenuStrip()
            {
                //Dock = DockStyle.None,
                //Width = form.Width - 16

            };           
            



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
            form.Size = new Size(picture.Size.Width+16, picture.Size.Height + TaskBarHeight);

            //picture.Dock = DockStyle.Fill;
            picture.SizeMode = PictureBoxSizeMode.CenterImage;


            TextBox imageScale_tb;


            imageScale_tb = new TextBox()
            {
                //Dock = DockStyle.Bottom
                Name = "hScrollBar1",
                //Location = new Point(711, 18),
                Size = new Size(80, 21),
                Text = "100%",
                Dock = DockStyle.Left
                //Enabled = false
            };


            containerBOX.Width = form.Width - 16;
            // Place the PictureBox inside the new Form.
            //containerBOX.Controls.Add(menuStrip);
            //containerBOX.Controls.Add(imageScale_tb);
            //form.Controls.Add(containerBOX);
            form.Controls.Add(menuStrip);
            form.Controls.Add(picture);
            //form.Controls.Add(imageScale_tb);
            imageScale_tb.Enabled = true;

            form.Show();

            ToolStripMenuItem file_tsmi = new ToolStripMenuItem();

            file_tsmi.Name = "file_tsmi";
            file_tsmi.Text = ProgramLanguage.GetValue("file_tsmi");

            menuStrip.Items.AddRange(new ToolStripItem[]{
                file_tsmi
            });



            //menuStrip.Items.Add("File");


            ImagePage resultPage = new ImagePage(form, containerBOX, menuStrip, picture, imageScale_tb);


            return resultPage;
        }
    }
}
