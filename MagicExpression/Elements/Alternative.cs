namespace MagicExpression.Elements
{
    using MagicExpression.RegexMagexLexicon;
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

				sb.Append(RegexMagexLexicon.FormallydentifyableSegments[SegmentNames.AlternativeBegin]);

				for (var i = 0; i < elements.Count; i++)
				{
					if (i > 0)
					{
                        sb.Append(RegexMagexLexicon.FormallydentifyableSegments[SegmentNames.AlternativeSeparator]);
					}

					sb.Append(this.elements[i].Expression);
				}

                sb.Append(RegexMagexLexicon.FormallydentifyableSegments[SegmentNames.ParenthesisEnd]);

				return sb.ToString();
			}
		}
	}
}
