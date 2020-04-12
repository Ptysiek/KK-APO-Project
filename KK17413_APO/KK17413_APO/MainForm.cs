using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using KK17413_APO.Toolbox_Tools_Expanded;
using KK17413_APO.Forms_and_Pages;

namespace KK17413_APO
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class MainForm : SurplusForm
    {
        // #################################################################################################
        private Taskbar taskbar;
        private Panel dragNdropContainer;
        private Label dragNdropText1;
        private Label dragNdropText2;

        private FlowLayoutPanel bookmarkContainer;  // Holds all Bookmark elements
        private Panel menuContainer;
        private MenuStrip menuStrip;

        private ToolStripMenuItem file_tsmi;
        private ToolStripMenuItem open_tsmi;
        private ToolStripMenuItem project_tsmi;
        private ToolStripMenuItem settings_tsmi;
        private ToolStripMenuItem language_tsmi;
        // *tsmi - Tool Strip Menu Item

        // ---------------------------------------------------------------------------------------------
        private List<ImagePage> imagePages = new List<ImagePage>();


        // #################################################################################################
        public MainForm()
        {
            ProgramSettings.language.SetLanguage("ANG");
            //ProgramSettings.language.SetLanguage("PL");
            
            ProgramSettings.ColorManager.SetColorSet("VisualS");


            InitializeComponent();
            Constructor_MainInit();
        }


        // #################################################################################################
        private void ReloadLanguage()
        {
            file_tsmi.Text = ProgramSettings.language.GetValue("file_tsmi");
            open_tsmi.Text = ProgramSettings.language.GetValue("open_tsmi");
            project_tsmi.Text = ProgramSettings.language.GetValue("project_tsmi");
            settings_tsmi.Text = ProgramSettings.language.GetValue("settings_tsmi");
            language_tsmi.Text = ProgramSettings.language.GetValue("language_tsmi");
        }        
        private void ReloadColorSet()
        {
            // This Form Layout:
            taskbar.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            taskbar.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");
            taskbar.IconChangeColor(ProgramSettings.ColorManager.GetValue("detailColor2"));

            dragNdropText1.ForeColor = ProgramSettings.ColorManager.GetValue("detailColor2");
            dragNdropText2.ForeColor = ProgramSettings.ColorManager.GetValue("detailColor2");
            bookmarkContainer.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");
            dragNdropContainer.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer3");

            // MenuStrip:
            menuStrip.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            menuStrip.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");

            open_tsmi.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            open_tsmi.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            language_tsmi.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            language_tsmi.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
        }
        private void ResizeItems()
        {
            dragNdropText1.Top = (dragNdropContainer.Height / 2) - (dragNdropText1.Height / 2);
            dragNdropText2.Top = dragNdropText1.Top + dragNdropText1.Height;

            dragNdropText1.Left = (dragNdropContainer.Width / 2) - dragNdropText1.Width / 2;
            dragNdropText2.Left = (dragNdropContainer.Width / 2) - dragNdropText2.Width / 2;
        }

        private void CreateWorkspace(string filename = null)
        {
            imagePages.Add(new ImagePage_Builder().GetResult(filename));
        }
    }
}
