using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem.RepositoryInterfaces;
using AirportTicketBookingSystem.Utilities.LoadingUtilities;
using AirportTicketBookingSystem.Utilities.StorageUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Users
{
    public class PassengerRepository : IPassengerRepository
    {
        public List<Passenger> RegisteredPassengers { get; } = [];
        public List<int> UsedIds { get; } = new List<int>() { 0 };

        public void PrintAllRegisteredPassengers()
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

        public Passenger CreateAccount(string passengerName, string passengerEmail, string passengerPassword)
        {
            int lastUsedId = UsedIds.Last();
            Passenger passenger = new(lastUsedId + 1, passengerName, passengerEmail, passengerPassword);
            RegisteredPassengers.Add(passenger);
            UsedIds.Add(lastUsedId + 1);

            return passenger;
        }

        public Passenger? ValidateAccount(string passengerEmail, string passengerPassword)
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

        public void SaveAllPassengers()
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
