using JishoTangoAssistant.Domain.Core.Models;
using JishoTangoAssistant.Infrastructure.ApiClients.Dtos;
using JishoTangoAssistant.Infrastructure.ApiClients.Extensions;
using JishoTangoAssistant.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace JishoTangoAssistant.Infrastructure.ApiClients;

public class JishoRepository(HttpClient client) : IJishoRepository
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
        var datumList = json.Data.Select(d => d.ToModel()).ToList();
        cache.Set(url, datumList, cacheDuration);
        return datumList;
    }
}
