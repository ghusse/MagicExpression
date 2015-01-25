using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicExpression.ReverseEngineering
{
    /// <summary>
    /// A wrapper class containing an <see cref="Expression"/>. 
    /// The expression wraps the actual Reverse engineering of the Magex.
    /// </summary>
    public class ReverseBuilder
    {
        private Expression _expression = null;
        public Expression Expression 
        { 
            get
            {
                return this._expression;    
            }
            private set
            { 
                this._expression = value; 
            }
        }

        public ReverseBuilder(string regex)
        {
            this.Expression = new Expression(regex);
        }
    }
}
