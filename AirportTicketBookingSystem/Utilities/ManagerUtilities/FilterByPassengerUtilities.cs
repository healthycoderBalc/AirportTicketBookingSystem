using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.RepositoryInterfaces;
using AirportTicketBookingSystem.Users;

namespace AirportTicketBookingSystem.Utilities.ManagerUtilities
{
    public class FilterByPassengerUtilities
    {
        private readonly IPassengerRepository _passengerRepository;

        public FilterByPassengerUtilities(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        public int GetPassengerSelected()
        {
            Console.WriteLine();
            _passengerRepository.PrintAllRegisteredPassengers();
            bool validPassengerId = false;
            int numericalPassengerId;
            do
            {
                Console.Write("Please write the number (Id) of the passenger you want to filter by: ");
                string? passengerId = Console.ReadLine();
                validPassengerId = int.TryParse(passengerId, out numericalPassengerId);
                if (!validPassengerId)
                {
                    Console.WriteLine("Invalid input. Select another flight");
                }
            } while (!validPassengerId);
            return numericalPassengerId;
        }
    }
}
