using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem.RepositoryInterfaces;
using AirportTicketBookingSystem.Users;
using AirportTicketBookingSystem.Utilities.StorageUtilities;

namespace AirportTicketBookingSystem
{
    public class BookingRepository : IBookingRepository
    {
        public List<Booking> Bookings { get; } = new List<Booking>();
        public List<int> UsedIds { get; } = new List<int>() { 0 };

        private readonly IPassengerRepository _passengerRepository;
        private readonly IFlightsInventory _flightsInventory;

        public BookingRepository(IPassengerRepository passengerRepository, IFlightsInventory flightInventory)
        {
            _passengerRepository = passengerRepository;
            _flightsInventory = flightInventory;
        }

        public static void PrintBookings(List<Booking>? bookings)
        {
            Console.Clear();
            Console.WriteLine("******************************");
            Console.WriteLine("*******   Bookings   *********");
            Console.WriteLine("******************************");

            if (bookings == null)
            {
                Console.WriteLine("No bookings to show");
            }
            else
            {
                foreach (Booking booking in bookings)
                {
                    Console.WriteLine(booking);
                    Console.WriteLine();
                }
            }
        }

        public List<Booking> GetBookingsByFlightId(int flightId)
        {
            List<Booking> bookingsResult = Bookings.Where(b => b.Flight.Id.Equals(flightId)).ToList();
            return bookingsResult;
        }

        public List<Booking> GetBookingsByPrice(double lowerPrice, double upperPrice)
        {
            List<Booking> bookingsResult = Bookings.Where(b => b.FlightAvailability.Price > lowerPrice && b.FlightAvailability.Price < upperPrice).ToList();
            return bookingsResult;
        }

        public List<Booking> GetBookingsByDepartureAirport(string airportName)
        {
            List<Booking> bookingsResult = Bookings.Where(b => b.Flight.DepartureAirport.Name.Equals(airportName)).ToList();
            return bookingsResult;
        }
        public List<Booking> GetBookingsByArrivalAirport(string airportName)
        {
            List<Booking> bookingsResult = Bookings.Where(b => b.Flight.ArrivalAirport.Name.Equals(airportName)).ToList();
            return bookingsResult;
        }

        public List<Booking> GetBookingsByDepartureCountry(string countryName)
        {
            List<Booking> bookingsResult = Bookings.Where(b => b.Flight.DepartureAirport.Country.Name.Equals(countryName)).ToList();
            return bookingsResult;
        }
        public List<Booking> GetBookingsByDestinationCountry(string countryName)
        {
            List<Booking> bookingsResult = Bookings.Where(b => b.Flight.ArrivalAirport.Country.Name.Equals(countryName)).ToList();
            return bookingsResult;
        }

        public List<Booking> GetBookingsByDepartureDate(List<int> date)
        {
            List<Booking> bookingsResult =
             Bookings.Where(b => b.Flight.DepartureDate.Year.Equals(date[0]) && b.Flight.DepartureDate.Month.Equals(date[1]) && b.Flight.DepartureDate.Day.Equals(date[2])).ToList();

            return bookingsResult;
        }

        public List<Booking> GetBookingsByPassengerId(int passengerId)
        {
            List<Booking> bookingsResult = Bookings.Where(b => b.Passenger.Id.Equals(passengerId)).ToList();
            return bookingsResult;
        }

        public List<Booking>? GetBookingsByPassenger(Passenger passenger)
        {
            List<Booking>? bookings = Bookings.Where(b => b.Passenger.Email.Equals(passenger.Email)).ToList();
            return bookings;
        }

        public List<Booking> GetBookingsByClass(int flightClass)
        {
            List<Booking> bookingsResult = Bookings.Where(b => (int)b.FlightAvailability.FlightClass == flightClass).ToList();

            return bookingsResult;
        }
        public void SaveAllBookings()
        {
            StorageBookingUtilities storageBookingUtilities = new StorageBookingUtilities();
            List<Booking> bookings = new List<Booking>();

            if (Bookings != null)
            {
                foreach (var b in Bookings)
                {
                    if (b != null)
                    {
                        bookings.Add(b);
                    }
                }
                if (bookings.Count > 0)
                {
                    storageBookingUtilities.SaveBookingsToFile(bookings);
                }
            }

        }

        public Booking CreateBooking(Flight flight, Passenger passenger, FlightAvailability flightAvailability)
        {
            int lastUsedId = UsedIds.Last();
            // creating booking object with passenger data
            Booking booking = new(lastUsedId + 1, flight, flightAvailability, passenger);

            if (!_passengerRepository.RegisteredPassengers.Contains(passenger))
            {
                throw new InvalidOperationException("Passenger is not registered.");
            }

            if (!_flightsInventory.Flights.Contains(flight))
            {
                throw new InvalidOperationException("Flight does not exist.");
            }

            if (!flight.FlightAvailabilities.Contains(flightAvailability))
            {
                throw new InvalidOperationException("That Flight Class does not exist in the selected flight.");
            }

            Bookings.Add(booking);
            UsedIds.Add(booking.Id);

            return booking;
        }
    }
}
