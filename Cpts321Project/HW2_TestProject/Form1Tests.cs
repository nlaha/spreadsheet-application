// <copyright file="Form1Tests.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace HW2_TestProject
{
    using HW2_Project;

    /// <summary>
    /// Tests for the Form1Tests.
    /// </summary>
    public class Form1Tests
    {
        /// <summary>
        /// Test for FillListRandomIntegers.
        /// </summary>
        [Test]
        public void TestFillListRandomIntegersReturnsFilledArray()
        {
            // arrange

            // act
            List<int> result = Form1.FillListRandomIntegers();

            // assert
            Assert.That(result.Count, Is.EqualTo(10000));
            Assert.That(result.Max(), Is.LessThanOrEqualTo(20000));
            Assert.That(result.Min(), Is.GreaterThanOrEqualTo(0));
        }

        /// <summary>
        /// Test for UniqueIntegersOne.
        /// </summary>
        [Test]
        public void TestUniqueIntegersOneReturnsNumUniqueIntegers()
        {
            // arrange
            List<int> random = Form1.FillListRandomIntegers();

            // act
            int numUnique = Form1.UniqueIntegersOne(random);

            // assert
            Assert.That(numUnique, Is.EqualTo(random.Distinct().Count()));
        }

        /// <summary>
        /// Test for UniqueIntegersOne.
        /// </summary>
        [Test]
        public void TestUniqueIntegersOneReturnsZeroForEmptyList()
        {
            // arrange
            List<int> random = new List<int>();

            // act
            int numUnique = Form1.UniqueIntegersOne(random);

            // assert
            Assert.That(numUnique, Is.EqualTo(0));
        }

        /// <summary>
        /// Test for UniqueIntegersTwo.
        /// </summary>
        [Test]
        public void TestUniqueIntegersTwoReturnsNumUniqueIntegers()
        {
            // arrange
            List<int> random = Form1.FillListRandomIntegers();

            // act
            int numUnique = Form1.UniqueIntegersTwo(random);

            // assert
            Assert.That(numUnique, Is.EqualTo(random.Distinct().Count()));
        }

        /// <summary>
        /// Test for UniqueIntegersTwo.
        /// </summary>
        [Test]
        public void TestUniqueIntegersTwoReturnsZeroForEmptyList()
        {
            // arrange
            List<int> random = new List<int>();

            // act
            int numUnique = Form1.UniqueIntegersTwo(random);

            // assert
            Assert.That(numUnique, Is.EqualTo(0));
        }

        /// <summary>
        /// Test for UniqueIntegersThree.
        /// </summary>
        [Test]
        public void TestUniqueIntegersThreeReturnsNumUniqueIntegers()
        {
            // arrange
            List<int> random = Form1.FillListRandomIntegers();

            // act
            int numUnique = Form1.UniqueIntegersThree(random);

            // assert
            Assert.That(numUnique, Is.EqualTo(random.Distinct().Count()));
        }

        /// <summary>
        /// Test for UniqueIntegersThree.
        /// </summary>
        [Test]
        public void TestUniqueIntegersThreeReturnsZeroForEmptyList()
        {
            // arrange
            List<int> random = new List<int>();

            // act
            int numUnique = Form1.UniqueIntegersThree(random);

            // assert
            Assert.That(numUnique, Is.EqualTo(0));
        }
    }
}