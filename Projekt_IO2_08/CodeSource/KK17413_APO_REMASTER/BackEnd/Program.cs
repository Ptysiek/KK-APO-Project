﻿using System.Windows.Forms;
using System.Collections.Generic;

using KK17413_APO_REMASTER.BackEnd.Factories;
using KK17413_APO_REMASTER.FrontEnd.Views_and_Expanded_Panels;
using KK17413_APO_REMASTER.BackEnd.ImageFormComponents;
using KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations;
using KK17413_APO_REMASTER.BackEnd.WindowBuilders;
using KK17413_APO_REMASTER.FrontEnd.WindowForms;
using KK17413_APO_REMASTER.BackEnd.Factories.PopupsBuilders;
using System.Runtime.InteropServices.WindowsRuntime;

namespace KK17413_APO_REMASTER.BackEnd
{
    public class Program
    {
        MainForm MainWindow;
        readonly List<ImageForm_Service> ImageWindows;

        readonly Language_Factory LANGUAGE_FACTORY;
        readonly ColorSet_Factory COLORSET_FACTORY;
        readonly ImageOperations_Factory IMAGEOPERATIONS_FACTORY;
        readonly Popup_Factory POPUP_FACTORY;

        public Program()
        {
            ImageWindows = new List<ImageForm_Service>();

            LANGUAGE_FACTORY = new Language_Factory();
            COLORSET_FACTORY = new ColorSet_Factory();
            IMAGEOPERATIONS_FACTORY = new ImageOperations_Factory();
            POPUP_FACTORY = new Popup_Factory();

            Build_MainWindow();
            Application.Run(MainWindow.Form);
        }

        #region Image Service Operations
        public IOperation GiveOperation(string operation)
        {
            if (IMAGEOPERATIONS_FACTORY == null)
                return null;

            return IMAGEOPERATIONS_FACTORY.GetOperation(operation);
        }
        /*
        public IPopup GivePopup(string decision)
        {
            if (POPUP_FACTORY == null)
                return null;

            return POPUP_FACTORY.GetValue(decision);
        }
        */
        public string GiveOperationName(string operation)
        {
            if (LANGUAGE_FACTORY == null)
                return null;

            return LANGUAGE_FACTORY.GetValue(operation);
        }

        public void ShowWindow(ImageForm imageWindow)
        {
            imageWindow.form.WindowState = FormWindowState.Normal;
            imageWindow.form.Activate();
        }

        public void HideAllWindowsExceptOne(ImageForm imageWindow)
        {
            foreach (var service in ImageWindows)
            {
                if (service.imageWindow != imageWindow)
                    service.imageWindow.form.WindowState = FormWindowState.Minimized;
            }
            imageWindow.form.WindowState = FormWindowState.Normal;
            imageWindow.form.Activate();
        }

        public void CloseWindow(ImageForm_Service service)
        {
            if (service.data != null)
            {
                service.data = null;
            }

            if (service.imageHandle != null)
            {
                MainWindow.pageHandlersContainer.Controls.Remove(service.imageHandle);
                service.imageHandle.Clear();
                service.imageHandle = null;
            }
            if (service.imageWindow != null)
            {
                service.imageWindow = null;
            }

            #pragma warning disable IDE0059
            ImageWindows.Remove(service);
            service = null;
            #pragma warning restore IDE0059
        }

        #endregion


        #region Files Verification
        public void BrowseFiles()
        {
            string[] files = FileVerification.BrowseFiles();
            if (files == null) return;

            foreach (string value in files)
                Build_ImageWindow(value);
        }        
        
        public string BrowseFile()
        {
            string file = FileVerification.BrowseFile();
            if (file == null) 
                return null;

            return (TestFile(file)) ? file : null;
        }
        
        public void TestFiles(string[] files)
        {
            foreach (string value in FileVerification.Verify(files))
                Build_ImageWindow(value);
        }
        
        public bool TestFile(string file)
        {
            return FileVerification.Verify(file);
        }

        #endregion


        #region Language Management
        public Language GetLanguage()
        {
            if (LANGUAGE_FACTORY == null)
                return null;

            return LANGUAGE_FACTORY.CurrentLanguage;
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

            foreach (var service in ImageWindows)
                service.ReloadLanguage(LANGUAGE_FACTORY.CurrentLanguage);
        }

        public void ReloadLanguage(ImageForm_Service service)
        {
            service.ReloadLanguage(LANGUAGE_FACTORY.CurrentLanguage);
        }

        #endregion


        #region Color Management
        public ColorSet GetColorSet()
        {
            if (COLORSET_FACTORY == null)
                return null;

            return COLORSET_FACTORY.CurrentColorSet;
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

            foreach (var service in ImageWindows)
                service.ReloadColorSet(COLORSET_FACTORY.CurrentColorSet);
        }

        public void ReloadColorSet(ImageForm_Service service)
        {
            service.ReloadColorSet(COLORSET_FACTORY.CurrentColorSet);
        }

        #endregion


        #region Build Window 
        public IPopup Build_PopupWindow(string whichPopup)
        {
            if (POPUP_FACTORY == null)
                return null;

            IPopupBuilder builder = POPUP_FACTORY.GetValue(whichPopup);

            if (builder == null)
                return null;


            IPopup result = builder.GetResult();

            if (result == null)
                return null;

            return result;
        }


        public void Build_ImageWindow(string filename = null)
        {
            ImageForm_Service imageForm_Service = new ImageForm_Service();

            // ---------------------------------------------------------------------------
            List<IPopup> popupList = new List<IPopup>();

            // ---------------------------------------------------------------------------
            ImageForm_Data newData = new ImageForm_Data();
            newData.CreateNewData(filename, LANGUAGE_FACTORY.GetValue("CreateNewData"));

            // ---------------------------------------------------------------------------
            FormBuilder_ImageWindow builder = new FormBuilder_ImageWindow();
            ImageForm newPage;

            builder.PrepareNewForm();

            builder.Init_Operations_tsmis(IMAGEOPERATIONS_FACTORY);
            builder.SetTransparencyKey(COLORSET_FACTORY.Transparent);
            builder.SetProgramReference(imageForm_Service);
            builder.SetEventHandlers();
            if (filename != null)
                builder.SetData(newData.LastData());

            newPage = builder.GetResult();
            newPage.ReloadModificationsList(newData.modifications);            
            newPage.form.Show();
            
            // ---------------------------------------------------------------------------
            HandlePanel_ImageWindow newPageHandle = new HandlePanel_ImageWindow(filename)
            {
                SERVICE = imageForm_Service
            };
            // Assign new page handle to the MainForm:
            MainWindow.pageHandlersContainer.Controls.Add(newPageHandle);

            // ---------------------------------------------------------------------------
            imageForm_Service.PROGRAM = this;
            imageForm_Service.imageWindow = newPage;
            imageForm_Service.imageHandle = newPageHandle;
            imageForm_Service.data = newData;
            imageForm_Service.popupList = popupList;

            ReloadLanguage(imageForm_Service);
            ReloadColorSet(imageForm_Service);

            ImageWindows.Add(imageForm_Service);
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

            ReloadLanguage_All();
            ReloadColorSet_All();
        }
        
        #endregion
    }
}



