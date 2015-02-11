using System;
using System.Collections.Generic;
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
            var root = Node.DecomposeRecusively(new Node(Node.NOT_IDENTIFIED) { RegularExpressionSegment = regex });

            this.Segments = new List<ISegment>(0) { new UnidentifiedSegment(regex) };
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
