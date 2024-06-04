using AirportTicketBookingSystem.FlightManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.RepositoryInterfaces
{
    public interface IFlightsInventory
    {
        List<Flight> Flights { get; }
        List<Country> Countries { get; }
        List<Airport> Airports { get; }
        Flight? GetFlightById(int id);
        List<Flight> SearchFlightsByClass(List<Flight> flightsToSearch, int fcNumber);
        List<Flight> SearchFlightsByPrice(List<Flight> flightsToSearch, List<double> priceRange);
        List<Flight> SearchFlightsByCountryName(List<Flight> flightsToSearch, string countryName, bool departure);
        List<Flight> SearchFlightsByAirportName(List<Flight> flightsToSearch, string airportName, bool departure);
        List<Flight> SearchFlightsByDate(List<Flight> flightsToSearch, List<int> date);
        void ShowFlights(List<Flight> flights, int? selectedClass = null);
    }
}
