using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MagicExpression.ReverseEngineering
{
    public static class StringExtension
    {
        private static bool CASE_SENSITIVE = false;

        public static IList<ISegment> ParseMagex(this string expression)
        {
            var list = new List<ISegment>(0);
            var lexicon = RegexMagexLexicon.GetLexicon();

            for(int i = 0; i< expression.Length; i++)
            {
                var initialIndex = i;
                var substring = expression[i].ToString();
                
                while(lexicon.CountOccurences(substring) > 0)
                {
                    i++;
                    if (i >= expression.Count()) break;
                    substring += expression[i];
                }

                if (substring.IsEmpty())
                    substring = expression[initialIndex].ToString();
                else if (substring.Length > 1 && lexicon.CountOccurences(substring) == 0)
                {
                    substring = substring.Substring(0, substring.Length - 1);
                    i--;
                }

                list.Add(new Segment() { Regex = substring, Magex = lexicon.GetMagex(substring) ?? string.Format(".Character('{0}')", substring) });
            }

            return list;
        }

        public static bool IsEmpty(this string chain)
        {
            return chain == string.Empty;
        }

        public static int CountOccurences(this List<RegMag> list, string substring)
        {
            var lst = list.Where(x => x.Key.StartsWith(substring, CASE_SENSITIVE, CultureInfo.CurrentCulture));
            return lst.Count();
        }

        /// <summary>
        /// Attempts to find the value which key is given
        /// </summary>
        /// <param name="list">The list in which to search for</param>
        /// <param name="key">the Key to search for</param>
        /// <returns>The Value if found, null otherwise</returns>
        public static string GetMagex(this List<RegMag> list, string key)
        {
            var blah = list.FirstOrDefault(x => x.Key == key);
            return blah != null ? blah.Value : null;
        }

        public static string Flatten(this IList<ISegment> list)
        {
            return list.Select(i => (i as Segment).Magex).Aggregate((i, j) => i + j);
        }
    }
}
