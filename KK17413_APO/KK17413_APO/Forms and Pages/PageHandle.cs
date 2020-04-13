using System;
using System.Windows.Forms;


namespace KK17413_APO.Forms_and_Pages
{
    [System.ComponentModel.DesignerCategory("")]
    public class PageHandle : Panel
    {
        private Button button;
        private ImagePage pageRef;


        // ##########################################################################
        public PageHandle(ImagePage pageRef, string filename)
        {
            button = new DoubleClickButton();
            button.Text = filename;
            button.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");

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
                page.form.WindowState = FormWindowState.Minimized;
            }
            pageRef.form.WindowState = FormWindowState.Normal;
            pageRef.form.Activate();
        }


        // ##########################################################################       
    }



    public class DoubleClickButton : Button
    {
        public DoubleClickButton()
        {
            SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);
        }
    }
}
