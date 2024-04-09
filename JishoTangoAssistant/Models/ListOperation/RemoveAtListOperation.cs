namespace JishoTangoAssistant.Models.ListOperation;

internal record RemoveListOperation<T>(T RemovedItem, int Index) : ListOperation;