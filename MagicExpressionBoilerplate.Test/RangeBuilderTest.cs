using System;
using System.Text.RegularExpressions;
using MagicExpression;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagicExpressionBoilerplate.Test
{
    public abstract class RangeTester
    {
        protected void AssertIsMatching(string range, string[] testData)
        {
            var rangePattern = GetPattern(range);
            var expression = new Regex(rangePattern);

            foreach (string shouldMatch in testData)
            {
                var message = String.Format("Expression: {0} should match: {1}", rangePattern, shouldMatch);
                Assert.IsTrue(expression.IsMatch(shouldMatch), message);
            }
        }

        protected void AssertIsNotMatching(string range, string[] testData)
        {
            var rangePattern = GetPattern(range);
            var expression = new Regex(rangePattern);

            foreach (string shouldMatch in testData)
            {
                var message = String.Format("Expression: {0} should not match: {1}", rangePattern, shouldMatch);
                Assert.IsFalse(expression.IsMatch(shouldMatch), message);
            }
        }

        private static string GetPattern(string range)
        {
            return string.Format(@"\b{0}\b", MagexBuilder.CreateNumericRange(range));
        }
    }

    [TestClass]
    public class RangeBuilderTest: RangeTester
    {
        [TestMethod]
        public void OneDigitRange()
        {
            const string argument = "0-9";
            this.AssertIsMatching(argument, new[] { "0", "9" });
            this.AssertIsNotMatching(argument, new[] { "10" });
        }

        [TestMethod]
        public void TwoDigitRange()
        {
            const string argument = "0-99";
            this.AssertIsMatching(argument, new[] { "0", "9", "99" });
            this.AssertIsNotMatching(argument, new[] { "100", "999" });
        }

        [TestMethod]
        public void ThreeDigitRange()
        {
            const string argument = "0-999";
            this.AssertIsMatching(argument, new[] { "0", "9", "99", "999" });
            this.AssertIsNotMatching(argument, new[] { "1000", "9999" });
        }

        [TestMethod]
        public void FourDigitRange()
        {
            const string argument = "0-9999";
            this.AssertIsMatching(argument, new[] { "0", "9", "99", "999", "9999" });
            this.AssertIsNotMatching(argument, new[] { "10000", "99999" });
        }

        [TestMethod]
        public void Range0_255()
        {
            const string argument = "0-255";
            this.AssertIsMatching(argument, new[] { "0", "9", "10", "19", "20", "42", "99", "100", "199", "200", "249", "250", "255" });
            this.AssertIsNotMatching(argument, new[] { "256" });
        }
    }
}
