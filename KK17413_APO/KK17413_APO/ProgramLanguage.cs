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
        }

        private Dictionary<string, Language> languageList = new Dictionary<string, Language>()
        {
            { "PL", new PL_Language() },
            { "ANG", new ANG_Language() }
        };

        private Language language; // Currently chosen language


        public bool SetLanguage(string key)
        {
            if (!languageList.ContainsKey(key)) return false;

            language = languageList[key];
            return true;
        }
        public List<string> Keys()
        {
            List<string> result = new List<string>();
            foreach (string key in languageList.Keys)
            {
                result.Add(key);
            }
            return result;
        } 


    }

    /*
    foreach( KeyValuePair<string, string> kvp in myDictionary )
    {
        Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
    }
    //*/

    class Language
    {
        private string name;

    }    

    //##################################################################################################
    class PL_Language : Language
    {
        // MAIN_FORM -----------------------------------------------------------------------------------

        //  tsmi - Tool Strip Menu Item
        public string File_tsmi() { return "Plik"; }
        public string Open_tsmi() { return "Plik"; }

    }    
    
    //##################################################################################################
    class ANG_Language : Language
    {
        // MAIN_FORM -----------------------------------------------------------------------------------

        //  tsmi - Tool Strip Menu Item
        public string File_tsmi() { return "File"; }
        public string Open_tsmi() { return "Open"; }

    }

}
