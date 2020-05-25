using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded
{
    [System.ComponentModel.DesignerCategory("")]
    public class Histogram : Panel
    {
        public List<Bar> bars;
        public FlowLayoutPanel container;
        public Color BarColor;

        public void ReloadHistogram(List<int> data)
        {
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
                ++i;
            }            
        }
    }



    // ##########################################################################################################################
    // ##########################################################################################################################

    #region Histogram_DataStructure
    public class Bar : Panel
    {
        public Bar()
        {
            this.Padding = new Padding(0);
            this.Margin = new Padding(1, 0, 0, 0);
        }

        public override Color ForeColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }
        public new int Height
        {
            get => height;
            set
            {
                height = value;

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
        public int MaxValue
        {
            get => maxvalue;
            set
            {
                maxvalue = value;
                CalculateColorPanelHeight();
            }
        }

        public int OriginalHeight;
        public int OriginalWidth;

        private int height;
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
            //colorPanel.Height = base.Height * value / maxvalue;
            base.Height = height * value / maxvalue;
        }
    }
    #endregion

    // ##########################################################################################################################
    // ##########################################################################################################################

    #region Histogram_Builder
    class Histogram_Builder
    {
        public static Histogram GetResult(Color ForeColor)
        {
            Histogram result = new Histogram()
            {
                Size = new Size(689, 281),
                Dock = DockStyle.None,
                BarColor = ForeColor,

                container = Get_Container()
            };
            result.Controls.Add(result.container);

            // Two For the left margin:                 
            result.container.Controls.Add(Get_MarginEmptyBar(result.container.Height));
            result.container.Controls.Add(Get_MarginEmptyBar(result.container.Height));

            Get_Bars(ref result, ForeColor);

            // Two For the right margin:   
            result.container.Controls.Add(Get_MarginEmptyBar(result.container.Height));
            Bar rightMargin = Get_MarginEmptyBar(result.container.Height);

            result.container.Controls.Add(rightMargin);
            result.Width = rightMargin.Left + rightMargin.Width + 2;
            //rightMargin.Visible = false;

            return result;
        }

        private static FlowLayoutPanel Get_Container()
        {
            return new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = false,
                WrapContents = false
            };
        }

        private static Bar Get_MarginEmptyBar(int height)
        {
            return new Bar
            {
                Width = 1,
                Height = height - 2,
                Enabled = false
            };
        }
        
        private static Bar Get_MainBar(int height, Color ForeColor)
        {
            return new Bar()
            {
                Dock = DockStyle.Bottom,
                OriginalWidth = 1,
                OriginalHeight = height - 2,
                //BackColor = ProgramSettings.ColorManager.Transparent,
                ForeColor = ForeColor
            };
        }

        private static void Get_Bars(ref Histogram result, Color ForeColor)
        {
            result.bars = new List<Bar>();

            for (int i = 0; i < 256; ++i)
            {
                Bar newBar = Get_MainBar(result.container.Height, ForeColor);
                newBar.Width = newBar.OriginalWidth;
                newBar.Height = newBar.OriginalHeight;

                result.container.Controls.Add(newBar);
                result.bars.Add(newBar);
            }
        }
    
    }
    #endregion

    // ##########################################################################################################################
    // ##########################################################################################################################
}
