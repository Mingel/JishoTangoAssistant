namespace JishoTangoAssistant.Models.ListOperation;

internal record RemoveAtListOperation<T>(int Index, T RemovedItem) : ListOperation;