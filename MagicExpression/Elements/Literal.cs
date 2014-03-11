namespace MagicExpression.Elements
{

	public class Literal : IExpressionElement
	{
		public Literal(string regex)
		{
			this.Expression = regex;
		}

		public string Expression { get; set; }
	}
}
