using System;
using System.Drawing;
using System.Windows.Forms;
using KK17413_APO.Toolbox_Tools_Expanded;


namespace KK17413_APO.Forms_and_Pages
{
    [System.ComponentModel.DesignerCategory("")]
    public class FormHandle : Panel
    {
        private MainForm mainForm;
        private ImageForm pageRef;
        private Button closeButton;
        private DoubleClickButton button;


        // ##########################################################################
        public FormHandle(MainForm mainForm, ImageForm pageRef, string filename)
        {

            button = new DoubleClickButton();
            button.Text = filename;
            button.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            button.FlatStyle = FlatStyle.Flat;
            //button.Left = button.Height - 2;

            closeButton = new Button();
            closeButton.Width = button.Height - 8; 
            closeButton.Height = button.Height - 8;
            closeButton.Left = (button.Height - closeButton.Height) / 2;
            closeButton.Top = (button.Height - closeButton.Height) / 2;
            closeButton.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            closeButton.BackColor = Color.IndianRed;
            //closeButton.Dock = DockStyle.Left;
            closeButton.FlatStyle = FlatStyle.Flat;

            this.mainForm = mainForm;
            this.pageRef = pageRef;
            this.Width = button.Width + closeButton.Width;
            this.Height = button.Height;

            closeButton.Click += closeButton_Click;
            button.Click += button_Click;
            button.DoubleClick += button_DoubleClick;

            button.Controls.Add(closeButton);
            this.Controls.Add(button);
        }



        // ##########################################################################
        private void closeButton_Click(object sender, EventArgs e)
        {
            //mainForm.DetachPageHandle(this);

            pageRef.form.Close();

            //ProgramSettings.Pages.Remove(pageRef);
        }

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
