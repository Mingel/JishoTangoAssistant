using Newtonsoft.Json;

namespace JishoTangoAssistant.Services.Jisho;

[JsonObject]
class JishoMessage
{
    [JsonProperty("meta")]
    public JishoMeta Meta { get; set; } = new();

    [JsonProperty("data")]
    public JishoDatum[] Data { get; set; } = [];
}

class JishoMeta
{
    [JsonProperty("status")]
    public int Status { get; set; }
}

class JishoDatum
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