namespace MagicExpression.Elements
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class PossibleChars : IExpressionElement
	{
		private string characters;

		public PossibleChars(Characters specialChars, char[] chars)
		{
			this.characters = RegexCharacters.Get(specialChars) + RegexCharacters.Escape(chars);
		}

		public string Expression
		{
			get
			{
				return "[" + this.characters + "]";
			}
		}
	}
}
