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
            this.Magic.Literal(Magex.NumericRange(0, 42));

            this.AssertIsMatching("0");
            this.AssertIsMatching("9");
            this.AssertIsMatching("20");
            this.AssertIsMatching("42");
            this.AssertIsNotMatching("43");
            this.AssertIsNotMatching("52");
        }
    }
}
