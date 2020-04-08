﻿using System;
using System.Collections.Generic;


namespace KK17413_APO.ProgramSettings_Tools
{
    public class LanguageFactory
    {        
        //private Language currentLanguage = null;   // Currently chosen language
        private Language currentLanguage;   // Currently chosen language


        private Dictionary<string, Language> languageList = new Dictionary<string, Language>()
        {
            { "PL", new PL_Language() },
            { "ANG", new ANG_Language() }
        };


        // ##########################################################################################################
        public LanguageFactory()
        {
            // Set default language:
            SetLanguage("ANG");
        }        
        public LanguageFactory(string key)
        {
            // Set start language:
            SetLanguage(key);
        }
        

        // ##########################################################################################################
        public string GetValue(string key)
        {
            if (currentLanguage == null) return null;
            return currentLanguage.GetValue(key);
        }

        public bool SetLanguage(string key)
        {
            if (!languageList.ContainsKey(key)) return false;

            currentLanguage = languageList[key];
            return true;
        }
        public List<string> Keys()
        {
            List<string> result = new List<string>();
            foreach (string key in languageList.Keys)
            {
                result.Add(key);
            }
            result.Sort();
            return result;
        }
        // ##########################################################################################################
    }
}