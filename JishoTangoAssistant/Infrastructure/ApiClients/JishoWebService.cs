using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JishoTangoAssistant.Core.Models;
using JishoTangoAssistant.Infrastructure.Dtos;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace JishoTangoAssistant.Infrastructure.ApiClients;

public class JishoWebService(HttpClient client) : IJishoWebService
{
    private const string BaseUrl = "https://jisho.org/";
    private const string Endpoint = "api/v1/search/words";

    private readonly MemoryCache cache = new(new MemoryCacheOptions());
    private readonly TimeSpan cacheDuration = TimeSpan.FromMinutes(5);

    public async Task<IEnumerable<JishoDatum>?> GetResultAsync(string keyword)
    {
        try
        {
            var escapedKeyword = Uri.EscapeDataString(keyword);
            var urlBuilder = new UriBuilder(BaseUrl)
            {
                Path = Endpoint,
                Query = $"keyword={escapedKeyword}"
            };
            var url = urlBuilder.Uri.ToString();
            return await GetDataWithMemoryCacheAsync(url);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Stacktrace: {ex.StackTrace}");
            throw;
        }
    }

    private static JishoMessageDto RemoveWikipediaEntries(JishoMessageDto messageDto)
    {
        foreach (var messageDtoData in messageDto.Data)
        {
            if (messageDtoData.Attribution.DbPedia != "false")
                messageDtoData.Senses = messageDtoData.Senses.Where(sense => !sense.PartsOfSpeech.Contains("Wikipedia definition")).ToArray();
        }

        if (messageDto.Data.Any(datum => datum.Senses.Count == 0))
            messageDto.Data = messageDto.Data.Where(datum => datum.Senses.Count > 0).ToArray();

        return messageDto;
    }

    private async Task<IEnumerable<JishoDatum>?> GetDataWithMemoryCacheAsync(string url)
    {
        if (cache.TryGetValue(url, out IEnumerable<JishoDatum>? cachedData))
        {
            Console.WriteLine($"Get data from cache: {url}");
            return cachedData;
        }

        Console.WriteLine($"Get data from URL: {url}");
        var response = await client.GetAsync(url);
        if (!response.IsSuccessStatusCode)
            return null;
        var jsonContent = await response.Content.ReadAsStringAsync();
        var json = JsonConvert.DeserializeObject<JishoMessageDto>(jsonContent);
        if (json == null)
            return null;
        json = RemoveWikipediaEntries(json);
        var datumList = json.Data.Select(JishoDatum.FromDto).ToList();
        cache.Set(url, datumList, cacheDuration);
        return datumList;
    }
}
