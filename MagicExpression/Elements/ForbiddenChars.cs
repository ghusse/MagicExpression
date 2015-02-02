namespace MagicExpression.Elements
{

	public class ForbiddenChars : IExpressionElement
	{
		public ForbiddenChars(Characters chars, char[] other)
		{
            //this.Expression = RegexMagexLexicon.FormallydentifyableSegments[SegmentNames.ForbiddenCharsBegin]
            //    + RegexCharacters.Get(chars) + RegexCharacters.Escape(other)
            //    + RegexMagexLexicon.FormallydentifyableSegments[SegmentNames.ForbiddenCharsEnd];
            this.Expression = "[^" + RegexCharacters.Get(chars) + RegexCharacters.Escape(other) + "]";
        }

		public string Expression { get; private set; }
	}
}
