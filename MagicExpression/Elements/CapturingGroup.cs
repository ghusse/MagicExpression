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
                //return RegexMagexLexicon.PartiallyIdentifyableSegments[SegmentNames.ParenthesisBegin] 
                //    + this.Grouped.Expression 
                //    + RegexMagexLexicon.PartiallyIdentifyableSegments[SegmentNames.ParenthesisEnd];
                return "(" + this.Grouped.Expression + ")";
            }
        }

		protected IExpressionElement Grouped { get; private set; }
	}
}
