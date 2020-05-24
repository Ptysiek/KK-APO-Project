using KK17413_APO_REMASTER.FrontEnd.Forms_and_Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KK17413_APO_REMASTER.BackEnd.Factories.FormBuilders
{
    class FormBuilder_MainForm : i_Builder
    {
        private MainForm Product;

        public override void PrepareNewProduct()
        {
            Product = new MainForm();
        }



        public MainForm GetResult()
        {
            return this.Product;
        }
        public override void Clear()
        => Product = null;       
    }
}
