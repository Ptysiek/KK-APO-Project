using System;
using System.Collections.Generic;
using System.Drawing;


namespace KK17413_APO_REMASTER.BackEnd.Factories
{
    public class ColorSet_Factory
    {
        public readonly Color Transparent = Color.Maroon;
        private ColorSet currentColorSet;
        private const string defaultSet = "VSDarkTheme";

        private Dictionary<string, ColorSet> colorSetList = new Dictionary<string, ColorSet>()
        {
            { "VSDarkTheme", new Vs_ColorSet() },
            { "WFBrightTheme", new WFBright_ColorSet() },
            { "TrevilamTheme", new Trevilam_ColorSet() },
            { "FishBonesTheme", new FishBonesTheme_ColorSet() },
            { "VulturTheme", new VulturTheme_ColorSet() }
        };


        // ##########################################################################################################
        public ColorSet_Factory()
        {
            // Set default color set:
            SetColorSet(defaultSet);
        }
        public ColorSet_Factory(string key)
        {
            // Set start color set:
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
