using MagicExpression.ReverseEngineering;
using System.Collections.Generic;

namespace MagicExpression
{
    public static class RegexMagexLexicon
    {
        private static bool AS_REGEX = true;

        public static List<RegMag> KnownRegexElements()
        {
            return new List<RegMag>()
            {
                new RegMag(@"(?:", @".Capture("),
                new RegMag(@"|", @","),
                new RegMag(@"[^", @".CharacterNotIn("),

                new RegMag(@"k<", @".BackReference("),
                new RegMag(@">", @")"),

                new RegMag(@"?", @".Repeat.AtMostOnce()"),
                new RegMag(@"*", @".Repeat.Any()"),
                new RegMag(@"+", @".Repeat.AtLeastOnce()"),

                new RegMag(@"{\n(,\n)?}", @".Repeat.Times(", AS_REGEX),

                new RegMag(@"\", @""), //Escaping character, swallow it

                new RegMag(@")", @")"),
                new RegMag(@"(", @"("),

                new RegMag(@"\[", @".CharacterIn(", AS_REGEX),
                new RegMag(@"\]", @")", AS_REGEX),


                new RegMag(@"(\d-\d)+",@"Characters.Numeral", AS_REGEX),
                new RegMag(@"a-zA-Z",@"Characters.Letter"),
                new RegMag(@"A-Z", @"Characters.UpperCaseLetter"),
                new RegMag(@"a-z", @"Characters.LowerCaseLetter"),

                new RegMag(@"\s", @"Characters.WhiteSpace"),
                
                new RegMag(@"\r", @"Characters.CarriageReturn"),
                new RegMag(@"\n", @"Characters.NewLine"),
                new RegMag(@"\f", @"Characters.FormFeed"),
                new RegMag(@"\t", @"Characters.Tab"),
                new RegMag(@"\v", @"Characters.VerticalTab"),
                new RegMag(@"\a", @"Characters.Bell"),
                new RegMag(@"\b", @"Characters.BackSpace"),
                new RegMag(@"\e", @"Characters.Escape"),                

                new RegMag(@"\D", @"Characters.NonNumeral"),
                new RegMag(@"\d", @"Characters.Numeral"),
                new RegMag(@"\W", @"Characters.NonAlphanumeric"),
                new RegMag(@"\w", @"Characters.Alphanumeric"),

                new RegMag(@"\S", @"Characters.NonWhiteSpace"),
            };
        }
    }
}
