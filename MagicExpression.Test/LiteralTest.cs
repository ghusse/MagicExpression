namespace MagicExpression.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class LiteralTest : MagicTest
	{
		[TestInitialize]
		public override void Setup()
		{
			base.Setup();
		}

		[TestMethod]
		public void Literal()
		{
			this.Magic.Literal(@"[A-Z][a-z]*[\s][a-z]*\.");

			this.AssertIsMatching("Hello world.");
			this.AssertIsNotMatching("hello world.");
		}
	}
}
