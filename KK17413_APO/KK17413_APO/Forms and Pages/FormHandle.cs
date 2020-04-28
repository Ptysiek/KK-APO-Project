using System;
using System.Windows.Forms;
using KK17413_APO.Toolbox_Tools_Expanded;


namespace KK17413_APO.Forms_and_Pages
{
    [System.ComponentModel.DesignerCategory("")]
    public class FormHandle : Panel
    {
        private MainForm mainForm;
        private DoubleClickButton button;
        private ImageForm pageRef;


        // ##########################################################################
        public FormHandle(MainForm mainForm, ImageForm pageRef, string filename)
        {
            button = new DoubleClickButton();
            button.Text = filename;
            button.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");

            this.mainForm = mainForm;
            this.pageRef = pageRef;
            this.Width = button.Width;
            this.Height = button.Height;

            button.Click += button_Click;
            button.DoubleClick += button_DoubleClick;

            this.Controls.Add(button);
        }


        // ##########################################################################
        private void button_Click(object sender, EventArgs e)
        {
            pageRef.form.WindowState = FormWindowState.Normal;
            pageRef.form.Activate();
        }

        private void button_DoubleClick(object sender, EventArgs e)
        {
            foreach(var page in ProgramSettings.Pages)
            {
                if (page != pageRef)
                    page.form.WindowState = FormWindowState.Minimized;
            }
            pageRef.form.WindowState = FormWindowState.Normal;
            pageRef.form.Activate();
        }


        // ##########################################################################
        public void DetachItself()
        {
            mainForm.DetachPageHandle(this);
        }
    }
}
