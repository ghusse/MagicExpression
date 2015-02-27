namespace MagicExpression
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using MagicExpression.Elements;
    using MagicExpression.ReverseEngineering;

    public class Magex : IRepeatable, IRepeat, ILasiness, IBuilder
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

		/// <summary>
		/// Creates a new instance of Magic Expression
		/// </summary>
		/// <returns>New instance of IMagex</returns>
		public static IMagex New()
		{
			return new Magex();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the regular expression string. Can be used to create a regular expression
		/// from the Magex.
		/// </summary>
		/// <value>
		/// The regular expression string.
		/// </value>
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

		/// <summary>
		/// Gets or sets the options.
		/// </summary>
		/// <value>
		/// The regular expression options.
		/// </value>
		public RegexOptions Options { get; set; }

        /// <summary>
        /// Gets the regular expression constructed with this instance of Magic Expression
        /// </summary>
        /// <value>
        /// The Regex object
        /// </value>
        public Regex Regex
		{
			get
			{
				return new Regex(this.Expression, this.Options);
			}
		}

		/// <summary>
		/// Gets the repetition possibilities on the last element
		/// added to the Magic expression.
		/// </summary>
		/// <value>
		/// Repetitions on the last element
		/// </value>
		public IRepeat Repeat
		{
			get
			{
				return this;
			}
		}

		public IBuilder Builder
		{
			get
			{
				return this;
			}
		}

		#endregion

		#region Characters

		/// <summary>
		/// Adds a character within a list of allowed chars
		/// </summary>
		/// <param name="chars">The allowed special chars.</param>
		/// <param name="otherChars">The allowed chars (escaped, if necessary).</param>
		/// <returns>This, as a repeatable object.</returns>
		public IRepeatable CharacterIn(Characters chars, string otherChars)
		{
			return this.CharacterIn(chars, otherChars.ToCharArray());
		}

		/// <summary>
		/// Adds a character within a list of allowed chars
		/// </summary>
		/// <param name="chars">The allowed special chars.</param>
		/// <param name="otherChars">The allowed chars (escaped, if necessary).</param>
		/// <returns>This, as a repeatable object.</returns>
		public IRepeatable CharacterIn(Characters chars, params char[] otherChars)
		{
			this.expression.Add(new PossibleChars(chars, otherChars));
			return this;
		}

		/// <summary>
		/// Adds a character within a list of allowed chars
		/// </summary>
		/// <param name="otherChars">The allowed chars (escaped, if necessary).</param>
		/// <returns>This, as a repeatable object.</returns>
		public IRepeatable CharacterIn(string otherChars)
		{
			return this.CharacterIn(Characters.None, otherChars.ToCharArray());
		}

		/// <summary>
		/// Adds a character within a list of allowed chars
		/// </summary>
		/// <param name="otherChars">The allowed chars (escaped, if necessary).</param>
		/// <returns>This, as a repeatable object.</returns>
		public IRepeatable CharacterIn(params char[] otherChars)
		{
			return this.CharacterIn(Characters.None, otherChars);
		}

		/// <summary>
		/// Adds a character not belonging to a list of forbidden chars
		/// </summary>
		/// <param name="chars">The forbidden special chars</param>
		/// <param name="otherChars">The forbidden chars, escaped if necessary.</param>
		/// <returns>This, as a repeatable object.</returns>
		public IRepeatable CharacterNotIn(Characters chars, string otherChars)
		{
			return this.CharacterNotIn(chars, otherChars.ToCharArray());
		}

		/// <summary>
		/// Adds a character not belonging to a list of forbidden chars
		/// </summary>
		/// <param name="chars">The forbidden special chars</param>
		/// <param name="otherChars">The forbidden chars, escaped if necessary.</param>
		/// <returns>This, as a repeatable object.</returns>
		public IRepeatable CharacterNotIn(Characters chars, params char[] otherChars)
		{
			this.expression.Add(new ForbiddenChars(chars, otherChars));
			return this;
		}

		/// <summary>
		/// Adds a character not belonging to a list of forbidden chars
		/// </summary>
		/// <param name="otherChars">The forbidden chars, escaped if necessary.</param>
		/// <returns>This, as a repeatable object.</returns>
		public IRepeatable CharacterNotIn(string otherChars)
		{
			return this.CharacterNotIn(Characters.None, otherChars.ToCharArray());
		}

		/// <summary>
		/// Adds a character not belonging to a list of forbidden chars
		/// </summary>
		/// <param name="otherChars">The forbidden chars, escaped if necessary.</param>
		/// <returns>This, as a repeatable object.</returns>
		public IRepeatable CharacterNotIn(params char[] otherChars)
		{
			return this.CharacterNotIn(Characters.None, otherChars);
		}

		/// <summary>
		/// Adds a character, matching any character.
		/// </summary>
		/// <returns>This, as a repeatable object.</returns>
		public IRepeatable Character()
		{
			this.expression.Add(new Literal("."));
			return this;
		}

		/// <summary>
		/// Adds a specific character
		/// </summary>
		/// <param name="theChar">The character, escaped if necessary.</param>
		/// <returns>This, as a repeatable object.</returns>
		public IRepeatable Character(char theChar)
		{
			this.expression.Add(new StringElement(theChar.ToString()));
			return this;
		}

		/// <summary>
		/// Adds a line start/chain start (depending on options)
		/// </summary>
		/// <returns>This</returns>
		public IMagex StartOfLine()
		{
			this.expression.Add(new Literal("^"));
			return this;
		}

		/// <summary>
		/// Adds an end of line/end of string (depending on options)
		/// </summary>
		/// <returns>this</returns>
		public IMagex EndOfLine()
		{
			this.expression.Add(new Literal("$"));
			return this;
		}

		#endregion

		#region String

		/// <summary>
		/// Adds a specific string
		/// </summary>
		/// <param name="val">The string to be added, escaped if necessary.</param>
		/// <remarks>Cannot be repeated as is: you need to create a group. If you want to repeat the last char, add a character and apply repetition on it.</remarks>
		/// <returns>This</returns>
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

		/// <summary>
		/// Creates a non capturing group
		/// </summary>
		/// <param name="grouped">The grouped element.</param>
		/// <returns>this as a repeatable object.</returns>
		public IRepeatable Group(IExpressionElement grouped)
		{
			this.expression.Add(new NonCapturingGroup(grouped));
			return this;
		}

		/// <summary>
		/// Creates an indexed capture
		/// </summary>
		/// <param name="captured">The captured element.</param>
		/// <returns>this as a repeatable object.</returns>
		public IRepeatable Capture(IExpressionElement captured)
		{
			this.expression.Add(new CapturingGroup(captured));
			return this;
		}

		/// <summary>
		/// Creates a named capture
		/// </summary>
		/// <param name="name">The capture name.</param>
		/// <param name="captured">The captured element.</param>
		/// <returns>this as a repeatable object.</returns>
		public IRepeatable CaptureAs(string name, IExpressionElement captured)
		{
			this.expression.Add(new NamedGroup(name, captured));
			return this;
		}

		/// <summary>
		/// Creates a non capturing group
		/// </summary>
		/// <param name="expression">The magex creation lambda.</param>
		/// <returns>this as a repeatable object.</returns>
		public IRepeatable Group(Action<IMagex> expression)
		{
			var magex = Magex.New();
			expression(magex);

			return this.Group(magex);
		}

		/// <summary>
		/// Creates an indexed capture
		/// </summary>
		/// <param name="expression">The magex creation lambda.</param>
		/// <returns>this as a repeatable object.</returns>
		public IRepeatable Capture(Action<IMagex> expression)
		{
			var magex = Magex.New();
			expression(magex);

			return this.Capture(magex);
		}

		/// <summary>
		/// Creates a named capture
		/// </summary>
		/// <param name="name">The capture name.</param>
		/// <param name="expression">The magex creation lambda.</param>
		/// <returns>this as a repeatable object.</returns>
		public IRepeatable CaptureAs(string name, Action<IMagex> expression)
		{
			var magex = Magex.New();
			expression(magex);

			return this.CaptureAs(name, magex);
		}

		#endregion

		#region Alt

		/// <summary>
		/// Creates an alternative element
		/// </summary>
		/// <param name="alternatives">The alternatives.</param>
		/// <returns>This, as a repeatable object</returns>
		public IRepeatable Alternative(params IExpressionElement[] alternatives)
		{
			this.expression.Add(new Alternative(alternatives));
			return this;
		}

		/// <summary>
		/// Creates an alternative element
		/// </summary>
		/// <param name="alternatives">The alternatives, as lambda expressions.</param>
		/// <returns>This, as a repeatable object</returns>
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

		/// <summary>
		/// Specifies that the last operator must be interpreted with laziness.
		/// </summary>
		/// <returns>this</returns>
		public IMagex Lazy()
		{
			this.expression.Add(new Literal("?"));
			return this;
		}

		#endregion

		#region References

		/// <summary>
		/// Creates a named backreference. To be used with a named capture.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>This as a repeatable object</returns>
		public IRepeatable BackReference(string name)
		{
			this.expression.Add(new NamedBackReference(name));
			return this;
		}

		/// <summary>
		/// Creates an indexed backreference. To be used with an indexed capture.
		/// </summary>
		/// <param name="index">Capture index, starting from 1</param>
		/// <returns>This as a repeatable object</returns>
		public IRepeatable BackReference(uint index)
		{
			this.expression.Add(new IndexedBackReference(index));
			return this;
		}

		#endregion

		#region Literal

		/// <summary>
		/// Adds a literal regex.
		/// </summary>
		/// <param name="regex">The regex.</param>
		/// <returns>this</returns>
		public IMagex Literal(string regex)
		{
			this.expression.Add(new Literal(regex));
			return this;
		}

		public IRepeatable NumericRange(ulong from, ulong to)
		{
			this.expression.Add(new Literal(MagexBuilder.NumericRange(from, to)));
			return this;
		}

        public IRepeatable NumericRange(ulong from, ulong to, RangeOptions options)
        {
            this.expression.Add(new Literal(MagexBuilder.NumericRange(from, to, options)));
            return this;
        }

        #endregion

        #region ReverseEngineering

        public static string ReverseEngineer(string expression)
        {
            IList<ISegment> outputList = expression.ParseMagex();
            return string.Format("Magex.New(){0};", ConvertToMagex(outputList));
        }

        private static string ConvertToMagex(IList<ISegment> list)
        {
            var outputString = "";



            return outputString;
        }

        #endregion
    }
}
