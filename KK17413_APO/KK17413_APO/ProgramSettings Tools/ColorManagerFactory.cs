using System;
using System.Collections.Generic;
using System.Drawing;


namespace KK17413_APO.ProgramSettings_Tools
{
    public class ColorManagerFactory
    {
        //private Language currentLanguage = null;   // Currently chosen language
        private ColorManager currentColorSet;   // Currently chosen set


        private Dictionary<string, ColorManager> colorSetList = new Dictionary<string, ColorManager>()
        {
            { "VSDarkTheme", new Vs_ColorSet() },
            { "WFBrightTheme", new WFBright_ColorSet() },
            { "TrevilamTheme", new Trevilam_ColorSet() },
            { "FishBonesTheme", new FishBonesTheme_ColorSet() },
            { "VulturTheme", new VulturTheme_ColorSet() }
        };


        // ##########################################################################################################
        public ColorManagerFactory()
        {
            // Set default language:
            SetColorSet("VSDarkTheme");
        }
        public ColorManagerFactory(string key)
        {
            // Set start language:
            SetColorSet(key);
        }


        // ##########################################################################################################
        public Color GetValue(string key)
        {
            if (currentColorSet == null) return Color.Empty;
            return currentColorSet.GetValue(key);
        }

        public bool SetColorSet(string key)
        {
            if (!colorSetList.ContainsKey(key)) return false;

            currentColorSet = colorSetList[key];
            return true;
        }
        public List<string> Keys()
        {
            List<string> result = new List<string>();
            foreach (string key in colorSetList.Keys)
            {
                result.Add(key);
            }
            result.Sort();
            return result;
        }
        // ##########################################################################################################
    }
}
