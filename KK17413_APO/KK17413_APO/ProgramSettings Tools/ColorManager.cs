using System;
using System.Collections.Generic;
using System.Drawing;


namespace KK17413_APO.ProgramSettings_Tools
{
    class ColorManager
    {
        protected ColorManager() { }

        protected Dictionary<string, Color> colorFields_Dict;

        public Color GetValue(string key)
        {
            if (!colorFields_Dict.ContainsKey(key)) return Color.Empty;
            if (colorFields_Dict[key] == null) return Color.Empty;

            return colorFields_Dict[key];
        }

        public List<string> Keys()
        {
            if (colorFields_Dict == null) { return null; }

            List<string> result = new List<string>();
            foreach (string key in colorFields_Dict.Keys)
            {
                result.Add(key);
            }
            result.Sort();
            return result;
        }
    }

    //##################################################################################################
    class Vs_ColorSet : ColorManager
    {
        public Vs_ColorSet()
        {
            colorFields_Dict = new Dictionary<string, Color>()
            {
                { "fontColor", Color.FromArgb(255, 255, 255) },
                { "bgColorLayer1", Color.FromArgb(45, 45, 48) },
                { "bgColorLayer2", Color.FromArgb(30, 30, 30) },
                { "bgColorLayer3", Color.FromArgb(37, 37, 38) },
                { "bgColorLayer4", Color.FromArgb(62, 62, 66) },
                { "detailColor1", Color.FromArgb(0, 122, 204) },
                { "detailColor2", Color.FromArgb(104, 104, 104) },
                { "detailColor3", Color.FromArgb(215, 172, 106) }
            };
        }
    }
}




