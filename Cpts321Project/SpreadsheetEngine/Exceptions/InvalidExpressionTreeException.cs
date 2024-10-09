// <copyright file="InvalidExpressionTreeException.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.Exceptions
{
    /// <summary>
    /// Exception thrown when an expression tree is invalid
    /// </summary>
    public class InvalidExpressionTreeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidExpressionTreeException"/> class.
        /// </summary>
        public InvalidExpressionTreeException()
        {
            return;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidExpressionTreeException"/> class.
        /// </summary>
        /// <param name="message">the message</param>
        public InvalidExpressionTreeException(string message)
            : base(message)
        {
            return;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidExpressionTreeException"/> class.
        /// </summary>
        /// <param name="message">the message</param>
        /// <param name="inner">the inner exception</param>
        public InvalidExpressionTreeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
