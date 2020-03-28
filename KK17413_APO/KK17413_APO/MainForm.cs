using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;


namespace KK17413_APO
{
    [System.ComponentModel.DesignerCategory("")]
    //[System.ComponentModel.DesignerCategory("Form")]
    public partial class MainForm : Form
    {
        // #################################################################################################
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
        private List<ImagePage> imagePages = new List<ImagePage>();

        // ---------------------------------------------------------------------------------------------
        private bool initialized = false;   // INIT FLAG


        // #################################################################################################
        public MainForm()
        {
            InitializeComponent();
            Constructor_MainInit();
            ReloadLanguage();
        }


        // #################################################################################################
        private void ReloadLanguage()
        {
            file_tsmi.Text = languageDictionary.GetValue("file_tsmi");
            open_tsmi.Text = languageDictionary.GetValue("open_tsmi");
            settings_tsmi.Text = languageDictionary.GetValue("settings_tsmi");
            language_tsmi.Text = languageDictionary.GetValue("language_tsmi");
        }
        private void ResizeItems()
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
            ScrollbarLogic();
        }
        public void ScrollbarLogic()
        {
            container.Location = new Point(-scrollbar.Value, 0);
        }


        private void BrowseFile()
        {
            // FD - File Dialog for image browsing:
            OpenFileDialog FD = new OpenFileDialog()
            {
                // Set the default directory to Current Directory:
                InitialDirectory = System.IO.Directory.GetCurrentDirectory(),
                Title = "Browse Your Image",
                Multiselect = true,
                AddExtension = false,
            };

            // ShowDialog() - Opens "Browse Window" with File Dialog 
            if (FD.ShowDialog() == DialogResult.OK)
            {
                foreach (string value in FD.FileNames)
                {
                    //Console.WriteLine(value);
                    //GetResult(string fileName)
                    AddPage(value);

                }
            }                        
        }

        private void AddPage(string filename)
        {
            imagePages.Add(ImagePage_Builder.GetResult(filename));
        }

    }
}
