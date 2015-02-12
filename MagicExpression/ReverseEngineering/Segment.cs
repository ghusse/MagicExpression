using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MagicExpression.ReverseEngineering
{
    public interface ISegment
    {
        string RegexSegment { get; set; }
        SegmentNames Name { get; set; }

        bool IsIdentified { get; }
    }

    public abstract class Segment : ISegment
    {
        public string RegexSegment { get; set; }

        public abstract SegmentNames Name { get; set; }

        public virtual bool IsIdentified { get { return false; } }

        public Segment(string _regexSegment)
        {
            this.RegexSegment = _regexSegment;
        }
    }

    public abstract class IdentifiedSegment: Segment
    {
        public override SegmentNames Name { get; set; }

        public string Magex { get; set; }

        public override bool IsIdentified { get { return true; } }

        public IdentifiedSegment(string _regexSegment, SegmentNames _name)
            :base(_regexSegment)
        {
            this.Name = _name;
            this.Magex = GetMagex(this.Name);
        }

        protected string GetMagex(SegmentNames name)
        {
            var Lexicons = new[]
            {
                RegexMagexLexicon.FormallydentifyableSegmentsWithBackslashes,
                RegexMagexLexicon.FormallydentifyableSegments,
                RegexMagexLexicon.PartiallyIdentifyableSegments,
                RegexMagexLexicon.NotIdentifyableSegments,
            };

            foreach(var lex in Lexicons)
                if (lex.ContainsKey(name))
                    return lex[name].Magex;

            throw new Exception(String.Format("SegmentName '{0}' not found in the lexicon", name));
        }
    }

    public class EscapingSegment: IdentifiedSegment
    {
        public EscapingSegment(string _regexSegment)
            : base(_regexSegment, SegmentNames.EscapingBackslash)
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

        public UnidentifiedSegment(string _regexSegment) 
            : base( _regexSegment)
        {
        }
    }

    public class FormallyIdentifiedSegment: IdentifiedSegment
    {
        public FormallyIdentifiedSegment(string _regexSegment, SegmentNames _name)
            : base(_regexSegment, _name)
        {
        }
    }

    public class PotentiallyIdentifiedSegment : IdentifiedSegment
    {
        public PotentiallyIdentifiedSegment(string _regexSegment, SegmentNames _name)
            : base(_regexSegment, _name)
        {
        }
    }

    public class NotIdentifyableSegment : IdentifiedSegment
    {
        public NotIdentifyableSegment(string _regexSegment, SegmentNames _name)
            : base(_regexSegment, _name)
        {
        }
    }

    public class OptimizedSegment : IdentifiedSegment
    {
        public OptimizedSegment()
            :base("", SegmentNames.Optimized)
        {
        }
    }
}
