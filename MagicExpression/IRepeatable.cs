namespace MagicExpression
{

	public interface IRepeatable : IChainable
	{
		IRepeat Repeat { get; }
	}
}
