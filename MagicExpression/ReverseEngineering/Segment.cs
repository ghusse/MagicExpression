using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MagicExpression.ReverseEngineering
{
    public interface ISegment
    {

    }

    public class Segment : ISegment
    {
        public string Regex { get; set; }
        public string Magex { get; set; }

    }
}
