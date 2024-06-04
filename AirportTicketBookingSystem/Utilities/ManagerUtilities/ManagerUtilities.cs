using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem.RepositoryInterfaces;
using AirportTicketBookingSystem.Utilities.LoadingUtilities;
using AirportTicketBookingSystem.Utilities.PassengerUtilities;
using AirportTicketBookingSystem.Utilities.UtilitiesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities.ManagerUtilities
{
    public class ManagerUtilities : IManagerUtilities
    {
        private readonly FilterBookingsUtilities _filterBookingsUtilities;
        private readonly IFlightsInventory _flightsInventory;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly StorageFlightsUtilities _storageFlightsUtilities;
        private readonly IUtilities _utilities;


        public ManagerUtilities(FilterBookingsUtilities filterBookingUtilities, IFlightsInventory flightsInventory, IPassengerRepository passengerRepository, IBookingRepository bookingRepository, StorageFlightsUtilities storageFlightsUtilities, IUtilities utilities)
        {
            _filterBookingsUtilities = filterBookingUtilities;
            _flightsInventory = flightsInventory;
            _passengerRepository = passengerRepository;
            _bookingRepository = bookingRepository;
            _storageFlightsUtilities = storageFlightsUtilities;
            _utilities = utilities;
        }

        public string RequestManagerCode()
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
        private List<string> ManagerOptions()
        {
            List<string> menu = new List<string>();
            menu.Add("Filter Bookings");
            menu.Add("Upload flights form CSV document");
            menu.Add("See all Flights");
            menu.Add("Show Validation details");
            menu.Add("Save Data to files");

            return menu;
        }

        private void LaunchManagerSelection(string selection)
        {
            switch (selection)
            {
                // Filter Bookings
                case "1":
                    _filterBookingsUtilities.ShowAndLaunchFilterBookingsMenu();
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                // Upload flights from file
                case "2":
                    List<Flight> flightsLoaded = _storageFlightsUtilities.LoadFlightsFromFile();
                    _flightsInventory.Flights.AddRange(flightsLoaded);
                    Console.WriteLine($"{_flightsInventory.Flights.Count} flights were loaded!");
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                // See al flights
                case "3":
                    _flightsInventory.ShowFlights(_flightsInventory.Flights);
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;


                // Show Flight Validation Details
                case "4":
                    _storageFlightsUtilities.FetchAnnotations2();
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                    // save all data to files
                case "5":
                    _passengerRepository.SaveAllPassengers();
                    _bookingRepository.SaveAllBookings();
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

        public void ShowAndLaunchManagerOptionsMenu()
        {
            List<string> menu = ManagerOptions();
            string title = "Manager Functionality";
            string selectedOption;
            do
            {
                Console.WriteLine();
                selectedOption = _utilities.ShowMenu(menu, title);

                LaunchManagerSelection(selectedOption);
            } while (selectedOption != "0");
        }


    }
}
