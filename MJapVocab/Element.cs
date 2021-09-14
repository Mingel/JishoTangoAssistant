using System;
using System.Text;
using Newtonsoft.Json;

namespace MJapVocab
{
    public class Element
    {
        public string Word { get; set; }
        public bool ShowReading { get; set; }
        public string Reading { get; set; }
        public string Output { get; set; }

        public Element(string word, bool showReading, string reading, string output)
        {
            Word = word;
            ShowReading = showReading;
            Reading = reading;
            Output = output;
        }

        public static string ListToJson(Element[] elements)
        {
            return JsonConvert.SerializeObject(elements, Formatting.Indented);
        }

        public static Element[] JsonToList(string json)
        {
            return JsonConvert.DeserializeObject<Element[]>(json);
        }

        public static string ListToJapEng(Element[] elements)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var elem in elements)
            {
                sb.Append(elem.Word);
                sb.Append(";");
                sb.Append("\"");
                if (elem.ShowReading)
                {
                    sb.Append(elem.Reading.Replace("\"","\"\""));
                    sb.Append(Environment.NewLine);
                }
                sb.Append(elem.Output.Replace("\"", "\"\""));
                sb.Append("\"");
                sb.Append(Environment.NewLine);
            }
            return sb.ToString().TrimEnd();
        }
    }
}
