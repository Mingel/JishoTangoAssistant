using Newtonsoft.Json;

namespace JishoTangoAssistant.Services.Jisho;

[JsonObject]
class JishoMessage
{
    [JsonProperty("meta")]
    public JishoMeta Meta { get; set; } = new JishoMeta();

    [JsonProperty("data")]
    public JishoDatum[] Data { get; set; } = new JishoDatum[0];
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
    public JishoJapaneseItem[] Japanese { get; set; } = new JishoJapaneseItem[0];

    [JsonProperty("senses")]
    public JishoSense[] Senses { get; set; } = new JishoSense[0];

    [JsonProperty("attribution")]
    public JishoAttribution Attribution = new JishoAttribution();
}

class JishoJapaneseItem
{
    [JsonProperty("word")]
    public string Word { get; set; } = string.Empty;

    [JsonProperty("reading")]
    public string Reading { get; set; } = string.Empty;
}

class JishoSense
{
    [JsonProperty("english_definitions")]
    public string[] EnglishDefinitions { get; set; } = new string[0];

    [JsonProperty("parts_of_speech")]
    public string[] PartsOfSpeech { get; set; } = new string[0];

    [JsonProperty("tags")]
    public string[] Tags { get; set; } = new string[0];
}

class JishoAttribution
{
    [JsonProperty("jmdict")]
    public bool JmDict { get; set; }

    [JsonProperty("jmnedict")]
    public bool JmNedict { get; set; }

    [JsonProperty("dbpedia")]
    public string DbPedia { get; set; } = string.Empty;
}
