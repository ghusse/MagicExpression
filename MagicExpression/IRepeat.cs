namespace MagicExpression
{

	public interface IRepeat
	{
		ILasiness Any();
		IMagex Once();
		IMagex Times(uint number);
		ILasiness AtMostOnce();
		ILasiness Between(uint min, uint max);
		ILasiness AtLeastOnce();
	}
}
