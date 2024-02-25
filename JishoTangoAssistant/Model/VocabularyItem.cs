using System;
using System.ComponentModel.DataAnnotations;

namespace JishoTangoAssistant.Model;

public record VocabularyItem(string Word, bool ShowReading, string Reading, string Output)
{
    [Key]
    public int Id { get; set; }
    public int Order { get; set; }
    [MaxLength(200)]
    public string Word { get; init; } = Word;
    public bool ShowReading { get; init; } = ShowReading;
    [MaxLength(500)]
    public string Reading { get; init; } = Reading;
    [MaxLength(5000)]
    public string Output { get; init; } = Output;

    public virtual bool Equals(VocabularyItem? other)
    {
        if (other == null)
            return false;

        return Word == other.Word &&
               ShowReading == other.ShowReading &&
               Reading == other.Reading &&
               Output == other.Output;
    }

    public override int GetHashCode() => HashCode.Combine(Word, Reading, ShowReading, Output);
}