using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicExpression.ReverseEngineering
{
    public interface IExpression { }

    /// <summary>
    /// A class that wraps the actual Reverse engineering of the Magex.
    /// The Expression class contains a list of Parts each of which recursively contains part of the expression
    /// </summary>
    public class Expression: IExpression
    {
        private string _regularExpression = string.Empty;
        public string RegularExpression
        {
            get
            {
                return this._regularExpression;
            }
            private set
            {
                this._regularExpression = value;
                this.Parts = SplitIntoKnownParts();
            }
        }

        public Expression(string regex)
        {
            this.RegularExpression = regex;
        }

        private IList<IExpression> _parts = new List<IExpression>(0);
        public IList<IExpression> Parts
        {
            get
            {
                return this._parts;
            }
            private set
            {
                this._parts = value;
            }
        }

        /// <summary>
        /// Splits the saved regex into sub-expressions
        /// </summary>
        /// <returns></returns>
        internal IList<IExpression> SplitIntoKnownParts()
        {
            var parts = new List<IExpression>(0);

            // Search for all the known character sets
            foreach (var set in RegexCharacters.KnownSets)
            {
                var done = false;
                var startIndex = 0;
                while (!done)
                {
                    // Find the first occurence
                    startIndex = this.RegularExpression.IndexOf(set.Value, startIndex);

                    // If not found => exit
                    if (startIndex == -1)
                    {
                        done = true;
                        continue;
                    }

                    // If the pattern was found (and is not after an escaping character)
                    if (startIndex == 0 || (startIndex > 0 && this.RegularExpression[startIndex - 1] != '\\'))
                        parts.Add(new Leaf(startIndex, startIndex + set.Value.Length, set.Key));
                    
                    // Update the index
                    startIndex += set.Value.Length;

                    // If reached the end of the string
                    if (startIndex > this.RegularExpression.Length)
                        done = true;
                }
            }

            // Search for capturing groups



            // Orderby startIndex 
            parts = parts.OrderBy(x => (x as Leaf).StartIndex).ToList<IExpression>();

            // TODO: Handle superimposed elements

            return parts;
        }
    }

    public class Leaf: IExpression
    {
        public int StartIndex { get; set; }
        public int StopIndex { get; set; }
        public Characters CharacterSet { get; set; }
        public string RegularExpression { get; set; }

        public Leaf(int startIndex, int stopIndex, Characters characterSet)
        {
            this.StartIndex = startIndex;
            this.StopIndex = stopIndex;
            this.CharacterSet = characterSet;
            this.RegularExpression = RegexCharacters.Get(characterSet);
        }
    }
}
