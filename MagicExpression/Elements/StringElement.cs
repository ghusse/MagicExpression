namespace MagicExpression.Elements
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class StringElement : IExpressionElement
	{
		public StringElement(string value)
		{
			this.Expression = RegexCharacters.Escape(value.ToCharArray());
		}

		public string Expression { get; set; }
	}
}
