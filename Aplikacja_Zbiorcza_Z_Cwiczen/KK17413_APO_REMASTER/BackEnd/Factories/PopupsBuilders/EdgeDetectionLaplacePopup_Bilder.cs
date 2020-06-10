using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using KK17413_APO_REMASTER.BackEnd.DataStructures;
using KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations;
using KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded;


namespace KK17413_APO_REMASTER.BackEnd.Factories.PopupsBuilders
{
    class EdgeDetectionLaplacePopup_Bilder : IPopupBuilder
    {
        public override IPopup GetResult()
        {
            int extraMargin = 5;
            Histogram toScaleHistogram = Histogram_Builder.GetResult(Color.White);
            int HistogramWidth = toScaleHistogram.Width;

            EdgeDetectionLaplacePopup result = new EdgeDetectionLaplacePopup
            {
                form = new Form(),
                Ok_Button = new Button() { Text = "ok" },
                Cancel_Button = new Button() { Text = "cancel" },
                Aply_Button = new Button() { Text = "apply" },

                apertureSize_Text = new Label() { Text = "aperture size:   [1 < a < 31]   and   [a % 2 == 1]" },
                apertureSize = new TrackBar(),
                apertureSize_Value = new Label()
            };

            FlowLayoutPanel ButtonContainer = new FlowLayoutPanel()
            {
                Dock = DockStyle.Bottom,
                Height = result.Ok_Button.Height + extraMargin,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false
            };

            result.apertureSize_Text.Left = extraMargin;
            result.apertureSize.Left = extraMargin;

            result.form.FormClosing += result.Form_FormClosing;

            result.Ok_Button.Click += result.Ok_Button_Click;
            result.Cancel_Button.Click += result.Cancel_Button_Click;
            result.Aply_Button.Click += result.Aply_Button_Click;


            result.apertureSize_Text.Height = ButtonContainer.Height / 2;
            result.apertureSize_Text.Width = HistogramWidth;
            result.apertureSize_Text.Top = extraMargin;

            result.apertureSize.Height = ButtonContainer.Height / 2;
            result.apertureSize.Width = HistogramWidth;
            result.apertureSize.Top = result.apertureSize_Text.Top
                                    + result.apertureSize_Text.Height;

            result.apertureSize_Value.Left = HistogramWidth + extraMargin;
            result.apertureSize_Value.Top = result.apertureSize.Top;

            result.apertureSize.ValueChanged += result.Value_ValueChanged;

            result.form.Height = ButtonContainer.Height
                                + ButtonContainer.Height * 2
                                + 64;
            result.form.Width = HistogramWidth
                                + result.apertureSize_Value.Width
                                + extraMargin * 3;

            result.form.Controls.Add(result.apertureSize_Text);
            result.form.Controls.Add(result.apertureSize);
            result.form.Controls.Add(result.apertureSize_Value);

            result.form.Controls.Add(ButtonContainer);
            ButtonContainer.Controls.Add(result.Aply_Button);
            ButtonContainer.Controls.Add(result.Cancel_Button);
            ButtonContainer.Controls.Add(result.Ok_Button);
            //result.form.Show();

            return result;
        }
    }



    class EdgeDetectionLaplacePopup : IPopup
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

        public Label apertureSize_Text;
        public TrackBar apertureSize;
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
