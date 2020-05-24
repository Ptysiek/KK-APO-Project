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
        ~ FormBuilder_MainWindow()
        => Clear();        

        public MainWindow result;

        public void PrepareNewForm()
        {
            result = new MainWindow();
            result.Form = new AdjustedForm();
            result.menuStrip = Get_menuStrip();
            result.taskbar = Get_taskbar();

            result.pageHandlersContainer = Get_pageHandlersContainer();
            result.dragNdropContainer = Get_dragNdropContainer();
            result.dragNdropText1 = Get_dragNdropLabel("Drop your image here", 26);
            result.dragNdropText2 = Get_dragNdropLabel("[ bmp, jpg, png, tiff ]", 13);

            // -----------------------------------------------------------------------------      
            Configure_Parenthood();
            result.AssignEventHandlers();
            result.ResizeItems();
        }
        public MainWindow GetResult()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<MainWindow, MainWindow>());
            var mapper = new Mapper(config);
            return mapper.Map<MainWindow>(result);
        }
        public void Clear()
        {
            if (result != null) result.taskbar = null;
            if (result != null) result.dragNdropContainer = null;
            if (result != null) result.dragNdropText1 = null;
            if (result != null) result.dragNdropText2 = null;

            if (result != null) result.pageHandlersContainer = null;
            if (result != null) result.menuStrip = null;

            result = null;
        }
        
        // ########################################################################################################
        public void Init_Language_tsmis(List<string> LanguageKeys)
        {
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

            foreach (var obj in result.Language_tsmis)
                obj.Click += result.Language_tsmis_Click;
        }
        public void Init_ColorSet_tsmis(List<string> ColorSetKeys)
        {
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

            foreach (var obj in result.Color_tsmis)
                obj.Click += result.Color_tsmis_Click;
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

        // ########################################################################################################
        private Taskbar Get_taskbar()
        {
            Taskbar taskbar = Taskbar_Builder.GetResult(result.Form);
            taskbar.IconAssignImage("KK17413_APO_REMASTER.Resources.Icon.png");
            taskbar.Dock = DockStyle.Top;
            taskbar.Text = "DistortImage";
            return taskbar;
        }
        private MenuStrip Get_menuStrip()
        {
            MenuStrip menuStrip = new MenuStrip(){
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
            menuStrip.Items.AddRange(new ToolStripItem[]{
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
            //result.Menu_tsmis.AddRange(new ToolStripMenuItem[]{
            result.Menu_tsmis = new List<ToolStripMenuItem>() { 
                file_tsmi,
                open_tsmi,
                project_tsmi,
                settings_tsmi,
                language_tsmi,
                colorTheme_tsmi
            };

            // ----------------------------------------------------------------------
            result.Language_tsmis = new List<ToolStripMenuItem>();
            result.Color_tsmis = new List<ToolStripMenuItem>();

            // ----------------------------------------------------------------------
            return menuStrip;
        }
        private FlowLayoutPanel Get_pageHandlersContainer()
        {
            return new FlowLayoutPanel()
            {
                Dock = DockStyle.Top,
                BorderStyle = BorderStyle.None,
                Height = result.menuStrip.Height + 22,
                AutoScroll = true,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight
            };
        }        
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




