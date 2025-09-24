namespace JishoTangoAssistant.Domain.Models.Common.Data.ListOperations;

public record ClearListOperation<T>(ICollection<T> Copy) : ListOperation;