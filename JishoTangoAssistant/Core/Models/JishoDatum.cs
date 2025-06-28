using System.Collections.Generic;
using System.Linq;
using JishoTangoAssistant.Infrastructure.Dtos;

namespace JishoTangoAssistant.Core.Models;

public record JishoDatum
{
    public string Slug { get; set; } = string.Empty;

    public IEnumerable<JishoJapaneseItem> Japanese { get; set; } = [];

    public IEnumerable<JishoSense> Senses { get; set; } = [];

    public JishoAttribution Attribution { get; set; } = new();

    public static JishoDatum FromDto(JishoDto dto)
    {
        return new JishoDatum
        {
            Slug = dto.Slug,
            Japanese = dto.Japanese.Select(JishoJapaneseItem.FromDto),
            Senses = dto.Senses.Select(JishoSense.FromDto),
            Attribution = JishoAttribution.FromDto(dto.Attribution),
        };
    }
}

public record JishoAttribution
{
    public bool JmDict { get; init; }
    
    public bool JmNedict { get; init; }
    
    public string DbPedia { get; init; } = string.Empty;
    
    public static JishoAttribution FromDto(JishoAttributionDto dto)
    {
        return new JishoAttribution
        {
            JmDict = dto.JmDict,
            JmNedict = dto.JmNedict,
            DbPedia = dto.DbPedia
        };
    }
}

public record JishoJapaneseItem
{
    public string Word { get; init; } = string.Empty;
    
    public string Reading { get; init; } = string.Empty;

    public static JishoJapaneseItem FromDto(JishoJapaneseItemDto dto)
    {
        return new JishoJapaneseItem
        {
            Word = dto.Word,
            Reading = dto.Reading
        };
    }
}

internal record JishoMessage
{
    public JishoMeta Meta { get; set; } = new();

    public JishoDto[] Data { get; set; } = [];
    
    public static JishoMessage FromDto(JishoMessageDto dto)
    {
        return new JishoMessage
        {
            Meta = JishoMeta.FromDto(dto.Meta),
            Data = dto.Data
        };
    }
}

public record JishoMeta
{
    public int Status { get; init; }
    
    public static JishoMeta FromDto(JishoMetaDto dto)
    {
        return new JishoMeta { Status = dto.Status };
    }
}

public record JishoSense
{
    public string[] EnglishDefinitions { get; set; } = [];

    public string[] PartsOfSpeech { get; set; } = [];

    public string[] Tags { get; set; } = [];
    
    
    public static JishoSense FromDto(JishoSenseDto dto)
    {
        return new JishoSense
        {
            EnglishDefinitions = dto.EnglishDefinitions,
            PartsOfSpeech = dto.PartsOfSpeech,
            Tags = dto.Tags
        };
    }
}