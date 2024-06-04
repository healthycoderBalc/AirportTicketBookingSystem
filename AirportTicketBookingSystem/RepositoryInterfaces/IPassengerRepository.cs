using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.RepositoryInterfaces
{
    public interface IPassengerRepository
    {
        List<Passenger> RegisteredPassengers { get; }
        List<int> UsedIds { get; }
        void PrintAllRegisteredPassengers();
        void SaveAllPassengers();

        Passenger CreateAccount(string passengerName, string passengerEmail, string passengerPassword);
        Passenger? ValidateAccount(string passengerEmail, string passengerPassword);
    }
}
