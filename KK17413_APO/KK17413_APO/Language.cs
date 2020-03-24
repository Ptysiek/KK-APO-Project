using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK17413_APO
{
    class Language
    {
        protected Language() { }

        protected Dictionary<string, string> wordFields_Dict;


        public string GetValue(string key)
        {
            if (!wordFields_Dict.ContainsKey(key)) return "";
            if (wordFields_Dict[key] == null) return "";

            return wordFields_Dict[key];
        }

        public List<string> Keys()
        {
            if(wordFields_Dict == null) { return null; }

            List<string> result = new List<string>();
            foreach (string key in wordFields_Dict.Keys)
            {
                result.Add(key);
            }
            result.Sort();
            return result;
        }
    }

    //##################################################################################################
    class PL_Language : Language
    {
        public PL_Language()
        {
            wordFields_Dict = new Dictionary<string, string>()
            {
                // ---------------------------------------------------------------------------------------------
                // MAIN_FORM -----------------------------------------------------------------------------------
                { "file_tsmi", "Plik" },
                { "open_tsmi", null },
                { "settings_tsmi", "Ustawienia" },
                { "language_tsmi", "Język" }

                //  *tsmi - Tool Strip Menu Item
                // ---------------------------------------------------------------------------------------------
            };
        }
    }

    //##################################################################################################
    class ANG_Language : Language
    {
        public ANG_Language()
        {
            wordFields_Dict = new Dictionary<string, string>()
            {
                // ---------------------------------------------------------------------------------------------
                // MAIN_FORM -----------------------------------------------------------------------------------
                { "file_tsmi", "File" },
                { "open_tsmi", "Open" },
                { "settings_tsmi", "Settings" },
                { "language_tsmi", "Language" }

                //  *tsmi - Tool Strip Menu Item
                // ---------------------------------------------------------------------------------------------
            };
        }
    }
}
