using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JishoTangoAssistant.Core.Models;

namespace JishoTangoAssistant.Infrastructure.Entities;

using SimilarMeaningGroup = IEnumerable<string>;

public record VocabularyItemEntity
{
    [Key]
    public int Id { get; set; }

    [MaxLength(32)]
    public string? AnkiGuid { get; set; }
    
    [MaxLength(200)]
    public string Word { get; init; }
    
    public bool ShowReading { get; init; }

    [MaxLength(500)]
    public string Reading { get; init; }

    public List<SimilarMeaningGroup> Meanings { get; init; }

    [MaxLength(2000)]
    public string? AdditionalCommentsJapanese { get; init; }
    
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
}