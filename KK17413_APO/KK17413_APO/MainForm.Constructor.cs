using System;
using System.Windows.Forms;
using System.Drawing;           // Size


namespace KK17413_APO
{    
    [System.ComponentModel.DesignerCategory("")]
    partial class MainForm
    {        
         private void Constructor_MainInit()
        {
            // [Step 1]
            CreateInstances();

            // [Step 2]
            Init_MenuStrip();

            // [Step 3]
            Init_EventHandlers();

            // [Step 4]
            // Assigning FormComponents to this MainForm:
            this.Controls.Add(menuStrip);
            this.MainMenuStrip = menuStrip;
        }


        private void CreateInstances() // [Step 1] ------------------------------------------------ ###
        {
            // MAIN FORM SETTINGS - PROGRAM SETTINGS:
            programSettings = new ProgramSettings();
            languageDictionary = new ProgramLanguage();

            // MAIN FORM - MAIN MENU STRIP:
            menuStrip = new MenuStrip();
            file_tsmi = new ToolStripMenuItem();
            open_tsmi = new ToolStripMenuItem();
            settings_tsmi = new ToolStripMenuItem();
            language_tsmi = new ToolStripMenuItem();

            // MAIN FORM - BOOKMARKS:
            containerBOX = new ListBox();
            container = new ListBox();
            h_scrollbar = new HScrollBar();
        }


        private void Init_MenuStrip() // [Step 2] ------------------------------------------------- ###
        {
            //menuStrip.ImageScalingSize   = new Size(20, 20);
            //menuStrip.Size               = new Size(800, 28);
            //menuStrip.Location           = new Point(0, 0);
            //menuStrip.TabIndex           = 0;
            //file_tsmi.Size = new Size(46, 24);
            //open_tsmi.Size = new Size(128, 26);

            menuStrip.Name = "menuStrip";
            menuStrip.Text = "menuStrip";

            file_tsmi.Name = "file_tsmi";
            open_tsmi.Name = "open_tsmi";
            settings_tsmi.Name = "settings_tsmi";
            language_tsmi.Name = "language_tsmi";


            // Assigning Items to menuStrip:
            menuStrip.Items.AddRange(new ToolStripItem[]
            {
                file_tsmi,
                settings_tsmi
            });

            // Assigning Items to file_tsmi:
            file_tsmi.DropDownItems.AddRange(new ToolStripItem[]
            {
                open_tsmi
            });

            // Assigning Items to file_tsmi:
            settings_tsmi.DropDownItems.AddRange(new ToolStripItem[]
            {
                language_tsmi
            });
        }


        private void Init_EventHandlers() // [Step 3] --------------------------------------------- ###
        {
            // Assigning EventHandlers:
            Click += new EventHandler(open_tsmi_Click);
        }
    }
}
