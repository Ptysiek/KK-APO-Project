using KK17413_APO_REMASTER.BackEnd.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KK17413_APO_REMASTER.BackEnd
{
    public class Program
    {
        Form_Factory FORM_FACTORY;
        Language_Factory LANGUAGE_FACTORY;
        ColorSet_Factory COLORSET_FACTORY;
        ImageOperations_Factory IMAGEOPERATIONS_FACTORY;

        public Program()
        {
            FORM_FACTORY = new Form_Factory();
            LANGUAGE_FACTORY = new Language_Factory();
            COLORSET_FACTORY = new ColorSet_Factory();
            IMAGEOPERATIONS_FACTORY = new ImageOperations_Factory();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());
            Application.Run(MainForm_Builder.GetResult());
        }



    }
}
