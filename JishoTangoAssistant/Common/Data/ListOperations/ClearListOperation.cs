using System.Collections.Generic;

namespace JishoTangoAssistant.Common.Data.ListOperations;

internal record ClearListOperation<T>(ICollection<T> Copy) : ListOperation;