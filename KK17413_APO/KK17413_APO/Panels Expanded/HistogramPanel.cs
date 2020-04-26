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

        public TabControl tabControl;

        private TabPage red_hist_Page;
        private TabPage green_hist_Page;

        public HistogramPanel()
        {
            Red_hist = new Histogram();

            tabControl = new TabControl();

            red_hist_Page = new TabPage()
            {
                Padding = new Padding(3),
                Text = "Red",
                UseVisualStyleBackColor = true,                
                Height = Red_hist.Height + 50
            };

            green_hist_Page = new TabPage();

            tabControl.Dock = DockStyle.Top;
            tabControl.Height = red_hist_Page.Height;

            tabControl.Appearance = TabAppearance.Normal;

            green_hist_Page.Padding = new Padding(3);
            green_hist_Page.Text = "Green";
            green_hist_Page.UseVisualStyleBackColor = true;           



            this.Controls.Add(tabControl);

            red_hist_Page.Controls.Add(Red_hist);

            tabControl.Controls.Add(red_hist_Page);
            tabControl.Controls.Add(green_hist_Page);

            ReloadColorSet();
        }

        public void ReloadColorSet()
        {
            this.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer3");
            tabControl.BackColor = Color.Red;
            this.ForeColor = ProgramSettings.ColorManager.GetValue("bgColorLayer3");

            red_hist_Page.BackColor = ProgramSettings.ColorManager.GetValue("bgColorLayer3");
        }



    }
}
