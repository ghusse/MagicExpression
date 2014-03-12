namespace MagicExpression.Test
{
	using System;
	using System.Text.RegularExpressions;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class SyntaxTest
	{
		[TestMethod]
		public void Simple()
		{
			var emailDetector = Magex.New();
			string allowedChars = @"!#$%&'*+/=?^_`{|}~-";

			emailDetector
				.CharacterIn(Characters.Alphanumeric, allowedChars).Repeat.AtLeastOnce()
				.Group(x =>
					x.Character('.')
					.CharacterIn(Characters.Alphanumeric, allowedChars).Repeat.AtLeastOnce()
				).Repeat.Any()
				.Character('@')
				.Group(x =>
					x.CharacterIn(Characters.Alphanumeric)
					.Group(y =>
						y.CharacterIn(Characters.Alphanumeric, '-').Repeat.Any()
						.CharacterIn(Characters.Alphanumeric)
					).Repeat.AtMostOnce()
					.Character('.')
				).Repeat.AtLeastOnce()
				.CharacterIn(Characters.Alphanumeric)
				.Group(z =>
					z.CharacterIn(Characters.Alphanumeric, '-').Repeat.Any()
					.CharacterIn(Characters.Alphanumeric)
				).Repeat.AtMostOnce();

			Regex realStuff = new Regex(emailDetector.Expression);

			Assert.IsTrue(realStuff.IsMatch("guillaume@ghusse.com"));
			Assert.IsTrue(realStuff.IsMatch("guillaume+nospam@ghusse.com"));
		}
	}
}
