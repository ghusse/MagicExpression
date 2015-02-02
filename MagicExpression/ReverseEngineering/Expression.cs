//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace MagicExpression.ReverseEngineering
//{
//    public interface IExpression { }

//    /// <summary>
//    /// A class that wraps the actual Reverse engineering of the Magex.
//    /// The Expression class contains a list of Parts each of which recursively contains part of the expression
//    /// </summary>
//    public class Expression : IExpression
//    {
//        private string _regularExpression = string.Empty;
//        public string RegularExpression
//        {
//            get
//            {
//                return this._regularExpression;
//            }
//            private set
//            {
//                this._regularExpression = value;
//                this.Parts = SplitIntoKnownParts();
//            }
//        }

//        public Expression(string regex)
//        {
//            this.RegularExpression = regex;
//        }

//        private IList<IExpression> _parts = new List<IExpression>(0);
//        public IList<IExpression> Parts
//        {
//            get
//            {
//                return this._parts;
//            }
//            private set
//            {
//                this._parts = value;
//            }
//        }

//        /// <summary>
//        /// Splits the saved regex into sub-expressions
//        /// </summary>
//        /// <returns></returns>
//        internal IList<IExpression> SplitIntoKnownParts()
//        {
//            IList<IExpression> parts = new List<IExpression>(0);

//            SearchForKnownSets(parts);
//            parts = parts.OrderBy(x => (x as Leaf).StartIndex).ToList<IExpression>();

//            IList<INode> OrderedList = CreateOutputStructure(parts);

//            //ProofConsistency(OrderedList);

//            SimplifyList(OrderedList);

//            return parts;
//        }

//        private void SimplifyList(IList<INode> OrderedList)
//        {
//            OrderedList = FactorizeCharactersAndLiterals(OrderedList);
//        }

//        /// <summary>
//        /// Goes through the list and each time it finds:
//        /// - Two Character segments -> merges them into one "Literal()" segment
//        /// - A Literal and a Character -> One Literal
//        /// - A Character and a Literal -> One Literal
//        /// - Two Literal -> One Literal
//        /// </summary>
//        private IList<INode> FactorizeCharactersAndLiterals(IList<INode> OrderedList)
//        {
//            //Go through the whole list and inspect the element two by two
//            for (int i = 0; i < OrderedList.Count - 1; i++)
//            {
//                var segment = OrderedList[i].Possibilities.FirstOrDefault() as Leaf;
//                var nextSegment = OrderedList[i + 1].Possibilities.FirstOrDefault() as Leaf;

//                if (segment != null && nextSegment != null)
//                    throw new Exception("Preprocessing of the list left some unreadable elements");

//                // If the current segment and the next are character segments
//                if ((segment.CharacterSet == SegmentNames.CharacterSingle && nextSegment.CharacterSet == SegmentNames.CharacterSingle)
//                    || (segment.CharacterSet == SegmentNames.Literal && nextSegment.CharacterSet == SegmentNames.CharacterSingle)
//                    || (segment.CharacterSet == SegmentNames.CharacterSingle && nextSegment.CharacterSet == SegmentNames.Literal)
//                    || (segment.CharacterSet == SegmentNames.Literal && nextSegment.CharacterSet == SegmentNames.Literal))
//                {
//                    segment.CharacterSet = SegmentNames.Literal;
//                    segment.RegularExpression += nextSegment.RegularExpression;
//                    segment.StopIndex = nextSegment.StopIndex;
//                }
//            }

//            return OrderedList;
//        }

//        /// <summary>
//        /// Splits the List into a constructed list of nodes containing each the possible expression(s) starting at this point
//        /// </summary>
//        private IList<INode> CreateOutputStructure(IList<IExpression> parts)
//        {
//            IList<INode> outputList = CreateAndInitialiseList(this.RegularExpression.Length);

//            // Replace each identified part into one or more liste (regardless of the output)
//            foreach (var part in parts)
//            {
//                var leaf = part as Leaf;
//                if (leaf == null)
//                    continue;

//                // If the list of nodes at this index is empty
//                if (outputList[leaf.StartIndex] == null)
//                    outputList[leaf.StartIndex] = new Node(leaf);
//                else if (outputList[leaf.StartIndex].Possibilities.Count == 1)
//                    throw new Exception("Todo: I had foreseen this, but could not find an example for it..."
//                        + " there should not be more than one possibility for one character at this point!");
//                else
//                    outputList[leaf.StartIndex].Possibilities.Add(leaf);
//            }
//            return outputList;
//        }

//        private IList<INode> CreateAndInitialiseList(int count)
//        {
//            var outputList = new List<INode>(count);
//            for (int i = 0; i < count; i++)
//                outputList.Add(new Node());
//            return outputList;
//        }

//        private void SearchForKnownSets(IList<IExpression> parts)
//        {
//            foreach (var segment in RegexMagexLexicon.FormallydentifyableSegments)
//            {
//                SearchForSegment(parts, segment.Key, segment.Value);
//            }
//        }

//        private void SearchForSegment(IList<IExpression> parts, SegmentNames key, RegexString value)
//        {
//            var done = false;
//            var startIndex = 0;
//            while (!done)
//            {
//                // Find the first occurence
//                startIndex = this.RegularExpression.IndexOf(value.Regex, startIndex);

//                // If not found => exit
//                if (startIndex == -1)
//                {
//                    done = true;
//                    continue;
//                }

//                // If the pattern was found (and is not after an escaping character)
//                if (startIndex == 0 || (startIndex > 0 && this.RegularExpression[startIndex - 1] != '\\'))
//                    parts.Add(new Leaf(startIndex, startIndex + value.Regex.Length, key));

//                // Update the index
//                startIndex += value.Regex.Length;

//                // If reached the end of the string
//                if (startIndex > this.RegularExpression.Length)
//                    done = true;
//            }
//        }
//    }

//    public class Leaf : IExpression
//    {
//        public int StartIndex { get; set; }
//        public int StopIndex { get; set; }
//        public SegmentNames CharacterSet { get; set; }
//        public string RegularExpression { get; set; }

//        public Leaf(int startIndex, int stopIndex, SegmentNames magexString)
//        {
//            this.StartIndex = startIndex;
//            this.StopIndex = stopIndex;
//            this.CharacterSet = magexString;
//            this.RegularExpression = RegexMagexLexicon.FormallydentifyableSegments[magexString] != null ?
//                RegexMagexLexicon.FormallydentifyableSegments[magexString].Magex : null;
//        }

//        public Leaf(int startIndex, int stopIndex, SegmentNames magexString, char character)
//        {
//            this.StartIndex = startIndex;
//            this.StopIndex = stopIndex;
//            this.CharacterSet = magexString;
//            this.RegularExpression = character.ToString();
//        }
//    }
//}
