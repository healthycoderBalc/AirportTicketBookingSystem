using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem
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

    }
}
