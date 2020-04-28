using System;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.IO;

namespace KK17413_APO.Toolbox_Tools_Expanded
{
    [System.ComponentModel.DesignerCategory("")]
    public class Taskbar : Panel
    {
        // #################################################################################################
        public override string Text { get{return Title.Text;}  set{Title.Text=value;} }
        public float FontSize { 
            get{return Title.Font.Size;}  
            set{Title.Font=new Font(Title.Font.Name,value,Title.Font.Style);} }
        public string FontName { 
            get{return Title.Font.Name;}  
            set{Title.Font=new Font(value,Title.Font.Size,Title.Font.Style);} }


        // #################################################################################################
        public Form dragControl = null;
        public Label Title;
        public PictureBox Icon;
        public Bitmap iconBitmap;

        private Button minimize;
        private Button maximize;
        private Button close;
        
        // Take position:
        private int xMouseDown;
        private int yMouseDown;

        // Take action:
        private bool mousePressed;


        // #################################################################################################
        public Taskbar(Form dragControl)
        {
            this.dragControl = dragControl;
            Init();
        }    

        // #################################################################################################
        private void Init()
        {
            Title = new Label();
            Icon = new PictureBox();
            minimize = new Button();
            maximize = new Button();
            close = new Button();

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
            Icon.SizeMode = PictureBoxSizeMode.Zoom;
            Icon.ClientSize = new Size(IconSize, IconSize);
            
            minimize.Dock = DockStyle.Right;
            minimize.Width = this.Height;
            maximize.Dock = DockStyle.Right;
            maximize.Width = this.Height;
            close.Dock = DockStyle.Right;
            close.Width = this.Height;
            close.TextAlign = ContentAlignment.MiddleCenter;
            close.Text = "X";
            close.BackColor = Color.IndianRed;
            close.TabStop = false;
            close.FlatStyle = FlatStyle.Flat;
            close.UseVisualStyleBackColor = false;


            //this.Controls.Add(minimize);
            //this.Controls.Add(maximize);
            this.Controls.Add(close);

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

            //minimize.Click += minimize_Click;
            //maximize.Click += maximize_Click;
            close.Click += close_Click;

            mousePressed = false;
        }
        

        // #################################################################################################
        private void close_Click(object sender, EventArgs e)
        => dragControl.Close();        

        private void maximize_Click(object sender, EventArgs e)
        {}

        private void minimize_Click(object sender, EventArgs e)
        {}


        // #################################################################################################
        private void taskbar_Resize(object sender, EventArgs e)
        {
            Title.Width = (this.Width) - this.Height;
        }

        private void taskbar_MouseUp(object sender, MouseEventArgs e) 
        => ProceedMouseUp();
        
        private void taskbar_MouseDown(object sender, MouseEventArgs e)
        => ProceedMouseDown();
        
        private void taskbar_MouseMove(object sender, MouseEventArgs e)
        => RelocateParent();


        // #################################################################################################
        public void IconAssignImage(string filename)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream stm = asm.GetManifestResourceStream(filename);

            iconBitmap = new Bitmap(stm);
            Icon.Image = iconBitmap;
        }

        public void IconAddColor(Color color)
        {
            Bitmap tmp = iconBitmap;

            for (int y = 0; y < tmp.Height; y++)
            {
                for (int x = 0; x < tmp.Width; x++)
                {
                    //get pixel value
                    Color p = tmp.GetPixel(x, y);

                    //extract ARGB value from p and sum up with color:
                    int a = (p.A + color.A > 255)?  255 : (p.A + color.A);
                    int r = (p.R + color.R > 255)?  255 : (p.R + color.R);
                    int g = (p.G + color.G > 255)?  255 : (p.G + color.G);
                    int b = (p.B + color.B > 255)?  255 : (p.B + color.B);

                    //set image pixel
                    tmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }
            Icon.Image = tmp;            
        }
        
        public void IconChangeColor(Color color)
        {
            Bitmap tmp = iconBitmap;

            for (int y = 0; y < tmp.Height; y++)
            {
                for (int x = 0; x < tmp.Width; x++)
                {
                    //get pixel value
                    Color p = tmp.GetPixel(x, y);

                    //extract ARGB value from p and sum up with color:
                    int a = p.A;
                    int r = color.R;
                    int g = color.G;
                    int b = color.B;

                    //set image pixel
                    tmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }
            Icon.Image = tmp;            
        }



        // #################################################################################################
        private void ProceedMouseUp()
        {
            if (dragControl == null) return;

            // Set the mousePressed flag:  [Released]
            mousePressed = false;
        }

        private void ProceedMouseDown()
        {
            if (dragControl == null) return;

            // Set the mousePressed flag:
            mousePressed = true;

            // Capture the mouse position from the moment of click:
            xMouseDown = Cursor.Position.X - dragControl.Left;
            yMouseDown = Cursor.Position.Y - dragControl.Top;
        }

        private void RelocateParent()
        {
            Cursor = Cursors.Default;

            if (dragControl == null) return;
            if (!mousePressed) return;

            int x = Cursor.Position.X - xMouseDown;
            int y = Cursor.Position.Y - yMouseDown;

            dragControl.Left = x;
            dragControl.Top = y;
        }
        // #################################################################################################
    }
}









// NOTES TODO:

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
//this.SizeGripStyle = SizeGripStyle.Show;
//this.TopLevel = true;

/* Cool
this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
this.ShowInTaskbar = true; // ALT + TAB
this.MinimizeBox = true;
this.MaximizeBox = true;
*/

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

