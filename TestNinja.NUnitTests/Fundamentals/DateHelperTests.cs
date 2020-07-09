using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestNinja.Fundamentals;

namespace TestNinja.NUnitTests.Fundamentals
{
    [TestFixture]
    public class DateHelperTests
    {
        [Test]
        [TestCase("06/10/2020", "07/1/2020")]
        [TestCase("12/12/2020", "01/01,2021")]
        public void FirstOfNextMonth_WhenCalled_ReturnsDateWhichIsFirstOfNextMonth(DateTime inputDate, DateTime expectedDate)
        {
            var result = DateHelper.FirstOfNextMonth(inputDate);

            Assert.That(result, Is.EqualTo(expectedDate));
        }
    }
}
