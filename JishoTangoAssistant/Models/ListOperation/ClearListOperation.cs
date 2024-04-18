using System.Collections.Generic;

namespace JishoTangoAssistant.Models.ListOperation;

internal record ClearListOperation<T>(ICollection<T> Copy) : ListOperation;