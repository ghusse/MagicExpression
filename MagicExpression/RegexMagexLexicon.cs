using MagicExpression.ReverseEngineering;
using System.Collections.Generic;

namespace MagicExpression
{
    public static class RegexMagexLexicon
    {
        public static List<RegMag> GetLexicon()
        {
            return new List<RegMag>()
            {
                new RegMag(@"\w", @".CharacterIn(Characters.Alphanumeric)"),
                new RegMag(@"\W", @".CharacterIn(Characters.NonAlphanumeric)"),
                new RegMag(@"\d", @".CharacterIn(Characters.Numeral)"),
                new RegMag(@"[0-9]", @".CharacterIn(Characters.Numeral)"),
                new RegMag(@"\D", @".CharacterIn(Characters.NonNumeral)"),
                new RegMag(@"\s", @".CharacterIn(Characters.WhiteSpace)"),
                new RegMag(@"\S", @".CharacterIn(Characters.NonWhiteSpace)"),
                new RegMag(@"\r", @".CharacterIn(Characters.CarriageReturn)"),
                new RegMag(@"\n", @".CharacterIn(Characters.NewLine)"),
                new RegMag(@"\f", @".CharacterIn(Characters.FormFeed)"),
                new RegMag(@"\t", @".CharacterIn(Characters.Tab)"),
                new RegMag(@"\v", @".CharacterIn(Characters.VerticalTab)"),
                new RegMag(@"\a", @".CharacterIn(Characters.Bell)"),
                new RegMag(@"\b", @".CharacterIn(Characters.BackSpace)"),
                new RegMag(@"\e", @".CharacterIn(Characters.Escape)"),

                new RegMag(@"[a-zA-Z]", @".CharacterIn(Characters.Letter)"),
                new RegMag(@"[A-Z]", @".CharacterIn(Characters.UpperCaseLetter)"),
                new RegMag(@"[a-z]", @".CharacterIn(Characters.LowerCaseLetter)"),
                new RegMag(@"(?:", @".Capture("),
                new RegMag(@"|", @","),
                new RegMag(@"[^", @".CharacterNotIn("),
                new RegMag(@"]", @")"),
                new RegMag(@"\k<", @".BackReference("),
                new RegMag(@">", @")"),

                new RegMag(@"?", @".Repeat.AtMostOnce()"),
                new RegMag(@"*", @".Repeat.Any()"),
                new RegMag(@"+", @".Repeat.AtLeastOnce()"),

                new RegMag(@"\", @""), //Swallow it

                new RegMag(@")", @")"),
                new RegMag(@"(", @"("),
            };
        }
    }
}
