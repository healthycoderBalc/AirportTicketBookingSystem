using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.FlightManagement
{
    public class FlightAvailability
    {
        public FlightClass FlightClass { get; set; }
        public double Price { get; set; }
        public int TotalPlaces { get; set; }

        public int AvailablePlaces { get; set; }

        public FlightAvailability( FlightClass flightClass, double price, int totalPlaces, int availablePlaces)
        {
            FlightClass = flightClass;
            Price = price;
            TotalPlaces = totalPlaces;
            AvailablePlaces = availablePlaces;
        }


    }
}
