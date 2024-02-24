using Newtonsoft.Json;

namespace JishoTangoAssistant.Model.Jisho;

public record JishoDatum
{
    [JsonProperty("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonProperty("japanese")]
    public JishoJapaneseItem[] Japanese { get; set; } = [];

    [JsonProperty("senses")]
    public JishoSense[] Senses { get; set; } = [];

    [JsonProperty("attribution")]
    public JishoAttribution Attribution { get; set; } = new();
}