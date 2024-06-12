using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using JishoTangoAssistant.Helper;
using Newtonsoft.Json;

namespace JishoTangoAssistant.Models;

using SimilarMeaningGroup = IEnumerable<string>;

public record VocabularyItem([property: MaxLength(200)] string Word, 
                                bool ShowReading, 
                                [property: MaxLength(500)] string Reading, 
                                List<SimilarMeaningGroup> Meanings, 
                                [property: MaxLength(2000), JsonProperty(NullValueHandling=NullValueHandling.Ignore)] string? AdditionalCommentsJapanese = null)
{
    [Key]
    public int Id { get; set; }
    [MaxLength(32)]
    public string? AnkiGuid { get; set; }
    public int Order { get; set; }

    public virtual bool Equals(VocabularyItem? other)
    {
        if (other == null)
            return false;

        return Word == other.Word &&
               ShowReading == other.ShowReading &&
               Reading == other.Reading &&
               Meanings.SequenceEqual(other.Meanings, new EnumerableComparer<string>()) &&
               AdditionalCommentsJapanese == other.AdditionalCommentsJapanese;
    }

    public override int GetHashCode() => HashCode.Combine(Word, Reading, ShowReading, Meanings, AdditionalCommentsJapanese);
}