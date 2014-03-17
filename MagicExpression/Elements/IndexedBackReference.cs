namespace MagicExpression.Elements
{

	public class IndexedBackReference : IExpressionElement
	{
		public IndexedBackReference(uint index)
		{
			this.Expression = @"\" + index.ToString();
		}

		public string Expression { get; private set; }
	}
}
