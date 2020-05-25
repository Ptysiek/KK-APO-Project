using KK17413_APO_REMASTER.BackEnd.Factories;
using KK17413_APO_REMASTER.BackEnd.ImageFormComponents;
using KK17413_APO_REMASTER.FrontEnd.Forms_and_Popups;
using KK17413_APO_REMASTER.FrontEnd.Views_and_Expanded_Panels;


namespace KK17413_APO_REMASTER.BackEnd
{
    public class ImageForm_Service
    {
        public Program PROGRAM;

        public ImageWindow imageWindow;
        public ImageWindow_HandlePanel imageHandle;
        public ImageForm_Data data;

        //public List<i_Popups> ActivePopups;



        public void CloseWindow()
        {
            PROGRAM.CloseWindow(this);
        }

        public void ShowWindow()
        {
            PROGRAM.ShowWindow(imageWindow);
        }

        public void HideAllWindowsExceptOne()
        {
            PROGRAM.HideAllWindowsExceptOne(imageWindow);
        }


        public void ReloadLanguage(Language LanguageSet)
        {
            imageWindow.ReloadLanguage(LanguageSet);
            // PopUpy...
        }
        public void ReloadColorSet(ColorSet ColorSet)
        {
            imageWindow.ReloadColorSet(ColorSet);
            imageHandle.ReloadColorSet(ColorSet);
            // PopUpy...
        }

    }
}
