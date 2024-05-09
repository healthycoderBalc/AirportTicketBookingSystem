using AirportTicketBookingSystem.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities.StorageUtilities
{
    public class StoragePassengerUtilities
    {
        private string directory = @"D:\Foothill\C#Course\Exercise\Storage\";
        private string passengerFileName = "passengersSaved.csv";

        public void SavePassengersToFile(List<Passenger> passengers)
        {
            StringBuilder sb = new StringBuilder();
            string path = $"{directory}{passengerFileName}";

            sb.Append($"Id;Name;Email");
            sb.AppendLine();
            foreach (Passenger passenger in passengers)
            {
                sb.Append(Passenger.SaveToFile(passenger));
                //sb.Append(Environment.NewLine);
            }

            File.WriteAllText(path, sb.ToString());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Saved Passengers successfully");
            Console.ResetColor();
        }
    }
}
