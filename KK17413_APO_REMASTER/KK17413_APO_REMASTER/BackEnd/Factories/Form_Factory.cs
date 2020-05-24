using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KK17413_APO_REMASTER.FrontEnd.Forms_and_Popups;


namespace KK17413_APO_REMASTER.BackEnd.Factories
{
    class Form_Factory
    {

        public MainForm MainForm;
        //public List<ref ImageForm> ImageForms;

        public void Build_MainForm()
        {
            MainForm = Form_MainForm_Builder.GetResult();
        }

    }
}

