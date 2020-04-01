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
                //Location = new Point(0, 0),
                //Dock = DockStyle.Fill,
                Name = fileName + "_Picture",
                Image = new Bitmap(fileName),
                SizeMode = PictureBoxSizeMode.AutoSize,     // Used to calculate the form size                
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.None
            };
            picture.SizeMode = PictureBoxSizeMode.Zoom;



            TextBox imageScale_tb = new TextBox()
            {
                Location = new Point(menuStrip.Width, 0),
                Name = "imageScale_tb",
                Text = "100%",
                Width = 40
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

            

            // Calculate the TaskBar Height, for better image position.
            int boundsH = Screen.PrimaryScreen.Bounds.Height;
            int workingAreaH = Screen.PrimaryScreen.WorkingArea.Height;
            int TaskBarH = boundsH - workingAreaH;

            form.Size = new Size(picture.Image.Width + 20, picture.Image.Height + TaskBarH + containerMenu.Height);


            form.Controls.Add(containerMenu);
            containerMenu.Controls.Add(menuStrip);
            containerMenu.Controls.Add(imageScale_tb);

            form.Controls.Add(containerWorkspace);
            containerWorkspace.Controls.Add(containerImage);
            containerWorkspace.Controls.Add(containerInfo);

            containerImage.Controls.Add(picture);

            form.Show();

            return new ImagePage( form, 
                                  containerMenu, 
                                  containerWorkspace, 
                                  containerImage, 
                                  containerInfo, 
                                  
                                  menuStrip, 
                                  file_tsmi, 
                                  histogram_tsmi, 

                                  picture, 
                                  imageScale_tb);
        }

        public static ImagePage GetResult()
        {
            Form form = new Form();
            FlowLayoutPanel containerMenu = new FlowLayoutPanel();
            TableLayoutPanel containerWorkspace = new TableLayoutPanel();

            MenuStrip menuStrip = new MenuStrip();
            ToolStripMenuItem file_tsmi = new ToolStripMenuItem();
            ToolStripMenuItem histogram_tsmi = new ToolStripMenuItem();




            file_tsmi.Name = "file_tsmi";
            file_tsmi.Text = ProgramLanguage.GetValue("file_tsmi");

            histogram_tsmi.Name = "histogram_tsmi";
            histogram_tsmi.Text = ProgramLanguage.GetValue("histogram_tsmi");

            menuStrip.Items.AddRange(new ToolStripItem[]{
                file_tsmi,
                histogram_tsmi
            });

            TextBox imageScale_tb = new TextBox()
            {
                Location = new Point(menuStrip.Width, 0),
                Name = "imageScale_tb",
                Text = "100%",
                Width = 40
            };

            //form.Name = fileName + "_Form";
            //form.Text = fileName;
            form.Name = "_Form";
            form.Text = "_Form";


            containerMenu.Name = "containerMenu";
            containerMenu.Dock = DockStyle.Top;
            containerMenu.BackColor = Color.Blue;
            containerMenu.Height = menuStrip.Height;


            containerWorkspace.Name = "containerWorkspace";
            containerWorkspace.Dock = DockStyle.Fill;
            containerWorkspace.BackColor = Color.Gray;
            containerWorkspace.ColumnCount = 1;
            containerWorkspace.RowCount = 1;




            form.Controls.Add(containerMenu);
            containerMenu.Controls.Add(menuStrip);
            containerMenu.Controls.Add(imageScale_tb);
            form.Controls.Add(containerWorkspace);


            FlowLayoutPanel containerImage = new FlowLayoutPanel();
            FlowLayoutPanel containerInfo = new FlowLayoutPanel();
            PictureBox picture = new PictureBox();

            return new ImagePage(form,
                                  containerMenu,
                                  containerWorkspace,
                                  containerImage,
                                  containerInfo,

                                  menuStrip,
                                  file_tsmi,
                                  histogram_tsmi,

                                  picture,
                                  imageScale_tb);
        }
    }
}
