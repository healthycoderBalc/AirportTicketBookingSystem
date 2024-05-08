using AirportTicketBookingSystem.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.FlightManagement
{
    public class Booking
    {
        public int Id { get; set; }
        public Flight Flight { get; set; }
        public FlightAvailability FlightAvailability { get; set; }
        public Passenger Passenger { get; set; }
        public Booking(int id, Flight flight,FlightAvailability flightAvailability, Passenger passenger)
        {
            Flight = flight;
            FlightAvailability = flightAvailability;
            Passenger = passenger;
            Id = id;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Booking number: {Id}");
            stringBuilder.AppendLine($"Passenger: {Passenger.Name}");
            stringBuilder.AppendLine($"Flight: {Flight.ShowFlightShort(Flight)}");
            stringBuilder.Append($"Flight Class: {FlightAvailability.FlightClass.ToString()}");
            stringBuilder.Append($" Price: {FlightAvailability.Price}");

            return stringBuilder.ToString();
        }
    }
}
