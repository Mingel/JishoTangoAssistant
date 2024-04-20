using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JishoTangoAssistant.Interfaces;
using JishoTangoAssistant.Models.Jisho;
using Newtonsoft.Json;

namespace JishoTangoAssistant.Services;

public class JishoWebService : IJishoWebService
{
    private const string BaseUrl = "https://jisho.org/";
    private const string Endpoint = "api/v1/search/words";
    
    private readonly HttpClient client = new();
    
    public async Task<IList<JishoDatum>?> GetResultJsonAsync(string keyword)
    {
        try
        {
            // caching
            var tempWordFilePath = GetTempWordFilePath(keyword);
            var resultFromCache = await GetResultJsonFromCacheAsync(tempWordFilePath);

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
            var json = JsonConvert.DeserializeObject<JishoMessage>(jsonContent);
            if (json == null)
                return null;
            json = RemoveWikipediaEntries(json);
            await File.WriteAllTextAsync(tempWordFilePath, JsonConvert.SerializeObject(json));
            return json.Data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Stacktrace: {ex.StackTrace}");
            throw;
        }
    }

    private static JishoMessage RemoveWikipediaEntries(JishoMessage message)
    {
        foreach (var datum in message.Data)
        {
            if (datum.Attribution.DbPedia != "false")
                datum.Senses = datum.Senses.Where(sense => !sense.PartsOfSpeech.Contains("Wikipedia definition")).ToArray();
        }

        if (message.Data.Any(datum => datum.Senses.Length == 0))
            message.Data = message.Data.Where(datum => datum.Senses.Length > 0).ToArray();

        return message;
    }

    private static string GetTempWordFilePath(string keyword)
    {
        var tmpPath = Path.GetTempPath();
        var tmpAppPath = Path.Combine(tmpPath, "JishoTangoAssistant");
        var tmpWordFilename = string.Join("_", keyword.Split(Path.GetInvalidFileNameChars())) + ".json";
        return Path.Combine(tmpAppPath, tmpWordFilename);
    }

    private static async Task<IList<JishoDatum>?> GetResultJsonFromCacheAsync(string tempWordFilePath)
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
            var fileJson = JsonConvert.DeserializeObject<JishoMessage>(fileContent);

            return fileJson?.Data;
        }
        return null;
    }
}
