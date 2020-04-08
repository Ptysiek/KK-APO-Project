﻿using System;
using System.Windows.Forms;
using System.Drawing;


namespace KK17413_APO.Toolbox_Tools_Expanded
{
    class Taskbar : Panel
    {
        // #################################################################################################
        public bool AutonomicMode { get{return autonomic;}  set{autonomic=value;} }
        public override string Text { get{return Title.Text;}  set{Title.Text=value;} }
        public float FontSize { 
            get{return Title.Font.Size;}  
            set{Title.Font=new Font(Title.Font.Name,value,Title.Font.Style);} }
        public string FontName { 
            get{return Title.Font.Name;}  
            set{Title.Font=new Font(value,Title.Font.Size,Title.Font.Style);} }


        // #################################################################################################
        public Label Title;
        public PictureBox Icon;
        
        // Take position:
        private int xMouseDown;
        private int yMouseDown;

        // Take action:
        private bool mousePressed;
        private bool autonomic;


        // #################################################################################################
        public Taskbar()
        {
            Init();
        }        
        public Taskbar(bool AutonomicMode)
        {
            Init();
            this.autonomic = AutonomicMode;
        }


        // #################################################################################################
        private void Init()
        {
            Title = new Label();
            Icon = new PictureBox();

            this.Dock = DockStyle.Top;
            this.BorderStyle = BorderStyle.None;
            this.Height = 32;
            this.BackColor = Color.LightGray;

            Title.Text = "[Default Taskbar Title]";
            Title.Top = (this.Height / 8) + (this.Height / 8);
            Title.Height -= Title.Height / 8;
            Title.Left = this.Height;

            Title.AutoSize = true;
            Title.AutoEllipsis = true;
            Title.Font = new Font("Corbel", (this.Height / 4) + 2, Title.Font.Style);

            int IconScale = 6;
            int IconSize = this.Height - IconScale;
            Icon.Left = IconScale/2;
            Icon.Top = IconScale/2;
            Icon.Width = IconSize;
            Icon.Height = IconSize;

            Icon.Image = new Bitmap("Icon.png");
            /*
            Bitmap tmp = Icon.Image;

            for (int y = 0; y < tmp.Height; y++)
            {
                for (int x = 0; x < tmp.Width; x++)
                {
                    //get pixel value
                    Color p = tmp.GetPixel(x, y);

                    //extract ARGB value from p
                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    r = 45;
                    g = 45;
                    b = 48;

                    //set image pixel
                    tmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }
            Icon.Image = tmp;
            //*/
            Icon.SizeMode = PictureBoxSizeMode.Zoom;
            Icon.ClientSize = new Size(IconSize, IconSize);

            this.Controls.Add(Title);
            this.Controls.Add(Icon);

            this.Resize += taskbar_Resize;
            this.MouseUp += taskbar_MouseUp;
            this.MouseDown += taskbar_MouseDown;
            this.MouseMove += taskbar_MouseMove;

            Title.MouseUp += taskbar_MouseUp;
            Title.MouseDown += taskbar_MouseDown;
            Title.MouseMove += taskbar_MouseMove;

            Icon.MouseUp += taskbar_MouseUp;
            Icon.MouseDown += taskbar_MouseDown;
            Icon.MouseMove += taskbar_MouseMove;

            mousePressed = false;
            autonomic = true;
        }




        // #################################################################################################
        private void taskbar_Resize(object sender, EventArgs e)
        {
            Title.Width = (this.Width) - this.Height;
        }
        private void taskbar_MouseUp(object sender, MouseEventArgs e) 
        {            
            if (!autonomic) return;
            ProceedMouseUp();
        }
        private void taskbar_MouseDown(object sender, MouseEventArgs e)
        {
            if (!autonomic) return;
            ProceedMouseDown();
        }
        private void taskbar_MouseMove(object sender, MouseEventArgs e)
        {
            if (!autonomic) return;
            RelocateParent();
        }



        // #################################################################################################
        public void ProceedMouseUp()
        {
            if (this.Parent == null) return;

            // Set the mousePressed flag:  [Released]
            mousePressed = false;
        }

        public void ProceedMouseDown()
        {
            if (this.Parent == null) return;

            // Set the mousePressed flag:
            mousePressed = true;

            // Capture the mouse position from the moment of click:
            xMouseDown = Cursor.Position.X - this.Parent.Left;
            yMouseDown = Cursor.Position.Y - this.Parent.Top;
        }

        public void RelocateParent()
        {
            if (this.Parent == null) return;
            if (!mousePressed) return;

            int x = Cursor.Position.X - xMouseDown;
            int y = Cursor.Position.Y - yMouseDown;

            this.Parent.Left = x;
            this.Parent.Top = y;
        }
        // #################################################################################################
    }
}












//this.Icon
/// ShowIcon 
/// ShowInTaskbar 
//this.FormBorderStyle = FormBorderStyle.FixedDialog;
//this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
//this.MinimizeBox = true;
//this.MaximizeBox = true;
//this.ShowInTaskbar = true; // ALT + TAB
// Form.WindowState

// COOL ADDITIONAL:
// TabStop

// Śmieszny pokurcz:
//this.SizeGripStyle = SizeGripStyle.Show;

/* Cool
this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
this.ShowInTaskbar = true; // ALT + TAB
this.MinimizeBox = true;
this.MaximizeBox = true;
*/


// FrameStyle Enum

/*  
// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// Create form to be owned.
Form ownedForm = new Form();
// Set the text of the owned form.
ownedForm.Text = "Owned Form " + this.OwnedForms.Length;
// Add the form to the array of owned forms.
this.AddOwnedForm(ownedForm);
// Show the owned form.
ownedForm.Show();

// Loop through all owned forms and change their text.
for (int x = 0; x < this.OwnedForms.Length; x++)
    this.OwnedForms[x].Text = "My Owned Form " + x.ToString();

// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
*/



//this.SetBounds(10, 10, 200, 200);
// Do not allow form to be displayed in taskbar.

//this.ShowDialog();


//this.OnLayout += new LayoutEventArgs(test);
//Form.WndProc(Message) Method
//protected override void WndProc(ref System.Windows.Forms.Message m);

//this.CreateHandle();



//this.CreateParams.Style = 5;
//this.CreateParams.

//this.TopLevel = true;



