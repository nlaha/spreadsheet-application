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
            List<int> list = new List<int>();

            // act
            List<int> result = Form1.FillListRandomIntegers(list);

            // assert
            Assert.That(result.Count, Is.EqualTo(10000));
            Assert.That(result.Max(), Is.LessThanOrEqualTo(20000));
            Assert.That(result.Min(), Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void TestFillListRandomIntegersClearsList()
        {
            // arrange
            List<int> list = [-2, -3, -6, -10, -123];

            // act
            List<int> result = Form1.FillListRandomIntegers(list);

            // assert
            Assert.That(result, Does.Not.Contain(-2));
            Assert.That(result, Does.Not.Contain(-10));
        }
    }
}