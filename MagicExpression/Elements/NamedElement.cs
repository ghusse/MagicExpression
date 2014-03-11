namespace MagicExpression.Elements
{
	using System;

	public abstract class NamedElement
	{
		private string name;

		protected NamedElement(string name)
		{
			this.Name = name;
		}

		public string Name
		{
			get
			{
				return this.name;
			}

			private set
			{
				if (!RegexCharacters.IsValidName(value))
				{
					throw new InvalidOperationException("Name must start with a letter and can only contain numbers and letters.");
				}

				this.name = value;
			}
		}
	}
}
