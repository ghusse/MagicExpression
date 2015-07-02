namespace MagicExpression.Elements
{

	public class UnescapedStringElement : IExpressionElement
	{
        public UnescapedStringElement(string value)
		{
			this.Expression = value;
		}

		public string Expression { get; set; }
	}
}
