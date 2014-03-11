namespace MagicExpression.Elements
{

	public class ForbiddenChars : IExpressionElement
	{
		public ForbiddenChars(Characters chars, char[] other)
		{
			this.Expression = "[^" + RegexCharacters.Get(chars) + RegexCharacters.Escape(other) + "]";
		}

		public string Expression { get; private set; }
	}
}
