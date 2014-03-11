namespace MagicExpression.Test
{
	using System;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class GroupTest : MagicTest
	{
		[TestInitialize]
		public override void Setup()
		{
			base.Setup();
		}

		[TestMethod]
		public void NonCapturingTest()
		{
			this.Magic.Character('a')
				.Group(
					Magex.New().String("bcde")
				).Character('f');

			this.AssertIsMatching("abcdef");
			this.AssertIsNotMatching("af", "bcde");

			this.AssertCaptures(matches =>
			{
				return matches.Count == 1
					&& matches[0].Groups.Count == 1
					&& matches[0].Groups[0].Value == "abcdef";
			}, "abcdef");
		}

		[TestMethod]
		public void CaptureTest()
		{
			this.Magic.Character('a')
				.Capture(
					Magex.New().String("bcde")
				).Character('f');

			this.AssertIsMatching("abcdef");
			this.AssertIsNotMatching("af", "bcde", "a", "f");

			this.AssertCaptures(matches =>
			{
				return matches.Count == 1
					&& matches[0].Groups.Count == 2
					&& matches[0].Groups[0].Value == "abcdef"
					&& matches[0].Groups[1].Value == "bcde";
			}, "abcdef");
		}

		[TestMethod]
		public void NamedCaptureTest()
		{
			this.Magic.Character('a')
				.CaptureAs("gotcha", Magex.New().String("bcde"))
				.Character('f');

			this.AssertIsMatching("abcdef");

			this.AssertIsNotMatching("af", "bcde", "a", "f");

			this.AssertCaptures(matches =>
			{
				return matches.Count == 1
					&& matches[0].Groups.Count == 2
					&& matches[0].Groups[0].Value == "abcdef"
					&& matches[0].Groups["gotcha"] != null
					&& matches[0].Groups["gotcha"].Value == "bcde";
			}, "abcdef");
		}

		[TestMethod]
		public void GroupRepetitionTest()
		{
			this.Magic.Character('a')
				.Group(Magex.New().String("bc")).Repeat.Times(3)
				.Character('d');

			this.AssertIsMatching("abcbcbcd");
			this.AssertIsNotMatching("abcbcd", "abcbcbcbcd");
		}
	}
}
