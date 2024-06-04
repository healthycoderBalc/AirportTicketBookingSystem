using AirportTicketBookingSystem.FlightManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities.BookingUtilities
{
    public class BookingPresentationUtilities
    {
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
    }
}
