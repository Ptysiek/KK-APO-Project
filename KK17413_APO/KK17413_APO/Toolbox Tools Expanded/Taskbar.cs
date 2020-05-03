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

        public Button minimize;
        public Button maximize;
        public Button close;
        
        // Take position:
        private int xMouseDown;
        private int yMouseDown;

        // Take action:
        private bool mousePressed = false;


        // #################################################################################################   
        public void AssignEventHandlers()
        {
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
        => Title.Width = (this.Width) - this.Height;

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


    // ##########################################################################################################################
    // ##########################################################################################################################
    #region Taskbar_Builder
    public class Taskbar_Builder
    {
        public static Taskbar GetResult(Form dragControl)
        {
            Taskbar result = new Taskbar()
            {
                dragControl = dragControl,
                BorderStyle = BorderStyle.None,
                BackColor = Color.LightGray,
                Dock = DockStyle.Top,
                Height = 32
            };

            result.Title = Get_Title(result.Height);
            result.Icon = Get_Icon(result.Height);

            result.minimize = Get_Button(result.Height);
            result.maximize = Get_Button(result.Height);
            result.close = Get_CloseButton(result.Height);

            //result.Controls.Add(result.minimize);
            //result.Controls.Add(result.maximize);
            result.Controls.Add(result.close);
            result.Controls.Add(result.Title);
            result.Controls.Add(result.Icon);            

            result.AssignEventHandlers();

            return result;
        }

        // ########################################################################################################
        private static Label Get_Title(int resultHeight)
        {
            Label Title = new Label();
            Title.Text = "[Default Taskbar Title]";
            Title.Top = (resultHeight / 8) + (resultHeight / 8);
            Title.Height -= Title.Height / 8;
            Title.Left = resultHeight;
            Title.AutoSize = true;
            Title.AutoEllipsis = true;
            Title.Font = new Font("Corbel", (resultHeight / 4) + 2, Title.Font.Style);
            return Title;
        }                
        private static PictureBox Get_Icon(int resultHeight)
        {
            PictureBox Icon = new PictureBox();
            int IconScale = 6;
            int IconSize = resultHeight - IconScale;
            Icon.Left = IconScale / 2;
            Icon.Top = IconScale / 2;
            Icon.Width = IconSize;
            Icon.Height = IconSize;
            Icon.SizeMode = PictureBoxSizeMode.Zoom;
            Icon.ClientSize = new Size(IconSize, IconSize);
            return Icon;
        }
        private static Button Get_CloseButton(int resultHeight)
        {
            Button close = Get_Button(resultHeight);
            close.TextAlign = ContentAlignment.MiddleCenter;
            close.Text = "X";
            close.BackColor = Color.IndianRed;
            close.TabStop = false;
            close.FlatStyle = FlatStyle.Flat;
            close.UseVisualStyleBackColor = false;
            return close;
        }
        private static Button Get_Button(int resultHeight)
        {
            return new Button() {
                Dock = DockStyle.Right,
                Width = resultHeight
            };
        }
    }
    #endregion
    // ##########################################################################################################################
    // ##########################################################################################################################
}
