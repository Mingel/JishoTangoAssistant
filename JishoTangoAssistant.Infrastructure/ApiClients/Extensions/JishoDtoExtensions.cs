using JishoTangoAssistant.Domain.Core.Models;
using JishoTangoAssistant.Infrastructure.ApiClients.Dtos;

namespace JishoTangoAssistant.Infrastructure.ApiClients.Extensions;

public static class JishoDtoExtensions
{
    public static JishoDatum ToModel(this JishoDto dto)
    {
        return new JishoDatum
        {
            Slug = dto.Slug,
            Japanese = dto.Japanese.Select(d => d.ToModel()),
            Senses = dto.Senses.Select(d => d.ToModel()),
            Attribution = dto.Attribution.ToModel(),
        };
    }

    private static JishoAttribution ToModel(this JishoAttributionDto dto)
    {
        return new JishoAttribution
        {
            JmDict = dto.JmDict,
            JmNedict = dto.JmNedict,
            DbPedia = dto.DbPedia
        };
    }

    private static JishoJapaneseItem ToModel(this JishoJapaneseItemDto dto)
    {
        return new JishoJapaneseItem
        {
            Word = dto.Word,
            Reading = dto.Reading
        };
    }

    public static JishoMessage ToModel(this JishoMessageDto dto)
    {
        return new JishoMessage
        {
            Meta = dto.Meta.ToModel(),
            Data = dto.Data.Select(d => d.ToModel())
        };
    }

    private static JishoMeta ToModel(this JishoMetaDto dto)
    {
        return new JishoMeta { Status = dto.Status };
    }

    private static JishoSense ToModel(this JishoSenseDto dto)
    {
        return new JishoSense
        {
            EnglishDefinitions = dto.EnglishDefinitions,
            PartsOfSpeech = dto.PartsOfSpeech,
            Tags = dto.Tags
        };
    }
}