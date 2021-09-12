using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MJapVocab
{
    class JishoMessage
    {
        public JishoMeta meta;
        public JishoDatum[] data;
    }

    class JishoMeta
    {
        public int status;
    }

    class JishoDatum
    {
        public string slug;
        public JishoJapaneseItem[] japanese;
        public JishoSense[] senses;
    }

    class JishoJapaneseItem
    {
        public string word;
        public string reading;
    }

    class JishoSense
    {
        public string[] english_definitions;
        public string[] tags;
    }

    class JishoWebAPIClient
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<JishoDatum[]> RunAsync(string keyword)
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
