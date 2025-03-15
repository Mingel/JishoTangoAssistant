using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JishoTangoAssistant.Core.Models;
using JishoTangoAssistant.Infrastructure.Dtos;
using Newtonsoft.Json;

namespace JishoTangoAssistant.Infrastructure.ApiClients;

public class JishoWebService : IJishoWebService
{
    private const string BaseUrl = "https://jisho.org/";
    private const string Endpoint = "api/v1/search/words";
    
    private readonly HttpClient client = new();
    
    public async Task<IEnumerable<JishoDatum>?> GetResultAsync(string keyword)
    {
        try
        {
            // caching
            var tempWordFilePath = GetTempWordFilePath(keyword);
            var resultFromCache = await GetResultFromCacheAsync(tempWordFilePath);

            if (resultFromCache != null)
                return resultFromCache;

            var escapedKeyword = Uri.EscapeDataString(keyword);
            
            var urlBuilder = new UriBuilder(BaseUrl)
            {
                Path = Endpoint,
                Query = $"keyword={escapedKeyword}"
            };
            using var response = await client.GetAsync(urlBuilder.Uri);
            if (!response.IsSuccessStatusCode)
                return null;
            var jsonContent = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<JishoMessageDto>(jsonContent);
            if (json == null)
                return null;
            json = RemoveWikipediaEntries(json);
            await File.WriteAllTextAsync(tempWordFilePath, JsonConvert.SerializeObject(json));
            return json.Data.Select(JishoDatum.FromDto);
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
        foreach (var datum in messageDto.Data)
        {
            if (datum.Attribution.DbPedia != "false")
                datum.Senses = datum.Senses.Where(sense => !sense.PartsOfSpeech.Contains("Wikipedia definition")).ToArray();
        }

        if (messageDto.Data.Any(datum => datum.Senses.Count == 0))
            messageDto.Data = messageDto.Data.Where(datum => datum.Senses.Count > 0).ToArray();

        return messageDto;
    }

    private static string GetTempWordFilePath(string keyword)
    {
        var tmpPath = Path.GetTempPath();
        var tmpAppPath = Path.Combine(tmpPath, "JishoTangoAssistant");
        var tmpWordFilename = string.Join("_", keyword.Split(Path.GetInvalidFileNameChars())) + ".json";
        return Path.Combine(tmpAppPath, tmpWordFilename);
    }

    private static async Task<IEnumerable<JishoDatum>?> GetResultFromCacheAsync(string tempWordFilePath)
    {
        var directory = Path.GetDirectoryName(tempWordFilePath);
        if (directory != null && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        else if (File.Exists(tempWordFilePath) &&
                 File.GetLastWriteTime(tempWordFilePath) > DateTimeOffset.Now.AddDays(-30))
        {
            var fileContent = await File.ReadAllTextAsync(tempWordFilePath);
            var fileJson = JsonConvert.DeserializeObject<JishoMessageDto>(fileContent);

            return fileJson?.Data.Select(JishoDatum.FromDto);
        }
        return null;
    }
}
