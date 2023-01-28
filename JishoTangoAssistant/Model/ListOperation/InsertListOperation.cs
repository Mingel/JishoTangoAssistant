namespace JishoTangoAssistant.Model.ListOperation
{
    internal class InsertListOperation<T> : ListOperation<T>
    {
        internal readonly int index;

        public InsertListOperation(int index)
        {
            this.index = index;
        }
    }
}
