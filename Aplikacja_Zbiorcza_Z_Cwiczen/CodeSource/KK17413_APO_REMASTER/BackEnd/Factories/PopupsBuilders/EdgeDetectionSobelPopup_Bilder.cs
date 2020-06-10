using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using KK17413_APO_REMASTER.BackEnd.DataStructures;
using KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations;
using KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded;


namespace KK17413_APO_REMASTER.BackEnd.Factories.PopupsBuilders
{
    class EdgeDetectionSobelPopup_Bilder : IPopupBuilder
    {
        public override IPopup GetResult()
        {
            int extraMargin = 5;
            Histogram toScaleHistogram = Histogram_Builder.GetResult(Color.White);
            int HistogramWidth = toScaleHistogram.Width;

            EdgeDetectionSobelPopup result = new EdgeDetectionSobelPopup
            {
                form = new Form(),
                Ok_Button = new Button() { Text = "ok" },
                Cancel_Button = new Button() { Text = "cancel" },
                Aply_Button = new Button() { Text = "apply" },

                xOrder_Text = new Label() { Text = "x order:   [0 < x < 2]   and   [x + y == 1]" },
                yOrder_Text = new Label() { Text = "y order:   [0 < y < 2]   and   [x + y == 1]" },
                apertureSize_Text = new Label() { Text = "aperture size:   [0 < a < 31]   and   [a % 2 == 1]" },

                xOrder = new TrackBar(),
                yOrder = new TrackBar(),
                apertureSize = new TrackBar(),

                xOrder_Value = new Label(),
                yOrder_Value = new Label(),
                apertureSize_Value = new Label()
            };

            FlowLayoutPanel ButtonContainer = new FlowLayoutPanel()
            {
                Dock = DockStyle.Bottom,
                Height = result.Ok_Button.Height + extraMargin,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false
            };

            result.xOrder_Text.Left = extraMargin;
            result.yOrder_Text.Left = extraMargin;
            result.apertureSize_Text.Left = extraMargin;

            result.xOrder.Left = extraMargin;
            result.yOrder.Left = extraMargin;
            result.apertureSize.Left = extraMargin;

            result.form.FormClosing += result.Form_FormClosing;

            result.Ok_Button.Click += result.Ok_Button_Click;
            result.Cancel_Button.Click += result.Cancel_Button_Click;
            result.Aply_Button.Click += result.Aply_Button_Click;



            result.xOrder_Text.Height = ButtonContainer.Height / 2;
            result.xOrder_Text.Width = HistogramWidth;
            result.xOrder_Text.Top = extraMargin;
            result.xOrder.Height = ButtonContainer.Height / 2;
            result.xOrder.Width = HistogramWidth;
            result.xOrder.Top = result.xOrder_Text.Top
                                    + result.xOrder_Text.Height;
            result.yOrder_Text.Height = ButtonContainer.Height / 2;
            result.yOrder_Text.Width = HistogramWidth;
            result.yOrder_Text.Top = result.xOrder.Top
                                    + result.xOrder.Height;
            result.yOrder.Height = ButtonContainer.Height / 2;
            result.yOrder.Width = HistogramWidth;
            result.yOrder.Top = result.yOrder_Text.Top
                                    + result.yOrder_Text.Height;
            result.apertureSize_Text.Height = ButtonContainer.Height / 2;
            result.apertureSize_Text.Width = HistogramWidth;
            result.apertureSize_Text.Top = result.yOrder.Top
                                    + result.yOrder.Height;
            result.apertureSize.Height = ButtonContainer.Height / 2;
            result.apertureSize.Width = HistogramWidth;
            result.apertureSize.Top = result.apertureSize_Text.Top
                                    + result.apertureSize_Text.Height;


            result.xOrder_Value.Left = HistogramWidth + extraMargin;
            result.yOrder_Value.Left = HistogramWidth + extraMargin;
            result.apertureSize_Value.Left = HistogramWidth + extraMargin;

            result.xOrder_Value.Top = result.xOrder.Top;
            result.yOrder_Value.Top = result.yOrder.Top;
            result.apertureSize_Value.Top = result.apertureSize.Top;


            result.xOrder.ValueChanged += result.Value_ValueChanged;
            result.yOrder.ValueChanged += result.Value_ValueChanged;
            result.apertureSize.ValueChanged += result.Value_ValueChanged;


            result.form.Height = ButtonContainer.Height
                                + ButtonContainer.Height * 8
                                + 64;
            result.form.Width = HistogramWidth
                                + result.xOrder_Value.Width
                                + extraMargin * 3;

            result.form.Controls.Add(result.xOrder_Text);
            result.form.Controls.Add(result.yOrder_Text);
            result.form.Controls.Add(result.apertureSize_Text);

            result.form.Controls.Add(result.xOrder);
            result.form.Controls.Add(result.yOrder);
            result.form.Controls.Add(result.apertureSize);


            result.form.Controls.Add(result.xOrder_Value);
            result.form.Controls.Add(result.yOrder_Value);
            result.form.Controls.Add(result.apertureSize_Value);

            result.form.Controls.Add(ButtonContainer);
            ButtonContainer.Controls.Add(result.Aply_Button);
            ButtonContainer.Controls.Add(result.Cancel_Button);
            ButtonContainer.Controls.Add(result.Ok_Button);
            //result.form.Show();

            return result;
        }
    }



