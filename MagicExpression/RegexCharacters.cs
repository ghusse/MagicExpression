namespace MagicExpression
{
	using System.Collections.Generic;
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

            foreach(var set in knownSets)
                AddIf(chars, set.Key, set.Value, result);

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

        public static IDictionary<Characters, string> KnownSets { get { return knownSets; } }
        private static IDictionary<Characters, string> knownSets = new Dictionary<Characters, string>()
        {
            {Characters.Alphanumeric, @"\w"},
            {Characters.NonAlphanumeric, @"\W"},
            {Characters.Numeral, @"\d"},
            {Characters.NonNumeral, @"\D"},
            {Characters.WhiteSpace, @"\s"},
            {Characters.NonWhiteSpace, @"\S"},
            {Characters.CarriageReturn, @"\r"},
            {Characters.NewLine, @"\n"},
            {Characters.FormFeed, @"\f"},
            {Characters.Tab, @"\t"},
            {Characters.VerticalTab, @"\v"},
            {Characters.Bell, @"\a"},
            {Characters.BackSpace, @"\b"},
            {Characters.Escape, @"\e"},
            {Characters.Letter, @"a-zA-Z"},
            {Characters.UpperCaseLetter, @"A-Z"},
            {Characters.LowerCaseLetter, @"a-z"}
        };
	}
}
