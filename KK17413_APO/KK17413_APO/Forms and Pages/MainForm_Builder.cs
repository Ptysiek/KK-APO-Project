using System.Windows.Forms;
using System.Drawing;
using KK17413_APO.Toolbox_Tools_Expanded;


namespace KK17413_APO.Forms_and_Pages
{
    // ##########################################################################################################################
    // ##########################################################################################################################
    #region MainForm_Builder
    public class MainForm_Builder
    {
        public static MainForm GetResult()
        {
            // ------------------------------------------------------------------
            MainForm result = new MainForm();
            result.Workspace.BackColor = Color.Black;

            // ------------------------------------------------------------------
            result.taskbar = Get_taskbar(result);
            result.menuStrip = Get_menuStrip(ref result);
            result.menuContainer = Get_menuContainer(result.menuStrip.Height);
            result.pageHandlersContainer = Get_pageHandlersContainer(result.menuStrip.Height);
            result.dragNdropContainer = Get_dragNdropContainer();
            result.dragNdropText1 = Get_dragNdropLabel("Drop your image here", 26);
            result.dragNdropText2 = Get_dragNdropLabel("[ bmp, jpg, png, tiff ]", 13);

            // -----------------------------------------------------------------------------        
            Configure_Parenthood(ref result);
            result.AssignEventHandlers();
            result.ResizeItems();
            result.ReloadLanguage();
            result.ReloadColorSet();

            // ------------------------------------------------------------------
            return result;
        }

        // ########################################################################################################
        private static Taskbar Get_taskbar(MainForm result)
        {
            Taskbar taskbar = Taskbar_Builder.GetResult(result);
            taskbar.IconAssignImage("KK17413_APO.Resources.Icon.png");
            taskbar.Dock = DockStyle.Top;
            taskbar.Text = "DistortImage";
            return taskbar;
        }
        private static MenuStrip Get_menuStrip(ref MainForm result)
        {
            MenuStrip menuStrip = new MenuStrip()
            {
                Dock = DockStyle.Fill
            };
            ToolStripMenuItem file_tsmi = new ToolStripMenuItem();
            ToolStripMenuItem open_tsmi = new ToolStripMenuItem();
            ToolStripMenuItem project_tsmi = new ToolStripMenuItem();
            ToolStripMenuItem settings_tsmi = new ToolStripMenuItem();
            ToolStripMenuItem language_tsmi = new ToolStripMenuItem();

            // Assign Items to menuStrip:
            menuStrip.Items.AddRange(new ToolStripItem[]{
                file_tsmi,
                project_tsmi,
                settings_tsmi
            });
            file_tsmi.DropDownItems.Add(open_tsmi);
            settings_tsmi.DropDownItems.Add(language_tsmi);

            // Assign Items to MainForm result:
            result.file_tsmi = file_tsmi;
            result.open_tsmi = open_tsmi;
            result.project_tsmi = project_tsmi;
            result.settings_tsmi = settings_tsmi;
            result.language_tsmi = language_tsmi;
            return menuStrip;
        }
        private static Panel Get_menuContainer(int menuStripHeight)
        {
            return new Panel()
            {
                Dock = DockStyle.Top,
                Height = menuStripHeight
            };
        }
        private static FlowLayoutPanel Get_pageHandlersContainer(int menuStripHeight)
        {
            return new FlowLayoutPanel()
            {
                Dock = DockStyle.Top,
                BorderStyle = BorderStyle.None,
                Height = menuStripHeight + 22,
                AutoScroll = true,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight
            };
        }
        private static Panel Get_dragNdropContainer()
        {
            return new Panel()
            {
                Dock = DockStyle.Fill,
                AllowDrop = true
            };
        }
        private static Label Get_dragNdropLabel(string value, int fontsize)
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
        private static void Configure_Parenthood(ref MainForm result)
        {
            result.ControlsAdd(result.pageHandlersContainer);

            result.ControlsAdd(result.menuContainer);
            result.menuContainer.Controls.Add(result.menuStrip);

            result.ControlsAdd(result.dragNdropContainer);
            result.dragNdropContainer.Controls.Add(result.dragNdropText1);
            result.dragNdropContainer.Controls.Add(result.dragNdropText2);

            result.ControlsAdd(result.taskbar);
        }
    }
    #endregion
    // ##########################################################################################################################
    // ##########################################################################################################################
}
