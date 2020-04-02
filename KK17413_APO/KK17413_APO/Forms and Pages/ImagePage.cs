using System;
using System.Windows.Forms;
using System.Drawing;


namespace KK17413_APO
{
    class ImagePage
    {

        private Form form;
        private FlowLayoutPanel containerMenu;
        private SplitContainer containerWorkspace;
        private SplitterPanel imagePanel;
        private SplitterPanel infoPanel;

        private MenuStrip menuStrip;
        private ToolStripMenuItem file_tsmi;
        private ToolStripMenuItem histogram_tsmi;

        private PictureBox picture;
        private TextBox imageScale_tb;

        TreeView treeView;


        private bool collapsedInfoPanel
        {
            get { return containerWorkspace.Panel2Collapsed; }
            set { containerWorkspace.Panel2Collapsed = value; }
        }




        public ImagePage(   Form form,
                            FlowLayoutPanel containerMenu,
                            SplitContainer containerWorkspace,

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
            imagePanel = this.containerWorkspace.Panel1;
            infoPanel = this.containerWorkspace.Panel2;

            this.menuStrip = menuStrip;
            this.file_tsmi = file_tsmi;
            this.histogram_tsmi = histogram_tsmi;

            this.picture = picture;
            this.imageScale_tb = imageScale_tb;

            this.picture.MouseWheel += new MouseEventHandler(imagePage_Resize);
            this.histogram_tsmi.Click += new EventHandler(histogram_tsmi_Click);
            this.form.Resize += new EventHandler(form_Resize);

            // InfoPage
            this.treeView = new TreeView()
            { 
                Location = new Point(0, 100)
            };
            
            //treeView.Location = new Point(0,100);
           // this.containerInfo.Controls.Add(this.treeView);

            TreeNode Histogram = new TreeNode("Histogram");
            TreeNode Info = new TreeNode("Info");

            treeView.Nodes.AddRange(new TreeNode[] {
                Histogram,
                Info 
            });

            infoPanel.Controls.Add(treeView);
            //treeView.Top = 10;
            //treeView.Dock = DockStyle.Bottom;
            //treeView.Parent = this.containerInfo.Controls;


            //ResizeForm();

            this.form.Show();
        }

        private void AssignImage(string filename)
        {
            picture.Image = new Bitmap(filename);

            // Calculate the TaskBar Height, for better image position.
            int boundsH = Screen.PrimaryScreen.Bounds.Height;
            int workingAreaH = Screen.PrimaryScreen.WorkingArea.Height;
            int TaskBarH = boundsH - workingAreaH;


            form.Size = new Size(picture.Image.Width + 20, picture.Image.Height + TaskBarH + containerMenu.Height);
        }


        // ##########################################################################
        public void form_Resize(object sender, EventArgs e)
        {
            //ResizeForm();
        }


        public void histogram_tsmi_Click(object sender, EventArgs e)
        {
            collapsedInfoPanel = !collapsedInfoPanel;
        }




        //TogleFormExpandWindow();
        public void imagePage_Resize(object sender, MouseEventArgs e)
        {
            bool positive = (e.Delta > 0) ? true : false;

            //ResizePicture(positive, e.Location);            

            return;
        }

        // ##########################################################################
        //bool _expendWindow;


        /*
        private void TogleFormExpandWindow()
        {
            if (!initialized) return;

            expendWindow = !expendWindow;

            if (expendWindow)
            {                
                //form.Width
                int tmpW = containerImage.Width;
                int tmpH = containerImage.Height;

                lastKnownContainerImage_W = containerImage.Width;
                lastKnownContainerImage_H = containerImage.Height;

                containerInfo.Width = 30 * containerImage.Width / 70;

                // Resize Form:
                form.Width = containerImage.Width + containerInfo.Width;

                containerWorkspace.ColumnCount = 2;
                containerWorkspace.RowCount = 1;

                containerInfo.Visible = true;

                containerImage.Width = tmpW;
            }
            else
            {
                containerInfo.Visible = false;

                containerWorkspace.ColumnCount = 1;
                containerWorkspace.RowCount = 1;

                // Calculate the TaskBar Height, for better image position.
                //int PSBH = Screen.PrimaryScreen.Bounds.Height;
                //int TaskBarHeight = PSBH - Screen.PrimaryScreen.WorkingArea.Height;

                //form.Size = new Size(file.Size.Width + 100, file.Size.Height + TaskBarHeight);
                //form.Size = new Size(lastKnownContainerImage_W, lastKnownContainerImage_H + containerMenu.Height);
                form.Width = lastKnownContainerImage_W + 20;
            }
            ResizeForm();
            //containerInfo.Width = 0;
        }

        int lastKnownContainerImage_W = 0;
        int lastKnownContainerImage_H = 0;

        private void ResizeForm()
        {
            containerImage.SuspendLayout();

            if (containerImage.Width > form.Width)
                containerImage.Width = form.Width - 20;

            picture.Left = (containerImage.Width - picture.Width) / 2;
            picture.Top = (containerImage.Height - picture.Height + containerMenu.Height) / 2;
            
            containerImage.PerformLayout();
        }
        */



        
        private void ResizePicture(bool positive, Point mouseLocation)
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

            //containerImage.SuspendLayout();
            // Zmień wartość wymiarów obrazka:
            picture.ClientSize = new Size(sizeW, sizeH);

            //form.clie

            // picture.Left = (form.ClientSize.Width - picture.Width) / 2;
            // picture.Top = (form.ClientSize.Height - picture.Height + menuStrip.Height) / 2;
            // picture.Top = (form.ClientSize.Height - picture.Height) / 2;




            //picture.Left = (containerImage.Width - picture.Width) / 2;
            //picture.Top = (containerImage.Height - picture.Height + 20) / 2;





            //picture.Dock = DockStyle.Fill;
            //picture.Left = 50;

            //containerImage.PerformLayout();

            // Zmień: imageScale_tb:
            imageScale_tb.Text = value.ToString() + "%";
        }


    }
}
