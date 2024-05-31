using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem.Utilities.LoadingUtilities;
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
            menu.Add("See all Flights");
            menu.Add("Show Validation details");
            menu.Add("Save Data to files");

            return menu;
        }

        private static void LaunchManagerSelection(string selection)
        {
            StorageFlightsUtilities storageFlightsUtilities = new StorageFlightsUtilities();
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
                    
                    List<Flight> flightsLoaded = storageFlightsUtilities.LoadFlightsFromFile();
                    FlightsInventory.Flights = flightsLoaded;
                    Console.WriteLine($"{FlightsInventory.Flights.Count} flights were loaded!");
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                // See al flights
                case "3":
                    FlightsInventory.ShowFlights(FlightsInventory.Flights);
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;


                // Show Flight Validation Details
                case "4":
                    storageFlightsUtilities.FetchAnnotations2();
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                    // save all data to files
                case "5":
                    Users.PassengerRepository.SaveAllPassengers();
                    BookingRepository.SaveAllBookings();
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
