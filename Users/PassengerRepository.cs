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
        private static readonly List<Passenger> RegisteredPassengers = [];
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
    }
}
