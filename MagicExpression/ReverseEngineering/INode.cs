using System;

namespace MagicExpression.ReverseEngineering
{
    public interface INode
    {
        System.Collections.Generic.IList<IExpression> Possibilities { get; set; }

        bool IsEmpty();
    }
}
