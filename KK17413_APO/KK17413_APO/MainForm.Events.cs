using System;
using System.Windows.Forms;


namespace KK17413_APO
{
    partial class MainForm
    {
        // #################################################################################################
        public void mainForm_Resize(object sender, EventArgs e)
        {
            ResizeItems();            
        }

        // #################################################################################################
        public void open_tsmi_Click(object sender, EventArgs e)
        {
            foreach (string value in ProgramSettings.fileVerification.BrowseFiles())            
                CreateWorkspace(value);            
        }
        
        public void project_tsmi_Click(object sender, EventArgs e)
        {
            CreateWorkspace();
        }

        // #################################################################################################
        public void scrollbar_Scroll(object sender, EventArgs e)
        {
            ScrollbarLogic();
        }

        private void dragNdropContainer_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void dragNdropContainer_DragDrop(object sender, DragEventArgs e)
        {

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (string value in files)
            {
                CreateWorkspace(value);
                Console.WriteLine(value);
            }
            
        }
    }
}