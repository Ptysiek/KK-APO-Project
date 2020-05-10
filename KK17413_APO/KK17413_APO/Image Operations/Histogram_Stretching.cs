using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using KK17413_APO.Toolbox_Tools_Expanded;
using KK17413_APO.Data_Structures;
using KK17413_APO.Panels_Expanded;
using System.Collections.Generic;

namespace KK17413_APO.Image_Operations
{

    [System.ComponentModel.DesignerCategory("")]
    public class Histogram_Stretching
    {
        public static ImageData GetResult(ImageData before)
        {
            int[] LUTred = calculateLUT(before.data_R);
            int[] LUTgreen = calculateLUT(before.data_G);
            int[] LUTblue = calculateLUT(before.data_B);

            HistogramData general = new HistogramData();
            HistogramData red = new HistogramData();
            HistogramData green = new HistogramData();
            HistogramData blue = new HistogramData();

            Bitmap oldBitmap = before.bitmap;
            Bitmap newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height);
            //Bitmap newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height, PixelFormat.Format24bppRgb);


            for (int x = 0; x < oldBitmap.Width; x++)
            {
                for (int y = 0; y < oldBitmap.Height; y++)
                {
                    Color pixel = oldBitmap.GetPixel(x, y);
                    Color newPixel = Color.FromArgb(LUTred[pixel.R], LUTgreen[pixel.G], LUTblue[pixel.B]);

                    newBitmap.SetPixel(x, y, newPixel);

                    general.SumUp(newPixel.R);
                    general.SumUp(newPixel.G);
                    general.SumUp(newPixel.B);

                    red.SumUp(newPixel.R);
                    green.SumUp(newPixel.G);
                    blue.SumUp(newPixel.B);
                }
            }
            general.SetLeast();
            red.SetLeast();
            green.SetLeast();
            blue.SetLeast();

            ImageData after = new ImageData(newBitmap, before.ID);
            after.data = general;
            after.data_A = before.data_A;
            after.data_R = red;
            after.data_G = green;
            after.data_B = blue;

            return after;
        }

        private static int[] calculateLUT(HistogramData data)
        {
            //poszukaj wartości minimalnej
            int minValue = data.minValue;

            //poszukaj wartości maksymalnej
            int maxValue = data.maxValue;

            //przygotuj tablice zgodnie ze wzorem
            int[] result = new int[256];
            double a = 255.0 / (maxValue - minValue);
            for (int i = 0; i < 256; i++)
            {
                result[i] = (int)(a * (i - minValue));
            }

            return result;
        }

    }















    /*
    [System.ComponentModel.DesignerCategory("")]
    public class Histogram_Stretching : Form
    {
        private int[] red = null;
        private int[] green = null;
        private int[] blue = null;

        public Panel topContainer;
        public FlowLayoutPanel bottomContainer;
        public Panel imageContainer;
        public Panel histogramContainer;

        public Label title;
        public Button accept_Button;
        public Button cancel_Button;


        public Histogram his_Before;
        public Histogram his_After;

        //public ImageWorkspace pic_Before;
       // public ImageWorkspace pic_After;

        public PictureBox pic_Before;
        public PictureBox pic_After;

        public HistogramData data_Before; 
        public HistogramData data_After; 

        public Bitmap before;
        public Bitmap after;


        public Histogram_Stretching(Bitmap bitmap, HistogramData data)
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
            imageContainer = new Panel()
            {
                //Dock = DockStyle.Top,
                Width = this.Width,
                BackColor = Color.Blue
            };


            //pic_Before = ImageWorkspace_Builder.GetResult();
            // pic_After = ImageWorkspace_Builder.GetResult();

            //pic_Before.AssignImage(bitmap);
            //pic_Before.RelocatePicture();

            pic_Before = new PictureBox();
            pic_After = new PictureBox();

            pic_Before.Image = bitmap;

            //infoRightWingPanel.LoadInfoPanel(bitmap, filename);
            //infoRightWingPanel.histogramTabControl.AssignBitmap(bitmap, filename);
            //HistogramCalculatePermision = true;



            //imageContainer.Height = pic_Before.picture.Image.Height;
            imageContainer.Height = pic_Before.Image.Height;

            pic_Before.Dock = DockStyle.Left;
            pic_Before.Top = topContainer.Height;

            pic_After.Top = topContainer.Height;
            pic_After.Dock = DockStyle.Right;

            

            // --------------------------------------------------------------------------------------
            his_Before = new Histogram(Color.White);
            his_After = new Histogram(Color.White);

            histogramContainer = new Panel()
            {
                //Dock = DockStyle.Fill,
                Height = his_Before.Height,
                BackColor = Color.Yellow
            };
            his_Before.Dock = DockStyle.Left;
            his_After.Dock = DockStyle.Right;

            his_Before.ReloadHistogram(data.data);

            // --------------------------------------------------------------------------------------
            bottomContainer = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.Black
            };


            // --------------------------------------------------------------------------------------
            this.Width = his_Before.Width + 50 + his_After.Width;
            this.Height = topContainer.Height + 50 + his_Before.Height * 2;


            this.Controls.Add(bottomContainer);
            bottomContainer.Controls.Add(imageContainer);
            bottomContainer.Controls.Add(histogramContainer);


            //this.Controls.Add(histogramContainer);
            histogramContainer.Controls.Add(his_After);
            histogramContainer.Controls.Add(his_Before);

            //this.Controls.Add(imageContainer);
            imageContainer.Controls.Add(pic_Before);
            imageContainer.Controls.Add(pic_After);

            this.Controls.Add(topContainer);
            topContainer.Controls.Add(title);
            topContainer.Controls.Add(accept_Button);
            topContainer.Controls.Add(cancel_Button);


            //wczytaj_Click();
            //GetResult();


            imageContainer.Width = bottomContainer.Width - 16;
            histogramContainer.Width = bottomContainer.Width - 16;
            this.Show();

            pic_Before.Height = imageContainer.Height;
            pic_After.Height = imageContainer.Height;

            //pic_Before.RelocatePicture();

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

    */
}
 