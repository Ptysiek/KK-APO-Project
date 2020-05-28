using KK17413_APO_REMASTER.BackEnd.DataStructures;
using KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations;


namespace KK17413_APO_REMASTER.BackEnd.Factories.PopupsBuilders
{
    public abstract class IPopup
    {
        public abstract void Start(Program program, ImageForm_Service service, IOperation operation, string operationName);

        public abstract void ReloadLanguage();

        public abstract void ReloadLanguage(Language LanguageSet);

        public abstract void ReloadColorSet();

        public abstract void ReloadColorSet(ColorSet ColorSet);

    }
}
