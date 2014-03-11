namespace MagicExpression.Elements
{

	public class IndexedBackReference : IExpressionElement
	{
		public IndexedBackReference(uint index)
		{
			this.Expression = @"\k" + index.ToString();
		}

		public string Expression { get; private set; }
	}
}
