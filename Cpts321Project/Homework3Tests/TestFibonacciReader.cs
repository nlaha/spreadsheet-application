// <copyright file="TestFibonacciReader.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace Homework3Tests
{
    using System.Numerics;
    using Homework3;

    public class TestFibonacciReader
    {
        [Test]
        public void FibonacciReader_ReturnsFirst50Numbers()
        {
            // Arrange
            var fibTextReader = new FibonacciTextReader(50);

            // Act
            var result = fibTextReader.ReadToEnd();

            // Assert
        }

        [Test]
        public void FibonacciReader_ReturnsFirst100Numbers()
        {
            // Arrange
            var fibTextReader = new FibonacciTextReader(50);

            // Act
            var result = fibTextReader.ReadToEnd();

            // Assert
        }

        [Test]
        public void FibonacciReader_ThrowsOnNegativeInput()
        {
            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new FibonacciTextReader(-22));
        }

        [Test]
        public void FibonacciReader_ReturnsEmptyOnZeroInput()
        {
            // Arrange
            var fibTextReader = new FibonacciTextReader(0);

            // Act
            var result = fibTextReader.ReadToEnd();

            // Assert
            Assert.That(result, Is.Empty);
        }
    }
}