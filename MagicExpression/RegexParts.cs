using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicExpression
{
    public enum SegmentNames
    {
        EscapingBackslash = 0,

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
    }

    public static class RegexParts
    {
        public static IDictionary<SegmentNames, RegexString> FormallydentifyableSegments = new Dictionary<SegmentNames, RegexString>()
        {
            {SegmentNames.CharactersAlphanumeric, new RegexString(@"\w",  @".Characters(Characters.Alphanumeric)")},
            {SegmentNames.CharactersNonAlphanumeric, new RegexString(@"\W", @".Characters(Characters.NonAlphanumeric)")},
            {SegmentNames.CharactersNumeral, new RegexString(@"\d", @".Characters(Characters.Numeral)")},
            {SegmentNames.CharactersNonNumeral, new RegexString(@"\D", @".Characters(Characters.NonNumeral)")},
            {SegmentNames.CharactersWhiteSpaces, new RegexString(@"\s", @".Characters(Characters.WhiteSpace)")},
            {SegmentNames.CharactersNonWhiteSpace, new RegexString(@"\S", @".Characters(Characters.NonWhiteSpace)")},
            {SegmentNames.CharactersCarriageReturn, new RegexString(@"\r", @".Characters(Characters.CarriageReturn)")},
            {SegmentNames.CharactersNewLine, new RegexString(@"\n", @".Characters(Characters.NewLine)")},
            {SegmentNames.CharactersFormFeed, new RegexString(@"\f", @".Characters(Characters.FormFeed)")},
            {SegmentNames.CharactersTab, new RegexString(@"\t", @".Characters(Characters.Tab)")},
            {SegmentNames.CharactersVerticalTab, new RegexString(@"\v", @".Characters(Characters.VerticalTab)")},
            {SegmentNames.CharactersBell, new RegexString(@"\a", @".Characters(Characters.Bell)")},
            {SegmentNames.CharactersBackSpace, new RegexString(@"\b", @".Characters(Characters.BackSpace)")},
            {SegmentNames.CharactersEscape, new RegexString(@"\e", @".Characters(Characters.Escape)")},
            {SegmentNames.CharactersLetter, new RegexString(@"a-zA-Z", @".Characters(Characters.Letter)")},
            {SegmentNames.CharactersUpperCaseLetter, new RegexString(@"A-Z", @".Characters(Characters.UpperCaseLetter)")},
            {SegmentNames.CharactersLowerCaseLetter, new RegexString(@"a-z", @".Characters(Characters.LowerCaseLetter)")},
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
