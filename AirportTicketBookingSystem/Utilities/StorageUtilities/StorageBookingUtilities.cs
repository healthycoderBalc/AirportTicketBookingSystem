using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities.StorageUtilities
{
    public class StorageBookingUtilities
    {
        private string directory = @"D:\Foothill\C#Course\Exercise\Storage\";
        private string bookingFileName = "bookingsSaved.csv";

        public void SaveBookingsToFile(List<Booking> bookings)
        {
            StringBuilder sb = new StringBuilder();
            string path = $"{directory}{bookingFileName}";

            sb.Append($"Id;Flight Id;Flight Class; Passenger Id");
            sb.AppendLine();
            foreach (Booking booking in bookings)
            {
                sb.Append(BookingRepository.SaveToFile(booking));
            }

            File.WriteAllText(path, sb.ToString());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Saved Bookings successfully");
            Console.ResetColor();
        }
    }
}
