using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicExpression.ReverseEngineering
{
    public interface INode
    {

    }

    public class Node : INode
    {
        public IDictionary<string, INode> Dictionary { get; set; }

    }

    /// <summary>
    /// I Know this is 
    /// </summary>
    public class RegMag
    {
        public RegMag(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }
    }

    //    public interface INode
    //    {
    //        INode LeftNode { get; set; }
    //        INode RightNode { get; set; }
    //        string RegularExpressionSegment { get; set; }
    //        SegmentNames NodeType { get; set; }
    //        bool IsIdentified { get; set; }
    //    }

    //    public class Node : INode
    //    {
    //        public static bool IDENTIFIED = true;
    //        public static bool NOT_IDENTIFIED = false;

    //        public INode LeftNode { get; set; }
    //        public INode RightNode { get; set; }
    //        public string RegularExpressionSegment { get; set; }
    //        public SegmentNames NodeType { get; set; }
    //        public bool IsIdentified { get; set; }

    //        public Node(bool identified)
    //        {
    //            this.IsIdentified = identified;
    //        }

    //        /// <summary>
    //        /// Creates a Syntactic Tree of the formally identifyable elements of the regex string
    //        /// </summary>
    //        /// <param name="node">The root node of the branch</param>
    //        /// <returns>The decomposed root node of the current branch</returns>
    //        public static INode DecomposeRecusively(INode node)
    //        {
    //            if(node == null || String.IsNullOrEmpty(node.RegularExpressionSegment))
    //                return null;

    //            INode newNode = null;

    //            var Lexicons = new[]
    //            {
    //                RegexMagexLexicon.FormallydentifyableSegmentsWithBackslashes,
    //                RegexMagexLexicon.FormallydentifyableSegments,
    //                RegexMagexLexicon.PartiallyIdentifyableSegments,
    //            };

    //            foreach (var lexicon in Lexicons)
    //            {
    //                //Identify a segment
    //                foreach (var identifyableSegment in lexicon)
    //                {
    //                    var index = node.RegularExpressionSegment.IndexOf(identifyableSegment.Value.Regex);

    //                    // Found a match
    //                    if (index > -1)
    //                    {
    //                        //If the segment is not negated by a backslash, skip it. Ex: "\\d" means the string "\d" not a numeric segment
    //                        if (index > 0 && node.RegularExpressionSegment[index - 1] == '\\')
    //                        {
    //                            if (OddNumberOfTrailingBackslashes(node.RegularExpressionSegment.Substring(0, index)))
    //                                continue;
    //                        }

    //                        newNode = CreateNewNode(node, identifyableSegment, index);
    //                        break;
    //                    }
    //                }
    //                if (newNode != null) break;
    //            }

    //            if (newNode != null)
    //            {
    //                //Descend left
    //                if (newNode.LeftNode != null)
    //                    newNode.LeftNode = DecomposeRecusively(newNode.LeftNode);

    //                //Descend right
    //                if (newNode.RightNode != null)
    //                    newNode.RightNode = DecomposeRecusively(newNode.RightNode);
    //            }
    //            else
    //                newNode = new Node(NOT_IDENTIFIED) { RegularExpressionSegment = node.RegularExpressionSegment };

    //            //Return the new segment
    //            return newNode;
    //        }

    //        /// <summary>
    //        /// Counts the number of trailing '\' in a string
    //        /// </summary>
    //        /// <param name="s">the string to consider</param>
    //        /// <returns>True if the count is odd, false otherwise</returns>
    //        public static bool OddNumberOfTrailingBackslashes(string s)
    //        {
    //            if (string.IsNullOrEmpty(s))
    //                return false;

    //            var reversedString = new string(s.Reverse().ToArray());
    //            for (int i = 0; i < reversedString.Length; i++)
    //            {
    //                if (reversedString[i] != '\\')
    //                    return (i - 1) % 2 == 0;
    //            }
    //            return false;
    //        }

    //        /// <summary>
    //        /// Recursively fills the list passed as a parameter
    //        /// </summary>
    //        /// <param name="currentNode">The node to start the recursion with (root)</param>
    //        /// <param name="listToFill">The list to add elements to</param>
    //        public static void ConvertASTToSegmentsList(INode currentNode, IList<ISegment> listToFill)
    //        {
    //            if (currentNode.LeftNode != null)
    //                ConvertASTToSegmentsList(currentNode.LeftNode, listToFill);

    //            if (currentNode.IsIdentified)
    //                listToFill.Add(new FormallyIdentifiedSegment(currentNode.RegularExpressionSegment, currentNode.NodeType));
    //            else
    //                if (currentNode.RegularExpressionSegment.Length == 1)
    //                    listToFill.Add(new FormallyIdentifiedSegment(currentNode.RegularExpressionSegment, SegmentNames.CharacterSingle));
    //                else
    //                    listToFill.Add(new FormallyIdentifiedSegment(currentNode.RegularExpressionSegment, SegmentNames.Literal));

    //            if (currentNode.RightNode != null)
    //                ConvertASTToSegmentsList(currentNode.RightNode, listToFill);
    //        }

    //        private static INode CreateNewNode(INode node, KeyValuePair<SegmentNames, RegexString> identifyableSegment, int index)
    //        {
    //            var newNode = new Node(IDENTIFIED)
    //            {
    //                RegularExpressionSegment = identifyableSegment.Value.Regex,
    //                NodeType = identifyableSegment.Key,
    //            };
    //            var leftCharacters = node.RegularExpressionSegment.Substring(0, index);
    //            var rightCharacters = node.RegularExpressionSegment.Substring(index + identifyableSegment.Value.Regex.Length);
    //            newNode.LeftNode = leftCharacters.Length > 0 ? new Node(NOT_IDENTIFIED) { RegularExpressionSegment = leftCharacters } : null;
    //            newNode.RightNode = rightCharacters.Length > 0 ? new Node(NOT_IDENTIFIED) { RegularExpressionSegment = rightCharacters } : null;

    //            return newNode;
    //        }
    //    }

    //    public enum SegmentType
    //    {
    //        Unidentified = 0,
    //        FormallyIdentifiedWithBackslashes,
    //        FormallyIdentified,
    //    }
}
