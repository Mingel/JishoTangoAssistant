namespace JishoTangoAssistant.Model.ListOperation
{
    internal class RemoveAtListOperation<T> : ListOperation<T>
    {
        internal readonly int index;
        internal readonly T removedItem;

        internal RemoveAtListOperation(int index, T removedItem)
        {
            this.index = index;
            this.removedItem = removedItem;
        }
    }
}
