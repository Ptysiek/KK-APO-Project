using System;
using System.Windows.Forms;
using System.Drawing;



namespace KK17413_APO.Toolbox_Tools_Expanded
{
    [System.ComponentModel.DesignerCategory("")]
    public class AdjustedForm : Form
    {
        //___________________________________________________________________________________________
        // ##########################################################################################
        public int BorderToResizeWidth { get => esc;  set => esc=value; }
        public Panel Workspace { get => workspace; }


        //___________________________________________________________________________________________
        // ##########################################################################################
        private Panel workspace;

        // Distance from the edge of the form that grips the mouse position:
        private int esc;    // *esc - Extra Space Capture

        //Mouse position saved after clicking [MouseDown]:
        private int mouseState;
        private bool edgeAccess_X;
        private bool edgeAccess_Y;

        //___________________________________________________________________________________________
        /* ALL the possible Mouse States: 
         * -2 =>    MouseButton was pressed before reaching any of Form edges.
                    Mouse functionalities are blocked until its button released (mouseState = -1).
                    But also, since Taskbar field:
                    This code is perfect to distinct FormResizeOperation with FormRelocateOperation.

         * -1 =>    Mouse is Idle. Waiting to be clicked
         *  0 =>    Mouse clicked, but it is not known on which edge yet.         

         *  1 =>    Mouse clicked on Left edge.
         *  2 =>    Mouse clicked on Right edge.
         *  3 =>    Mouse clicked on Top edge.
         *  4 =>    Mouse clicked on [Left + Top] corner: [1+3]
         *  5 =>    Mouse clicked on [Right + Top] corner: [2+3]
         *  6 =>    Mouse clicked on Bottom edge.
         *  7 =>    Mouse clicked on [Left + Bottom] corner: [1+6]
         *  8 =>    Mouse clicked on [Right + Bottom] corner: [2+6]

         when (edgeAccess_X = false):
         *      This form has reached its size limit on the WIDTH axis. (LEFT or RIGHT EDGE)
                Mouse functionalities are blocked until button release (mouseState = -1)
                Or until [Cursor.Position.X] approach the left or right edge again.

         when (edgeAccess_Y = false):
         *      This form has reached its size limit on the HEIGHT axis. (TOP or BOTTOM EDGE)
                Mouse functionalities are blocked until button release (mouseState = -1)
                Or until [Cursor.Position.Y] approach the left or right edge again.         
         */


        //___________________________________________________________________________________________
        // ##########################################################################################
        public AdjustedForm()
        {
            Init();
        }


