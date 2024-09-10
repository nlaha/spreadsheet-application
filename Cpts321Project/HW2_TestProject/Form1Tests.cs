using HW2_Project;

namespace HW2_TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

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