using System;
using System.ComponentModel.DataAnnotations;

namespace JishoTangoAssistant.Models;

public record VocabularyItem([property: MaxLength(200)] string Word, bool ShowReading, [property: MaxLength(500)] string Reading, [property: MaxLength(5000)] string Output)
{
    [Key]
    public int Id { get; set; }
    public int Order { get; set; }

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