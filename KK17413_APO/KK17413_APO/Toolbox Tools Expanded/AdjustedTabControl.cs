using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KK17413_APO.Toolbox_Tools_Expanded
{
    [System.ComponentModel.DesignerCategory("")]
    public class AdjustedTabControl : Panel
    {
        public int ButtonContainerHeight {get => buttonContainer.Height;}


        private FlowLayoutPanel buttonContainer;
        private Panel workspace;

        private List<AdjustedTabControl_PageButton> buttons;


        public AdjustedTabControl()
        {
            buttonContainer = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.LeftToRight,
                Dock = DockStyle.Top,
                WrapContents = false,
                AutoSize = false,
                Height = 30     // Buttons Height
            };

            workspace = new Panel()
            {
                Dock = DockStyle.Fill
            };

            buttons = new List<AdjustedTabControl_PageButton>();

            buttonContainer.Dock = DockStyle.Top;


            this.Controls.Add(workspace);
            this.Controls.Add(buttonContainer);
        }

        public void AddPage(string name, Panel panel)
        {
            panel.Visible = false;

            AdjustedTabControl_PageButton newButton = new AdjustedTabControl_PageButton()
            {
                Text = name,
                ActivePanel = panel
            };

            buttons.Add(newButton);
            buttonContainer.Controls.Add(newButton);


            workspace.Controls.Add(panel);

            newButton.Click += PageButton_Click;
        }

        

        private void PageButton_Click(object sender, EventArgs e)
        {
            foreach (var button in buttons)
            {
                if (sender.Equals(button))
                    button.ActivePanel.Visible = true;
                else
                    button.ActivePanel.Visible = false;
            }
        }
    }



    class AdjustedTabControl_PageButton : Button
    {
        public Panel ActivePanel;
    }
}
