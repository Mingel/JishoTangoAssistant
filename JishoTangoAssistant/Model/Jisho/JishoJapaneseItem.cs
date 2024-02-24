using Newtonsoft.Json;

namespace JishoTangoAssistant.Model.Jisho;

public record JishoJapaneseItem(
    [property: JsonProperty("word")] string Word = "",
    [property: JsonProperty("reading")] string Reading = "");