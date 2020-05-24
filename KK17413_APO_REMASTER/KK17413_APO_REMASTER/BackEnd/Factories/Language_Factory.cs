using System;
using System.Collections.Generic;


namespace KK17413_APO_REMASTER.BackEnd.Factories
{
    public class Language_Factory
    {
        //private Language currentLanguage = null;   // Currently chosen language
        private Languages currentLanguage;   // Currently chosen language


        private Dictionary<string, Languages> languageList = new Dictionary<string, Languages>()
        {
            { "PL", new PL_Language() },
            { "ANG", new ANG_Language() }
        };


        // ##########################################################################################################
        public Language_Factory()
        {
            // Set default language:
            SetLanguage("ANG");
        }
        public Language_Factory(string key)
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
