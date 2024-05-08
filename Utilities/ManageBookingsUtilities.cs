using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.Users;
using AirportTicketBookingSystem.FlightManagement;

namespace AirportTicketBookingSystem.Utilities
{
    public static class ManageBookingsUtilities
    {
        public static void CancelBooking(string bookingNumber, Passenger passenger)
        {
            List<Booking>? bookings = PassengerRepository.GetBookingsByPassenger(passenger);
            bool validBookingNumber = int.TryParse(bookingNumber, out int numericalBookingNumber);

            if (bookings != null && validBookingNumber)
            {
                for (int i = 0; i < bookings.Count; i++)
                {
                    if (numericalBookingNumber == i + 1)
                    {
                        foreach (Passenger p in PassengerRepository.RegisteredPassengers)
                        {
                            if (p.Email.Equals(passenger.Email))
                            {
                                p.Bookings?.Remove(p.Bookings[i]);
                                break;
                            }
                        }
                        break;
                    }
                }
            }

        }

        public static void ModifyBooking(string bookingNumber, Passenger passenger, Flight flight, FlightAvailability flightAvailability)
        {
            List<Booking>? bookings = PassengerRepository.GetBookingsByPassenger(passenger);
            bool validBookingNumber = int.TryParse(bookingNumber, out int numericalBookingNumber);

            Booking booking = new(flight, flightAvailability, passenger);


            if (bookings != null && validBookingNumber)
            {
                for (int i = 0; i < bookings.Count; i++)
                {
                    if (numericalBookingNumber == i + 1)
                    {
                        foreach (Passenger p in PassengerRepository.RegisteredPassengers)
                        {
                            if (p.Email.Equals(passenger.Email))
                            {
                                p.Bookings?.Remove(p.Bookings[i]);
                                p.Bookings?.Add(booking);
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }

        public static void ViewMyBookings(Passenger passenger)
        {
            List<Booking>? bookings = PassengerRepository.GetBookingsByPassenger(passenger);
            Console.WriteLine();
            Console.WriteLine("**************************************");
            Console.WriteLine("**********  Your Bookings ************");
            Console.WriteLine("**************************************");
            Console.WriteLine();

            if (bookings == null)
            {
                Console.WriteLine("Currently you don't have bookings");
            }
            else
            {
                foreach (Booking booking in bookings)
                {
                    Console.WriteLine($"Booking number: {bookings.IndexOf(booking) + 1}");
                    Console.WriteLine(booking);
                }
            }
        }

        public static Booking? SelectBooking(string bookingNumber, Passenger passenger)
        {
            List<Booking>? bookings = PassengerRepository.GetBookingsByPassenger(passenger);
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
