using System;

namespace JishoTangoAssistant.Model
{
    public record VocabularyItem(string Word, bool ShowReading, string Reading, string Output)
    {
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
}
