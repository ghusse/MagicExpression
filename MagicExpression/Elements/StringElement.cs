namespace MagicExpression.Elements
{

	public class StringElement : IExpressionElement
	{
		public StringElement(string value)
		{
			this.Expression = RegexCharacters.Escape(value.ToCharArray());
		}

		public string Expression { get; set; }
	}
}
