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

        public int Count { get; private set; }
        public int Height { get => TreeHeightRecursive(_root); }
        public int MinHeight { get => (int)Math.Ceiling(Math.Log2(Count + 1));  }

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
            if (_root == null)
            {
                _root = new BSTNode<T>(item);
                Count++;
                return true;
            }
            else
            {
                BSTNode<T>? current = _root;

                while (current != null)
                {
                    if (current.Value.Equals(item))
                    {
                        // disallow duplicates
                        return false;
                    }

                    // insert in sorted order
                    if (current.Value.CompareTo(item) > 0)
                    {
                        if (current.Left == null)
                        {
                            current.Left = new BSTNode<T>(item);
                            Count++;
                            return true;
                        }
                        else
                        {
                            current = current.Left;
                        }
                    } else
                    {
                        if (current.Right == null)
                        {
                            current.Right = new BSTNode<T>(item);
                            Count++;
                            return true;
                        }
                        else
                        {
                            current = current.Right;
                        }
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Prints the BST values from smallest to largest
        /// </summary>
        public void Print()
        {
            Action<T> printAction = v => Console.Write($"{v}, ");
            InOrderTraversal(_root, printAction);
            Console.Write("\n");
        }

        /// <summary>
        /// Internal function for finding the number of levels in the tree
        /// </summary>
        /// <param name="node"></param>
        private int TreeHeightRecursive(BSTNode<T>? node)
        {
            if (node == null)
                return 0;

            var hLeft = TreeHeightRecursive(node.Left);
            var hRight = TreeHeightRecursive(node.Right);
            return Math.Max(hLeft, hRight) + 1;
        }

        /// <summary>
        /// Internal recursive function for tree traversal
        /// </summary>
        /// <param name="node">the node to start with (usually the root)</param>
        /// <param name="processFunc">an action delegate that is called on the value of each node traversed</param>
        private void InOrderTraversal(BSTNode<T>? node, Action<T> processFunc)
        {
            if (node == null)
            {
                return;
            }

            // in order traversal
            InOrderTraversal(node.Left, processFunc);

            processFunc(node.Value);

            InOrderTraversal(node.Right, processFunc);
        }
    }
}
