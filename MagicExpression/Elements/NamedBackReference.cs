namespace MagicExpression.Elements
{

	public class NamedBackReference : NamedElement, IExpressionElement
	{
		public NamedBackReference(string name)
			: base(name)
		{
            // @"\k<" + name + ">"
            this.Expression = RegexParts.FormallydentifyableSegments[SegmentNames.NamedBackReferenceBegin]
                + name
                + RegexParts.FormallydentifyableSegments[SegmentNames.NamedBackReferenceEnd];
        }

		public string Expression { get; private set; }
	}
}
