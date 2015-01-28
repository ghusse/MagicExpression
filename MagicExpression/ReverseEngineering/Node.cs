using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicExpression.ReverseEngineering
{
    public class Node : MagicExpression.ReverseEngineering.INode
    {
        public IList<IExpression> Possibilities { get; set; }

        public Node()
        {
            Initialize();
        }

        public Node(IExpression part)
        {
            Initialize();
            Possibilities.Add(part);
        }

        private void Initialize()
        {
            Possibilities = new List<IExpression>(0);
        }

        public bool IsEmpty()
        {
            if (this.Possibilities != null && this.Possibilities.Count > 0)
                return false;
            return true;
        }
    }
}
