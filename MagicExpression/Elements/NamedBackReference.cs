namespace MagicExpression.Elements
{

	public class NamedBackReference : NamedElement, IExpressionElement
	{
		public NamedBackReference(string name)
			: base(name)
		{
			this.Expression = @"\k<" + name + ">";
		}

		public string Expression { get; private set; }
	}
}
