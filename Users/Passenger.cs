using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Users
{
    public class Passenger
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<FlightManagement.Booking>? Bookings { get; set; }

        public Passenger(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{Name} Email: {Email}");

            return stringBuilder.ToString();
        }
    }
}
