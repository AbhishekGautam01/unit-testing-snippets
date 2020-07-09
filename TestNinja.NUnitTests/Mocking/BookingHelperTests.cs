using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestNinja.Mocking;

namespace TestNinja.NUnitTests.Mocking
{
    [TestFixture]
    public class BookingHelper_OverlappingBookingsExistTests
    {
        private Mock<IBookingRepository> _bookingRepository;
        private Booking _existingBooking;

        [SetUp]
        public void SetUp()
        {
            _existingBooking = new Booking {
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Id = 2,
                Reference = "a" };
            _bookingRepository = new Mock<IBookingRepository>();

            _bookingRepository.Setup(br => br.GetActiveBookings(It.IsAny<int>())).Returns(new List<Booking>
            {
                _existingBooking
            }.AsQueryable());
        }

        [Test]
        public void BookingStartsAndFinishesBeforeExistingBooking_ReturnEmptyString()
        {

            //Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = Before(_existingBooking.ArrivalDate)
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(String.Empty));

        }

        [Test]
        public void BookingStartsBeforeAndFinshesInMiddleOfExistingBooking_ReturnExistingBookingReference()
        {

            //Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = After(_existingBooking.ArrivalDate)
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));

        }

        [Test]
        public void BookingStartsBeforeAndFinshesAfterExistingBooking_ReturnExistingBookingReference()
        {

            //Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = After(_existingBooking.DepartureDate)
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));

        }

        [Test]
        public void BookingStartsBeforeAndFinshesInMiddleOfAnExistingBooking_ReturnExistingBookingReference()
        {

            //Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate)
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));

        }
        [Test]
        public void BookingStartsInMiddleOfAnExistingBookingAndFinshesAfterExistingBooking_ReturnExistingBookingReference()
        {

            //Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate)
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));

        }

        [Test]
        public void BookingStartsAndFinshesAfterExistingBooking_ReturnEmptyString()
        {

            //Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate),
                DepartureDate = After(_existingBooking.DepartureDate, days: 2)
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(String.Empty));

        }

        [Test]
        public void BookingOverlapButNewbookingIsCancled_ReturnEemptyString()
        {

            //Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
                Status = "Cancelled"
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(String.Empty));

        }
        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 12, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }


    }
}
