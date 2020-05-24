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


namespace KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded
{
    [System.ComponentModel.DesignerCategory("")]
    public class Histogram : Panel
    {

        List<Bar> bars;
        FlowLayoutPanel container;

        public readonly Color BarColor;


        public Histogram(Color ForeColor)
        {
            this.Size = new Size(689, 281);
            this.Dock = DockStyle.None;

            BarColor = ForeColor;

            container = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = false,
                WrapContents = false
            };

            this.Controls.Add(container);


            // Two For the left margin:            
            container.Controls.Add(new Bar
            {
                Width = 1,
                Height = container.Height - 2,
                Enabled = false
            });
            container.Controls.Add(new Bar
            {
                Width = 1,
                Height = container.Height - 2,
                Enabled = false
            });
            // -------------------------------------------------

            bars = new List<Bar>();
            var rand = new Random();

            for (int i = 0; i < 256; ++i)
            {
                Bar newBar = new Bar()
                {
                    Dock = DockStyle.Bottom,
                    OriginalWidth = 1,
                    OriginalHeight = container.Height - 2,
                    //BackColor = ProgramSettings.ColorManager.Transparent,
                    ForeColor = ForeColor
                };
                newBar.Width = newBar.OriginalWidth;
                newBar.Height = newBar.OriginalHeight;

                container.Controls.Add(newBar);
                bars.Add(newBar);
            }

            // Two For the right margin:   
            container.Controls.Add(new Bar
            {
                Width = 1,
                Height = container.Height - 2,
                Enabled = false
            });
            Bar rightMargin = new Bar
            {
                Width = 1,
                Height = container.Height - 2,
                //BackColor = Color.White,
                //ForeColor = Color.White,
                Enabled = false
            };
            container.Controls.Add(rightMargin);
            // -------------------------------------------------

            this.Width = rightMargin.Left + rightMargin.Width + 2;
            //rightMargin.Visible = false;
        }

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

    public class Bar : Panel
    {
        public Bar()
        {
            this.Padding = new Padding(0);
            this.Margin = new Padding(1, 0, 0, 0);
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
        private int height;
        public override Color ForeColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
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
}
