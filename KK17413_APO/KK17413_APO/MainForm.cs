using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;


namespace KK17413_APO
{
    public partial class MainForm : Form
    {
        private ProgramSettings programSettings;
        private ProgramLanguage languageDictionary;      // Set which language is currently used.
                                                         // Get wordFields_Dictionary of currently chosen language.

        // ---------------------------------------------------------------------------------------------
        private MenuStrip menuStrip;
        private ToolStripMenuItem file_tsmi;
        private ToolStripMenuItem open_tsmi;
        private ToolStripMenuItem settings_tsmi;
        private ToolStripMenuItem language_tsmi;
        // *tsmi - Tool Strip Menu Item

        // ---------------------------------------------------------------------------------------------
        //private HScrollBar h_scrollbar;


        public MainForm()
        {
            InitializeComponent();
            Init();
        }


        public void Init()
        {
            programSettings = new ProgramSettings();
            languageDictionary = new ProgramLanguage();
        
            // _____________________________________________________________________________________________
            // ---------------------------------------------------------------------------------------------
            menuStrip = new MenuStrip()
            {
                //ImageScalingSize    = new Size(20,20),
                //Size                = new Size(800,28),
                //Location            = new Point(0,0),

                //TabIndex = 0,
                Name = "menuStrip",
                Text = "menuStrip"
            };

            // ---------------------------------------------------------------------------------------------
            file_tsmi = new ToolStripMenuItem()
            {
                //Size = new Size(46, 24),
                Name = "file_tsmi",
                Text = languageDictionary.GetValue("file_tsmi")
            };

            open_tsmi = new ToolStripMenuItem()
            {
                //Size = new Size(128, 26),
                Name = "open_tsmi",
                Text = languageDictionary.GetValue("open_tsmi")
            };

            settings_tsmi = new ToolStripMenuItem()
            {
                //Size = new Size(46, 24),
                Name = "settings_tsmi",
                Text = languageDictionary.GetValue("settings_tsmi")
            };

            language_tsmi = new ToolStripMenuItem()
            {
                //Size = new Size(46, 24),
                Name = "language_tsmi",
                Text = languageDictionary.GetValue("language_tsmi")
            };



            // _____________________________________________________________________________________________
            // ---------------------------------------------------------------------------------------------
            // Assigning Items to menuStrip:
            menuStrip.Items.AddRange(new ToolStripItem[] 
            {
                file_tsmi,
                settings_tsmi
            });

            // ---------------------------------------------------------------------------------------------
            // Assigning Items to file_tsmi:
            file_tsmi.DropDownItems.AddRange(new ToolStripItem[] 
            {
                open_tsmi
            });

            // ---------------------------------------------------------------------------------------------
            // Assigning Items to file_tsmi:
            settings_tsmi.DropDownItems.AddRange(new ToolStripItem[] 
            {
                language_tsmi
            });

            // ---------------------------------------------------------------------------------------------
            // Assigning EventHandlers:
            Click += new EventHandler(open_tsmi_Click);


            // _____________________________________________________________________________________________
            // ---------------------------------------------------------------------------------------------
            // Assigning FormComponents to this MainForm:
            this.Controls.Add(menuStrip);
            this.MainMenuStrip = menuStrip;


        }



        private void open_tsmi_Click(object sender, EventArgs e)
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



    }
}
