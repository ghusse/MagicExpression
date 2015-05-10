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
            string expression = @"\d\s";
            var list = expression.ParseMagex();
            var flattenedList = list.Flatten();
            Assert.AreEqual(".CharacterIn(Characters.Numeral).CharacterIn(Characters.WhiteSpace)", flattenedList);
        }

        [TestMethod]
        public void ReverseEngineeringTest_NumericWhitespaceNumeric()
        {
            string expression = @"\d\s\d";
            var list = expression.ParseMagex();
            var flattenedList = list.Flatten();
            Assert.AreEqual(".CharacterIn(Characters.Numeral).CharacterIn(Characters.WhiteSpace).CharacterIn(Characters.Numeral)", flattenedList);
        }

        [TestMethod]
        public void ReverseEngineeringTest_DoubleEscapingBackslash()
        {
            string expression = @"\\";
            var list = expression.ParseMagex();
            var flattenedList = list.Flatten();
            Assert.AreEqual(".Character('\\')", flattenedList);
        }

        [TestMethod]
        public void ReverseEngineeringTest_TripleEscapingBackslash()
        {
            string expression = @"\\\"; // Illegal... WTF should we do?
        }

        [Ignore] //Concept work required to handle wrapping literals (X)
        [TestMethod]
        public void ReverseEngineeringTest_EscapingWhitespace()
        {
            string expression = @"\\s";
            var list = expression.ParseMagex();
            var flattenedList = list.Flatten();
            Assert.AreEqual(".Literal(\"\\s\")", flattenedList);
        }
    }
}
