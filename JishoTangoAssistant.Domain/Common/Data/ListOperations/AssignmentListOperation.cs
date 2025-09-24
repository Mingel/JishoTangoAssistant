namespace JishoTangoAssistant.Domain.Models.Common.Data.ListOperations;

public record AssignmentListOperation<T>(int Index, T ReplacedItem) : ListOperation;