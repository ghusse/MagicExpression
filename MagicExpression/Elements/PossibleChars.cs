namespace MagicExpression.Elements
{

	public class PossibleChars : IExpressionElement
	{
		private string characters;

		public PossibleChars(Characters specialChars, char[] chars)
		{
			this.characters = RegexCharacters.Get(specialChars) + RegexCharacters.Escape(chars);
		}

		public string Expression
		{
			get
			{
				return "[" + this.characters + "]";
			}
		}
	}
}
