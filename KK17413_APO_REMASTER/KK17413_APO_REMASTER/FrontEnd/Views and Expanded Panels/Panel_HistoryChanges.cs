using KK17413_APO_REMASTER.BackEnd.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KK17413_APO_REMASTER.FrontEnd.Views_and_Expanded_Panels
{
    [System.ComponentModel.DesignerCategory("")]
    public class Panel_HistoryChanges : Panel
    {
        public FlowLayoutPanel infoLabelsContainer;
        private List<Label> infoLabels;


        public int LabelsHeight
        {
            get
            {
                if (infoLabels == null) return 0;

                return (infoLabels.Count > 0) ? infoLabels[0].Font.Height + 3 : 0;
            }
        }
        public int LabelsCount
        {
            get { return (infoLabels == null)? 0 : infoLabels.Count; }
        }


        public void ReloadInfo(List<Modification> modifications)
        {
            if (modifications == null)
                return;
            
            if (modifications.Count < 1)
                return;

            if (infoLabelsContainer != null)
            {
                infoLabelsContainer.Dispose();
                infoLabelsContainer = null;
            }
            infoLabelsContainer = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(1, 5, 30, 1),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true
            };
            this.Controls.Add(infoLabelsContainer);


            if (infoLabels != null)
            {
                infoLabels.Clear();
                infoLabels = null;
            }
            infoLabels = new List<Label>();

            Label tmp = new Label();
            infoLabels.Add(tmp);
            //infoLabelsContainer.Controls.Add(tmp);
            int labelsWIDTH = infoLabelsContainer.Width - 20;

            Console.WriteLine(modifications.Count);
            foreach (var value in modifications)
            {
                Console.WriteLine(value.info);
                Console.WriteLine(value.time);

                Label newLabel = new Label
                {
                    AutoEllipsis = true,
                    AutoSize = false,
                    Height = LabelsHeight,
                    Width = labelsWIDTH,
                    Text = "[" + value.time + "]   " + value.info
                };

                infoLabels.Add(newLabel);
                infoLabelsContainer.Controls.Add(newLabel);
            }

            ResizeInfoLabels();
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
