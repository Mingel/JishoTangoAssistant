using System;
using System.Text;
using JishoTangoAssistant.Models;

namespace JishoTangoAssistant.Utils;

public static class VocabularyListExporter
{
    private const string visualNewLine = "<br>";

    public static string JapaneseToEnglish(ReadOnlyObservableVocabularyList items)
    {
        var sb = new StringBuilder();

        sb.Append(AddFileHeader());

        foreach (var item in items)
        {
            sb.Append(item.AnkiGuid + "-j2e");
            sb.Append(';');
            var word = CurrentSession.customFontSize >= 0 ? AddFontSizeHtml(CurrentSession.customFontSize, item.Word, true) : item.Word;
            sb.Append(word);
            sb.Append(";\"");
            if (item.ShowReading)
            {
                sb.Append(item.Reading.Replace("\"", "\"\""));
                sb.Append(visualNewLine);
            }
            sb.Append(item.Output.Replace(Environment.NewLine, visualNewLine).Replace("\"", "\"\""));
            sb.AppendLine("\"");
        }
        return sb.ToString().TrimEnd();
    }

    public static string EnglishToJapanese(ReadOnlyObservableVocabularyList items)
    {
        var sb = new StringBuilder();
        
        sb.Append(AddFileHeader());

        foreach (var item in items)
        {
            sb.Append(item.AnkiGuid + "-e2j");
            sb.Append(';');
            sb.Append('"');
            sb.Append(item.Output.Replace(Environment.NewLine, visualNewLine).Replace("\"", "\"\""));
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
            sb.AppendLine("\"");
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

    private static string AddFileHeader()
    {
        var result = "#separator:Semicolon" + Environment.NewLine +
                     "#html:true" + Environment.NewLine +
                     "#columns:ID;Front;Back" + Environment.NewLine +
                     "#guid column:1" + Environment.NewLine +
                     "#deck:Basic" + Environment.NewLine;
        return result;
    }
}
