using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK17413_APO.ProgramSettings_Tools
{
    class Language
    {
        protected Language() { }

        protected Dictionary<string, string> wordFields_Dict;


        public string GetValue(string key)
        {
            if (!wordFields_Dict.ContainsKey(key)) return "[NULL-KEY]";
            if (wordFields_Dict[key] == null) return "[NULL-VALUE]";

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
                { "project_tsmi", "Projekt" },
                { "settings_tsmi", "Ustawienia" },
                { "language_tsmi", "Język" },
                { "histogram_tsmi", "Histogram" },
                { "fileInfo_tsmi", null },

                { "histogram_iwn", "Histogram" },
                { "fileInfo_iwn", "Informacje o pliku" }

                //  *tsmi - Tool Strip Menu Item
                //  **iwn - Image Workspace Nodes
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
                { "project_tsmi", "Project" },
                { "settings_tsmi", "Settings" },
                { "language_tsmi", "Language" },
                { "histogram_tsmi", "Histogram" },
                { "fileInfo_tsmi", "File Info" },

                { "histogram_iwn", "Histogram" },
                { "fileInfo_iwn", "File info" }

                //  *tsmi - Tool Strip Menu Item
                //  **iwn - Image Workspace Nodes
                // ---------------------------------------------------------------------------------------------
            };
        }
    }
}
