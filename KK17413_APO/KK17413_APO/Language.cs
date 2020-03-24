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
                { "File_tsmi", "Plik" },
                { "Open_tsmi", null }

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
                { "File_tsmi", "File" },
                { "Open_tsmi", "Open" }

                //  *tsmi - Tool Strip Menu Item
                // ---------------------------------------------------------------------------------------------
            };
        }
    }
}
