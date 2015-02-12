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
    public class ReverseEngineeringTest_ForbiddenChars
    {
        [TestMethod]
        public void ReverseEngineeringTest_SimplyForbiddenChars()
        {
            //Magex.New().CharacterNotIn("abc");
            Assert.AreEqual("Magex.New().CharacterNotIn(\"abc\");", Magex.ReverseEngineer(@"[^abc]"));
        }

        [Ignore] //Not sure what this should do
        [TestMethod]
        public void ReverseEngineeringTest_ForbiddenCharsWithEscape()
        {
            //Magex.New().CharacterNotIn("abc\\]");
            Assert.AreEqual("Magex.New().CharacterNotIn(\"abc\");", Magex.ReverseEngineer(@"[^abc\\]]"));
        }
    }
}
