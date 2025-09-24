using System.Text;
using JishoTangoAssistant.Domain.Models.Core.Collections;
using JishoTangoAssistant.Domain.Models.Core.Models;

namespace JishoTangoAssistant.Application.Core.Utils;

public static class VocabularyListExporter
{
    private const string VisualNewLine = "<br>";

    public static string JapaneseToEnglish(ReadOnlyObservableVocabularyList items, ExportSettings exportSettings)
    {
        var sb = new StringBuilder();

        sb.Append(AddFileHeader());

        foreach (var item in items)
        {
            sb.Append(item.AnkiGuid + "-j2e");
            sb.Append(';');
            var word = exportSettings.FontSize >= 0 ? AddFontSizeHtml(exportSettings.FontSize, item.Word, true) : item.Word;
            sb.Append(word);
            sb.Append(";\"");
            if (item.ShowReading)
            {
                sb.Append(item.Reading.Replace("\"", "\"\""));
                sb.Append(VisualNewLine);
            }
            // TODO do not flatten when using counting system for meanings
            var meaningValues = string.Join("; ", item.Meanings.SelectMany(g => g));
            sb.Append(string.Join(VisualNewLine, meaningValues).Replace("\"", "\"\""));
            if (!string.IsNullOrWhiteSpace(item.AdditionalCommentsJapanese))
            {
                sb.Append(VisualNewLine);
                sb.Append(item.AdditionalCommentsJapanese.Trim().Replace("\"", "\"\""));
            }
            sb.AppendLine("\"");
        }
        return sb.ToString().TrimEnd();
    }

    public static string EnglishToJapanese(ReadOnlyObservableVocabularyList items, ExportSettings exportSettings)
    {
        var sb = new StringBuilder();
        
        sb.Append(AddFileHeader());

        foreach (var item in items)
        {
            sb.Append(item.AnkiGuid + "-e2j");
            sb.Append(';');
            sb.Append('"');
            // TODO do not flatten when using counting system for meanings
            var meaningValues = string.Join("; ", item.Meanings.SelectMany(g => g));
            sb.Append(string.Join(VisualNewLine, meaningValues).Replace("\"", "\"\""));
            if (!string.IsNullOrWhiteSpace(item.AdditionalCommentsJapanese))
            {
                sb.Append(VisualNewLine);
                sb.Append(item.AdditionalCommentsJapanese.Replace("\"", "\"\""));
            }
            sb.Append("\";\"");
            if (exportSettings.FontSize >= 0)
                sb.Append(AddFontSizeHtml(exportSettings.FontSize, item.Word));
            else
                sb.Append(item.Word);
            if (item.ShowReading)
            {
                sb.Append(VisualNewLine);
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
