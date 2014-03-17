namespace MagicExpression.Elements
{

	public class NamedGroup : NamedElement, IExpressionElement
	{
		private IExpressionElement element;

		public NamedGroup(string name, IExpressionElement grouped)
			: base(name)
		{
			this.element = grouped;
		}

		public string Expression
		{
			get
			{
				return "(?<" + this.Name + ">" + this.element.Expression + ")";
			}
		}
	}
}
