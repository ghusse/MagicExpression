using MagicExpression.ReverseEngineering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace MagicExpressionReverse.Test
{
    [TestClass]
    public class ExtensionTest
    {
        [TestMethod]
        public void ExtensionTest_NotNull()
        {
            string expression = "";
            var list = expression.ParseMagex();
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void ExtensionTest_SimpleCharacter()
        {
            string expression = "-";
            var list = expression.ParseMagex();
            var flattenedList = list.Flatten();
            Assert.AreEqual(".Character('-')", flattenedList);
        }

        [TestMethod]
        public void ExtensionTest_SimpleEnsemble()
        {
            string expression = @"\d";
            var list = expression.ParseMagex();
            var flattenedList = list.Flatten();
            Assert.AreEqual(".CharacterIn(Characters.Numeral)", flattenedList);
        }

        [TestMethod]
        public void ExtensionTest_SimpleEnsembles()
        {
            string expression = @"\d\s";
            var list = expression.ParseMagex();
            var flattenedList = list.Flatten();
            Assert.AreEqual(".CharacterIn(Characters.Numeral).CharacterIn(Characters.WhiteSpace)", flattenedList);
        }

        #region DOC_Tests

        [TestMethod]
        public void ExtensionTest_FloatingNumber()
        {
            string expression = @"-?[0-9]*\.[0-9]+";
            var list = expression.ParseMagex();
            var flattenedList = list.Flatten();
            Assert.AreEqual(".Character('-').Repeat.AtMostOnce().CharacterIn(Characters.Numeral).Repeat.Any()"
                            +".Character('.').CharacterIn(Characters.Numeral).Repeat.AtLeastOnce()", flattenedList);
        }

        [TestMethod]
        [Ignore]
        public void ExtensionTest_GroupsCapturesAndBackref()
        {
            string expression = @"<(?<tag>[^>]+)>.*?</\\k<tag>>";
            var list = expression.ParseMagex();
            var flattenedList = list.Flatten();
            Assert.AreEqual(".Character('<').CaptureAs(\"tag\", x => x.CharacterNotIn('>').Repeat.AtLeastOnce()).Character('>').Character().Repeat.Any().Lazy().String(\"</\").BackReference(\"tag\").Character('>').Character('.').CharacterIn(Characters.Numeral).Repeat.AtLeastOnce()", flattenedList);
        }

        [TestMethod]
        [Ignore]
        public void ExtensionTest_Alternatives()
        {
            string expression = @"(?:a|b)";
            var list = expression.ParseMagex();
            var flattenedList = list.Flatten();
            Assert.AreEqual(".Alternative(x => x.Character('a'), x => x.Character('b'))"
                , flattenedList);
        }

        [TestMethod]
        [Ignore]
        public void ExtensionTest_Lazy()
        {
            string expression = @"<(?:.*)>";
            var list = expression.ParseMagex();
            var flattenedList = list.Flatten();
            Assert.AreEqual(".Character('<').Group(x => x.Character().Repeat.Any().Lazy()).Character('>')"
                , flattenedList);
        }

        [Ignore]
        [TestMethod]
        public void ExtensionTest_Range()
        {
            string expression = @"(?<![0-9])(?:[0-9]|[1-3][0-9]|4[0-2])(?![0-9])";
            var list = expression.ParseMagex();
            var flattenedList = list.Flatten();
            Assert.AreEqual(".Builder.NumericRange(0, 42)"
                , flattenedList);
        }

        [TestMethod]
        public void ExtensionTest_Hexa()
        {
            string expression = "0[xX][\\dab]";
            var list = expression.ParseMagex();
            var flattenedList = list.Flatten();
            Assert.AreEqual(".Character('0').CharacterIn(\"xX\").CharacterIn(Characters.Numeral, \"ab\")"
                , flattenedList);
        }
        #endregion

        /*
        (?<![0-9])(?:[0-9]|[1-3][0-9]|4[0-2])(?![0-9])
        0[xX][\\dabcdefABCDEF]{6}
        ^#(?:[\\dabcdefABCDEF]{3}|[\\dabcdefABCDEF]{6})$
        (?<![0-9])(?:[1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(?![0-9])\\.(?<![0-9])(?:[0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(?![0-9])\\.(?<![0-9])(?:[0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(?![0-9])\\.(?<![0-9])(?:[0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(?![0-9])
        */
    }
}
