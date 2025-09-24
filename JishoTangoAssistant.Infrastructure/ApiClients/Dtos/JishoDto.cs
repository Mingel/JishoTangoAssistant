using Newtonsoft.Json;

namespace JishoTangoAssistant.Infrastructure.ApiClients.Dtos;

public record JishoDto
{
    [JsonProperty("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonProperty("japanese")]
    public IReadOnlyCollection<JishoJapaneseItemDto> Japanese { get; set; } = [];

    [JsonProperty("senses")]
    public IReadOnlyCollection<JishoSenseDto> Senses { get; set; } = [];

    [JsonProperty("attribution")]
    public JishoAttributionDto Attribution { get; set; } = new();
}

public record JishoAttributionDto([property: JsonProperty("jmdict")] bool JmDict = false, 
    [property: JsonProperty("jmnedict")] bool JmNedict = false, 
    [property: JsonProperty("dbpedia")] string DbPedia = "");

public record JishoJapaneseItemDto(
    [property: JsonProperty("word")] string Word = "",
    [property: JsonProperty("reading")] string Reading = "");
    
[JsonObject]
public record JishoMessageDto
{
    [JsonProperty("meta")]
    public JishoMetaDto Meta { get; set; } = new();

    [JsonProperty("data")]
    public IReadOnlyCollection<JishoDto> Data { get; set; } = [];
}

public record JishoMetaDto([property: JsonProperty("status")] int Status = 0);

public record JishoSenseDto
{
    [JsonProperty("english_definitions")]
    public IReadOnlyCollection<string> EnglishDefinitions { get; set; } = [];

    [JsonProperty("parts_of_speech")]
    public IReadOnlyCollection<string> PartsOfSpeech { get; set; } = [];

    [JsonProperty("tags")]
    public IReadOnlyCollection<string> Tags { get; set; } = [];
}