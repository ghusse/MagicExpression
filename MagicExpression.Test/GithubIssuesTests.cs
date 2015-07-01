using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicExpression.Test
{
    [TestClass]
    public class GithubIssuesTests
    {
        /// <summary>
        /// Test showing the use of Character(Characters.Numerals)
        /// </summary>
        [TestMethod]
        public void TestIssue2_CharacterEnumWithoutCharacterIn()
        {
            var ipAdress = Magex.New().Alternative(
                Magex.New()
                          .Character('2')
                          .CharacterIn('0', '-', '4')
                          .Character(Characters.Numeral),
                Magex.New()
                          .String("25")
                          .CharacterIn('0', '-', '5'),
                Magex.New()
                          .CharacterIn('0', '1').Repeat.AtMostOnce()
                          .Character(Characters.Numeral)
                          .Character(Characters.Numeral).Repeat.AtMostOnce());

            var expression = Magex.New().CaptureAs("First", ipAdress)
                .Character('.')
                .CaptureAs("Second", ipAdress)
                .Character('.')
                .CaptureAs("Third", ipAdress)
                .Character('.')
                .CaptureAs("Fourth", ipAdress);

            Assert.AreEqual(
                @"(?<First>(?:2[0-4]\\d|25[0-5]|[01]?\\d\\d?))\.(?<Second>(?:2[0-4]\\d|25[0-5]|[01]?\\d\\d?))\.(?<Third>(?:2[0-4]\\d|25[0-5]|[01]?\\d\\d?))\.(?<Fourth>(?:2[0-4]\\d|25[0-5]|[01]?\\d\\d?))"
                , expression.Expression, expression.Expression);
        }

    }
}
