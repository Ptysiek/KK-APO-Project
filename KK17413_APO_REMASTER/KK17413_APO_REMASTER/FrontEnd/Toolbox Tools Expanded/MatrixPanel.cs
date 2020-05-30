using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded
{
    [System.ComponentModel.DesignerCategory("")]
    public class MatrixPanel : FlowLayoutPanel
    {
        public List<List<TextBox>> panels;


        //*
        public float[,] GetMatrix()
        {
            if (panels == null)
                return null;

            float[,] result = new float[panels.Count,panels.Count];

            for (int c=0; c<panels.Count; ++c)
            {
                for (int r=0; r<panels.Count; ++r)
                {
                    result[c, r] = float.Parse(panels[c][r].Text);
                }
            }

            return result;
        }
        //*/
    }
    



    public static class MatrixPanel_Builder
    {
        public static MatrixPanel GetResult(int size)
        {
            if (size < 0)
                return null;

            MatrixPanel result = new MatrixPanel()
            {
                panels = Get_PanelsInstances(size),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = false,
                AutoSize = false
            };

            int panelWidth = result.panels[0][0].Width;
            result.Width = size * (panelWidth + 5);
            result.Height = result.Width;

            foreach (var raw in result.panels) {
                foreach (var textbox in raw) {
                    result.Controls.Add(textbox);
                }
            }            

            return result;
        }

        private static List<List<TextBox>> Get_PanelsInstances(int size)
        {
            var panels = new List<List<TextBox>>(size);
            for (int i = 0; i < size; ++i)
            {
                var tmp = new List<TextBox>(size);

                for (int v = 0; v < size; ++v)
                {
                    tmp.Add(Get_TextBox());
                }
                panels.Add(tmp);
            }
            return panels;
        }

        private static TextBox Get_TextBox()
        {
            return new TextBox()
            {
                Width = 20,
                Height = 20,
                WordWrap = true                
            };
        }

    }
}
