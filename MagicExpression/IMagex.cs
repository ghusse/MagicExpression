namespace MagicExpression
{
	using System;
	using System.Text.RegularExpressions;

	public interface IMagex : IExpressionElement
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

		IMagex StartOfLine();
		IMagex EndOfLine();

		IMagex Literal(string regex);

		IRepeatable Group(IExpressionElement grouped);
		IRepeatable Capture(IExpressionElement captured);
		IRepeatable CaptureAs(string name, IExpressionElement captured);

		IRepeatable Group(Action<IMagex> expression);
		IRepeatable Capture(Action<IMagex> expression);
		IRepeatable CaptureAs(string name, Action<IMagex> expression);

		IMagex String(string val);
		IRepeatable Character(char theChar);

		IRepeatable Alternative(params IExpressionElement[] alternatives);
		IRepeatable Alternative(params Action<IMagex>[] alternatives);

		IRepeatable BackReference(string name);
		IRepeatable BackReference(uint index);

		IBuilder Builder { get; }
		#region Properties
		RegexOptions Options { get; set; }

		Regex Regex { get; }
		#endregion
	}
}