        // #################################################################################################
        private void form_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)  // if (LMB) - Left Mouse Button
            {
                // Make mouse idle:
                mouseState = -1;
                edgeAccess_X = true;
                edgeAccess_Y = true;
                Cursor = Cursors.Default;
            }
        }

        private void form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)  // if (LMB) - Left Mouse Button
            {
                // Mouse clicked, but it is not known on which edge:
                mouseState = 0;
                edgeAccess_X = true;
                edgeAccess_Y = true;
            }
            else
            {
                // Make mouse idle:
                mouseState = -1;
                edgeAccess_X = true;
                edgeAccess_Y = true;
                Cursor = Cursors.Default;
                // Making mouse idle here prevents crashes 
                // caused by clicking other buttons while resizing the form.
            }
        }

        private void form_MouseMove(object sender, MouseEventArgs e)
        => ResizementLogic();

        // #################################################################################################
        private void ResizementLogic()
        {
            if (mouseState == -1) ChangeCursor();
            else if (mouseState == 0) mouseState = ChangeCursor();

            if (mouseState == -1) return;           // Button not pressed
            if (mouseState == 0) mouseState = -2;   // Prevents misclick scenerio

            LeftEdgeLogic();
            TopEdgeLogic();
            RightEdgeLogic();
            BottomEdgeLogic();
        }

        private void LeftEdgeLogic()
        {
            // Is this edge active:
            if (mouseState == 1) { }       // Yes
            else if (mouseState == 4) { }  // Yes
            else if (mouseState == 7) { }  // Yes
            else return;    // No

            int MouseShift_X = Cursor.Position.X - Left;

            if (edgeAccess_X)
            {
                if ((Width - MouseShift_X) <= MinimumSize.Width)
                {
                    edgeAccess_X = false;
                    return;
                }
            }
            else
            {
                if (Cursor.Position.X < Left)
                    edgeAccess_X = true;
            }

            if (edgeAccess_X)
            {
                Width -= MouseShift_X;
                Left += MouseShift_X;
            }
        }

        private void RightEdgeLogic()
        {
            // Is this edge active:
            if (mouseState == 2) { }       // Yes
            else if (mouseState == 5) { }  // Yes
            else if (mouseState == 8) { }  // Yes
            else return;    // No

            // MouseShift_X:
            Width = Cursor.Position.X - Left;

            if (!edgeAccess_X)
                if (Width > MinimumSize.Width)
                    edgeAccess_X = true;
        }
        
        private void TopEdgeLogic()
        {
            // Is this edge active:
            if (mouseState == 3){}       // Yes
            else if (mouseState == 4){}  // Yes
            else if (mouseState == 5){}  // Yes
            else return;    // No


            int MouseShift_Y = Cursor.Position.Y - Top;

            if (edgeAccess_Y)
            {
                if ((Height - MouseShift_Y) <= MinimumSize.Height)
                {
                    edgeAccess_Y = false;
                    return;
                }
            }
            else
            {
                if (Cursor.Position.Y < Top)
                    edgeAccess_Y = true;
            }

            if (edgeAccess_Y)
            {
                Height -= MouseShift_Y;
                Top += MouseShift_Y;
            }
        }
       
        private void BottomEdgeLogic()
        {
            // Is this edge active:
            if (mouseState == 6){}       // Yes
            else if (mouseState == 7){}  // Yes
            else if (mouseState == 8){}  // Yes
            else return;    // No

            // MouseShift_Y:
            Height = Cursor.Position.Y - Top;

            if (!edgeAccess_Y)
                if (Height > MinimumSize.Height)
                    edgeAccess_Y = true;
        }


        // #################################################################################################
        public void ControlsAdd(Control control)
        => this.workspace.Controls.Add(control);
        

        public void MouseFix()
        => Cursor = Cursors.Default;
        

        // #################################################################################################
        private void Init()
        {
            //taskbar = new Taskbar(this);
            workspace = new Panel();

            // Set default values:
            esc = 5;
            esc = 10;

            // The mouse is idle:
            mouseState = -1;
            edgeAccess_X = true;
            edgeAccess_Y = true;

            this.FormBorderStyle = FormBorderStyle.None;
            this.AutoScaleMode = AutoScaleMode.None;
            this.MinimumSize = new Size(100, 100);
            this.ShowInTaskbar = true;  // ALT + TAB

            this.Width = 512;           // (1024 / 2)
            this.Height = 288;          // (576 / 2)
            this.BackColor = ProgramSettings.ColorManager.Transparent;
            this.TransparencyKey = ProgramSettings.ColorManager.Transparent;

            //int edgeWidth = esc * 2;
            int edgeWidth = esc;
            workspace.Dock = DockStyle.None;
            workspace.Width = this.Width - (edgeWidth * 2);
            workspace.Height = this.Height - (edgeWidth * 2);
            workspace.Left = edgeWidth;
            workspace.Top = edgeWidth;
            //workspace.BackColor = Color.White;
            workspace.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);

            this.Controls.Add(workspace);

            this.MouseUp += form_MouseUp;
            this.MouseDown += form_MouseDown;
            this.MouseMove += form_MouseMove;

            this.Show();
        }

        private int ChangeCursor()
        {
            int edges = 0;

            // Calculate which edges are under the mouse:
            if ((Cursor.Position.X - Left) < (0 + esc))      edges += 1;     // LEFT EDGE
            if ((Cursor.Position.X - Left) > (Width - esc))  edges += 2;     // RIGHT EDGE

            // _________________________________________________________________ TOP EDGE 
            if ((Cursor.Position.Y - Top) < (0 + esc))
            {                
                if (edges == 1)         Cursor = Cursors.SizeNWSE;  // top left corner                
                else if (edges == 2)    Cursor = Cursors.SizeNESW;  // top right corner                
                else                    Cursor = Cursors.SizeNS;    // top edge

                return (edges + 3);     // 3 =>  TOP EDGE
            }
            // _________________________________________________________________ BOTTOM EDGE
            if ((Cursor.Position.Y - Top) > (Height - esc))
            {
                if (edges == 1)         Cursor = Cursors.SizeNESW;  // bottom left corner
                else if (edges == 2)    Cursor = Cursors.SizeNWSE;  // bottom right corner
                else                    Cursor = Cursors.SizeNS;    // bottom edge

                return (edges + 6);     // 6 =>  BOTTOM EDGE
            }
            // _________________________________________________________________
            if (edges == 1)         Cursor = Cursors.SizeWE;    // left edge
            else if (edges == 2)    Cursor = Cursors.SizeWE;    // right edge
            else                    Cursor = Cursors.Default;   // none

            return edges;
        }


        // #################################################################################################
    }
}
