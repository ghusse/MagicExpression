using MagicExpression.ReverseEngineering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Assert.AreEqual("-", (list[0] as Segment).Regex);
            Assert.AreEqual(".Character('-')", (list[0] as Segment).Magex);
        }

        [TestMethod]
        public void ExtensionTest_SimpleEnsemble()
        {
            string expression = @"\d";
            var list = expression.ParseMagex();
            Assert.AreEqual(@"\d", (list[0] as Segment).Regex);
            Assert.AreEqual(".CharacterIn(Characters.Numeral)", (list[0] as Segment).Magex);
        }

        [TestMethod]
        public void ExtensionTest_SimpleEnsembles()
        {
            string expression = @"\d\s";
            var list = expression.ParseMagex();
            Assert.AreEqual(@"\d", (list[0] as Segment).Regex);
            Assert.AreEqual(@"\s", (list[1] as Segment).Regex);
            Assert.AreEqual(".CharacterIn(Characters.Numeral)", (list[0] as Segment).Magex);
            Assert.AreEqual(".CharacterIn(Characters.WhiteSpace)", (list[1] as Segment).Magex);
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



        #endregion

        /*
        -?[0-9]*\.[0-9]+
        <(?<tag>[^>]+)>.*?</\\k<tag>>
        (?:a|b)
        <(?:.*)>
        (?<![0-9])(?:[0-9]|[1-3][0-9]|4[0-2])(?![0-9])
        0[xX][\\dabcdefABCDEF]{6}
        ^#(?:[\\dabcdefABCDEF]{3}|[\\dabcdefABCDEF]{6})$
        (?<![0-9])(?:[1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(?![0-9])\\.(?<![0-9])(?:[0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(?![0-9])\\.(?<![0-9])(?:[0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(?![0-9])\\.(?<![0-9])(?:[0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(?![0-9])
        */
    }
}
