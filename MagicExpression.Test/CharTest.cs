namespace MagicExpression.Test
{
	using System;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class CharTest : MagicTest
	{
		[TestInitialize]
		public override void Setup()
		{
			base.Setup();
		}

		[TestMethod]
		public void CharacterInTest()
		{
			this.Magic.CharacterIn(Characters.Alphanumeric, "-");

			this.AssertIsMatching("a", "Z", "6", "-");
			this.AssertIsNotMatching("$", "^");
		}

		[TestMethod]
		public void CharacterInEscapedTest()
		{
			string special = @".${[(|*+?\)";
			this.Magic.CharacterIn(special);

			this.AssertIsMatching(".", "$", "{", "[", "(", "|", "*", "+", "?", @"\", ")");
		}
	}
}
