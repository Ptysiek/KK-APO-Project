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
            /*
            //OpenFileDialog FD = MainFormOperations.BrowseFile();
            OpenFileDialog FD = MainFormOperations.BrowseFile();

            if (FD != null)
            {
                bookMarks.AddPage(FD.FileName);
            }
            //*/
        }

        // #################################################################################################
        public void scrollbar_Scroll(object sender, EventArgs e)
        {
            ScrollbarLogic();
        }
    }
}