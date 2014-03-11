namespace MagicExpression
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public interface IRepeatable : IChainable
	{
		IRepeat Repeat { get; }
	}
}
