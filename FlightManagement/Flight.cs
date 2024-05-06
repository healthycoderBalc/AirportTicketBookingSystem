using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.Users;

namespace AirportTicketBookingSystem.FlightManagement
{
    public class Flight
    {
        public int Id { get; set; }
        public DateTime DepartureDate { get; set; }
        public Airport DepartureAirport { get; set; }
        public Airport ArrivalAirport { get; set; }
        public List<FlightAvailability> FlightAvailabilities { get; set; }

        public Flight(int id, DateTime departureDate, Airport departureAirport, Airport arrivalAirport, List<FlightAvailability> flightAvailabilities)
        {
            Id = id;
            DepartureDate = departureDate;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            FlightAvailabilities = flightAvailabilities;

        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Id: {Id}");
            stringBuilder.AppendLine("Departure:");
            stringBuilder.AppendLine($"  Airport: {DepartureAirport}");
            stringBuilder.AppendLine($"  Date: {DepartureDate}");
            stringBuilder.AppendLine("Arrival:");
            stringBuilder.AppendLine($"  Airport: {ArrivalAirport}");
            stringBuilder.AppendLine($"Classes:");

            foreach (FlightAvailability flightAvailability in FlightAvailabilities)
            {
                stringBuilder.AppendLine($"  {flightAvailability.FlightClass}: ${flightAvailability.Price} - Places: {flightAvailability.TotalPlaces}");
            }

            return stringBuilder.ToString();
        }


    }
}
