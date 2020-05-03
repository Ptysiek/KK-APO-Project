using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using KK17413_APO.Toolbox_Tools_Expanded;
using KK17413_APO.Forms_and_Pages;


namespace KK17413_APO.Forms_and_Pages
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class MainForm : AdjustedForm
    {
        // #################################################################################################
        public Taskbar taskbar;
        public Panel dragNdropContainer;
        public Label dragNdropText1;
        public Label dragNdropText2;

        public FlowLayoutPanel pageHandlersContainer;
        public Panel menuContainer;
        public MenuStrip menuStrip;

        public ToolStripMenuItem file_tsmi;
        public ToolStripMenuItem open_tsmi;
        public ToolStripMenuItem project_tsmi;
        public ToolStripMenuItem settings_tsmi;
        public ToolStripMenuItem language_tsmi;
        // *tsmi - Tool Strip Menu Item


        // #################################################################################################
        public void DetachPageHandle(ImageForm_Handle pageHandle)
        {
            pageHandlersContainer.Controls.Remove(pageHandle);
        }
        public void AssignEventHandlers() // [Step 5] --------------------------------------------- ###
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


        // #################################################################################################
        public void ReloadLanguage()
        {
            file_tsmi.Text = ProgramSettings.Language.GetValue("file_tsmi");
            open_tsmi.Text = ProgramSettings.Language.GetValue("open_tsmi");
            project_tsmi.Text = ProgramSettings.Language.GetValue("project_tsmi");
            settings_tsmi.Text = ProgramSettings.Language.GetValue("settings_tsmi");
            language_tsmi.Text = ProgramSettings.Language.GetValue("language_tsmi");
        }
        public void ReloadColorSet()
        {
            // This Form Layout:
            taskbar.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            taskbar.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");
            taskbar.IconChangeColor(ProgramSettings.ColorManager.GetValue("detailColor2"));

            dragNdropText1.ForeColor = ProgramSettings.ColorManager.GetValue("detailColor2");
            dragNdropText2.ForeColor = ProgramSettings.ColorManager.GetValue("detailColor2");
            pageHandlersContainer.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");
            dragNdropContainer.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer3");

            // MenuStrip:
            menuStrip.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            menuStrip.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");

            open_tsmi.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            open_tsmi.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
            language_tsmi.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            language_tsmi.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer1");
        }
        public void ResizeItems()
        {
            dragNdropText1.Top = (dragNdropContainer.Height / 2) - (dragNdropText1.Height / 2);
            dragNdropText2.Top = dragNdropText1.Top + dragNdropText1.Height;

            dragNdropText1.Left = (dragNdropContainer.Width / 2) - dragNdropText1.Width / 2;
            dragNdropText2.Left = (dragNdropContainer.Width / 2) - dragNdropText2.Width / 2;
        }
        private void CreateImageWorkspace(string filename = null)
        {
            // Create new ImagePage:
            ImageForm newPage = new ImageForm_Builder().GetResult(filename);

            // Create new PageHandle:
            ImageForm_Handle newPageHandle = new ImageForm_Handle(this, newPage, filename);

            // Assign new page handle to the new image page:
            newPage.PageHandle = newPageHandle;

            // Assign new page handle to the MainForm:
            pageHandlersContainer.Controls.Add(newPageHandle);

            // Add new page to the list:
            ProgramSettings.Pages.Add(newPage);
        }
        
        
        // #################################################################################################
        public void mainForm_Resize(object sender, EventArgs e)
        {
            ResizeItems();
        }

        private void MouseFix_MouseMove(object sender, MouseEventArgs e)
        {
            MouseFix();
        }

        // #################################################################################################
        public void open_tsmi_Click(object sender, EventArgs e)
        {
            foreach (string value in ProgramSettings.FileVerification.BrowseFiles())
                CreateImageWorkspace(value);
        }

        public void project_tsmi_Click(object sender, EventArgs e)
        {
            CreateImageWorkspace();
        }

        // #################################################################################################
        private void dragNdropContainer_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void dragNdropContainer_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (string value in files)
                CreateImageWorkspace(value);
        }

        // #################################################################################################
    }
}
