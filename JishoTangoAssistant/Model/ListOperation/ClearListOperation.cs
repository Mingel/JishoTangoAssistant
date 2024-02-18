using System.Collections.Generic;

namespace JishoTangoAssistant.Model.ListOperation;

internal class ClearListOperation<T> : ListOperation<T>
{
    internal readonly IList<T> copy;

    internal ClearListOperation(IList<T> copy)
    {
        this.copy = copy;
    }
}