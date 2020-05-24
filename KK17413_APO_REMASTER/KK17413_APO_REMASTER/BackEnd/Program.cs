using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KK17413_APO_REMASTER.BackEnd.Factories;
using KK17413_APO_REMASTER.FrontEnd.Forms_and_Popups;


namespace KK17413_APO_REMASTER.BackEnd
{
    public class Program
    {
        public MainWindow MainWindow;
        //public List<ref ImageForm> ImageForms;

        Language_Factory LANGUAGE_FACTORY;
        ColorSet_Factory COLORSET_FACTORY;
        ImageOperations_Factory IMAGEOPERATIONS_FACTORY;

        public Program()
        {
            LANGUAGE_FACTORY = new Language_Factory();
            COLORSET_FACTORY = new ColorSet_Factory();
            IMAGEOPERATIONS_FACTORY = new ImageOperations_Factory();

            Build_MainForm();
            Application.Run(MainWindow.Form);
        }

        private void Build_MainForm()
        {
            FormBuilder_MainWindow builder = new FormBuilder_MainWindow();

            builder.PrepareNewForm();

            builder.Init_Language_tsmis(LANGUAGE_FACTORY.Keys());
            builder.Init_ColorSet_tsmis(COLORSET_FACTORY.Keys());
            builder.SetTransparencyKey(COLORSET_FACTORY.Transparent);
            builder.SetProgramReference(this);

            MainWindow = builder.GetResult();
            builder.Clear();

            ReloadLanguage_All();
            ReloadColorSet_All();
        }



        public void SetLanguage(string key)
        {
            if (LANGUAGE_FACTORY.SetLanguage(key)){
                ReloadLanguage_All();
            }
        }

        public void ReloadLanguage_All()
        {
            MainWindow.ReloadLanguage(LANGUAGE_FACTORY.CurrentLanguage);

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
            MainWindow.ReloadColorSet(COLORSET_FACTORY.CurrentColorSet);

            //foreach (var imageform in FORM_FACTORY.ImageForms)
            //    imageform.ReloadColorSet(COLORSET_FACTORY.CurrentColorSet);
        }
    }
}
