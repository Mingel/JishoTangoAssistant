using System.Collections.Generic;

namespace JishoTangoAssistant.Model.ListOperation;

internal class CopyToOperation<T> : ListOperation<T>
{
    internal readonly IList<T> replacedItems;
    internal readonly int arrayIndex;

    internal CopyToOperation(IList<T> replacedItems, int arrayIndex)
    {
        this.replacedItems = replacedItems;
        this.arrayIndex = arrayIndex;
    }
}