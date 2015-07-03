using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagicExpression.Test
{
    [TestClass]
    public class MagexBuilderTest : MagicTest
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
        }

        [TestMethod]
        public void Range()
        {
            this.Magic.Range(0, 42);

            this.AssertIsMatching("0", "9", "20", "42");
            this.AssertIsNotMatching("43", "52");
        }

        [TestMethod]
        public void RangeInBetween()
        {
            this.Magic.Character('a').Range(0, 42).Character('a');

            this.AssertIsMatching("a0a", "a9a", "a20a", "a42a");
            this.AssertIsNotMatching("", "%", "a9b", "b52a", "4242");
        }

        [TestMethod]
        public void RangeCharBounds()
        {
            this.Magic.Range('a', 'g').Character('a');

            this.AssertIsMatching("aa", "da");
            this.AssertIsNotMatching("a", "5", string.Empty, "$");
        }

        [TestMethod]
        [Ignore] //Not working yet
        public void RangeMixedBounds()
        {
            this.Magic.Range(0, 4, 'a', 'g');

            this.AssertIsMatching("0", "3", "a", "d");
            this.AssertIsNotMatching("", "5", "k", string.Empty, "$");
        }
    }
}
