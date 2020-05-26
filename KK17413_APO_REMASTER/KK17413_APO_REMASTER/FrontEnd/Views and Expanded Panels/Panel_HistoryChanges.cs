using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KK17413_APO_REMASTER.FrontEnd.Views_and_Expanded_Panels
{
    public class Panel_HistoryChanges : Panel
    {
        public FlowLayoutPanel infoLabelsContainer;
        private readonly List<Label> infoLabels;


        public int LabelsHeight
        {
            get
            {
                return (infoLabels.Count > 0) ? infoLabels[0].Font.Height + 3 : 0;
            }
        }
        public int LabelsCount
        {
            get => infoLabels.Count;
        }

        public Panel_HistoryChanges()
        {
            infoLabelsContainer = new FlowLayoutPanel();
            infoLabels = new List<Label>();

            infoLabelsContainer.Dock = DockStyle.Fill;

            int labelsWIDTH = infoLabelsContainer.Width - 20;
            this.Controls.Add(infoLabelsContainer);
            for (int i = 0; i < 12; ++i)
            {
                Label newLabel = new Label
                {
                    AutoEllipsis = true,
                    AutoSize = false,
                    Height = LabelsHeight,
                    Width = labelsWIDTH
                };

                infoLabels.Add(newLabel);
                infoLabelsContainer.Controls.Add(newLabel);
            }
            ResizeInfoLabels();
        }


        public void ReloadImageInfo()
        {/*
            infoLabels[0].Text = " ";
            infoLabels[1].Text = "image width:  " + image.Width.ToString();
            infoLabels[2].Text = "image height:  " + image.Height.ToString();
            infoLabels[3].Text = "horizontal resolution:  " + image.HorizontalResolution.ToString();
            infoLabels[4].Text = "vertical resolution:  " + image.VerticalResolution.ToString();
            infoLabels[5].Text = " ";

            List<string> values = CalculatePixelFormat(image.PixelFormat.ToString(), filename);
            infoLabels[6].Text = "image pixel format:  ";
            infoLabels[7].Text = values[0];
            infoLabels[8].Text = values[1];
            infoLabels[9].Text = values[2];

            infoLabels[10].Text = " ";
            infoLabels[11].Text = "image flags:  ";
            extendedLabels.Clear();
            CalculatePictureFlags(image.Flags);

            //int labelsHEIGHT = infoLabels[0].Font.Height + 3;
            int labelsWIDTH = infoLabelsContainer.Width - 20;

            foreach (var elabel in extendedLabels)
            {
                elabel.AutoEllipsis = true;
                elabel.AutoSize = false;
                elabel.Height = LabelsHeight;
                elabel.Width = labelsWIDTH;
                infoLabelsContainer.Controls.Add(elabel);
            }

            ResizeInfoLabels();
            */
        }


        public void ResizeInfoLabels()
        {
            if (infoLabels != null)
            {
                foreach (var label in infoLabels)
                {
                    label.Width = infoLabelsContainer.Width - 20;
                }
            }
        }
    }
}
