// <copyright file="InvalidOperatorException.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.Exceptions
{
    /// <summary>
    /// Exception thrown when an expression tree is invalid
    /// </summary>
    public class InvalidOperatorException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidOperatorException"/> class.
        /// </summary>
        public InvalidOperatorException()
        {
            return;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidOperatorException"/> class.
        /// </summary>
        /// <param name="message">the message</param>
        public InvalidOperatorException(string message)
            : base(message)
        {
            return;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidOperatorException"/> class.
        /// </summary>
        /// <param name="message">the message</param>
        /// <param name="inner">the inner exception</param>
        public InvalidOperatorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}