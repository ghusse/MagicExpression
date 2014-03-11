namespace MagicExpression.Elements
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class Literal : IExpressionElement
	{
		public Literal(string regex)
		{
			this.Expression = regex;
		}

		public string Expression { get; set; }
	}
}
