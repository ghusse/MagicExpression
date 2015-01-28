using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicExpression.ReverseEngineering
{
    public interface ISegment
    {
        string RegexSegment { get; set; }
        int StartIndex { get; set; }
        int StopIndex { get; set; }
    }

    public abstract class Segment : ISegment
    {
        public int StartIndex { get; set; }
        public int StopIndex { get; set; }

        public string RegexSegment { get; set; }

        public Segment(int _startIndex, int _stopIndex, string _regexSegment)
        {
            this.StartIndex = _startIndex;
            this.StopIndex = _stopIndex;
            this.RegexSegment = _regexSegment;
        }
    }

    public abstract class IdentifiedSegment: Segment
    {
        public string CharacterSet { get; set; }

        public IdentifiedSegment(int _startIndex, int _stopIndex, string _regexSegment, string _characterSet)
            :base(_startIndex, _stopIndex, _regexSegment)
        {
            this.CharacterSet = _characterSet;
        }
    }

    public class EscapingSegment: IdentifiedSegment
    {
        public EscapingSegment(int _startIndex, int _stopIndex, string _regexSegment)
            : base(_startIndex, _stopIndex, _regexSegment, "EscapingBackslash")
        {
        }
    }

    public class UnidentifiedSegment: Segment
    {
        public UnidentifiedSegment(int _startIndex, int _stopIndex, string _regexSegment) 
            : base(_startIndex, _stopIndex, _regexSegment)
        {
        }
    }

    public class FormallyIdentifiedSegment: IdentifiedSegment
    {
        public FormallyIdentifiedSegment(int _startIndex, int _stopIndex, string _regexSegment, string _characterSet)
            : base(_startIndex, _stopIndex, _regexSegment, _characterSet)
        {
        }
    }

    public class PotentiallyIdentifiedSegment : IdentifiedSegment
    {
        public PotentiallyIdentifiedSegment(int _startIndex, int _stopIndex, string _regexSegment, string _characterSet)
            : base(_startIndex, _stopIndex, _regexSegment, _characterSet)
        {
        }
    }
}
