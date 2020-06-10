using System.Drawing;
using System.Windows.Forms;
using KK17413_APO_REMASTER.BackEnd.DataStructures;
using KK17413_APO_REMASTER.BackEnd.Factories;
using KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded;


namespace KK17413_APO_REMASTER.FrontEnd.Views_and_Expanded_Panels
{
    [System.ComponentModel.DesignerCategory("")]
    public class Histogram_InfoPanel : Panel
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

        public void ReloadData(HistogramData fullData)
        {
            Histogram.ReloadHistogram(fullData.data);
            ReloadHistogram_InfoLabels(fullData);
        }

        public void ReloadColorSet(ColorSet ColorSet)
        {
            Histogram.BackColor = ColorSet.GetValue("bgHistogram");
        }

        private void ReloadHistogram_InfoLabels(HistogramData fullData)
        {
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
    }



    // ##########################################################################################################################
    // ##########################################################################################################################

    #region Panel_HistogramInfoPanel_Builder
    class Panel_HistogramInfoPanel_Builder
    {
        public static Histogram_InfoPanel GetResult(Color ForeColor)
        {
            Histogram_InfoPanel result = new Histogram_InfoPanel()
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

            result.Histogram = Histogram_Builder.GetResult(ForeColor);
            //result.Histogram = new Histogram(ForeColor);

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

            Configure_PipePosition(ref result);

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

        private static void Configure_PipePosition(ref Histogram_InfoPanel result)
        {
            int topVal = result.Histogram.Height;

            result.pipe_0.Left = 0;
            result.pipe_64.Left = result.Histogram.Width / 4 - (result.pipe_64.Width / 2) - 6;
            result.pipe_128.Left = result.Histogram.Width / 2 - (result.pipe_128.Width / 2) - 6;
            result.pipe_192.Left = result.Histogram.Width / 2 + result.Histogram.Width / 4 - (result.pipe_192.Width / 2) - 6;
            result.pipe_255.Left = result.Histogram.Width - (result.pipe_255.Width / 2) - 6;
            result.pipe_0.Top = topVal;
            result.pipe_64.Top = topVal;
            result.pipe_128.Top = topVal;
            result.pipe_192.Top = topVal;
            result.pipe_255.Top = topVal;

            int Lh = result.MostValue.Height + 5;  // Label Height

            result.MostValue.Top = topVal + 30;
            result.MostValue_Color.Top = topVal + 30;
            result.MostValue_Color.Left = result.MostValue.Width;

            result.MostValue_Index.Top = result.MostValue.Top + Lh;
            result.MostValue_Quantity.Top = result.MostValue_Index.Top + Lh;

            // ----------------------------------------------------------------------

            result.LeastValue.Top = result.MostValue_Quantity.Top + Lh * 2;
            result.LeastValue_Color.Top = result.MostValue_Quantity.Top + Lh * 2;
            result.LeastValue_Color.Left = result.LeastValue.Width;

            result.LeastValue_Index.Top = result.LeastValue.Top + Lh;
            result.LeastValue_Quantity.Top = result.LeastValue_Index.Top + Lh;

            // ----------------------------------------------------------------------

            result.MaxValue.Top = topVal + 30;
            result.MaxValue.Left = result.Histogram.Width / 2 - (result.pipe_128.Width / 2) - 6;

            result.MinValue.Top = result.MostValue.Top + Lh;
            result.MinValue.Left = result.Histogram.Width / 2 - (result.pipe_128.Width / 2) - 6;
        }
    }
    #endregion
    
    // ##########################################################################################################################
    // ##########################################################################################################################
}
