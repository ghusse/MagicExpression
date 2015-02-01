namespace MagicExpression.Elements
{

	public class CapturingGroup : IExpressionElement
	{
		public CapturingGroup(IExpressionElement element)
		{
			this.Grouped = element;
		}

		public virtual string Expression
		{
			get
			{
                return RegexMagexLexicon.FormallydentifyableSegments[SegmentNames.ParenthesisBegin] + this.Grouped.Expression + RegexMagexLexicon.FormallydentifyableSegments[SegmentNames.ParenthesisEnd];
			}
		}

		protected IExpressionElement Grouped { get; private set; }
	}
}
