namespace JishoTangoAssistant.Domain.Models.Common.Data.ListOperations;

public record RemoveAtListOperation<T>(int Index, T RemovedItem) : ListOperation;