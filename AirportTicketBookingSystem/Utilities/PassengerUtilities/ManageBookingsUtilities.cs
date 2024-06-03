using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.Users;
using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem.RepositoryInterfaces;

namespace AirportTicketBookingSystem.Utilities.PassengerUtilities
{
    public class ManageBookingsUtilities
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPassengerRepository _passengerRepository;

        public ManageBookingsUtilities(IBookingRepository bookingRepository, IPassengerRepository passengerRepository)
        {
            _bookingRepository = bookingRepository;
            _passengerRepository = passengerRepository;
        }

        public void CancelBooking(string bookingNumber, IPassenger passenger)
        {
            var booking = _bookingRepository.Bookings.Single(b => b.Id.ToString() == bookingNumber && b.Passenger.Id == passenger.Id);

            if (booking == null)
            {
                throw new Exception("The booking number you have selected does not exist in your bookings");
            }

            _bookingRepository.Bookings.Remove(booking);

            Console.WriteLine();
            Console.WriteLine("***********************************************************");
            Console.WriteLine($"Booking number {bookingNumber} has been Canceled!");
            Console.WriteLine("***********************************************************");
            Console.WriteLine();
        }

        public void ModifyBooking(string bookingNumber, IPassenger passenger, Flight flight, FlightAvailability flightAvailability)
        {
            var booking = _bookingRepository.Bookings.Single(b => b.Id.ToString() == bookingNumber && b.Passenger.Id == passenger.Id);
            if (booking == null)
            {
                throw new Exception("The booking number you have selected does not exist in your bookings");
            }

            booking.Flight = flight;
            booking.FlightAvailability = flightAvailability;

            Console.WriteLine();
            Console.WriteLine("***********************************************************");
            Console.WriteLine($"Booking number {bookingNumber} has been Modified!");
            Console.WriteLine("***********************************************************");
            Console.WriteLine();


            //List<Booking>? bookings = PassengerRepository.GetBookingsByPassenger(passenger);
            //bool validBookingNumber = int.TryParse(bookingNumber, out int numericalBookingNumber);

            //Booking booking = new(numericalBookingNumber, flight, flightAvailability, passenger);


            //if (bookings != null && validBookingNumber)
            //{
            //    if (BookingRepository.Bookings.Single(b => b.Id.Equals(numericalBookingNumber)).Passenger.Email.Equals(passenger.Email))
            //    {
            //        BookingRepository.Bookings.Remove(BookingRepository.Bookings.Single(b => b.Id.Equals(numericalBookingNumber)));
            //        BookingRepository.Bookings.Add(booking);
            //        Console.WriteLine();
            //        Console.WriteLine("***********************************************************");
            //        Console.WriteLine($"Booking number {numericalBookingNumber} has been Modified!");
            //        Console.WriteLine("***********************************************************");
            //        Console.WriteLine();
            //    }
            //    else
            //    {
            //        Console.WriteLine("The booking number you have selected does not exist in your bookings");
            //    }
            //}
        }

        public void ViewMyBookings(Passenger passenger)
        {
            List<Booking>? bookings = _bookingRepository.GetBookingsByPassenger(passenger);
            Console.WriteLine();
            Console.WriteLine("**************************************");
            Console.WriteLine("**********  Your Bookings ************");
            Console.WriteLine("**************************************");
            Console.WriteLine();

            BookingRepository.PrintBookings(bookings);
        }

        public Booking? SelectBooking(string bookingNumber, Passenger passenger)
        {
            List<Booking>? bookings = _bookingRepository.GetBookingsByPassenger(passenger);
            bool validBookingNumber = int.TryParse(bookingNumber, out int numericalBookingNumber);
            Booking? booking = null;

            if (bookings != null && validBookingNumber)
            {
                for (int i = 0; i < bookings.Count; i++)
                {
                    if (numericalBookingNumber == i + 1)
                    {
                        booking = bookings[i];
                        break;
                    }
                }
            }

            return booking;
        }


    }
}
