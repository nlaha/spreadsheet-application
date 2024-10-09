// <copyright file="AttributeHelpers.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace SpreadsheetEngine.Attributes
{
    using System.ComponentModel;

    /// <summary>
    /// Helper class for attributes
    /// </summary>
    internal static class AttributeHelpers
    {
        /// <summary>
        /// Generic function for getting an attribute on an enum value
        /// </summary>
        /// <typeparam name="TAttribute">the attribute type</typeparam>
        /// <param name="value">the enum</param>
        /// <returns>the attribute value</returns>
        /// <exception cref="InvalidOperationException">thrown when the attribute could not be found</exception>
        /// <remarks>Source: https://stackoverflow.com/a/35040378/24202835</remarks>
        public static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value) ??
                throw new InvalidOperationException("Could not get name from enum");
            return enumType?.GetField(name)?
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault() ??
                throw new InvalidOperationException($"Could not find attribute");
        }

        /// <summary>
        /// Generic function for getting all attributes of a type on an enum
        /// </summary>
        /// <typeparam name="TAttribute">the attribute type</typeparam>
        /// <param name="type">the enum type</param>
        /// <returns>the attributes</returns>
        /// <exception cref="InvalidOperationException">thrown when the attribute could not be found</exception>
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(Type type)
        {
            // skip the first (default) field
            return type?.GetFields().Skip(1).Select(f => f.GetCustomAttributes(false).OfType<TAttribute>().Single()) ??
                throw new InvalidOperationException($"Could not find attribute");
        }
    }
}