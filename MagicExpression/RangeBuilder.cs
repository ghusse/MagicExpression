using System;
using System.Collections.Generic;
using System.Globalization;

namespace MagicExpression
{
    /// <summary>
    /// Helper class used to generate regular expression ranges, ex: "1-42" ->  "([0-9]|[1-3][0-9]|4[0-2])"
    /// </summary>
    public static class RangeBuilder
    {
        /// <summary>
        /// Create a regex valid numeric range
        /// </summary>
        /// <param name="argument">The range in format "from-to"</param>
        /// <returns>The range as a string, wrapped with parenthesis</returns>
        /// <example>("0-42") ->  "([0-9]|[1-3][0-9]|4[0-2])"</example>
        public static string CreateNumericRange(string argument)
        {
            var from = Convert.ToInt64(argument.Substring(0, argument.IndexOf('-')));
            var to = Convert.ToInt64(argument.Substring(argument.IndexOf('-') + 1));

            return CreateNumericRange(from, to);
        }

        /// <summary>
        /// Create a regex valid numeric range
        /// </summary>
        /// <param name="from">The first bound of the range</param>
        /// <param name="to">The second bound of the range, must be bigger than the fist bound</param>
        /// <returns>The range as a string, wrapped with parenthesis</returns>
        /// <example>("0", "42") ->  "([0-9]|[1-3][0-9]|4[0-2])"</example>
        public static string CreateNumericRange(long from, long to)
        {
            if (from < 0 || to < 0)
                throw new RangeException("Invalid range, negative numbers are not allowed");
            if (from > to)
                throw new RangeException("Invalid range, from > to");

            return GetNumericRange(from, to);
        }

        private static string GetNumericRange(long from, long to)
        {
            IList<string> ranges = DecomposeSteps(from, to);

            //var regex = "/^(?:";
            var regex = "(";//"?:";

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

            //return regex.Substring(0, regex.Length - 1) + ")$/";
            return regex.Substring(0, regex.Length - 1) + ")";
        }

        private static List<string> DecomposeSteps(long from, long to)
        {
            var increment = 1;
            var next = from;
            var higher = true;

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
                        increment = higher ? increment * 10 : increment / 10;
                    }
                }

                if (!higher && increment < 10)
                    break;
            }

            ranges.Add((to + 1).ToString(CultureInfo.InvariantCulture));
            return ranges;
        }
    }

    public class RangeException: Exception
    {
        public RangeException(string message): base(message){}
    }
}
