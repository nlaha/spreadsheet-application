// <copyright file="OperatorSymbolAttribute.cs" company="Nathan Laha">
// 11762135
// </copyright>
namespace SpreadsheetEngine.Attributes
{
    using System.ComponentModel;

    /// <summary>
    /// Attribute used on operator enum
    /// to denote the symbol, i.e. +, -, /
    /// </summary>
    public class OperatorSymbolAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorSymbolAttribute"/> class.
        /// </summary>
        /// <param name="operatorSymbol">operator symbol</param>
        public OperatorSymbolAttribute(string operatorSymbol)
        {
            this.OperatorSymbol = operatorSymbol;
        }

        /// <summary>
        /// Gets the operator precedence
        /// </summary>
        public string OperatorSymbol { get; private set; }

        /// <summary>
        /// Gets the enum value by it's symbol attribute
        /// </summary>
        /// <typeparam name="T">the enum type</typeparam>
        /// <param name="symbol">the symbol string</param>
        /// <returns>the enum value</returns>
        /// <exception cref="ArgumentException">thrown if not found</exception>
        /// <remarks>Based on: https://stackoverflow.com/a/4367868/24202835</remarks>
        public static T GetValueFromSymbol<T>(string symbol)
            where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(OperatorSymbolAttribute)) is OperatorSymbolAttribute attribute)
                {
                    if (attribute.OperatorSymbol == symbol)
                    {
                        var value = field.GetValue(null);
                        if (value is T)
                        {
                            return (T)value;
                        }
                    }
                }
            }

            throw new ArgumentException("Symbol not found.", nameof(symbol));
        }
    }
}
