using System;
using System.Windows.Forms;
using System.Drawing;


namespace KK17413_APO
{
    class ImagePage_Builder
    {
        private Form form;
        private FlowLayoutPanel containerMenu;
        private SplitContainer containerWorkspace;

        private MenuStrip menuStrip;
        private ToolStripMenuItem file_tsmi;
        private ToolStripMenuItem histogram_tsmi;
        private TextBox imageScale_tb;
        
        private PictureBox picture;



        public ImagePage GetResult(string filename)
        {
            // [Step 1]
            CreateInstances();

            // [Step 2]
            Init_FormLayout(filename);

            // [Step 3]
            Init_FormMenu();

            // [Step 4]
            Init_PictureBox(filename);

            // [Step 5]
            // Assigning FormComponents to this MainForm: [Assigning parenthood]
            form.Controls.Add(containerMenu);
            containerMenu.Controls.Add(menuStrip);
            containerMenu.Controls.Add(imageScale_tb);

            form.Controls.Add(containerWorkspace);
            form.Show();

            // Create the Result Product - Image Page:
            return new ImagePage( form,
                                  containerMenu,
                                  containerWorkspace,                                  
                                  menuStrip, 
                                  file_tsmi, 
                                  histogram_tsmi, 
                                  picture, 
                                  imageScale_tb);
        }



        private void CreateInstances() // [Step 1] ------------------------------------------------ ###
        {
            // ImagePage form layout: 
            form = new Form();
            containerMenu = new FlowLayoutPanel();
            containerWorkspace = new SplitContainer();

            // Menu Container Elements:
            menuStrip = new MenuStrip();
            file_tsmi = new ToolStripMenuItem();
            histogram_tsmi = new ToolStripMenuItem();

            imageScale_tb = new TextBox();

            // Image Container Elements:
            picture = new PictureBox();
        }

        private void Init_FormLayout(string filename) // [Step 2] ------------------------------------------------ ###
        {
            // Calculate the TaskBar Height:
            int boundsH = Screen.PrimaryScreen.Bounds.Height;
            int workingAreaH = Screen.PrimaryScreen.WorkingArea.Height;
            int TaskBarH = boundsH - workingAreaH;

            // Init Form
            form.Name = filename + "_Form";
            form.Text = (filename != null) ? filename : "New Workspace";

            // Init Menu Dock.Top Container:
            containerMenu.Name = "containerMenu";
            containerMenu.Dock = DockStyle.Top;
            containerMenu.Height = menuStrip.Height;
            containerMenu.BackColor = Color.Blue;

            // Init Workspace Dock.Fill Container:
            containerWorkspace.Name = "containerWorkspace";
            containerWorkspace.Top = containerMenu.Height;
            containerWorkspace.Width = form.Width - 16;
            containerWorkspace.Height = form.Height - containerMenu.Height - TaskBarH + 1;

            containerWorkspace.FixedPanel = FixedPanel.Panel2;
            containerWorkspace.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);

            containerWorkspace.Panel2Collapsed = true;
            containerWorkspace.BorderStyle = BorderStyle.FixedSingle;
            containerWorkspace.BackColor = Color.Blue;

            // imagePanel - Panel 1:
            containerWorkspace.Panel1.BackColor = Color.Red;

            // infoPanel - Panel 2:
            containerWorkspace.Panel2.BackColor = Color.Green;
        }
        
        private void Init_FormMenu() // [Step 3] ----------------------------------------------------------------- ###
        {
            file_tsmi.Name = "file_tsmi";
            file_tsmi.Text = ProgramLanguage.GetValue("file_tsmi");

            histogram_tsmi.Name = "histogram_tsmi";
            histogram_tsmi.Text = ProgramLanguage.GetValue("histogram_tsmi");

            menuStrip.Items.AddRange(new ToolStripItem[]{
                file_tsmi,
                histogram_tsmi
            });

            imageScale_tb.Name = "imageScale_tb";
            imageScale_tb.Text = "100%";
            imageScale_tb.Location = new Point(menuStrip.Width, 0);
            imageScale_tb.Height = menuStrip.Height;
            imageScale_tb.Width = 40;
        }

        private void Init_PictureBox(string filename) // [Step 4] ------------------------------------------------ ###
        {
            picture.Name = filename + "_Picture";
            //picture.SizeMode = PictureBoxSizeMode.AutoSize;
            //picture.Dock = DockStyle.None;
            picture.BorderStyle = BorderStyle.FixedSingle;
            picture.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
