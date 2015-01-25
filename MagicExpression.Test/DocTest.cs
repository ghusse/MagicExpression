using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagicExpression.Test
{
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
			IMagex magicWand = Magex.New();

            #region
            magicWand.Character('-').Repeat.AtMostOnce()
                     .CharacterIn(Characters.Numeral).Repeat.Any()
                     .Character('.').Repeat.Once()
                     .CharacterIn(Characters.Numeral).Repeat.AtLeastOnce();
            #endregion

			// Creates a regex corresponding to
			// -?[0-9]*\.[0-9]+
			var floatingPointNumberDetector = new Regex(magicWand.Expression);

            Assert.IsTrue(floatingPointNumberDetector.IsMatch("1.234"), "1");
            Assert.IsTrue(floatingPointNumberDetector.IsMatch("-1.234"), "2");
            Assert.IsTrue(floatingPointNumberDetector.IsMatch("0.0"), "3");
            Assert.IsTrue(floatingPointNumberDetector.IsMatch(".01"), "4");

			Assert.IsFalse(floatingPointNumberDetector.IsMatch("0"));
			Assert.IsFalse(floatingPointNumberDetector.IsMatch("1,234"));
			Assert.IsFalse(floatingPointNumberDetector.IsMatch("0x234"));
			Assert.IsFalse(floatingPointNumberDetector.IsMatch("#1a4f66"));
		}

		[TestMethod]
		public void GroupsCapturesAndBackref()
		{
			IMagex magicWand = Magex.New();
			IMagex notSoMagicWand = Magex.New();

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
			IMagex magicWand = Magex.New();

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
			IRepeatable lazyMagicWand = Magex.New().Character('<')
			                                 .Group(x => x.Character().Repeat.Any().Lazy())
			                                 .Character('>');

			var lazyDetector = new Regex(lazyMagicWand.Expression);
			MatchCollection matchCollection = lazyDetector.Matches("<em>something</em>");
			Assert.AreEqual(2, matchCollection.Count);

			// The group will match the larges ensemble possible, e.g. the whole "<em>something</em>"
			IRepeatable greedyMagicWand = Magex.New().Character('<')
			                                   .Group(x => x.Character().Repeat.Any())
			                                   .Character('>');

			var greedyDetector = new Regex(greedyMagicWand.Expression);
			matchCollection = greedyDetector.Matches("<em>something</em>");
			Assert.AreEqual(1, matchCollection.Count);
		}

		[TestMethod]
		public void DocRange()
		{
			IMagex magicWand = Magex.New();

			magicWand.Builder.NumericRange(0, 42);

			var detector = new Regex(magicWand.Expression);

			Assert.IsTrue(detector.IsMatch("0"));
			Assert.IsTrue(detector.IsMatch("9"));
			Assert.IsTrue(detector.IsMatch("20"));
			Assert.IsTrue(detector.IsMatch("42"));
			Assert.IsFalse(detector.IsMatch("43"));
			Assert.IsFalse(detector.IsMatch("52"));
		}

		[TestMethod]
		public void Hexadecimal()
		{
			// 0x34cde4
			IMagex magicWand = Magex.New();

            #region
            magicWand
                .Character('0')
                .CharacterIn("xX")
                .CharacterIn(Characters.Numeral, "abcdefABCDEF")
                .Repeat.Times(6);
            #endregion

            var detector = new Regex(magicWand.Expression);

			Assert.IsTrue(detector.IsMatch("0x123456"));
			Assert.IsTrue(detector.IsMatch("0x1a3b5C"));
			Assert.IsFalse(detector.IsMatch(""));
			Assert.IsFalse(detector.IsMatch("1x1"));
			Assert.IsFalse(detector.IsMatch("1x1a3b5C"));
			Assert.IsFalse(detector.IsMatch("1xxyzxyz"));
			Assert.IsFalse(detector.IsMatch("0x1z3b5c"));
		}

		[TestMethod]
		public void HexColor()
		{
			// #34cde4 or #54f
			IMagex magicWand = Magex.New();

            #region
            magicWand
                .StartOfLine()
                .Character('#')
                .Alternative(
                    Magex.New().CharacterIn(Characters.Numeral, "abcdefABCDEF").Repeat.Times(3),
                    Magex.New().CharacterIn(Characters.Numeral, "abcdefABCDEF").Repeat.Times(6))
                .EndOfLine();
            #endregion

            var detector = new Regex(magicWand.Expression);

			Assert.IsTrue(detector.IsMatch("#123456"));
            Assert.IsTrue(detector.IsMatch("#123"));
            Assert.IsTrue(detector.IsMatch("#1a3b5C"));
			Assert.IsTrue(detector.IsMatch("#1a3"));

			Assert.IsFalse(detector.IsMatch(""), "Empty");
            Assert.IsFalse(detector.IsMatch("#1"), "One char");
            Assert.IsFalse(detector.IsMatch("#1a3b5Z"), "Wrong letters");
            Assert.IsFalse(detector.IsMatch("##1a3b5C"), "Two #s");
            Assert.IsFalse(detector.IsMatch("#12345678"), "Too many chars");
            Assert.IsFalse(detector.IsMatch("#1z3"), "Wrong chars");
		}

		[TestMethod]
		public void IpAddress()
		{
			IMagex magicWand = Magex.New();

            #region
            magicWand
				.Builder.NumericRange(1, 255).Character('.')
				.Builder.NumericRange(0, 255).Character('.')
				.Builder.NumericRange(0, 255).Character('.')
				.Builder.NumericRange(0, 255);
            #endregion

            var detector = new Regex(magicWand.Expression);

			Assert.IsTrue(detector.IsMatch("73.60.124.136"));

			Assert.IsFalse(detector.IsMatch(""));
			Assert.IsFalse(detector.IsMatch("0.60.124.136"));
			Assert.IsFalse(detector.IsMatch("0.60.124.136."));
			Assert.IsFalse(detector.IsMatch(".0.60.124.136"));
			Assert.IsFalse(detector.IsMatch("73-60.124.136"));
			Assert.IsFalse(detector.IsMatch("256.60.124.276"));
			Assert.IsFalse(detector.IsMatch("0000000000001.0000000023.00000414.000022"));
		}

		[TestMethod]
		public void Url()
		{
			IMagex magicWand = Magex.New();

			magicWand.Options = RegexOptions.IgnoreCase;

			const string allowedChars = @"!#$%&'*+/=?^_`{|}~-";

            #region
            magicWand
				.Alternative(
					Magex.New().String("http"),
					Magex.New().String("ftp"))
				.Character('s').Repeat.AtMostOnce()
				.String("://")
				.Group(Magex.New().String("www.")).Repeat.AtMostOnce()
				.CharacterIn(Characters.Alphanumeric, allowedChars);
            #endregion
            
            var detector = new Regex(magicWand.Expression);

			Assert.IsTrue(detector.IsMatch("http://url.com"));
			Assert.IsTrue(detector.IsMatch("https://url.com"));
            Assert.IsTrue(detector.IsMatch("http://www.url.com"));
            Assert.IsTrue(detector.IsMatch("https://www.url.com"));
            //Assert.IsTrue(detector.IsMatch("htTpS://Www.url.com"));
			Assert.IsTrue(detector.IsMatch("ftp://url.com"));
			Assert.IsTrue(detector.IsMatch("ftps://url.com"));

			Assert.IsFalse(detector.IsMatch(""));
			Assert.IsFalse(detector.IsMatch("0.60.124.136"));
		}

		[TestMethod]
		public void TestIPv6()
		{
			// 2001:0db8:85a3:08d3:1319:8a2e:0370:7344

			IMagex magicwand = Magex.New();

            #region
            const string hexChars = "0123456789abcdefABCDEF";

            IMagex hexAndColumn = Magex.New().CharacterIn(hexChars).Repeat.Between(0, 4).Character(':').Repeat.Once();
            
            magicwand
                .Group(hexAndColumn).Repeat.Times(7)
                    .CharacterIn(hexChars).Repeat.Times(4) //Final Block
                    .EndOfLine();
            #endregion

            //1 to 7 blocks with 0-4 hex and a :
            //Alternative(
            // a final hex
            //  OR
            // another : with at most 1-7 blocks

            var detector = new Regex(magicwand.Expression);

			Assert.IsTrue(detector.IsMatch("2001:0db8:85a3:08d3:1319:8a2e:0370:7344"), "Standard case");
			Assert.IsTrue(detector.IsMatch("2001:db8:85a3:8d3:1319:8a2e:370:7344"), "Leading zeroes can be ommited");
			
            Assert.IsTrue(detector.IsMatch("2001:2002:0000:0000:0000:0000:0000:2008"), "Or can be kept...");
            //Assert.IsTrue(detector.IsMatch("2001:2002::2008"), "...and be compressed...");
            //Assert.IsFalse(detector.IsMatch("2001::0000:0000:0000::0000:2008"), "... but only once per adress");

            Assert.IsFalse(detector.IsMatch("20r1:0db8:85m3:08d3:1319:8k2e:0370:7l44"), "Illegal characters");
            Assert.IsFalse(detector.IsMatch("2001:0db8:helloworld:0370:7l44"), "Too long block + illegal characters");
            Assert.IsFalse(detector.IsMatch("2001:0db8"), "Too few blocks");
		}
	}
}