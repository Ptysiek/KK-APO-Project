using System;
using System.Windows.Forms;
using System.Drawing;


namespace KK17413_APO
{
    class ImagePage
    {
        public ImagePage(   Form form,
                            FlowLayoutPanel containerMenu,
                            TableLayoutPanel containerWorkspace,
                            FlowLayoutPanel containerImage,
                            FlowLayoutPanel containerInfo,
                           // ListBox containerBOX,
                            MenuStrip menuStrip,
                            ToolStripMenuItem file_tsmi,
                            ToolStripMenuItem histogram_tsmi,

                            PictureBox picture,
                            TextBox imageScale_tb
                        )
        {
            this.form = form;
            this.containerMenu = containerMenu;
            this.containerWorkspace = containerWorkspace;
            this.containerImage = containerImage;
            this.containerInfo = containerInfo;

           // this.containerBOX = containerBOX;
            this.menuStrip = menuStrip;
            this.file_tsmi = file_tsmi;
            this.histogram_tsmi = histogram_tsmi;

            this.picture = picture;
            this.imageScale_tb = imageScale_tb;

          //  this.form.Resize += new EventHandler(imagePage_Resize);

            // this.picture.MouseHover
            this.picture.MouseWheel += new MouseEventHandler(imagePage_Resize);

            this.histogram_tsmi.Click += new EventHandler(histogram_tsmi_Click);

        }
        public void histogram_tsmi_Click(object sender, EventArgs e)
        {            
            TogleFormExpandWindow();
        }

        bool expendWindow = false;

        private void TogleFormExpandWindow()
        {
            expendWindow = !expendWindow;

            if (expendWindow)
            {
                
                //form.Width
                int tmpW = containerImage.Width;
                int tmpH = containerImage.Height;

                //containerMenu.Dock = DockStyle.None;
                //containerImage.Dock = DockStyle.None;
                //containerHistogram.Dock = DockStyle.None;

                // Resize Form:
                form.Width = containerImage.Width + containerInfo.Width;

                // Attach containers:
                //containerImage.Dock = DockStyle.Fill;
                //containerInfo.Dock = DockStyle.Right;
                //containerMenu.Dock = DockStyle.Top;

                containerWorkspace.ColumnCount = 2;
                containerWorkspace.RowCount = 1;

                containerInfo.Visible = true;
                //containerInfo.Dock = DockStyle.Fill;

                containerImage.Width = tmpW;
                //containerImage.Height = tmpH;


            }
            else
            {
                
                //containerInfo.Dock = DockStyle.None;

                containerInfo.Visible = false;
                containerWorkspace.ColumnCount = 1;
                containerWorkspace.RowCount = 1;


                form.Width = containerImage.Width;
                //form.Height = containerImage.Height;

                //containerImage.Dock = DockStyle.Fill;


                // Calculate the TaskBar Height, for better image position.
                int PSBH = Screen.PrimaryScreen.Bounds.Height;
                int TaskBarHeight = PSBH - Screen.PrimaryScreen.WorkingArea.Height;

                //form.Size = new Size(file.Size.Width + 100, file.Size.Height + TaskBarHeight);
                form.Size = new Size(picture.Size.Width + 16, picture.Size.Height + TaskBarHeight + menuStrip.Height);
            }
        }

        private void ResizeForm()
        {
            /*
                containerImage
            
            */

        }



        private Form form;
        private FlowLayoutPanel containerMenu;
        private TableLayoutPanel containerWorkspace;
        private FlowLayoutPanel containerImage;
        private FlowLayoutPanel containerInfo;

       ///private ListBox containerBOX;
        private MenuStrip menuStrip;
        private ToolStripMenuItem file_tsmi;
        private ToolStripMenuItem histogram_tsmi;

        private PictureBox picture;
        private TextBox imageScale_tb;





        public void imagePage_Resize(object sender, MouseEventArgs e)
        {
            bool positive = (e.Delta > 0)?  true : false;

            ResizePicture(positive);            

            return;
        }

        private void ResizePicture(bool positive)
        {

            // Take value from: imageScale_tb:
            string text = "";
            string tmp = imageScale_tb.Text;

            for (int i = 0; i < tmp.Length; ++i)
                if (tmp[i] == '%') break;
                else text += tmp[i];

            float valueF = float.Parse(text);
            int value = Convert.ToInt32(valueF);


            // Określ szukaną wartość w %:
            // Określ czy zwiększamy czy zmniejszamy: [mam Positive]
            if (valueF > 490 && positive)
            {
                value = 500;
            }
            else if (valueF < 20 && !positive)
            {
                value = 10;
            }
            else
            {
                if (positive)
                {
                    int tmpvalue = value;
                    tmpvalue /= 10;
                    tmpvalue *= 10;

                    if (tmpvalue == valueF)
                    {
                        value += 10; // gotowe value
                    }
                    else
                    {
                        value = tmpvalue + 10; // gotowe value
                    }

                }
                else
                {
                    int tmpvalue = value;
                    tmpvalue /= 10;
                    tmpvalue *= 10;

                    if (tmpvalue == valueF)
                    {
                        value -= 10; // gotowe value
                    }
                    else
                    {
                        value = tmpvalue; // gotowe value
                    }
                }
            }

            // Oblicz wyszukany procent z oryginalnych wymiarów 
            /*
                Current Picture size:
                    picture.ClientSize.Width
                    picture.ClientSize.Height

                Oryginal Picture size:
                    picture.Image.Width
                    picture.Image.Height
            */
            int sizeW = value * picture.Image.Width / 100;
            int sizeH = value * picture.Image.Height / 100;

            // Zmień wartość wymiarów obrazka:
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            picture.ClientSize = new Size(sizeW, sizeH);

            picture.Left = (form.ClientSize.Width - picture.Width) / 2;
           // picture.Top = (form.ClientSize.Height - picture.Height + menuStrip.Height) / 2;
            picture.Top = (form.ClientSize.Height - picture.Height) / 2;

            // Zmień: imageScale_tb:
            imageScale_tb.Text = value.ToString() + "%";
            imageScale_tb.TabIndex = 0;
        }


    }
}
