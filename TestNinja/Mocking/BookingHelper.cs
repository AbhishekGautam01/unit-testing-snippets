using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public static class BookingHelper
    {
        //as this method is static then we cant do constructor DI or paramter Injection so we can either remove static keyword 
        //which can break if it is used else where in code or another approach is method DI but the problem is  our DI framework does not support it.
        public static string OverlappingBookingsExist(Booking booking, IBookingRepository bookingRepository)
        {
            if (booking.Status == "Cancelled")
                return string.Empty;

            var bookings = bookingRepository.GetActiveBookings(booking.Id);

            var overlappingBooking =
                bookings.FirstOrDefault(
                    b =>
                        booking.ArrivalDate < b.DepartureDate && 
                        b.ArrivalDate < booking.DepartureDate);

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }

    

    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }
}