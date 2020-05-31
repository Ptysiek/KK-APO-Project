using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using KK17413_APO_REMASTER.BackEnd.DataStructures;
using KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations;
using KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded;


namespace KK17413_APO_REMASTER.BackEnd.Factories.PopupsBuilders
{
    class ChooseSecondImagePopup_Bilder : IPopupBuilder
    {
        public override IPopup GetResult()
        {
            int extraMargin = 5;
            Histogram toScaleHistogram = Histogram_Builder.GetResult(Color.White);
            int HistogramHeight = toScaleHistogram.Height;
            int HistogramWidth = toScaleHistogram.Width;


            ChooseSecondImagePopup result = new ChooseSecondImagePopup
            {
                form = new Form(),
                BrowseButton = new Button() { Text = "Browse Second Image", AutoSize = true },

                Ok_Button = new Button() { Text = "ok" },
                Cancel_Button = new Button() { Text = "cancel" },
                Aply_Button = new Button() { Text = "apply" },

                dragNdropContainer = new Panel() { AllowDrop = true, Dock = DockStyle.Top },
                dragNdropText1 = Get_dragNdropLabel("Drop your image here", 26),
                dragNdropText2 = Get_dragNdropLabel("[ bmp, jpg, png, tiff ]", 13),

                valuebar = new TrackBar(),
                valuebar_Text = new Label(),
                FileNameLabel = new Label() { Text = "Choose Second Image!" , AutoSize = true },
            };

            FlowLayoutPanel ButtonContainer = new FlowLayoutPanel()
            {
                Dock = DockStyle.Bottom,
                Height = result.Ok_Button.Height + extraMargin,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false
            };



            result.form.Resize += result.Form_Resize;
            result.form.FormClosing += result.Form_FormClosing;

            result.Ok_Button.Click += result.Ok_Button_Click;
            result.Cancel_Button.Click += result.Cancel_Button_Click;
            result.Aply_Button.Click += result.Aply_Button_Click;

            result.dragNdropContainer.Height = HistogramHeight * 2 / 3;

            result.BrowseButton.Top = result.dragNdropContainer.Bottom + extraMargin;
            result.BrowseButton.Left = extraMargin;

            result.FileNameLabel.Top = result.BrowseButton.Bottom + extraMargin;
            result.FileNameLabel.Left = extraMargin;


            result.form.Height = result.dragNdropContainer.Height
                                + HistogramHeight / 2
                                + 64;
            result.form.Width = HistogramWidth
                                + result.valuebar_Text.Width
                                + extraMargin * 3;


            result.valuebar.ValueChanged += result.Value_ValueChanged;

            result.BrowseButton.Click += result.Open_tsmi_Click;
            result.dragNdropContainer.DragDrop += result.DragNdropContainer_DragDrop;
            result.dragNdropContainer.DragEnter += result.DragNdropContainer_DragEnter;
            //result.dragNdropContainer.MouseMove += result.MouseFix_MouseMove;


            result.form.Controls.Add(result.valuebar);
            result.form.Controls.Add(result.valuebar_Text);


            result.form.Controls.Add(result.FileNameLabel);
            result.form.Controls.Add(result.BrowseButton);
            result.form.Controls.Add(result.dragNdropContainer);
            result.dragNdropContainer.Controls.Add(result.dragNdropText1);
            result.dragNdropContainer.Controls.Add(result.dragNdropText2);

            result.form.Controls.Add(ButtonContainer);
            ButtonContainer.Controls.Add(result.Aply_Button);
            ButtonContainer.Controls.Add(result.Cancel_Button);
            ButtonContainer.Controls.Add(result.Ok_Button);

            result.valuebar.Height = ButtonContainer.Height / 2;
            result.valuebar.Width = HistogramWidth;
            result.valuebar.Top = ButtonContainer.Top
                                  - result.valuebar.Height
                                  - extraMargin;

            result.valuebar_Text.Left = HistogramWidth + extraMargin;
            result.valuebar_Text.Top = result.valuebar.Top;

            return result;
        }


        private Label Get_dragNdropLabel(string value, int fontsize)
        {
            Label dragNdropText = new Label()
            {
                Text = value,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
            };
            dragNdropText.Font = new Font(dragNdropText.Font.Name, fontsize, dragNdropText.Font.Style);
            return dragNdropText;
        }
    }



    class ChooseSecondImagePopup : IPopup
    {
        private Program PROGRAM;
        private ImageForm_Service SERVICE;
        private IOperation OPERATION;

        private string OperationIDName;
        private ImageData LastModification;

        private Bitmap lastBitmap = null;
        public Label FileNameLabel;

        public Form form;
        public Button BrowseButton;
        public Button Ok_Button;
        public Button Cancel_Button;
        public Button Aply_Button;


        public Panel dragNdropContainer;
        public Label dragNdropText1;
        public Label dragNdropText2;

        public TrackBar valuebar;
        public Label valuebar_Text;


        private bool wait = true;



        public void Form_Resize(object sender, EventArgs e)
        {
            ResizeItems();
        }
        public void ResizeItems()
        {
            dragNdropText1.Top = (dragNdropContainer.Height / 2) - (dragNdropText1.Height / 2);
            dragNdropText2.Top = dragNdropText1.Top + dragNdropText1.Height;

            dragNdropText1.Left = (dragNdropContainer.Width / 2) - dragNdropText1.Width / 2;
            dragNdropText2.Left = (dragNdropContainer.Width / 2) - dragNdropText2.Width / 2;
        }

        public void DragNdropContainer_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        public void DragNdropContainer_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (files == null)
                return;

            if (files[0] == null)
                return;

            if (PROGRAM.TestFile(files[0]))
            {
                FileNameLabel.Text = files[0];
                lastBitmap = new Bitmap(files[0]);
            }
        }

        public void Open_tsmi_Click(object sender, EventArgs e)
        {
            string file = PROGRAM.BrowseFile();

            if (file == null)
                return;

            if (PROGRAM.TestFile(file))
            {
                FileNameLabel.Text = file;
                lastBitmap = new Bitmap(file);
            }
        }

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

            valuebar.Maximum = 1024;
            valuebar.Minimum = 0;

            valuebar.Value = 5;

            ReloadLanguage();
            ReloadColorSet();
            ResizeItems();

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

            //Ok_Button.Text = LanguageSet.GetValue("Ok_Button");
            //Cancel_Button.Text = LanguageSet.GetValue("Cancel_Button");
            //Aply_Button.Text = LanguageSet.GetValue("Apply_Button");
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

            dragNdropText1.ForeColor = ColorSet.GetValue("detailColor2");
            dragNdropText2.ForeColor = ColorSet.GetValue("detailColor2");
            dragNdropContainer.BackColor = ColorSet.GetValue("bgColorLayer3");
        }



        public void Value_ValueChanged(object sender, EventArgs e)
        {
            valuebar_Text.Text = valuebar.Value.ToString();

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
                valuebar.Value
            };
        }

        private Bitmap GetBitmap()
        {            
            return lastBitmap;
        }

        private void Recalculations()
        {
            if (lastBitmap == null)
                return;

            LastModification = OPERATION.GetResult(SERVICE, GetBitmap(), GetArgs());

            // Show Preview:
            SERVICE.imageWindow.ReloadImageData_All(LastModification);
        }
    }
}



