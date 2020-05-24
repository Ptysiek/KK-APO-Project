using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK17413_APO_REMASTER.BackEnd.Factories.FormBuilders
{
    abstract public class i_Form
    {
        abstract public void ReloadLanguage(Language LanguageSet);
        abstract public void ReloadColorSet(ColorSet ColorSet);
    }
}
