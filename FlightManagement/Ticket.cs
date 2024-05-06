using AirportTicketBookingSystem.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.FlightManagement
{
    public class Ticket
    {
        public FlightAvailability FlightAvailability { get; set; }
        public Passenger Passenger { get; set; }
        public Ticket(FlightAvailability flightAvailability, Passenger passenger)
        {
            FlightAvailability = flightAvailability;
            Passenger = passenger;
        }
    }
}
