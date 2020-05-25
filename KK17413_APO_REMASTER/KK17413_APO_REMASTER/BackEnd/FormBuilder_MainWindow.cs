using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using AutoMapper;
using KK17413_APO_REMASTER.FrontEnd.Forms_and_Popups;
using KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded;


namespace KK17413_APO_REMASTER.BackEnd.Factories
{
    public class FormBuilder_MainWindow
    {
        public MainForm result;

        public void PrepareNewForm()
        {
            result = new MainForm
            {
                Form = new AdjustedForm(),
                dragNdropContainer = Get_dragNdropContainer(),
                dragNdropText1 = Get_dragNdropLabel("Drop your image here", 26),
                dragNdropText2 = Get_dragNdropLabel("[ bmp, jpg, png, tiff ]", 13)
            };

            Configure_taskbar();
            Configure_menuStrip();
            Configure_pageHandlersContainer();
            Configure_Parenthood();
            result.ResizeItems();
        }
        public MainForm GetResult()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<MainForm, MainForm>());
            var mapper = new Mapper(config);
            return mapper.Map<MainForm>(result);
        }

        // ########################################################################################################
        public void Init_Language_tsmis(List<string> LanguageKeys)
        {
            result.Language_tsmis = new List<ToolStripMenuItem>();
            foreach (var key in LanguageKeys)
            {
                ToolStripMenuItem tmp_tsmi = new ToolStripMenuItem()
                {
                    Name = key,
                    Text = key
                };
                result.Menu_tsmis.Find(x => x.Name == "language_tsmi").DropDownItems.Add(tmp_tsmi);
                result.Language_tsmis.Add(tmp_tsmi);
            }
        }
        public void Init_ColorSet_tsmis(List<string> ColorSetKeys)
        {
            result.Color_tsmis = new List<ToolStripMenuItem>();
            foreach (var key in ColorSetKeys)
            {
                ToolStripMenuItem tmp_tsmi = new ToolStripMenuItem()
                {
                    Name = key,
                    Text = key
                };
                result.Menu_tsmis.Find(x => x.Name == "colorTheme_tsmi").DropDownItems.Add(tmp_tsmi);
                result.Color_tsmis.Add(tmp_tsmi);
            }
        }
        public void SetTransparencyKey(Color Transparent)
        {
            result.Form.BackColor = Transparent;
            result.Form.TransparencyKey = Transparent;
        }
        public void SetProgramReference(Program program)
        {
            result.PROGRAM = program;
        }
        public void SetEventHandlers()
        {
            // Assigning EventHandlers:
            result.Form.Resize += result.MainForm_Resize;

            result.dragNdropContainer.DragDrop += result.DragNdropContainer_DragDrop;
            result.dragNdropContainer.DragEnter += result.DragNdropContainer_DragEnter;

            result.Menu_tsmis.Find(x => x.Name == "open_tsmi").Click += result.Open_tsmi_Click;
            result.Menu_tsmis.Find(x => x.Name == "project_tsmi").Click += result.Project_tsmi_Click;

            result.pageHandlersContainer.MouseMove += result.MouseFix_MouseMove;
            result.menuStrip.MouseMove += result.MouseFix_MouseMove;
            result.dragNdropContainer.MouseMove += result.MouseFix_MouseMove;

            foreach (var obj in result.Language_tsmis)
                obj.Click += result.Language_tsmis_Click;

            foreach (var obj in result.Color_tsmis)
                obj.Click += result.Color_tsmis_Click;
        }

        // ########################################################################################################
       
        
        private Panel Get_dragNdropContainer()
        {
            return new Panel()
            {
                Dock = DockStyle.Fill,
                AllowDrop = true
            };
        }
        
        private Label Get_dragNdropLabel(string value, int fontsize)
        {
            Label dragNdropText = new Label()
            {
                Text = value,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
            };
            dragNdropText.Font = new Font(dragNdropText.Font.Name, fontsize, dragNdropText.Font.Style);
            return dragNdropText;
        }

        private void Configure_taskbar()
        {
            result.taskbar = Taskbar_Builder.GetResult(result.Form);
            result.taskbar.IconAssignImage("KK17413_APO_REMASTER.Resources.Icon.png");
            result.taskbar.Dock = DockStyle.Top;
            result.taskbar.Text = "DistortImage";
        }

        private void Configure_menuStrip()
        {
            result.menuStrip = new MenuStrip()
            {
                Dock = DockStyle.Top, //Fill,
                Stretch = true
            };

            // ----------------------------------------------------------------------
            ToolStripMenuItem file_tsmi = new ToolStripMenuItem() { Name = "file_tsmi" };
            ToolStripMenuItem open_tsmi = new ToolStripMenuItem() { Name = "open_tsmi" };
            ToolStripMenuItem project_tsmi = new ToolStripMenuItem() { Name = "project_tsmi" };
            ToolStripMenuItem settings_tsmi = new ToolStripMenuItem() { Name = "settings_tsmi" };
            ToolStripMenuItem language_tsmi = new ToolStripMenuItem() { Name = "language_tsmi" };
            ToolStripMenuItem colorTheme_tsmi = new ToolStripMenuItem() { Name = "colorTheme_tsmi" };

            // ----------------------------------------------------------------------
            result.menuStrip.Items.AddRange(new ToolStripItem[]{
                file_tsmi,
                project_tsmi,
                settings_tsmi
            });
            settings_tsmi.DropDownItems.AddRange(new ToolStripItem[]{
                language_tsmi,
                colorTheme_tsmi
            });
            file_tsmi.DropDownItems.Add(open_tsmi);

            // ----------------------------------------------------------------------
            result.Menu_tsmis = new List<ToolStripMenuItem>() {
                file_tsmi,
                open_tsmi,
                project_tsmi,
                settings_tsmi,
                language_tsmi,
                colorTheme_tsmi
            };
        }

        private void Configure_pageHandlersContainer()
        {
            result.pageHandlersContainer = new FlowLayoutPanel()
            {
                Dock = DockStyle.Top,
                BorderStyle = BorderStyle.None,
                Height = result.menuStrip.Height + 22,
                AutoScroll = true,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight
            };
        }

        private void Configure_Parenthood()
        {
            result.Form.ControlsAdd(result.pageHandlersContainer);
            result.Form.ControlsAdd(result.menuStrip);

            result.Form.ControlsAdd(result.dragNdropContainer);
            result.dragNdropContainer.Controls.Add(result.dragNdropText1);
            result.dragNdropContainer.Controls.Add(result.dragNdropText2);

            result.Form.ControlsAdd(result.taskbar);
        }
    }
}

