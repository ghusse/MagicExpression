namespace MagicExpression
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Text.RegularExpressions;
	using MagicExpression.Elements;

    public partial class Magex : IRepeatable, IRepeat, ILasiness
	{
		#region Fields

		private List<IExpressionElement> expression;

		#endregion

		#region Ctors

		private Magex()
		{
			this.expression = new List<IExpressionElement>();
		}

		#endregion

		#region Static constructor

		public static IMagex New()
		{
			return new Magex();
		}

		#endregion

		#region Properties

		public string Expression
		{
			get
			{
				StringBuilder s = new StringBuilder();

				foreach (IExpressionElement element in this.expression)
				{
					s.Append(element.Expression);
				}

				return s.ToString();
			}
		}

		public RegexOptions Options { get; set; }

		public Regex Regex
		{
			get
			{
				return new Regex(this.Expression, this.Options);
			}
		}

		public IRepeat Repeat
		{
			get
			{
				return this;
			}
		}

		#endregion

		#region Characters

		public IRepeatable CharacterIn(Characters chars, string otherChars)
		{
			return this.CharacterIn(chars, otherChars.ToCharArray());
		}

		public IRepeatable CharacterIn(Characters chars, params char[] otherChars)
		{
			this.expression.Add(new PossibleChars(chars, otherChars));
			return this;
		}

		public IRepeatable CharacterIn(string otherChars)
		{
			return this.CharacterIn(Characters.None, otherChars.ToCharArray());
		}

		public IRepeatable CharacterIn(params char[] otherChars)
		{
			return this.CharacterIn(Characters.None, otherChars);
		}

		public IRepeatable CharacterNotIn(Characters chars, string otherChars)
		{
			return this.CharacterNotIn(chars, otherChars.ToCharArray());
		}

		public IRepeatable CharacterNotIn(Characters chars, params char[] otherChars)
		{
			this.expression.Add(new ForbiddenChars(chars, otherChars));
			return this;
		}

		public IRepeatable CharacterNotIn(string otherChars)
		{
			return this.CharacterNotIn(Characters.None, otherChars.ToCharArray());
		}

		public IRepeatable CharacterNotIn(params char[] otherChars)
		{
			return this.CharacterNotIn(Characters.None, otherChars);
		}

		public IRepeatable Character()
		{
			this.expression.Add(new Literal("."));
			return this;
		}

		public IRepeatable Character(char theChar)
		{
			this.expression.Add(new StringElement(theChar.ToString()));
			return this;
		}

		public IMagex StartOfLine()
		{
			this.expression.Add(new Literal("^"));
			return this;
		}

		public IMagex EndOfLine()
		{
			this.expression.Add(new Literal("$"));
			return this;
		}

		#endregion

		#region String

		public IMagex String(string val)
		{
			this.expression.Add(new StringElement(val));
			return this;
		}

		#endregion

		#region Repeat

		public ILasiness Any()
		{
			this.expression.Add(new Literal("*"));
			return this;
		}

		public IMagex Once()
		{
			return this;
		}

		public IMagex Times(uint number)
		{
			this.expression.Add(new Literal("{" + number.ToString() + "}"));
			return this;
		}

		public ILasiness AtMostOnce()
		{
			this.expression.Add(new Literal("?"));
			return this;
		}

		public ILasiness Between(uint min, uint max)
		{
			this.expression.Add(new Literal("{" + min.ToString() + "," + max.ToString() + "}"));
			return this;
		}

		public ILasiness AtLeastOnce()
		{
			this.expression.Add(new Literal("+"));
			return this;
		}

		#endregion

		#region Groups

		public IRepeatable Group(IExpressionElement grouped)
		{
			this.expression.Add(new NonCapturingGroup(grouped));
			return this;
		}

		public IRepeatable Capture(IExpressionElement captured)
		{
			this.expression.Add(new CapturingGroup(captured));
			return this;
		}

		public IRepeatable CaptureAs(string name, IExpressionElement captured)
		{
			this.expression.Add(new NamedGroup(name, captured));
			return this;
		}

		public IRepeatable Group(Action<IMagex> expression)
		{
			var magex = Magex.New();
			expression(magex);

			return this.Group(magex);
		}

		public IRepeatable Capture(Action<IMagex> expression)
		{
			var magex = Magex.New();
			expression(magex);

			return this.Capture(magex);
		}

		public IRepeatable CaptureAs(string name, Action<IMagex> expression)
		{
			var magex = Magex.New();
			expression(magex);

			return this.CaptureAs(name, magex);
		}

		#endregion

		#region Alt

		public IRepeatable Alternative(params IExpressionElement[] alternatives)
		{
			this.expression.Add(new Alternative(alternatives));
			return this;
		}

		public IRepeatable Alternative(params Action<IMagex>[] alternatives)
		{
			IExpressionElement[] constructedAlternatives = new IExpressionElement[alternatives.Length];

			for (var i = 0; i < alternatives.Length; i++)
			{
				var possibility = Magex.New();
				alternatives[i](possibility);
				constructedAlternatives[i] = possibility;
			}

			return this.Alternative(constructedAlternatives);
		}

		#endregion

		#region Laziness

		public IMagex Lazy()
		{
			this.expression.Add(new Literal("?"));
			return this;
		}

		#endregion

		#region References

		public IRepeatable BackReference(string name)
		{
			this.expression.Add(new NamedBackReference(name));
			return this;
		}

		public IRepeatable BackReference(uint index)
		{
			this.expression.Add(new IndexedBackReference(index));
			return this;
		}

		#endregion

		#region Literal

		public IMagex Literal(string regex)
		{
			this.expression.Add(new Literal(regex));
			return this;
		}

		#endregion
	}
}
