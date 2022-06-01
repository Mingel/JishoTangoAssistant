using System;
using System.Text;

namespace JishoTangoAssistant
{
    public class VocabularyItem
    {
        public string Word { get; set; }
        public bool ShowReading { get; set; }
        public string Reading { get; set; }
        public string Output { get; set; }

        public VocabularyItem(string word, bool showReading, string reading, string output)
        {
            Word = word;
            ShowReading = showReading;
            Reading = reading;
            Output = output;
        }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                VocabularyItem item = (VocabularyItem)obj;
                return (Word.Equals(item.Word)) && (Reading.Equals(item.Reading))
                    && (ShowReading.Equals(item.ShowReading)) && (Output.Equals(item.Output));
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Word, this.Reading, this.ShowReading, this.Output);
        }
    }
}
