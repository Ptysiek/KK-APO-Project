using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KK17413_APO.Panels_Expanded
{

    [System.ComponentModel.DesignerCategory("")]
    public class InfoPanel : Panel
    {

        public FlowLayoutPanel infoLabelsContainer;
        public List<Label> infoLabels;



        public InfoPanel()
        {
            infoLabelsContainer = new FlowLayoutPanel();
            infoLabels = new List<Label>();

            for (int i=0; i<5; ++i)            
                infoLabels.Add(new Label());

            this.Controls.Add(infoLabelsContainer);
        }


        public void ReloadImageInfo(Image image, string filename)
        {
            infoLabels[0].Text = "image width:  " + image.Width.ToString();
            infoLabels[1].Text = "image height:  " + image.Height.ToString();
            infoLabels[2].Text = "horizontal resolution:  " + image.HorizontalResolution.ToString();
            infoLabels[3].Text = "vertical resolution:  " + image.VerticalResolution.ToString();
            infoLabels[4].Text = " ";

            infoLabels.Add(new Label() { Text = "image pixel format:  " });
            CalculatePixelFormat(image.PixelFormat.ToString(), filename);

            infoLabels.Add(new Label() { Text = " " });

            infoLabels.Add(new Label() { Text = "image flags:  " });
            CalculatePictureFlags(image.Flags);


            int labelsHEIGHT = infoLabels[0].Font.Height + 3;
            int labelsWIDTH = infoLabelsContainer.Width - 20;

            foreach (var label in infoLabels)
            {
                label.AutoEllipsis = true;
                label.AutoSize = false;
                label.Height = labelsHEIGHT;
                label.Width = labelsWIDTH;
                infoLabelsContainer.Controls.Add(label);
            }

            //fileInfo_iwn.PanelHeight = labelsHEIGHT * (2 + infoLabels.Count);
        }


        public void ResizeInfoLabels()
        {
            if (infoLabels != null)
                foreach (var label in infoLabels)
                {
                    label.Width = infoLabelsContainer.Width - 20;
                }
        }


        private void CalculatePixelFormat(string value, string filename)
        {
            var inf = new FileInfo(filename);

            string tmp = "    ";

            for (int i = 1; i < inf.Extension.Length; ++i)
                tmp += inf.Extension[i];

            tmp += " ";

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

            infoLabels.Add(new Label() { Text = tmp });

            tmp = "    ";
            for (; ind < value.Length; ++ind)
                tmp += value[ind];

            infoLabels.Add(new Label() { Text = tmp });
        }

        private void CalculatePictureFlags(int value)
        {
            if (value == 0)
            {
                infoLabels.Add(new Label() { Text = "    [ ImageFlagsNone ]" });
            }
            else
            {
                if (value >= 131072)
                {
                    infoLabels.Add(new Label() { Text = "    [ ImageFlagsCaching ]" });
                    value -= 131072;
                }
                if (value >= 65536)
                {
                    infoLabels.Add(new Label() { Text = "    [ ImageFlagsReadOnly ]" });
                    value -= 65536;
                }
                if (value >= 8192)
                {
                    infoLabels.Add(new Label() { Text = "    [ ImageFlagsHasRealPixelSize ]" });
                    value -= 8192;
                }
                if (value >= 4096)
                {
                    infoLabels.Add(new Label() { Text = "    [ ImageFlagsHasRealDPI ]" });
                    value -= 4096;
                }
                if (value >= 256)
                {
                    infoLabels.Add(new Label() { Text = "    [ ImageFlagsColorSpaceYCCK ]" });
                    value -= 256;
                }
                if (value >= 128)
                {
                    infoLabels.Add(new Label() { Text = "    [ ImageFlagsColorSpaceYCBCR ]" });
                    value -= 128;
                }
                if (value >= 64)
                {
                    infoLabels.Add(new Label() { Text = "    [ ImageFlagsColorSpaceGRAY ]" });
                    value -= 64;
                }
                if (value >= 32)
                {
                    infoLabels.Add(new Label() { Text = "    [ ImageFlagsColorSpaceCMYK ]" });
                    value -= 32;
                }
                if (value >= 16)
                {
                    infoLabels.Add(new Label() { Text = "    [ ImageFlagsColorSpaceRGB ]" });
                    value -= 16;
                }
                if (value >= 8)
                {
                    infoLabels.Add(new Label() { Text = "    [ ImageFlagsPartiallyScalable ]" });
                    value -= 8;
                }
                if (value >= 4)
                {
                    infoLabels.Add(new Label() { Text = "    [ ImageFlagsHasTranslucent ]" });
                    value -= 4;
                }
                if (value >= 2)
                {
                    infoLabels.Add(new Label() { Text = "    [ ImageFlagsHasAlpha ]" });
                    value -= 2;
                }
                if (value >= 1)
                {
                    infoLabels.Add(new Label() { Text = "    [ ImageFlagsScalable ]" });
                }
            }
        }
    }
}
