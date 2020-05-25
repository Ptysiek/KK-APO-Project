using KK17413_APO_REMASTER.BackEnd.DataStructures;


namespace KK17413_APO_REMASTER.BackEnd.Factories.Image_Operations
{
    public abstract class IOperation
    {
        abstract public ImageData GetResult(ImageForm_Service x);
    }
}
