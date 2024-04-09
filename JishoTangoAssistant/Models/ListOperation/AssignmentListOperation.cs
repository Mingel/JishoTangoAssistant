namespace JishoTangoAssistant.Models.ListOperation;

internal record AssignmentListOperation<T>(int Index, T ReplacedItem) : ListOperation;