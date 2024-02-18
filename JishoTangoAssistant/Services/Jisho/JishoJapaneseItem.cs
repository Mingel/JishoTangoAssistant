using Newtonsoft.Json;

namespace JishoTangoAssistant.Services.Jisho;

class JishoJapaneseItem
{
    [JsonProperty("word")]
    public string Word { get; set; } = string.Empty;

    [JsonProperty("reading")]
    public string Reading { get; set; } = string.Empty;
}