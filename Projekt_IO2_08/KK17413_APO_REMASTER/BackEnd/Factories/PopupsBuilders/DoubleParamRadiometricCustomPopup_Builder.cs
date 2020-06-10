using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using KK17413_APO_REMASTER.BackEnd.DataStructures;
using KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations;
using KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded;


namespace KK17413_APO_REMASTER.BackEnd.Factories.PopupsBuilders
{
    class DoubleParamRadiometricCustomPopup_Builder : IPopupBuilder
    {
        public override IPopup GetResult()
        {
            int extraMargin = 5;
            Histogram toScaleHistogram = Histogram_Builder.GetResult(Color.White);
            int HistogramWidth = toScaleHistogram.Width;

            DoubleParamRadiometricCustomPopup result = new DoubleParamRadiometricCustomPopup
            {
                form = new Form(),
                Ok_Button = new Button() { Text = "ok" },
                Cancel_Button = new Button() { Text = "cancel" },
                Aply_Button = new Button() { Text = "apply" },

                Radio_Text = new Label() { Text = "R: " },
                Radio = new TrackBar(),
                Radio_Value = new Label(),

                Value_Text = new Label() { Text = "Value: " },
                Value = new TrackBar(),
                Value_Value = new Label()
            };

            FlowLayoutPanel ButtonContainer = new FlowLayoutPanel()
            {
                Dock = DockStyle.Bottom,
                Height = result.Ok_Button.Height + extraMargin,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false
            };

            result.Radio_Text.Left = extraMargin;
            result.Radio.Left = extraMargin;

            result.form.FormClosing += result.Form_FormClosing;

            result.Ok_Button.Click += result.Ok_Button_Click;
            result.Cancel_Button.Click += result.Cancel_Button_Click;
            result.Aply_Button.Click += result.Aply_Button_Click;



            result.Radio_Text.Height = ButtonContainer.Height / 2;
            result.Radio_Text.Width = HistogramWidth;
            result.Radio_Text.Top = extraMargin * 2;

            result.Radio.Height = ButtonContainer.Height / 2;
            result.Radio.Width = HistogramWidth;
            result.Radio.Top = result.Radio_Text.Top
                                    + result.Radio_Text.Height;
            

            result.Value_Text.Height = ButtonContainer.Height / 2;
            result.Value_Text.Width = HistogramWidth;
            result.Value_Text.Top = result.Radio.Top
                                    + result.Radio.Height;
            

            result.Value.Height = ButtonContainer.Height / 2;
            result.Value.Width = HistogramWidth;
            result.Value.Top = result.Value_Text.Top
                                    + result.Value_Text.Height;



            result.Radio_Value.Left = HistogramWidth + extraMargin;
            result.Radio_Value.Top = result.Radio.Top;
            result.Value_Value.Left = HistogramWidth + extraMargin;
            result.Value_Value.Top = result.Value.Top;

            result.Radio.ValueChanged += result.Value_ValueChanged;
            result.Value.ValueChanged += result.Value_ValueChanged;


            result.form.Height = ButtonContainer.Height
                                + ButtonContainer.Height * 3
                                + 64 * 2;
            result.form.Width = HistogramWidth
                                + result.Radio_Value.Width
                                + extraMargin * 3;

            result.form.Controls.Add(result.Radio_Text);
            result.form.Controls.Add(result.Radio);
            result.form.Controls.Add(result.Radio_Value);

            result.form.Controls.Add(result.Value_Text);
            result.form.Controls.Add(result.Value);
            result.form.Controls.Add(result.Value_Value);

            result.form.Controls.Add(ButtonContainer);
            ButtonContainer.Controls.Add(result.Aply_Button);
            ButtonContainer.Controls.Add(result.Cancel_Button);
            ButtonContainer.Controls.Add(result.Ok_Button);
            //result.form.Show();

            return result;
        }
    }



    class DoubleParamRadiometricCustomPopup : IPopup
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

        public Label Radio_Text;
        public TrackBar Radio;
        public Label Radio_Value;        

        public Label Value_Text;
        public TrackBar Value;
        public Label Value_Value;

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

            int tmp = (SERVICE.data.LastData().Bitmap.Width > SERVICE.data.LastData().Bitmap.Height) ?
                             SERVICE.data.LastData().Bitmap.Width * 3/4 :
                             SERVICE.data.LastData().Bitmap.Height * 3/4;

            Radio.Maximum = tmp;
            Radio.Value = tmp / 2;
            Radio.Minimum = 0;

            Value.Maximum = 255;
            Value.Value = 128;
            Value.Minimum = 0;

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
            Radio_Value.Text = Radio.Value.ToString();
            Value_Value.Text = Value.Value.ToString();

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
                Radio.Value,
                Value.Value
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
