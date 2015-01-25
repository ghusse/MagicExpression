using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicExpression
{
    public static class RegexParts
    {
        public static string CharactersAlphanumeric   = "CharactersAlphanumeric"              ;
        public static string CharactersNonAlphanumeric= "CharactersNonAlphanumeric"           ;
        public static string CharactersNumeral        = "CharactersNumeral"                   ;
        public static string CharactersNonNumeral     = "CharactersNonNumeral"                ;
        public static string CharactersWhiteSpaces = "CharactersWhiteSpace";   
        public static string CharactersNonWhiteSpace  = "CharactersNonWhiteSpace"             ;
        public static string CharactersCarriageReturn = "CharactersCarriageReturn"            ;
        public static string CharactersNewLine        = "CharactersNewLine"                   ;
        public static string CharactersFormFeed       = "CharactersFormFeed"                  ;
        public static string CharactersTab            = "CharactersTab"                       ;
        public static string CharactersVerticalTab    = "CharactersVerticalTab"               ;
        public static string CharactersBell           = "CharactersBell"                      ;
        public static string CharactersBackSpace    = "CharactersBackSpace"                 ;
        public static string CharactersEscape         = "CharactersEscape"                    ;
        public static string CharactersLetter         = "CharactersLetter"                    ;
        public static string CharactersUpperCaseLetter = "CharactersUpperCaseLetter"           ;
        public static string CharactersLowerCaseLetter = "CharactersLowerCaseLetter"           ;


        public static string AlternativeBegin = "AlternativeBegin" ;
        public static string AlternativeSeparator = "AlternativeSeparator";
        public static string AlternativeEnd = "AlternativeEnd";
        
        public static string GroupBegin = "GroupBegin";
        public static string GroupEnd = "GroupEnd";

        public static IDictionary<string, string> Segments = new Dictionary<string, string>()
        {
            {CharactersAlphanumeric, @"\w"},
            {CharactersNonAlphanumeric, @"\W"},
            {CharactersNumeral, @"\d"},
            {CharactersNonNumeral, @"\D"},
            {CharactersWhiteSpaces, @"\s"},
            {CharactersNonWhiteSpace, @"\S"},
            {CharactersCarriageReturn, @"\r"},
            {CharactersNewLine, @"\n"},
            {CharactersFormFeed, @"\f"},
            {CharactersTab, @"\t"},
            {CharactersVerticalTab, @"\v"},
            {CharactersBell, @"\a"},
            {CharactersBackSpace, @"\b"},
            {CharactersEscape, @"\e"},
            {CharactersLetter, @"a-zA-Z"},
            {CharactersUpperCaseLetter, @"A-Z"},
            {CharactersLowerCaseLetter, @"a-z"},
        
            {AlternativeBegin, @"(?:"},
            {AlternativeSeparator, @"|"},
            {AlternativeEnd, @")"},
             
            {GroupBegin, @"("},
            {GroupEnd, @")"},
        };
    }
}
