using System;
using System.Windows.Forms;


namespace KK17413_APO_REMASTER.FrontEnd.Toolbox_Tools_Expanded
{
    [System.ComponentModel.DesignerCategory("")]
    public class DoubleClickButton : Button
    {
        public DoubleClickButton()
        => SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);
    }
}
