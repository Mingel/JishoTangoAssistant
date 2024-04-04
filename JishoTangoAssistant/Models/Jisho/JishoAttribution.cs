using Newtonsoft.Json;

namespace JishoTangoAssistant.Models.Jisho;

public record JishoAttribution([property: JsonProperty("jmdict")] bool JmDict = default, 
        [property: JsonProperty("jmnedict")] bool JmNedict = default, 
        [property: JsonProperty("dbpedia")] string DbPedia = "");
        