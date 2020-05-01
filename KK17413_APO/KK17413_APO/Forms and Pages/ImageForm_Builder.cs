using System;
using System.Windows.Forms;
using System.Drawing;
using KK17413_APO.Toolbox_Tools_Expanded;
using AutoMapper;
using KK17413_APO.Panels_Expanded;

namespace KK17413_APO.Forms_and_Pages
{
    class ImageForm_Builder
    {
        public Form form;
        public Panel containerMenu;
        public SplitContainer containerWorkspace;

        //public FlowLayoutPanel iwnContainer;   // Image Workspace Nodes Container
        //public AdjustedSplitContainer histogram_iwn;
        //public AdjustedSplitContainer fileInfo_iwn;
        //public Panel bottomMargin_iwn;
        // *iwn - Image Workspace Nodes

        public MenuStrip menuStrip;
        public ToolStripMenuItem file_tsmi;
        public ToolStripMenuItem histogram_tsmi;
        public ToolStripMenuItem fileInfo_tsmi;

        public ImageWorkspace imageLeftWingPanel;
        public InfoWorkspace infoRightWingPanel;

        //public HistogramPanel histogramPanel;
        //public InfoPanel infoPanel;


        public ImageForm GetResult(string filename)
        {
            // [Step 1]
            CreateInstances();

            // [Step 2]
            Init_FormLayout();

            // [Step 3]
            Init_FormMenu();

            // [Step 4]
            Configure_ImageWorkspace();

            // [Step 5]
            AssignParenthood();

            // [Step 6] - Create the result product by using AutoMapper:
            // Prepare the configuration for mapping:
            // The type on the left is the source type, and the type on the right is the destination type. 
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ImageForm_Builder, ImageForm>());

            // Based on the configuration perform a mapping:
            var mapper = new Mapper(config);

            // Create the Result Product - the Image Page:
            ImageForm result = mapper.Map<ImageForm>(this);

            // [Step 7] - If given, assign image to the picturebox:
            if (filename != null)
                result.AssignData(filename);

            // [Step 8]
            result.FinalInit();
            result.ReloadLanguage();
            result.ReloadColorSet();

            result.form.Show();

            return result;  // GetResult
        }

        private void CreateInstances() // [Step 1] --------------------------------------------------------------- ###
        {
            // ImagePageForm layout Elements: 
            form = new Form();
            containerMenu = new Panel();
            containerWorkspace = new SplitContainer();

            // Menu Container Elements:
            menuStrip = new MenuStrip();
            file_tsmi = new ToolStripMenuItem();
            histogram_tsmi = new ToolStripMenuItem();
            fileInfo_tsmi = new ToolStripMenuItem();

            // Extended Panels:
            imageLeftWingPanel = new ImageWorkspace();
            infoRightWingPanel = InfoWorkspace_Builder.GetResult();
        }

        private void Init_FormLayout() // [Step 2] --------------------------------------------------------------- ###
        {
            form.ShowIcon = false;
            form.MinimumSize = new Size(300, 300);

            // Init Menu Dock.Top Container:
            containerMenu.Dock = DockStyle.Top;
            containerMenu.Height = menuStrip.Height;

            // Init Workspace Dock.Fill Container:
            containerWorkspace.Dock = DockStyle.Fill;
            containerWorkspace.FixedPanel = FixedPanel.Panel2;
            containerWorkspace.Panel2Collapsed = true;
            containerWorkspace.BorderStyle = BorderStyle.FixedSingle;            
        }

        private void Init_FormMenu() // [Step 3] ----------------------------------------------------------------- ###
        {
            file_tsmi.Name = "file_tsmi";
            histogram_tsmi.Name = "histogram_tsmi";
            histogram_tsmi.Name = "fileInfo_tsmi";

            menuStrip.Items.AddRange(new ToolStripItem[]{
                file_tsmi,
                histogram_tsmi,
                fileInfo_tsmi
            });
        }






        private void Configure_ImageWorkspace() // [Step 4] -------------------------------------------------------- ###
        {
            // Image Panel
            imageLeftWingPanel.Dock = DockStyle.Fill;
            imageLeftWingPanel.imageScale_tb.Text = "100%";
            imageLeftWingPanel.imageScale_tb.Width = 40;

            imageLeftWingPanel.picture.BorderStyle = BorderStyle.FixedSingle;
            imageLeftWingPanel.picture.SizeMode = PictureBoxSizeMode.Zoom;
            imageLeftWingPanel.picture.Visible = false;

        }

        private void AssignParenthood() // [Step 5] ----------------------------------------------------------- ###
        {
            // Assigning FormItems to this MainForm:   
            //containerMenu.Controls.Add(imageScale_tb);

            form.Controls.Add(containerWorkspace);
            containerWorkspace.Panel1.Controls.Add(imageLeftWingPanel);

            form.Controls.Add(containerMenu);
            containerMenu.Controls.Add(menuStrip);

            // ________________________________________________________
            containerWorkspace.Panel2.Controls.Add(infoRightWingPanel);
        }
    }
}
