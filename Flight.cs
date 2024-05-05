using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.Users;

namespace AirportTicketBookingSystem
{
    public class Flight
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public DateTime DepartureDate { get; set; }
        public Airport DepartureAirport { get; set; }
        public Airport ArrivalAirport { get; set; }
        public FlightClass FlightClass { get; set; }

        public bool Booked { get; set; }

        public Passenger Passenger { get; set; }

        public Flight(int id, double price, DateTime departureDate, Airport departureAirport, Airport arrivalAirport, FlightClass flightClass, bool booked, Passenger passenger)
        {
            Id = id;
            Price = price;
            DepartureDate = departureDate;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            FlightClass = flightClass;
            Booked = booked;
            Passenger = passenger;
        }


    }
}
