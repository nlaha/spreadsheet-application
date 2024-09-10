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
    }
}