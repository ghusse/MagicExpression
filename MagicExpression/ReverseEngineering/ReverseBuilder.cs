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

        public IList<string> Magex { get { return CreateMagex(); } }

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
                        var regexSegment = formallydentifyableSegment.Value.Regex;
                        if (remainingRegexToMatch.StartsWith(regexSegment, IGNORE_CASE, CultureInfo.CurrentCulture))
                        {
                            var length = regexSegment.Length;
                            decomposedSegments.Add(new FormallyIdentifiedSegment(globalStartIndex, globalStartIndex + length, 
                                regexSegment, formallydentifyableSegment.Key));
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

        private IList<string> CreateMagex()
        {
            var list = new List<string>();

            // Go through each segment and replace it with its magex equivalent

            // Do not forget that there may be multiple paths trough the segments

            return list;
        }
    }
}
