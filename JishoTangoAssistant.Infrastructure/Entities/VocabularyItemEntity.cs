using System.ComponentModel.DataAnnotations;
using JishoTangoAssistant.Domain.Core.Models;

namespace JishoTangoAssistant.Infrastructure.Entities;

using SimilarMeaningGroup = IEnumerable<string>;

public record VocabularyItemEntity
{
    [Key]
    public int Id { get; set; }

    [MaxLength(32)]
    public string? AnkiGuid { get; set; }
    
    [MaxLength(200)]
    public string Word { get; set; } = string.Empty;
    
    public bool ShowReading { get; set; }

    [MaxLength(500)]
    public string Reading { get; set; } = string.Empty;

    public List<SimilarMeaningGroup> Meanings { get; set; } = [];

    [MaxLength(2000)]
    public string? AdditionalCommentsJapanese { get; set; }
    
    public int Order { get; set; }

    public VocabularyItem MapToModel()
    {
        return new VocabularyItem
        {
            AnkiGuid = AnkiGuid,
            Word = Word,
            ShowReading = ShowReading,
            Reading = Reading,
            Meanings = [..Meanings],
            AdditionalCommentsJapanese = AdditionalCommentsJapanese
        };
    }

    public static VocabularyItemEntity MapFromModel(VocabularyItem vocabularyItem)
    {
        return new VocabularyItemEntity
        {
            Id = 0,
            AnkiGuid = vocabularyItem.AnkiGuid,
            Word = vocabularyItem.Word,
            ShowReading = vocabularyItem.ShowReading,
            Reading = vocabularyItem.Reading,
            Meanings = vocabularyItem.Meanings.ToList(),
            AdditionalCommentsJapanese = vocabularyItem.AdditionalCommentsJapanese,
            Order = vocabularyItem.Order
        };
    }
}