namespace MagicExpression
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public interface IChainable : IExpressionElement
	{
		IRepeatable CharacterIn(Characters chars, string otherChars);
		IRepeatable CharacterIn(Characters chars, params char[] otherChars);
		IRepeatable CharacterIn(string otherChars);
		IRepeatable CharacterIn(params char[] otherChars);
		IRepeatable CharacterNotIn(Characters chars, string otherChars);
		IRepeatable CharacterNotIn(Characters chars, params char[] otherChars);
		IRepeatable CharacterNotIn(string otherChars);
		IRepeatable CharacterNotIn(params char[] otherChars);
		IRepeatable Character();
		IChainable StartOfLine();
		IChainable EndOfLine();

		IRepeatable Group(IExpressionElement grouped);
		IRepeatable Capture(IExpressionElement captured);
		IRepeatable CaptureAs(string name, IExpressionElement captured);

		IRepeatable Group(Action<IChainable> expression);
		IRepeatable Capture(Action<IChainable> expression);
		IRepeatable CaptureAs(string name, Action<IChainable> expression);

		IChainable String(string val);
		IRepeatable Character(char theChar);

		IRepeatable Alternative(params IExpressionElement[] alternatives);
		IRepeatable Alternative(params Action<IChainable>[] alternatives);

		IRepeatable BackReference(string name);
		IRepeatable BackReference(uint index);
	}
}
