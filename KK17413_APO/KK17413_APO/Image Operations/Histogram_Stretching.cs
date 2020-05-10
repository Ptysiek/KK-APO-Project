using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using KK17413_APO.Toolbox_Tools_Expanded;

namespace KK17413_APO.Image_Operations
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class Histogram_Stretching : Form
    {
        private int[] red = null;
        private int[] green = null;
        private int[] blue = null;

        public Panel topContainer;

        public Label title;
        public Button accept_Button;
        public Button cancel_Button;


        public Histogram his_Before;
        public Histogram his_After;

        public PictureBox pic_Before;
        public PictureBox pic_After;

        public Bitmap before;
        public Bitmap after;

        public Histogram_Stretching()
        {
            // --------------------------------------------------------------------------------------
            topContainer = new Panel() {
                Dock = DockStyle.Top,
                BackColor = Color.Red
            };

            title = new Label()
            {
                Dock = DockStyle.Top
            };
            accept_Button = new Button()
            {
                Text = "Confirm",
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 35,
                Width = 70
            };
            accept_Button.Left = 10;

            cancel_Button = new Button()
            {
                Text = "Cancel",
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 35,
                Width = 70
            };
            cancel_Button.Left = accept_Button.Left + accept_Button.Width + accept_Button.Left;

            topContainer.Height = title.Height + 50 + accept_Button.Height;
            accept_Button.Top = topContainer.Height - accept_Button.Height - accept_Button.Left;
            cancel_Button.Top = topContainer.Height - accept_Button.Height - accept_Button.Left;

            // --------------------------------------------------------------------------------------

            his_Before = new Histogram(Color.White);
            his_After = new Histogram(Color.White);

            pic_Before = new PictureBox();
            pic_After = new PictureBox();



            this.Width = his_Before.Width + 50 + his_After.Width;
            this.Height = topContainer.Height + 50 + his_Before.Height * 2;
            this.Show();


            this.Controls.Add(topContainer);
            topContainer.Controls.Add(title);
            topContainer.Controls.Add(accept_Button);
            topContainer.Controls.Add(cancel_Button);

            this.Controls.Add(his_Before);
            this.Controls.Add(his_After);
            this.Controls.Add(pic_Before);
            this.Controls.Add(pic_After);

            //wczytaj_Click();
            //GetResult();
        }


        public void AssignData()
        {
            
        }














        PictureBox picture = new PictureBox();
        /// <summary>
        /// Wczytaj obraz
        /// </summary>
        public void wczytaj_Click()
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Otwórz obraz";
            dlg.Filter = "Obrazy (*.jpg;*.gif;*.png;*.bmp)|*.jpg;*.gif;*.png;*.bmp";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //Wczytaj plik
                picture.Image = new Bitmap(dlg.OpenFile());
                picture.Height = picture.Image.Height;
                picture.Width = picture.Image.Width;
                //this.ClientSize = new Size(Math.Max(picture.Width + 30, 345), picture.Height + 155);

                //Oblicz histogram
                red = new int[256];
                green = new int[256];
                blue = new int[256];
                for (int x = 0; x < picture.Width; x++)
                {
                    for (int y = 0; y < picture.Height; y++)
                    {
                        Color pixel = ((Bitmap)picture.Image).GetPixel(x, y);
                        red[pixel.R]++;
                        green[pixel.G]++;
                        blue[pixel.B]++;
                    }
                }
            }
            dlg.Dispose();
        }

        /// <summary>
        /// Oblicza tablice LUT dla histogramu podanej składowej
        /// </summary>
        /// <param name="values">histogram dla składowej</param>
        /// <returns>tablica LUT do rozciagniecia histogramu</returns>
        public int[] calculateLUT(int[] values)
        {
            //poszukaj wartości minimalnej
            int minValue = 0;
            for (int i = 0; i < 256; i++)
            {
                if (values[i] != 0)
                {
                    minValue = i;
                    break;
                }
            }

            //poszukaj wartości maksymalnej
            int maxValue = 255;
            for (int i = 255; i >= 0; i--)
            {
                if (values[i] != 0)
                {
                    maxValue = i;
                    break;
                }
            }

            //przygotuj tablice zgodnie ze wzorem
            int[] result = new int[256];
            double a = 255.0 / (maxValue - minValue);
            for (int i = 0; i < 256; i++)
            {
                result[i] = (int)(a * (i - minValue));
            }

            return result;
        }

        /// <summary>
        /// Rozciaganie histogramu
        public void GetResult()
        {
            //Sprawdz czy wczytano obraz
            if (red == null)
            {
                return;
            }

            //Tablice LUT dla skladowych
            int[] LUTred = calculateLUT(red);
            int[] LUTgreen = calculateLUT(green);
            int[] LUTblue = calculateLUT(blue);

            //Przetworz obraz i oblicz nowy histogram
            red = new int[256];
            green = new int[256];
            blue = new int[256];
            Bitmap oldBitmap = (Bitmap)picture.Image;
            Bitmap newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height, PixelFormat.Format24bppRgb);
            for (int x = 0; x < picture.Width; x++)
            {
                for (int y = 0; y < picture.Height; y++)
                {
                    Color pixel = oldBitmap.GetPixel(x, y);
                    Color newPixel = Color.FromArgb(LUTred[pixel.R], LUTgreen[pixel.G], LUTblue[pixel.B]);
                    newBitmap.SetPixel(x, y, newPixel);
                    red[newPixel.R]++;
                    green[newPixel.G]++;
                    blue[newPixel.B]++;
                }
            }
            picture.Image = newBitmap;

            //Wyswietl histogram na wykresie




        }

    }
}