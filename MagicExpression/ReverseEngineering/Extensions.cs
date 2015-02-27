using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicExpression.ReverseEngineering
{
    public static class Extensions
    {
        public static bool ContainsOnly(this string chain, char character)
        {
            foreach (var c in chain)
                if (c != character)
                    return false;
            return true;
        }
    }
}
