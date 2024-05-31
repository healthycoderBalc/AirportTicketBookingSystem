using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem.Users;
using AirportTicketBookingSystem.Utilities.StorageUtilities;

namespace AirportTicketBookingSystem
{
    public static class BookingRepository
    {
        public static List<Booking> Bookings = new List<Booking>();
        public static List<int> UsedIds = new List<int>() { 0 };

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


        public static List<Booking> GetBookingsByFlightId(int flightId)
        {
            List<Booking> bookingsResult = Bookings.Where(b => b.Flight.Id.Equals(flightId)).ToList();
            return bookingsResult;
        }

        public static List<Booking> GetBookingsByPrice(double lowerPrice, double upperPrice)
        {
            List<Booking> bookingsResult = Bookings.Where(b => b.FlightAvailability.Price > lowerPrice && b.FlightAvailability.Price < upperPrice).ToList();
            return bookingsResult;
        }

        public static List<Booking> GetBookingsByDepartureAirport(string airportName)
        {
            List<Booking> bookingsResult = Bookings.Where(b => b.Flight.DepartureAirport.Name.Equals(airportName)).ToList();
            return bookingsResult;
        }
        public static List<Booking> GetBookingsByArrivalAirport(string airportName)
        {
            List<Booking> bookingsResult = Bookings.Where(b => b.Flight.ArrivalAirport.Name.Equals(airportName)).ToList();
            return bookingsResult;
        }

        public static List<Booking> GetBookingsByDepartureCountry(string countryName)
        {
            List<Booking> bookingsResult = Bookings.Where(b => b.Flight.DepartureAirport.Country.Name.Equals(countryName)).ToList();
            return bookingsResult;
        }
        public static List<Booking> GetBookingsByDestinationCountry(string countryName)
        {
            List<Booking> bookingsResult = Bookings.Where(b => b.Flight.ArrivalAirport.Country.Name.Equals(countryName)).ToList();
            return bookingsResult;
        }

        public static List<Booking> GetBookingsByDepartureDate(List<int> date)
        {
            List<Booking> bookingsResult =
             Bookings.Where(b => b.Flight.DepartureDate.Year.Equals(date[0]) && b.Flight.DepartureDate.Month.Equals(date[1]) && b.Flight.DepartureDate.Day.Equals(date[2])).ToList();

            return bookingsResult;
        }

        public static List<Booking> GetBookingsByPassengerId(int passengerId)
        {
            List<Booking> bookingsResult = Bookings.Where(b => b.Passenger.Id.Equals(passengerId)).ToList();
            return bookingsResult;
        }

        public static List<Booking> GetBookingsByClass(int flightClass)
        {
            List<Booking> bookingsResult = Bookings.Where(b => b.FlightAvailability.FlightClass.Equals((FlightClass)flightClass-1)).ToList();
            return bookingsResult;
        }
        public static void SaveAllBookings()
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
    }
}
