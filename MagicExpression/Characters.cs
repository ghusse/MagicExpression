namespace MagicExpression
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	[Flags]
	public enum Characters
	{
		None = 0,
		Letter = 1,
		Numeral = 1 << 1,
		NonNumeral = 1 << 2,
		Alphanumeric = 1 << 3,
		NonAlphanumeric = 1 << 4,
		WhiteSpace = 1 << 5,
		NonWhiteSpace = 1 << 6,
		CarriageReturn = 1 << 7,
		NewLine = 1 << 8,
		FormFeed = 1 << 9,
		Tab = 1 << 10,
		VerticalTab = 1 << 11,
		Bell = 1 << 12,
		BackSpace = 1 << 13,
		Escape = 1 << 14,
		UpperCaseLetter = 1 << 15,
		LowerCaseLetter = 1 << 16
	}
}
