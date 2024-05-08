using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem.Utilities.PassengerUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities.ManagerUtilities
{
    public static class ManagerUtilities
    {
        public static string RequestManagerCode()
        {
            string? code;
            do
            {
                Console.WriteLine("Please write the Manager Code: ");
                code = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(code));
            return code;
        }

        // ******************************************
        // Manager Options
        // ******************************************
        private static List<string> ManagerOptions()
        {
            List<string> menu = new List<string>();
            menu.Add("Filter Bookings");
            menu.Add("Upload flights form CSV document");

            return menu;
        }

        private static void LaunchManagerSelection(string selection)
        {
            switch (selection)
            {
                // Filter Bookings
                case "1":
                    FilterBookingsUtilities.ShowAndLaunchFilterBookingsMenu();
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                // Upload flights from file
                case "2":
                    // pending functionality

                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                //Going back
                case "0":
                    Console.WriteLine();
                    Console.Write("Press Enter to Go back");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Yoy have not selected a valid option, please try again: ");
                    break;
            }

        }

        public static void ShowAndLaunchManagerOptionsMenu()
        {
            List<string> menu = ManagerOptions();
            string title = "Manager Functionality";
            string selectedOption;
            do
            {
                Console.WriteLine();
                selectedOption = Utilities.ShowMenu(menu, title);

                LaunchManagerSelection(selectedOption);
            } while (selectedOption != "0");
        }


    }
}
