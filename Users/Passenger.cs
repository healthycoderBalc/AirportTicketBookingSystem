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
        public string Email { get; set; }

        public Passenger(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
