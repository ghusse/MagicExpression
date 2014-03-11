namespace MagicExpression
{

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
