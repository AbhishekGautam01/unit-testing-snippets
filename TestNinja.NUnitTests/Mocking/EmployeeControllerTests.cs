using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestNinja.Mocking;

namespace TestNinja.NUnitTests.Mocking
{
    [TestFixture]
    class EmployeeControllerTests
    {
        //Testing Interaction
        [Test]
        public void DeleteEmplyee_WhenCalled_DeleteEmployeeFromDb()
        {
            var stroage = new Mock<IEmployeeStorage>();
            var empController = new EmployeeController(stroage.Object);

            empController.DeleteEmployee(1);

            stroage.Verify(s => s.DeleteEmployee(1));
        }

        [Test]
        public void DeleteEmployee_WhenCalled_ReturnsRedirectResultObject()
        {
            var stroage = new Mock<IEmployeeStorage>();
            var empController = new EmployeeController(stroage.Object);

            var result = empController.DeleteEmployee(1);

            Assert.That(result, Is.TypeOf<RedirectResult>());
        }

    }
}
