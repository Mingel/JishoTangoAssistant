namespace JishoTangoAssistant.Domain.Core.Models;

public record JishoTangoAssistantProfile
{
    public IReadOnlyList<VocabularyItem> VocabularyItems = [];
    public required ExportSettings ExportSettings;
}