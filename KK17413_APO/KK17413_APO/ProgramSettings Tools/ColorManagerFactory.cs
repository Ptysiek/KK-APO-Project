using System;
using System.Collections.Generic;
using System.Drawing;


namespace KK17413_APO.ProgramSettings_Tools
{
    public class ColorManagerFactory
    {
        public readonly Color Transparent = Color.Maroon;

        //private Language currentLanguage = null;   // Currently chosen language
        private ColorManager currentColorSet;   // Currently chosen set
        private const string defaultSet = "VSDarkTheme";

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
            SetColorSet(defaultSet);
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
            if (!colorSetList.ContainsKey(key))
            {
                currentColorSet = colorSetList[defaultSet];
                return false;
            }
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
