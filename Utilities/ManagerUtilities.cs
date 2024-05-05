using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities
{
    public static class ManagerUtilities
    {
        public static string RequestManagerCode()
        {
            string? code;
            do
            {
                Console.WriteLine("Please write the Manager Code: ");
                code = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(code));
            return code;
        }

    }
}
