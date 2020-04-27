
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KK17413_APO.Toolbox_Tools_Expanded
{
    [System.ComponentModel.DesignerCategory("")]
    public class Histogram : Panel
    {

        List<Bar> bars;
        FlowLayoutPanel container;



        public Histogram(Color ForeColor)
        {
            this.Size = new Size(689, 281);
            this.Dock = DockStyle.None;
            //this.BorderStyle = BorderStyle.FixedSingle;

            container = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = false,
                WrapContents = false
            };

            this.Controls.Add(container);


            // One For the left margin:            
            container.Controls.Add(new Bar
            {
                Width = 1,
                Height = container.Height - 2,
                //BackColor = Color.White,
                //ForeColor = Color.White,
                Enabled = false
                //Visible = false
            });


            bars = new List<Bar>();
            var rand = new Random();

            for (int i = 0; i < 256; ++i)
            {
                bars.Add(new Bar());
            }

            foreach (var bar in bars)
            {
                bar.OriginalWidth = 1;
                bar.OriginalHeight = container.Height - 2;
                bar.ForeColor = ForeColor;

                bar.Width = bar.OriginalWidth;
                bar.Height = bar.OriginalHeight;

                //bar.MaxValue = bar.OriginalHeight;
                //bar.Value = rand.Next(bar.Height);


                //bar.colorPanel.Height = rand.Next(bar.Height);
                container.Controls.Add(bar);
            }


            //flowLayoutPanel1.ClientSize = new Size(1, 1);
            //flowLayoutPanel1.ClientSize


            // One For the right margin:   
            Bar rightMargin = new Bar
            {
                Width = 1,
                Height = container.Height - 2,
                //BackColor = Color.White,
                //ForeColor = Color.White,
                Enabled = false
            };
            container.Controls.Add(rightMargin);

            this.Width = rightMargin.Left + rightMargin.Width + 2;
            //rightMargin.Visible = false;
        }


        public void ReloadHistogram(Bitmap bitmap)
        {
            List<List<int>> data = CalculateHistogram(bitmap);

            int i = 0;
            int maxval = 0;



            foreach (var value in data[1])
            {
                if (value > maxval)
                    maxval = value;
            }
            foreach (var bar in bars)
            {
                bar.MaxValue = maxval;
                bar.Value = data[1][i];

                //bars[i].Value = value;
                //chart.Series["Red"].Points.AddXY(i, value);
                ++i;
            }
        }
        public void ReloadHistogram(List<int> data)
        {
            //List<List<int>> data = CalculateHistogram(bitmap);

            int i = 0;
            int maxval = 0;



            foreach (var value in data)
            {
                if (value > maxval)
                    maxval = value;                
            }
            foreach (var bar in bars)
            {
                bar.MaxValue = maxval;
                bar.Value = data[i];

                //bars[i].Value = value;
                //chart.Series["Red"].Points.AddXY(i, value);
                ++i;
            }
        }

        private List<List<int>> CalculateHistogram(Bitmap bitmap)
        {
            List<List<int>> result = new List<List<int>>
            {
                new List<int>(256),
                new List<int>(256),
                new List<int>(256),
                new List<int>(256)
            };


            for (int i = 0; i < result.Capacity; ++i)
                for (int index = 0; index < result[i].Capacity; ++index)
                    result[i].Add(0);


            for (int h = 0; h < bitmap.Height; ++h)
            {
                for (int w = 0; w < bitmap.Width; ++w)
                {
                    result[0][bitmap.GetPixel(w, h).A] += 1;
                    result[1][bitmap.GetPixel(w, h).R] += 1;
                    result[2][bitmap.GetPixel(w, h).G] += 1;
                    result[3][bitmap.GetPixel(w, h).B] += 1;
                }
            }

            return result;
        }
    }




    public class Bar : Panel
    {
        public Bar()
        {
            //this.BackColor = Color.Red;
            this.Padding = new Padding(0);
            this.Margin = new Padding(1, 0, 0, 0);

            colorPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Padding = new Padding(0),
                Margin = new Padding(1, 0, 0, 0)
            };

            this.Controls.Add(colorPanel);
        }

        public override Color ForeColor
        {
            get => colorPanel.BackColor;
            set => colorPanel.BackColor = value;
        }
        public new int Height
        {
            get => base.Height;
            set
            {
                base.Height = value;
                CalculateColorPanelHeight();
            }
        }

        public int OriginalHeight;
        public int OriginalWidth;

        public int MaxValue
        {
            get => maxvalue;
            set
            {
                maxvalue = value;
                CalculateColorPanelHeight();
            }
        }
        public int Value
        {
            get => value;
            set
            {
                this.value = value;
                CalculateColorPanelHeight();
            }
        }

        public Panel colorPanel;
        private int maxvalue;
        private int value;


        private void CalculateColorPanelHeight()
        {
            if (maxvalue < 1) maxvalue = 1;
            if (value > maxvalue) value = maxvalue;
            if (value < 0) value = 0;

            /** In this context:                 
                this.Height          is CONSTANT => 100%
                colorPanel.Height    is UNKNOWN => x

                Proportion:            
                              this.Height            MaxValue
                            ---------------    =    -----------
                           colorPanel.Height          Value
            **/
            colorPanel.Height = base.Height * value / maxvalue;
        }
    }
}
























/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Windows.Forms;
//using System.Windows.Media;

namespace KK17413_APO.Toolbox_Tools_Expanded
{

    [System.ComponentModel.DesignerCategory("")]
    public class Histogram : Panel
    {

        Chart chart;

        public Histogram()
        {
            chart = new Chart();

            ChartArea chartArea = new ChartArea();
            chartArea.AxisY.ArrowStyle = AxisArrowStyle.None;
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Maximum = 255;

            Legend legend = new Legend()
            {
                Enabled = false                
            };
            Series Red = new Series()
            {
                Name = "Red",
                Color = Color.Red,
                IsVisibleInLegend = false,
                //ChartType = SeriesChartType.Column,
                MarkerStyle = MarkerStyle.None,
                BorderWidth = 1,
                BorderColor = Color.White
            };
            

            //chart.Dock = DockStyle.Fill;
            chart.MinimumSize = new Size(500, 400);
            chart.Size = new Size(600, 455);
            

            chart.Legends.Add(legend);
            chart.ChartAreas.Add(chartArea);

            chart.Series.Add(Red);
            //chart.Series["tak?"].Points.AddXY(50, 100);



            this.Controls.Add(chart);



            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;


        }

        public void ReloadHistogram(Bitmap bitmap)
        {
            List<List<int>> data = CalculateHistogram(bitmap);

            int i = 0;

            foreach (var value in data[1])
            {
                chart.Series["Red"].Points.AddXY(i, value);
                ++i;
            }

        }

        private List<List<int>> CalculateHistogram(Bitmap bitmap)
        {
            List<List<int>> result = new List<List<int>>
            {
                new List<int>(257),
                new List<int>(257),
                new List<int>(257),
                new List<int>(257)
            };


            for (int i = 0; i < result.Capacity; ++i)
                for (int index = 0; index < result[i].Capacity; ++index)
                    result[i].Add(0);


            for (int h = 0; h < bitmap.Height; ++h){
                for (int w = 0; w < bitmap.Width; ++w){
                    result[0][bitmap.GetPixel(w, h).A] += 1;
                    result[1][bitmap.GetPixel(w, h).R] += 1;
                    result[2][bitmap.GetPixel(w, h).G] += 1;
                    result[3][bitmap.GetPixel(w, h).B] += 1;
                }
            }

            return result;
        }
    }
}
//*/
