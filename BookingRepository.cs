using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.FlightManagement;

namespace AirportTicketBookingSystem
{
    public static class BookingRepository
    {
        public static List<Booking> Bookings = new List<Booking>();
        public static List<int> UsedIds = new List<int>() { 0 };



    }
}
