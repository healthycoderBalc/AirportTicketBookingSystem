using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.RepositoryInterfaces
{
    public interface IBookingRepository
    {
        List<Booking> Bookings { get; }
        List<int> UsedIds { get; }

        List<Booking> GetBookingsByPassengerId(int passengerId);

        List<Booking> GetBookingsByFlightId(int flightId);

        List<Booking> GetBookingsByPrice(double lowerPrice, double upperPrice);

        List<Booking> GetBookingsByDepartureCountry(string countryName);
        List<Booking> GetBookingsByDestinationCountry(string countryName);

        List<Booking> GetBookingsByDepartureDate(List<int> date);

        List<Booking> GetBookingsByDepartureAirport(string airportName);
        List<Booking> GetBookingsByArrivalAirport(string airportName);
        List<Booking> GetBookingsByClass(int flightClass);
        void SaveAllBookings();

        Booking CreateBooking(Flight flight, Passenger passenger, FlightAvailability flightAvailability);
        List<Booking>? GetBookingsByPassenger(Passenger passenger);
    }
}
