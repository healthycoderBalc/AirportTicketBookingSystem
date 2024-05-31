using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem.Utilities.LoadingUtilities;
using AirportTicketBookingSystem.Utilities.StorageUtilities;
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
        public static List<int> UsedIds = new List<int>() { 0 };

        public static void PrintAllRegisteredPassengers()
        {
            if (RegisteredPassengers == null)
            {
                Console.WriteLine("No passengers to show");
            }
            else
            {
                foreach (Passenger passenger in RegisteredPassengers)
                {
                    Console.WriteLine(passenger);
                    Console.WriteLine();
                }
            }
        }

        public static Passenger CreateAccount(string passengerName, string passengerEmail, string passengerPassword)
        {
            int lastUsedId = PassengerRepository.UsedIds.Last();
            Passenger passenger = new(lastUsedId + 1, passengerName, passengerEmail, passengerPassword);
            RegisteredPassengers.Add(passenger);
            UsedIds.Add(lastUsedId + 1);

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
            int lastUsedId = BookingRepository.UsedIds.Last();
            // creating booking object with passenger data
            Booking booking = new(lastUsedId + 1, flight, flightAvailability, passenger);

            // look for passenger and add the booking to its list of bookings.
            foreach (Passenger passenger1 in RegisteredPassengers)
            {
                if (passenger1.Email.Equals(passenger.Email))
                {
                    BookingRepository.Bookings.Add(booking);
                    BookingRepository.UsedIds.Add(booking.Id);
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

        public static List<Booking>? GetBookingsByPassenger1(Passenger passenger)
        {
            List<Booking>? bookings = new List<Booking>();
            bookings = RegisteredPassengers.Single(p => p.Email.Equals(passenger.Email)).Bookings;
            return bookings;
        }

        public static List<Booking>? GetBookingsByPassenger(Passenger passenger)
        {
            List<Booking>? bookings = BookingRepository.Bookings.Where(b => b.Passenger.Email.Equals(passenger.Email)).ToList();
            return bookings;
        }

        public static void SaveAllPassengers()
        {
            StoragePassengerUtilities storagePassengerUtilities = new StoragePassengerUtilities();
            List<Passenger> passengers = new List<Passenger>();

            if (RegisteredPassengers != null)
            {
                foreach (var p in RegisteredPassengers)
                {
                    if (p != null)
                    {
                        passengers.Add(p);
                    }
                }
                if (passengers.Count > 0)
                {
                    storagePassengerUtilities.SavePassengersToFile(passengers);
                }
            }

        }
    }
}
