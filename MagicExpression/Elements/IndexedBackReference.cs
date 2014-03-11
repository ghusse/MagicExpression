namespace MagicExpression.Elements
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class IndexedBackReference : IExpressionElement
	{
		public IndexedBackReference(uint index)
		{
			this.Expression = @"\k" + index.ToString();
		}

		public string Expression { get; private set; }
	}
}
