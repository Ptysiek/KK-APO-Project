using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK17413_APO
{
    class ProgramLanguage
    {
        public ProgramLanguage()
        {
            // Set default language:
            SetLanguage("ANG");
            SetLanguage("PL");
        }

        private Dictionary<string, Language> languageList = new Dictionary<string, Language>()
        {
            { "PL", new PL_Language() },
            { "ANG", new ANG_Language() }
        };

        private Language currentLanguage = null; // Currently chosen language


        public string GetValue(string key)
        {
            if (currentLanguage == null) return "";
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
    }
}
