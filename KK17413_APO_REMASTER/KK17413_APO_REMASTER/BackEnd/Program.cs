using KK17413_APO_REMASTER.BackEnd.Factories;
using KK17413_APO_REMASTER.FrontEnd.Forms_and_Popups;
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

            FORM_FACTORY.Build_MainForm();
            FORM_FACTORY.MainForm.Form.SetTransparencyKey(COLORSET_FACTORY.Transparent);
            FORM_FACTORY.MainForm.AssignProgramReference(this);
            FORM_FACTORY.MainForm.Init_Language_tsmis(LANGUAGE_FACTORY.Keys());
            FORM_FACTORY.MainForm.Init_ColorSet_tsmis(COLORSET_FACTORY.Keys());
            ReloadLanguage_All();
            ReloadColorSet_All();

            Application.Run(FORM_FACTORY.MainForm.Form);
        }



        public void SetLanguage(string key)
        {
            if (LANGUAGE_FACTORY.SetLanguage(key)){
                ReloadLanguage_All();
            }
        }

        public void ReloadLanguage_All()
        {
            FORM_FACTORY.MainForm.ReloadLanguage(LANGUAGE_FACTORY.CurrentLanguage);

            //foreach (var imageform in FORM_FACTORY.ImageForms)
            //    imageform.ReloadLanguage(LANGUAGE_FACTORY.CurrentLanguage);
        }




        public void SetColorSet(string key)
        {
            if (COLORSET_FACTORY.SetColorSet(key)){
                ReloadColorSet_All();
            }
        }

        public void ReloadColorSet_All()
        {
            FORM_FACTORY.MainForm.ReloadColorSet(COLORSET_FACTORY.CurrentColorSet);

            //foreach (var imageform in FORM_FACTORY.ImageForms)
            //    imageform.ReloadColorSet(COLORSET_FACTORY.CurrentColorSet);
        }
    }
}
