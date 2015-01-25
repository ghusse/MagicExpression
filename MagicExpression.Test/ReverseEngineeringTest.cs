using MagicExpression.ReverseEngineering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicExpression.Test
{
    [TestClass]
    public class ReverseEngineeringTest
    {
        [TestMethod]
        public void ReverseEngineeringTest_PartsNotNull()
        {
            var expression = @"\d";
            var reverseBuilder = new ReverseBuilder(expression);
            Assert.IsTrue(reverseBuilder.Expression.Parts[0] != null);
        }

        [TestMethod]
        public void ReverseEngineeringTest_Numeric()
        {
            var expression = @"\d";
            var reverseBuilder = new ReverseBuilder(expression);
            var leaf = reverseBuilder.Expression.Parts[0] as Leaf;
            Assert.IsNotNull(leaf);
            Assert.AreEqual(Characters.Numeral, leaf.CharacterSet);
        }
    }
}
