using System;
using System.Windows.Forms;


namespace KK17413_APO.Toolbox_Tools_Expanded
{
    public class DoubleClickButton : Button
    {
        public DoubleClickButton()
        {
            SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);
        }
    }
}
