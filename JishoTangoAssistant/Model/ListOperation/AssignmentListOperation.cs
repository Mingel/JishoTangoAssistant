namespace JishoTangoAssistant.Model.ListOperation;

internal class AssignmentListOperation<T> : ListOperation<T>
{
    internal readonly int index;
    internal readonly T replacedItem;

    internal AssignmentListOperation(int index, T replacedItem)
    {
        this.index = index;
        this.replacedItem = replacedItem;
    }
}