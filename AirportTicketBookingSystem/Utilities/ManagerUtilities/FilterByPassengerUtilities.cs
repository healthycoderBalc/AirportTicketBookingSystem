using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.Users;

namespace AirportTicketBookingSystem.Utilities.ManagerUtilities
{
    public static class FilterByPassengerUtilities
    {
        public static int GetPassengerSelected()
        {
            Console.WriteLine();
            PassengerRepository.PrintAllRegisteredPassengers();
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
