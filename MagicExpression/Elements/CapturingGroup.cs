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
                return RegexParts.FormallydentifyableSegments[SegmentNames.ParenthesisBegin] + this.Grouped.Expression + RegexParts.FormallydentifyableSegments[SegmentNames.ParenthesisEnd];
			}
		}

		protected IExpressionElement Grouped { get; private set; }
	}
}
