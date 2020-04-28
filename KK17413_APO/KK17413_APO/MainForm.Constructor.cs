using System;
using System.Windows.Forms;
using System.Drawing;
using KK17413_APO.Toolbox_Tools_Expanded;
using System.Reflection;
using System.IO;
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
            Init_Containers();

            // [Step 5]
            Init_EventHandlers();

            // [Step 6]
            // Assigning FormComponents to this MainForm: [Assigning parenthood]            
            this.ControlsAdd(pageHandlersContainer);

            this.ControlsAdd(menuContainer);
            menuContainer.Controls.Add(menuStrip);

            this.ControlsAdd(dragNdropContainer);
            dragNdropContainer.Controls.Add(dragNdropText1);
            dragNdropContainer.Controls.Add(dragNdropText2);

            this.ControlsAdd(taskbar);
            // -----------------------------------------------------------------------------            
            ResizeItems();
            ReloadLanguage();
            ReloadColorSet();
        }


        private void CreateInstances() // [Step 1] ------------------------------------------------ ###
        {
            taskbar = new Taskbar(this);

            // MAIN FORM - MAIN MENU STRIP:
            menuContainer = new Panel();
            menuStrip = new MenuStrip();
            
            file_tsmi = new ToolStripMenuItem();
            open_tsmi = new ToolStripMenuItem();
            project_tsmi = new ToolStripMenuItem();
            settings_tsmi = new ToolStripMenuItem();
            language_tsmi = new ToolStripMenuItem();

            // MAIN FORM - CONTAINERS:
            pageHandlersContainer = new FlowLayoutPanel();

            dragNdropContainer = new Panel();
            dragNdropText1 = new Label();
            dragNdropText2 = new Label();
        }

        private void Init_Form() // [Step 2] ------------------------------------------------------ ###
        {
            taskbar.IconAssignImage("KK17413_APO.Resources.Icon.png");

            this.Workspace.BackColor = Color.Black;

            taskbar.Dock = DockStyle.Top;
            taskbar.Text = "DistortImage";
        }

        private void Init_MenuStrip() // [Step 3] ------------------------------------------------- ###
        {
            menuContainer.Dock = DockStyle.Top;
            menuContainer.Height = menuStrip.Height;

            menuStrip.Dock = DockStyle.Fill;

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

        private void Init_Containers() // [Step 4] ------------------------------------------------ ###
        {
            pageHandlersContainer.Dock = DockStyle.Top;
            pageHandlersContainer.BorderStyle = BorderStyle.None;
            pageHandlersContainer.Height = menuStrip.Height + 22;
            pageHandlersContainer.AutoScroll = true;
            pageHandlersContainer.WrapContents = false;
            pageHandlersContainer.FlowDirection = FlowDirection.LeftToRight;

            dragNdropContainer.Dock = DockStyle.Fill;
            dragNdropContainer.AllowDrop = true;
            dragNdropText1.Text = "Drop your image here";
            dragNdropText2.Text = "[ bmp, jpg, png, tiff ]";
            dragNdropText1.TextAlign = ContentAlignment.MiddleCenter;
            dragNdropText2.TextAlign = ContentAlignment.MiddleCenter;

            dragNdropText1.AutoSize = true;
            dragNdropText2.AutoSize = true;

            dragNdropText1.Font = new Font(dragNdropText1.Font.Name, 26, dragNdropText1.Font.Style);
            dragNdropText2.Font = new Font(dragNdropText2.Font.Name, 13, dragNdropText2.Font.Style);
        }

        private void Init_EventHandlers() // [Step 5] --------------------------------------------- ###
        {
            // Assigning EventHandlers:
            Resize += new EventHandler(mainForm_Resize);

            dragNdropContainer.DragDrop += dragNdropContainer_DragDrop;
            dragNdropContainer.DragEnter += dragNdropContainer_DragEnter;

            open_tsmi.Click += new EventHandler(open_tsmi_Click);
            project_tsmi.Click += new EventHandler(project_tsmi_Click);

            pageHandlersContainer.MouseMove += MouseFix_MouseMove;
            menuContainer.MouseMove += MouseFix_MouseMove;
            menuStrip.MouseMove += MouseFix_MouseMove;
            dragNdropContainer.MouseMove += MouseFix_MouseMove;
        }

    }
}
