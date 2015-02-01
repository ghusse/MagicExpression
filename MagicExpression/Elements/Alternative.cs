namespace MagicExpression.Elements
{
    using MagicExpression.RegexParts;
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

				sb.Append(RegexParts.FormallydentifyableSegments[SegmentNames.AlternativeBegin]);

				for (var i = 0; i < elements.Count; i++)
				{
					if (i > 0)
					{
                        sb.Append(RegexParts.FormallydentifyableSegments[SegmentNames.AlternativeSeparator]);
					}

					sb.Append(this.elements[i].Expression);
				}

                sb.Append(RegexParts.FormallydentifyableSegments[SegmentNames.ParenthesisEnd]);

				return sb.ToString();
			}
		}
	}
}
