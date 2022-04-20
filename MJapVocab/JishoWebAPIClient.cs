using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MJapVocab
{
    class JishoWebAPIClient
    {
        public static async Task<JishoDatum[]> GetResultJsonAsync(string keyword)
        {
            // caching
            var tmpPath = Path.GetTempPath();
            var tmpAppPath = Path.Combine(tmpPath, "mjapvocab");
            var tmpWordFilename = string.Join("_", keyword.Split(Path.GetInvalidFileNameChars())) + ".json";
            var tmpWordFilePath = Path.Combine(tmpAppPath, tmpWordFilename);

            if (!Directory.Exists(tmpAppPath))
            {
                Directory.CreateDirectory(tmpAppPath);
            }

            if (File.Exists(tmpWordFilePath))
            {
                var json = JsonConvert.DeserializeObject<JishoMessage>(System.IO.File.ReadAllText(tmpWordFilePath));
                var result = json.data;
                return result;
            }

            using (var client = new HttpClient())
            {
                var url = String.Format("https://jisho.org/api/v1/search/words?keyword=\"{0}\"", keyword);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<JishoMessage>(message.ToString());
                    File.WriteAllText(tmpWordFilePath, message);
                    var result = json.data;
                    return result;
                }
            }
            return null;
        }
    }
}
