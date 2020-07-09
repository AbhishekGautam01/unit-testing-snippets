using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.NUnitTests.Fundamentals
{
    [TestFixture]
    public class Tests
    {
        
        [Test]
        public void GetOutput_InputIsDivisibleByBoth3And5_ReturnsFizzBuzz()
        {
            var result = FizzBuzz.GetOutput(15);
            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }

        [Test]
        public void GetOutput_InputIsDivisibleByOnly3_ReturnsFizz()
        {
            var result = FizzBuzz.GetOutput(6);
            Assert.That(result, Is.EqualTo("Fizz"));
        }
        [Test]
        public void GetOutput_InputIsDivisibleByOnly5_ReturnsBuzz()
        {
            var result = FizzBuzz.GetOutput(20);
            Assert.That(result, Is.EqualTo("Buzz"));
        }
        [Test]
        public void GetOutput_InputIsDivisibleNotDivisbleByEither3Or5_ReturnsTheSameNumber()
        {
            var result = FizzBuzz.GetOutput(7);
            Assert.That(result, Is.EqualTo("7"));
        }



    }
}