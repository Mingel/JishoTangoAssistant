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
    public string Word { get; set; }
    
    public bool ShowReading { get; set; }

    [MaxLength(500)]
    public string Reading { get; set; }

    public List<SimilarMeaningGroup> Meanings { get; set; }

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
}