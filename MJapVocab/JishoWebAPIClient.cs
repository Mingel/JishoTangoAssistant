using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MJapVocab
{
    class JishoWebAPIClient
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<JishoDatum[]> GetResultJsonAsync(string keyword)
        {
            using (var client = new HttpClient())
            {
                var message = await client.GetStringAsync(String.Format("https://jisho.org/api/v1/search/words?keyword=\"{0}\"", keyword));

                var json = JsonConvert.DeserializeObject<JishoMessage>(message);
                if (json.meta.status == 200)
                {
                    var result = json.data;
                    return result;
                }
            }
            return null;
        }
    }
}
