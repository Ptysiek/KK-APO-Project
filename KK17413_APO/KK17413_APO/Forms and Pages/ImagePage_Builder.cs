using System;
using System.Windows.Forms;
using System.Drawing;
using KK17413_APO.Toolbox_Tools_Expanded;
using AutoMapper;
using KK17413_APO.Panels_Expanded;

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
        public ToolStripMenuItem fileInfo_tsmi;

        public FlowLayoutPanel iwnContainer;   // Image Workspace Nodes Container
        public AdjustedSplitContainer histogram_iwn;
        public AdjustedSplitContainer fileInfo_iwn;
        // *iwn - Image Workspace Nodes

        public ImagePanel imagePanel;
        public HistogramPanel histogramPanel;
        public InfoPanel infoPanel;



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
            // ImagePage form layout: 
            form = new Form();
            containerMenu = new Panel();
            containerWorkspace = new SplitContainer();

            // Menu Container Elements:
            menuStrip = new MenuStrip();
            file_tsmi = new ToolStripMenuItem();
            histogram_tsmi = new ToolStripMenuItem();
            fileInfo_tsmi = new ToolStripMenuItem();

            //imageScale_tb = new TextBox();
            // Image Panel Items:
            //picture = new PictureBox();

            // Info Panel Items:
            iwnContainer = new FlowLayoutPanel();
            histogram_iwn = new AdjustedSplitContainer();
            fileInfo_iwn = new AdjustedSplitContainer();
            //infoLabelsContainer = new FlowLayoutPanel();

            imagePanel = new ImagePanel();
            histogramPanel = new HistogramPanel();
            infoPanel = new InfoPanel();
        }

        private void Init_FormLayout() // [Step 2] --------------------------------------------------------------- ###
        {
            // Calculate the TaskBar Height:
            int boundsH = Screen.PrimaryScreen.Bounds.Height;
            int workingAreaH = Screen.PrimaryScreen.WorkingArea.Height;
            int TaskBarH = boundsH - workingAreaH;

            form.ShowIcon = false;
            form.MinimumSize = new Size(300, 300);

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
            histogram_tsmi.Name = "fileInfo_tsmi";

            menuStrip.Items.AddRange(new ToolStripItem[]{
                file_tsmi,
                histogram_tsmi,
                fileInfo_tsmi
            });
        }

        private void Init_WorkspaceItems() // [Step 4] -------------------------------------------------------- ###
        {
            /*
            picture.BorderStyle = BorderStyle.FixedSingle;
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            picture.Visible = false;
            */

            iwnContainer.Dock = DockStyle.Fill;
            iwnContainer.BorderStyle = BorderStyle.FixedSingle;
            iwnContainer.FlowDirection = FlowDirection.TopDown;
            iwnContainer.WrapContents = false;
            iwnContainer.AutoScroll = true;




            imagePanel.Dock = DockStyle.Fill;
            imagePanel.imageScale_tb.Text = "100%";
            imagePanel.imageScale_tb.Width = 40;

            imagePanel.picture.BorderStyle = BorderStyle.FixedSingle;
            imagePanel.picture.SizeMode = PictureBoxSizeMode.Zoom;
            imagePanel.picture.Visible = false;


            histogramPanel.tabControl.Height = histogramPanel.PageHeight + histogramPanel.tabControl.ButtonContainerHeight;

            histogram_iwn.PanelHeight = histogramPanel.Height;
            histogramPanel.tabControl.Dock = DockStyle.Fill;
            histogramPanel.Dock = DockStyle.Fill;
            histogramPanel.Visible = false;

            infoPanel.Height = infoPanel.labelsHeight * (2 + infoPanel.infoLabels.Count);
            fileInfo_iwn.PanelHeight = infoPanel.Height;
            infoPanel.Dock = DockStyle.Fill;
            infoPanel.Visible = false;

            infoPanel.infoLabelsContainer.Dock = DockStyle.Fill;
            infoPanel.infoLabelsContainer.FlowDirection = FlowDirection.TopDown;
            infoPanel.infoLabelsContainer.WrapContents = false;
            infoPanel.infoLabelsContainer.AutoScroll = true;

        }

        private void AssignParenthood() // [Step 5] ----------------------------------------------------------- ###
        {
            // Assigning FormItems to this MainForm:   
            form.Controls.Add(containerMenu);
            containerMenu.Controls.Add(menuStrip);
            //containerMenu.Controls.Add(imageScale_tb);

            form.Controls.Add(containerWorkspace);
            containerWorkspace.Panel1.Controls.Add(imagePanel);
            containerWorkspace.Panel2.Controls.Add(iwnContainer);

            iwnContainer.Controls.Add(histogram_iwn);
            iwnContainer.Controls.Add(fileInfo_iwn);

            histogram_iwn.Panel2.Controls.Add(histogramPanel);
            fileInfo_iwn.Panel2.Controls.Add(infoPanel);
        }
    }
}
