using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestNinja.Fundamentals;

namespace TestNinja.NUnitTests.Fundamentals
{
    [TestFixture]
    class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _demeritsPointsCalculator;
        private const int MAX_SPEED = 300;

        [SetUp]
        public void SetUp()
        {
            _demeritsPointsCalculator = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(-1)]
        [TestCase(MAX_SPEED+1)]
        public void CalculateDemeritPoints_OutofRangeSpeedInput_ThrowsArgumentOutOfRangeException(int speed)
        {
            Assert.That(() => _demeritsPointsCalculator.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
       }

        [Test]
        [TestCase(0, 0)]
        [TestCase(65, 0)]
        [TestCase(50, 0)]
        [TestCase(66, 0)]
        [TestCase(70, 1)]
        [TestCase(75, 2)]
        public void CalculateDemeritPoints_WhenCalled_ReturnsDemritPoints(int speed, int expectedResult)
        {
            var result = _demeritsPointsCalculator.CalculateDemeritPoints(speed);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
        [Test]
        public void CalculateDemeritPoints_InputSpeedIsMoreThanSpeedLimit_ReturnsDemeritPoints()
        {
            var result = _demeritsPointsCalculator.CalculateDemeritPoints(80);
            Assert.That(result, Is.EqualTo(3));
        }

    }
}
