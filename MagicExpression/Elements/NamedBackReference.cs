namespace MagicExpression.Elements
{

	public class NamedBackReference : NamedElement, IExpressionElement
	{
		public NamedBackReference(string name)
			: base(name)
		{
            // @"\k<" + name + ">"
            this.Expression = RegexMagexLexicon.FormallydentifyableSegments[SegmentNames.NamedBackReferenceBegin]
                + name
                + RegexMagexLexicon.FormallydentifyableSegments[SegmentNames.NamedBackReferenceEnd];
        }

		public string Expression { get; private set; }
	}
}
