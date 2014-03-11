namespace MagicExpression.Elements
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class ForbiddenChars : IExpressionElement
	{
		public ForbiddenChars(Characters chars, char[] other)
		{
			this.Expression = "[^" + RegexCharacters.Get(chars) + RegexCharacters.Escape(other) + "]";
		}

		public string Expression { get; private set; }
	}
}
