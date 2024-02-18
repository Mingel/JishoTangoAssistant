namespace JishoTangoAssistant.Model.ListOperation;

internal class RemoveListOperation<T> : ListOperation<T>
{
    internal readonly T removedItem;
    internal readonly int index;

    internal RemoveListOperation(T removedItem, int index)
    {
        this.removedItem = removedItem;
        this.index = index;
    }
}