namespace MagicExpression.Test
{
	using System;
	using System.Text;
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	/// <summary>
	/// Summary description for RepetitionTest
	/// </summary>
	[TestClass]
	public class RepetitionTest : MagicTest
	{
		[TestInitialize]
		public override void Setup()
		{
			base.Setup();
		}

		[TestMethod]
		public void OneTest()
		{
			this.Magic.Character('a')
				.Character('b').Repeat.Once()
				.Character('c');

			this.AssertIsMatching("abc", "aabc");
			this.AssertIsNotMatching("abbc");
		}

		[TestMethod]
		public void AnyTest()
		{
			this.Magic.Character('a')
				.Character('b').Repeat.Any()
				.Character('c');

			this.AssertIsMatching("ac", "abc", "abbc");
			this.AssertIsNotMatching("bc", "ab");
		}

		[TestMethod]
		public void AtLeastOnceTest()
		{
			this.Magic.Character('a')
				.Character('b').Repeat.AtLeastOnce()
				.Character('c');

			this.AssertIsMatching("abc", "abbc");
			this.AssertIsNotMatching("ac", "bc", "ab");
		}

		[TestMethod]
		public void AtMostOnceTest()
		{
			this.Magic.Character('a')
				.Character('b').Repeat.AtMostOnce()
				.Character('c');

			this.AssertIsMatching("abc", "ac");
			this.AssertIsNotMatching("abbc", "ab", "bc");
		}

		[TestMethod]
		public void TimesTest()
		{
			this.Magic.Character('a')
				.Character('b').Repeat.Times(3)
				.Character('c');

			this.AssertIsMatching("aabbbccc", "abbbc");
			this.AssertIsNotMatching("abbc", "aaabccc");
		}
	}
}
