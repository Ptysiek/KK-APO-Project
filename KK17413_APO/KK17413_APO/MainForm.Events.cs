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
        
        private void MouseFix_MouseMove(object sender, MouseEventArgs e)
        {
            MouseFix();
        }
        
        // #################################################################################################
        public void open_tsmi_Click(object sender, EventArgs e)
        {
            foreach (string value in ProgramSettings.fileVerification.BrowseFiles())
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