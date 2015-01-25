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

            // Search for character sets
            foreach (var set in RegexCharacters.KnownSets)
            {
                var startIndex = this.RegularExpression.IndexOf(set.Value);

                if (startIndex >= 0)
                    parts.Add(new Leaf(startIndex, startIndex + set.Value.Length, set.Key));
            }

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
