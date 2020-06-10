using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KK17413_APO_REMASTER.BackEnd.Factories.PopupsBuilders
{
    public class OneValue_Popup
    {
        public Form form = new Form();
        public Button OKButton = new Button();


        public OneValue_Popup()
        {
            form.Controls.Add(OKButton);

            form.Show();
        }
    }
}
