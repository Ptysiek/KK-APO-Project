using System;
using System.Windows.Forms;
using System.Drawing;



namespace KK17413_APO.Toolbox_Tools
{
     class SurplusForm
    {
        Form newform;
        Panel panel;

        // Take position:
        int xMouse;
        int yMouse;

        // Take action:
        int mousePressed;


        public SurplusForm()
        {
            newform = new Form();
            newform.FormBorderStyle = FormBorderStyle.None;

            newform.MouseUp += Panel_MouseUp;
            newform.MouseDown += Panel_MouseDown;
            newform.MouseMove += Panel_MouseMove;
            mousePressed = -1;

            newform.Show();

            newform.MinimumSize = new Size(100, 100);
        }



        private void Panel_MouseUp(object sender, MouseEventArgs e) => mousePressed = -1;
        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            mousePressed = 0;

            // Poprzednia pozycja myszy:
            xMouse = Cursor.Position.X;
            yMouse = Cursor.Position.Y;
        }



        private void ChangeCursor(int border)
        {
            switch (border)
            {
                case 1:     // [left]   
                case 2:     // [right]
                    newform.Cursor = Cursors.SizeWE; break;

                case 3:     // [top]
                case 6:     // [bottom]
                    newform.Cursor = Cursors.SizeNS; break;

                case 5:     // [right + top]
                case 7:     // [left + bottom]
                    newform.Cursor = Cursors.SizeNESW; break;

                case 4:     // [left + top]
                case 8:     // [right + bottom]
                    newform.Cursor = Cursors.SizeNWSE; break;

                default:    // [none]
                    newform.Cursor = Cursors.Arrow; break;
            }
        }

        private void CalculateFormResizeXRelocation(int border)
        {
            // Różnica z nowej pozycji myszy:
            int newMousePos_X = Cursor.Position.X - xMouse;
            int newMousePos_Y = Cursor.Position.Y - yMouse;

            // Przeniesienie (shift) różnicy na lewy górny narożnik (window handle):
            int w = newform.Width;
            int h = newform.Height;

            int x = newform.Location.X;
            int y = newform.Location.Y;

            switch (border)
            {
                case 1:     // [left] --------------------------------------------------------  
                    w -= newMousePos_X;
                    x += newMousePos_X;
                    newform.Width = w;

                    if (newform.Width > newform.MinimumSize.Width)
                        newform.Left = x;
                    break;

                case 2:     // [right] -------------------------------------------------------
                    w += newMousePos_X;
                    newform.Width = w;
                    break;

                case 3:     // [top] ---------------------------------------------------------
                    h -= newMousePos_Y;
                    y += newMousePos_Y;
                    newform.Height = h;

                    if (newform.Height > newform.MinimumSize.Height)
                        newform.Top = y;
                    break;

                case 6:     // [bottom] ------------------------------------------------------
                    h += newMousePos_Y;
                    newform.Height = h;
                    break;

                case 5:     // [right + top] -------------------------------------------------
                    w += newMousePos_X;
                    h -= newMousePos_Y;
                    y += newMousePos_Y;
                    newform.Size = new Size(w, h);

                    if (newform.Height > newform.MinimumSize.Height)
                        newform.Top = y;
                    break;

                case 7:     // [left + bottom] -----------------------------------------------
                    w -= newMousePos_X;
                    h += newMousePos_Y;
                    x += newMousePos_X;
                    newform.Size = new Size(w, h);

                    if (newform.Width > newform.MinimumSize.Width)
                        newform.Left = x;
                    break;

                case 4:     // [left + top] --------------------------------------------------
                    w -= newMousePos_X;
                    h -= newMousePos_Y;
                    x += newMousePos_X;
                    y += newMousePos_Y;
                    newform.Size = new Size(w, h);

                    if (newform.Width > newform.MinimumSize.Width)
                        newform.Left = x;

                    if (newform.Height > newform.MinimumSize.Height)
                        newform.Top = y;
                    break;

                case 8:     // [right + bottom] ----------------------------------------------
                    w += newMousePos_X;
                    h += newMousePos_Y;
                    newform.Size = new Size(w, h);
                    break;
            }
        }


        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {   /*
                1 =>  LEFT BORDER
                2 =>  RIGHT BORDER
                3 =>  TOP BORDER
                6 =>  BOTTOM BORDER

                1  or  2 =>  SizeWE         [left] or [right] 
                3  or  6 =>  SizeNS         [top] or [bottom]
                5  or  7 =>  SizeNESW       [right + top] or [left + bottom]
                4  or  8 =>  SizeNWSE       [left + top] or [right + bottom]
            */

            const int ces = 10;   // CES - Capture Extra Space
            int border = 0;



            if (mousePressed <= 0)
            {   // Calculate border:
                if (e.X < 0 + ces) border += 1;                 // LEFT BORDER
                if (e.X > newform.Width - ces) border += 2;     // RIGHT BORDER
                if (e.Y < 0 + ces) border += 3;                 // TOP BORDER 
                if (e.Y > newform.Height - ces) border += 6;    // BOTTOM BORDER
            }
            else border = mousePressed;

            ChangeCursor(border);



            if (mousePressed == -1) return;

            mousePressed = border;

            CalculateFormResizeXRelocation(border);

            xMouse = Cursor.Position.X;
            yMouse = Cursor.Position.Y;
        }









    }
}
