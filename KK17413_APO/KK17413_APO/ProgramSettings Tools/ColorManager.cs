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
                { "detailColor3", Color.FromArgb(215, 172, 106) },
                { "bgHistogram", Color.FromArgb(45, 45, 48) }
            };
        }
    }

    class WFBright_ColorSet : ColorManager
    {
        public WFBright_ColorSet()
        {
            colorFields_Dict = new Dictionary<string, Color>()
            {
                { "fontColor", Color.FromArgb(0, 0, 0) },
                { "bgColorLayer1", Color.FromArgb(255, 255, 255) },
                { "bgColorLayer2", Color.FromArgb(247, 247, 247) },
                { "bgColorLayer3", Color.FromArgb(247, 247, 247) },
                { "bgColorLayer4", Color.FromArgb(255, 0, 0) },
                { "detailColor1", Color.FromArgb(0, 122, 204) },
                { "detailColor2", Color.FromArgb(0, 0, 0) },
                { "detailColor3", Color.FromArgb(215, 172, 106) },
                { "bgHistogram", Color.FromArgb(184, 184, 184) }
            };
        }
    }
    
    class Trevilam_ColorSet : ColorManager
    {
        public Trevilam_ColorSet()
        {
            colorFields_Dict = new Dictionary<string, Color>()
            {
                { "fontColor", Color.FromArgb(255, 255, 255) },
                { "bgColorLayer1", Color.FromArgb(80, 98, 110) },
                { "bgColorLayer2", Color.FromArgb(42, 65, 79) },
                { "bgColorLayer3", Color.FromArgb(49, 72, 87) },
                { "bgColorLayer4", Color.FromArgb(178, 196, 200) },
                { "detailColor1", Color.FromArgb(0, 122, 204) },
                { "detailColor2", Color.FromArgb(255, 255, 255) },
                { "detailColor3", Color.FromArgb(215, 172, 106) },
                { "bgHistogram", Color.FromArgb(49, 72, 87) }
            };
        }
    }

    class FishBonesTheme_ColorSet : ColorManager
    {
        public FishBonesTheme_ColorSet()
        {
            colorFields_Dict = new Dictionary<string, Color>()
            {
                { "fontColor", Color.FromArgb(66, 61, 67) },
                { "bgColorLayer1", Color.FromArgb(236, 236, 234) },
                { "bgColorLayer2", Color.FromArgb(206, 206, 206) },
                { "bgColorLayer3", Color.FromArgb(212, 212, 212) },
                { "bgColorLayer4", Color.FromArgb(246, 246, 238) },
                { "detailColor1", Color.FromArgb(0, 122, 204) },
                { "detailColor2", Color.FromArgb(66, 61, 67) },
                { "detailColor3", Color.FromArgb(215, 172, 106) },
                { "bgHistogram", Color.FromArgb(184, 184, 184) }
            };
        }
    }

    class VulturTheme_ColorSet : ColorManager
    {
        public VulturTheme_ColorSet()
        {
            colorFields_Dict = new Dictionary<string, Color>()
            {
                { "fontColor", Color.FromArgb(76, 77, 79) },
                { "bgColorLayer1", Color.FromArgb(219, 216, 197) },
                { "bgColorLayer2", Color.FromArgb(155, 152, 137) },
                { "bgColorLayer3", Color.FromArgb(188, 188, 176) },
                { "bgColorLayer4", Color.FromArgb(255, 255, 255) },
                { "detailColor1", Color.FromArgb(0, 122, 204) },
                { "detailColor2", Color.FromArgb(76, 77, 79) },
                { "detailColor3", Color.FromArgb(215, 172, 106) },
                { "bgHistogram", Color.FromArgb(188, 188, 176) }
            };
        }
    }
}




