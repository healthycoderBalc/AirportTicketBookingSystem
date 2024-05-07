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
                    SearchFlightUtilities.ShowAndLaunchSearchFlightsMenu();
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

            string managePassenger;
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
            List<string> menu = new();
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
    }
}
