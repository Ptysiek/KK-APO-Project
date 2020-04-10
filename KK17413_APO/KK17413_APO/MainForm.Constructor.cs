using System;
using System.Windows.Forms;
using System.Drawing;
using KK17413_APO.Toolbox_Tools_Expanded;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KK17413_APO
{    
    partial class MainForm
    {        
         private void Constructor_MainInit()
        {
            // [Step 1]
            CreateInstances();

            // [Step 2]
            Init_Form();

            // [Step 3]
            Init_MenuStrip();

            // [Step 4]
            Init_BookMarks();

            // [Step 5]
            Init_EventHandlers();

            // [Step 6]
            // Assigning FormComponents to this MainForm: [Assigning parenthood]            
            this.ControlsAdd(bookmarkContainer);

            this.ControlsAdd(menuContainer);
            menuContainer.Controls.Add(menuStrip);

            this.ControlsAdd(dragNdropContainer);

            this.ControlsAdd(taskbar);
            // -----------------------------------------------------------------------------
            ResizeItems();
            ReloadLanguage();
            ReloadColorSet();
        }


        private void CreateInstances() // [Step 1] ------------------------------------------------ ###
        {
            taskbar = new Taskbar(this);
            dragNdropContainer = new Panel();

            // MAIN FORM - MAIN MENU STRIP:
            menuContainer = new Panel();
            menuStrip = new MenuStrip();
            
            file_tsmi = new ToolStripMenuItem();
            open_tsmi = new ToolStripMenuItem();
            project_tsmi = new ToolStripMenuItem();
            settings_tsmi = new ToolStripMenuItem();
            language_tsmi = new ToolStripMenuItem();

            // MAIN FORM - BOOKMARKS:
            bookmarkContainer = new FlowLayoutPanel();
        }



        private void Init_Form() // [Step 2] ------------------------------------------------ ###
        {
            taskbar.IconAssignImage("Icon.png");
            this.Workspace.BackColor = Color.Black;

            taskbar.Dock = DockStyle.Top;
            taskbar.Text = "DistortImage";
        }


        private void Init_MenuStrip() // [Step 3] ------------------------------------------------- ###
        {
            //menuStrip.ImageScalingSize   = new Size(20, 20);
            //menuStrip.Size               = new Size(800, 28);
            //menuStrip.Location           = new Point(0, 0);
            //menuStrip.TabIndex           = 0;
            //file_tsmi.Size = new Size(46, 24);
            //open_tsmi.Size = new Size(128, 26);

            menuContainer.Dock = DockStyle.Top;
            menuContainer.Height = menuStrip.Height;

            menuStrip.Dock = DockStyle.Fill;

            //menuContainer.Top = taskbar.Height;
            //menuContainer.Width = this.Workspace.Width;
            //menuContainer.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);




            // Assigning Items to menuStrip:
            menuStrip.Items.AddRange(new ToolStripItem[]{
                file_tsmi,
                project_tsmi,
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


        private void Init_BookMarks() // [Step 4] ------------------------------------------------- ###
        {
            bookmarkContainer.BorderStyle = BorderStyle.None;
            bookmarkContainer.Height = menuStrip.Height;
            bookmarkContainer.AutoScroll = true;

            bookmarkContainer.Dock = DockStyle.Top;

            //bookmarkContainer.Width = this.Workspace.Width;
            //bookmarkContainer.Top = taskbar.Height + menuStrip.Height;
            //bookmarkContainer.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);




            dragNdropContainer.Dock = DockStyle.Fill;
            //dragNdropContainer.Top = taskbar.Height + menuStrip.Height + bookmarkContainer.Height;
            //dragNdropContainer.Width = this.Workspace.Width;
            //dragNdropContainer.Height = this.Workspace.Height - dragNdropContainer.Top;
            //dragNdropContainer.BackColor = Color.Yellow;
            
            //dragNdropContainer.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);

            dragNdropContainer.AllowDrop = true;

        }


        private void Init_EventHandlers() // [Step 5] --------------------------------------------- ###
        {
            // Assigning EventHandlers:
            Resize += new EventHandler(mainForm_Resize);
            //scrollbar.Scroll += new ScrollEventHandler(scrollbar_Scroll);
            dragNdropContainer.DragDrop += dragNdropContainer_DragDrop;
            dragNdropContainer.DragEnter += dragNdropContainer_DragEnter;

            open_tsmi.Click += new EventHandler(open_tsmi_Click);
            project_tsmi.Click += new EventHandler(project_tsmi_Click);
        }


    }
}
