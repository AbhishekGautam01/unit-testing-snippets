using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestClass]
    public class ReservationTests
    {
        [TestMethod]
        public void CanBeCanceledBy_Admin_ReturnsTrue()
        {
            //Arrange
            Reservation reservation = new Reservation();

            //Act
            bool result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanBeCancledBy_UserIsWhoCreatedIt_ReturnsTrue()
        {
            //Arrange
            User user = new User();
            Reservation reservation = new Reservation { MadeBy = user};
            
            //Act
            bool result = reservation.CanBeCancelledBy(user);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanBeCanceledBy_AnotherUserCanceling_ReturnsFalse()
        {
            //Arrange
            User user = new User(); 
            Reservation reservation = new Reservation { MadeBy = user};

            //Act
            bool result = reservation.CanBeCancelledBy(new User());

            //Assert
            Assert.IsFalse(result);

        }
    }
}
