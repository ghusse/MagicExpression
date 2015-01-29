using MagicExpression.ReverseEngineering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicExpressionReverse.Test
{
    [TestClass]
    public class ReverseEngineeringTest_ForbiddenChars
    {
        [TestMethod]
        public void ReverseEngineeringTest_SimplyForbiddenChars()
        {
            var expression = @"[^abc]";
            var reverseBuilder = new ReverseBuilder(expression);

            // Soooo wrong currently takes the groups as well "(" and ")"
            Assert.AreEqual(5, reverseBuilder.Segments.Count);
        }

        [TestMethod]
        public void ReverseEngineeringTest_ForbiddenCharsWithEscape()
        {
            var expression = @"[^abc\\]]";
            var reverseBuilder = new ReverseBuilder(expression);

            // Soooo wrong currently takes the groups as well "(" and ")"
            Assert.AreEqual(8, reverseBuilder.Segments.Count);
        }
    }
}
