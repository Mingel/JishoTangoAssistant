namespace JishoTangoAssistant.Domain.Models.Common.Data.ListOperations;

public record RemoveListOperation<T>(T RemovedItem, int Index) : ListOperation;