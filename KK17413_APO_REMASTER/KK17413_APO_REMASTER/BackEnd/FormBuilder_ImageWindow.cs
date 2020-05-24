using System;
using System.Windows.Forms;
using System.Drawing;
using AutoMapper;
using System.Collections.Generic;
using KK17413_APO_REMASTER.FrontEnd.Forms_and_Popups;
using KK17413_APO_REMASTER.FrontEnd.Views_and_Expanded_Panels;


namespace KK17413_APO_REMASTER.BackEnd.Factories
{
    class FormBuilder_ImageWindow
    {
        ~FormBuilder_ImageWindow()
        => Clear();

        public ImageWindow result;

        public void PrepareNewForm()
        {            
            CreateInstances();
            Init_FormLayout();
            Init_FormMenu();
            Configure_Parenthood();

            if (filename != null)
                result.AssignData(filename);

            


        }
        public ImageWindow GetResult()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ImageWindow, ImageWindow>());
            var mapper = new Mapper(config);
            return mapper.Map<ImageWindow>(result);
        }
        public void Clear()
        {
            /*
            if (result != null) result.taskbar = null;

            result = null;
            */
        }




        public void Init_Operations_tsmis(List<string> LanguageKeys)
        {
            result.Operations_tsmis = new List<ToolStripMenuItem>();
            foreach (var key in LanguageKeys)
            {
                ToolStripMenuItem tmp_tsmi = new ToolStripMenuItem()
                {
                    Name = key,
                    Text = key
                };
                result.Menu_tsmis.Find(x => x.Name == "operations_tsmi").DropDownItems.Add(tmp_tsmi);
                result.Operations_tsmis.Add(tmp_tsmi);
            }
        }
        public void SetTransparencyKey(Color Transparent)
        {
            result.form.TransparencyKey = Transparent;
        }
        public void SetEventHandlers()
        {
            result.form.Resize += result.form_Resize;
            result.form.FormClosed += result.form_AfterFormClosed;

            result.containerWorkspace.SplitterMoved += result.workspace_SplitterMoved;

            result.Menu_tsmis.Find(x => x.Name == "histogram_tsmi").Click += result.histogram_tsmi_Click;
            result.Menu_tsmis.Find(x => x.Name == "fileInfo_tsmi").Click += result.fileInfo_tsmi_Click;

            // TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP /  
            result.Operations_tsmis.Find(x => x.Name == "histogram_Stretching_tsmi").Click += result.histogram_Stretching_tsmi_Click;
            result.Operations_tsmis.Find(x => x.Name == "histogram_Equalization_tsmi").Click += result.histogram_Equalization_tsmi_Click;
            result.Operations_tsmis.Find(x => x.Name == "negation_tsmi").Click += result.negation_tsmi_Click;
            // TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP / TMP /  

            result.infoRightWingPanel.histogramTabControl.VisibleChanged += result.histogramPanel_VisibleChanged;
        }







        private void CreateInstances() // [Step 1] --------------------------------------------------------------- ###
        {
            ImageWindow result = new ImageWindow
            {
                // ImageForm layout Elements: 
                form = new Form(),
                MenuContainer = new Panel(),
                containerWorkspace = new SplitContainer(),
                progressBar = new ProgressBar(),

                // Extended Panels:
                imageLeftWingPanel = ImageView_Builder.GetResult(),
                infoRightWingPanel = InfoView_Builder.GetResult()

                //modifications = new List<ImageData>()
            };
        }

        private void Init_FormLayout()
        {
            result.form.ShowIcon = false;
            result.form.MinimumSize = new Size(300, 300);

            result.MenuContainer.Dock = DockStyle.Top;
            result.MenuContainer.Height = result.menuStrip.Height;

            result.containerWorkspace.Dock = DockStyle.Fill;
            result.containerWorkspace.FixedPanel = FixedPanel.Panel2;
            result.containerWorkspace.Panel2Collapsed = true;
            result.containerWorkspace.BorderStyle = BorderStyle.FixedSingle;
        }

        private void Init_FormMenu()
        {
            result.progressBar.Dock = DockStyle.None;
            result.progressBar.Visible = false;
            result.progressBar.Maximum = 100;

            result.menuStrip.Dock = DockStyle.None;

            ToolStripMenuItem file_tsmi = new ToolStripMenuItem() { Name = "file_tsmi" };
            ToolStripMenuItem operations_tsmi = new ToolStripMenuItem() { Name = "operations_tsmi" };
            ToolStripMenuItem histogram_tsmi = new ToolStripMenuItem() { Name = "histogram_tsmi" };
            ToolStripMenuItem fileInfo_tsmi = new ToolStripMenuItem() { Name = "fileInfo_tsmi" };

            result.menuStrip.Items.AddRange(new ToolStripItem[]{
                file_tsmi,
                operations_tsmi,
                histogram_tsmi,
                fileInfo_tsmi
            });

            operations_tsmi.DropDownItems.AddRange(new ToolStripItem[]{
                histogram_Stretching_tsmi,
                histogram_Equalization_tsmi,
                negation_tsmi
            });

            result.Menu_tsmis = new List<ToolStripMenuItem>() {
                file_tsmi,
                operations_tsmi,
                histogram_tsmi,
                fileInfo_tsmi
            };
        }

        private void Configure_Parenthood()
        {
            // Assigning FormItems to this MainForm:  
            result.form.Controls.Add(result.containerWorkspace);
            result.containerWorkspace.Panel1.Controls.Add(result.imageLeftWingPanel);
            result.containerWorkspace.Panel2.Controls.Add(result.infoRightWingPanel);

            result.form.Controls.Add(result.MenuContainer);
            result.MenuContainer.Controls.Add(result.menuStrip);
            result.MenuContainer.Controls.Add(result.progressBar);
        }


    }
}
