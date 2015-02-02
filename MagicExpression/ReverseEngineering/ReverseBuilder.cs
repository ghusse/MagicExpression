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

        public IList<string> MagexPossibilities
        {
            get
            {
                return ListMagexPossibilities();
            }
        }

        public ReverseBuilder(string regex)
        {
            this.Segments = DecomposeSegments(regex);
            this.Segments = FactorizeCharactersAndLiterals(this.Segments);
        }

        /// <summary>
        /// Scans the regex string for known regex-segment, the scan is performed in 2 passes:
        ///   - The FormallyIdentifyableSegments first
        ///   - The PotentiallyIdentifyableSegments then
        /// </summary>
        /// <param name="regex">The regex string to reverse engineer</param>
        /// <returns>An unsorted list of all identified segments</returns>
        private IList<ISegment> DecomposeSegments(string regex)
        {
            var decomposedSegments = new List<ISegment>(0);
            string remainingRegexToMatch = regex;
            int globalStartIndex = 0;
            bool found = false;

            //Go through the whole regex string
            while (remainingRegexToMatch.Length > 0)
            {
                found = false;

                // If there is no identified segment yet, or if the last segment identified is NOT an escaping backslash "\"
                var lastSegment = decomposedSegments.LastOrDefault();
                if (lastSegment == null || (lastSegment != null && !(lastSegment is EscapingSegment)))
                {
                    // Go through all the FormallydentifyableSegments until a match is found
                    //   if a match is found, it is the one, we can break
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
                                regexSegment, formallydentifyableSegment.Key));
                            remainingRegexToMatch = remainingRegexToMatch.Remove(0, length);
                            globalStartIndex += length;
                            found = true;
                            break;
                        }
                    }

                    //If we reached this point, it means we didn't find a 100% match in the formally identifyable segments
                    //  should try the PartiallyIdentifyableSegments e.g. ) closing parenthesis and such
                    //foreach (var partiallyIdentifyableSegment in RegexMagexLexicon.PartiallyIdentifyableSegments)
                    //{
                    //}
                }

                if (!found)
                {
                    // If the first character is a backslash and it is not escaped by a previous backslash
                    if (remainingRegexToMatch[0] == '\\' && !(decomposedSegments.LastOrDefault() is EscapingSegment))
                        decomposedSegments.Add(new EscapingSegment(globalStartIndex, globalStartIndex + 1, remainingRegexToMatch.Substring(0, 1)));
                    else
                        decomposedSegments.Add(new UnidentifiedSegment(globalStartIndex, globalStartIndex + 1, remainingRegexToMatch.Substring(0, 1)));
                    remainingRegexToMatch = remainingRegexToMatch.Remove(0, 1);
                    globalStartIndex += 1;
                }
            }

            return decomposedSegments;
        }

        /// <summary>
        /// Goes through the list and each time it finds:
        /// - Two Character segments -> merges them into one "Literal()" segment
        /// - A Literal and a Character -> One Literal
        /// - A Character and a Literal -> One Literal
        /// - Two Literal -> One Literal
        /// </summary>
        private IList<ISegment> FactorizeCharactersAndLiterals(IList<ISegment> OrderedList)
        {
            //Go through the whole list and inspect the element two by two
            for (int i = 0; i < OrderedList.Count - 1; i++)
            {
                var segment = OrderedList[i] as Segment;
                var nextSegment = OrderedList[i + 1] as Segment;

                if (segment == null || nextSegment == null)
                    continue;

                // If the current segment and the next are character segments
                if ((segment.Name == SegmentNames.CharacterSingle && nextSegment.Name == SegmentNames.CharacterSingle)
                    || (segment.Name == SegmentNames.Literal && nextSegment.Name == SegmentNames.CharacterSingle)
                    || (segment.Name == SegmentNames.CharacterSingle && nextSegment.Name == SegmentNames.Literal)
                    || (segment.Name == SegmentNames.Literal && nextSegment.Name == SegmentNames.Literal))
                {
                    OrderedList[i] = new NotIdentifyableSegment(segment.StartIndex, nextSegment.StopIndex, 
                        segment.RegexSegment + nextSegment.RegexSegment, SegmentNames.Literal);
                    OrderedList[i + 1] = new PotentiallyIdentifiedSegment(nextSegment.StartIndex, nextSegment.StopIndex, string.Empty, SegmentNames.ParenthesisEnd);
                }
            }

            return OrderedList;
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
                else if (segment as IdentifiedSegment != null)
                    // HOW TO CAPTURE THE CONTENT OF THE LITERAL (OR A GROUP FOR THAT MATTER)?
                    list[0] += (segment as IdentifiedSegment).Magex.ToString();
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
