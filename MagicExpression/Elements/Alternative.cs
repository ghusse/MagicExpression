namespace MagicExpression.Elements
{
	using System.Collections.Generic;
	using System.Text;

	public class Alternative : IExpressionElement
	{
		private List<IExpressionElement> elements;

		public Alternative(params IExpressionElement[] elements)
		{
			this.elements = new List<IExpressionElement>(elements);
		}

		public string Expression
		{
			get
			{
				StringBuilder sb = new StringBuilder();

				sb.Append(RegexParts.UniquelyIdentifiedSegments["AlternativeBegin"]);

				for (var i = 0; i < elements.Count; i++)
				{
					if (i > 0)
					{
                        sb.Append(RegexParts.UniquelyIdentifiedSegments["AlternativeSeparator"]);
					}

					sb.Append(this.elements[i].Expression);
				}

                sb.Append(RegexParts.UniquelyIdentifiedSegments["AlternativeEnd"]);

				return sb.ToString();
			}
		}
	}
}
