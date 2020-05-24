using System.Windows.Forms;
using System.Collections.Generic;
using KK17413_APO_REMASTER.BackEnd.Factories;
using KK17413_APO_REMASTER.FrontEnd.Forms_and_Popups;


namespace KK17413_APO_REMASTER.BackEnd
{
    public class Program
    {
        public MainWindow MainWindow;
        public List<ImageWindow> ImageForms;

        Language_Factory LANGUAGE_FACTORY;
        ColorSet_Factory COLORSET_FACTORY;
        ImageOperations_Factory IMAGEOPERATIONS_FACTORY;

        public Program()
        {
            LANGUAGE_FACTORY = new Language_Factory();
            COLORSET_FACTORY = new ColorSet_Factory();
            IMAGEOPERATIONS_FACTORY = new ImageOperations_Factory();

            Build_MainWindow();
            Application.Run(MainWindow.Form);
        }


        #region Files Verification
        public void BrowseFiles()
        {
            string[] files = FileVerification.BrowseFiles();
            if (files == null) return;

            foreach (string value in files)
                Build_ImageWindow(value);
        }
        
        public void TestFiles(string[] files)
        {
            foreach (string value in FileVerification.Verify(files))
                Build_ImageWindow(value);
        }

        #endregion


        #region Language Management
        public void SetLanguage(string key)
        {
            if (LANGUAGE_FACTORY.SetLanguage(key)){
                ReloadLanguage_All();
            }
        }

        public void ReloadLanguage_All()
        {
            MainWindow.ReloadLanguage(LANGUAGE_FACTORY.CurrentLanguage);

            foreach (var imageform in ImageForms)
                imageform.ReloadLanguage(LANGUAGE_FACTORY.CurrentLanguage);
        }

        #endregion


        #region Color Management
        public void SetColorSet(string key)
        {
            if (COLORSET_FACTORY.SetColorSet(key)){
                ReloadColorSet_All();
            }
        }

        public void ReloadColorSet_All()
        {
            MainWindow.ReloadColorSet(COLORSET_FACTORY.CurrentColorSet);

            foreach (var imageform in ImageForms)
                imageform.ReloadColorSet(COLORSET_FACTORY.CurrentColorSet);
        }

        #endregion


        #region Build Window 
        public void Build_ImageWindow(string filename = null)
        {
            FormBuilder_ImageWindow builder = new FormBuilder_ImageWindow();
            ImageWindow newPage;

            // Create new ImagePage:
            builder.PrepareNewForm();
            builder.Init_Operations_tsmis(IMAGEOPERATIONS_FACTORY.Keys());
            builder.SetTransparencyKey(COLORSET_FACTORY.Transparent);
            builder.SetEventHandlers();

            newPage = builder.GetResult();
            builder.Clear();

            // Create new PageHandle:
            ImageForm_Handle newPageHandle = new ImageForm_Handle(this, newPage, filename);

            // Assign new page handle to the new image page:
            newPage.PageHandle = newPageHandle;

            // Assign new page handle to the MainForm:
            pageHandlersContainer.Controls.Add(newPageHandle);

            // Add new page to the list:
            ProgramSettings.Pages.Add(newPage);
        }

        private void Build_MainWindow()
        {
            FormBuilder_MainWindow builder = new FormBuilder_MainWindow();
            builder.PrepareNewForm();

            builder.Init_Language_tsmis(LANGUAGE_FACTORY.Keys());
            builder.Init_ColorSet_tsmis(COLORSET_FACTORY.Keys());
            builder.SetTransparencyKey(COLORSET_FACTORY.Transparent);
            builder.SetProgramReference(this);
            builder.SetEventHandlers();

            MainWindow = builder.GetResult();
            builder.Clear();

            ReloadLanguage_All();
            ReloadColorSet_All();
        }
        
        #endregion
    }
}



