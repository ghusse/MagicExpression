using System.Collections.Generic;

namespace MagicExpression.ReverseEngineering
{
    public interface INode
    {
        IList<IExpression> Possibilities { get; set; }
        bool IsEmpty();
    }

    public class Node : INode
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
