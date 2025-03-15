namespace JishoTangoAssistant.Common.Data.ListOperations;

internal record RemoveListOperation<T>(T RemovedItem, int Index) : Common.Data.ListOperations.ListOperation;