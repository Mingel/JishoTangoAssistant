namespace JishoTangoAssistant.Models.ListOperation;

internal class InsertListOperation<T>(int index) : ListOperation<T>
{
    internal readonly int index = index;
}