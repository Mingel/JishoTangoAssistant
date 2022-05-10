using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JishoTangoAssistant
{
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
            {
                Directory.CreateDirectory(tmpAppPath);
            }

            if (File.Exists(tmpWordFilePath))
            {
                var json = JsonConvert.DeserializeObject<JishoMessage>(System.IO.File.ReadAllText(tmpWordFilePath));

                if (json == null) 
                    return null;

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

                    if (json == null)
                        return null;

                    File.WriteAllText(tmpWordFilePath, message);
                    var result = json.data;
                    return result;
                }
            }
            return null;
        }
    }
}
