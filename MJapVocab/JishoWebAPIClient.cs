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
        private static bool running = false;

        public static async Task<string[]> RunAsync(string keyword)
        {
            if (!running)
            {
                running = true;
                using (var client = new HttpClient())
                {
                    var message = await client.GetStringAsync(String.Format("https://jisho.org/api/v1/search/words?keyword=\"{0}\"", keyword));

                    var json = JsonConvert.DeserializeObject<JishoMessage>(message);
                    if (json.meta.status == 200)
                    {
                        var result = new string[json.data[0].senses.Length + 2];
                        result[0] = json.data[0].slug;
                        result[1] = json.data[0].japanese[0].reading;
                        for (int i = 0; i < json.data[0].senses.Length; i++)
                        {
                            result[i + 2] = String.Join("; ", json.data[0].senses[i].english_definitions);
                        }
                        running = false;
                        return result;
                    }
                }
                running = false;
                return new string[] { };
            }
            return new string[] { };
        }
    }
}
