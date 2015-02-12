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
    public class ReverseEngineeringTest_Groups
    {
        [Ignore] //WTF?
        [TestMethod]
        public void ReverseEngineeringTest_CapturingGroup()
        {
            //Magex.New()....
            Assert.AreEqual("Magex.New().C...", Magex.ReverseEngineer(@"(?:abc)"));
        }
    }
}
