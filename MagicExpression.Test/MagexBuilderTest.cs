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
  }
}
