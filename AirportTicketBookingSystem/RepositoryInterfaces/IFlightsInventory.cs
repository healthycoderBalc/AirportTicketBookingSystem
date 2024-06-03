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
    }
}
