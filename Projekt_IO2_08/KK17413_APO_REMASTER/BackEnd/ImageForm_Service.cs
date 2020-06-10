using System;
using System.Collections.Generic;

using KK17413_APO_REMASTER.BackEnd.DataStructures;
using KK17413_APO_REMASTER.BackEnd.Factories;
using KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations;
using KK17413_APO_REMASTER.BackEnd.Factories.PopupsBuilders;
using KK17413_APO_REMASTER.BackEnd.ImageFormComponents;
using KK17413_APO_REMASTER.FrontEnd.Views_and_Expanded_Panels;
using KK17413_APO_REMASTER.FrontEnd.WindowForms;

namespace KK17413_APO_REMASTER.BackEnd
{
    public class ImageForm_Service
    {
        public Program PROGRAM;

        public ImageForm imageWindow;
        public HandlePanel_ImageWindow imageHandle;
        public ImageForm_Data data;

        public List<IPopup> popupList;

        //public List<i_Popups> ActivePopups;


        #pragma warning disable IDE0060
        public void CloseWindow()
        {
            // Close its poopufps

            imageWindow.form.Close();
            PROGRAM.CloseWindow(this);
        }
        public void CloseWindow(ImageForm img)
        {
            // Program Close Its popsup
            PROGRAM.CloseWindow(this);
        }
        #pragma warning restore IDE0060
        
        public void ShowWindow()
        {
            PROGRAM.ShowWindow(imageWindow);
            // Program show its popup
        }

        public void HideAllWindowsExceptOne()
        {
            PROGRAM.HideAllWindowsExceptOne(imageWindow);
            // And show this ones popups
        }


        
        public void ImageOperation(string tsmi)
        {
            ImageData newData;
            IOperation OPERATION = PROGRAM.GiveOperation(tsmi);

            if (OPERATION == null)
                return;
            
            string decision = OPERATION.AskIfPopup();

            // ----------------------------------------------------------
            if (decision == null)
            {
                return;              
            }
            else if (decision == "NONE")
            {
                try
                {
                    newData = OPERATION.GetResult(this);
                    if (newData == null)
                    {
                        return;
                    }
                    DataOperation(newData, tsmi);
                }
                catch
                {
                    imageWindow.ReloadImageData_All(data.LastData());
                    imageWindow.ReloadModificationsList(data.modifications);
                    imageWindow.CloseProgressBar();
                    return;
                }
            }
            else
            {
                IPopup popup = PROGRAM.Build_PopupWindow(decision);
                if (popup == null)
                    return;

                //string operationName = PROGRAM.GiveOperationName(tsmi);
                popupList.Add(popup);
                popup.Start(PROGRAM, this, OPERATION, tsmi);
            }

            imageWindow.CloseProgressBar();                
        }

        public void DataOperation(ImageData newData, string operationName)
        {   
            if (newData == null)
                return;

            if (operationName == null)
                return;

            // ----------------------------------------------------------
            data.Add(newData, operationName);
            imageWindow.ReloadImageData_All(data.LastData());
            imageWindow.ReloadModificationsList(data.modifications);
        }


        public void ReloadLanguage(Language LanguageSet)
        {
            imageWindow.ReloadLanguage(LanguageSet);

            foreach (var popup in popupList)
                popup.ReloadLanguage(LanguageSet);
        }
        public void ReloadColorSet(ColorSet ColorSet)
        {
            imageWindow.ReloadColorSet(ColorSet);
            imageHandle.ReloadColorSet(ColorSet);

            foreach (var popup in popupList)
                popup.ReloadColorSet(ColorSet);
        }
    }
}
