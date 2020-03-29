using System;
using System.Windows.Forms;
using System.Drawing;


namespace KK17413_APO
{
    class ImagePage_Builder
    {

        public static ImagePage GetResult(string fileName)
        {
            Form form = new Form()
            {
                Name = fileName + "_Form",
                Text = fileName
            };

            FlowLayoutPanel containerMenu = new FlowLayoutPanel()
            {
                Name = "containerMenu",
                Dock = DockStyle.Top,
                BackColor = Color.Blue
            };
            TableLayoutPanel containerWorkspace = new TableLayoutPanel()
            {
                Name = "containerWorkspace",
                Dock = DockStyle.Fill,
                //BackColor = Color.Black,
                ColumnCount = 1,
                RowCount = 1
            };
            FlowLayoutPanel containerImage = new FlowLayoutPanel()
            {
                Name = "containerImage",
                Dock = DockStyle.Fill,
                BackColor = Color.Red
            };

            FlowLayoutPanel containerInfo = new FlowLayoutPanel()
            {
                Name = "containerInfo",
                Dock = DockStyle.Fill,
                BackColor = Color.Yellow,
                Visible = false
            };





            MenuStrip menuStrip = new MenuStrip()
            {
                //Dock = DockStyle.None,
                //Width = form.Width - 16                
            };

            PictureBox picture = new PictureBox()
            {
                //Location = new Point(0, menuStrip.Height),
                Location = new Point(0, 0),
                SizeMode = PictureBoxSizeMode.AutoSize,     // Used to calculate the form size
                Name = fileName + "_Picture",
                Image = new Bitmap(fileName),
                BorderStyle = BorderStyle.FixedSingle
            };

            TextBox imageScale_tb = new TextBox()
            {
                //Dock = DockStyle.Bottom
                Location = new Point(menuStrip.Width, 0),
                Name = "imageScale_tb",
                //Location = new Point(711, 18),
                Size = new Size(80, 21),
                Text = "100%"
                //Dock = DockStyle.Left
                //Enabled = false
            };

            containerMenu.Height = menuStrip.Height;


            ToolStripMenuItem file_tsmi = new ToolStripMenuItem();
            file_tsmi.Name = "file_tsmi";
            file_tsmi.Text = ProgramLanguage.GetValue("file_tsmi");

            ToolStripMenuItem histogram_tsmi = new ToolStripMenuItem();
            histogram_tsmi.Name = "histogram_tsmi";
            histogram_tsmi.Text = ProgramLanguage.GetValue("histogram_tsmi");

            menuStrip.Items.AddRange(new ToolStripItem[]{
                file_tsmi,
                histogram_tsmi
            });



            form.Controls.Add(containerMenu);
            containerMenu.Controls.Add(menuStrip);

            form.Controls.Add(containerWorkspace);
            containerWorkspace.Controls.Add(containerImage);
            containerWorkspace.Controls.Add(containerInfo);


            form.Show();

            ImagePage resultPage = new ImagePage(form, containerMenu, containerWorkspace, containerImage, containerInfo, menuStrip, file_tsmi, histogram_tsmi, picture, imageScale_tb);

            return resultPage;
        }
        
        public static ImagePage Welp(string fileName)
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
                Dock = DockStyle.None,
                Width = form.Width - 16

            };           
            



            Bitmap bitmap = new Bitmap(fileName);


            // Create new PictureBox for the Bitmap
            PictureBox picture;
            picture = new PictureBox()
            {
                //Location = new Point(0, menuStrip.Height),
                Location = new Point(0, 0),
                SizeMode = PictureBoxSizeMode.AutoSize,     // Used to calculate the form size
                Name = fileName + "_Picture",
                Image = bitmap,
                BorderStyle = BorderStyle.FixedSingle

            };


            //picture.Dock = DockStyle.Fill;
            picture.SizeMode = PictureBoxSizeMode.CenterImage;


            TextBox imageScale_tb;


            imageScale_tb = new TextBox()
            {
                //Dock = DockStyle.Bottom
                
                Location = new Point(menuStrip.Width, 0),
                Name = "imageScale_tb",
                //Location = new Point(711, 18),
                Size = new Size(80, 21),
                Text = "100%"
                //Dock = DockStyle.Left
                //Enabled = false
            };

            //containerBOX.Width = form.Width - 15;
            //containerBOX.Height = form.Height - menuStrip.Height - TaskBarHeight;

            // Calculate the TaskBar Height, for better image position.
            int PSBH = Screen.PrimaryScreen.Bounds.Height;
            int TaskBarHeight = PSBH - Screen.PrimaryScreen.WorkingArea.Height;

            //form.Size = new Size(file.Size.Width + 100, file.Size.Height + TaskBarHeight);
            form.Size = new Size(picture.Size.Width + 16, picture.Size.Height + TaskBarHeight + menuStrip.Height);


            containerBOX.Dock = DockStyle.Bottom;
            containerBOX.Height = form.Height - menuStrip.Height - TaskBarHeight;
            //containerBOX.Location = new Point(0, menuStrip.Height);
            containerBOX.Enabled = true;

            // Place the PictureBox inside the new Form.
            //containerBOX.Controls.Add(menuStrip);
            containerBOX.Controls.Add(picture);
            form.Controls.Add(containerBOX);
            form.Controls.Add(menuStrip);
            //form.Controls.Add(picture);
            form.Controls.Add(imageScale_tb);
            imageScale_tb.Enabled = true;

            form.Show();

            ToolStripMenuItem file_tsmi = new ToolStripMenuItem();

            file_tsmi.Name = "file_tsmi";
            file_tsmi.Text = ProgramLanguage.GetValue("file_tsmi");

            menuStrip.Items.AddRange(new ToolStripItem[]{
                file_tsmi
            });
            
                            ToolStripMenuItem histogram_tsmi = new ToolStripMenuItem();

            FlowLayoutPanel containerMenu = new FlowLayoutPanel();
            TableLayoutPanel containerWorkspace = new TableLayoutPanel();
            FlowLayoutPanel containerImage = new FlowLayoutPanel();
            FlowLayoutPanel containerInfo = new FlowLayoutPanel();


        ImagePage resultPage = new ImagePage(form, containerMenu, containerWorkspace, containerImage, containerInfo, menuStrip, file_tsmi, histogram_tsmi, picture, imageScale_tb);


            return resultPage;
        }
    }
}
