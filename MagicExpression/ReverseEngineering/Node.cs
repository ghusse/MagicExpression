//using System.Collections.Generic;

//namespace MagicExpression.ReverseEngineering
//{
//    public interface INode
//    {
//        IList<IExpression> Possibilities { get; set; }
//        bool IsEmpty();
//    }

//    /// <summary>
//    /// Represents a single character in the original Regular Expression, 
//    /// regardless if it represents something special or just a character
//    /// </summary>
//    public class Node : INode
//    {
//        /// <summary>
//        /// A list of all the possibilities for this character
//        /// Can be something special (ex: "\d") or a simple character (ex: "x")
//        /// The Node representing the first character of "\d" would contain a "NumericSegment" and potentially a "Character('\')" segment... 
//        ///  (but it actually doesn't since "\d" can be formally 100% identified)
//        /// </summary>
//        public IList<IExpression> Possibilities { get; set; }

//        public Node()
//        {
//            Initialize();
//        }

//        public Node(IExpression part)
//        {
//            Initialize();
//            Possibilities.Add(part);
//        }

//        private void Initialize()
//        {
//            Possibilities = new List<IExpression>(0);
//        }

//        public bool IsEmpty()
//        {
//            if (this.Possibilities != null && this.Possibilities.Count > 0)
//                return false;
//            return true;
//        }
//    }
//}
