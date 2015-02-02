using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicExpression
{
    public enum SegmentNames
    {
        EscapingBackslash = 0,
        Optimized,

        // FormallydentifyableSegments
        CharactersAlphanumeric,
        CharactersNonAlphanumeric,
        CharactersNumeral,
        CharactersNonNumeral,
        CharactersWhiteSpaces,
        CharactersNonWhiteSpace,
        CharactersCarriageReturn,
        CharactersNewLine,
        CharactersFormFeed,
        CharactersTab,
        CharactersVerticalTab,
        CharactersBell,
        CharactersBackSpace,
        CharactersEscape,
        CharactersLetter,
        CharactersUpperCaseLetter,
        CharactersLowerCaseLetter,
        CharacterSingle,
        AlternativeBegin,
        AlternativeSeparator,
        ParenthesisEnd,
        ParenthesisBegin,
        ForbiddenCharsBegin,
        ForbiddenCharsEnd,
        NamedBackReferenceBegin,
        NamedBackReferenceEnd,
        Literal,
        
    }

    public static class RegexMagexLexicon
    {
        public static IDictionary<SegmentNames, RegexString> FormallydentifyableSegments = new Dictionary<SegmentNames, RegexString>()
        {
            // The escape is transparent in itself
            {SegmentNames.EscapingBackslash, new RegexString(@"\\", @"") },

            {SegmentNames.CharactersAlphanumeric, new RegexString(@"\w",  @".CharacterIn(Characters.Alphanumeric)")},
            {SegmentNames.CharactersNonAlphanumeric, new RegexString(@"\W", @".CharacterIn(Characters.NonAlphanumeric)")},
            {SegmentNames.CharactersNumeral, new RegexString(@"\d", @".CharacterIn(Characters.Numeral)")},
            {SegmentNames.CharactersNonNumeral, new RegexString(@"\D", @".CharacterIn(Characters.NonNumeral)")},
            {SegmentNames.CharactersWhiteSpaces, new RegexString(@"\s", @".CharacterIn(Characters.WhiteSpace)")},
            {SegmentNames.CharactersNonWhiteSpace, new RegexString(@"\S", @".CharacterIn(Characters.NonWhiteSpace)")},
            {SegmentNames.CharactersCarriageReturn, new RegexString(@"\r", @".CharacterIn(Characters.CarriageReturn)")},
            {SegmentNames.CharactersNewLine, new RegexString(@"\n", @".CharacterIn(Characters.NewLine)")},
            {SegmentNames.CharactersFormFeed, new RegexString(@"\f", @".CharacterIn(Characters.FormFeed)")},
            {SegmentNames.CharactersTab, new RegexString(@"\t", @".CharacterIn(Characters.Tab)")},
            {SegmentNames.CharactersVerticalTab, new RegexString(@"\v", @".CharacterIn(Characters.VerticalTab)")},
            {SegmentNames.CharactersBell, new RegexString(@"\a", @".CharacterIn(Characters.Bell)")},
            {SegmentNames.CharactersBackSpace, new RegexString(@"\b", @".CharacterIn(Characters.BackSpace)")},
            {SegmentNames.CharactersEscape, new RegexString(@"\e", @".CharacterIn(Characters.Escape)")},
            {SegmentNames.CharactersLetter, new RegexString(@"a-zA-Z", @".CharacterIn(Characters.Letter)")},
            {SegmentNames.CharactersUpperCaseLetter, new RegexString(@"A-Z", @".CharacterIn(Characters.UpperCaseLetter)")},
            {SegmentNames.CharactersLowerCaseLetter, new RegexString(@"a-z", @".CharacterIn(Characters.LowerCaseLetter)")},
            {SegmentNames.AlternativeBegin, new RegexString(@"(?:", @".Capture(")},
            {SegmentNames.AlternativeSeparator, new RegexString(@"|", @",")},
            {SegmentNames.ForbiddenCharsBegin, new RegexString(@"[^", @".CharacterNotIn(")},
            {SegmentNames.ForbiddenCharsEnd, new RegexString(@"]", @")")},
            {SegmentNames.NamedBackReferenceBegin, new RegexString(@"\k<", @".BackReference(") },
            {SegmentNames.NamedBackReferenceEnd, new RegexString(@">", @")") },
        };

        public static IDictionary<SegmentNames, RegexString> PartiallyIdentifyableSegments = new Dictionary<SegmentNames, RegexString>()
        {
            {SegmentNames.ParenthesisEnd, new RegexString(@")", @")")},
            {SegmentNames.ParenthesisBegin, new RegexString(@"(", @"(")}, // False positive if followed by "?:", but will be matched on smaller ensembles only so should be ok...
        };

        // Segments that are not identifyable, but used in the logic nonetheless
        public static IDictionary<SegmentNames, RegexString> NotIdentifyableSegments = new Dictionary<SegmentNames, RegexString>()
        {
            {SegmentNames.Literal, new RegexString(@"", @".Literal(") },
            {SegmentNames.Optimized, new RegexString(@"", @"") },
        };
    }

    public class RegexString
    {
        public string Regex { get; set; }
        public string Magex { get; set; }

        public RegexString(string regex, string magex)
        {
            this.Regex = regex;
            this.Magex = magex;
        }
    }
}
