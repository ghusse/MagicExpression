using System;
using System.Text.RegularExpressions;
using MagicExpression;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagicExpressionBoilerplate.Test
{
	public abstract class RangeTester
	{
		protected void AssertIsMatching(ulong from, ulong to, string[] testData)
		{
			var rangePattern = GetRangePattern(from, to);
			var expression = new Regex(rangePattern);

			foreach (string shouldMatch in testData)
			{
				var message = String.Format("Expression: {0} should match: {1}", rangePattern, shouldMatch);
				Assert.IsTrue(expression.IsMatch(shouldMatch), message);
			}
		}

		protected void AssertIsNotMatching(ulong from, ulong to, string[] testData)
		{
			var rangePattern = GetRangePattern(from, to);
			var expression = new Regex(rangePattern);

			foreach (string shouldMatch in testData)
			{
				var message = String.Format("Expression: {0} should not match: {1}", rangePattern, shouldMatch);
				Assert.IsFalse(expression.IsMatch(shouldMatch), message);
			}
		}

		private static string GetRangePattern(ulong from, ulong to)
		{
			return string.Format(@"\b{0}\b", MagexBuilder.NumericRange(from, to));
		}
	}

	[TestClass]
	public class RangeBuilderTest : RangeTester
	{
		[TestMethod]
		public void OneDigitRange()
		{
			const ulong from = 0, to = 9;

			this.AssertIsMatching(from, to, new[] { "0", "9" });
			this.AssertIsNotMatching(from, to, new[] { "10" });
		}

		[TestMethod]
		public void TwoDigitRange()
		{
			const ulong from = 10, to = 99;

			this.AssertIsMatching(from, to, new[] { "10", "19", "99" });
			this.AssertIsNotMatching(from, to, new[] { "100", "999" });
		}

		[TestMethod]
		public void ThreeDigitRange()
		{
			const ulong from = 21, to = 999;

			this.AssertIsMatching(from, to, new[] { "21", "39", "99", "999" });
			this.AssertIsNotMatching(from, to, new[] { "1000", "9999" });
		}

		[TestMethod]
		public void FourDigitRange()
		{
			const ulong from = 42, to = 9999;

			this.AssertIsMatching(from, to, new[] { "42", "99", "999", "9999" });
			this.AssertIsNotMatching(from, to, new[] { "10000", "99999" });
		}

		[TestMethod]
		public void Range0_255()
		{
			const ulong from = 0, to = 255;

			this.AssertIsMatching(from, to, new[] { "0", "9", "10", "19", "20", "42", "99", "100", "199", "200", "249", "250", "255" });
			this.AssertIsNotMatching(from, to, new[] { "256" });
		}

		[TestMethod]
		[ExpectedException(typeof(RangeException))]
		public void InvalidRange()
		{
			MagexBuilder.NumericRange(10, 5);
		}
	}
}
