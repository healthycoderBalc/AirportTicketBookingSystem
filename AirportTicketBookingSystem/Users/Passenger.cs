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
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Passenger(int id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{Id}. {Name} Email: {Email}");

            return stringBuilder.ToString();
        }

        public static string SaveToFile(Passenger p)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{p.Id};{p.Name};{p.Email}");

            return stringBuilder.ToString();
        }
    }
}
