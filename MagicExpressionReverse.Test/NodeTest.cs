using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagicExpression.ReverseEngineering
{
    [TestClass]
    public class NodeTest
    {
        [TestMethod]
        public void NodeOddNumberOfTrailingBackslashesTest()
        {
            Assert.IsFalse(Node.OddNumberOfTrailingBackslashes(null), "NullCase");
            Assert.IsFalse(Node.OddNumberOfTrailingBackslashes(@""), "EmptyCase");
            Assert.IsFalse(Node.OddNumberOfTrailingBackslashes(@"tfcgv\\hjhgj"), "TrashCase");

            Assert.IsTrue(Node.OddNumberOfTrailingBackslashes(@"fdfsdfs\"), "One");
            Assert.IsFalse(Node.OddNumberOfTrailingBackslashes(@"dfsg\\"), "Two");
            Assert.IsTrue(Node.OddNumberOfTrailingBackslashes(@"sdfg\\\"), "Three");
        }

        [TestMethod]
        public void NodeDecomposeRecusively_NullNodeOrNullOrEmptyRegex()
        {
            var nullNode = Node.DecomposeRecusively(null);
            Assert.IsNull(nullNode);

            var nullRegex = Node.DecomposeRecusively(new Node(false) { RegularExpressionSegment = null });
            Assert.IsNull(nullRegex);

            var emptyRegex = Node.DecomposeRecusively(new Node(false) { RegularExpressionSegment = null });
            Assert.IsNull(emptyRegex);
        }

        [TestMethod]
        public void NodeDecomposeRecusively_Numeric()
        {
            var reg = @"\d";
            var node = Node.DecomposeRecusively(new Node(false) { RegularExpressionSegment = reg });
            Assert.IsNotNull(node);
            Assert.IsNull(node.LeftNode);
            Assert.IsNull(node.RightNode);
            Assert.AreEqual(reg, node.RegularExpressionSegment);
        }

        [TestMethod]
        public void NodeDecomposeRecusively_NumericLeft()
        {
            var reg = @"aaa\d";
            var node = Node.DecomposeRecusively(new Node(false) { RegularExpressionSegment = reg });
            Assert.IsNotNull(node.LeftNode);
            Assert.IsNull(node.RightNode);
            Assert.AreEqual(@"\d", node.RegularExpressionSegment);
            Assert.AreEqual(@"aaa", node.LeftNode.RegularExpressionSegment);
        }

        [TestMethod]
        public void NodeDecomposeRecusively_NumericRight()
        {
            var reg = @"\dbbb";
            var node = Node.DecomposeRecusively(new Node(false) { RegularExpressionSegment = reg });
            Assert.IsNotNull(node.RightNode);
            Assert.IsNull(node.LeftNode);
            Assert.AreEqual(@"\d", node.RegularExpressionSegment);
            Assert.AreEqual(@"bbb", node.RightNode.RegularExpressionSegment);
        }

        [TestMethod]
        public void NodeDecomposeRecusively_NumericLeftRight()
        {
            var reg = @"aaa\dbbb";
            var node = Node.DecomposeRecusively(new Node(false) { RegularExpressionSegment = reg });
            Assert.AreEqual(@"\d", node.RegularExpressionSegment);
            Assert.AreEqual(@"aaa", node.LeftNode.RegularExpressionSegment);
            Assert.AreEqual(@"bbb", node.RightNode.RegularExpressionSegment);
        }

        [TestMethod]
        public void NodeDecomposeRecusively_ForbiddenChar()
        {
            var reg = @"[^abc]";
            var node = Node.DecomposeRecusively(new Node(false) { RegularExpressionSegment = reg });
            Assert.AreEqual(@"[^", node.RegularExpressionSegment);
            Assert.IsNull(node.LeftNode);
            Assert.AreEqual(@"]", node.RightNode.RegularExpressionSegment);
            Assert.IsNull(node.RightNode.RightNode);
            Assert.AreEqual(@"abc", node.RightNode.LeftNode.RegularExpressionSegment);
        }

        [TestMethod]
        public void NodeDecomposeRecusively_ForbiddenCharRightLeft()
        {
            var reg = @"xyz[^abc]efg";
            var node = Node.DecomposeRecusively(new Node(false) { RegularExpressionSegment = reg });

            Assert.AreEqual(@"[^", node.RegularExpressionSegment);
            Assert.AreEqual(@"xyz", node.LeftNode.RegularExpressionSegment);
            Assert.AreEqual(@"]", node.RightNode.RegularExpressionSegment);
            Assert.AreEqual(@"abc", node.RightNode.LeftNode.RegularExpressionSegment);
            Assert.AreEqual(@"efg", node.RightNode.RightNode.RegularExpressionSegment);
        }

        [TestMethod]
        public void NodeConvertASTToSegmentsList_ForbiddenCharRightLeft()
        {
            var reg = @"xyz[^abc]efg";
            var node = Node.DecomposeRecusively(new Node(false) { RegularExpressionSegment = reg });

            var list = new List<ISegment>(0);
            Node.ConvertASTToSegmentsList(node, list);

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);

            Assert.AreEqual(@"xyz", list[0].RegexSegment);
            Assert.AreEqual(@"[^", list[1].RegexSegment);
            Assert.AreEqual(@"abc", list[2].RegexSegment);
            Assert.AreEqual(@"]", list[3].RegexSegment);
            Assert.AreEqual(@"efg", list[4].RegexSegment);
        }
    }
}
