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

		[Ignore]	//TODO: currently doesn't work because of the \bEXP\b added by the range builder
							//TODO: also doesn't allow leading zeros
							//TODO: also shouldn't allow leading/trailing numbers
		[TestMethod]
		public void RangeBetweenTest()
		{
			this.Magic.Character('a').Builder.NumericRange(0, 42).Character('a');

			this.AssertIsMatching("a0a", "a9a", "a20a", "a42a");
			this.AssertIsNotMatching("", "%", "a9b", "b52a");
		}
  }
}
