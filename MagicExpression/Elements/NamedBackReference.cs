namespace MagicExpression.Elements
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

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
