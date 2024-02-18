using Newtonsoft.Json;

namespace JishoTangoAssistant.Services.Jisho;

class JishoSense
{
    [JsonProperty("english_definitions")]
    public string[] EnglishDefinitions { get; set; } = [];

    [JsonProperty("parts_of_speech")]
    public string[] PartsOfSpeech { get; set; } = [];

    [JsonProperty("tags")]
    public string[] Tags { get; set; } = [];
}