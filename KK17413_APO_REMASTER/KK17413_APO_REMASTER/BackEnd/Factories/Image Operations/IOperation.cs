using KK17413_APO_REMASTER.BackEnd.DataStructures;
using System.Collections.Generic;

namespace KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations
{
    public abstract class IOperation
    {
        abstract public string AskIfPopup();
        abstract public ImageData GetResult(ImageForm_Service x);
        abstract public ImageData GetResult(ImageForm_Service x, List<int> args);
    }
}
