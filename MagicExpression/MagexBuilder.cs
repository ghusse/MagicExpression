using System;
using System.Collections.Generic;
using System.Globalization;

namespace MagicExpression
{
	/// <summary>
	/// Helper class
	/// </summary>
	public static class MagexBuilder
	{
		/// <summary>
		/// Create a regex valid numeric range
		/// </summary>
		/// <param name="from">The first bound of the range</param>
		/// <param name="to">The second bound of the range, must be bigger than the fist bound</param>
		/// <returns>The range as a string, wrapped with parenthesis</returns>
		/// <example>("0", "42") ->  "([0-9]|[1-3][0-9]|4[0-2])"</example>
		public static string NumericRange(ulong from, ulong to)
		{
			if (from > to)
				throw new RangeException("Invalid range, from > to");

			return string.Format(@"\b{0}\b", GetNumericRange(from, to));
		}

		#region Range support functions

		private static string GetNumericRange(ulong from, ulong to)
		{
			IList<string> ranges = DecomposeSteps(from, to);

			var regex = "(?:";

			for (var i = 0; i < ranges.Count - 1; i++)
			{
				string strFrom = ranges[i];
				string strTo = ((Convert.ToInt64(ranges[i + 1])) - 1).ToString(CultureInfo.InvariantCulture);

				for (var j = 0; j < strFrom.Length; j++)
				{
					if (strFrom[j] == strTo[j])
						regex += strFrom[j];
					else
						regex += "[" + strFrom[j] + "-" + strTo[j] + "]";
				}
				regex += "|";
			}

			return regex.Substring(0, regex.Length - 1) + ")";
		}

		private static List<string> DecomposeSteps(ulong from, ulong to)
		{
			ulong increment = 1;
			ulong next = from;
			bool higher = true;

			var ranges = new List<string> { from.ToString(CultureInfo.InvariantCulture) };

			while (true)
			{
				next += increment;
				if (next + increment > to)
				{
					if (next <= to)
						ranges.Add(next.ToString(CultureInfo.InvariantCulture));
					increment /= 10;
					higher = false;
				}
				else
				{
					if (next % (increment * 10) == 0)
					{
						ranges.Add(next.ToString(CultureInfo.InvariantCulture));
						increment = increment * 10;
					}
				}

				if (!higher && increment < 10)
					break;
			}

			ranges.Add((to + 1).ToString(CultureInfo.InvariantCulture));
			return ranges;
		}

		#endregion
	}

	[Serializable]
	public class RangeException : Exception
	{
		public RangeException(string message) : base(message) { }
	}
}
