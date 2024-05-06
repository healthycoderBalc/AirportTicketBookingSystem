using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.FlightManagement
{
    public class Airport
    {
        public string Name { get; set; }
        public Country Country { get; set; }

        public Airport(string name, Country country)
        {
            Name = name;
            Country = country;
        }

        public override string ToString()
        {
            return $"{Name} - Country: {Country.Name}";
        }

    }
}
