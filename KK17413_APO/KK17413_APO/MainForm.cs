using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using KK17413_APO.Toolbox_Tools_Expanded;


namespace KK17413_APO
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class MainForm : SurplusForm
    {
        // #################################################################################################
        private FlowLayoutPanel bookmarkContainer;  // Holds all Bookmark elements
        private Panel dragNdropContainer;
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

            //SurplusForm tak = new SurplusForm();

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
            // This form:
            this.Taskbar.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");
            this.Taskbar.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            this.Taskbar.IconChangeColor(ProgramSettings.ColorManager.GetValue("detailColor3"));

            // MenuStrip:
            menuStrip.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            menuStrip.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");

            bookmarkContainer.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");


            open_tsmi.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            open_tsmi.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");

            language_tsmi.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            language_tsmi.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
        }
        private void ResizeItems()
        {

        }
        public void ScrollbarLogic()
        {
            //container.Location = new Point(-scrollbar.Value, 0);
        }

        private void CreateWorkspace(string filename = null)
        {
            imagePages.Add(new ImagePage_Builder().GetResult(filename));
        }
    }
}
