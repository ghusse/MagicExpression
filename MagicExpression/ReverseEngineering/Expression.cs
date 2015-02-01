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
            IList<IExpression> parts = new List<IExpression>(0);

            SearchForKnownSets(parts);
            parts = parts.OrderBy(x => (x as Leaf).StartIndex).ToList<IExpression>();
            
            IList<INode> OrderedList = CreateOutputStructure(parts);

            ProofConsistency(OrderedList);

            //FillupWithSingleChars(OrderedList);
            
            return parts;
        }

        /// <summary>
        /// Scans the whole list for consistency
        /// </summary>
        /// <param name="OrderedList">The list to proof</param>
        private void ProofConsistency(IList<INode> OrderedList)
        {
            //Check groups
            //

            //foreach (var node in OrderedList)
            //{
            //    // The node is empty
            //    if(node == null)
            //        throw new Exception("Node cannot be null at this point");

            //    // The node has no possibilities (BEEP)
            //    // The node has 0 possibilities (OK)
            //    // The node has more than 1 possibilities (BEEP)
            //    if(node.Possibilities == null || (node.Possibilities != null && node.Possibilities.Count > 1))
            //        throw new Exception("There should not be more than one possibility at this point");

            //    var leaf = node.Possibilities.First() as Leaf;
            //    if (leaf == null)
            //        throw new Exception("An expression is not allowed in the chain at this point");

            //    for (int i = leaf.StartIndex + 1; i < leaf.StopIndex; i++)
            //        if (!OrderedList[i].IsEmpty())
            //            throw new Exception(string.Format("Leaf {0} is not empty", i));
            //}
        }

        //private void FillupWithSingleChars(IList<INode> OrderedList)
        //{
        //    //foreach(var node in OrderedList)
        //    //{

        //    //}
        //}

        /// <summary>
        /// Splits the List into a constructed list of nodes containing each the possible expressions starting at this point
        /// [0] -> Nothing
        /// [1] -> \d
        /// </summary>
        /// <param name="parts"></param>
        /// <returns></returns>
        private IList<INode> CreateOutputStructure(IList<IExpression> parts)
        {
            IList<INode> outputList = GetPopulatedList(this.RegularExpression.Length);
            foreach(var part in parts)
            { 
                var leaf = part as Leaf;
                if(leaf == null)
                    continue;

                if (outputList[leaf.StartIndex] == null)
                    outputList[leaf.StartIndex] = new Node(leaf);
                else
                    outputList[leaf.StartIndex].Possibilities.Add(leaf);

                if (outputList[leaf.StartIndex].Possibilities.Count > 1)
                    throw new Exception("There should not be more than one possibility at this point");
            }
            return outputList;
        } 

        private IList<INode> GetPopulatedList(int count)
        {
            var outputList = new List<INode>(count);
            for (int i = 0; i < count; i++ )
                outputList.Add(new Node());
            return outputList;
        }

        private void SearchForKnownSets(IList<IExpression> parts)
        {
            foreach (var segment in RegexMagexLexicon.FormallydentifyableSegments)
            {
                SearchForSegment(parts, segment.Key, segment.Value);
            }
        }

        private void SearchForSegment(IList<IExpression> parts, SegmentNames key, RegexString value)
        {
            var done = false;
            var startIndex = 0;
            while (!done)
            {
                // Find the first occurence
                startIndex = this.RegularExpression.IndexOf(value.Regex, startIndex);

                // If not found => exit
                if (startIndex == -1)
                {
                    done = true;
                    continue;
                }

                // If the pattern was found (and is not after an escaping character)
                if (startIndex == 0 || (startIndex > 0 && this.RegularExpression[startIndex - 1] != '\\'))
                    parts.Add(new Leaf(startIndex, startIndex + value.Regex.Length, key));

                // Update the index
                startIndex += value.Regex.Length;

                // If reached the end of the string
                if (startIndex > this.RegularExpression.Length)
                    done = true;
            }
        }
    }

    public class Leaf: IExpression
    {
        public int StartIndex { get; set; }
        public int StopIndex { get; set; }
        public SegmentNames CharacterSet { get; set; }
        public string RegularExpression { get; set; }

        public Leaf(int startIndex, int stopIndex, SegmentNames magexString)
        {
            this.StartIndex = startIndex;
            this.StopIndex = stopIndex;
            this.CharacterSet = magexString;
            this.RegularExpression = RegexMagexLexicon.FormallydentifyableSegments[magexString] != null ?
                RegexMagexLexicon.FormallydentifyableSegments[magexString].Magex : null;
        }

        public Leaf(int startIndex, int stopIndex, SegmentNames magexString, char character)
        {
            this.StartIndex = startIndex;
            this.StopIndex = stopIndex;
            this.CharacterSet = magexString;
            this.RegularExpression = character.ToString();
        }
    }
}
