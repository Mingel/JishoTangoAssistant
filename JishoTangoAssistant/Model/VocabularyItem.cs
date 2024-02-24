using System;

namespace JishoTangoAssistant.Model
{
    public class VocabularyItem(string word, bool showReading, string reading, string output)
        : IEquatable<VocabularyItem>
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Word { get; set; } = word;
        public bool ShowReading { get; set; } = showReading;
        public string Reading { get; set; } = reading;
        public string Output { get; set; } = output;

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
}
