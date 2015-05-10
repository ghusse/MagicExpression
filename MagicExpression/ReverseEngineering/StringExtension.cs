using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MagicExpression.ReverseEngineering
{
    public static class StringExtension
    {
        private static bool CASE_SENSITIVE = false;

        /// <summary>
        /// Attempts to parse a Magex Expression
        /// </summary>
        /// <param name="expression">The expression to parse</param>
        /// <returns>A list of <see cref="ISegment"/> representing the decomposed Magex</returns>
        public static IList<ISegment> ParseMagex(this string expression)
        {
            var list = new List<ISegment>(0);
            var lexicon = RegexMagexLexicon.KnownRegexElements();

            // Linearly parse the whole expression in search for known parts
            for(int i = 0; i< expression.Length; i++)
            {
                var initialIndex = i;
                var substring = expression[i].ToString();

                // Find the best (eager-est) matching regex
                while (lexicon.CountOccurences(substring) > 0)
                {
                    i++;
                    if (i >= expression.Count()) break;
                    substring += expression[i];
                }

                if (substring.IsEmpty())
                    substring = expression[initialIndex].ToString();
                // If the part of the pattern being worked on is not empty but there are no matches
                else if (substring.Length > 1 && lexicon.CountOccurences(substring) == 0)
                {
                    // Backtrack once
                    substring = substring.Substring(0, substring.Length - 1);
                    i--;
                }

                // Match
                list.Add(
                    new Segment() {
                        Regex = substring,
                        Magex = lexicon.GetMagex(substring) ??
                               string.Format(".SingleCharacter('{0}')", substring) }
                    );
            }

            

            return list;
        }

        public static string GetMagex(this string expression)
        {
            return ParseMagex(expression).Flatten();
        }

        public static bool IsEmpty(this string chain)
        {
            return chain == string.Empty;
        }

        public static int CountOccurences(this List<RegMag> list, string substring)
        {
            var lst = list.Where(x => x.isMatch(substring)); 

            //var lst = list.Where(x => x.Key.StartsWith(substring, CASE_SENSITIVE, CultureInfo.CurrentCulture));
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
            var matches = list.Where(x => x.isMatch(key));
            var match = matches.FirstOrDefault();
            return match != null ? match.Value : null;
        }

        /// <summary>
        /// This is probably the most aweful function I have ever written...
        /// </summary>
        public static string Flatten(this IList<ISegment> list)
        {
            var fullyOptimized = false;
            while(!fullyOptimized)
            {
                // Loop through all the items of the list
                for (var i = 0; i < list.Count(); i++)
                {
                    var sg = list[i] as Segment;
                    if (CanBeOptimized(sg))
                    {
                        if (i == 0)
                        {
                            sg.Magex = sg.Magex.Replace("Single", "");
                        }
                        else
                        {
                            var previousSegment = (list[i - 1] as Segment);
                            var targetCharacter = sg.Magex.Substring(sg.Magex.Length - 3, 1);

                            // First element following a .CharacterIn(, don't need to repeat ".Character"
                            if (previousSegment.Magex.StartsWith(".CharacterIn"))
                            {
                                sg.Magex = string.Format("\"{0}\"", targetCharacter);
                            }
                            // Already following another .Character that was replaced into a "x"
                            else if (previousSegment.Magex.EndsWith("\""))
                            {
                                sg.Magex = "";
                                previousSegment.Magex = previousSegment.Magex.Insert(previousSegment.Magex.Length - 1, targetCharacter);
                            }
                            // Following a "Characters.Something", add it as a second parameter
                            else if (previousSegment.Magex.StartsWith("Characters."))
                            {
                                sg.Magex = string.Format(", \"{0}\"", targetCharacter);
                            }
                        }
                    }
                }

                fullyOptimized = true;
            }

            return list.Select(i => (i as Segment).Magex).Aggregate((i, j) => i + j);
        }

        private static bool CanBeOptimized(Segment segment)
        {
            // A .SingleCharacter("x") inside of a CharacterIn should be simplified into "x"
            // A .SingleCharacter("x") preceded by a "f" should be concatenated to "fx"
            if (segment.Magex.StartsWith(".SingleChar")) return true;

            return false;
        }
    }
}
