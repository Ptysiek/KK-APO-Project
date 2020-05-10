using KK17413_APO.Data_Structures;
using KK17413_APO.Toolbox_Tools_Expanded;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


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
        public Label MostValue_Index;
        public Label MostValue_Quantity;
        public Panel MostValue_Color;

        public Label LeastValue;
        public Label LeastValue_Index;
        public Label LeastValue_Quantity;
        public Panel LeastValue_Color;

        public Label MinValue;
        public Label MaxValue;


        public void ReloadColorSet()
        {
            Histogram.BackColor = ProgramSettings.ColorManager.GetValue("bgHistogram");
        }



        public void ReloadHistogram(HistogramData fullData)
        {
            Histogram.ReloadHistogram(fullData.data);

            MostValue_Index.Text = "    index:  " + fullData.mostValueIndex;
            LeastValue_Index.Text = "    index:  " + fullData.leastValueIndex;

            MostValue_Quantity.Text = "    quantity:  " + fullData.mostValueCounter + " px";
            LeastValue_Quantity.Text = "    quantity:  " + fullData.leastValueCounter + " px";

            if (Histogram.BarColor == Color.White)
            {
                MostValue_Color.BackColor = Color.FromArgb(fullData.mostValueIndex, fullData.mostValueIndex, fullData.mostValueIndex);
                LeastValue_Color.BackColor = Color.FromArgb(fullData.leastValueIndex, fullData.leastValueIndex, fullData.leastValueIndex);
            }
            else if (Histogram.BarColor == Color.Red)
            {
                MostValue_Color.BackColor = Color.FromArgb(fullData.mostValueIndex, 0, 0);
                LeastValue_Color.BackColor = Color.FromArgb(fullData.leastValueIndex, 0, 0);
            }
            else if (Histogram.BarColor == Color.Green)
            {
                MostValue_Color.BackColor = Color.FromArgb(0, fullData.mostValueIndex, 0);
                LeastValue_Color.BackColor = Color.FromArgb(0, fullData.leastValueIndex, 0);
            }
            else if (Histogram.BarColor == Color.Blue)
            {
                MostValue_Color.BackColor = Color.FromArgb(0, 0, fullData.mostValueIndex);
                LeastValue_Color.BackColor = Color.FromArgb(0, 0, fullData.leastValueIndex);
            }


            MaxValue.Text = "Max Value:  " + fullData.maxValue;
            MinValue.Text = "Min Value:  " + fullData.minValue;
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

            int Lh = MostValue.Height + 5;  // Label Height

            MostValue.Top = topVal + 30;
            MostValue_Color.Top = topVal + 30;
            MostValue_Color.Left = MostValue.Width;

            MostValue_Index.Top = MostValue.Top + Lh;
            MostValue_Quantity.Top = MostValue_Index.Top + Lh;



            LeastValue.Top = MostValue_Quantity.Top + Lh*2;
            LeastValue_Color.Top = MostValue_Quantity.Top + Lh*2;
            LeastValue_Color.Left = LeastValue.Width;

            LeastValue_Index.Top = LeastValue.Top + Lh;
            LeastValue_Quantity.Top = LeastValue_Index.Top + Lh;



            MaxValue.Top = topVal + 30;
            MaxValue.Left = Histogram.Width / 2 - (pipe_128.Width / 2) - 6;

            MinValue.Top = MostValue.Top + Lh;
            MinValue.Left = Histogram.Width / 2 - (pipe_128.Width / 2) - 6;
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
                pipe_255 = Get_pipe("255"),

                MostValue = Get_Label("Most Value:  "),
                MostValue_Index = Get_Label(""),
                MostValue_Quantity = Get_Label(""),
                LeastValue = Get_Label("Least Value:  "),
                LeastValue_Index = Get_Label(""),
                LeastValue_Quantity = Get_Label(""),

                MaxValue = Get_Label("Max Value:  "),
                MinValue = Get_Label("Min Value:  ")
            };

            
            result.Histogram = new Histogram(ForeColor);

            result.MostValue_Color = Get_ColorPanel();
            result.LeastValue_Color = Get_ColorPanel();


            result.Height = result.Histogram.Height + 20;
            result.Dock = DockStyle.Fill;


            result.Controls.AddRange(new Control[]
            {
                result.Histogram,
                result.MostValue,
                result.MostValue_Index,
                result.MostValue_Quantity,
                result.MostValue_Color,
                result.LeastValue,
                result.LeastValue_Index,
                result.LeastValue_Quantity,
                result.LeastValue_Color,

                result.MaxValue,
                result.MinValue
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

        private static Label Get_Label(string value)
        {
            return Get_pipe(value);
        }

        private static Panel Get_ColorPanel()
        {
            return new Panel()
            {
                BorderStyle = BorderStyle.FixedSingle,
                Width = 13,
                Height = 13,
            };
        }
    }
    #endregion
    // ##########################################################################################################################
    // ##########################################################################################################################
}
