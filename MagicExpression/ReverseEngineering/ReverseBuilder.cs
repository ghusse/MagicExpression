using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MagicExpression.ReverseEngineering
{
    /// <summary>
    /// A wrapper class containing an <see cref="Expression"/>. 
    /// The expression wraps the actual Reverse engineering of the Magex.
    /// </summary>
    public class ReverseBuilder
    {
        private bool IGNORE_CASE = true;

        public IList<ISegment> Segments { get; set; }

        public IList<string> MagexPossibilities { get { return ListMagexPossibilities(); } }

        public ReverseBuilder(string regex)
        {
            this.Segments = DecomposeSegments(regex);
        }

        private IList<ISegment> DecomposeSegments(string regex)
        {
            var decomposedSegments = new List<ISegment>(0);
            string remainingRegexToMatch = regex;
            int globalStartIndex = 0;
            bool found = false;

            while (remainingRegexToMatch.Length > 0)
            {
                found = false;

                // If there is no identified segment yet, or if the last segment identified is NOT an escaping backslash "\"
                var lastSegment = decomposedSegments.LastOrDefault();
                if (lastSegment == null || (lastSegment != null && !(lastSegment is EscapingSegment)))
                {
                    // Go through all the FormallydentifyableSegments until a match is found
                    foreach (var formallydentifyableSegment in RegexMagexLexicon.FormallydentifyableSegments)
                    {
                        // Skip the escape as a special char, it is to be handled last or it will produce false positives
                        if (formallydentifyableSegment.Key == SegmentNames.EscapingBackslash)
                            continue;

                        var regexSegment = formallydentifyableSegment.Value.Regex;
                        var magexSegment = formallydentifyableSegment.Value.Magex;
                        if (remainingRegexToMatch.StartsWith(regexSegment, IGNORE_CASE, CultureInfo.CurrentCulture))
                        {
                            var length = regexSegment.Length;
                            decomposedSegments.Add(new FormallyIdentifiedSegment(globalStartIndex, globalStartIndex + length, 
                                regexSegment, magexSegment));
                            remainingRegexToMatch = remainingRegexToMatch.Remove(0, length);
                            globalStartIndex += length;
                            found = true;
                            break;
                        }
                    }
                }

                if (!found)
                {
                    // If the first character is a backslash and it is not escaped by a former backslash
                    if (remainingRegexToMatch[0] == '\\' && !(decomposedSegments.LastOrDefault() is EscapingSegment))
                        decomposedSegments.Add(new EscapingSegment(globalStartIndex, globalStartIndex + 1, remainingRegexToMatch.Substring(0, 1)));
                    else
                        decomposedSegments.Add(new UnidentifiedSegment(globalStartIndex, globalStartIndex + 1, remainingRegexToMatch.Substring(0,1)));
                    remainingRegexToMatch = remainingRegexToMatch.Remove(0, 1);
                    globalStartIndex += 1;
                }
            }

            return decomposedSegments;
        }

        private IList<string> ListMagexPossibilities()
        {
            var list = new List<string>(0);

            // Go through each segment and replace it with its magex equivalent
            foreach(var segment in this.Segments)
            {
                if (list.Count == 0)
                    list.Add(string.Empty);

                if (segment as EscapingSegment != null)
                    continue; //Skip the escaping segments ( => // is an escaping + an unidentified)
                else if (segment as FormallyIdentifiedSegment != null)
                    list[0] += (segment as FormallyIdentifiedSegment).Magex.ToString();
                else if (segment as UnidentifiedSegment != null)
                {
                    var rgxSeg = (segment as UnidentifiedSegment).RegexSegment.ToString();
                    list[0] += String.Format(".Character('{0}')", rgxSeg != @"\\" ? rgxSeg : @"\\" + rgxSeg);
                }
                else
                    throw new Exception("Upps, that shouldn't have happened... like... ever...");
            }

            // Do not forget that there may be multiple paths trough the segments

            return list;
        }
    }
}
