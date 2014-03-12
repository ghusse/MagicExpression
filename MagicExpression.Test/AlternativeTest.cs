namespace MagicExpression.Test
{
	using System;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class AlternativeTest : MagicTest
	{
		[TestInitialize]
		public override void Setup()
		{
			base.Setup();
		}

		[TestMethod]
		public void AltWithCtorTest()
		{
			this.Magic
			.Alternative(
				Magex.New().String("hello"),
				Magex.New().String("world"));

			this.AssertIsMatching("hello", "world");
			this.AssertIsNotMatching("hellword");
		}

		[TestMethod]
		public void AltWithLambdaTest()
		{
			this.Magic
				.Alternative(x => x.String("hello"), y => y.String("world"));

			this.AssertIsMatching("hello", "world");
			this.AssertIsNotMatching("hellword");
		}

		[TestMethod]
		public void RealStuffTest()
		{
			this.Magic
				.Alternative(
					Magex.New().String("pizza ")
						.Alternative(
							Magex.New().String("beef"),
							Magex.New().String("chorizo")
						),
					Magex.New().String("burger ")
						.Alternative(
						Magex.New().String("fries"),
						Magex.New().String("potatoes")));

			this.AssertIsMatching("pizza beef", "burger potatoes");
			this.AssertIsNotMatching("pizza fries", "burger chorizo");
		}

		[TestMethod]
		public void AltAndRepeatTest()
		{
			this.Magic
				.Alternative(
					Magex.New().String("pizza"),
					Magex.New().String("burger")
				).Repeat.Times(2);

			this.AssertIsMatching("pizzapizza", "pizzaburger", "burgerpizza");
			this.AssertIsNotMatching("pizzaa", "burgerr", "pizza", "burger");
		}
	}
}
