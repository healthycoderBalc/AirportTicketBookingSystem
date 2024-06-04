using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Users
{
    public static class Manager
    {
        public static string ManagerCode { get; } = "1234";

        public static bool Validate(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return false;
            }
            else
            {
                if (code.Equals(ManagerCode))
                {
                    Console.WriteLine("Welcome Manager!");
                    return true;
                }
                else
                {
                    Console.WriteLine("The code you have entered is not correct. Going back");
                    return false;
                }
            }


        }
    }
}
