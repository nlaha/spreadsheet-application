using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1_BST
{
    public class BSTNode<T> where T : IComparable<T>
    {
        /// <summary>
        /// The value inside this node
        /// </summary>
        public T Value {  get; set; }
        
        /// <summary>
        /// Left child node
        /// </summary>
        public BSTNode<T>? Left { get; set; }

        /// <summary>
        /// Right child node
        /// </summary>
        public BSTNode<T>? Right { get; set; }

        /// <summary>
        /// Constructor to make a node with a value
        /// </summary>
        /// <param name="value"></param>
        public BSTNode(T value)
        {
            Value = value;
        }
    }
}
