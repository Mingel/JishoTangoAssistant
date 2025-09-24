using JishoTangoAssistant.Domain.Models.Common.Helpers;
using Newtonsoft.Json;

namespace JishoTangoAssistant.Domain.Core.Models;

using SimilarMeaningGroup = IEnumerable<string>;

public record VocabularyItem
{
    public VocabularyItem()
    {
        Word = string.Empty;
        Reading = string.Empty;
        Meanings = [];
    }
    
    public VocabularyItem(string Word, 
        bool ShowReading, 
        string Reading, 
        List<SimilarMeaningGroup> Meanings, 
        string? AdditionalCommentsJapanese = null)
    {
        this.Word = Word;
        this.ShowReading = ShowReading;
        this.Reading = Reading;
        this.Meanings = Meanings;
        this.AdditionalCommentsJapanese = AdditionalCommentsJapanese;
    }

    public string? AnkiGuid { get; set; }
    public int Order { get; set; }
    public string Word { get; init; }
    public bool ShowReading { get; init; }
    public string Reading { get; init; }
    public List<SimilarMeaningGroup> Meanings { get; init; }
    
    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
    public string? AdditionalCommentsJapanese { get; init; }

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