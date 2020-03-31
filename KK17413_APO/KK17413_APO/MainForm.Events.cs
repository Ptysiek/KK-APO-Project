using System;


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
            BrowseFile();
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
    }
}