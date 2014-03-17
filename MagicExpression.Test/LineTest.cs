namespace MagicExpression.Test
{
	using System;
	using Microsoft.VisualStudio.TestTools.UnitTesting;


	[TestClass]
	public class LineTest : MagicTest
	{
		[TestInitialize]
		public override void Setup()
		{
			base.Setup();
		}

		[TestMethod]
		public void StartTest()
		{
			this.Magic.StartOfLine()
				.String("abc");

			this.AssertIsMatching("abc");
			this.AssertIsNotMatching("aabc");
		}

		[TestMethod]
		public void EndTest()
		{
			this.Magic.String("abc").EndOfLine();

			this.AssertIsMatching("abc", "aaaabc");
			this.AssertIsNotMatching("abcc");
		}
	}
}
