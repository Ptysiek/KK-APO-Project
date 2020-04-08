using System;
using System.Windows.Forms;
using System.Drawing;


namespace KK17413_APO.Toolbox_Tools_Expanded
{
    class Taskbar : Panel
    {
        // #################################################################################################
        public bool AutonomicMode
        {
            get { return autonomic; }
            set { autonomic = value; }
        }

        // Take position:
        private int xMouseDown;
        private int yMouseDown;

        // Take action:
        private bool mousePressed;
        private bool autonomic;


        // #################################################################################################
        public Taskbar()
        {
            this.Dock = DockStyle.Top;
            this.BorderStyle = BorderStyle.None;
            this.Height = 40 - 8;
            this.BackColor = Color.Red;

            this.MouseUp += Taskbar_MouseUp;
            this.MouseDown += Taskbar_MouseDown;
            this.MouseMove += Taskbar_MouseMove;            

            mousePressed = false;
            autonomic = true;
        }


        // #################################################################################################
        private void Taskbar_MouseUp(object sender, MouseEventArgs e) 
        {            
            if (!autonomic) return;
            ProceedMouseUp();
        }
        private void Taskbar_MouseDown(object sender, MouseEventArgs e)
        {
            if (!autonomic) return;
            ProceedMouseDown();
        }
        private void Taskbar_MouseMove(object sender, MouseEventArgs e)
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



