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
                return RegexParts.UniquelyIdentifiedSegments["GroupBegin"] + this.Grouped.Expression + RegexParts.UniquelyIdentifiedSegments["GroupEnd"];
			}
		}

		protected IExpressionElement Grouped { get; private set; }
	}
}
