using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities
{
    public static class PassengerUtilities
    {
        public static List<string> MenuOptions()
        {
            List<string> menu = new List<string>();
            menu.Add("Book a flight");
            menu.Add("Manage Bookings");

            return menu;
        }
    }
}
