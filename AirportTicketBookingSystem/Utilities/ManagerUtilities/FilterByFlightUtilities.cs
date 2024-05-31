using AirportTicketBookingSystem.FlightManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities.ManagerUtilities
{
    public static class FilterByFlightUtilities
    {
        public static int GetSelectedFlightId()
        {
            Console.WriteLine();
            FlightsInventory.ShowFlights(FlightsInventory.Flights);
            bool validFlightNumber = false;
            int numericalFlightNumber;
            do
            {
                Console.Write("Please write the number (Id) of the flight you want to filter by: ");
                string? flightNumber = Console.ReadLine();
                validFlightNumber = int.TryParse(flightNumber, out numericalFlightNumber);
                if (!validFlightNumber)
                {
                    Console.WriteLine("Invalid input. Select another flight");
                }
            } while (!validFlightNumber);
            return numericalFlightNumber;
        }

    }
}
