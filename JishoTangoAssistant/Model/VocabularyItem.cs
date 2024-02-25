using System;
using System.ComponentModel.DataAnnotations;

namespace JishoTangoAssistant.Model;

public class VocabularyItem(string word, bool showReading, string reading, string output)
    : IEquatable<VocabularyItem>
{
    [Key]
    public int Id { get; set; }
    public int Order { get; set; }
    [MaxLength(200)]
    public string Word { get; init; } = word;
    public bool ShowReading { get; init; } = showReading;
    [MaxLength(500)]
    public string Reading { get; init; } = reading;
    [MaxLength(5000)]
    public string Output { get; init; } = output;

    public override bool Equals(object? obj)
    {
        return Equals(obj as VocabularyItem);
    }

    public bool Equals(VocabularyItem? other)
    {
        if (other == null)
            return false;

        return Word == other.Word &&
               ShowReading == other.ShowReading &&
               Reading == other.Reading &&
               Output == other.Output;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Word, Reading, ShowReading, Output);
    }
}