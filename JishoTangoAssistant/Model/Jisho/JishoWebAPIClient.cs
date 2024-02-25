using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JishoTangoAssistant.Model.Jisho;

public static class JishoWebApiClient
{
    private const string BaseUrl = "https://jisho.org/";
    private const string Endpoint = "api/v1/search/words";
    
    private static readonly HttpClient Client = new();
    
    public static async Task<JishoDatum[]?> GetResultJsonAsync(string keyword)
    {
        try
        {
            // caching
            var tmpPath = Path.GetTempPath();
            var tmpAppPath = Path.Combine(tmpPath, "JishoTangoAssistant");
            var tmpWordFilename = string.Join("_", keyword.Split(Path.GetInvalidFileNameChars())) + ".json";
            var tmpWordFilePath = Path.Combine(tmpAppPath, tmpWordFilename);
            if (!Directory.Exists(tmpAppPath))
                Directory.CreateDirectory(tmpAppPath);
            if (File.Exists(tmpWordFilePath))
            {
                var fileJson =
                    JsonConvert.DeserializeObject<JishoMessage>(await File.ReadAllTextAsync(tmpWordFilePath));

                return fileJson?.Data;
            }

            var escapedKeyword = Uri.EscapeDataString(keyword);
            
            var urlBuilder = new UriBuilder(BaseUrl)
            {
                Path = Endpoint,
                Query = $"keyword={escapedKeyword}"
            };;
            using var response = await Client.GetAsync(urlBuilder.Uri);
            if (!response.IsSuccessStatusCode) return null;
            var jsonContent = await response.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<JishoMessage>(jsonContent);
            if (json == null)
                return null;
            json = RemoveWikipediaEntries(json);
            await File.WriteAllTextAsync(tmpWordFilePath, JsonConvert.SerializeObject(json));
            var result = json.Data;
            return result;
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
}
