using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1_BST
{
    public class BST<T> where T : IComparable<T>
    {
        private BSTNode<T>? _root;

        public BST()
        {
            _root = null;
        }

        /// <summary>
        /// Adds an item to the BST
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true if insertion was succesful</returns>
        public bool Add(T item)
        {
            BSTNode<T>? current = _root;

            if (current == null)
            {
                current = new BSTNode<T>(item);
            }
            else
            {
                while (current != null)
                {
                    if (current.Value.Equals(item))
                    {
                        // disallow duplicates
                        return false;
                    }

                    // insert in sorted order
                    if (current.Value.CompareTo(item) < 0)
                    {
                        current = current.Left;
                    } else
                    {
                        current = current.Right;
                    }
                }

                current = new BSTNode<T>(item);
            }

            return true;
        }

        /// <summary>
        /// Prints the BST values from smallest to largest
        /// </summary>
        public void Print()
        {
            Action<T> printAction = v => Console.Write($"{v}, ");
            Traverse(_root, printAction);
        }

        /// <summary>
        /// Internal recursive function for tree traversal
        /// </summary>
        /// <param name="node">the node to start with (usually the root)</param>
        /// <param name="processFunc">an action delegate that is called on the value of each node traversed</param>
        private void Traverse(BSTNode<T>? node, Action<T> processFunc)
        {
            if (node == null)
            {
                return;
            }

            // in order traversal
            Traverse(node.Left, processFunc);

            processFunc(node.Value);

            Traverse(node.Right, processFunc);
        }
    }
}
