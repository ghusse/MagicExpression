namespace MagicExpression
{
    public partial class Magex
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
            return MagexBuilder.NumericRange(from, to);
        }
    }
}
