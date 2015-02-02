namespace MagicExpression.Elements
{

	public class NamedBackReference : NamedElement, IExpressionElement
	{
		public NamedBackReference(string name)
			: base(name)
		{
            //this.Expression = RegexMagexLexicon.FormallydentifyableSegments[SegmentNames.NamedBackReferenceBegin]
            //    + name
            //    + RegexMagexLexicon.FormallydentifyableSegments[SegmentNames.NamedBackReferenceEnd];
            this.Expression = @"\k<" + name + ">";
        }

		public string Expression { get; private set; }
	}
}
