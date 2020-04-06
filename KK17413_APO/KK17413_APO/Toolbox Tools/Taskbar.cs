using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace KK17413_APO.Toolbox_Tools
{
    class Taskbar
    {
        Form newform;
        Panel panel;

        Taskbar()
        {
            newform = new Form();
            newform.FormBorderStyle = FormBorderStyle.None;

            panel = new Panel();

            panel.Dock = DockStyle.Top;
            panel.BorderStyle = BorderStyle.None;
            panel.Height = 40 - 8;
            panel.BackColor = Color.Red;


            panel.MouseUp += Panel_MouseUp;
            panel.MouseDown += Panel_MouseDown;
            panel.MouseMove += Panel_MouseMove;


            newform.Controls.Add(panel);
            mousePressed = false;
        }



        private void Panel_MouseUp(object sender, MouseEventArgs e) => mousePressed = false;
        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            mousePressed = true;

            // Poprzednia pozycja myszy:
            xMouse = Cursor.Position.X;
            yMouse = Cursor.Position.Y;
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mousePressed) return;

            // Różnica z nowej pozycji myszy:
            int newMousePos_X = Cursor.Position.X - xMouse;
            int newMousePos_Y = Cursor.Position.Y - yMouse;

            // Przeniesienie (shift) różnicy na lewy górny narożnik (window handle):
            int x = newform.Location.X + newMousePos_X;
            int y = newform.Location.Y + newMousePos_Y;

            newform.Location = new Point(x, y);

            xMouse = Cursor.Position.X;
            yMouse = Cursor.Position.Y;
        }



        // Take position:
        int xMouse;
        int yMouse;

        // Take action:
        bool mousePressed;

        
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




    }
}
