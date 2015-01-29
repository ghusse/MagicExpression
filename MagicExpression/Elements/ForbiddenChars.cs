namespace MagicExpression.Elements
{

	public class ForbiddenChars : IExpressionElement
	{
		public ForbiddenChars(Characters chars, char[] other)
		{
            this.Expression = RegexParts.FormallydentifyableSegments[RegexParts.ForbiddenCharsBegin]
                + RegexCharacters.Get(chars) + RegexCharacters.Escape(other)
                + RegexParts.FormallydentifyableSegments[RegexParts.ForbiddenCharsEnd];
		}

		public string Expression { get; private set; }
	}
}
