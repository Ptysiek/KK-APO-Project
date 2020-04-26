using KK17413_APO.Toolbox_Tools_Expanded;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KK17413_APO.Panels_Expanded
{
    [System.ComponentModel.DesignerCategory("")]
    public class HistogramPanel : Panel
    {

        public Histogram Red_hist;

        //public TabControl tabControl;
        public AdjustedTabControl tabControl;

       // private TabPage red_hist_Page;
       // private TabPage green_hist_Page;

        private Panel red_hist_Page;
        private Panel green_hist_Page;

        public HistogramPanel()
        {
            tabControl = new AdjustedTabControl();


            Red_hist = new Histogram()
            {
                Top = tabControl.ButtonContainerHeight
            };


            red_hist_Page = new Panel()
            {
                //Margin = new Padding(10),
                //Text = "Red",
                // UseVisualStyleBackColor = true,    
                Height = Red_hist.Height + tabControl.ButtonContainerHeight,
                Dock = DockStyle.Fill
            };

            green_hist_Page = new Panel();

            tabControl.Dock = DockStyle.Top;
            tabControl.Height = red_hist_Page.Height + tabControl.ButtonContainerHeight;

            //tabControl.Appearance = TabAppearance.Normal;

            //green_hist_Page.Padding = new Padding(3);
            //green_hist_Page.Text = "Green";
            //green_hist_Page.UseVisualStyleBackColor = true;           



            this.Controls.Add(tabControl);
            red_hist_Page.Controls.Add(Red_hist);

            tabControl.AddPage("Red", red_hist_Page);
            tabControl.AddPage("Green", green_hist_Page);




            //tabControl.Controls.Add(red_hist_Page);
            //tabControl.Controls.Add(green_hist_Page);

        }

    }
}
