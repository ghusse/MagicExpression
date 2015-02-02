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
            Assert.AreEqual(RegexMagexLexicon.FormallydentifyableSegments[SegmentNames.CharactersNumeral].Magex,
                (reverseBuilder.Segments[0] as FormallyIdentifiedSegment).Magex);
           
            //var magex = Magex.New().CharacterIn(Characters.Numeral);
            Assert.AreEqual(".CharacterIn(Characters.Numeral)", reverseBuilder.MagexPossibilities[0]);
        }

        [TestMethod]
        public void ReverseEngineeringTest_Whitespace()
        {
            var expression = @"\s";
            var reverseBuilder = new ReverseBuilder(expression);
            Assert.AreEqual(RegexMagexLexicon.FormallydentifyableSegments[SegmentNames.CharactersWhiteSpaces].Magex,
                (reverseBuilder.Segments[0] as FormallyIdentifiedSegment).Magex);

            //var magex = Magex.New().CharacterIn(Characters.WhiteSpace);
            Assert.AreEqual(".CharacterIn(Characters.WhiteSpace)", reverseBuilder.MagexPossibilities[0]);
        }

        [TestMethod]
        public void ReverseEngineeringTest_NumericPlusWhitespace()
        {
            var expression = @"\d\s";
            var reverseBuilder = new ReverseBuilder(expression);

            Assert.AreEqual(2, reverseBuilder.Segments.Count);
            Assert.AreEqual(RegexMagexLexicon.FormallydentifyableSegments[SegmentNames.CharactersNumeral].Magex,
                (reverseBuilder.Segments[0] as FormallyIdentifiedSegment).Magex);
            Assert.AreEqual(RegexMagexLexicon.FormallydentifyableSegments[SegmentNames.CharactersWhiteSpaces].Magex,
                (reverseBuilder.Segments[1] as FormallyIdentifiedSegment).Magex);

            //var magex = Magex.New().CharacterIn(Characters.Numeral).CharacterIn(Characters.WhiteSpace);
            Assert.AreEqual(".CharacterIn(Characters.Numeral).CharacterIn(Characters.WhiteSpace)", reverseBuilder.MagexPossibilities[0]);
        }

        [TestMethod]
        public void ReverseEngineeringTest_NumericWhitespaceNumeric()
        {
            var expression = @"\d\s\d";
            var reverseBuilder = new ReverseBuilder(expression);

            Assert.AreEqual(3, reverseBuilder.Segments.Count);
            Assert.AreEqual(RegexMagexLexicon.FormallydentifyableSegments[SegmentNames.CharactersNumeral].Magex,
                (reverseBuilder.Segments[0] as FormallyIdentifiedSegment).Magex);
            Assert.AreEqual(RegexMagexLexicon.FormallydentifyableSegments[SegmentNames.CharactersWhiteSpaces].Magex,
                (reverseBuilder.Segments[1] as FormallyIdentifiedSegment).Magex);
            Assert.AreEqual(RegexMagexLexicon.FormallydentifyableSegments[SegmentNames.CharactersNumeral].Magex,
                (reverseBuilder.Segments[2] as FormallyIdentifiedSegment).Magex);

            //var magex = Magex.New().CharacterIn(Characters.Numeral).CharacterIn(Characters.WhiteSpace).CharacterIn(Characters.Numeral);
            Assert.AreEqual(".CharacterIn(Characters.Numeral).CharacterIn(Characters.WhiteSpace).CharacterIn(Characters.Numeral)", reverseBuilder.MagexPossibilities[0]);
        }

        [TestMethod]
        public void ReverseEngineeringTest_DoubleEscapingBackslash()
        {
            var expression = @"\\";
            var reverseBuilder = new ReverseBuilder(expression);
            Assert.AreEqual(2, reverseBuilder.Segments.Count);
            Assert.IsTrue(reverseBuilder.Segments[0] is EscapingSegment);
            Assert.IsTrue(reverseBuilder.Segments[1] is UnidentifiedSegment);

            var magex = Magex.New().Character('\\');
            Assert.AreEqual(".Character('\\')", reverseBuilder.MagexPossibilities[0]);
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

        [Ignore] //Concept work required to handle wrapping literals (X)
        [TestMethod]
        public void ReverseEngineeringTest_EscapingWhitespace()
        {
            var expression = @"\\s";
            var reverseBuilder = new ReverseBuilder(expression);
            Assert.AreEqual(3, reverseBuilder.Segments.Count);

            //var magex = Magex.New().Character('\\').Character('s');
            //var magex = Magex.New().Literal("\\s");
            Assert.AreEqual(".Literal(\"\\s\")", reverseBuilder.MagexPossibilities[0]);
        }
    }
}
