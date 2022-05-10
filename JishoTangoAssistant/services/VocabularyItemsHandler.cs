using Newtonsoft.Json;

namespace JishoTangoAssistant
{
    class VocabularyItemsHandler
    {
        public static string VocabularyListToJson(VocabularyItem[] items)
        {
            return JsonConvert.SerializeObject(items, Formatting.Indented);
        }

        public static VocabularyItem[]? JsonToVocabularyList(string json)
        {
            return JsonConvert.DeserializeObject<VocabularyItem[]>(json);
        }
    }
}
