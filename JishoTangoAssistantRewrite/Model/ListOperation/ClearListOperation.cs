using System.Collections.Generic;

namespace JishoTangoAssistantRewrite.Model.ListOperation
{
    internal class ClearListOperation<T> : ListOperation<T>
    {
        internal readonly IList<T> copy;

        internal ClearListOperation(IList<T> copy)
        {
            this.copy = copy;
        }
    }
}
