using Newtonsoft.Json;

namespace MJapVocab
{
    class VocabularyItemHandler
    {
        public static string ListToJson(VocabularyItem[] items)
        {
            return JsonConvert.SerializeObject(items, Formatting.Indented);
        }

        public static VocabularyItem[] JsonToList(string json)
        {
            return JsonConvert.DeserializeObject<VocabularyItem[]>(json);
        }
    }
}
