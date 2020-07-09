using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestNinja.Fundamentals;

namespace TestNinja.NUnitTests.Fundamentals
{
    [TestFixture]
    public class CustomControllerTests
    {
        private CustomerController _customerController;

        [SetUp]
        public void SetUp()
        {
            _customerController = new CustomerController();
        }

        [Test]
        public void GetCustomer_WhenValidIdPassed_ReturnsOK()
        {
            var result = _customerController.GetCustomer(1);

            Assert.That(result, Is.TypeOf<Ok>());
        }


        [Test]
        public void GetCustomer_WhenValidIsInvalid_ReturnsNotFound()
        {
            var result = _customerController.GetCustomer(0);

            Assert.That(result, Is.TypeOf<NotFound>());
        }
    }
}
