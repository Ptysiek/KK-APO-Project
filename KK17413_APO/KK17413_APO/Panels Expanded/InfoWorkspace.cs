using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KK17413_APO.Toolbox_Tools_Expanded;


namespace KK17413_APO.Panels_Expanded
{
    [System.ComponentModel.DesignerCategory("")]
    public class InfoWorkspace : Panel
    {
        public FlowLayoutPanel iwnContainer;
        public AdjustedSplitContainer histogram_iwn;
        public AdjustedSplitContainer fileInfo_iwn;
        public Panel bottomMargin_iwn;
        // *iwn - Image Workspace Nodes

        public HistogramPanel histogramPanel;
        public InfoPanel infoPanel;




        public InfoWorkspace()
        {
            iwnContainer = new FlowLayoutPanel();
            histogram_iwn = new AdjustedSplitContainer();
            fileInfo_iwn = new AdjustedSplitContainer();
            bottomMargin_iwn = new Panel();

            histogramPanel = new HistogramPanel();
            infoPanel = new InfoPanel();
        }
    }
}
