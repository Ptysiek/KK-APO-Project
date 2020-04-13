using System;
using System.Drawing;
using System.Windows.Forms;


namespace KK17413_APO.Toolbox_Tools_Expanded
{
    [System.ComponentModel.DesignerCategory("")]
    public class ImageWorkspaceNode : SplitContainer
    {
        public SplitterPanel HeadPanel { get => headPanel; }
        public SplitterPanel BodyPanel { get => bodyPanel; }
        public Button ToggleButton { get => headPanel_Button; }
        public override string Text { get => headPanel_Text.Text; set => headPanel_Text.Text = value; }


        private SplitterPanel headPanel;
        private SplitterPanel bodyPanel;

        private Button headPanel_Button;
        private Label headPanel_Text;

        int Collapsed_height;

        public ImageWorkspaceNode()
        {
            headPanel = this.Panel1;
            bodyPanel = this.Panel2;

            headPanel_Button = new Button();
            headPanel_Text = new Label();

            Collapsed_height = headPanel_Text.Height;

            this.Orientation = Orientation.Horizontal;
            //workspace.SplitterDistance = Collapsed_height;
            this.IsSplitterFixed = true;
            this.Panel1Collapsed = false;
            this.Panel2Collapsed = true;
            this.Panel1MinSize = 1;
            this.Panel2MinSize = 1;
            this.Height = Collapsed_height;

            headPanel.BackColor = Color.Yellow;
            bodyPanel.BackColor = Color.Orange;

            headPanel_Button.Text = "";
            headPanel_Button.BackColor = Color.White;
            headPanel_Button.Height = headPanel_Text.Height - 10;
            headPanel_Button.Width = headPanel_Button.Height;
            headPanel_Button.Top = headPanel.Height/2 - headPanel_Button.Height/2;

            headPanel_Text.Left = headPanel_Button.Width + 5;
            headPanel_Text.TextAlign = ContentAlignment.MiddleLeft;


            headPanel.Controls.Add(headPanel_Button);
            headPanel.Controls.Add(headPanel_Text);


            headPanel_Button.Click += new EventHandler(hp_Button_Click);
        }


        private void hp_Button_Click(object sender, EventArgs e)
        {
            this.Panel2Collapsed = !this.Panel2Collapsed;

            if (this.Panel2Collapsed)
            {
                this.Height = Collapsed_height;
            }
            else
            {
                this.Height = 100;
                this.SplitterDistance = Collapsed_height;
            }
        }
    }
}
