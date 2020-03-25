using System;
using System.Windows.Forms;
using System.Drawing;


namespace KK17413_APO
{    
    partial class MainForm
    {        
         private void Constructor_MainInit()
        {
            // [Step 1]
            CreateInstances();

            // [Step 2]
            Init_MenuStrip();

            // [Step 3]
            Init_BookMarks();

            // [Step 4]
            Init_EventHandlers();

            // [Step 4]
            // Assigning FormComponents to this MainForm: [Assigning parenthood]
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);
            this.Controls.Add(containerBOX);

            containerBOX.Controls.Add(scrollbar);
            containerBOX.Controls.Add(container);

            // -----------------------------------------------------------------------------
            initialized = true;
            ResizeItems();
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
            scrollbar = new HScrollBar();
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
            menuStrip.Items.AddRange(new ToolStripItem[]{
                file_tsmi,
                settings_tsmi
            });

            // Assigning Items to file_tsmi:
            file_tsmi.DropDownItems.AddRange(new ToolStripItem[]{
                open_tsmi
            });

            // Assigning Items to file_tsmi:
            settings_tsmi.DropDownItems.AddRange(new ToolStripItem[]{
                language_tsmi
            });
        }


       
        private void Init_BookMarks() // [Step 3] ------------------------------------------------- ###
        {            
            // CONTAINER BOX - Holds all Bookmark elements
            containerBOX.Enabled = false;
            containerBOX.Name = "containerBOX";
            containerBOX.BorderStyle = BorderStyle.Fixed3D;
            containerBOX.Location = new Point(0, 24);
            containerBOX.Height = 43;
            //containerBOX.BackColor = Color.Silver;


            // CONTAINER - Holds generated Buttons
            container.Name = "container";
            container.BorderStyle = BorderStyle.None;
            //container.Width = AbstractWidth;
            //container.Height = 26;
            //container.Size = new Size(AbstractWidth, 26);
            //container.BackColor = Color.Silver;


            // SCROLLBAR - BOOKMARK SCROLLBAR
            scrollbar.Enabled = false;
            scrollbar.Location = new Point(0, 26);
            scrollbar.Height = 10;
            scrollbar.SmallChange = 75;     // 75 = Default Button Width
            //scrollbar.FlatStyle = FlatStyle.Flat
        }

        private void Init_EventHandlers() // [Step 4] --------------------------------------------- ###
        {
            // Assigning EventHandlers:
            Resize += new EventHandler(mainForm_Resize);
            scrollbar.Scroll += new ScrollEventHandler(scrollbar_Scroll);
            Click += new EventHandler(open_tsmi_Click);
        }
    }
}
