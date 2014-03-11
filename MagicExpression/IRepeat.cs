namespace MagicExpression
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public interface IRepeat
	{
		ILasiness Any();
		IChainable Once();
		IChainable Times(uint number);
		ILasiness AtMostOnce();
		ILasiness Between(uint min, uint max);
		ILasiness AtLeastOnce();
	}
}
