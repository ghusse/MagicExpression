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
    public class ReverseEngineeringTest_Backreferences
    {
        [TestMethod]
        public void ReverseEngineeringTest_BackReference()
        {
            var expression = @"\k<abc>def";
            var reverseBuilder = new ReverseBuilder(expression);

            // "abc" is recognized as a sequence of three characters 
            Assert.AreEqual(8, reverseBuilder.Segments.Count);
        }

        [TestMethod]
        public void ReverseEngineeringTest_BackReferenceWithBS()
        {
            var expression = @"\k<a\dcd>def";
            var reverseBuilder = new ReverseBuilder(expression);

            // "\d" is recognized as a special sequence, not sure if this should be as this
            Assert.AreEqual(9, reverseBuilder.Segments.Count);
        }
    }
}
