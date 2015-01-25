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
            Assert.IsTrue(reverseBuilder.Expression.Parts[0] != null);
        }

        [TestMethod]
        public void ReverseEngineeringTest_SingleNumeral()
        {
            var expression = @"\d";
            var reverseBuilder = new ReverseBuilder(expression);
            var leaf = reverseBuilder.Expression.Parts[0] as Leaf;
            Assert.IsNotNull(leaf);
            Assert.AreEqual(RegexParts.CharactersNumeral, leaf.CharacterSet);
        }

        [TestMethod]
        public void ReverseEngineeringTest_Whitespace()
        {
            var expression = @"\s";
            var reverseBuilder = new ReverseBuilder(expression);
            var leaf = reverseBuilder.Expression.Parts[0] as Leaf;
            Assert.IsNotNull(leaf);
            Assert.AreEqual(RegexParts.CharactersWhiteSpaces, leaf.CharacterSet);
        }

        [TestMethod]
        public void ReverseEngineeringTest_NumericPlusWhitespace()
        {
            var expression = @"\d\s";
            var reverseBuilder = new ReverseBuilder(expression);
            var leaf1 = reverseBuilder.Expression.Parts[0] as Leaf;
            Assert.IsNotNull(leaf1);
            Assert.AreEqual(RegexParts.CharactersNumeral, leaf1.CharacterSet);
            var leaf2 = reverseBuilder.Expression.Parts[1] as Leaf;
            Assert.IsNotNull(leaf2);
            Assert.AreEqual(RegexParts.CharactersWhiteSpaces, leaf2.CharacterSet);
        }

        [TestMethod]
        public void ReverseEngineeringTest_NumericWhitespaceNumeric()
        {
            var expression = @"\d\s\d";
            var reverseBuilder = new ReverseBuilder(expression);

            var leaf1 = reverseBuilder.Expression.Parts[0] as Leaf;
            Assert.IsNotNull(leaf1);
            Assert.AreEqual(RegexParts.CharactersNumeral, leaf1.CharacterSet);
 
            var leaf2 = reverseBuilder.Expression.Parts[1] as Leaf;
            Assert.IsNotNull(leaf2);
            Assert.AreEqual(RegexParts.CharactersWhiteSpaces, leaf2.CharacterSet);

            var leaf3 = reverseBuilder.Expression.Parts[2] as Leaf;
            Assert.IsNotNull(leaf3);
            Assert.AreEqual(RegexParts.CharactersNumeral, leaf3.CharacterSet);
        }

        [TestMethod]
        public void ReverseEngineeringTest_EscapingWhitespace()
        {
            var expression = @"\\s";
            var reverseBuilder = new ReverseBuilder(expression);
            Assert.AreEqual(3, reverseBuilder.Expression.Parts.Count);
        }


    }
}
