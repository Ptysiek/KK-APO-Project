using KK17413_APO.Toolbox_Tools_Expanded;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KK17413_APO.Panels_Expanded
{
    [System.ComponentModel.DesignerCategory("")]
    public class HistogramPanel : Panel
    {
        public Histogram Histogram;

        public Label pipe_0;
        public Label pipe_64;
        public Label pipe_128;
        public Label pipe_192;
        public Label pipe_255;

        public Label MostValue;
        public Panel MaxValueColor;

        public Label LeastValue;
        public Panel MinValueColor;


        public void ReloadColorSet()
        {
            Histogram.BackColor = ProgramSettings.ColorManager.GetValue("bgHistogram");
        }



        public void ReloadHistogram(List<int> data)
        {
            Histogram.ReloadHistogram(data);


        }

        public void Configure_PipePosition()
        {
            int topVal = Histogram.Height;

            pipe_0.Left = 0;
            pipe_64.Left = Histogram.Width / 4 - (pipe_64.Width / 2) - 6;
            pipe_128.Left = Histogram.Width / 2 - (pipe_128.Width / 2) - 6;
            pipe_192.Left = Histogram.Width / 2 + Histogram.Width / 4 - (pipe_192.Width / 2) - 6;
            pipe_255.Left = Histogram.Width - (pipe_255.Width / 2) - 6;
            pipe_0.Top = topVal;
            pipe_64.Top = topVal;
            pipe_128.Top = topVal;
            pipe_192.Top = topVal;
            pipe_255.Top = topVal;


            MostValue.Top = topVal + 30;
            LeastValue.Top = MostValue.Top + MostValue.Height;
        }
    }
    


    // ##########################################################################################################################
    // ##########################################################################################################################
    #region HistogramPanel_Builder
    class HistogramPanel_Builder
    {
        public static HistogramPanel GetResult(Color ForeColor)
        {
            HistogramPanel result = new HistogramPanel()
            {
                pipe_0 = Get_pipe("0"),
                pipe_64 = Get_pipe("64"),
                pipe_128 = Get_pipe("128"),
                pipe_192 = Get_pipe("192"),
                pipe_255 = Get_pipe("255")
            };

            
            result.Histogram = new Histogram(ForeColor);

            result.MaxValueColor = new Panel();
            result.MinValueColor = new Panel();

            result.Height = result.Histogram.Height;
            result.Dock = DockStyle.Fill;

            result.MostValue = new Label
            {
                Text = "Most Value: ",
                AutoEllipsis = false,
                AutoSize = true
            };

            result.LeastValue = new Label
            {
                Text = "Least Value: ",
                AutoEllipsis = false,
                AutoSize = true
            };

            result.Controls.AddRange(new Control[]
            {
                result.Histogram,
                result.MostValue,
                result.LeastValue
            });

            result.Controls.AddRange(new Control[]{
                result.pipe_0,
                result.pipe_64,
                result.pipe_128,
                result.pipe_192,
                result.pipe_255
            });

            result.Configure_PipePosition();

            return result;
        }


        private static Label Get_pipe(string value)
        {
            return new Label()
            {
                Text = value,
                AutoSize = true,
                AutoEllipsis = false
            };
        }
    }
    #endregion
    // ##########################################################################################################################
    // ##########################################################################################################################
}
