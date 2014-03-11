namespace MagicExpression
{
	using System.Linq;
	using System.Text;
	using System.Text.RegularExpressions;

	public static class RegexCharacters
	{
		private static char[] specialChars = new char[11] { '.', '$', '{', '[', '(', '|', '*', '+', '?', '\\', ')' };
		private static Regex namePattern = new Regex("^[a-z][0-9a-z]*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public static string Get(Characters chars)
		{
			StringBuilder result = new StringBuilder();

			AddIf(chars, Characters.Alphanumeric, @"\w", result);
			AddIf(chars, Characters.NonAlphanumeric, @"\W", result);
			AddIf(chars, Characters.Numeral, @"\d", result);
			AddIf(chars, Characters.NonNumeral, @"\D", result);
			AddIf(chars, Characters.WhiteSpace, @"\s", result);
			AddIf(chars, Characters.NonWhiteSpace, @"\S", result);
			AddIf(chars, Characters.CarriageReturn, @"\r", result);
			AddIf(chars, Characters.NewLine, @"\n", result);
			AddIf(chars, Characters.FormFeed, @"\f", result);
			AddIf(chars, Characters.Tab, @"\t", result);
			AddIf(chars, Characters.VerticalTab, @"\v", result);
			AddIf(chars, Characters.Bell, @"\a", result);
			AddIf(chars, Characters.BackSpace, @"\b", result);
			AddIf(chars, Characters.Escape, @"\e", result);
			AddIf(chars, Characters.Letter, @"a-zA-Z", result);
			AddIf(chars, Characters.UpperCaseLetter, @"A-Z", result);
			AddIf(chars, Characters.LowerCaseLetter, @"a-z", result);

			return result.ToString();
		}

		public static string Escape(char unescaped)
		{
			if (specialChars.Contains(unescaped))
			{
				return @"\" + unescaped;
			}

			return unescaped.ToString();
		}

		public static string Escape(params char[] unescaped)
		{
			StringBuilder result = new StringBuilder();

			foreach (var unescapedChar in unescaped)
			{
				result.Append(Escape(unescapedChar));
			}

			return result.ToString();
		}

		public static bool IsValidName(string name)
		{
			return namePattern.IsMatch(name);
		}

		private static void AddIf(Characters input, Characters testedValue, string testedString, StringBuilder result)
		{
			if ((input & testedValue) == testedValue)
			{
				result.Append(testedString);
			}
		}
	}
}
