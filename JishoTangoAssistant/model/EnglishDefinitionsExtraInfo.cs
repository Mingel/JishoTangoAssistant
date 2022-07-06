using System.Collections.Generic;

namespace JishoTangoAssistant
{
    public class EnglishDefinitionsExtraInfo
    {
        public int DataLength { get; }
        public IList<int> EnglishDefinitionsLengths { get; }
        public IList<string> FlattenedEnglishDefinitions { get; }

        public EnglishDefinitionsExtraInfo(int dataLength, IList<int> englishDefinitionsLengths, IList<string> flattenedEnglishDefinitions)
        {
            DataLength = dataLength;
            EnglishDefinitionsLengths = englishDefinitionsLengths;
            FlattenedEnglishDefinitions = flattenedEnglishDefinitions;
        }

        public IEnumerable<double> CumulativeEnglishDefinitionsLengthsSum()
        {
            double sum = 0;
            foreach (var item in EnglishDefinitionsLengths)
            {
                sum += item;
                yield return sum;
            }
        }
    }
}
