namespace MagicExpression.Elements
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class NonCapturingGroup : CapturingGroup
	{
		public NonCapturingGroup(string regex)
			: base(regex)
		{
		}

		public NonCapturingGroup(IExpressionElement grouped)
			: base(grouped)
		{
		}

		public override string Expression
		{
			get
			{
				return "(?:" + this.Grouped.Expression + ")";
			}
		}
	}
}
