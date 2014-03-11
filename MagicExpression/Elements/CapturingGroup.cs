namespace MagicExpression.Elements
{

	public class CapturingGroup : IExpressionElement
	{
		public CapturingGroup(IExpressionElement element)
		{
			this.Grouped = element;
		}

		public CapturingGroup(string regex)
			: this(new Literal(regex))
		{
		}

		public virtual string Expression
		{
			get
			{
				return "(" + this.Grouped.Expression + ")";
			}
		}

		protected IExpressionElement Grouped { get; private set; }
	}
}
