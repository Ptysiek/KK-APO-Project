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
            HistogramPopup result = new HistogramPopup
            {
                form = new Form(),
                Histogram = Histogram_Builder.GetResult(Color.White),
                Ok_Button = new Button() { Text = "ok" },
                Cancel_Button = new Button() { Text = "cancel" },
                Aply_Button = new Button() { Text = "apply" }
            };

            FlowLayoutPanel ButtonContainer = new FlowLayoutPanel()
            {
                Dock = DockStyle.Bottom,
                Height = result.Ok_Button.Height + 5,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false
            };

            result.Ok_Button.Click += result.Ok_Button_Click;
            result.Cancel_Button.Click += result.Cancel_Button_Click;
            result.Aply_Button.Click += result.Aply_Button_Click;

            result.form.Height = result.Histogram.Height + ButtonContainer.Height + 64;
            result.form.Width = result.Histogram.Width;

            result.form.Controls.Add(result.Histogram);
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
        private ImageForm_Service SERVICE;
        private IOperation OPERATION;

        private string OperationName;
        private ImageData LastModification;

        public Form form;
        public Histogram Histogram;
        public Button Ok_Button;
        public Button Cancel_Button;
        public Button Aply_Button;

        
        public override void Start(ImageForm_Service service, IOperation operation, string operationName)
        {
            this.SERVICE = service;
            this.OPERATION = operation;
            this.OperationName = operationName;

            ImageData newData = operation.GetResult(service, GetArgs());
            service.DataOperation(newData, operationName);
            this.form.Show();
        }


        public void Cancel_Button_Click(object sender, EventArgs e)
        {
            form.Close();
            //SERVICE.ClosePopup(this);
        }
        public void Ok_Button_Click(object sender, EventArgs e)
        {
            form.Close();
            //SERVICE.ClosePopup(this);
            SERVICE.DataOperation(LastModification, OperationName);
        }
        public void Aply_Button_Click(object sender, EventArgs e)
        {
            SERVICE.DataOperation(LastModification, OperationName);
        }

        private List<int> GetArgs()
        {
            return new List<int>()
            {
                
            };
        }
        private void Recalculations()
        {
            LastModification = OPERATION.GetResult(SERVICE, GetArgs());
        }
    }
}
