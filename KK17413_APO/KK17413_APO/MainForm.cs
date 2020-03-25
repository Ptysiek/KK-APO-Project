﻿using System;
using System.Windows.Forms;
using System.Drawing;


namespace KK17413_APO
{
    public partial class MainForm : Form
    {
        // ---------------------------------------------------------------------------------------------
        private ProgramSettings programSettings;
        private ProgramLanguage languageDictionary;     // Set which language is currently used.
                                                        // Get wordFields_Dictionary of currently chosen language.

        // ---------------------------------------------------------------------------------------------
        // MAIN FORM - MAIN MENU STRIP:
        private MenuStrip menuStrip;
        private ToolStripMenuItem file_tsmi;
        private ToolStripMenuItem open_tsmi;
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
        private bool initialized = false;   // INIT FLAG



        public MainForm()
        {
            InitializeComponent();
            Constructor_MainInit();
            ReloadLanguage();
        }

        private void ReloadLanguage()
        {
            file_tsmi.Text = languageDictionary.GetValue("file_tsmi");
            open_tsmi.Text = languageDictionary.GetValue("open_tsmi");
            settings_tsmi.Text = languageDictionary.GetValue("settings_tsmi");
            language_tsmi.Text = languageDictionary.GetValue("language_tsmi");
        }
        private void Resize2()
        {
            if (!initialized) return;

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
            ScrollbarLogics();
        }
        public void ScrollbarLogics()
        {
            container.Location = new Point(-scrollbar.Value, 0);
        }

        public void open_tsmi_Click(object sender, EventArgs e)
        {
            /*
            //OpenFileDialog FD = MainFormOperations.BrowseFile();
            OpenFileDialog FD = MainFormOperations.BrowseFile();

            if (FD != null)
            {
                bookMarks.AddPage(FD.FileName);
            }
            //*/
        }
        public void scrollbar_Scroll(object sender, EventArgs e)
        {
            ScrollbarLogics();
        }        
        
        public void mainForm_Resize(object sender, EventArgs e)
        {
            Resize2();
        }
    }
}
