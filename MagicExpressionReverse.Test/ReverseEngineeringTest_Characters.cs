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
        public void ReverseEngineeringTest_SingleNumeral()
        {           
            //var magex = Magex.New().CharacterIn(Characters.Numeral);
            Assert.AreEqual("Magex.New().CharacterIn(Characters.Numeral);", Magex.ReverseEngineer(@"\d"));
        }

        [TestMethod]
        public void ReverseEngineeringTest_Whitespace()
        {
            //var magex = Magex.New().CharacterIn(Characters.WhiteSpace);
            Assert.AreEqual("Magex.New().CharacterIn(Characters.WhiteSpace);", Magex.ReverseEngineer(@"\s"));
        }

        [TestMethod]
        public void ReverseEngineeringTest_NumericPlusWhitespace()
        {
            //var magex = Magex.New().CharacterIn(Characters.Numeral).CharacterIn(Characters.WhiteSpace);
            Assert.AreEqual("Magex.New().CharacterIn(Characters.Numeral).CharacterIn(Characters.WhiteSpace)", Magex.ReverseEngineer(@"\d\s"));
        }

        [TestMethod]
        public void ReverseEngineeringTest_NumericWhitespaceNumeric()
        {
            //var magex = Magex.New().CharacterIn(Characters.Numeral).CharacterIn(Characters.WhiteSpace).CharacterIn(Characters.Numeral);
            Assert.AreEqual(".CharacterIn(Characters.Numeral).CharacterIn(Characters.WhiteSpace).CharacterIn(Characters.Numeral)", Magex.ReverseEngineer(@"\d\s\d"));
        }

        [TestMethod]
        public void ReverseEngineeringTest_DoubleEscapingBackslash()
        {
            //var magex = Magex.New().Character('\\');
            Assert.AreEqual(".Character('\\')", Magex.ReverseEngineer(@"\\"));
        }

        [Ignore]
        [TestMethod]
        public void ReverseEngineeringTest_TripleEscapingBackslash()
        {
            //var expression = @"\\\";
            //var reverseBuilder = new ReverseBuilder(expression);
            //Assert.AreEqual(3, reverseBuilder.Segments.Count);
            //Assert.IsTrue(reverseBuilder.Segments[0] is EscapingSegment);
            //Assert.IsTrue(reverseBuilder.Segments[1] is UnidentifiedSegment);
            //Assert.IsTrue(reverseBuilder.Segments[2] is EscapingSegment);
        }

        [Ignore] //Concept work required to handle wrapping literals (X)
        [TestMethod]
        public void ReverseEngineeringTest_EscapingWhitespace()
        {
            //var magex = Magex.New().Character('\\').Character('s');
            //var magex = Magex.New().Literal("\\s");
            Assert.AreEqual("Magex.New().Literal(\"\\s\")", Magex.ReverseEngineer(@"\\s"));
        }
    }
}
