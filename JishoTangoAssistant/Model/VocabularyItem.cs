using System;

namespace JishoTangoAssistant.Model
{
    public class VocabularyItem(string word, bool showReading, string reading, string output)
        : IEquatable<VocabularyItem>
    {
        public string Word { get; } = word ?? throw new ArgumentNullException(nameof(word));
        public bool ShowReading { get; } = showReading;
        public string Reading { get; } = reading ?? throw new ArgumentNullException(nameof(reading));
        public string Output { get; } = output ?? throw new ArgumentNullException(nameof(output));

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
