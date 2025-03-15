namespace JishoTangoAssistant.Common.Data.ListOperations;

internal record RemoveAtListOperation<T>(int Index, T RemovedItem) : ListOperation;