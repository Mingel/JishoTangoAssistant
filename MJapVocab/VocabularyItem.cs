using System;
using System.Text;

namespace MJapVocab
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

        public static string ListToJapEng(VocabularyItem[] items)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in items)
            {
                sb.Append(item.Word);
                sb.Append(";\"");
                if (item.ShowReading)
                {
                    sb.Append(item.Reading.Replace("\"","\"\""));
                    sb.Append(Environment.NewLine);
                }
                sb.Append(item.Output.Replace("\"", "\"\""));
                sb.Append("\"");
                sb.Append(Environment.NewLine);
            }
            return sb.ToString().TrimEnd();
        }

        public static string ListToEngJap(VocabularyItem[] items)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in items)
            {
                sb.Append("\"");
                sb.Append(item.Output.Replace("\"", "\"\""));
                sb.Append("\";\"");
                sb.Append(item.Word);
                if (item.ShowReading)
                {
                    sb.Append(Environment.NewLine);
                    sb.Append(item.Reading.Replace("\"", "\"\""));
                }
                sb.Append("\"");
                sb.Append(Environment.NewLine);
            }
            return sb.ToString().TrimEnd();
        }
    }
}
