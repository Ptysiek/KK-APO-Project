using System;
using System.Windows.Forms;
using System.Drawing;


namespace KK17413_APO
{
    class ImagePage
    {
        public ImagePage(   Form form,
                            ListBox containerBOX,
                            MenuStrip menuStrip, 
                            PictureBox picture,
                            TextBox imageScale_tb
                        )
        {
            this.form = form;
            this.containerBOX = containerBOX;
            this.menuStrip = menuStrip;
            this.picture = picture;
            this.imageScale_tb = imageScale_tb;

          //  this.form.Resize += new EventHandler(imagePage_Resize);

            // this.picture.MouseHover
            this.picture.MouseWheel += new MouseEventHandler(imagePage_Resize);

        }


        private Form form;
        private ListBox containerBOX;
        private MenuStrip menuStrip;

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
            picture.Top = (form.ClientSize.Height - picture.Height) / 2;

            // Zmień: imageScale_tb:
            imageScale_tb.Text = value.ToString() + "%";
        }


    }
}
