using System;

namespace JishoTangoAssistant.Model
{
    public class VocabularyItem : IEquatable<VocabularyItem>
    {
        public string Word { get; }
        public bool ShowReading { get; }
        public string Reading { get; }
        public string Output { get; }

        public VocabularyItem(string word, bool showReading, string reading, string output)
        {
            Word = word ?? throw new ArgumentNullException(nameof(word));
            ShowReading = showReading;
            Reading = reading ?? throw new ArgumentNullException(nameof(reading));
            Output = output ?? throw new ArgumentNullException(nameof(output));
        }

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
