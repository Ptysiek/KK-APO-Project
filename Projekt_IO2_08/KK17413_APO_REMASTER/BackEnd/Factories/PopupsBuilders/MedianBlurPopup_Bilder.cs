using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using KK17413_APO_REMASTER.BackEnd.DataStructures;
using KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations;
using KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded;


namespace KK17413_APO_REMASTER.BackEnd.Factories.PopupsBuilders
{
    class MedianBlurPopup_Bilder : IPopupBuilder
    {
        public override IPopup GetResult()
        {
            int extraMargin = 5;
            Histogram toScaleHistogram = Histogram_Builder.GetResult(Color.White);
            int HistogramWidth = toScaleHistogram.Width;

            MedianBlurPopup result = new MedianBlurPopup
            {
                form = new Form(),
                Ok_Button = new Button() { Text = "ok" },
                Cancel_Button = new Button() { Text = "cancel" },
                Aply_Button = new Button() { Text = "apply" },

                widthValue_Text = new Label() { Text = "ksize:   [ksize % 2 == 1]" },

                widthValue = new TrackBar(),
                widthValue_Value = new Label()
            };

            FlowLayoutPanel ButtonContainer = new FlowLayoutPanel()
            {
                Dock = DockStyle.Bottom,
                Height = result.Ok_Button.Height + extraMargin,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false
            };

            result.widthValue_Text.Left = extraMargin;
            result.widthValue.Left = extraMargin;

            result.form.FormClosing += result.Form_FormClosing;

            result.Ok_Button.Click += result.Ok_Button_Click;
            result.Cancel_Button.Click += result.Cancel_Button_Click;
            result.Aply_Button.Click += result.Aply_Button_Click;



            result.widthValue_Text.Height = ButtonContainer.Height / 2;
            result.widthValue_Text.Width = HistogramWidth;
            result.widthValue_Text.Top = extraMargin * 2;
            
            result.widthValue.Height = ButtonContainer.Height / 2;
            result.widthValue.Width = HistogramWidth;
            result.widthValue.Top = result.widthValue_Text.Top
                                    + result.widthValue_Text.Height;           



            result.widthValue_Value.Left = HistogramWidth + extraMargin;
            result.widthValue_Value.Top = result.widthValue.Top;

            result.widthValue.ValueChanged += result.Value_ValueChanged;


            result.form.Height = ButtonContainer.Height
                                + ButtonContainer.Height * 3
                                + 64;
            result.form.Width = HistogramWidth
                                + result.widthValue_Value.Width
                                + extraMargin * 3;

            result.form.Controls.Add(result.widthValue_Text);
            result.form.Controls.Add(result.widthValue);
            result.form.Controls.Add(result.widthValue_Value);

            result.form.Controls.Add(ButtonContainer);
            ButtonContainer.Controls.Add(result.Aply_Button);
            ButtonContainer.Controls.Add(result.Cancel_Button);
            ButtonContainer.Controls.Add(result.Ok_Button);
            //result.form.Show();

            return result;
        }
    }



    class MedianBlurPopup : IPopup
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

        public Label widthValue_Text;
        public TrackBar widthValue;
        public Label widthValue_Value;

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

            widthValue.Maximum = 64;
            widthValue.Value = 33;
            widthValue.Minimum = 0;

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
            
            tmp = widthValue.Value;
            if (tmp % 2 != 1)                  // ksize.width % 2 == 1
            {
                --tmp;
            }
            if (tmp <= 0)                      // ksize.width > 0
            {
                tmp = 1;
            }
            widthValue.Value = tmp;
            

            widthValue_Value.Text = widthValue.Value.ToString();

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
                widthValue.Value
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
