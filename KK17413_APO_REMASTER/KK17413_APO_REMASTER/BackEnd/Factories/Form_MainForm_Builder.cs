using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using KK17413_APO_REMASTER.FrontEnd.Forms_and_Popups;
using KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded;


namespace KK17413_APO_REMASTER.BackEnd.Factories
{
    // ##########################################################################################################################
    // ##########################################################################################################################
    #region MainForm_Builder
    public class Form_MainForm_Builder
    {
        public static MainForm GetResult()
        {
            // ------------------------------------------------------------------
            MainForm result = new MainForm();

            // ------------------------------------------------------------------
            result.taskbar = Get_taskbar(result);
            result.menuStrip = Get_menuStrip(ref result);
            result.pageHandlersContainer = Get_pageHandlersContainer(result.menuStrip.Height);
            result.dragNdropContainer = Get_dragNdropContainer();
            result.dragNdropText1 = Get_dragNdropLabel("Drop your image here", 26);
            result.dragNdropText2 = Get_dragNdropLabel("[ bmp, jpg, png, tiff ]", 13);

            // -----------------------------------------------------------------------------        
            Configure_Parenthood(ref result);
            result.AssignEventHandlers();
            result.ResizeItems();
            //result.ReloadLanguage();
            //result.ReloadColorSet();

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
                Dock = DockStyle.Top, //Fill,
                Stretch = true
            };
            ToolStripMenuItem file_tsmi = new ToolStripMenuItem();
            ToolStripMenuItem open_tsmi = new ToolStripMenuItem();
            ToolStripMenuItem project_tsmi = new ToolStripMenuItem();
            ToolStripMenuItem settings_tsmi = new ToolStripMenuItem();
            ToolStripMenuItem language_tsmi = new ToolStripMenuItem();
            ToolStripMenuItem colorTheme_tsmi = new ToolStripMenuItem();
            List<ToolStripMenuItem> Language_tsmis = new List<ToolStripMenuItem>();
            List<ToolStripMenuItem> Color_tsmis = new List<ToolStripMenuItem>();

            // Assign Items to menuStrip:
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

            foreach (var key in ProgramSettings.Language.Keys())
            {
                ToolStripMenuItem tmp_tsmi = new ToolStripMenuItem()
                {
                    Name = key,
                    Text = key
                };
                language_tsmi.DropDownItems.Add(tmp_tsmi);
                Language_tsmis.Add(tmp_tsmi);
            }

            foreach (var key in ProgramSettings.ColorManager.Keys())
            {
                ToolStripMenuItem tmp_tsmi = new ToolStripMenuItem()
                {
                    Name = key,
                    Text = key
                };
                colorTheme_tsmi.DropDownItems.Add(tmp_tsmi);
                Color_tsmis.Add(tmp_tsmi);
            }

            // Assign Items to MainForm result:
            result.file_tsmi = file_tsmi;
            result.open_tsmi = open_tsmi;
            result.project_tsmi = project_tsmi;
            result.settings_tsmi = settings_tsmi;
            result.language_tsmi = language_tsmi;
            result.colorTheme_tsmi = colorTheme_tsmi;
            result.Language_tsmis = Language_tsmis;
            result.Color_tsmis = Color_tsmis;
            return menuStrip;
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

            result.ControlsAdd(result.menuStrip);

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
