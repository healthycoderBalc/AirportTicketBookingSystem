using AirportTicketBookingSystem.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.FlightManagement;

namespace AirportTicketBookingSystem.Utilities
{
    public static class PassengerUtilities
    {
        // ******************************************
        // Manage Passenger Flights
        // ******************************************

        private static List<string> ManagePassengerOptions()
        {
            List<string> menu = new List<string>();
            menu.Add("Book a flight");
            menu.Add("Manage Bookings");

            return menu;
        }

        private static void LaunchManagePassengerSelection(string selection)
        {
            switch (selection)
            {
                // Book a flight
                case "1":
                    ShowAndLaunchSearchFlightsMenu();
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                // Manage Bookings
                case "2":
                    List<string> options = ManageBookingsOptions();
                    string manageBookingsSelection = Utilities.ShowMenu(options);

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

        public static void ShowAndLaunchManagePassengerMenu()
        {
            List<string> menu = ManagePassengerOptions();

            string managePassenger = string.Empty;
            do
            {
                managePassenger = Utilities.ShowMenu(menu);
                LaunchManagePassengerSelection(managePassenger);
            } while (managePassenger != "0");
        }


        // ******************************************
        // Manage Passenger Bookings
        // ******************************************

        private static List<string> ManageBookingsOptions()
        {
            List<string> menu = new List<string>();
            menu.Add("Cancel a Booking");
            menu.Add("Modify a Booking");
            menu.Add("View my bookings");

            return menu;
        }

        private static void LaunchManageBookingsSelection(string selection)
        {
            switch (selection)
            {
                // Cancel Booking
                case "1":
                    // Pending functionality
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                // Modify Booking
                case "2":
                    // Pending functionality
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                // View my Bookings
                case "3":
                    // Pending functionality
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

        public static void ShowAndLaunchManageBookingsMenu()
        {
            List<string> menu = ManageBookingsOptions();

            string manageBookings = string.Empty;
            do
            {
                manageBookings = Utilities.ShowMenu(menu);
                LaunchManageBookingsSelection(manageBookings);
            } while (manageBookings != "0");
        }


        // ******************************************
        // Search Flights
        // ******************************************

        private static List<string> SearchFlightsOptions()
        {
            List<string> menu = new List<string>();
            menu.Add("Price");
            menu.Add("Departure Country");
            menu.Add("Destination Country");
            menu.Add("Departure Date");
            menu.Add("Departure Airport");
            menu.Add("Arrival Airport");
            menu.Add("Flight Class");

            return menu;
        }

        private static void LaunchSearchFlightsSelection(string selection)
        {
            switch (selection)
            {
                // Search By Price
                case "1":
                    // Pending functionality
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                // Search By Departure Country
                case "2":
                    // Pending functionality
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                // Search By Destination Country
                case "3":
                    // Pending functionality

                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                // Search By Departure Date
                case "4":
                    // Pending functionality

                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                // Search By Departure Airport
                case "5":
                    string departureAirport = SearchBy("Departure Airport");
                    List<Flight> searchedFlights = FlightsInventory.SearchFlightsByDepartureAirport(departureAirport);
                    FlightsInventory.ShowFlights(searchedFlights);
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                // Search By Arrival Airport
                case "6":
                    // Pending functionality

                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                // Search By Flight Class

                case "7":
                    // Pending functionality

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

        public static void ShowAndLaunchSearchFlightsMenu()
        {
            List<string> menu = SearchFlightsOptions();
            string title = "Please select a parameter to search flights";
            string searchFlight;
            do
            {
                searchFlight = Utilities.ShowMenu(menu, title);
                LaunchSearchFlightsSelection(searchFlight);
            } while (searchFlight != "0");
        }


        // ******************************************
        // Search By
        // ******************************************

        private static string SearchBy(string by)
        {
            string? searchTerm;
            do
            {
                Console.WriteLine($"Search by {by}");
                Console.WriteLine($"Please write the {by}: ");
                searchTerm = Console.ReadLine();
                Console.Write($"Your search term: {searchTerm}");
            } while (string.IsNullOrEmpty(searchTerm));
            return searchTerm;
        }

    }
}
