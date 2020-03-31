using System;
using System.Windows.Forms;
using System.Drawing;


namespace KK17413_APO
{
    class WorkspacePage
    {
        public WorkspacePage()
        {
            form = new Form();
            containerMenu = new FlowLayoutPanel();
            containerWorkspace = new TableLayoutPanel();

            menuStrip = new MenuStrip();
            file_tsmi = new ToolStripMenuItem();
            histogram_tsmi = new ToolStripMenuItem();


            form.Show();
        }


        private Form form;
        private FlowLayoutPanel containerMenu;
        private TableLayoutPanel containerWorkspace;

        private MenuStrip menuStrip;
        private ToolStripMenuItem file_tsmi;
        private ToolStripMenuItem histogram_tsmi;




    }
}
