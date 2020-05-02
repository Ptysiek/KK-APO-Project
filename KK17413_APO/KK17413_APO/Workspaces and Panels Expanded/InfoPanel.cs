using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KK17413_APO.Panels_Expanded
{

    [System.ComponentModel.DesignerCategory("")]
    public class InfoPanel : Panel
    {

        public FlowLayoutPanel infoLabelsContainer;
        private List<Label> infoLabels;
        private List<Label> extendedLabels;

        public int labelsHeight
        {
            get
            {
                return (infoLabels.Count > 0)? infoLabels[0].Font.Height + 3 : 0;

                if (infoLabels.Count > 0)
                    return infoLabels[0].Font.Height + 3;
                else
                    return 0;
            }
        }
        public int labelsCount
        {
            get => infoLabels.Count + extendedLabels.Count;
        }

        public InfoPanel()
        {
            infoLabelsContainer = new FlowLayoutPanel();
            infoLabels = new List<Label>();
            extendedLabels = new List<Label>();

            infoLabelsContainer.Dock = DockStyle.Fill;

            int labelsWIDTH = infoLabelsContainer.Width - 20;
            this.Controls.Add(infoLabelsContainer);
            for (int i=0; i<12; ++i)
            {
                Label newLabel = new Label();

                newLabel.AutoEllipsis = true;
                newLabel.AutoSize = false;
                newLabel.Height = labelsHeight;
                newLabel.Width = labelsWIDTH;

                infoLabels.Add(newLabel);
                infoLabelsContainer.Controls.Add(newLabel);
            }
            ResizeInfoLabels();
        }


        public void ReloadImageInfo(Image image, string filename)
        {
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
                elabel.Height = labelsHeight;
                elabel.Width = labelsWIDTH;
                infoLabelsContainer.Controls.Add(elabel);
            }

            ResizeInfoLabels();
        }


        public void ResizeInfoLabels()
        {
            if (infoLabels != null) 
                foreach (var label in infoLabels)
                {
                    label.Width = infoLabelsContainer.Width - 20;
                }

            if (extendedLabels != null) 
                foreach (var label in extendedLabels)
                {
                    label.Width = infoLabelsContainer.Width - 20;
                }
        }


        private List<string> CalculatePixelFormat(string value, string filename)
        {
            List<string> result = new List<string>();
            var inf = new FileInfo(filename);

            // [0] ---------------------------------------------------------
            string tmp = "    ";

            for (int i = 1; i < inf.Extension.Length; ++i)
                tmp += inf.Extension[i];

            result.Add(tmp);

            // [1] ---------------------------------------------------------
            tmp = "    ";

            int ind = 6;
            while (value[ind] != 'b')
            {
                tmp += value[ind];
                ++ind;
            }
            ++ind; // b
            ++ind; // p
            ++ind; // p
            tmp += " bpp [ BitsPerPixel ]";

            result.Add(tmp);

            // [2] ---------------------------------------------------------
            tmp = "    ";
            for (; ind < value.Length; ++ind)
                tmp += value[ind];

            result.Add(tmp);

            //     ---------------------------------------------------------
            return result;
        }

        private void CalculatePictureFlags(int value)
        {
            if (value == 0)
            {
                extendedLabels.Add(new Label() { Text = "    [ ImageFlagsNone ]" });
            }
            else
            {
                if (value >= 131072)
                {
                    extendedLabels.Add(new Label() { Text = "    [ ImageFlagsCaching ]" });
                    value -= 131072;
                }
                if (value >= 65536)
                {
                    extendedLabels.Add(new Label() { Text = "    [ ImageFlagsReadOnly ]" });
                    value -= 65536;
                }
                if (value >= 8192)
                {
                    extendedLabels.Add(new Label() { Text = "    [ ImageFlagsHasRealPixelSize ]" });
                    value -= 8192;
                }
                if (value >= 4096)
                {
                    extendedLabels.Add(new Label() { Text = "    [ ImageFlagsHasRealDPI ]" });
                    value -= 4096;
                }
                if (value >= 256)
                {
                    extendedLabels.Add(new Label() { Text = "    [ ImageFlagsColorSpaceYCCK ]" });
                    value -= 256;
                }
                if (value >= 128)
                {
                    extendedLabels.Add(new Label() { Text = "    [ ImageFlagsColorSpaceYCBCR ]" });
                    value -= 128;
                }
                if (value >= 64)
                {
                    extendedLabels.Add(new Label() { Text = "    [ ImageFlagsColorSpaceGRAY ]" });
                    value -= 64;
                }
                if (value >= 32)
                {
                    extendedLabels.Add(new Label() { Text = "    [ ImageFlagsColorSpaceCMYK ]" });
                    value -= 32;
                }
                if (value >= 16)
                {
                    extendedLabels.Add(new Label() { Text = "    [ ImageFlagsColorSpaceRGB ]" });
                    value -= 16;
                }
                if (value >= 8)
                {
                    extendedLabels.Add(new Label() { Text = "    [ ImageFlagsPartiallyScalable ]" });
                    value -= 8;
                }
                if (value >= 4)
                {
                    extendedLabels.Add(new Label() { Text = "    [ ImageFlagsHasTranslucent ]" });
                    value -= 4;
                }
                if (value >= 2)
                {
                    extendedLabels.Add(new Label() { Text = "    [ ImageFlagsHasAlpha ]" });
                    value -= 2;
                }
                if (value >= 1)
                {
                    extendedLabels.Add(new Label() { Text = "    [ ImageFlagsScalable ]" });
                }
            }
        }
    }
}
