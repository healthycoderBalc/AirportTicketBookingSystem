using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities.ManagerUtilities
{
    public static class FilterByAirportAndCountryUtilities
    {
        public static string GetGivenName(bool airport)
        {
            string? searchTerm;
            do
            {
                Console.WriteLine();
                Console.WriteLine($"****** Search by {(airport ? "Airport" : "Country")} ******");

                Console.Write($"Please write the {(airport ? "Airport: " : "Country")}: ");

                searchTerm = Console.ReadLine();
                Console.WriteLine($"Your search term: {searchTerm}");
                Console.WriteLine();
            } while (string.IsNullOrEmpty(searchTerm));
            return searchTerm;
        }
    }
}
