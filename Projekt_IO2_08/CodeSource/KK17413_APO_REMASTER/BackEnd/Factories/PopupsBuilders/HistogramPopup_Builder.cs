using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using KK17413_APO_REMASTER.BackEnd.DataStructures;
using KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations;
using KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded;


namespace KK17413_APO_REMASTER.BackEnd.Factories.PopupsBuilders
{
    class HistogramPopup_Bilder : IPopupBuilder
    {
        public override IPopup GetResult()
        {
            int extraMargin = 5;

            HistogramPopup result = new HistogramPopup
            {
                form = new Form(),
                Histogram = Histogram_Builder.GetResult(Color.White),
                Ok_Button = new Button() { Text = "ok" },
                Cancel_Button = new Button() { Text = "cancel" },
                Aply_Button = new Button() { Text = "apply" },

                value = new TrackBar(),
                MaxValue = new TrackBar(),

                value_Value = new Label(),
                MaxValue_Value = new Label(),

                ifMaxValue = new CheckBox()
            };

            FlowLayoutPanel ButtonContainer = new FlowLayoutPanel()
            {
                Dock = DockStyle.Bottom,
                Height = result.Ok_Button.Height + extraMargin,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false
            };

            result.Histogram.Top = extraMargin;
            result.Histogram.Left = extraMargin;
            result.value.Left = extraMargin;
            result.MaxValue.Left = extraMargin;

            result.form.FormClosing += result.Form_FormClosing;

            result.Ok_Button.Click += result.Ok_Button_Click;
            result.Cancel_Button.Click += result.Cancel_Button_Click;
            result.Aply_Button.Click += result.Aply_Button_Click;

            result.value.Height = ButtonContainer.Height / 2;
            result.value.Width = result.Histogram.Width;
            result.value.Top = result.Histogram.Height + extraMargin;            

            result.MaxValue.Height = result.value.Height;
            result.MaxValue.Width = result.value.Width;
            result.MaxValue.Top = result.value.Top + result.value.Height;

            result.value.Maximum = 255;
            result.value.Minimum = 0;

            result.MaxValue.Maximum = 255;
            result.MaxValue.Minimum = 0;

            result.value.ValueChanged += result.Value_ValueChanged;
            result.MaxValue.ValueChanged += result.Value_ValueChanged;

            result.form.Height = result.Histogram.Height 
                                + ButtonContainer.Height 
                                + result.value.Height
                                + result.MaxValue.Height
                                + 64;
            result.form.Width = result.Histogram.Width 
                                + result.value_Value.Width
                                + extraMargin * 3;

            result.value_Value.Top = result.value.Top;
            result.value_Value.Left = result.value.Width + extraMargin;

            result.MaxValue_Value.Top = result.MaxValue.Top;
            result.MaxValue_Value.Left = result.MaxValue.Width + extraMargin;

            result.form.Controls.Add(result.Histogram);
            result.form.Controls.Add(result.value);
            result.form.Controls.Add(result.MaxValue);

            result.form.Controls.Add(result.value_Value);
            result.form.Controls.Add(result.MaxValue_Value);

            result.form.Controls.Add(ButtonContainer);
            ButtonContainer.Controls.Add(result.Aply_Button);
            ButtonContainer.Controls.Add(result.Cancel_Button);
            ButtonContainer.Controls.Add(result.Ok_Button);
            //result.form.Show();

            return result;
        }
    }



    class HistogramPopup : IPopup
    {
        private Program PROGRAM;
        private ImageForm_Service SERVICE;
        private IOperation OPERATION;

        private string OperationIDName;
        private ImageData LastModification;

        public Form form;
        public Histogram Histogram;
        public Button Ok_Button;
        public Button Cancel_Button;
        public Button Aply_Button;

        public TrackBar value;
        public TrackBar MaxValue;

        public Label value_Value;
        public Label MaxValue_Value;

        public CheckBox ifMaxValue;
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

            if (!SERVICE.data.LastData().Ready)
                SERVICE.ImageOperation("RecalculateHistogramData_tsmi");

            this.Histogram.ReloadHistogram(SERVICE.data.LastData().data.data);           

            MaxValue.Value = SERVICE.data.LastData().data.maxValue;
            value.Value = MaxValue.Value / 2;

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
            this.Histogram.BackColor = ColorSet.GetValue("bgHistogram");

            form.ForeColor = ColorSet.GetValue("fontColor");
            form.BackColor = ColorSet.GetValue("bgColorLayer1");
        }



        public void Value_ValueChanged(object sender, EventArgs e)
        {
            value_Value.Text = value.Value.ToString();
            MaxValue_Value.Text = MaxValue.Value.ToString();

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

            if (!SERVICE.data.LastData().Ready)
                SERVICE.ImageOperation("RecalculateHistogramData_tsmi");

            this.Histogram.ReloadHistogram(SERVICE.data.LastData().data.data);
        }

        private List<int> GetArgs()
        {
            return new List<int>()
            {
                value.Value,
                MaxValue.Value,
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
