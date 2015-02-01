using MagicExpression;
using MagicExpression.ReverseEngineering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicExpressionReverse.Test
{
    [TestClass]
    public class ReverseEngineeringTest_Characters
    {
        [TestMethod]
        public void ReverseEngineeringTest_PartsNotNull()
        {
            var expression = @"\d";
            var reverseBuilder = new ReverseBuilder(expression);
            Assert.AreEqual(1, reverseBuilder.Segments.Count);
        }

        [TestMethod]
        public void ReverseEngineeringTest_SingleNumeral()
        {
            var expression = @"\d";
            var reverseBuilder = new ReverseBuilder(expression);
            Assert.AreEqual(SegmentNames.CharactersNumeral, (reverseBuilder.Segments[0] as FormallyIdentifiedSegment).CharacterSet);
        }

        [TestMethod]
        public void ReverseEngineeringTest_Whitespace()
        {
            var expression = @"\s";
            var reverseBuilder = new ReverseBuilder(expression);
            Assert.AreEqual(SegmentNames.CharactersWhiteSpaces, (reverseBuilder.Segments[0] as FormallyIdentifiedSegment).CharacterSet);
        }

        [TestMethod]
        public void ReverseEngineeringTest_NumericPlusWhitespace()
        {
            var expression = @"\d\s";
            var reverseBuilder = new ReverseBuilder(expression);

            Assert.AreEqual(2, reverseBuilder.Segments.Count);
            Assert.AreEqual(SegmentNames.CharactersNumeral, (reverseBuilder.Segments[0] as FormallyIdentifiedSegment).CharacterSet);
            Assert.AreEqual(SegmentNames.CharactersWhiteSpaces, (reverseBuilder.Segments[1] as FormallyIdentifiedSegment).CharacterSet);
        }

        [TestMethod]
        public void ReverseEngineeringTest_NumericWhitespaceNumeric()
        {
            var expression = @"\d\s\d";
            var reverseBuilder = new ReverseBuilder(expression);

            Assert.AreEqual(3, reverseBuilder.Segments.Count);
            Assert.AreEqual(SegmentNames.CharactersNumeral, (reverseBuilder.Segments[0] as FormallyIdentifiedSegment).CharacterSet);
            Assert.AreEqual(SegmentNames.CharactersWhiteSpaces, (reverseBuilder.Segments[1] as FormallyIdentifiedSegment).CharacterSet);
            Assert.AreEqual(SegmentNames.CharactersNumeral, (reverseBuilder.Segments[2] as FormallyIdentifiedSegment).CharacterSet);
        }

        [TestMethod]
        public void ReverseEngineeringTest_DoubleEscapingBackslash()
        {
            var expression = @"\\";
            var reverseBuilder = new ReverseBuilder(expression);
            Assert.AreEqual(2, reverseBuilder.Segments.Count);
            Assert.IsTrue(reverseBuilder.Segments[0] is EscapingSegment);
            Assert.IsTrue(reverseBuilder.Segments[1] is UnidentifiedSegment);
        }

        [TestMethod]
        public void ReverseEngineeringTest_TripleEscapingBackslash()
        {
            var expression = @"\\\";
            var reverseBuilder = new ReverseBuilder(expression);
            Assert.AreEqual(3, reverseBuilder.Segments.Count);
            Assert.IsTrue(reverseBuilder.Segments[0] is EscapingSegment);
            Assert.IsTrue(reverseBuilder.Segments[1] is UnidentifiedSegment);
            Assert.IsTrue(reverseBuilder.Segments[2] is EscapingSegment);
        }

        [TestMethod]
        public void ReverseEngineeringTest_EscapingWhitespace()
        {
            var expression = @"\\s";
            var reverseBuilder = new ReverseBuilder(expression);
            Assert.AreEqual(3, reverseBuilder.Segments.Count);
        }


    }
}
