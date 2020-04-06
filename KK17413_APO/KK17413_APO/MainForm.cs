using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;


namespace KK17413_APO
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class MainForm : Form
    {
        // #################################################################################################
        // MAIN FORM - MAIN MENU STRIP:
        private MenuStrip menuStrip;
        private ToolStripMenuItem file_tsmi;
        private ToolStripMenuItem open_tsmi;
        private ToolStripMenuItem project_tsmi;
        private ToolStripMenuItem settings_tsmi;
        private ToolStripMenuItem language_tsmi;
        // *tsmi - Tool Strip Menu Item

        // ---------------------------------------------------------------------------------------------
        // MAIN FORM - BOOKMARKS:
        private ListBox containerBOX;       // Holds all Bookmark elements
        private ListBox container;          // Holds generated Buttons
        private HScrollBar scrollbar;       // container movement => Buttons movement
        private int AbstractWidth = 0;

        // ---------------------------------------------------------------------------------------------
        private List<ImagePage> imagePages = new List<ImagePage>();

        Form newform;
        Panel panel;
        // #################################################################################################
        public MainForm()
        {
            ProgramSettings.language.SetLanguage("ANG");
            //ProgramSettings.language.SetLanguage("PL");
            
            ProgramSettings.ColorManager.SetColorSet("VisualS");


            //newform = new Form();
            //newform.FormBorderStyle = FormBorderStyle.None;

            //newform.Show();
            

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
            menuStrip.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            this.ForeColor = Color.Red;

            this.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            menuStrip.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            containerBOX.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");
            container.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");


            containerBOX.BorderStyle = BorderStyle.None;
            scrollbar.Visible = false;


            open_tsmi.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            open_tsmi.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");

            language_tsmi.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            language_tsmi.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
        }
        private void ResizeItems()
        {
            containerBOX.Width = this.Width - 16;
            scrollbar.Width = this.Width - 16;

            // Scrollbar additional resizements:
            if (AbstractWidth + 20 > this.Width)
            {
                scrollbar.Enabled = true;
                scrollbar.Maximum = AbstractWidth + 30 - this.Width;
            }
            else
            {
                scrollbar.Enabled = false;
            }
            ScrollbarLogic();
        }
        public void ScrollbarLogic()
        {
            container.Location = new Point(-scrollbar.Value, 0);
        }

        private void CreateWorkspace(string filename = null)
        {
            imagePages.Add(new ImagePage_Builder().GetResult(filename));
        }
    }
}
