using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using AutoMapper;


using KK17413_APO_REMASTER.FrontEnd.Views_and_Expanded_Panels;
using KK17413_APO_REMASTER.BackEnd.DataStructures;
using KK17413_APO_REMASTER.BackEnd.Factories;
using KK17413_APO_REMASTER.FrontEnd.WindowForms;

namespace KK17413_APO_REMASTER.BackEnd.WindowBuilders
{
    class FormBuilder_ImageWindow
    {

        public ImageForm result;

        public void PrepareNewForm()
        {            
            CreateInstances();
            Init_FormLayout();
            Init_FormMenu();
            Configure_Parenthood();           


        }
        public ImageForm GetResult()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ImageForm, ImageForm>());
            var mapper = new Mapper(config);
            return mapper.Map<ImageForm>(result);
        }






        public void Init_Operations_tsmis(ImageOperations_Factory operations)
        {
            result.Operations_tsmis = new List<ToolStripMenuItem>();
            result.OperationsFamily_tsmis = new List<ToolStripMenuItem>();
            ToolStripMenuItem tmp_operations_tsmi = result.Menu_tsmis.Find(x => x.Name == "operations_tsmi");

            foreach (string key in operations.FamilyKeys())
            {
                ToolStripMenuItem new_tsmi = new ToolStripMenuItem()
                {
                    Name = key,
                    Text = key
                };

                if (operations.GetFamily(key).OperationsKeys() != null)
                {
                    foreach (var operationKey in operations.GetFamily(key).OperationsKeys())
                    {
                        ToolStripMenuItem new_Operation_tsmi = new ToolStripMenuItem()
                        {
                            Name = operationKey,
                            Text = operationKey
                        };

                        new_tsmi.DropDownItems.Add(new_Operation_tsmi);
                        result.Operations_tsmis.Add(new_Operation_tsmi);
                    }
                }

                tmp_operations_tsmi.DropDownItems.Add(new_tsmi);
                result.OperationsFamily_tsmis.Add(new_tsmi);
            }
        }
        public void SetTransparencyKey(Color Transparent)
        {
            result.form.TransparencyKey = Transparent;
        }
        public void SetProgramReference(ImageForm_Service service)
        {
            result.SERVICE = service;
        }
        public void SetEventHandlers()
        {
            result.form.Resize += result.Form_Resize;
            result.form.FormClosed += result.Form_AfterFormClosed;

            result.containerWorkspace.SplitterMoved += result.Workspace_SplitterMoved;

            result.Menu_tsmis.Find(x => x.Name == "histogram_tsmi").Click += result.Histogram_tsmi_Click;
            result.Menu_tsmis.Find(x => x.Name == "fileInfo_tsmi").Click += result.FileInfo_tsmi_Click;

            foreach (var tsmi in result.Operations_tsmis)
            {
                tsmi.Click += result.ImageOperation_Tsmi_Click;
            }

            result.infoRightWingPanel.panel_HistogramTabControl.VisibleChanged += result.HistogramPanel_VisibleChanged;
        }
        public void SetData(ImageData data)
        {
            if (data == null) return;

            result.form.Text = data.ID;
            //Bitmap bitmap = new Bitmap(filename);

            result.imageLeftWingPanel.AssignImage(data.Bitmap);
            result.ResizeFormToPicture();
            result.imageLeftWingPanel.RelocatePicture();

            result.infoRightWingPanel.LoadInfoPanel(data.Bitmap, data.ID);


            //result.infoRightWingPanel.histogramTabControl.AssignBitmap(data.Bitmap, data.ID);

            //result.HistogramCalculatePermision = true;
            //modifications.Add(new ImageData(bitmap, filename));
        }



        private void CreateInstances() // [Step 1] --------------------------------------------------------------- ###
        {
            result = new ImageForm
            {
                // ImageForm layout Elements: 
                form = new Form(),
                MenuContainer = new Panel(),
                containerWorkspace = new SplitContainer(),
                progressBar = new ProgressBar(),
                menuStrip = new MenuStrip(),

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

            /*
            operations_tsmi.DropDownItems.AddRange(new ToolStripItem[]{
                histogram_Stretching_tsmi,
                histogram_Equalization_tsmi,
                negation_tsmi
            });
            */
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
