namespace MagicExpression.Test
{
	using System;
	using System.Text;
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	/// <summary>
	/// Summary description for BackReferenceTest
	/// </summary>
	[TestClass]
	public class BackReferenceTest : MagicTest
	{
		[TestInitialize]
		public override void Setup()
		{
			base.Setup();
		}

		[TestMethod]
		public void IndexedTest()
		{
			this.Magic.Capture(x => x.String("abcd"))
				.BackReference(1);

			this.AssertIsMatching("abcdabcd", "aabcdabcdd");
			this.AssertIsNotMatching("abcd");
		}

		[TestMethod]
		public void NamedTest()
		{
			this.Magic.CaptureAs("myCapture", x => x.String("ab"))
				.BackReference("myCapture");

			this.AssertIsMatching("abab", "aababb");
			this.AssertIsNotMatching("ab", "aba");
		}
	}
}
