namespace MagicExpression.Test
{
    using System.Text.RegularExpressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DocTest : MagicTest
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }

        [TestMethod]
        public void FloatingPoint()
        {
            var magicWand = Magex.New();

            magicWand.Character('-').Repeat.AtMostOnce()
                     .CharacterIn(Characters.Numeral).Repeat.Any()
                     .Character('.')
                     .CharacterIn(Characters.Numeral).Repeat.AtLeastOnce();

            // Creates a regex corresponding to
            // -?[0-9]*\.[0-9]+
            var floatingPointNumberDetector = new Regex(magicWand.Expression);

            Assert.IsTrue(floatingPointNumberDetector.IsMatch("1.234"));
            Assert.IsTrue(floatingPointNumberDetector.IsMatch("-1.234"));
            Assert.IsTrue(floatingPointNumberDetector.IsMatch("0.0"));

            Assert.IsFalse(floatingPointNumberDetector.IsMatch("0"));
            Assert.IsFalse(floatingPointNumberDetector.IsMatch("1,234"));
            Assert.IsFalse(floatingPointNumberDetector.IsMatch("0x234"));
            Assert.IsFalse(floatingPointNumberDetector.IsMatch("#1a4f66"));
        }

        [TestMethod]
        public void GroupsCapturesAndBackref()
        {
            var magicWand = Magex.New();
            var notSoMagicWand = Magex.New();

            // You can use sub expressions, created before
            // or create them in place
            magicWand.Group(notSoMagicWand.Character('a'));
            magicWand.Group(Magex.New().Character('a'));

            // Or use lambdas
            magicWand.Group(x => x.Character('a'));

            magicWand = Magex.New();
            // Reference a previous capture
            // Will match something like <strong>hello world</strong> (but please don't parse HTML with Magex in real life)
            magicWand.Character('<')
                     .CaptureAs("tag", x => x.CharacterNotIn('>').Repeat.AtLeastOnce())
                     .Character('>')
                     .Character().Repeat.Any().Lazy()
                     .String("</")
                     .BackReference("tag")
                     .Character('>');

            var badHtmlTagDetector = new Regex(magicWand.Expression);

            Assert.IsTrue(badHtmlTagDetector.IsMatch("<strong>hello world</strong>"));
            Assert.IsTrue(badHtmlTagDetector.IsMatch("<h1>A title</h1>"));

            Assert.IsFalse(badHtmlTagDetector.IsMatch("<h1>A tag mismatch</strong>"));
        }

        [TestMethod]
        public void Alternatives()
        {
            var magicWand = Magex.New();

            // With subexpressions
            magicWand.Alternative(
                Magex.New().Character('a'),
                Magex.New().Character('b')
                );

            magicWand = Magex.New();
            // With lambdas
            magicWand.Alternative(
                x => x.Character('a'),
                x => x.Character('b')
                );

            var alternativeDetector = new Regex(magicWand.Expression);

            Assert.IsTrue(alternativeDetector.IsMatch("a"));
            Assert.IsTrue(alternativeDetector.IsMatch("b"));

            Assert.IsFalse(alternativeDetector.IsMatch("c"));
        }

        [TestMethod]
        public void Lazy()
        {
            // The group will match the smallest ensemble possible, e.g. "<em>" and "</em>"
            var lazyMagicWand = Magex.New().Character('<')
                     .Group(x => x.Character().Repeat.Any().Lazy())
                     .Character('>');

            var lazyDetector = new Regex(lazyMagicWand.Expression);
            var matchCollection = lazyDetector.Matches("<em>something</em>");
            Assert.AreEqual(2, matchCollection.Count);

            // The group will match the larges ensemble possible, e.g. the whole "<em>something</em>"
            var greedyMagicWand = Magex.New().Character('<')
                     .Group(x => x.Character().Repeat.Any())
                     .Character('>');

            var greedyDetector = new Regex(greedyMagicWand.Expression);
            matchCollection = greedyDetector.Matches("<em>something</em>");
            Assert.AreEqual(1, matchCollection.Count);
        }

        [TestMethod]
        public void DocRange()
        {
					var magicWand = Magex.New();

          magicWand.Builder.NumericRange(0, 42);

          var detector = new Regex(magicWand.Expression);

          Assert.IsTrue(detector.IsMatch("0"));
          Assert.IsTrue(detector.IsMatch("9"));
          Assert.IsTrue(detector.IsMatch("20"));
          Assert.IsTrue(detector.IsMatch("42"));
          Assert.IsFalse(detector.IsMatch("43"));
          Assert.IsFalse(detector.IsMatch("52"));   
        }
    }
}