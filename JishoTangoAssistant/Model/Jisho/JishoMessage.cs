using Newtonsoft.Json;

namespace JishoTangoAssistant.Services.Jisho;

[JsonObject]
internal record JishoMessage
{
    [JsonProperty("meta")]
    public JishoMeta Meta { get; set; } = new();

    [JsonProperty("data")]
    public JishoDatum[] Data { get; set; } = [];
}