    class EdgeDetectionSobelPopup : IPopup
    {
        private Program PROGRAM;
        private ImageForm_Service SERVICE;
        private IOperation OPERATION;

        private string OperationIDName;
        private ImageData LastModification;

        public Form form;
        public Button Ok_Button;
        public Button Cancel_Button;
        public Button Aply_Button;

        public Label xOrder_Text;
        public Label yOrder_Text;
        public Label apertureSize_Text;

        public TrackBar xOrder;
        public TrackBar yOrder;
        public TrackBar apertureSize;

        public Label xOrder_Value;
        public Label yOrder_Value;
        public Label apertureSize_Value;

        private bool wait = true;

        public override void Start(Program program, ImageForm_Service service, IOperation operation, string operationName)
        {
            if (program == null)
                return;
            if (service == null)
                return;
            if (operation == null)
                return;
            if (operationName == null)
                return;

            this.PROGRAM = program;
            this.SERVICE = service;
            this.OPERATION = operation;
            this.OperationIDName = operationName;

            if (SERVICE.data == null)
                return;
            if (SERVICE.data.LastData() == null)
                return;


            xOrder.Maximum = 1;
            xOrder.Minimum = 0;

            yOrder.Maximum = 1;
            yOrder.Minimum = 0;

            apertureSize.Maximum = 31;
            apertureSize.Minimum = 1;


            ReloadLanguage();
            ReloadColorSet();

            this.form.Show();
            wait = false;
        }

        public override void ReloadLanguage()
        {
            Language LanguageSet = PROGRAM.GetLanguage();
            ReloadLanguage(LanguageSet);
        }
        public override void ReloadLanguage(Language LanguageSet)
        {
            form.Text = LanguageSet.GetValue(OperationIDName);

            Ok_Button.Text = LanguageSet.GetValue("Ok_Button");
            Cancel_Button.Text = LanguageSet.GetValue("Cancel_Button");
            Aply_Button.Text = LanguageSet.GetValue("Apply_Button");
        }
        public override void ReloadColorSet()
        {
            ColorSet ColorSet = PROGRAM.GetColorSet();
            ReloadColorSet(ColorSet);
        }
        public override void ReloadColorSet(ColorSet ColorSet)
        {
            form.ForeColor = ColorSet.GetValue("fontColor");
            form.BackColor = ColorSet.GetValue("bgColorLayer1");
        }


        public void Value_ValueChanged(object sender, EventArgs e)
        {
            int tmp;

            tmp = apertureSize.Value;
            if (tmp % 2 != 1)                  // apertureSize % 2 == 1
            {
                --tmp;
            }
            if (tmp < apertureSize.Minimum)               
            {
                tmp = apertureSize.Minimum;
            }
            apertureSize.Value = tmp;


            int test = 0;
            if (xOrder.Value + yOrder.Value != 1)                        
            {
                ++test;
                xOrder_Value.ForeColor = Color.Red;
                yOrder_Value.ForeColor = Color.Red;
            }
            else
            {
                xOrder_Value.ForeColor = Color.Black;
                yOrder_Value.ForeColor = Color.Black;
            }
            wait = (test > 0) ? true : false;


            xOrder_Value.Text = xOrder.Value.ToString();
            yOrder_Value.Text = yOrder.Value.ToString();
            apertureSize_Value.Text = apertureSize.Value.ToString();

            if (wait)
                return;

            Recalculations();
        }


        public void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SERVICE == null)
                return;
            if (SERVICE.data == null)
                return;
            if (SERVICE.imageWindow == null)
                return;
            if (SERVICE.data.LastData() == null)
                return;

            SERVICE.imageWindow.ReloadImageData_All(SERVICE.data.LastData());
        }

        public void Cancel_Button_Click(object sender, EventArgs e)
        {
            form.Close();

            if (SERVICE == null)
                return;
            if (SERVICE.data == null)
                return;
            if (SERVICE.imageWindow == null)
                return;
            if (SERVICE.data.LastData() == null)
                return;

            SERVICE.imageWindow.ReloadImageData_All(SERVICE.data.LastData());
            //SERVICE.ClosePopup(this);
        }

        public void Ok_Button_Click(object sender, EventArgs e)
        {
            string OperationName = PROGRAM.GiveOperationName(OperationIDName);
            SERVICE.DataOperation(LastModification, OperationName);
            form.Close();
            //SERVICE.ClosePopup(this);
        }

        public void Aply_Button_Click(object sender, EventArgs e)
        {
            string OperationName = PROGRAM.GiveOperationName(OperationIDName);
            SERVICE.DataOperation(LastModification, OperationName);
        }

        private List<int> GetArgs()
        {
            return new List<int>()
            {
                xOrder.Value,
                yOrder.Value,
                apertureSize.Value
            };
        }

        private void Recalculations()
        {
            LastModification = OPERATION.GetResult(SERVICE, GetArgs());

            // Show Preview:
            SERVICE.imageWindow.ReloadImageData_All(LastModification);
        }
    }
}
