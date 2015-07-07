using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MagicExpression
{
    /// <summary>
    /// Helper class
    /// </summary>
    public static class MagexBuilder
    {
        private const string NoLeadingNumbers = "(?<![0-9])";
        private const string NoTrailingNumbers = "(?![0-9])";
        private const string LeadingZeros = "0*";

        /// <summary>
        /// Attempts to create a range using the given parameters.
        /// <param name="from">First argument as a <see cref="ulong"/> or <see cref="char"/> Types</param>
        /// <param name="to">Second argument as a <see cref="ulong"/> or <see cref="char"/> Types</param>
        /// </summary>
        /// <returns>The regex or an <see cref="ArgumentException"/></returns>
        public static string CreateRange(object from, object to)
        {
            if (isNumber(from) && isNumber(to))
            {
                var fromLong = Convert.ToUInt64(from);
                var toLong = Convert.ToUInt64(to);

                //Simple numeric range with single digit numbers
                if (0 <= fromLong && fromLong <= 9 && 0 <= toLong && toLong <= 9)
                {
                    return SimpleRange(fromLong, toLong);
                }
                //Complex range with multiple digit numbers
                return NumericRange(fromLong, toLong);
            }
            else if (from is char && to is char)
            {
                return SimpleRange(Convert.ToChar(from), Convert.ToChar(to));
            }
            else
            {
                throw new ArgumentException(
                    string.Format(
                    "Arguments '{0}' and '{1}' passed to the Range function cannot be handled",
                    from, to));
            }
        }

        private static bool isNumber(object value)
        {
            return value is sbyte
                || value is byte
                || value is short
                || value is ushort
                || value is int
                || value is uint
                || value is long
                || value is ulong
                || value is float
                || value is double
                || value is decimal;
        }

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

            return string.Format(@"{0}{1}{2}", NoLeadingNumbers, GetNumericRange(from, to), NoTrailingNumbers);
        }

        public static string NumericRange(ulong from, ulong to, RangeOptions options)
        {
            if (from > to)
                throw new RangeException("Invalid range, from > to");

            return string.Format(@"{0}{1}{2}{3}", NoLeadingNumbers, LeadingZeros, GetNumericRange(from, to), NoTrailingNumbers);
        }

        /// <summary>
        /// Creates a range
        /// </summary>
        /// <param name="from">The first bound of the range</param>
        /// <param name="to">The second bound of the range, must be bigger (ASCII-wise) than the fist bound</param>
        /// <returns>The range as a string</returns>
        /// <example>('a', 'd') ->  "[a-d]" OR (3, 5) -> "[3-5]"</example>
        public static string SimpleRange(object from, object to)
        {
            if (Convert.ToInt64(from) > Convert.ToInt64(to))
                throw new ArgumentException(string.Format("From parameter {0} must be smaller (ASCII-wise) than the to {1} parameter", from, to));

            return string.Format("{0}-{1}", from, to);
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
                {
                    break;
                }
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
