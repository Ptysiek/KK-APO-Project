using System;
using System.Windows.Forms;
using System.Drawing;
using KK17413_APO.Toolbox_Tools_Expanded;
using AutoMapper;


namespace KK17413_APO.Forms_and_Pages
{
    class ImagePage_Builder
    {
        public Form form;
        public Panel containerMenu;
        public SplitContainer containerWorkspace;

        public MenuStrip menuStrip;
        public ToolStripMenuItem file_tsmi;
        public ToolStripMenuItem histogram_tsmi;
        public TextBox imageScale_tb;

        public PictureBox picture;
        public FlowLayoutPanel iwnContainer;   // Image Workspace Nodes Container
        public FlowLayoutPanel infoLabelsContainer;
        public ImageWorkspaceNode histogram_iwn;
        public ImageWorkspaceNode fileInfo_iwn;
        // *iwn - Image Workspace Nodes

        public Histogram histogram;



        public ImagePage GetResult(string filename)
        {
            // [Step 1]
            CreateInstances();

            // [Step 2]
            Init_FormLayout();

            // [Step 3]
            Init_FormMenu();

            // [Step 4]
            Init_WorkspaceItems();

            // [Step 5]
            AssignParenthood();

            // [Step 6] - Create the result product by using AutoMapper:
            // Prepare the configuration for mapping:
            // The type on the left is the source type, and the type on the right is the destination type. 
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ImagePage_Builder, ImagePage>());

            // Based on the configuration perform a mapping:
            var mapper = new Mapper(config);

            // Create the Result Product - the Image Page:
            ImagePage result = mapper.Map<ImagePage>(this);

            // [Step 7] - If given, assign image to the picturebox:
            if (filename != null)
                result.AssignImage(filename);

            // [Step 8]
            result.FinalInit();
            result.ReloadLanguage();
            result.ReloadColorSet();

            result.form.Show();

            return result;  // GetResult
        }

        private void CreateInstances() // [Step 1] --------------------------------------------------------------- ###
        {
            // ImagePage form layout: 
            form = new Form();
            containerMenu = new Panel();
            containerWorkspace = new SplitContainer();

            // Menu Container Elements:
            menuStrip = new MenuStrip();
            file_tsmi = new ToolStripMenuItem();
            histogram_tsmi = new ToolStripMenuItem();
            imageScale_tb = new TextBox();

            // Image Panel Items:
            picture = new PictureBox();

            // Info Panel Items:
            iwnContainer = new FlowLayoutPanel();
            histogram_iwn = new ImageWorkspaceNode();
            fileInfo_iwn = new ImageWorkspaceNode();
            infoLabelsContainer = new FlowLayoutPanel();

            histogram = new Histogram();
        }

        private void Init_FormLayout() // [Step 2] --------------------------------------------------------------- ###
        {
            // Calculate the TaskBar Height:
            int boundsH = Screen.PrimaryScreen.Bounds.Height;
            int workingAreaH = Screen.PrimaryScreen.WorkingArea.Height;
            int TaskBarH = boundsH - workingAreaH;

            form.ShowIcon = false;

            // Init Menu Dock.Top Container:
            containerMenu.Dock = DockStyle.Top;
            containerMenu.Height = menuStrip.Height;
            containerMenu.BackColor = Color.Blue;

            // Init Workspace Dock.Fill Container:
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
            histogram_tsmi.Name = "histogram_tsmi";

            menuStrip.Items.AddRange(new ToolStripItem[]{
                file_tsmi,
                histogram_tsmi
            });

            imageScale_tb.Text = "100%";
            imageScale_tb.Location = new Point(menuStrip.Width, 0);
            imageScale_tb.Height = menuStrip.Height;
            imageScale_tb.Width = 40;
        }

        private void Init_WorkspaceItems() // [Step 4] -------------------------------------------------------- ###
        {
            picture.BorderStyle = BorderStyle.FixedSingle;
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            picture.Visible = false;

            iwnContainer.Dock = DockStyle.Fill;
            iwnContainer.BorderStyle = BorderStyle.FixedSingle;
            iwnContainer.FlowDirection = FlowDirection.TopDown;
            iwnContainer.WrapContents = false;
            iwnContainer.AutoScroll = true;

            infoLabelsContainer.Dock = DockStyle.Fill;
            infoLabelsContainer.FlowDirection = FlowDirection.TopDown;
            infoLabelsContainer.WrapContents = false;
            infoLabelsContainer.AutoScroll = true;

            //histogram.Dock = DockStyle.Fill;
            histogram_iwn.PanelHeight = 350;
        }

        private void AssignParenthood() // [Step 5] ----------------------------------------------------------- ###
        {
            // Assigning FormItems to this MainForm:   
            form.Controls.Add(containerMenu);
            containerMenu.Controls.Add(menuStrip);
            containerMenu.Controls.Add(imageScale_tb);

            form.Controls.Add(containerWorkspace);
            containerWorkspace.Panel1.Controls.Add(picture);
            containerWorkspace.Panel2.Controls.Add(iwnContainer);

            iwnContainer.Controls.Add(histogram_iwn);
            iwnContainer.Controls.Add(fileInfo_iwn);

            fileInfo_iwn.Panel2.Controls.Add(infoLabelsContainer);

            histogram_iwn.Panel2.Controls.Add(histogram);
        }
    }
}
