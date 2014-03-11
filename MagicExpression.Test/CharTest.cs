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

		[TestMethod]
		public void CharacterNotInTest()
		{
			this.Magic.CharacterNotIn(Characters.LowerCaseLetter, "%");

			this.AssertIsMatching("L", "9", "$");
			this.AssertIsNotMatching("a", "z", "%");
		}

		[TestMethod]
		public void OneCharTest()
		{
			this.Magic.Character('a');

			this.AssertIsMatching("ab", "ba");
			this.AssertIsNotMatching("b");
		}

		[TestMethod]
		public void CharListTest()
		{
			this.Magic.CharacterIn('a', 'b');

			this.AssertIsMatching("ac", "bc");
			this.AssertIsNotMatching("cd");
		}

		[TestMethod]
		public void CharListNotInTest()
		{
			this.Magic.CharacterNotIn('a', 'b');

			this.AssertIsMatching("ac", "cb");
			this.AssertIsNotMatching("ab");
		}

		[TestMethod]
		public void CharStringTest()
		{
			this.Magic.CharacterIn("abcde");

			this.AssertIsMatching("az", "9b");
			this.AssertIsNotMatching("z9");
		}

		[TestMethod]
		public void CharNotInStringTest()
		{
			this.Magic.CharacterNotIn("abcde");

			this.AssertIsMatching("az", "zb");
			this.AssertIsNotMatching("ab", "bc");
		}

		[TestMethod]
		public void AnyCharTest()
		{
			this.Magic.Character();

			this.AssertIsMatching("0", "o", "^");
			this.AssertIsNotMatching(string.Empty);
		}
	}
}
