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

        public static string ListToJapEng(VocabularyItem[] items)
        {
            StringBuilder sb = new StringBuilder();

            // condition: if font size is set, then we need the html way for successful import
            // visualNewLine is only for setting new lines in a card, not for separating card purposes
            string visualNewLine = "<br>";

            foreach (var item in items)
            {
                if (CurrentSession.customFontSize >= 0)
                    sb.Append(AddFontSizeHtml(CurrentSession.customFontSize, item.Word, true));
                else
                    sb.Append(item.Word);
                sb.Append(";\"");
                if (item.ShowReading)
                {
                    sb.Append(item.Reading.Replace("\"", "\"\""));
                    sb.Append(visualNewLine);
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

            // condition: if font size is set, then we need the html way for successful import
            string visualNewLine = "<br>";

            foreach (var item in items)
            {
                sb.Append("\"");
                sb.Append(item.Output.Replace("\"", "\"\""));
                sb.Append("\";\"");
                if (CurrentSession.customFontSize >= 0)
                    sb.Append(AddFontSizeHtml(CurrentSession.customFontSize, item.Word));
                else
                    sb.Append(item.Word);
                if (item.ShowReading)
                {
                    sb.Append(visualNewLine);
                    sb.Append(item.Reading.Replace("\"", "\"\""));
                }
                sb.Append("\"");
                sb.Append(Environment.NewLine);
            }
            return sb.ToString().TrimEnd();
        }

        private static string AddFontSizeHtml(int fontSize, string input, bool appendQuotationMarks = false)
        {
            var result = $"<p style=\"\"font-size:{fontSize}px;\"\">{input}</p>";
            if (appendQuotationMarks)
                result = $"\"{result}\"";
            return result;
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
