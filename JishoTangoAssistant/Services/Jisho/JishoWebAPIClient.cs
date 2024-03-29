﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;

namespace JishoTangoAssistant.Services.Jisho;

class JishoWebAPIClient
{
    public static async Task<JishoDatum[]?> GetResultJsonAsync(string keyword)
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
            var json = JsonConvert.DeserializeObject<JishoMessage>(File.ReadAllText(tmpWordFilePath));

            if (json == null) 
                return null;

            var result = json.Data;
            return result;
        }

        using (var client = new HttpClient())
        {
            var url = $"https://jisho.org/api/v1/search/words?keyword=\"{keyword}\"";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<JishoMessage>(message.ToString());

                if (json == null)
                    return null;

                json = RemoveWikipediaEntries(json);
                File.WriteAllText(tmpWordFilePath, JsonConvert.SerializeObject(json));
                var result = json.Data;
                return result;
            }
        }
        return null;
    }

    private static JishoMessage RemoveWikipediaEntries(JishoMessage message)
    {
        foreach (var datum in message.Data)
        {
            if (!datum.Attribution.DbPedia.Equals("false"))
                datum.Senses = datum.Senses.Where(sense => !sense.PartsOfSpeech.Contains("Wikipedia definition")).ToArray();
        }

        if (message.Data.Any(datum => datum.Senses.Length == 0))
            message.Data = message.Data.Where(datum => datum.Senses.Length > 0).ToArray();

        return message;
    }
}
