using System.Collections.Generic;
using Newtonsoft.Json;

namespace JishoTangoAssistant.Infrastructure.Dtos;

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

public record JishoAttributionDto([property: JsonProperty("jmdict")] bool JmDict = default, 
    [property: JsonProperty("jmnedict")] bool JmNedict = default, 
    [property: JsonProperty("dbpedia")] string DbPedia = "");

public record JishoJapaneseItemDto(
    [property: JsonProperty("word")] string Word = "",
    [property: JsonProperty("reading")] string Reading = "");
    
[JsonObject]
internal record JishoMessageDto
{
    [JsonProperty("meta")]
    public JishoMetaDto Meta { get; set; } = new();

    [JsonProperty("data")]
    public JishoDto[] Data { get; set; } = [];
}

public record JishoMetaDto([property: JsonProperty("status")] int Status = default);

public record JishoSenseDto
{
    [JsonProperty("english_definitions")]
    public string[] EnglishDefinitions { get; set; } = [];

    [JsonProperty("parts_of_speech")]
    public string[] PartsOfSpeech { get; set; } = [];

    [JsonProperty("tags")]
    public string[] Tags { get; set; } = [];
}