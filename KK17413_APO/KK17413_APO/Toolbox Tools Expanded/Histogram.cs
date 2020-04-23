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
            var dataPointSeries = new Series
            {
                Name = "chartName",
                Color = Color.Blue,
                IsVisibleInLegend = false,
                ChartType = SeriesChartType.Column,
                MarkerStyle = MarkerStyle.None,
                BorderWidth = 1,
                BorderColor = Color.White


            };

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




            chart.Dock = DockStyle.Fill;

            chart.Legends.Add(legend);
            chart.ChartAreas.Add(chartArea);

            chart.Series.Add(Red);
            //chart.Series["tak?"].Points.AddXY(50, 100);



            this.Controls.Add(chart);
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
