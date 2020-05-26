using KK17413_APO_REMASTER.BackEnd.DataStructures;
using KK17413_APO_REMASTER.BackEnd.Factories;
using KK17413_APO_REMASTER.BackEnd.ImageFormComponents;
using KK17413_APO_REMASTER.FrontEnd.Forms_and_Popups;
using KK17413_APO_REMASTER.FrontEnd.Views_and_Expanded_Panels;
using System;

namespace KK17413_APO_REMASTER.BackEnd
{
    public class ImageForm_Service
    {
        public Program PROGRAM;

        public ImageForm imageWindow;
        public HandlePanel_ImageWindow imageHandle;
        public ImageForm_Data data;

        //public List<i_Popups> ActivePopups;


        #pragma warning disable IDE0060
        public void CloseWindow()
        {
            imageWindow.form.Close();
            PROGRAM.CloseWindow(this);
        }
        public void CloseWindow(ImageForm img)
        {
            PROGRAM.CloseWindow(this);
        }
        #pragma warning restore IDE0060
        public void ShowWindow()
        {
            PROGRAM.ShowWindow(imageWindow);
        }

        public void HideAllWindowsExceptOne()
        {
            PROGRAM.HideAllWindowsExceptOne(imageWindow);
        }


        
        public void ImageOperation(string tsmi)
        {    
            ImageData newData = PROGRAM.RunOperation(this, tsmi);

            imageWindow.CloseProgressBar();                

            if (newData == null)
                return;

            data.Add(newData);
            imageWindow.ReloadImageData_All(newData);
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
