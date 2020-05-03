using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace KK17413_APO.Forms_and_Pages
{
    [System.ComponentModel.DesignerCategory("")]
    public class ImageForm_Handle : Panel
    {
        private MainForm mainForm;
        private ImageForm pageRef;
        private Button closeButton;
        private Panel mainButton;
        private Label mbText; // mainButtonText


        // ##########################################################################
        public ImageForm_Handle(MainForm mainForm, ImageForm pageRef, string filename)
        {
            mbText = new Label();
            mainButton = new Panel();

            mainButton.Width = 72;
            mainButton.Height = 23;

            Size minSize = new Size (mainButton.Width, mainButton.Height);
            Size maxSize = new Size (200, mainButton.Height);
            mbText.MinimumSize = minSize;
            mbText.MaximumSize = maxSize;
            mainButton.MinimumSize = minSize;
            mainButton.MaximumSize = maxSize;
            this.MinimumSize = minSize;
            this.MaximumSize = maxSize;

            mbText.AutoEllipsis = false;
            mbText.AutoSize = true;
            mbText.Text = CalculateText(filename);            
            mbText.TextAlign = ContentAlignment.TopLeft;            
            
            mainButton.BorderStyle = BorderStyle.FixedSingle;

            closeButton = new Button();            
            closeButton.BackColor = Color.IndianRed;
            closeButton.FlatStyle = FlatStyle.Flat;
            const int closeButtonPadding = 8;
            closeButton.Width = mainButton.Height - closeButtonPadding; 
            closeButton.Height = mainButton.Height - closeButtonPadding;
            closeButton.Left = (closeButtonPadding-1) / 2;
            closeButton.Top = (closeButtonPadding-1) / 2;
            mbText.Left = 0;
            mbText.Top = (closeButtonPadding - 1) / 2;

            this.mainForm = mainForm;
            this.pageRef = pageRef;

            closeButton.Click += closeButton_Click;
            mainButton.Click += button_Click;
            mainButton.DoubleClick += button_DoubleClick;
            mbText.Click += button_Click;
            mbText.DoubleClick += button_DoubleClick;

            mainButton.Controls.Add(closeButton);
            mainButton.Controls.Add(mbText);
            this.Controls.Add(mainButton);

            int buttonWidth = mbText.Width;
            int buttonHeight = mainButton.Height;
            mbText.AutoSize = false;
            mbText.AutoEllipsis = true;
            
            mbText.Width = buttonWidth;
            mbText.Height = buttonHeight;
            this.Width = buttonWidth;
            this.Height = buttonHeight;
            mainButton.Width = buttonWidth;
            mainButton.Height = buttonHeight;

            ReloadColorSet();
        }


        // TODO:
        // Issiue: You can scroll the handleContainer when form's not active


        // ##########################################################################
        public void DetachItself()
        => mainForm.DetachPageHandle(this);    
        
        public void ReloadColorSet()
        {
            mbText.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
            mainButton.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer2");
            closeButton.ForeColor = ProgramSettings.ColorManager.GetValue("fontColor");
        }

        // ##########################################################################
        private void closeButton_Click(object sender, EventArgs e)
        => pageRef.form.Close();        

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
        private string CalculateText(string filename)
        {
            if (filename == null) return filename;

            var value = new FileInfo(filename);

            return "       " + value.Name;
        }
    }
}
