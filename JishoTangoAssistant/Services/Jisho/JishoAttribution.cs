using Newtonsoft.Json;

namespace JishoTangoAssistant.Services.Jisho;

class JishoAttribution
{
    [JsonProperty("jmdict")]
    public bool JmDict { get; set; }

    [JsonProperty("jmnedict")]
    public bool JmNedict { get; set; }

    [JsonProperty("dbpedia")]
    public string DbPedia { get; set; } = string.Empty;
}