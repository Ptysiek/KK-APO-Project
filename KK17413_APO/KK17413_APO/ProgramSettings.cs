using System.Collections.Generic;
using KK17413_APO.ProgramSettings_Tools;
using KK17413_APO.Forms_and_Pages;

namespace KK17413_APO
{
    public static class ProgramSettings
    {
        public static LanguageFactory Language = new LanguageFactory();

        public static ColorManagerFactory ColorManager = new ColorManagerFactory();

        public static FileVerification FileVerification = new FileVerification();

        public static List<ImageForm> Pages = new List<ImageForm>();


    }
}
