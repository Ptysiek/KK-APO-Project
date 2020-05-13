﻿using System;
using System.Windows.Forms;
using System.Drawing;
using KK17413_APO.Toolbox_Tools_Expanded;
using AutoMapper;
using KK17413_APO.Panels_Expanded;
using System.Collections.Generic;
using KK17413_APO.Data_Structures;

namespace KK17413_APO.Forms_and_Pages
{
    class ImageForm_Builder
    {
        public Form form;
        public Panel MenuContainer;
        public SplitContainer containerWorkspace;

        public ProgressBar progressBar;

        public MenuStrip menuStrip;
        public ToolStripMenuItem file_tsmi;
        public ToolStripMenuItem histogram_tsmi;
        public ToolStripMenuItem fileInfo_tsmi;

        // Operations_tsmi:
        public ToolStripMenuItem operations_tsmi;
        public ToolStripMenuItem histogram_Stretching_tsmi;

        public ImageWorkspace imageLeftWingPanel;
        public InfoWorkspace infoRightWingPanel;

        public List<ImageData> modifications;


        public ImageForm GetResult(string filename)
        {
            // [Step 1]
            CreateInstances();

            // [Step 2]
            Init_FormLayout();

            // [Step 3]
            Init_FormMenu();

            // [Step 4]
            AssignParenthood();

            // [Step 5] - Create the result product by using AutoMapper:
            // Prepare the configuration for mapping:
            // The type on the left is the source type, and the type on the right is the destination type. 
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ImageForm_Builder, ImageForm>());

            // Based on the configuration perform a mapping:
            var mapper = new Mapper(config);

            // Create the Result Product - the ImageForm:
            ImageForm result = mapper.Map<ImageForm>(this);

            // [Step 6] - If given, assign image to the picturebox:
            if (filename != null)
                result.AssignData(filename);

            // [Step 7]
            result.AssignEventHandlers();            
            result.ReloadLanguage();
            result.ReloadColorSet();
            result.form.Show();

            return result;  // GetResult
        }

        private void CreateInstances() // [Step 1] --------------------------------------------------------------- ###
        {
            // ImageForm layout Elements: 
            form = new Form();
            MenuContainer = new Panel();
            containerWorkspace = new SplitContainer();

            // Menu Container Elements:
            progressBar = new ProgressBar();

            menuStrip = new MenuStrip();
            file_tsmi = new ToolStripMenuItem();
            histogram_tsmi = new ToolStripMenuItem();
            fileInfo_tsmi = new ToolStripMenuItem();

            operations_tsmi = new ToolStripMenuItem();
            histogram_Stretching_tsmi = new ToolStripMenuItem();

            // Extended Panels:
            imageLeftWingPanel = ImageWorkspace_Builder.GetResult();
            infoRightWingPanel = InfoWorkspace_Builder.GetResult();

            modifications = new List<ImageData>();
        }

        private void Init_FormLayout() // [Step 2] --------------------------------------------------------------- ###
        {
            form.ShowIcon = false;
            form.MinimumSize = new Size(300, 300);
            form.TransparencyKey = ProgramSettings.ColorManager.Transparent;

            MenuContainer.Dock = DockStyle.Top;
            MenuContainer.Height = menuStrip.Height;

            containerWorkspace.Dock = DockStyle.Fill;
            containerWorkspace.FixedPanel = FixedPanel.Panel2;
            containerWorkspace.Panel2Collapsed = true;
            containerWorkspace.BorderStyle = BorderStyle.FixedSingle;            
        }

        private void Init_FormMenu() // [Step 3] ----------------------------------------------------------------- ###
        {
            progressBar.Dock = DockStyle.None;            
            progressBar.Value = 50;
            progressBar.Width = MenuContainer.Width - menuStrip.Width - 8;
            progressBar.Height = MenuContainer.Height / 2;
            progressBar.Left = menuStrip.Width;
            progressBar.Top = MenuContainer.Height / 4;

            file_tsmi.Name = "file_tsmi";
            histogram_tsmi.Name = "histogram_tsmi";
            fileInfo_tsmi.Name = "fileInfo_tsmi";

            operations_tsmi.Name = "operations_tsmi";
            histogram_Stretching_tsmi.Name = "histogram_Stretching_tsmi";

            menuStrip.Dock = DockStyle.None;

            menuStrip.Items.AddRange(new ToolStripItem[]{
                file_tsmi,
                operations_tsmi,
                histogram_tsmi,
                fileInfo_tsmi
            });

            operations_tsmi.DropDownItems.AddRange(new ToolStripItem[]{
                histogram_Stretching_tsmi
            });
        }

        private void AssignParenthood() // [Step 4] ----------------------------------------------------------- ###
        {
            // Assigning FormItems to this MainForm:  
            form.Controls.Add(containerWorkspace);
            containerWorkspace.Panel1.Controls.Add(imageLeftWingPanel);
            containerWorkspace.Panel2.Controls.Add(infoRightWingPanel);

            form.Controls.Add(MenuContainer);
            MenuContainer.Controls.Add(menuStrip);
            MenuContainer.Controls.Add(progressBar);
        }
    }
}
