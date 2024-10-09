// <copyright file="OperatorPrecedenceAttribute.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.Attributes
{
    /// <summary>
    /// Attribute used on operator enum
    /// to denote precedence
    /// </summary>
    public class OperatorPrecedenceAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorPrecedenceAttribute"/> class.
        /// </summary>
        /// <param name="operatorPrecedence">operator precedence</param>
        public OperatorPrecedenceAttribute(int operatorPrecedence)
        {
            this.OperatorPrecedence = operatorPrecedence;
        }

        /// <summary>
        /// Gets the operator precedence
        /// </summary>
        public int OperatorPrecedence { get; private set; }
    }
}
