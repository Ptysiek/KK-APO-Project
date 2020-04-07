using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace KK17413_APO.Toolbox_Tools_Expanded
{

    // ########################################################################################################################################
    // ########################################################################################################################################
    public class AccordionContainer
    {
        // ____________________________________________________________________________________________________________________________________
        // ------------------------------------------------------------------------------------------------------------------------------------
        public Control Control { get{return frame;} }
        public Control Parent  { get{return frame.Parent;}  set{frame.Parent = value;} }
        public Control TopLevelControl { get{return frame.TopLevelControl;} }
        public AccessibleObject AccessibilityObject { get{return frame.AccessibilityObject;} }
        public AccessibleRole AccessibleRole { get{return frame.AccessibleRole;}  set{frame.AccessibleRole = value;} }
        public string AccessibleDefaultActionDescription { get{return frame.AccessibleDefaultActionDescription;}  set{frame.AccessibleDefaultActionDescription = value;} }
        public string AccessibleDescription { get{return frame.AccessibleDescription;}  set{frame.AccessibleDescription = value;} }
        public string AccessibleName { get{return frame.AccessibleName;}  set{frame.AccessibleName = value;} }
        public bool HasChildren { get{return frame.HasChildren;} }
        public bool Disposing { get{return frame.Disposing;} }
        public IntPtr Handle { get{return frame.Handle;} }

        // ------------------------------------------------------------------------------------------------------------------------------------
        public IContainer Container { get{return frame.Container;} }
        public Control.ControlCollection Controls { get{return frame.Controls;} }
        public ControlBindingsCollection DataBindings { get{return frame.DataBindings;} }
        public BindingContext BindingContext { get{return frame.BindingContext;}  set{frame.BindingContext = value;} }
        public Size ClientSize  { get{return frame.ClientSize;}  set{frame.ClientSize = value;} }
        public Rectangle Bounds { get{return frame.Bounds;}      set{frame.Bounds = value;} }
        public Rectangle ClientRectangle  { get{return frame.ClientRectangle;} }
        public Rectangle DisplayRectangle { get{return frame.DisplayRectangle;} }

        // ------------------------------------------------------------------------------------------------------------------------------------
        public string Name  { get{return frame.Name;}     set{frame.Name = value;} }
        public bool Visible { get{return frame.Visible;}  set{frame.Visible = value;} }
        public bool Enabled { get{return frame.Enabled;}  set{frame.Enabled = value;} }
        public bool WrapContents { get{return frame.WrapContents;}  set{frame.WrapContents = value;} }
        public FlowDirection FlowDirection { get{return frame.FlowDirection;}  set{frame.FlowDirection = value;} }

        // ------------------------------------------------------------------------------------------------------------------------------------
        public AnchorStyles Anchor { get{return frame.Anchor;}  set{frame.Anchor = value;} }
        public DockStyle Dock { get{return frame.Dock;}  set{frame.Dock = value;} }
        public bool AutoSize  { get{return frame.AutoSize;}  set{frame.AutoSize = value;} }
        public AutoSizeMode AutoSizeMode { get{return frame.AutoSizeMode;}  set{frame.AutoSizeMode = value;} }

        // ------------------------------------------------------------------------------------------------------------------------------------
        public Size Size { get{return frame.Size;}  set{frame.Size = value;} }
        public Size MaximumSize { get{return frame.MaximumSize;}  set{frame.MaximumSize = value;} }
        public Size MinimumSize { get{return frame.MinimumSize;}  set{frame.MinimumSize = value;} }
        public int Width { get{return frame.Width;}  set{frame.Width = value;} }
        public int Height { get{return frame.Height;}  set{frame.Height = value;} }
        public Padding Margin { get{return frame.Margin;}  set{frame.Margin = value;} }
        public Padding Padding { get{return frame.Padding;}  set{frame.Padding = value;} }

        // ------------------------------------------------------------------------------------------------------------------------------------
        public Point Location { get{return frame.Location;}  set{frame.Location = value;} }
        public int Top  { get{return frame.Top;}  set{frame.Top = value;} }
        public int Left { get{return frame.Left;}  set{frame.Left = value;} }
        public int Bottom { get{return frame.Bottom;} }
        public int Right { get{return frame.Right;} }

        // ------------------------------------------------------------------------------------------------------------------------------------
        public bool AutoScroll { get{return frame.AutoScroll;}  set{frame.AutoScroll = value;} }
        public Size AutoScrollMargin { get{return frame.AutoScrollMargin;}  set{frame.AutoScrollMargin = value;} }
        public Size AutoScrollMinSize { get{return frame.AutoScrollMinSize;}  set{frame.AutoScrollMinSize = value;} }
        public Point AutoScrollOffset { get{return frame.AutoScrollOffset;}  set{frame.AutoScrollOffset = value;} }
        public Point AutoScrollPosition { get{return frame.AutoScrollPosition;}  set{frame.AutoScrollPosition = value;} }
        public HScrollProperties HorizontalScroll { get{return frame.HorizontalScroll;} }
        public VScrollProperties VerticalScroll { get{return frame.VerticalScroll;} }

        // ------------------------------------------------------------------------------------------------------------------------------------
        public BorderStyle BorderStyle { get{return frame.BorderStyle;}  set{frame.BorderStyle = value;} }
        public Color BackColor { get{return frame.BackColor;}  set{frame.BackColor = value;} }
        public Color ForeColor { get{return frame.ForeColor;}  set{frame.ForeColor = value;} }
        public Font Font { get{return frame.Font;}  set{frame.Font = value;} }



        // ____________________________________________________________________________________________________________________________________
        // ------------------------------------------------------------------------------------------------------------------------------------
        private FlowLayoutPanel frame;
        private List<AccordionNode> items;



        // ____________________________________________________________________________________________________________________________________
        // ------------------------------------------------------------------------------------------------------------------------------------
        public AccordionContainer()
        {
            frame = new FlowLayoutPanel();
            items = new List<AccordionNode>();


            items.Add(new AccordionNode());
            items.Add(new AccordionNode());
            items.Add(new AccordionNode());
            items.Add(new AccordionNode());


            frame.Dock = DockStyle.Fill;
            frame.AutoScroll = true;
            //frame.AutoScrollMinSize = new Size(100, 100) ;


            frame.WrapContents = false;
            frame.FlowDirection = FlowDirection.TopDown;


            //frame..Add
            foreach (var item in items)
            {
                frame.Controls.Add(item.Control);
                //frame.Container.Add(item);
            }

        }
    }



    // ########################################################################################################################################
    // ########################################################################################################################################
    class AccordionNode
    {
        public Control Control { get { return workspace; } }


        private SplitContainer workspace;
        private SplitterPanel headPanel;
        private SplitterPanel bodyPanel;

        private Button headPanel_Button;
        private Label headPanel_Text;

        int Collapsed_height;

        public AccordionNode()
        {
            workspace = new SplitContainer();
            headPanel = workspace.Panel1;
            bodyPanel = workspace.Panel2;

            headPanel_Button = new Button();
            headPanel_Text = new Label();

            Collapsed_height = headPanel_Text.Height - 10;

            workspace.Orientation = Orientation.Horizontal;
            //workspace.SplitterDistance = Collapsed_height;
            workspace.IsSplitterFixed = true;
            workspace.Panel1Collapsed = false;
            workspace.Panel2Collapsed = true;
            workspace.Panel1MinSize = 1;
            workspace.Panel2MinSize = 1;
            workspace.Height = Collapsed_height;

            headPanel.BackColor = Color.Yellow;            
            bodyPanel.BackColor = Color.Orange;


            headPanel_Button.Text = "";
            headPanel_Button.BackColor = Color.White;
            headPanel_Button.Height = headPanel_Text.Height - 10;
            headPanel_Button.Width = headPanel_Button.Height;

            headPanel_Text.Text = "AccordionNode";
            headPanel_Text.Left = headPanel_Button.Width;


            headPanel.Controls.Add(headPanel_Button);
            headPanel.Controls.Add(headPanel_Text);


            headPanel_Button.Click += new EventHandler(hp_Button_Click);
        }


        private void hp_Button_Click(object sender, EventArgs e)
        {
            workspace.Panel2Collapsed = !workspace.Panel2Collapsed;

            if(workspace.Panel2Collapsed)
            {
                workspace.Height = Collapsed_height;
            }
            else
            {
                workspace.Height = 100;
                workspace.SplitterDistance = Collapsed_height;
            }
        }
    }
}
