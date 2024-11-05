// <copyright file="TestHelpers.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace Spreadsheet_Nathan_Laha_Tests
{
    using System.Reflection;

    /// <summary>
    /// Helper functions for tests
    /// </summary>
    internal class TestHelpers
    {
        /// <summary>
        /// Gets a method on the main form object using reflection
        /// </summary>
        /// <typeparam name="T">the type of the object</typeparam>
        /// <param name="instance">the instance of the object</param>
        /// <param name="methodName">the name of the method</param>
        /// <returns>the method info</returns>
        internal static MethodInfo GetMethod<T>(T instance, string methodName)
        {
            if (string.IsNullOrWhiteSpace(methodName))
            {
                Assert.Fail("methodName cannot be null or whitespace");
            }

            if (instance == null)
            {
                Assert.Fail("instance cannot be null");
            }

            var method = instance!.GetType()
                .GetMethod(
                    methodName,
                    BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            if (method == null)
            {
                Assert.Fail(string.Format("{0} method not found", methodName));
            }

            return method!;
        }

        /// <summary>
        /// Gets a property on an object instance using reflection
        /// </summary>
        /// <typeparam name="T">the type of the object</typeparam>
        /// <param name="instance">the instance of the object</param>
        /// <param name="propertyName">the name of the property</param>
        /// <returns>the property value</returns>
        internal static object? GetProperty<T>(T instance, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                Assert.Fail("propertyName cannot be null or whitespace");
            }

            if (instance == null)
            {
                Assert.Fail("instance cannot be null");
            }

            var property = instance!.GetType()
                .GetProperty(
                    propertyName,
                    BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            if (property == null)
            {
                Assert.Fail(string.Format("{0} property not found", propertyName));
            }

            return property!.GetValue(instance);
        }
    }
}
