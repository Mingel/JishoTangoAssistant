using JishoTangoAssistant.Shared.Constants;

namespace JishoTangoAssistant.Domain.Core.Models;

public record ExportSettings
{
    public int FontSize = Constants.DefaultFontSize;
    public string AnkiDeckName = string.Empty;
}