using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.FlightManagement;
using System.Linq;
using System.Reflection;
using System.Diagnostics.Metrics;
using AirportTicketBookingSystem.RepositoryInterfaces;

namespace AirportTicketBookingSystem
{
    public class FlightsInventory : IFlightsInventory
    {
        public List<Flight> Flights { get;  } = new List<Flight>();
        public List<Country> Countries { get; } = new List<Country>();
        public List<Airport> Airports { get; } = new List<Airport>();

        // ******************************************
        // Searches
        // ******************************************

        public List<Flight> SearchFlightsByAirportOrCountryName(List<Flight> flightsToSearch, string fieldName, string fieldValue, bool country)
        {
            // getting type of flight and property by its name
            Type flightType = typeof(Flight);
            PropertyInfo? property = flightType.GetProperty(fieldName);

            if (property != null)
            {
                IEnumerable<Flight> resultSearch =
                   flightsToSearch.Where(flight =>
                   { // using property to check search
                       var propertyValue = property.GetValue(flight);
                       if (propertyValue != null && propertyValue.GetType() == typeof(Airport))
                       {
                           if (country)
                           {
                               return ((Airport)propertyValue).Country.Name == fieldValue;
                           }
                           else
                           {
                               return ((Airport)propertyValue).Name == fieldValue;
                           }
                       }
                       return false;
                   });
                return resultSearch.ToList();
            }
            else
            {
                return [];
            }
        }

        public static List<Flight> SearchFlightsByAirportName(List<Flight> flightsToSearch, string airportName, bool departure)
        {
            if (flightsToSearch == null || string.IsNullOrWhiteSpace(airportName))
            {
                return [];
            }

            IEnumerable<Flight> resultSearch =
             flightsToSearch.Where(flight => {
                 return departure
                     ? flight.DepartureAirport.Name.Contains(airportName)
                     : flight.ArrivalAirport.Name.Contains(airportName);
             });

            return resultSearch.ToList();
        }

        public static List<Flight> SearchFlightsByCountryName(List<Flight> flightsToSearch, string countryName, bool departure)
        {
            if (flightsToSearch == null || string.IsNullOrWhiteSpace(countryName))
            {
                return [];
            }

            IEnumerable<Flight> resultSearch =
             flightsToSearch.Where(flight => {
                 return departure
                     ? flight.DepartureAirport.Country.Name.Contains(countryName)
                     : flight.ArrivalAirport.Country.Name.Contains(countryName);
             });

            return resultSearch.ToList();
        }



        public static List<Flight> SearchFlightsByClass(List<Flight> flightsToSearch, int fcNumber)
        {

            IEnumerable<Flight> resultSearch =
               flightsToSearch.Where(flight => flight.FlightAvailabilities.Any(availability => ((int)availability.FlightClass == fcNumber && availability.AvailablePlaces > 0)));

            return resultSearch.ToList();
        }

        public static List<Flight> SearchFlightsByPrice(List<Flight> flightsToSearch, List<double> priceRange)
        {
            IEnumerable<Flight> resultSearch =
              flightsToSearch.Where(flight => flight.FlightAvailabilities.Any(availability => (availability.Price >= priceRange[0] && availability.Price <= priceRange[1])));

            return resultSearch.ToList();
        }

        public static List<Flight> SearchFlightsByDate(List<Flight> flightsToSearch, List<int> date)
        {
            IEnumerable<Flight> resultSearch =
              flightsToSearch.Where(flight => flight.DepartureDate.Year.Equals(date[0]) && flight.DepartureDate.Month.Equals(date[1]) && flight.DepartureDate.Day.Equals(date[2]));

            return resultSearch.ToList();
        }


        public static void ShowFlights(List<Flight> flights, int? selectedClass = null)
        {
            if (flights.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine($"*********************************");
                Console.WriteLine($"********* Flights Found *********");
                Console.WriteLine($"*********************************");
                foreach (Flight f in flights)
                {
                    Console.WriteLine();
                    Console.WriteLine(Flight.ShowFlightShort(f));
                    Console.WriteLine(Flight.ShowFlightAvailabilitiesShort(f.FlightAvailabilities, selectedClass ?? null));
                }
                Console.WriteLine($"*********************************");
            }
            else { Console.WriteLine("\nNo flights to show"); }
        }

        public Flight? GetFlightById(int id)
        {
            try
            {
                Flight flight = Flights.Where(f => f.Id.Equals(id)).Single();
                return flight;
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("There is no flight with that number (Id)");
                return null;
            }

        }

    }
}
