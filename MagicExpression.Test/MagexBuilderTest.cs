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
			this.Magic.Builder.NumericRange(0, 42);

			this.AssertIsMatching("0", "9", "20", "42");
			this.AssertIsNotMatching("43", "52");
    }

		[TestMethod]
		public void RangeInBetween()
		{
			this.Magic.Character('a').Builder.NumericRange(0, 42).Character('a');

			this.AssertIsMatching("a0a", "a9a", "a20a", "a42a");
			this.AssertIsNotMatching("", "%", "a9b", "b52a", "4242");
		}

		[TestMethod]
		public void RangeWithLeadingZeros()
		{
            this.Magic.Builder.NumericRange(0, 42, RangeOptions.AllowLeadingZeroes);

			this.AssertIsMatching("001");
			this.AssertIsNotMatching("0001000");
		}

		[TestMethod]
		public void RangeInBetweenWithLeadingZeros()
		{
			this.Magic.Character('a').Builder.NumericRange(0, 42, RangeOptions.AllowLeadingZeroes).Character('a');

			this.AssertIsMatching("a001a");
			this.AssertIsNotMatching("a0001000a");
		}
  }
}
