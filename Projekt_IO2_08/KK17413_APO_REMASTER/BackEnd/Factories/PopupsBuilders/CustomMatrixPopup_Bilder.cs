using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using KK17413_APO_REMASTER.BackEnd.DataStructures;
using KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations;
using KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded;


namespace KK17413_APO_REMASTER.BackEnd.Factories.PopupsBuilders
{
    class CustomMatrixPopup_Bilder : IPopupBuilder
    {
        public override IPopup GetResult()
        {
            int extraMargin = 5;

            CustomMatrixPopup result = new CustomMatrixPopup
            {
                form = new Form(),
                //Histogram = Histogram_Builder.GetResult(Color.White),
                matrixpanel1 = MatrixPanel_Builder.GetResult(3),
                matrixpanel2 = MatrixPanel_Builder.GetResult(3),
                matrixpanel3 = MatrixPanel_Builder.GetResult(3),
                Ok_Button = new Button() { Text = "ok" },
                Cancel_Button = new Button() { Text = "cancel" },
                Aply_Button = new Button() { Text = "apply" },

                value = new TrackBar()
            };


            result.matrixpanel1.DisablePanels();
            result.matrixpanel2.DisablePanels();
            result.matrixpanel3.DisablePanels();

            // ---------------------------------------------

            result.matrixpanel1.panels[0][0].Text = "0";
            result.matrixpanel1.panels[0][1].Text = "-1";
            result.matrixpanel1.panels[0][2].Text = "0";

            result.matrixpanel1.panels[1][0].Text = "-1";
            result.matrixpanel1.panels[1][1].Text = "4";
            result.matrixpanel1.panels[1][2].Text = "-1";            

            result.matrixpanel1.panels[2][0].Text = "0";
            result.matrixpanel1.panels[2][1].Text = "-1";
            result.matrixpanel1.panels[2][2].Text = "0";

            // ---------------------------------------------

            result.matrixpanel2.panels[0][0].Text = "-1";
            result.matrixpanel2.panels[0][1].Text = "-1";
            result.matrixpanel2.panels[0][2].Text = "-1";

            result.matrixpanel2.panels[1][0].Text = "-1";
            result.matrixpanel2.panels[1][1].Text = "8";
            result.matrixpanel2.panels[1][2].Text = "-1";

            result.matrixpanel2.panels[2][0].Text = "-1";
            result.matrixpanel2.panels[2][1].Text = "-1";
            result.matrixpanel2.panels[2][2].Text = "-1";

            // ---------------------------------------------

            result.matrixpanel3.panels[0][0].Text = "1";
            result.matrixpanel3.panels[0][1].Text = "-2";
            result.matrixpanel3.panels[0][2].Text = "1";

            result.matrixpanel3.panels[1][0].Text = "-2";
            result.matrixpanel3.panels[1][1].Text = "4";
            result.matrixpanel3.panels[1][2].Text = "-2";

            result.matrixpanel3.panels[2][0].Text = "1";
            result.matrixpanel3.panels[2][1].Text = "-2";
            result.matrixpanel3.panels[2][2].Text = "1";

            // ---------------------------------------------

            result.matrixpanel1.Top = extraMargin;
            result.matrixpanel2.Top = extraMargin;
            result.matrixpanel3.Top = extraMargin;

            result.matrixpanel1.Left = extraMargin;
            result.matrixpanel2.Left = result.matrixpanel1.Left + result.matrixpanel1.Width + extraMargin;
            result.matrixpanel3.Left = result.matrixpanel2.Left + result.matrixpanel2.Width + extraMargin;



            /*
            float[,] k;
            
            k = new float[,] { { 0, -1,  0},
                                {-1,  4, -1},
                                { 0, -1,  0} };

            foreach (var v in k)
                Console.WriteLine(v);
            */





            FlowLayoutPanel ButtonContainer = new FlowLayoutPanel()
            {
                Dock = DockStyle.Bottom,
                Height = result.Ok_Button.Height + extraMargin,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false
            };

            //result.Histogram.Top = extraMargin;
            //result.Histogram.Left = extraMargin;
            result.value.Left = extraMargin;

            result.form.FormClosing += result.Form_FormClosing;

            result.Ok_Button.Click += result.Ok_Button_Click;
            result.Cancel_Button.Click += result.Cancel_Button_Click;
            result.Aply_Button.Click += result.Aply_Button_Click;

            result.value.Height = ButtonContainer.Height / 2;
            result.value.Width = result.matrixpanel3.Left + result.matrixpanel3.Width - extraMargin * 6;
            result.value.Top = result.matrixpanel3.Top + result.matrixpanel3.Height + extraMargin ;

            result.value.Maximum = 2;
            result.value.Value = 1;
            result.value.Minimum = 0;

            result.value.ValueChanged += result.Value_ValueChanged;

            result.form.Height = result.matrixpanel1.Height
                                + ButtonContainer.Height
                                + result.value.Height
                                + 64;
            result.form.Width = result.matrixpanel1.Width * 3
                                + extraMargin * 3;

            //result.form.Controls.Add(result.Histogram);
            result.form.Controls.Add(result.matrixpanel1);
            result.form.Controls.Add(result.matrixpanel2);
            result.form.Controls.Add(result.matrixpanel3);
            result.form.Controls.Add(result.value);

            result.form.Controls.Add(ButtonContainer);
            ButtonContainer.Controls.Add(result.Aply_Button);
            ButtonContainer.Controls.Add(result.Cancel_Button);
            ButtonContainer.Controls.Add(result.Ok_Button);
            //result.form.Show();

            return result;
        }
    }



    class CustomMatrixPopup : IPopup
    {
        private Program PROGRAM;
        private ImageForm_Service SERVICE;
        private IOperation OPERATION;

        private string OperationIDName;
        private ImageData LastModification;

        public Form form;
        //public Histogram Histogram;
        public MatrixPanel matrixpanel1;
        public MatrixPanel matrixpanel2;
        public MatrixPanel matrixpanel3;

        public Button Ok_Button;
        public Button Cancel_Button;
        public Button Aply_Button;

        public TrackBar value;

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

            //MaxValue.Value = SERVICE.data.LastData().data.maxValue;
            //value.Value = MaxValue.Value / 2;

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
                value.Value
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
