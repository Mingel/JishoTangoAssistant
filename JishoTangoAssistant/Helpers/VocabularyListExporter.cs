using JishoTangoAssistant.Model;
using System;
using System.Text;

namespace JishoTangoAssistant.Services;

public static class VocabularyListExporter
{
    public static string JapaneseToEnglish(ObservableVocabularyList items)
    {
        StringBuilder sb = new StringBuilder();

        // condition: if font size is set, then we need the html way for successful import
        // visualNewLine is only for setting new lines in a card, not for separating card purposes
        const string visualNewLine = "<br>";

        foreach (var item in items)
        {
            var word = CurrentSession.customFontSize >= 0 ? AddFontSizeHtml(CurrentSession.customFontSize, item.Word, true) : item.Word;
            sb.Append(word);
            sb.Append(";\"");
            if (item.ShowReading)
            {
                sb.Append(item.Reading.Replace("\"", "\"\""));
                sb.Append(visualNewLine);
            }
            sb.Append(item.Output.Replace("\"", "\"\""));
            sb.Append('"');
            sb.Append(Environment.NewLine);
        }
        return sb.ToString().TrimEnd();
    }

    public static string EnglishToJapanese(ObservableVocabularyList items)
    {
        var sb = new StringBuilder();

        // condition: if font size is set, then we need the html way for successful import
        const string visualNewLine = "<br>";

        foreach (var item in items)
        {
            sb.Append('"');
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
            sb.Append('"');
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
}
