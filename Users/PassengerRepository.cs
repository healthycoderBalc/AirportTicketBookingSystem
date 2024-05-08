using AirportTicketBookingSystem.FlightManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Users
{
    public static class PassengerRepository
    {
        public static readonly List<Passenger> RegisteredPassengers = [];
        public static Passenger CreateAccount(string passengerName, string passengerEmail, string passengerPassword)
        {
            Passenger passenger = new(passengerName, passengerEmail, passengerPassword);
            RegisteredPassengers.Add(passenger);
            return passenger;
        }

        public static Passenger? ValidateAccount(string passengerEmail, string passengerPassword)
        {
            try
            {
                Passenger passengerValidated =
                  RegisteredPassengers.Where(passenger => passenger.Email.Equals(passengerEmail) && passenger.Password.Equals(passengerPassword)).Single();

                return passengerValidated;
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Either email or password are incorrect");
                return null;
            }

        }

        public static Booking CreateBooking(Flight flight, Passenger passenger, FlightAvailability flightAvailability)
        {
            // creating booking object with passenger data
            Booking booking = new(flight, flightAvailability, passenger);

            // look for passenger and add the booking to its list of bookings.
            foreach (Passenger passenger1 in RegisteredPassengers)
            {
                if (passenger1.Email.Equals(passenger.Email))
                {
                    if (passenger1.Bookings != null)
                    {

                        passenger1.Bookings.Add(booking);
                    }
                    else
                    {
                        passenger1.Bookings = [booking];
                    }
                }
            }
            return booking;
        }

        public static List<Booking>? GetBookingsByPassenger(Passenger passenger)
        {
            List<Booking>? bookings = new List<Booking>();
            bookings = RegisteredPassengers.Single(p => p.Email.Equals(passenger.Email)).Bookings;
            return bookings;
        }
    }
}
