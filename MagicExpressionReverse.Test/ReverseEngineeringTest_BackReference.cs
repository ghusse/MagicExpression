using MagicExpression;
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
            //Magex.New().BackReference("abc").Literal("def");
            Assert.AreEqual("Magex.New().BackReference(\"abc\").Literal(\"def\");", Magex.ReverseEngineer(@"\k<abc>def"));
        }

        [Ignore] //Not sure what the output of this should be
        [TestMethod]
        public void ReverseEngineeringTest_BackReferenceWithBS()
        {
            //Magex.New().BackReference("abc").Literal("def");
            Assert.AreEqual("Magex.New().BackReference(\"abc\").Literal(\"def\");", Magex.ReverseEngineer(@"\k<a\dcd>def"));
        }
    }
}
