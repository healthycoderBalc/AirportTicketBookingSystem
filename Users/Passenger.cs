using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Users
{
    public class Passenger
    {
        public string Name { get; set; }

        public Passenger(string name)
        {
            Name = name;
        }

        public List<string> MenuOptions()
        {
            List<string> menu = new List<string>();
            menu.Add("Book a flight");
            menu.Add("Manage Bookins");

            return menu;
        }
    }
}
