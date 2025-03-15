namespace JishoTangoAssistant.Common.Data.ListOperations;

internal record AssignmentListOperation<T>(int Index, T ReplacedItem) : ListOperation;