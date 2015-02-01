namespace MagicExpression.Elements
{

	public class ForbiddenChars : IExpressionElement
	{
		public ForbiddenChars(Characters chars, char[] other)
		{
            this.Expression = RegexParts.FormallydentifyableSegments[SegmentNames.ForbiddenCharsBegin]
                + RegexCharacters.Get(chars) + RegexCharacters.Escape(other)
                + RegexParts.FormallydentifyableSegments[SegmentNames.ForbiddenCharsEnd];
		}

		public string Expression { get; private set; }
	}
}
