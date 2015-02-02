using System;

namespace MagicExpression.ReverseEngineering
{
    public interface ISegment
    {
        string RegexSegment { get; set; }
        int StartIndex { get; set; }
        int StopIndex { get; set; }
        SegmentNames Name { get; set; }
    }

    public abstract class Segment : ISegment
    {
        public int StartIndex { get; set; }
        public int StopIndex { get; set; }

        public string RegexSegment { get; set; }

        public abstract SegmentNames Name { get; set; }

        public Segment(int _startIndex, int _stopIndex, string _regexSegment)
        {
            this.StartIndex = _startIndex;
            this.StopIndex = _stopIndex;
            this.RegexSegment = _regexSegment;
        }
    }

    public abstract class IdentifiedSegment: Segment
    {
        public override SegmentNames Name { get; set; }

        public string Magex { get; set; }

        public IdentifiedSegment(int _startIndex, int _stopIndex, string _regexSegment, SegmentNames _name)
            :base(_startIndex, _stopIndex, _regexSegment)
        {
            this.Name = _name;
            this.Magex = GetMagex(this.Name);
        }

        protected string GetMagex(SegmentNames name)
        {
            if (RegexMagexLexicon.FormallydentifyableSegments.ContainsKey(name))
                return RegexMagexLexicon.FormallydentifyableSegments[name].Magex;
            else if (RegexMagexLexicon.PartiallyIdentifyableSegments.ContainsKey(name))
                return RegexMagexLexicon.PartiallyIdentifyableSegments[name].Magex;
            else if (RegexMagexLexicon.NotIdentifyableSegments.ContainsKey(name))
                return RegexMagexLexicon.NotIdentifyableSegments[name].Magex;
            else
                throw new Exception(String.Format("SegmentName '{0}' not found in the lexicon", name));
        }
    }

    public class EscapingSegment: IdentifiedSegment
    {
        public EscapingSegment(int _startIndex, int _stopIndex, string _regexSegment)
            : base(_startIndex, _stopIndex, _regexSegment, SegmentNames.EscapingBackslash)
        {
        }
    }

    public class UnidentifiedSegment: Segment
    {
        public override SegmentNames Name
        {
            get { return SegmentNames.CharacterSingle; }
            set { throw new NotImplementedException(); }
        }

        public UnidentifiedSegment(int _startIndex, int _stopIndex, string _regexSegment) 
            : base(_startIndex, _stopIndex, _regexSegment)
        {
        }
    }

    public class FormallyIdentifiedSegment: IdentifiedSegment
    {
        public FormallyIdentifiedSegment(int _startIndex, int _stopIndex, string _regexSegment, SegmentNames _name)
            : base(_startIndex, _stopIndex, _regexSegment, _name)
        {
        }
    }

    public class PotentiallyIdentifiedSegment : IdentifiedSegment
    {
        public PotentiallyIdentifiedSegment(int _startIndex, int _stopIndex, string _regexSegment, SegmentNames _name)
            : base(_startIndex, _stopIndex, _regexSegment, _name)
        {
        }
    }

    public class NotIdentifyableSegment : IdentifiedSegment
    {
        public NotIdentifyableSegment(int _startIndex, int _stopIndex, string _regexSegment, SegmentNames _name)
            : base(_startIndex, _stopIndex, _regexSegment, _name)
        {
        }
    }

    public class OptimizedSegment : IdentifiedSegment
    {
        public OptimizedSegment(int _startIndex, int _stopIndex)
            :base(_startIndex, _stopIndex, "", SegmentNames.Optimized)
        {
        }
    }
}
