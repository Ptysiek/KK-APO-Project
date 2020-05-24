using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK17413_APO_REMASTER.BackEnd.Factories.FormBuilders
{
    public class FormBuilder_Director
    {
        private i_Builder Builder;
        private Language LanguageSet;
        private ColorSet ColorSet;

        public void GetResult()
        {

        }

        public void Config_Builder(i_Builder Builder)
        => this.Builder = Builder;        

        public void Config_Language(Language LanguageSet)
        => this.LanguageSet = LanguageSet;        

        public void Config_ColorSet(ColorSet ColorSet)
        => this.ColorSet = ColorSet;     
    }
}
